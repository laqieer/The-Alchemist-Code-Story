// Decompiled with JetBrains decompiler
// Type: SRPG.AwardList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(0, "StartUp", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "IsSelectAward", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "StartUpEnd", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(50, "TabNormal", FlowNode.PinTypes.Input, 50)]
  [FlowNode.Pin(51, "TabExtra", FlowNode.PinTypes.Input, 51)]
  [FlowNode.Pin(100, "Select", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "SelectEnd", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "ResetAward", FlowNode.PinTypes.Output, 102)]
  public class AwardList : MonoBehaviour, IFlowInterface
  {
    [SerializeField]
    private GameObject AwardListRoot;
    [SerializeField]
    private Text Pager;
    [SerializeField]
    private SRPG_Button Prev;
    [SerializeField]
    private SRPG_Button Next;
    [SerializeField]
    private GameObject Blank;
    private List<GameObject> mAwardItems;
    private GameManager gm;
    private AwardParam[] mAwards;
    private List<string> mOpenAwards;
    private int mMaxViewItems;
    private int mCurrentPage;
    private int mMaxPage;
    private bool IsRefresh;
    private AwardParam.Tab mCurrentTab = AwardParam.Tab.None;
    private AwardParam.Tab mPrevTab = AwardParam.Tab.None;
    private Dictionary<AwardParam.Tab, List<AwardParam>> mAwardDatas = new Dictionary<AwardParam.Tab, List<AwardParam>>();

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 0:
          this.gm = MonoSingleton<GameManager>.GetInstanceDirect();
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.gm, (UnityEngine.Object) null))
          {
            DebugUtility.LogWarning("AwardList.cs -> Activated():GameManager is Null References!");
            break;
          }
          this.RefreshAwardDatas();
          this.TabChange(AwardParam.Tab.Normal);
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
          break;
        case 1:
          this.IsRefresh = true;
          break;
        case 50:
          this.TabChange(AwardParam.Tab.Normal);
          break;
        case 51:
          this.TabChange(AwardParam.Tab.Extra);
          break;
      }
    }

    private void TabChange(AwardParam.Tab tab)
    {
      this.mPrevTab = this.mCurrentTab;
      if (this.mPrevTab == tab)
        return;
      this.mCurrentTab = tab;
      if (!this.mAwardDatas.ContainsKey(this.mCurrentTab))
        return;
      this.mMaxPage = this.mAwardDatas[this.mCurrentTab].Count % this.mMaxViewItems <= 0 ? this.mAwardDatas[this.mCurrentTab].Count / this.mMaxViewItems : this.mAwardDatas[this.mCurrentTab].Count / this.mMaxViewItems + 1;
      this.mCurrentPage = 0;
      this.IsRefresh = true;
    }

    private void Awake()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AwardListRoot, (UnityEngine.Object) null))
      {
        int childCount = this.AwardListRoot.transform.childCount;
        this.mAwardItems = new List<GameObject>();
        if (childCount > 0)
        {
          for (int index = 0; index < childCount; ++index)
          {
            Transform child = this.AwardListRoot.transform.GetChild(index);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) child, (UnityEngine.Object) null))
            {
              ((Component) child).gameObject.SetActive(false);
              this.mAwardItems.Add(((Component) child).gameObject);
            }
          }
        }
        this.mMaxViewItems = childCount;
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Blank, (UnityEngine.Object) null))
        this.Blank.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Prev, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.Prev.onClick).AddListener(new UnityAction((object) this, __methodptr(OnPrevPage)));
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Next, (UnityEngine.Object) null))
        return;
      // ISSUE: method pointer
      ((UnityEvent) this.Next.onClick).AddListener(new UnityAction((object) this, __methodptr(OnNextPage)));
    }

    private void Start()
    {
    }

    private void Update()
    {
      if (!this.IsRefresh)
        return;
      this.IsRefresh = false;
      this.Refresh();
    }

    private void Refresh()
    {
      if (this.mAwardItems == null || this.mAwardItems.Count <= 0 || this.mAwardDatas == null)
        return;
      foreach (GameObject mAwardItem in this.mAwardItems)
        mAwardItem.SetActive(false);
      this.Blank.SetActive(false);
      AwardParam[] array = !this.mAwardDatas.ContainsKey(this.mCurrentTab) ? (AwardParam[]) null : this.mAwardDatas[this.mCurrentTab].ToArray();
      if (array == null || array.Length <= 0)
      {
        this.Blank.SetActive(true);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Pager, (UnityEngine.Object) null))
          this.Pager.text = LocalizedText.Get("sys.TEXT_PAGER_TEMP", (object) 0, (object) 0);
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Prev, (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Next, (UnityEngine.Object) null))
          return;
        ((Selectable) this.Prev).interactable = false;
        ((Selectable) this.Next).interactable = false;
      }
      else
      {
        int length = array.Length;
        int num1 = this.mMaxViewItems * this.mCurrentPage;
        int num2 = num1 >= length ? 0 : num1;
        int num3 = this.mMaxViewItems * (this.mCurrentPage + 1);
        int num4 = num3 >= length ? length : num3;
        int index1 = num2;
        for (int index2 = 0; index2 < this.mAwardItems.Count; ++index2)
        {
          AwardParam awardParam = index1 >= array.Length ? (AwardParam) null : array[index1];
          if (awardParam != null)
          {
            GameObject mAwardItem = this.mAwardItems[index2];
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) mAwardItem, (UnityEngine.Object) null))
            {
              AwardListItem component1 = mAwardItem.GetComponent<AwardListItem>();
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null))
              {
                mAwardItem.SetActive(true);
                mAwardItem.SetActive(true);
                bool hasItem = this.mOpenAwards != null && this.mOpenAwards.Contains(awardParam.iname);
                component1.SetUp(awardParam.iname, hasItem, awardParam.iname == this.gm.Player.SelectedAward, awardParam.grade <= 0);
                Button component2 = (Button) mAwardItem.GetComponent<SRPG_Button>();
                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component2, (UnityEngine.Object) null))
                {
                  ((UnityEventBase) component2.onClick).RemoveAllListeners();
                  if (awardParam.grade <= 0)
                  {
                    // ISSUE: object of a compiler-generated type is created
                    // ISSUE: method pointer
                    ((UnityEvent) component2.onClick).AddListener(new UnityAction((object) new AwardList.\u003CRefresh\u003Ec__AnonStorey0()
                    {
                      \u0024this = this,
                      iname = awardParam.iname
                    }, __methodptr(\u003C\u003Em__0)));
                    ((Selectable) component2).interactable = true;
                  }
                  else
                  {
                    if (hasItem)
                    {
                      // ISSUE: object of a compiler-generated type is created
                      // ISSUE: method pointer
                      ((UnityEvent) component2.onClick).AddListener(new UnityAction((object) new AwardList.\u003CRefresh\u003Ec__AnonStorey1()
                      {
                        \u0024this = this,
                        iname = awardParam.iname
                      }, __methodptr(\u003C\u003Em__0)));
                    }
                    ((Selectable) component2).interactable = hasItem;
                  }
                }
              }
            }
            if (index1 <= num4)
              ++index1;
          }
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Pager, (UnityEngine.Object) null))
          this.Pager.text = LocalizedText.Get("sys.TEXT_PAGER_TEMP", (object) (this.mCurrentPage + 1).ToString(), (object) this.mMaxPage.ToString());
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Prev, (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Next, (UnityEngine.Object) null))
          return;
        ((Selectable) this.Prev).interactable = this.mCurrentPage - 1 >= 0;
        ((Selectable) this.Next).interactable = this.mCurrentPage + 1 < this.mMaxPage;
      }
    }

    private void OnNextPage()
    {
      this.mCurrentPage = Mathf.Min(this.mCurrentPage + 1, this.mMaxPage);
      this.Refresh();
    }

    private void OnPrevPage()
    {
      this.mCurrentPage = Mathf.Max(this.mCurrentPage - 1, 0);
      this.Refresh();
    }

    private void RefreshAwardDatas()
    {
      this.mAwardDatas.Clear();
      AwardParam[] allAwards = this.gm.MasterParam.GetAllAwards();
      if (allAwards == null || allAwards.Length <= 0)
      {
        DebugUtility.LogWarning("AwardList.cs => RefreshAwardDatas():awards is Null or Count 0.");
      }
      else
      {
        foreach (AwardParam.Tab key in Enum.GetValues(typeof (AwardParam.Tab)))
        {
          if (!this.mAwardDatas.ContainsKey(key))
            this.mAwardDatas.Add(key, new List<AwardParam>());
        }
        for (int index = 0; index < allAwards.Length; ++index)
        {
          AwardParam awardParam = allAwards[index];
          if (allAwards != null)
          {
            AwardParam.Tab tab = (AwardParam.Tab) awardParam.tab;
            if (!this.mAwardDatas.ContainsKey(tab))
              this.mAwardDatas.Add(tab, new List<AwardParam>());
            if (tab == AwardParam.Tab.Extra)
            {
              if (this.mOpenAwards != null && this.mOpenAwards.Contains(awardParam.iname))
                this.mAwardDatas[tab].Add(awardParam);
            }
            else
              this.mAwardDatas[tab].Add(awardParam);
          }
        }
        this.mAwardDatas[AwardParam.Tab.Normal].Insert(0, this.CreateRemoveAwardData());
        if (this.mAwardDatas[AwardParam.Tab.Extra].Count <= 0)
          return;
        this.mAwardDatas[AwardParam.Tab.Extra].Insert(0, this.CreateRemoveAwardData());
      }
    }

    private AwardParam CreateRemoveAwardData()
    {
      return new AwardParam()
      {
        grade = -1,
        iname = string.Empty,
        name = LocalizedText.Get("sys.TEXT_REMOVE_AWARD")
      };
    }

    private void OnSelected(string select = "")
    {
      if (!string.IsNullOrEmpty(select))
      {
        FlowNode_Variable.Set("CONFIRM_SELECT_AWARD", select);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      }
      else
      {
        FlowNode_Variable.Set("CONFIRM_SELECT_AWARD", select);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
      }
    }

    public void SetOpenAwards(string[] awards)
    {
      if (awards == null || awards.Length <= 0)
        return;
      this.mOpenAwards = new List<string>(awards.Length);
      for (int index = 0; index < awards.Length; ++index)
      {
        if (!string.IsNullOrEmpty(awards[index]))
          this.mOpenAwards.Add(awards[index]);
      }
    }
  }
}
