// Decompiled with JetBrains decompiler
// Type: SRPG.ReplayQuestList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(0, "更新", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "クエストが選択された", FlowNode.PinTypes.Output, 101)]
  public class ReplayQuestList : SRPG_ListBase, IFlowInterface
  {
    private const int PIN_ID_REFRESH = 0;
    private const int PIN_ID_SELECT = 10;
    public GameObject ItemTemplate;
    public GameObject ItemContainer;
    public Dictionary<string, GameObject> mListItemTemplates = new Dictionary<string, GameObject>();
    public bool Descending = true;
    public bool RefreshOnStart = true;
    public bool ShowAllQuests = true;
    public ScrollRect ScrollRect;
    private List<QuestParam> mQuests = new List<QuestParam>();
    private string chapterName;

    public void Activated(int pinID)
    {
      if (pinID != 0)
        return;
      this.Refresh();
    }

    protected override void Start()
    {
      base.Start();
      ((Action<GameObject, bool>) ((lie, value) =>
      {
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) lie, (UnityEngine.Object) null) || !lie.activeInHierarchy)
          return;
        lie.SetActive(value);
      }))(this.ItemTemplate, false);
      this.Refresh();
    }

    private GameObject LoadQuestListItem(QuestParam param)
    {
      if (string.IsNullOrEmpty(param.ItemLayout))
        return (GameObject) null;
      if (this.mListItemTemplates.ContainsKey(param.ItemLayout))
        return this.mListItemTemplates[param.ItemLayout];
      GameObject gameObject = AssetManager.Load<GameObject>("QuestListItems/" + param.ItemLayout);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
        this.mListItemTemplates.Add(param.ItemLayout, gameObject);
      return gameObject;
    }

    public void Refresh()
    {
      if (this.chapterName == null)
        this.chapterName = (string) GlobalVars.ReplaySelectedChapter;
      else if (this.chapterName.Equals((string) GlobalVars.ReplaySelectedChapter))
        return;
      this.chapterName = (string) GlobalVars.ReplaySelectedChapter;
      this.RefreshQuests();
      this.RefreshItems();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ScrollRect, (UnityEngine.Object) null))
        return;
      this.ScrollRect.normalizedPosition = Vector2.one;
    }

    private static bool CheckQuest(QuestParam quest, bool show_all_quests = false)
    {
      return !quest.IsMulti && quest.IsReplayDateUnlock() && (!string.IsNullOrEmpty(quest.event_start) || !string.IsNullOrEmpty(quest.event_clear)) && (quest.state == QuestStates.Challenged || quest.state == QuestStates.Cleared) && (quest.state != QuestStates.Challenged || !string.IsNullOrEmpty(quest.event_start)) && (show_all_quests || !((string) GlobalVars.ReplaySelectedChapter != quest.ChapterID));
    }

    private void RefreshQuests()
    {
      this.mQuests.Clear();
      this.mQuests = ReplayQuestList.GetSelectedChapterReplayQuests(this.ShowAllQuests);
    }

    private static List<QuestParam> GetSelectedChapterReplayQuests(bool show_all_quests = false)
    {
      List<QuestParam> chapterReplayQuests = new List<QuestParam>();
      foreach (QuestParam questParam in Array.FindAll<QuestParam>(MonoSingleton<GameManager>.Instance.Quests, (Predicate<QuestParam>) (q => q.ChapterID == (string) GlobalVars.ReplaySelectedChapter)))
      {
        if (show_all_quests || !string.IsNullOrEmpty(questParam.event_start) && ReplayQuestListManager.Instance.PlayableScenario.Contains(questParam.event_start) || !string.IsNullOrEmpty(questParam.event_clear) && ReplayQuestListManager.Instance.PlayableScenario.Contains(questParam.event_clear))
          chapterReplayQuests.Add(questParam);
      }
      return chapterReplayQuests;
    }

    private void RefreshItems()
    {
      this.ClearItems();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ItemTemplate, (UnityEngine.Object) null))
        return;
      QuestParam[] array = this.mQuests.ToArray();
      if (this.Descending)
        Array.Reverse((Array) array);
      for (int index = 0; index < array.Length; ++index)
      {
        QuestParam data = array[index];
        GameObject gameObject1 = (GameObject) null;
        if (!string.IsNullOrEmpty(data.ItemLayout))
          gameObject1 = this.LoadQuestListItem(data);
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject1, (UnityEngine.Object) null))
          gameObject1 = this.ItemTemplate;
        if (!UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject1, (UnityEngine.Object) null))
        {
          GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject1);
          ((UnityEngine.Object) gameObject2).hideFlags = (HideFlags) 52;
          DataSource.Bind<QuestParam>(gameObject2, data);
          ListItemEvents component = gameObject2.GetComponent<ListItemEvents>();
          component.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelectItem);
          if (data.type == QuestTypes.Event)
            this.SetQuestTimerActive(gameObject2.transform, false);
          gameObject2.transform.SetParent(this.ItemContainer.transform, false);
          gameObject2.gameObject.SetActive(true);
          this.AddItem(component);
        }
      }
    }

    private void SetQuestTimerActive(Transform obj, bool value)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) obj, (UnityEngine.Object) null))
        return;
      QuestTimeLimit component = ((Component) obj).GetComponent<QuestTimeLimit>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        ((Behaviour) component).enabled = value;
      Transform transform1 = obj.Find("bg");
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) transform1, (UnityEngine.Object) null))
        return;
      Transform transform2 = transform1.Find("timer_base");
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) transform2, (UnityEngine.Object) null))
        return;
      ((Component) transform2).gameObject.SetActive(value);
    }

    private void OnSelectItem(GameObject go)
    {
      QuestParam dataOfClass = DataSource.FindDataOfClass<QuestParam>(go, (QuestParam) null);
      if (dataOfClass == null)
        return;
      GlobalVars.ReplaySelectedQuestID.Set(dataOfClass.iname);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
    }
  }
}
