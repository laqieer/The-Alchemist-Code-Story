// Decompiled with JetBrains decompiler
// Type: SRPG.LoginBonusCustomWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class LoginBonusCustomWindow : MonoBehaviour, IFlowInterface
  {
    public string CheckName = "CHECK";
    private List<ListItemEvents> mItems = new List<ListItemEvents>();
    public GameObject ItemList;
    public ListItemEvents Item_Normal;
    public ListItemEvents Item_Taken;
    public Json_LoginBonus[] DebugItems;
    public int DebugBonusCount;
    private int mLoginBonusCount;
    public RectTransform TodayBadge;
    public RectTransform TommorowBadge;
    public LoginBonusVIPBadge VIPBadge;
    private ItemData mCurrentItem;
    public GameObject ItemInfo;
    public GameObject ItemBg;
    public GameObject PieceInfo;
    public GameObject PieceBg;

    private void Awake()
    {
      if ((UnityEngine.Object) this.Item_Normal != (UnityEngine.Object) null)
        this.Item_Normal.gameObject.SetActive(false);
      if ((UnityEngine.Object) this.Item_Taken != (UnityEngine.Object) null)
        this.Item_Taken.gameObject.SetActive(false);
      if ((UnityEngine.Object) this.VIPBadge != (UnityEngine.Object) null)
        this.VIPBadge.gameObject.SetActive(false);
      if ((UnityEngine.Object) this.TodayBadge != (UnityEngine.Object) null)
        this.TodayBadge.gameObject.SetActive(false);
      if (!((UnityEngine.Object) this.TommorowBadge != (UnityEngine.Object) null))
        return;
      this.TommorowBadge.gameObject.SetActive(false);
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
      if (jsonLoginBonusArray != null && (UnityEngine.Object) this.Item_Normal != (UnityEngine.Object) null && (UnityEngine.Object) this.ItemList != (UnityEngine.Object) null)
      {
        Transform transform1 = this.ItemList.transform;
        for (int index = 0; index < jsonLoginBonusArray.Length; ++index)
        {
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
              LoginBonusData loginBonusData = new LoginBonusData();
              loginBonusData.DayNum = index + 1;
              if (loginBonusData.Setup(0L, itemParam, num))
              {
                ListItemEvents original = index >= this.mLoginBonusCount - 1 ? this.Item_Normal : this.Item_Taken;
                ListItemEvents item = UnityEngine.Object.Instantiate<ListItemEvents>(original);
                DataSource.Bind<ItemData>(item.gameObject, (ItemData) loginBonusData, false);
                if ((UnityEngine.Object) original == (UnityEngine.Object) this.Item_Normal && (UnityEngine.Object) this.VIPBadge != (UnityEngine.Object) null && (jsonLoginBonusArray[index].vip != null && jsonLoginBonusArray[index].vip.lv > 0))
                {
                  LoginBonusVIPBadge loginBonusVipBadge = UnityEngine.Object.Instantiate<LoginBonusVIPBadge>(this.VIPBadge);
                  if ((UnityEngine.Object) loginBonusVipBadge.VIPRank != (UnityEngine.Object) null)
                    loginBonusVipBadge.VIPRank.text = jsonLoginBonusArray[index].vip.lv.ToString();
                  loginBonusVipBadge.transform.SetParent(item.transform, false);
                  ((RectTransform) loginBonusVipBadge.transform).anchoredPosition = Vector2.zero;
                  loginBonusVipBadge.gameObject.SetActive(true);
                }
                if ((UnityEngine.Object) this.TodayBadge != (UnityEngine.Object) null && index == this.mLoginBonusCount - 1)
                {
                  this.TodayBadge.SetParent(item.transform, false);
                  this.TodayBadge.anchoredPosition = Vector2.zero;
                  this.TodayBadge.gameObject.SetActive(true);
                }
                else if ((UnityEngine.Object) this.TommorowBadge != (UnityEngine.Object) null && index == this.mLoginBonusCount)
                {
                  this.TommorowBadge.SetParent(item.transform, false);
                  this.TommorowBadge.anchoredPosition = Vector2.zero;
                  this.TommorowBadge.gameObject.SetActive(true);
                }
                if (index < this.mLoginBonusCount - 1)
                {
                  Transform transform2 = item.transform.Find(this.CheckName);
                  if ((UnityEngine.Object) transform2 != (UnityEngine.Object) null)
                  {
                    Animator component = transform2.GetComponent<Animator>();
                    if ((UnityEngine.Object) component != (UnityEngine.Object) null)
                      component.enabled = false;
                  }
                }
                Button component1 = item.transform.GetComponent<Button>();
                if ((UnityEngine.Object) component1 != (UnityEngine.Object) null)
                  component1.onClick.AddListener((UnityAction) (() => this.ShowDetailWindow(item)));
                item.transform.SetParent(transform1, false);
                item.gameObject.SetActive(true);
                this.mItems.Add(item);
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
      ItemData dataOfClass = DataSource.FindDataOfClass<ItemData>(item.gameObject, (ItemData) null);
      if (dataOfClass == null)
        return;
      string empty = string.Empty;
      string EventName;
      if (string.IsNullOrEmpty(dataOfClass.Param.Flavor))
      {
        DataSource.Bind<ItemData>(this.PieceInfo, dataOfClass, false);
        GameParameter.UpdateAll(this.PieceInfo);
        EventName = "OPEN_PIECE_DETAIL";
        CanvasGroup component = this.PieceBg.GetComponent<CanvasGroup>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null)
        {
          component.interactable = true;
          component.blocksRaycasts = true;
        }
      }
      else
      {
        DataSource.Bind<ItemData>(this.ItemInfo, dataOfClass, false);
        GameParameter.UpdateAll(this.ItemInfo);
        EventName = "OPEN_ITEM_DETAIL";
        CanvasGroup component = this.ItemBg.GetComponent<CanvasGroup>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null)
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
      if (this.mLoginBonusCount < 0 || this.mItems.Count < this.mLoginBonusCount)
        return;
      int index = this.mLoginBonusCount - 1;
      ListItemEvents mItem = this.mItems[index];
      ItemData dataOfClass = DataSource.FindDataOfClass<ItemData>(mItem.gameObject, (ItemData) null);
      ListItemEvents newItem = UnityEngine.Object.Instantiate<ListItemEvents>(this.Item_Taken);
      DataSource.Bind<ItemData>(newItem.gameObject, dataOfClass, false);
      newItem.transform.SetParent(mItem.transform.parent, false);
      newItem.transform.SetSiblingIndex(mItem.transform.GetSiblingIndex());
      Button component = newItem.transform.GetComponent<Button>();
      if ((UnityEngine.Object) component != (UnityEngine.Object) null)
        component.onClick.AddListener((UnityAction) (() => this.ShowDetailWindow(newItem)));
      if ((UnityEngine.Object) this.TodayBadge != (UnityEngine.Object) null)
        this.TodayBadge.SetParent(newItem.transform, false);
      UnityEngine.Object.Destroy((UnityEngine.Object) mItem.gameObject);
      newItem.gameObject.SetActive(true);
      this.mItems[index] = newItem;
    }
  }
}
