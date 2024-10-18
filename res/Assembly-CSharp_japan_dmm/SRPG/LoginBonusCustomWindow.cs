// Decompiled with JetBrains decompiler
// Type: SRPG.LoginBonusCustomWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class LoginBonusCustomWindow : MonoBehaviour, IFlowInterface
  {
    public GameObject ItemList;
    public ListItemEvents Item_Normal;
    public ListItemEvents Item_Taken;
    public Json_LoginBonus[] DebugItems;
    public int DebugBonusCount;
    private int mLoginBonusCount;
    public RectTransform TodayBadge;
    public RectTransform TommorowBadge;
    public LoginBonusVIPBadge VIPBadge;
    public string CheckName = "CHECK";
    private List<ListItemEvents> mItems = new List<ListItemEvents>();
    private ItemData mCurrentItem;
    public GameObject ItemInfo;
    public GameObject ItemBg;
    public GameObject PieceInfo;
    public GameObject PieceBg;

    private void Awake()
    {
      if (Object.op_Inequality((Object) this.Item_Normal, (Object) null))
        ((Component) this.Item_Normal).gameObject.SetActive(false);
      if (Object.op_Inequality((Object) this.Item_Taken, (Object) null))
        ((Component) this.Item_Taken).gameObject.SetActive(false);
      if (Object.op_Inequality((Object) this.VIPBadge, (Object) null))
        ((Component) this.VIPBadge).gameObject.SetActive(false);
      if (Object.op_Inequality((Object) this.TodayBadge, (Object) null))
        ((Component) this.TodayBadge).gameObject.SetActive(false);
      if (!Object.op_Inequality((Object) this.TommorowBadge, (Object) null))
        return;
      ((Component) this.TommorowBadge).gameObject.SetActive(false);
    }

    private void Start()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      Json_LoginBonus[] jsonLoginBonusArray = instance.Player.LoginBonus;
      this.mLoginBonusCount = instance.Player.LoginBonusCount;
      if (this.DebugItems != null && this.DebugItems.Length > 0)
      {
        jsonLoginBonusArray = this.DebugItems;
        this.mLoginBonusCount = this.DebugBonusCount;
      }
      if (jsonLoginBonusArray != null && Object.op_Inequality((Object) this.Item_Normal, (Object) null) && Object.op_Inequality((Object) this.ItemList, (Object) null))
      {
        Transform transform1 = this.ItemList.transform;
        for (int index = 0; index < jsonLoginBonusArray.Length; ++index)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          LoginBonusCustomWindow.\u003CStart\u003Ec__AnonStorey1 startCAnonStorey1 = new LoginBonusCustomWindow.\u003CStart\u003Ec__AnonStorey1();
          // ISSUE: reference to a compiler-generated field
          startCAnonStorey1.\u0024this = this;
          string key = jsonLoginBonusArray[index].iname;
          int num = jsonLoginBonusArray[index].num;
          if (string.IsNullOrEmpty(key) && jsonLoginBonusArray[index].coin > 0)
          {
            key = "$COIN";
            num = jsonLoginBonusArray[index].coin;
          }
          if (!string.IsNullOrEmpty(key))
          {
            ItemParam itemParam = instance.GetItemParam(key);
            if (itemParam != null)
            {
              LoginBonusData data = new LoginBonusData();
              data.DayNum = index + 1;
              if (data.Setup(0L, itemParam, num))
              {
                ListItemEvents listItemEvents = index >= this.mLoginBonusCount - 1 ? this.Item_Normal : this.Item_Taken;
                // ISSUE: reference to a compiler-generated field
                startCAnonStorey1.item = Object.Instantiate<ListItemEvents>(listItemEvents);
                // ISSUE: reference to a compiler-generated field
                DataSource.Bind<ItemData>(((Component) startCAnonStorey1.item).gameObject, (ItemData) data);
                if (Object.op_Equality((Object) listItemEvents, (Object) this.Item_Normal) && Object.op_Inequality((Object) this.VIPBadge, (Object) null) && jsonLoginBonusArray[index].vip != null && jsonLoginBonusArray[index].vip.lv > 0)
                {
                  LoginBonusVIPBadge loginBonusVipBadge = Object.Instantiate<LoginBonusVIPBadge>(this.VIPBadge);
                  if (Object.op_Inequality((Object) loginBonusVipBadge.VIPRank, (Object) null))
                    loginBonusVipBadge.VIPRank.text = jsonLoginBonusArray[index].vip.lv.ToString();
                  // ISSUE: reference to a compiler-generated field
                  ((Component) loginBonusVipBadge).transform.SetParent(((Component) startCAnonStorey1.item).transform, false);
                  ((RectTransform) ((Component) loginBonusVipBadge).transform).anchoredPosition = Vector2.zero;
                  ((Component) loginBonusVipBadge).gameObject.SetActive(true);
                }
                if (Object.op_Inequality((Object) this.TodayBadge, (Object) null) && index == this.mLoginBonusCount - 1)
                {
                  // ISSUE: reference to a compiler-generated field
                  ((Transform) this.TodayBadge).SetParent(((Component) startCAnonStorey1.item).transform, false);
                  this.TodayBadge.anchoredPosition = Vector2.zero;
                  ((Component) this.TodayBadge).gameObject.SetActive(true);
                }
                else if (Object.op_Inequality((Object) this.TommorowBadge, (Object) null) && index == this.mLoginBonusCount)
                {
                  // ISSUE: reference to a compiler-generated field
                  ((Transform) this.TommorowBadge).SetParent(((Component) startCAnonStorey1.item).transform, false);
                  this.TommorowBadge.anchoredPosition = Vector2.zero;
                  ((Component) this.TommorowBadge).gameObject.SetActive(true);
                }
                if (index < this.mLoginBonusCount - 1)
                {
                  // ISSUE: reference to a compiler-generated field
                  Transform transform2 = ((Component) startCAnonStorey1.item).transform.Find(this.CheckName);
                  if (Object.op_Inequality((Object) transform2, (Object) null))
                  {
                    Animator component = ((Component) transform2).GetComponent<Animator>();
                    if (Object.op_Inequality((Object) component, (Object) null))
                      ((Behaviour) component).enabled = false;
                  }
                }
                // ISSUE: reference to a compiler-generated field
                Button component1 = ((Component) ((Component) startCAnonStorey1.item).transform).GetComponent<Button>();
                if (Object.op_Inequality((Object) component1, (Object) null))
                {
                  // ISSUE: method pointer
                  ((UnityEvent) component1.onClick).AddListener(new UnityAction((object) startCAnonStorey1, __methodptr(\u003C\u003Em__0)));
                }
                // ISSUE: reference to a compiler-generated field
                ((Component) startCAnonStorey1.item).transform.SetParent(transform1, false);
                // ISSUE: reference to a compiler-generated field
                ((Component) startCAnonStorey1.item).gameObject.SetActive(true);
                // ISSUE: reference to a compiler-generated field
                this.mItems.Add(startCAnonStorey1.item);
              }
            }
          }
        }
      }
      this.FlipTodaysItem();
      this.StartCoroutine(this.WaitLoadAsync());
    }

    private void ShowDetailWindow(ListItemEvents item)
    {
      ItemData dataOfClass = DataSource.FindDataOfClass<ItemData>(((Component) item).gameObject, (ItemData) null);
      if (dataOfClass == null)
        return;
      string empty = string.Empty;
      string EventName;
      if (string.IsNullOrEmpty(dataOfClass.Param.Flavor))
      {
        DataSource.Bind<ItemData>(this.PieceInfo, dataOfClass);
        GameParameter.UpdateAll(this.PieceInfo);
        EventName = "OPEN_PIECE_DETAIL";
        CanvasGroup component = this.PieceBg.GetComponent<CanvasGroup>();
        if (Object.op_Inequality((Object) component, (Object) null))
        {
          component.interactable = true;
          component.blocksRaycasts = true;
        }
      }
      else
      {
        DataSource.Bind<ItemData>(this.ItemInfo, dataOfClass);
        GameParameter.UpdateAll(this.ItemInfo);
        EventName = "OPEN_ITEM_DETAIL";
        CanvasGroup component = this.ItemBg.GetComponent<CanvasGroup>();
        if (Object.op_Inequality((Object) component, (Object) null))
        {
          component.interactable = true;
          component.blocksRaycasts = true;
        }
      }
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, EventName);
    }

    [DebuggerHidden]
    private IEnumerator WaitLoadAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      LoginBonusCustomWindow.\u003CWaitLoadAsync\u003Ec__Iterator0 loadAsyncCIterator0 = new LoginBonusCustomWindow.\u003CWaitLoadAsync\u003Ec__Iterator0();
      return (IEnumerator) loadAsyncCIterator0;
    }

    public void Activated(int pinID)
    {
    }

    private void FlipTodaysItem()
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      LoginBonusCustomWindow.\u003CFlipTodaysItem\u003Ec__AnonStorey2 itemCAnonStorey2 = new LoginBonusCustomWindow.\u003CFlipTodaysItem\u003Ec__AnonStorey2();
      // ISSUE: reference to a compiler-generated field
      itemCAnonStorey2.\u0024this = this;
      if (this.mLoginBonusCount < 0 || this.mItems.Count < this.mLoginBonusCount)
        return;
      int index = this.mLoginBonusCount - 1;
      ListItemEvents mItem = this.mItems[index];
      ItemData dataOfClass = DataSource.FindDataOfClass<ItemData>(((Component) mItem).gameObject, (ItemData) null);
      // ISSUE: reference to a compiler-generated field
      itemCAnonStorey2.newItem = Object.Instantiate<ListItemEvents>(this.Item_Taken);
      // ISSUE: reference to a compiler-generated field
      DataSource.Bind<ItemData>(((Component) itemCAnonStorey2.newItem).gameObject, dataOfClass);
      // ISSUE: reference to a compiler-generated field
      ((Component) itemCAnonStorey2.newItem).transform.SetParent(((Component) mItem).transform.parent, false);
      // ISSUE: reference to a compiler-generated field
      ((Component) itemCAnonStorey2.newItem).transform.SetSiblingIndex(((Component) mItem).transform.GetSiblingIndex());
      // ISSUE: reference to a compiler-generated field
      Button component = ((Component) ((Component) itemCAnonStorey2.newItem).transform).GetComponent<Button>();
      if (Object.op_Inequality((Object) component, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) component.onClick).AddListener(new UnityAction((object) itemCAnonStorey2, __methodptr(\u003C\u003Em__0)));
      }
      if (Object.op_Inequality((Object) this.TodayBadge, (Object) null))
      {
        // ISSUE: reference to a compiler-generated field
        ((Transform) this.TodayBadge).SetParent(((Component) itemCAnonStorey2.newItem).transform, false);
      }
      Object.Destroy((Object) ((Component) mItem).gameObject);
      // ISSUE: reference to a compiler-generated field
      ((Component) itemCAnonStorey2.newItem).gameObject.SetActive(true);
      // ISSUE: reference to a compiler-generated field
      this.mItems[index] = itemCAnonStorey2.newItem;
    }
  }
}
