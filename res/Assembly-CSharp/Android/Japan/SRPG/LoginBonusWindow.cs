// Decompiled with JetBrains decompiler
// Type: SRPG.LoginBonusWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(10, "Load Complete", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(11, "Take Bonus", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(12, "Last Day", FlowNode.PinTypes.Output, 2)]
  public class LoginBonusWindow : MonoBehaviour, IFlowInterface
  {
    public string CheckName = "CHECK";
    private List<ListItemEvents> mItems = new List<ListItemEvents>();
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

    private string MakeTodayText(GiftRecieveItemData todaysBonusItem)
    {
      return LocalizedText.Get(string.IsNullOrEmpty(this.TodayTextID) ? (string.IsNullOrEmpty(this.TableID) ? "sys.LOGBO_TODAY" : "sys.LOGBO_" + this.TableID.ToUpper() + "_TODAY") : this.TodayTextID, (object) todaysBonusItem.name, (object) todaysBonusItem.num, (object) this.mLoginBonusCount);
    }

    private string MakeTomorrowText(GiftRecieveItemData todaysBonusItem, GiftRecieveItemData tomorrowBonusItem)
    {
      return LocalizedText.Get(todaysBonusItem == null || !(todaysBonusItem.iname == tomorrowBonusItem.iname) ? (string.IsNullOrEmpty(this.TommorowTextID1) ? (string.IsNullOrEmpty(this.TableID) ? "sys.LOGBO_TOMMOROW" : "sys.LOGBO_" + this.TableID.ToUpper() + "_TOMMOROW") : this.TommorowTextID1) : (string.IsNullOrEmpty(this.TommorowTextID2) ? (string.IsNullOrEmpty(this.TableID) ? "sys.LOGBO_TOMMOROW2" : "sys.LOGBO_" + this.TableID.ToUpper() + "_TOMMOROW2") : this.TommorowTextID2), new object[1]{ (object) tomorrowBonusItem.name });
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
          Transform transform = parent.transform.Find(disabledFirstDayName);
          if ((UnityEngine.Object) transform != (UnityEngine.Object) null)
            transform.gameObject.SetActive(false);
        }
      }
    }

    private void Start()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      Json_LoginBonus[] jsonLoginBonusArray = instance.Player.FindLoginBonuses(this.TableID);
      this.mLoginBonusCount = instance.Player.LoginCountWithType(this.TableID);
      GiftRecieveItemData giftRecieveItemData1 = (GiftRecieveItemData) null;
      GiftRecieveItemData giftRecieveItemData2 = (GiftRecieveItemData) null;
      List<GiftRecieveItemData> giftRecieveItemDataList = new List<GiftRecieveItemData>();
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
          GiftRecieveItemData data = new GiftRecieveItemData();
          giftRecieveItemDataList.Add(data);
          string str = jsonLoginBonusArray[index].iname;
          int num = jsonLoginBonusArray[index].num;
          if (string.IsNullOrEmpty(str) && jsonLoginBonusArray[index].coin > 0)
          {
            str = "$COIN";
            num = jsonLoginBonusArray[index].coin;
          }
          if (!string.IsNullOrEmpty(str))
          {
            ItemParam itemParam = instance.MasterParam.GetItemParam(str, false);
            if (itemParam != null)
            {
              data.Set(str, GiftTypes.Item, itemParam.rare, num);
              data.name = itemParam.name;
            }
            ConceptCardParam conceptCardParam = instance.MasterParam.GetConceptCardParam(str);
            if (conceptCardParam != null)
            {
              data.Set(str, GiftTypes.ConceptCard, conceptCardParam.rare, num);
              data.name = conceptCardParam.name;
            }
            if (itemParam == null && conceptCardParam == null)
              DebugUtility.LogError(string.Format("不明な識別子が報酬として設定されています。itemID => {0}", (object) str));
            if (index == this.mLoginBonusCount - 1)
            {
              giftRecieveItemData1 = data;
              if (jsonLoginBonusArray[index].vip != null && jsonLoginBonusArray[index].vip.lv > 0)
                flag1 = true;
            }
            else if (index == this.mLoginBonusCount)
              giftRecieveItemData2 = data;
            ListItemEvents original = index >= this.mLoginBonusCount - 1 ? this.Item_Normal : this.Item_Taken;
            if (!((UnityEngine.Object) original == (UnityEngine.Object) null) && !((UnityEngine.Object) this.ItemList == (UnityEngine.Object) null))
            {
              ListItemEvents listItemEvents = UnityEngine.Object.Instantiate<ListItemEvents>(original);
              DataSource.Bind<GiftRecieveItemData>(listItemEvents.gameObject, data, false);
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
                Transform transform = listItemEvents.transform.Find(this.CheckName);
                if ((UnityEngine.Object) transform != (UnityEngine.Object) null)
                {
                  Animator component = transform.GetComponent<Animator>();
                  if ((UnityEngine.Object) component != (UnityEngine.Object) null)
                    component.enabled = false;
                }
              }
              Transform transform1 = this.ItemList.transform;
              if (this.PositionList != null && this.PositionList.Length > index && (UnityEngine.Object) this.PositionList[index] != (UnityEngine.Object) null)
                transform1 = this.PositionList[index].transform;
              if (index == 0)
                this.DisableFirstDayHiddenOject(listItemEvents.gameObject);
              listItemEvents.transform.SetParent(transform1, false);
              listItemEvents.gameObject.SetActive(true);
              listItemEvents.GetComponentInChildren<GiftRecieveItem>().UpdateValue();
              this.mItems.Add(listItemEvents);
            }
          }
        }
      }
      bool flag2 = instance.Player.IsLastLoginBonus(this.TableID);
      if (jsonLoginBonusArray != null && this.mLoginBonusCount == jsonLoginBonusArray.Length)
        flag2 = true;
      if ((UnityEngine.Object) this.Today != (UnityEngine.Object) null && giftRecieveItemData1 != null)
        this.Today.text = this.MakeTodayText(giftRecieveItemData1);
      if ((UnityEngine.Object) this.TodayItem != (UnityEngine.Object) null)
      {
        DataSource.Bind<GiftRecieveItemData>(this.TodayItem.gameObject, giftRecieveItemData1, false);
        this.TodayItem.gameObject.GetComponentInChildren<GiftRecieveItem>().UpdateValue();
      }
      if ((UnityEngine.Object) this.TommorowItem != (UnityEngine.Object) null)
      {
        DataSource.Bind<GiftRecieveItemData>(this.TommorowItem.gameObject, giftRecieveItemData2, false);
        this.TommorowItem.gameObject.GetComponentInChildren<GiftRecieveItem>().UpdateValue();
      }
      if ((UnityEngine.Object) this.Tommorow != (UnityEngine.Object) null && !flag2 && giftRecieveItemData2 != null)
        this.Tommorow.text = this.MakeTomorrowText(giftRecieveItemData1, giftRecieveItemData2);
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
      return (IEnumerator) new LoginBonusWindow.\u003CWaitLoadAsync\u003Ec__Iterator0() { \u0024this = this };
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
      GiftRecieveItemData dataOfClass = DataSource.FindDataOfClass<GiftRecieveItemData>(mItem.gameObject, (GiftRecieveItemData) null);
      ListItemEvents listItemEvents = UnityEngine.Object.Instantiate<ListItemEvents>(this.Item_Taken);
      DataSource.Bind<GiftRecieveItemData>(listItemEvents.gameObject, dataOfClass, false);
      listItemEvents.transform.SetParent(mItem.transform.parent, false);
      listItemEvents.transform.SetSiblingIndex(mItem.transform.GetSiblingIndex());
      GiftRecieveItem componentInChildren = listItemEvents.GetComponentInChildren<GiftRecieveItem>();
      if ((UnityEngine.Object) componentInChildren != (UnityEngine.Object) null)
        componentInChildren.UpdateValue();
      if ((UnityEngine.Object) this.TodayBadge != (UnityEngine.Object) null)
        this.TodayBadge.SetParent(listItemEvents.transform, false);
      UnityEngine.Object.Destroy((UnityEngine.Object) mItem.gameObject);
      listItemEvents.gameObject.SetActive(true);
      Transform transform = listItemEvents.transform.Find(this.CheckName);
      if ((UnityEngine.Object) transform != (UnityEngine.Object) null)
      {
        Animator component = transform.GetComponent<Animator>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null)
          component.enabled = true;
      }
      if (index == 0)
        this.DisableFirstDayHiddenOject(listItemEvents.gameObject);
      this.mItems[index] = listItemEvents;
    }
  }
}
