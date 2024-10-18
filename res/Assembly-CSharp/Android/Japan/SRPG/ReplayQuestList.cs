// Decompiled with JetBrains decompiler
// Type: SRPG.ReplayQuestList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(0, "更新", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "クエストが選択された", FlowNode.PinTypes.Output, 101)]
  public class ReplayQuestList : SRPG_ListBase, IFlowInterface
  {
    public Dictionary<string, GameObject> mListItemTemplates = new Dictionary<string, GameObject>();
    public bool Descending = true;
    public bool RefreshOnStart = true;
    public bool ShowAllQuests = true;
    private List<QuestParam> mQuests = new List<QuestParam>();
    private const int PIN_ID_REFRESH = 0;
    private const int PIN_ID_SELECT = 10;
    public GameObject ItemTemplate;
    public GameObject ItemContainer;
    public ScrollRect ScrollRect;
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
        if (!((UnityEngine.Object) lie != (UnityEngine.Object) null) || !lie.activeInHierarchy)
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
      if ((UnityEngine.Object) gameObject != (UnityEngine.Object) null)
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
      if (!((UnityEngine.Object) this.ScrollRect != (UnityEngine.Object) null))
        return;
      this.ScrollRect.normalizedPosition = Vector2.one;
    }

    private bool CheckQuest(QuestParam quest)
    {
      return !quest.IsMulti && quest.IsReplayDateUnlock(-1L) && (!string.IsNullOrEmpty(quest.event_start) || !string.IsNullOrEmpty(quest.event_clear)) && ((quest.state == QuestStates.Challenged || quest.state == QuestStates.Cleared) && (quest.state != QuestStates.Challenged || !string.IsNullOrEmpty(quest.event_start))) && (this.ShowAllQuests || !((string) GlobalVars.ReplaySelectedChapter != quest.ChapterID));
    }

    private void RefreshQuests()
    {
      this.mQuests.Clear();
      foreach (QuestParam availableQuest in MonoSingleton<GameManager>.Instance.Player.AvailableQuests)
      {
        if (this.CheckQuest(availableQuest))
          this.mQuests.Add(availableQuest);
      }
    }

    private void RefreshItems()
    {
      this.ClearItems();
      if ((UnityEngine.Object) this.ItemTemplate == (UnityEngine.Object) null)
        return;
      QuestParam[] array = this.mQuests.ToArray();
      if (this.Descending)
        Array.Reverse((Array) array);
      for (int index = 0; index < array.Length; ++index)
      {
        QuestParam data = array[index];
        GameObject original = (GameObject) null;
        if (!string.IsNullOrEmpty(data.ItemLayout))
          original = this.LoadQuestListItem(data);
        if ((UnityEngine.Object) original == (UnityEngine.Object) null)
          original = this.ItemTemplate;
        if (!((UnityEngine.Object) original == (UnityEngine.Object) null))
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
          gameObject.hideFlags = HideFlags.DontSave;
          DataSource.Bind<QuestParam>(gameObject, data, false);
          ListItemEvents component = gameObject.GetComponent<ListItemEvents>();
          component.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelectItem);
          if (data.type == QuestTypes.Event)
            this.SetQuestTimerActive(gameObject.transform, false);
          gameObject.transform.SetParent(this.ItemContainer.transform, false);
          gameObject.gameObject.SetActive(true);
          this.AddItem(component);
        }
      }
    }

    private void SetQuestTimerActive(Transform obj, bool value)
    {
      if ((UnityEngine.Object) obj == (UnityEngine.Object) null)
        return;
      QuestTimeLimit component = obj.GetComponent<QuestTimeLimit>();
      if ((UnityEngine.Object) component != (UnityEngine.Object) null)
        component.enabled = value;
      Transform transform1 = obj.Find("bg");
      if (!((UnityEngine.Object) transform1 != (UnityEngine.Object) null))
        return;
      Transform transform2 = transform1.Find("timer_base");
      if (!((UnityEngine.Object) transform2 != (UnityEngine.Object) null))
        return;
      transform2.gameObject.SetActive(value);
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
