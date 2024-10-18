// Decompiled with JetBrains decompiler
// Type: SRPG.ReplayCategoryList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(0, "初期化", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(1, "更新", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(10, "Story", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(11, "Event", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(12, "Chara", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(13, "Movie", FlowNode.PinTypes.Output, 103)]
  [FlowNode.Pin(14, "閲覧可能なストーリーがない", FlowNode.PinTypes.Output, 104)]
  [FlowNode.Pin(50, "QuestList(Restore)", FlowNode.PinTypes.Output, 105)]
  public class ReplayCategoryList : SRPG_ListBase, IFlowInterface
  {
    private const int PIN_ID_INIT = 0;
    private const int PIN_ID_REFRESH = 1;
    private const int PIN_ID_STORY = 10;
    private const int PIN_ID_EVENT = 11;
    private const int PIN_ID_CHARA = 12;
    private const int PIN_ID_MOVIE = 13;
    private const int PIN_ID_NOT_EXIST = 14;
    private const int PIN_ID_RESTORE = 50;
    public ListItemEvents Item_Story;
    public ListItemEvents Item_Event;
    public ListItemEvents Item_Character;
    public ListItemEvents Item_Movie;
    public CanvasGroup mCanvasGroup;
    private Dictionary<ReplayCategoryList.ReplayCategoryType, int> QuestCount;

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 0:
          this.Initialize();
          this.Refresh();
          if (HomeWindow.GetRestorePoint() == RestorePoints.ReplayQuestList && this.Resumable(MonoSingleton<GameManager>.Instance.FindQuest((string) GlobalVars.ReplaySelectedQuestID)))
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 50);
          HomeWindow.SetRestorePoint(RestorePoints.Home);
          break;
        case 1:
          this.Refresh();
          break;
      }
    }

    private bool Resumable(QuestParam quest)
    {
      return quest != null && !quest.IsMulti && quest.IsReplayDateUnlock(Network.GetServerTime()) && (!string.IsNullOrEmpty(quest.event_start) || !string.IsNullOrEmpty(quest.event_clear)) && (quest.state == QuestStates.Challenged || quest.state == QuestStates.Cleared);
    }

    private void Initialize()
    {
      this.QuestCount = new Dictionary<ReplayCategoryList.ReplayCategoryType, int>();
      QuestParam[] availableQuests = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
      long serverTime = Network.GetServerTime();
      foreach (QuestParam quest in availableQuests)
      {
        ReplayCategoryList.ReplayCategoryType replayCategoryType = ReplayCategoryList.GetReplayCategoryType(quest, serverTime);
        if (replayCategoryType != ReplayCategoryList.ReplayCategoryType.Invalid)
          this.AddQuestCount(replayCategoryType);
      }
    }

    public static ReplayCategoryList.ReplayCategoryType GetReplayCategoryType(
      QuestParam quest,
      long currentTime)
    {
      if (quest.state != QuestStates.Challenged && quest.state != QuestStates.Cleared || string.IsNullOrEmpty(quest.ChapterID) || !ReplayCategoryList.HasEvent(quest) || quest.IsMulti || !quest.IsReplayDateUnlock(currentTime) || quest.state != QuestStates.Challenged && quest.state != QuestStates.Cleared)
        return ReplayCategoryList.ReplayCategoryType.Invalid;
      if (quest.type == QuestTypes.Story || quest.Chapter != null && (quest.Chapter.IsSeiseki() || quest.Chapter.IsBabel()))
        return ReplayCategoryList.ReplayCategoryType.Story;
      if (quest.type == QuestTypes.Event || quest.type == QuestTypes.GenesisStory || quest.type == QuestTypes.AdvanceStory || quest.type == QuestTypes.UnitRental)
        return ReplayCategoryList.ReplayCategoryType.Event;
      return quest.type == QuestTypes.Character ? ReplayCategoryList.ReplayCategoryType.Character : ReplayCategoryList.ReplayCategoryType.Invalid;
    }

    public static bool HasEvent(QuestParam quest)
    {
      return !string.IsNullOrEmpty(quest.event_start) || !string.IsNullOrEmpty(quest.event_clear);
    }

    private void AddQuestCount(ReplayCategoryList.ReplayCategoryType type)
    {
      if (!this.QuestCount.ContainsKey(type))
      {
        this.QuestCount.Add(type, 1);
      }
      else
      {
        Dictionary<ReplayCategoryList.ReplayCategoryType, int> questCount;
        ReplayCategoryList.ReplayCategoryType key;
        (questCount = this.QuestCount)[key = type] = questCount[key] + 1;
      }
    }

    private int GetQuestCount(ReplayCategoryList.ReplayCategoryType type)
    {
      return this.QuestCount.ContainsKey(type) ? this.QuestCount[type] : 0;
    }

    private void Awake()
    {
      Action<ListItemEvents, bool> action = (Action<ListItemEvents, bool>) ((lie, value) =>
      {
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) lie, (UnityEngine.Object) null) || !((Component) lie).gameObject.activeInHierarchy)
          return;
        ((Component) lie).gameObject.SetActive(value);
      });
      action(this.Item_Story, false);
      action(this.Item_Event, false);
      action(this.Item_Character, false);
      action(this.Item_Movie, false);
    }

    private void Update()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mCanvasGroup, (UnityEngine.Object) null) || (double) this.mCanvasGroup.alpha >= 1.0)
        return;
      this.mCanvasGroup.alpha = Mathf.Clamp01(this.mCanvasGroup.alpha + Time.unscaledDeltaTime * 3.33333325f);
    }

    private void Refresh()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mCanvasGroup, (UnityEngine.Object) null))
        this.mCanvasGroup.alpha = 0.0f;
      this.ClearItems();
      Transform _transform = ((Component) this).transform;
      Action<ListItemEvents, int> action = (Action<ListItemEvents, int>) ((lie, outPinID) =>
      {
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) lie, (UnityEngine.Object) null))
          return;
        ListItemEvents listItemEvents = UnityEngine.Object.Instantiate<ListItemEvents>(lie);
        ((Component) listItemEvents).transform.SetParent(_transform, false);
        listItemEvents.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect);
        ((Component) listItemEvents).gameObject.SetActive(true);
        DataSource.Bind<int>(((Component) listItemEvents).gameObject, outPinID);
        this.AddItem(listItemEvents);
      });
      action(this.Item_Story, 10);
      action(this.Item_Event, 11);
      action(this.Item_Character, 12);
      action(this.Item_Movie, 13);
    }

    private void OnItemSelect(GameObject go)
    {
      int dataOfClass = DataSource.FindDataOfClass<int>(go, 10);
      switch (dataOfClass)
      {
        case 10:
          GlobalVars.ReplaySelectedSection.Set(string.Empty);
          GlobalVars.ReplaySelectedChapter.Set(string.Empty);
          if (this.GetQuestCount(ReplayCategoryList.ReplayCategoryType.Story) > 0)
          {
            FlowNode_GameObject.ActivateOutputLinks((Component) this, dataOfClass);
            break;
          }
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 14);
          break;
        case 11:
          GlobalVars.ReplaySelectedSection.Set("WD_DAILY");
          GlobalVars.ReplaySelectedChapter.Set(string.Empty);
          if (this.GetQuestCount(ReplayCategoryList.ReplayCategoryType.Event) > 0)
          {
            FlowNode_GameObject.ActivateOutputLinks((Component) this, dataOfClass);
            break;
          }
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 14);
          break;
        case 12:
          GlobalVars.ReplaySelectedSection.Set("WD_CHARA");
          GlobalVars.ReplaySelectedChapter.Set(string.Empty);
          if (this.GetQuestCount(ReplayCategoryList.ReplayCategoryType.Character) > 0)
          {
            FlowNode_GameObject.ActivateOutputLinks((Component) this, dataOfClass);
            break;
          }
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 14);
          break;
        case 13:
          FlowNode_GameObject.ActivateOutputLinks((Component) this, dataOfClass);
          break;
      }
    }

    public enum ReplayCategoryType
    {
      Invalid = -1, // 0xFFFFFFFF
      Story = 0,
      Event = 1,
      Character = 2,
      Movie = 3,
    }
  }
}
