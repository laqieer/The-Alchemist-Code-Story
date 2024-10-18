// Decompiled with JetBrains decompiler
// Type: SRPG.ReplayQuestChapterList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(0, "再読み込み", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "閲覧可能なストーリーがある", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(11, "閲覧可能なストーリーがない", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(12, "アイテムが選択された", FlowNode.PinTypes.Output, 102)]
  public class ReplayQuestChapterList : MonoBehaviour, IFlowInterface
  {
    private const int PIN_ID_REFRESH = 0;
    private const int PIN_ID_EXIST = 10;
    private const int PIN_ID_NOT_EXIST = 11;
    private const int PIN_ID_SELECT = 12;
    public ListItemEvents ItemTemplate;
    public GameObject ItemContainer;
    public bool Descending = true;
    public GameObject BackToSection;
    public GameObject BackToCategories;
    public ScrollRect ScrollRect;
    private string sectionName;
    private List<ListItemEvents> mItems = new List<ListItemEvents>();

    private void Start()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ItemTemplate, (UnityEngine.Object) null))
        ((Component) this.ItemTemplate).gameObject.SetActive(false);
      this.Refresh();
    }

    public void Activated(int pinID)
    {
      if (pinID != 0)
        return;
      this.Refresh();
    }

    private void Refresh()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ItemTemplate, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.ItemContainer, (UnityEngine.Object) null))
        return;
      if (this.sectionName == null)
        this.sectionName = (string) GlobalVars.ReplaySelectedSection;
      else if (this.sectionName.Equals((string) GlobalVars.ReplaySelectedSection))
        return;
      this.sectionName = (string) GlobalVars.ReplaySelectedSection;
      GameUtility.DestroyGameObjects<ListItemEvents>(this.mItems);
      this.mItems.Clear();
      GameManager instance = MonoSingleton<GameManager>.Instance;
      List<ReplayQuestChapterList.ReplayChapterParam> replayChapterParamList = new List<ReplayQuestChapterList.ReplayChapterParam>();
      QuestParam[] availableQuests = instance.Player.AvailableQuests;
      List<string> stringList = new List<string>();
      foreach (ChapterParam chapter1 in instance.Chapters)
      {
        ChapterParam chapter = chapter1;
        bool flag1 = false;
        bool flag2 = false;
        QuestParam[] all = Array.FindAll<QuestParam>(availableQuests, (Predicate<QuestParam>) (quest => quest.ChapterID == chapter.iname));
        if (all != null && all.Length > 0)
        {
          QuestParam[] questParamArray = all;
          int index = 0;
          if (index < questParamArray.Length)
          {
            QuestParam questParam = questParamArray[index];
            if (!string.IsNullOrEmpty(questParam.event_start))
            {
              if (!stringList.Contains(questParam.event_start))
              {
                if (ReplayQuestListManager.Instance.PlayableScenario.Contains(questParam.event_start))
                {
                  flag1 = true;
                  stringList.Add(questParam.event_start);
                }
              }
              else
                goto label_18;
            }
            if (!string.IsNullOrEmpty(questParam.event_clear))
            {
              if (!stringList.Contains(questParam.event_clear))
              {
                if (ReplayQuestListManager.Instance.PlayableScenario.Contains(questParam.event_clear))
                {
                  flag1 = true;
                  stringList.Add(questParam.event_clear);
                }
              }
              else
                goto label_18;
            }
            if (questParam.replayLimit && questParam.end > 0L)
              flag2 = true;
          }
label_18:
          if (flag1 && (string.IsNullOrEmpty((string) GlobalVars.ReplaySelectedSection) || chapter.section == (string) GlobalVars.ReplaySelectedSection || (string) GlobalVars.ReplaySelectedSection == "WD_DAILY" && (chapter.section == "WD_GENESIS" || chapter.section == "WD_ADVANCE" || chapter.section == "WD_RENTAL")))
            replayChapterParamList.Add(new ReplayQuestChapterList.ReplayChapterParam()
            {
              chapterParam = chapter,
              replayLimit = flag2
            });
        }
      }
      if (this.Descending)
        replayChapterParamList.Reverse();
      foreach (ReplayQuestChapterList.ReplayChapterParam replayChapterParam in replayChapterParamList)
      {
        ChapterParam chapterParam = replayChapterParam.chapterParam;
        ListItemEvents listItemEvents1 = (ListItemEvents) null;
        if (!string.IsNullOrEmpty(chapterParam.prefabPath))
        {
          StringBuilder stringBuilder = GameUtility.GetStringBuilder();
          stringBuilder.Append("QuestChapters/");
          stringBuilder.Append(chapterParam.prefabPath);
          listItemEvents1 = AssetManager.Load<ListItemEvents>(stringBuilder.ToString());
        }
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) listItemEvents1, (UnityEngine.Object) null))
          listItemEvents1 = this.ItemTemplate;
        ListItemEvents listItemEvents2 = UnityEngine.Object.Instantiate<ListItemEvents>(listItemEvents1);
        DataSource.Bind<ChapterParam>(((Component) listItemEvents2).gameObject, chapterParam);
        EventQuestBanner component = ((Component) listItemEvents2).gameObject.GetComponent<EventQuestBanner>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          component.SetHideChallengeCounter(true);
        ((Component) listItemEvents2).transform.SetParent(this.ItemContainer.transform, false);
        ((Component) listItemEvents2).gameObject.SetActive(true);
        listItemEvents2.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect);
        if (chapterParam.end > 0L)
          this.SetTimerActive(((Component) listItemEvents2).transform, replayChapterParam.replayLimit);
        this.mItems.Add(listItemEvents2);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BackToCategories, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BackToSection, (UnityEngine.Object) null))
      {
        bool flag = false;
        foreach (SectionParam section in instance.Sections)
        {
          if (section.iname == (string) GlobalVars.ReplaySelectedSection)
          {
            flag = !(section.iname == "WD_SEISEKI") && !(section.iname == "WD_BABEL") && section.hidden;
            break;
          }
        }
        this.BackToCategories.SetActive(flag);
        this.BackToSection.SetActive(!flag);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ScrollRect, (UnityEngine.Object) null))
        this.ScrollRect.normalizedPosition = Vector2.one;
      if (this.mItems.Count > 0)
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
      else
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 11);
    }

    private void SetTimerActive(Transform tr, bool value)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) tr, (UnityEngine.Object) null))
        return;
      QuestTimeLimit component = ((Component) tr).GetComponent<QuestTimeLimit>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component.Body, (UnityEngine.Object) null))
      {
        component.Body.SetActive(value);
      }
      else
      {
        Transform transform1 = tr.Find("bg");
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) transform1, (UnityEngine.Object) null))
        {
          Transform transform2 = transform1.Find("timer_base");
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) transform2, (UnityEngine.Object) null))
            ((Component) transform2).gameObject.SetActive(value);
        }
      }
      ((Behaviour) component).enabled = value;
    }

    private void OnItemSelect(GameObject go)
    {
      ChapterParam dataOfClass = DataSource.FindDataOfClass<ChapterParam>(go, (ChapterParam) null);
      if (dataOfClass == null)
        return;
      GlobalVars.ReplaySelectedChapter.Set(dataOfClass.iname);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 12);
    }

    private class ReplayChapterParam
    {
      internal ChapterParam chapterParam;
      internal bool replayLimit;
    }
  }
}
