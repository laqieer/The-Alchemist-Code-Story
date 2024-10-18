// Decompiled with JetBrains decompiler
// Type: SRPG.LoginBonusWindowMonthly
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
  [FlowNode.Pin(0, "Refresh", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "LoadComplete", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "詳細表示(アイテム)", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(12, "詳細表示(真理念装)", FlowNode.PinTypes.Output, 12)]
  [FlowNode.Pin(13, "補填表示", FlowNode.PinTypes.Output, 13)]
  public class LoginBonusWindowMonthly : MonoBehaviour, IFlowInterface
  {
    private const int PIN_IN_REFRESH = 0;
    private const int PIN_OT_LOAD_COMPLETE = 10;
    private const int PIN_OT_SELECT_ITEM = 11;
    private const int PIN_OT_SELECT_CONCEPTCARD = 12;
    private const int PIN_OT_SELECT_RECOVER = 13;
    [SerializeField]
    private GameObject ItemList;
    [SerializeField]
    private ScrollRect ScrollLayout;
    [HeaderBar("アイコン表示用オブジェクト")]
    [SerializeField]
    private ListItemEvents Item_Normal;
    [SerializeField]
    private ListItemEvents Item_Taken;
    [SerializeField]
    private RectTransform TodayBadge;
    [SerializeField]
    private RectTransform TommorowBadge;
    [HeaderBar("表示したいログボのID")]
    public string TableID = string.Empty;
    [HeaderBar("スタンプアニメーションオブジェクト名")]
    public string CheckName = string.Empty;
    [HeaderBar("コンフィグからの呼び出しか")]
    public bool IsConfigWindow;
    [HeaderBar("MonthlyLoginBonusUI")]
    [SerializeField]
    private Text TitleMonthText;
    [SerializeField]
    private Text LoginTotalCountText;
    [SerializeField]
    private Text RecoverRemainText;
    [SerializeField]
    private Text RecoverMaxText;
    private List<ListItemEvents> mItems = new List<ListItemEvents>();
    private bool mIsCanRecover;

    private void Awake()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Item_Normal, (UnityEngine.Object) null))
        ((Component) this.Item_Normal).gameObject.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Item_Taken, (UnityEngine.Object) null))
        ((Component) this.Item_Taken).gameObject.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TodayBadge, (UnityEngine.Object) null))
        ((Component) this.TodayBadge).gameObject.SetActive(false);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TommorowBadge, (UnityEngine.Object) null))
        return;
      ((Component) this.TommorowBadge).gameObject.SetActive(false);
    }

    private void Start() => this.Refresh();

    private void Refresh()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      DateTime dateTime = TimeManager.FromUnixTime(Network.GetServerTime());
      float forced_scroll_val = -1f;
      Json_PremiumLoginBonus[] premiumLoginBonusArray;
      int[] array;
      int num1;
      int num2;
      int num3;
      int num4;
      if (this.IsConfigWindow)
      {
        if (instance.Player.LoginBonus30days == null)
        {
          DebugUtility.LogError("対象のログインボーナスが存在しません(LoginBonus30days)");
          return;
        }
        premiumLoginBonusArray = instance.Player.LoginBonus30days.premium_bonuses;
        array = instance.Player.LoginBonus30days.login_days;
        num1 = instance.Player.LoginBonus30days.remain_recover;
        num2 = instance.Player.LoginBonus30days.max_recover;
        num3 = instance.Player.LoginBonus30days.current_month;
        num4 = instance.Player.LoginBonus30days.count;
        GlobalVars.MonthlyLoginBonus_SelectTableIname = instance.Player.LoginBonus30days.type;
      }
      else
      {
        if (!instance.Player.IsLoginBonusTable(this.TableID))
        {
          DebugUtility.LogError("対象のログインボーナスが存在しません(TableID:" + this.TableID + ")");
          return;
        }
        premiumLoginBonusArray = instance.Player.FindPremiumLoginBonuses(this.TableID);
        array = instance.Player.GetLoginBonusLoginDays(this.TableID);
        num1 = instance.Player.GetLoginBonusRemainRecover(this.TableID);
        num2 = instance.Player.GetLoginBonusMaxRecover(this.TableID);
        num3 = instance.Player.GetLoginBonusCurrentMonth(this.TableID);
        num4 = instance.Player.LoginCountWithType(this.TableID);
        GlobalVars.MonthlyLoginBonus_SelectTableIname = this.TableID;
      }
      this.mIsCanRecover = num1 > 0;
      if (premiumLoginBonusArray != null)
      {
        for (int index = 0; index < premiumLoginBonusArray.Length; ++index)
        {
          Json_PremiumLoginBonus premiumLoginBonus = premiumLoginBonusArray[index];
          if (premiumLoginBonus != null)
          {
            GiftRecieveItemData data1 = new GiftRecieveItemData();
            int day = index + 1;
            string str = string.IsNullOrEmpty(premiumLoginBonus.icon) ? (premiumLoginBonus.coin <= 0 || premiumLoginBonus.item != null ? premiumLoginBonus.item[0].iname : "$COIN") : premiumLoginBonus.icon;
            if (!string.IsNullOrEmpty(str))
            {
              int num5 = premiumLoginBonus.coin <= 0 || premiumLoginBonus.item != null ? (premiumLoginBonus.gold <= 0 || premiumLoginBonus.item != null ? (premiumLoginBonus.item == null || premiumLoginBonus.item.Length != 1 ? 1 : premiumLoginBonus.item[0].num) : premiumLoginBonus.gold) : premiumLoginBonus.coin;
              bool flag = false;
              ItemParam itemParam = instance.MasterParam.GetItemParam(str, false);
              if (itemParam != null)
              {
                data1.Set(str, GiftTypes.Item, itemParam.rare, num5);
                data1.name = itemParam.name;
                flag = true;
              }
              ConceptCardParam conceptCardParam = instance.MasterParam.GetConceptCardParam(str);
              if (conceptCardParam != null)
              {
                data1.Set(str, GiftTypes.ConceptCard, conceptCardParam.rare, num5);
                data1.name = conceptCardParam.name;
                flag = true;
              }
              if (!flag)
              {
                DebugUtility.LogError("不明な識別子が報酬として設定されています.");
                continue;
              }
            }
            else if (premiumLoginBonus.gold > 0)
            {
              data1.Set(string.Empty, GiftTypes.Gold, 0, premiumLoginBonus.gold);
              data1.name = premiumLoginBonus.gold.ToString() + LocalizedText.Get("sys.GOLD");
            }
            else
            {
              DebugUtility.LogError("itemIDが設定出来ません.");
              continue;
            }
            ListItemEvents listItemEvents = UnityEngine.Object.Instantiate<ListItemEvents>(Array.IndexOf<int>(array, day) <= -1 ? this.Item_Normal : this.Item_Taken);
            ((UnityEngine.Object) ((Component) listItemEvents).gameObject).name = index.ToString();
            listItemEvents.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect);
            DataSource.Bind<GiftRecieveItemData>(((Component) listItemEvents).gameObject, data1);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TodayBadge, (UnityEngine.Object) null) && index == dateTime.Day - 1)
            {
              ((Transform) this.TodayBadge).SetParent(((Component) listItemEvents).transform, false);
              this.TodayBadge.anchoredPosition = Vector2.zero;
              ((Component) this.TodayBadge).gameObject.SetActive(true);
            }
            else if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TommorowBadge, (UnityEngine.Object) null) && index == dateTime.Day)
            {
              ((Transform) this.TommorowBadge).SetParent(((Component) listItemEvents).transform, false);
              this.TommorowBadge.anchoredPosition = Vector2.zero;
              ((Component) this.TommorowBadge).gameObject.SetActive(true);
              Animator component = ((Component) this.TommorowBadge).GetComponent<Animator>();
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
                ((Behaviour) component).enabled = true;
            }
            if (GlobalVars.MonthlyLoginBonus_SelectRecoverDay <= 0 ? index != dateTime.Day - 1 || this.IsConfigWindow : index != GlobalVars.MonthlyLoginBonus_SelectRecoverDay - 1)
            {
              Transform transform = ((Component) listItemEvents).transform.Find(this.CheckName);
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) transform, (UnityEngine.Object) null))
              {
                Animator component = ((Component) transform).GetComponent<Animator>();
                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
                  ((Behaviour) component).enabled = false;
              }
            }
            LoginBonusMonthState state = LoginBonusMonthState.None;
            if (dateTime.Day > index)
            {
              state = Array.FindIndex<int>(array, (Predicate<int>) (value => value == day)) <= -1 ? LoginBonusMonthState.NotReceived : LoginBonusMonthState.Received;
              if (state != LoginBonusMonthState.Received)
                state = num1 <= 0 ? LoginBonusMonthState.ShortRecover : state;
            }
            LoginBonusMonthParam data2 = new LoginBonusMonthParam(state, index + 1);
            DataSource.Bind<LoginBonusMonthParam>(((Component) listItemEvents).gameObject, data2);
            Transform transform1 = this.ItemList.transform;
            ((Component) listItemEvents).transform.SetParent(transform1, false);
            ((Component) listItemEvents).gameObject.SetActive(true);
            ((Component) listItemEvents).GetComponentInChildren<GiftRecieveItem>().UpdateValue();
            this.mItems.Add(listItemEvents);
          }
        }
      }
      if (!this.IsConfigWindow && GlobalVars.MonthlyLoginBonus_SelectRecoverDay <= 0)
      {
        float num6 = Mathf.Floor((float) (premiumLoginBonusArray.Length / 2));
        if (dateTime.Day >= premiumLoginBonusArray.Length)
          forced_scroll_val = 999f;
        else if ((double) dateTime.Day > (double) num6)
          forced_scroll_val = 1f;
        else if ((double) dateTime.Day == (double) num6)
          forced_scroll_val = 0.5f;
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TitleMonthText, (UnityEngine.Object) null))
        this.TitleMonthText.text = num3.ToString();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.LoginTotalCountText, (UnityEngine.Object) null))
        this.LoginTotalCountText.text = num4.ToString();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RecoverRemainText, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RecoverMaxText, (UnityEngine.Object) null))
      {
        this.RecoverRemainText.text = num1.ToString();
        this.RecoverMaxText.text = num2.ToString();
      }
      this.StartCoroutine(this.WaitLoadAsync(forced_scroll_val));
    }

    [DebuggerHidden]
    private IEnumerator WaitLoadAsync(float forced_scroll_val = -1f)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new LoginBonusWindowMonthly.\u003CWaitLoadAsync\u003Ec__Iterator0()
      {
        forced_scroll_val = forced_scroll_val,
        \u0024this = this
      };
    }

    private void ClearItems()
    {
      if (this.mItems == null)
        return;
      for (int index = 0; index < this.mItems.Count; ++index)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mItems[index].Body, (UnityEngine.Object) null))
        {
          UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) this.mItems[index].Body).gameObject);
          this.mItems[index].Body = (Transform) null;
        }
      }
      GameUtility.DestroyGameObjects<ListItemEvents>(this.mItems);
      this.mItems.Clear();
    }

    private void OnItemSelect(GameObject go)
    {
      GiftRecieveItemData dataOfClass1 = DataSource.FindDataOfClass<GiftRecieveItemData>(go, (GiftRecieveItemData) null);
      if (dataOfClass1 == null)
      {
        DebugUtility.LogError("GiftRecieveItemDataがBindされていません.");
      }
      else
      {
        LoginBonusMonthParam dataOfClass2 = DataSource.FindDataOfClass<LoginBonusMonthParam>(go, (LoginBonusMonthParam) null);
        if (dataOfClass2 != null && dataOfClass2.State == LoginBonusMonthState.NotReceived && this.mIsCanRecover)
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
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 13);
        }
        else if (dataOfClass1.type == GiftTypes.Item)
        {
          GlobalVars.SelectedItemID = dataOfClass1.iname;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 11);
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
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 12);
          }
        }
        else
          DebugUtility.LogError(string.Format("不明な種類のログインボーナスが設定されています。{0} => {1}個", (object) dataOfClass1.iname, (object) dataOfClass1.num));
      }
    }

    public void Activated(int pinID)
    {
      if (pinID != 0)
        return;
      this.ClearItems();
      this.Refresh();
    }
  }
}
