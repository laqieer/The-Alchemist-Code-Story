// Decompiled with JetBrains decompiler
// Type: SRPG.LoginBonusWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(10, "Load Complete", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(11, "Take Bonus", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(12, "Last Day", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(20, "詳細表示(アイテム)", FlowNode.PinTypes.Output, 20)]
  [FlowNode.Pin(21, "詳細表示(真理念装)", FlowNode.PinTypes.Output, 21)]
  [FlowNode.Pin(22, "補填可能なアイコンをタップ", FlowNode.PinTypes.Output, 22)]
  public class LoginBonusWindow : MonoBehaviour, IFlowInterface
  {
    private const int PIN_OT_SELECT_ITEM = 20;
    private const int PIN_OT_SELECT_CONCEPTCARD = 21;
    private const int PIN_OT_SELECT_RECOVER = 22;
    public GameObject ItemList;
    public GameObject[] PositionList;
    [HeaderBar("▼アイコン表示用オブジェクト")]
    public ListItemEvents Item_Normal;
    public ListItemEvents Item_Taken;
    public Json_LoginBonus[] DebugItems;
    public int DebugBonusCount;
    private int mLoginBonusCount;
    public GameObject BonusParticleEffect;
    [HeaderBar("▼演出時のアイコン表示用オブジェクト")]
    public GameObject TodayItem;
    public GameObject TommorowItem;
    public Text Today;
    public Text Tommorow;
    public GameObject TommorowRow;
    public GameObject VIPBonusRow;
    public RectTransform TodayBadge;
    public RectTransform TommorowBadge;
    public LoginBonusVIPBadge VIPBadge;
    public string CheckName = "CHECK";
    public string[] DisabledFirstDayNames;
    public string TableID;
    public string TodayTextID;
    public string TommorowTextID1;
    public string TommorowTextID2;
    public string LastDayTextID;
    public bool IsConfigWindow;
    [HeaderBar("毎月ログボのタイトル月")]
    [SerializeField]
    private Text TitleMonthText;
    [HeaderBar("月のログイン総数")]
    [SerializeField]
    private Text LoginTotalCount;
    [HeaderBar("月の残り補填可能数")]
    [SerializeField]
    private Text RecoverRemain;
    [HeaderBar("月の補填可能最大数")]
    [SerializeField]
    private Text RecoverMax;
    private List<ListItemEvents> mItems = new List<ListItemEvents>();

    private void Awake()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Item_Normal, (UnityEngine.Object) null))
        ((Component) this.Item_Normal).gameObject.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Item_Taken, (UnityEngine.Object) null))
        ((Component) this.Item_Taken).gameObject.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.VIPBadge, (UnityEngine.Object) null))
        ((Component) this.VIPBadge).gameObject.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TodayBadge, (UnityEngine.Object) null))
        ((Component) this.TodayBadge).gameObject.SetActive(false);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TommorowBadge, (UnityEngine.Object) null))
        return;
      ((Component) this.TommorowBadge).gameObject.SetActive(false);
    }

    private string MakeTodayText(GiftRecieveItemData todaysBonusItem)
    {
      return LocalizedText.Get(string.IsNullOrEmpty(this.TodayTextID) ? (string.IsNullOrEmpty(this.TableID) ? "sys.LOGBO_TODAY" : "sys.LOGBO_" + this.TableID.ToUpper() + "_TODAY") : this.TodayTextID, (object) todaysBonusItem.name, (object) todaysBonusItem.num, (object) this.mLoginBonusCount);
    }

    private string MakeTomorrowText(
      GiftRecieveItemData todaysBonusItem,
      GiftRecieveItemData tomorrowBonusItem)
    {
      return LocalizedText.Get(todaysBonusItem == null || !(todaysBonusItem.iname == tomorrowBonusItem.iname) ? (string.IsNullOrEmpty(this.TommorowTextID1) ? (string.IsNullOrEmpty(this.TableID) ? "sys.LOGBO_TOMMOROW" : "sys.LOGBO_" + this.TableID.ToUpper() + "_TOMMOROW") : this.TommorowTextID1) : (string.IsNullOrEmpty(this.TommorowTextID2) ? (string.IsNullOrEmpty(this.TableID) ? "sys.LOGBO_TOMMOROW2" : "sys.LOGBO_" + this.TableID.ToUpper() + "_TOMMOROW2") : this.TommorowTextID2), (object) tomorrowBonusItem.name);
    }

    private string MakeLastText()
    {
      return LocalizedText.Get(string.IsNullOrEmpty(this.LastDayTextID) ? (string.IsNullOrEmpty(this.TableID) ? "sys.LOGBO_LAST" : "sys.LOGBO_" + this.TableID.ToUpper() + "_LAST") : this.LastDayTextID);
    }

    private void DisableFirstDayHiddenOject(GameObject parent)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) parent, (UnityEngine.Object) null) || this.DisabledFirstDayNames == null)
        return;
      for (int index = 0; index < this.DisabledFirstDayNames.Length; ++index)
      {
        string disabledFirstDayName = this.DisabledFirstDayNames[index];
        if (!string.IsNullOrEmpty(disabledFirstDayName))
        {
          Transform transform = parent.transform.Find(disabledFirstDayName);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) transform, (UnityEngine.Object) null))
            ((Component) transform).gameObject.SetActive(false);
        }
      }
    }

    private void Start()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      DateTime dateTime = TimeManager.FromUnixTime(Network.GetServerTime());
      Json_LoginBonus[] jsonLoginBonusArray;
      int[] array;
      int num1;
      int num2;
      int num3;
      if (this.IsConfigWindow)
      {
        jsonLoginBonusArray = instance.Player.LoginBonus30days.bonuses;
        this.mLoginBonusCount = instance.Player.LoginBonus30days.count;
        array = instance.Player.LoginBonus30days.login_days;
        num1 = instance.Player.LoginBonus30days.remain_recover;
        num2 = instance.Player.LoginBonus30days.max_recover;
        num3 = instance.Player.LoginBonus30days.current_month;
        GlobalVars.MonthlyLoginBonus_SelectTableIname = instance.Player.LoginBonus30days.type;
      }
      else
      {
        jsonLoginBonusArray = instance.Player.FindLoginBonuses(this.TableID);
        this.mLoginBonusCount = instance.Player.LoginCountWithType(this.TableID);
        array = instance.Player.GetLoginBonusLoginDays(this.TableID);
        num1 = instance.Player.GetLoginBonusRemainRecover(this.TableID);
        num2 = instance.Player.GetLoginBonusMaxRecover(this.TableID);
        num3 = instance.Player.GetLoginBonusCurrentMonth(this.TableID);
        GlobalVars.MonthlyLoginBonus_SelectTableIname = this.TableID;
      }
      bool flag1 = array != null && array.Length > 0;
      GiftRecieveItemData giftRecieveItemData1 = (GiftRecieveItemData) null;
      GiftRecieveItemData giftRecieveItemData2 = (GiftRecieveItemData) null;
      List<GiftRecieveItemData> giftRecieveItemDataList = new List<GiftRecieveItemData>();
      bool flag2 = false;
      if (this.DebugItems != null && this.DebugItems.Length > 0)
      {
        jsonLoginBonusArray = this.DebugItems;
        this.mLoginBonusCount = this.DebugBonusCount;
      }
      if (jsonLoginBonusArray != null)
      {
        for (int day1 = 0; day1 < jsonLoginBonusArray.Length; ++day1)
        {
          GiftRecieveItemData data1 = new GiftRecieveItemData();
          giftRecieveItemDataList.Add(data1);
          string str = jsonLoginBonusArray[day1].iname;
          int num4 = jsonLoginBonusArray[day1].num;
          int day = day1 + 1;
          if (string.IsNullOrEmpty(str) && jsonLoginBonusArray[day1].coin > 0)
          {
            str = "$COIN";
            num4 = jsonLoginBonusArray[day1].coin;
          }
          if (!string.IsNullOrEmpty(str))
          {
            ItemParam itemParam = instance.MasterParam.GetItemParam(str, false);
            if (itemParam != null)
            {
              data1.Set(str, GiftTypes.Item, itemParam.rare, num4);
              data1.name = itemParam.name;
            }
            ConceptCardParam conceptCardParam = instance.MasterParam.GetConceptCardParam(str);
            if (conceptCardParam != null)
            {
              data1.Set(str, GiftTypes.ConceptCard, conceptCardParam.rare, num4);
              data1.name = conceptCardParam.name;
            }
            if (itemParam == null && conceptCardParam == null)
              DebugUtility.LogError(string.Format("不明な識別子が報酬として設定されています。itemID => {0}", (object) str));
            if (day1 == this.mLoginBonusCount - 1)
            {
              giftRecieveItemData1 = data1;
              if (jsonLoginBonusArray[day1].vip != null && jsonLoginBonusArray[day1].vip.lv > 0)
                flag2 = true;
            }
            else if (day1 == this.mLoginBonusCount)
              giftRecieveItemData2 = data1;
            ListItemEvents listItemEvents1;
            if (flag1)
            {
              int num5 = day1 + 1;
              listItemEvents1 = Array.IndexOf<int>(array, num5) <= -1 ? this.Item_Normal : this.Item_Taken;
            }
            else
            {
              int num6 = this.mLoginBonusCount - (!this.IsConfigWindow ? 1 : 0);
              listItemEvents1 = day1 >= num6 ? this.Item_Normal : this.Item_Taken;
            }
            if (!UnityEngine.Object.op_Equality((UnityEngine.Object) listItemEvents1, (UnityEngine.Object) null) && !UnityEngine.Object.op_Equality((UnityEngine.Object) this.ItemList, (UnityEngine.Object) null))
            {
              ListItemEvents listItemEvents2 = UnityEngine.Object.Instantiate<ListItemEvents>(listItemEvents1);
              listItemEvents2.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect);
              DataSource.Bind<GiftRecieveItemData>(((Component) listItemEvents2).gameObject, data1);
              if (UnityEngine.Object.op_Equality((UnityEngine.Object) listItemEvents1, (UnityEngine.Object) this.Item_Normal) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.VIPBadge, (UnityEngine.Object) null) && jsonLoginBonusArray[day1].vip != null && jsonLoginBonusArray[day1].vip.lv > 0)
              {
                LoginBonusVIPBadge loginBonusVipBadge = UnityEngine.Object.Instantiate<LoginBonusVIPBadge>(this.VIPBadge);
                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) loginBonusVipBadge.VIPRank, (UnityEngine.Object) null))
                  loginBonusVipBadge.VIPRank.text = jsonLoginBonusArray[day1].vip.lv.ToString();
                ((Component) loginBonusVipBadge).transform.SetParent(((Component) listItemEvents2).transform, false);
                ((RectTransform) ((Component) loginBonusVipBadge).transform).anchoredPosition = Vector2.zero;
                ((Component) loginBonusVipBadge).gameObject.SetActive(true);
              }
              if (flag1)
              {
                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TodayBadge, (UnityEngine.Object) null) && day1 == dateTime.Day - 1)
                {
                  ((Transform) this.TodayBadge).SetParent(((Component) listItemEvents2).transform, false);
                  this.TodayBadge.anchoredPosition = Vector2.zero;
                  ((Component) this.TodayBadge).gameObject.SetActive(true);
                }
                else if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TommorowBadge, (UnityEngine.Object) null) && day1 == dateTime.Day)
                {
                  ((Transform) this.TommorowBadge).SetParent(((Component) listItemEvents2).transform, false);
                  this.TommorowBadge.anchoredPosition = Vector2.zero;
                  ((Component) this.TommorowBadge).gameObject.SetActive(true);
                }
              }
              else if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TodayBadge, (UnityEngine.Object) null) && day1 == this.mLoginBonusCount - 1)
              {
                ((Transform) this.TodayBadge).SetParent(((Component) listItemEvents2).transform, false);
                this.TodayBadge.anchoredPosition = Vector2.zero;
                ((Component) this.TodayBadge).gameObject.SetActive(true);
              }
              else if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TommorowBadge, (UnityEngine.Object) null) && day1 == this.mLoginBonusCount)
              {
                ((Transform) this.TommorowBadge).SetParent(((Component) listItemEvents2).transform, false);
                this.TommorowBadge.anchoredPosition = Vector2.zero;
                ((Component) this.TommorowBadge).gameObject.SetActive(true);
              }
              if (day1 < this.mLoginBonusCount - 1)
              {
                Transform transform = ((Component) listItemEvents2).transform.Find(this.CheckName);
                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) transform, (UnityEngine.Object) null))
                {
                  Animator component = ((Component) transform).GetComponent<Animator>();
                  if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
                    ((Behaviour) component).enabled = false;
                }
              }
              Transform transform1 = this.ItemList.transform;
              if (this.PositionList != null && this.PositionList.Length > day1 && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PositionList[day1], (UnityEngine.Object) null))
                transform1 = this.PositionList[day1].transform;
              if (day1 == 0)
                this.DisableFirstDayHiddenOject(((Component) listItemEvents2).gameObject);
              if (flag1)
              {
                LoginBonusMonthState state = LoginBonusMonthState.None;
                if (dateTime.Day > day1)
                  state = Array.FindIndex<int>(array, (Predicate<int>) (value => value == day)) <= -1 ? LoginBonusMonthState.NotReceived : LoginBonusMonthState.Received;
                LoginBonusMonthParam data2 = new LoginBonusMonthParam(state, day1);
                DataSource.Bind<LoginBonusMonthParam>(((Component) listItemEvents2).gameObject, data2);
              }
              ((Component) listItemEvents2).transform.SetParent(transform1, false);
              ((Component) listItemEvents2).gameObject.SetActive(true);
              ((Component) listItemEvents2).GetComponentInChildren<GiftRecieveItem>().UpdateValue();
              this.mItems.Add(listItemEvents2);
            }
          }
        }
      }
      bool flag3 = instance.Player.IsLastLoginBonus(this.TableID);
      if (jsonLoginBonusArray != null && this.mLoginBonusCount == jsonLoginBonusArray.Length)
        flag3 = true;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Today, (UnityEngine.Object) null) && giftRecieveItemData1 != null)
        this.Today.text = this.MakeTodayText(giftRecieveItemData1);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TodayItem, (UnityEngine.Object) null))
      {
        DataSource.Bind<GiftRecieveItemData>(this.TodayItem.gameObject, giftRecieveItemData1);
        this.TodayItem.gameObject.GetComponentInChildren<GiftRecieveItem>().UpdateValue();
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TommorowItem, (UnityEngine.Object) null))
      {
        DataSource.Bind<GiftRecieveItemData>(this.TommorowItem.gameObject, giftRecieveItemData2);
        this.TommorowItem.gameObject.GetComponentInChildren<GiftRecieveItem>().UpdateValue();
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Tommorow, (UnityEngine.Object) null) && !flag3 && giftRecieveItemData2 != null)
        this.Tommorow.text = this.MakeTomorrowText(giftRecieveItemData1, giftRecieveItemData2);
      else if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TommorowRow, (UnityEngine.Object) null))
        this.Tommorow.text = this.MakeLastText();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.VIPBonusRow, (UnityEngine.Object) null))
        this.VIPBonusRow.SetActive(flag2);
      if (flag3)
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 12);
      if (flag1)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TitleMonthText, (UnityEngine.Object) null))
        {
          if (num3 <= 0)
            DebugUtility.LogError("Monthの値が異常です.");
          this.TitleMonthText.text = num3.ToString();
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.LoginTotalCount, (UnityEngine.Object) null))
          this.LoginTotalCount.text = this.mLoginBonusCount.ToString();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RecoverRemain, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RecoverMax, (UnityEngine.Object) null))
        {
          if (num1 < 0)
            DebugUtility.LogError("補填可能回数の値が異常です.");
          this.RecoverRemain.text = num1.ToString();
          if (num2 < 0)
            DebugUtility.LogError("補填可能最大数の値が異常です.");
          this.RecoverMax.text = num2.ToString();
        }
      }
      this.StartCoroutine(this.WaitLoadAsync());
    }

    [DebuggerHidden]
    private IEnumerator WaitLoadAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new LoginBonusWindow.\u003CWaitLoadAsync\u003Ec__Iterator0()
      {
        \u0024this = this
      };
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
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BonusParticleEffect, (UnityEngine.Object) null))
        UIUtility.SpawnParticle(this.BonusParticleEffect, ((Component) mItem).transform as RectTransform, new Vector2(0.5f, 0.5f));
      GiftRecieveItemData dataOfClass = DataSource.FindDataOfClass<GiftRecieveItemData>(((Component) mItem).gameObject, (GiftRecieveItemData) null);
      ListItemEvents listItemEvents = UnityEngine.Object.Instantiate<ListItemEvents>(this.Item_Taken);
      DataSource.Bind<GiftRecieveItemData>(((Component) listItemEvents).gameObject, dataOfClass);
      ((Component) listItemEvents).transform.SetParent(((Component) mItem).transform.parent, false);
      ((Component) listItemEvents).transform.SetSiblingIndex(((Component) mItem).transform.GetSiblingIndex());
      GiftRecieveItem componentInChildren = ((Component) listItemEvents).GetComponentInChildren<GiftRecieveItem>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null))
        componentInChildren.UpdateValue();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TodayBadge, (UnityEngine.Object) null))
        ((Transform) this.TodayBadge).SetParent(((Component) listItemEvents).transform, false);
      UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) mItem).gameObject);
      ((Component) listItemEvents).gameObject.SetActive(true);
      Transform transform = ((Component) listItemEvents).transform.Find(this.CheckName);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) transform, (UnityEngine.Object) null))
      {
        Animator component = ((Component) transform).GetComponent<Animator>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          ((Behaviour) component).enabled = true;
      }
      if (index == 0)
        this.DisableFirstDayHiddenOject(((Component) listItemEvents).gameObject);
      this.mItems[index] = listItemEvents;
    }

    private void OnItemSelect(GameObject go)
    {
      GiftRecieveItemData dataOfClass1 = DataSource.FindDataOfClass<GiftRecieveItemData>(go, (GiftRecieveItemData) null);
      if (dataOfClass1 == null)
        return;
      LoginBonusMonthParam dataOfClass2 = DataSource.FindDataOfClass<LoginBonusMonthParam>(go, (LoginBonusMonthParam) null);
      if (dataOfClass2 != null && dataOfClass2.State == LoginBonusMonthState.NotReceived)
      {
        if (dataOfClass1.type == GiftTypes.Item)
          GlobalVars.SelectedItemID = dataOfClass1.iname;
        else if (dataOfClass1.type == GiftTypes.ConceptCard)
        {
          ConceptCardData cardDataForDisplay = ConceptCardData.CreateConceptCardDataForDisplay(dataOfClass1.iname);
          if (cardDataForDisplay == null)
          {
            DebugUtility.LogError("iname:" + dataOfClass1.iname + "の真理念装が存在しません.");
            return;
          }
          GlobalVars.SelectedConceptCardData.Set(cardDataForDisplay);
        }
        else
        {
          DebugUtility.LogError(string.Format("不明な種類のログインボーナスが設定されています。{0} => {1}個", (object) dataOfClass1.iname, (object) dataOfClass1.num));
          return;
        }
        GlobalVars.MonthlyLoginBonus_SelectRecoverDay = dataOfClass2.Day;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 22);
      }
      else if (dataOfClass1.type == GiftTypes.Item)
      {
        GlobalVars.SelectedItemID = dataOfClass1.iname;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 20);
      }
      else if (dataOfClass1.type == GiftTypes.ConceptCard)
      {
        ConceptCardData cardDataForDisplay = ConceptCardData.CreateConceptCardDataForDisplay(dataOfClass1.iname);
        if (cardDataForDisplay == null)
        {
          DebugUtility.LogError("iname:" + dataOfClass1.iname + "の真理念装が存在しません.");
        }
        else
        {
          GlobalVars.SelectedConceptCardData.Set(cardDataForDisplay);
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 21);
        }
      }
      else
        DebugUtility.LogError(string.Format("不明な種類のログインボーナスが設定されています。{0} => {1}個", (object) dataOfClass1.iname, (object) dataOfClass1.num));
    }
  }
}
