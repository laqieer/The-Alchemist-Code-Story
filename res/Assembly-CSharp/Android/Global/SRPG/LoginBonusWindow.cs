// Decompiled with JetBrains decompiler
// Type: SRPG.LoginBonusWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(10, "Load Complete", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(12, "Last Day", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(11, "Take Bonus", FlowNode.PinTypes.Input, 0)]
  public class LoginBonusWindow : MonoBehaviour, IFlowInterface
  {
    public string CheckName = "CHECK";
    private List<ListItemEvents> mItems = new List<ListItemEvents>();
    public GameObject ItemList;
    public GameObject[] PositionList;
    public ListItemEvents Item_Normal;
    public ListItemEvents Item_Taken;
    public Json_LoginBonus[] DebugItems;
    public int DebugBonusCount;
    private int mLoginBonusCount;
    public GameObject BonusParticleEffect;
    public GameObject TodayItem;
    public GameObject TommorowItem;
    public Text Today;
    public Text Tommorow;
    public GameObject TommorowRow;
    public GameObject VIPBonusRow;
    public RectTransform TodayBadge;
    public RectTransform TommorowBadge;
    public LoginBonusVIPBadge VIPBadge;
    public string[] DisabledFirstDayNames;
    public string TableID;
    public string TodayTextID;
    public string TommorowTextID1;
    public string TommorowTextID2;
    public string LastDayTextID;

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

    private string MakeTodayText(ItemData todaysBonusItem)
    {
      return LocalizedText.Get(string.IsNullOrEmpty(this.TodayTextID) ? (string.IsNullOrEmpty(this.TableID) ? "sys.LOGBO_TODAY" : "sys.LOGBO_" + this.TableID.ToUpper() + "_TODAY") : this.TodayTextID, (object) todaysBonusItem.Param.name, (object) todaysBonusItem.Num, (object) this.mLoginBonusCount);
    }

    private string MakeTomorrowText(ItemData todaysBonusItem, ItemData tomorrowBonusItem)
    {
      return LocalizedText.Get(todaysBonusItem == null || todaysBonusItem.Param != tomorrowBonusItem.Param ? (string.IsNullOrEmpty(this.TommorowTextID1) ? (string.IsNullOrEmpty(this.TableID) ? "sys.LOGBO_TOMMOROW" : "sys.LOGBO_" + this.TableID.ToUpper() + "_TOMMOROW") : this.TommorowTextID1) : (string.IsNullOrEmpty(this.TommorowTextID2) ? (string.IsNullOrEmpty(this.TableID) ? "sys.LOGBO_TOMMOROW2" : "sys.LOGBO_" + this.TableID.ToUpper() + "_TOMMOROW2") : this.TommorowTextID2), new object[1]{ (object) tomorrowBonusItem.Param.name });
    }

    private string MakeLastText()
    {
      return LocalizedText.Get(string.IsNullOrEmpty(this.LastDayTextID) ? (string.IsNullOrEmpty(this.TableID) ? "sys.LOGBO_LAST" : "sys.LOGBO_" + this.TableID.ToUpper() + "_LAST") : this.LastDayTextID);
    }

    private void DisableFirstDayHiddenOject(GameObject parent)
    {
      if ((UnityEngine.Object) parent == (UnityEngine.Object) null || this.DisabledFirstDayNames == null)
        return;
      for (int index = 0; index < this.DisabledFirstDayNames.Length; ++index)
      {
        string disabledFirstDayName = this.DisabledFirstDayNames[index];
        if (!string.IsNullOrEmpty(disabledFirstDayName))
        {
          Transform child = parent.transform.FindChild(disabledFirstDayName);
          if ((UnityEngine.Object) child != (UnityEngine.Object) null)
            child.gameObject.SetActive(false);
        }
      }
    }

    private void Start()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      Json_LoginBonus[] jsonLoginBonusArray = instance.Player.FindLoginBonuses(this.TableID);
      this.mLoginBonusCount = instance.Player.LoginCountWithType(this.TableID);
      ItemData itemData1 = (ItemData) null;
      ItemData itemData2 = (ItemData) null;
      bool flag1 = false;
      if (this.DebugItems != null && this.DebugItems.Length > 0)
      {
        jsonLoginBonusArray = this.DebugItems;
        this.mLoginBonusCount = this.DebugBonusCount;
      }
      if (jsonLoginBonusArray != null)
      {
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
              if (index == this.mLoginBonusCount - 1)
              {
                itemData1 = (ItemData) loginBonusData;
                if (jsonLoginBonusArray[index].vip != null && jsonLoginBonusArray[index].vip.lv > 0)
                  flag1 = true;
                if (loginBonusData != null && loginBonusData.Param != null)
                {
                  if (loginBonusData.Param.type == EItemType.Ticket)
                    AnalyticsManager.TrackNonPremiumCurrencyObtain(AnalyticsManager.NonPremiumCurrencyType.SummonTicket, (long) loginBonusData.Num, "Login Bonus", loginBonusData.ItemID);
                  else
                    AnalyticsManager.TrackNonPremiumCurrencyObtain(AnalyticsManager.NonPremiumCurrencyType.Item, (long) loginBonusData.Num, "Login Bonus", loginBonusData.ItemID);
                }
                if (string.IsNullOrEmpty(key) && jsonLoginBonusArray[index].coin > 0)
                  AnalyticsManager.TrackFreePremiumCurrencyObtain((long) jsonLoginBonusArray[index].coin, "Login Bonus");
              }
              else if (index == this.mLoginBonusCount)
                itemData2 = (ItemData) loginBonusData;
              if (loginBonusData.Setup(0L, itemParam, num))
              {
                ListItemEvents original = index >= this.mLoginBonusCount - 1 ? this.Item_Normal : this.Item_Taken;
                if (!((UnityEngine.Object) original == (UnityEngine.Object) null) && !((UnityEngine.Object) this.ItemList == (UnityEngine.Object) null))
                {
                  ListItemEvents listItemEvents = UnityEngine.Object.Instantiate<ListItemEvents>(original);
                  DataSource.Bind<ItemData>(listItemEvents.gameObject, (ItemData) loginBonusData);
                  if ((UnityEngine.Object) original == (UnityEngine.Object) this.Item_Normal && (UnityEngine.Object) this.VIPBadge != (UnityEngine.Object) null && (jsonLoginBonusArray[index].vip != null && jsonLoginBonusArray[index].vip.lv > 0))
                  {
                    LoginBonusVIPBadge loginBonusVipBadge = UnityEngine.Object.Instantiate<LoginBonusVIPBadge>(this.VIPBadge);
                    if ((UnityEngine.Object) loginBonusVipBadge.VIPRank != (UnityEngine.Object) null)
                      loginBonusVipBadge.VIPRank.text = jsonLoginBonusArray[index].vip.lv.ToString();
                    loginBonusVipBadge.transform.SetParent(listItemEvents.transform, false);
                    ((RectTransform) loginBonusVipBadge.transform).anchoredPosition = Vector2.zero;
                    loginBonusVipBadge.gameObject.SetActive(true);
                  }
                  if ((UnityEngine.Object) this.TodayBadge != (UnityEngine.Object) null && index == this.mLoginBonusCount - 1)
                  {
                    this.TodayBadge.SetParent(listItemEvents.transform, false);
                    this.TodayBadge.anchoredPosition = Vector2.zero;
                    this.TodayBadge.gameObject.SetActive(true);
                  }
                  else if ((UnityEngine.Object) this.TommorowBadge != (UnityEngine.Object) null && index == this.mLoginBonusCount)
                  {
                    this.TommorowBadge.SetParent(listItemEvents.transform, false);
                    this.TommorowBadge.anchoredPosition = Vector2.zero;
                    this.TommorowBadge.gameObject.SetActive(true);
                  }
                  if (index < this.mLoginBonusCount - 1)
                  {
                    Transform child = listItemEvents.transform.FindChild(this.CheckName);
                    if ((UnityEngine.Object) child != (UnityEngine.Object) null)
                    {
                      Animator component = child.GetComponent<Animator>();
                      if ((UnityEngine.Object) component != (UnityEngine.Object) null)
                        component.enabled = false;
                    }
                  }
                  Transform transform = this.ItemList.transform;
                  if (this.PositionList != null && this.PositionList.Length > index && (UnityEngine.Object) this.PositionList[index] != (UnityEngine.Object) null)
                    transform = this.PositionList[index].transform;
                  if (index == 0)
                    this.DisableFirstDayHiddenOject(listItemEvents.gameObject);
                  listItemEvents.transform.SetParent(transform, false);
                  listItemEvents.gameObject.SetActive(true);
                  this.mItems.Add(listItemEvents);
                }
              }
            }
          }
        }
      }
      bool flag2 = instance.Player.IsLastLoginBonus(this.TableID);
      if (jsonLoginBonusArray != null && this.mLoginBonusCount == jsonLoginBonusArray.Length)
        flag2 = true;
      if ((UnityEngine.Object) this.Today != (UnityEngine.Object) null && itemData1 != null && itemData1.Param != null)
        this.Today.text = this.MakeTodayText(itemData1);
      if ((UnityEngine.Object) this.TodayItem != (UnityEngine.Object) null)
      {
        DataSource.Bind<ItemData>(this.TodayItem.gameObject, itemData1);
        GameParameter.UpdateAll(this.TodayItem);
      }
      if ((UnityEngine.Object) this.TommorowItem != (UnityEngine.Object) null)
      {
        DataSource.Bind<ItemData>(this.TommorowItem.gameObject, itemData2);
        GameParameter.UpdateAll(this.TommorowItem);
      }
      if ((UnityEngine.Object) this.Tommorow != (UnityEngine.Object) null && !flag2 && (itemData2 != null && itemData2.Param != null))
        this.Tommorow.text = this.MakeTomorrowText(itemData1, itemData2);
      else if ((UnityEngine.Object) this.TommorowRow != (UnityEngine.Object) null)
        this.Tommorow.text = this.MakeLastText();
      if ((UnityEngine.Object) this.VIPBonusRow != (UnityEngine.Object) null)
        this.VIPBonusRow.SetActive(flag1);
      if (flag2)
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 12);
      this.StartCoroutine(this.WaitLoadAsync());
    }

    [DebuggerHidden]
    private IEnumerator WaitLoadAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new LoginBonusWindow.\u003CWaitLoadAsync\u003Ec__Iterator109() { \u003C\u003Ef__this = this };
    }

    public void Activated(int pinID)
    {
      if (pinID != 11)
        return;
      this.FlipTodaysItem();
    }

    private void FlipTodaysItem()
    {
      if (this.mLoginBonusCount < 0 || this.mItems.Count < this.mLoginBonusCount)
        return;
      int index = this.mLoginBonusCount - 1;
      ListItemEvents mItem = this.mItems[index];
      if ((UnityEngine.Object) this.BonusParticleEffect != (UnityEngine.Object) null)
        UIUtility.SpawnParticle(this.BonusParticleEffect, mItem.transform as RectTransform, new Vector2(0.5f, 0.5f));
      ItemData dataOfClass = DataSource.FindDataOfClass<ItemData>(mItem.gameObject, (ItemData) null);
      ListItemEvents listItemEvents = UnityEngine.Object.Instantiate<ListItemEvents>(this.Item_Taken);
      DataSource.Bind<ItemData>(listItemEvents.gameObject, dataOfClass);
      listItemEvents.transform.SetParent(mItem.transform.parent, false);
      listItemEvents.transform.SetSiblingIndex(mItem.transform.GetSiblingIndex());
      if ((UnityEngine.Object) this.TodayBadge != (UnityEngine.Object) null)
        this.TodayBadge.SetParent(listItemEvents.transform, false);
      UnityEngine.Object.Destroy((UnityEngine.Object) mItem.gameObject);
      listItemEvents.gameObject.SetActive(true);
      Transform child = listItemEvents.transform.FindChild(this.CheckName);
      if ((UnityEngine.Object) child != (UnityEngine.Object) null)
      {
        Animator component = child.GetComponent<Animator>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null)
          component.enabled = true;
      }
      if (index == 0)
        this.DisableFirstDayHiddenOject(listItemEvents.gameObject);
      this.mItems[index] = listItemEvents;
    }
  }
}
