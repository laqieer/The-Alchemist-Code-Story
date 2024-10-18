// Decompiled with JetBrains decompiler
// Type: SRPG.ReplayQuestChapterList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(0, "再読み込み", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "閲覧可能なストーリーがある", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(11, "閲覧可能なストーリーがない", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(12, "アイテムが選択された", FlowNode.PinTypes.Output, 102)]
  public class ReplayQuestChapterList : MonoBehaviour, IFlowInterface
  {
    public bool Descending = true;
    private List<ListItemEvents> mItems = new List<ListItemEvents>();
    private const int PIN_ID_REFRESH = 0;
    private const int PIN_ID_EXIST = 10;
    private const int PIN_ID_NOT_EXIST = 11;
    private const int PIN_ID_SELECT = 12;
    public ListItemEvents ItemTemplate;
    public GameObject ItemContainer;
    public GameObject BackToSection;
    public GameObject BackToCategories;
    public ScrollRect ScrollRect;
    private QuestParam[] questParams;
    private string sectionName;

    private void Start()
    {
      if ((UnityEngine.Object) this.ItemTemplate != (UnityEngine.Object) null)
        this.ItemTemplate.gameObject.SetActive(false);
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
      if (this.sectionName == null)
        this.sectionName = (string) GlobalVars.ReplaySelectedSection;
      else if (this.sectionName.Equals((string) GlobalVars.ReplaySelectedSection))
        return;
      this.sectionName = (string) GlobalVars.ReplaySelectedSection;
      GameUtility.DestroyGameObjects<ListItemEvents>(this.mItems);
      this.mItems.Clear();
      if ((UnityEngine.Object) this.ItemTemplate == (UnityEngine.Object) null || (UnityEngine.Object) this.ItemContainer == (UnityEngine.Object) null)
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      List<ReplayQuestChapterList.ReplayChapterParam> replayChapterParamList = new List<ReplayQuestChapterList.ReplayChapterParam>();
      QuestParam[] availableQuests = instance.Player.AvailableQuests;
      long serverTime = Network.GetServerTime();
      foreach (ChapterParam chapter in instance.Chapters)
      {
        bool flag1 = false;
        bool flag2 = false;
        foreach (QuestParam questParam in availableQuests)
        {
          if (!(questParam.ChapterID != chapter.iname) && !questParam.IsMulti && (questParam.type != QuestTypes.Beginner && questParam.IsReplayDateUnlock(serverTime)) && ((!string.IsNullOrEmpty(questParam.event_start) || !string.IsNullOrEmpty(questParam.event_clear)) && (questParam.state == QuestStates.Challenged || questParam.state == QuestStates.Cleared)))
          {
            if (questParam.replayLimit && questParam.end > 0L)
              flag2 = true;
            flag1 = true;
            break;
          }
        }
        if (flag1 && (string.IsNullOrEmpty((string) GlobalVars.ReplaySelectedSection) || chapter.section == (string) GlobalVars.ReplaySelectedSection || (string) GlobalVars.ReplaySelectedSection == "WD_DAILY" && chapter.section == "WD_GENESIS"))
          replayChapterParamList.Add(new ReplayQuestChapterList.ReplayChapterParam()
          {
            chapterParam = chapter,
            replayLimit = flag2
          });
      }
      if (this.Descending)
        replayChapterParamList.Reverse();
      foreach (ReplayQuestChapterList.ReplayChapterParam replayChapterParam in replayChapterParamList)
      {
        ChapterParam chapterParam = replayChapterParam.chapterParam;
        ListItemEvents original = (ListItemEvents) null;
        if (!string.IsNullOrEmpty(chapterParam.prefabPath))
        {
          StringBuilder stringBuilder = GameUtility.GetStringBuilder();
          stringBuilder.Append("QuestChapters/");
          stringBuilder.Append(chapterParam.prefabPath);
          original = AssetManager.Load<ListItemEvents>(stringBuilder.ToString());
        }
        if ((UnityEngine.Object) original == (UnityEngine.Object) null)
          original = this.ItemTemplate;
        ListItemEvents listItemEvents = UnityEngine.Object.Instantiate<ListItemEvents>(original);
        DataSource.Bind<ChapterParam>(listItemEvents.gameObject, chapterParam, false);
        listItemEvents.transform.SetParent(this.ItemContainer.transform, false);
        listItemEvents.gameObject.SetActive(true);
        listItemEvents.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect);
        if (chapterParam.end > 0L)
          this.SetTimerActive(listItemEvents.transform, replayChapterParam.replayLimit);
        this.mItems.Add(listItemEvents);
      }
      if ((UnityEngine.Object) this.BackToCategories != (UnityEngine.Object) null && (UnityEngine.Object) this.BackToSection != (UnityEngine.Object) null)
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
      if ((UnityEngine.Object) this.ScrollRect != (UnityEngine.Object) null)
        this.ScrollRect.normalizedPosition = Vector2.one;
      if (this.mItems.Count > 0)
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
      else
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 11);
    }

    private void SetTimerActive(Transform tr, bool value)
    {
      if ((UnityEngine.Object) tr == (UnityEngine.Object) null)
        return;
      QuestTimeLimit component = tr.GetComponent<QuestTimeLimit>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      if ((UnityEngine.Object) component.Body != (UnityEngine.Object) null)
      {
        component.Body.SetActive(value);
      }
      else
      {
        Transform transform1 = tr.Find("bg");
        if ((UnityEngine.Object) transform1 != (UnityEngine.Object) null)
        {
          Transform transform2 = transform1.Find("timer_base");
          if ((UnityEngine.Object) transform2 != (UnityEngine.Object) null)
            transform2.gameObject.SetActive(value);
        }
      }
      component.enabled = value;
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
