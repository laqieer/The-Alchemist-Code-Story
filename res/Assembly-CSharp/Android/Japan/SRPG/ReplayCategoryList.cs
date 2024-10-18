﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ReplayCategoryList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

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
    private List<int> QuestCount;

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
      return quest != null && !quest.IsMulti && quest.IsReplayDateUnlock(Network.GetServerTime()) && ((!string.IsNullOrEmpty(quest.event_start) || !string.IsNullOrEmpty(quest.event_clear)) && (quest.state == QuestStates.Challenged || quest.state == QuestStates.Cleared));
    }

    private void Initialize()
    {
      this.QuestCount = new List<int>(Enum.GetValues(typeof (ReplayCategoryList.ReplayCategoryType)).Length);
      for (int index = 0; index < this.QuestCount.Capacity; ++index)
        this.QuestCount.Add(0);
      QuestParam[] availableQuests = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
      long serverTime = Network.GetServerTime();
      foreach (QuestParam questParam in availableQuests)
      {
        if (!string.IsNullOrEmpty(questParam.ChapterID) && !questParam.IsMulti && questParam.IsReplayDateUnlock(serverTime) && ((!string.IsNullOrEmpty(questParam.event_start) || !string.IsNullOrEmpty(questParam.event_clear)) && (questParam.state == QuestStates.Challenged || questParam.state == QuestStates.Cleared)))
        {
          if (questParam.type == QuestTypes.Story)
            this.AddQuestCount(ReplayCategoryList.ReplayCategoryType.Story);
          else if (questParam.Chapter != null && (questParam.Chapter.IsSeiseki() || questParam.Chapter.IsBabel()))
            this.AddQuestCount(ReplayCategoryList.ReplayCategoryType.Story);
          else if (questParam.type == QuestTypes.Event || questParam.type == QuestTypes.GenesisStory)
            this.AddQuestCount(ReplayCategoryList.ReplayCategoryType.Event);
          else if (questParam.type == QuestTypes.Character)
            this.AddQuestCount(ReplayCategoryList.ReplayCategoryType.Character);
        }
      }
    }

    private void AddQuestCount(ReplayCategoryList.ReplayCategoryType type)
    {
      int num = (int) type;
      try
      {
        List<int> questCount;
        int index;
        (questCount = this.QuestCount)[index = num] = questCount[index] + 1;
      }
      catch (Exception ex)
      {
        DebugUtility.LogException(ex);
      }
    }

    private int GetQuestCount(ReplayCategoryList.ReplayCategoryType type)
    {
      int index = (int) type;
      try
      {
        return this.QuestCount[index];
      }
      catch (Exception ex)
      {
        DebugUtility.LogException(ex);
        return 0;
      }
    }

    private void Awake()
    {
      Action<ListItemEvents, bool> action = (Action<ListItemEvents, bool>) ((lie, value) =>
      {
        if (!((UnityEngine.Object) lie != (UnityEngine.Object) null) || !lie.gameObject.activeInHierarchy)
          return;
        lie.gameObject.SetActive(value);
      });
      action(this.Item_Story, false);
      action(this.Item_Event, false);
      action(this.Item_Character, false);
      action(this.Item_Movie, false);
    }

    private void Update()
    {
      if (!((UnityEngine.Object) this.mCanvasGroup != (UnityEngine.Object) null) || (double) this.mCanvasGroup.alpha >= 1.0)
        return;
      this.mCanvasGroup.alpha = Mathf.Clamp01(this.mCanvasGroup.alpha + Time.unscaledDeltaTime * 3.333333f);
    }

    private void Refresh()
    {
      if ((UnityEngine.Object) this.mCanvasGroup != (UnityEngine.Object) null)
        this.mCanvasGroup.alpha = 0.0f;
      this.ClearItems();
      Transform _transform = this.transform;
      Action<ListItemEvents, int> action = (Action<ListItemEvents, int>) ((lie, outPinID) =>
      {
        if ((UnityEngine.Object) lie == (UnityEngine.Object) null)
          return;
        ListItemEvents listItemEvents = UnityEngine.Object.Instantiate<ListItemEvents>(lie);
        listItemEvents.transform.SetParent(_transform, false);
        listItemEvents.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect);
        listItemEvents.gameObject.SetActive(true);
        DataSource.Bind<int>(listItemEvents.gameObject, outPinID, false);
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

    private enum ReplayCategoryType
    {
      Story,
      Event,
      Character,
      Movie,
    }
  }
}
