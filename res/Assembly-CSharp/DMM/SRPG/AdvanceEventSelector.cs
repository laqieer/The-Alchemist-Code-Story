// Decompiled with JetBrains decompiler
// Type: SRPG.AdvanceEventSelector
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
  [FlowNode.Pin(1, "セレクト開始", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "戻るボタン", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(101, "セレクトした", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "ホームへ戻る", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(111, "バナーがひとつもなかった", FlowNode.PinTypes.Output, 111)]
  public class AdvanceEventSelector : MonoBehaviour, IFlowInterface
  {
    [SerializeField]
    private GameObject Window;
    [Space(5f)]
    [SerializeField]
    private SRPG_ScrollRect ScrollRectController;
    [SerializeField]
    private GameObject GoSelParent;
    [SerializeField]
    private AdvanceEventSelectorItem SelBaseItem;
    [Space(5f)]
    [SerializeField]
    private bool IsSortUpper;
    [Space(5f)]
    [SerializeField]
    private GameObject NoItem;
    private const int PIN_IN_START = 1;
    private const int PIN_IN_BUTTON_BACK = 2;
    private const int PIN_OUT_SELECTED = 101;
    private const int PIN_OUT_BACK_TO_HOME = 102;
    private const int PIN_OUT_NOTHING_BANNER = 111;
    private List<ChapterParam> mChapterParams = new List<ChapterParam>();
    private List<AdvanceEventParam> mAdEvParams = new List<AdvanceEventParam>();
    private List<AdvanceEventSelectorItem> mAdEvSelectorItems = new List<AdvanceEventSelectorItem>();
    private Transform mSelectItemParent;

    private void Awake()
    {
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.Window))
        return;
      this.Window.SetActive(false);
    }

    private void Init()
    {
      GlobalVars.SelectedQuestID = string.Empty;
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.GoSelParent) && UnityEngine.Object.op_Implicit((UnityEngine.Object) this.SelBaseItem))
      {
        ((Component) this.SelBaseItem).gameObject.SetActive(false);
        this.mSelectItemParent = this.GoSelParent.transform;
      }
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) instance) || instance.AdvanceEventParamList == null || instance.AdvanceEventParamList.Count == 0)
        return;
      this.mAdEvParams = new List<AdvanceEventParam>((IEnumerable<AdvanceEventParam>) instance.AdvanceEventParamList);
      if (!this.IsSortUpper)
        this.mAdEvParams.Reverse();
      this.mChapterParams = new List<ChapterParam>();
      foreach (AdvanceEventParam mAdEvParam in this.mAdEvParams)
      {
        ChapterParam chapter = mAdEvParam.ChapterParam;
        if (chapter != null && AdvanceEventSelector.IsChapterQuestCond(chapter))
        {
          if (!this.mChapterParams.Contains(chapter))
            this.mChapterParams.Add(chapter);
          if (mAdEvParam.TransType != eTransType.EventList)
          {
            while (chapter.parent != null)
            {
              chapter = chapter.parent;
              if (!this.mChapterParams.Contains(chapter))
                this.mChapterParams.Add(chapter);
            }
          }
        }
      }
      ChapterParam current_chapter = AdvanceManager.CurrentChapterParam;
      if (current_chapter != null && this.mAdEvParams.FindIndex((Predicate<AdvanceEventParam>) (ad_ev_param => ad_ev_param.ChapterParam == current_chapter)) >= 0)
        AdvanceManager.CurrentChapterParam = current_chapter.parent;
      this.CreateAdvanceEventList(AdvanceManager.CurrentChapterParam);
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.Window))
        return;
      this.Window.SetActive(true);
    }

    private void CreateAdvanceEventList(ChapterParam current_chapter)
    {
      AdvanceManager.CurrentChapterParam = current_chapter;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      List<ChapterParam> chapterParamList1 = new List<ChapterParam>();
      List<ChapterParam> chapterParamList2 = current_chapter != null ? current_chapter.children : this.mChapterParams.FindAll((Predicate<ChapterParam>) (param => param.parent == null || this.mAdEvParams.FindIndex((Predicate<AdvanceEventParam>) (ad_param => param == ad_param.ChapterParam && ad_param.TransType == eTransType.EventList)) >= 0));
      if (this.mAdEvSelectorItems != null && this.mAdEvSelectorItems.Count > 0)
        this.mAdEvSelectorItems.ForEach((Action<AdvanceEventSelectorItem>) (item => ((Component) item).gameObject.SetActive(false)));
      long serverTime = Network.GetServerTime();
      bool flag = false;
      foreach (ChapterParam chapterParam in chapterParamList2)
      {
        ChapterParam child = chapterParam;
        if (AdvanceEventSelector.IsDateUnlock(child, serverTime))
        {
          AdvanceEventParam event_param = (AdvanceEventParam) null;
          if (child.children == null || child.children.Count <= 0)
          {
            event_param = this.mAdEvParams.Find((Predicate<AdvanceEventParam>) (ad_ev_param => ad_ev_param.ChapterParam == child));
            if (event_param == null)
              continue;
          }
          AdvanceEventSelectorItem eventSelectorItem = this.mAdEvSelectorItems.Find((Predicate<AdvanceEventSelectorItem>) (data => data.EventParam == event_param && data.ChapterParam == child));
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) eventSelectorItem, (UnityEngine.Object) null))
          {
            eventSelectorItem = UnityEngine.Object.Instantiate<AdvanceEventSelectorItem>(this.SelBaseItem, this.mSelectItemParent, false);
            DataSource.Bind<AdvanceEventParam>(((Component) eventSelectorItem).gameObject, event_param);
            DataSource.Bind<ChapterParam>(((Component) eventSelectorItem).gameObject, child);
            DataSource.Bind<AdvanceEventSelector>(((Component) eventSelectorItem).gameObject, this);
            eventSelectorItem.SetItem();
            this.mAdEvSelectorItems.Add(eventSelectorItem);
          }
          ((Component) eventSelectorItem).gameObject.SetActive(true);
          eventSelectorItem.Refresh();
          flag = true;
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NoItem, (UnityEngine.Object) null))
        this.NoItem.SetActive(!flag);
      if (flag)
      {
        if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.ScrollRectController))
          return;
        this.ScrollRectController.ResetVerticalPosition(true);
      }
      else
      {
        if (current_chapter != null)
          return;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 111);
      }
    }

    public void OnSelectItem(AdvanceEventSelectorItem item)
    {
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) item))
        return;
      ChapterParam chapterParam = item.ChapterParam;
      if (item.EventParam != null)
      {
        AdvanceManager.CurrentEventParam = item.EventParam;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
      }
      else
        this.CreateAdvanceEventList(chapterParam);
    }

    public static bool IsDateUnlock(ChapterParam chapter, long t)
    {
      if (chapter.children == null || chapter.children.Count <= 0)
        return chapter.IsDateUnlock(t);
      foreach (ChapterParam child in chapter.children)
      {
        if (AdvanceEventSelector.IsDateUnlock(child, t))
          return true;
      }
      return false;
    }

    public static bool IsChapterQuestCond(ChapterParam chapter)
    {
      if (chapter.children == null || chapter.children.Count <= 0)
        return chapter.IsQuestCondition();
      foreach (ChapterParam child in chapter.children)
      {
        if (AdvanceEventSelector.IsChapterQuestCond(child))
          return true;
      }
      return false;
    }

    public static bool IsKeyUnlock(ChapterParam chapter, long t)
    {
      if (chapter.children == null || chapter.children.Count <= 0)
        return chapter.IsKeyUnlock(t);
      foreach (ChapterParam child in chapter.children)
      {
        if (AdvanceEventSelector.IsKeyUnlock(child, t))
          return true;
      }
      return false;
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          this.Init();
          break;
        case 2:
          if (AdvanceManager.CurrentChapterParam == null)
          {
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
            break;
          }
          this.CreateAdvanceEventList(AdvanceManager.CurrentChapterParam.parent);
          break;
      }
    }
  }
}
