// Decompiled with JetBrains decompiler
// Type: SRPG.LoginBonusWindow28days
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(12, "Last Day", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(14, "詳細表示", FlowNode.PinTypes.Output, 4)]
  [FlowNode.Pin(16, "Message Closed", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(13, "Taked", FlowNode.PinTypes.Output, 3)]
  [FlowNode.Pin(10, "Load Complete", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(11, "Take Bonus", FlowNode.PinTypes.Input, 0)]
  public class LoginBonusWindow28days : MonoBehaviour, IFlowInterface
  {
    public string CheckName = "CHECK";
    public float PreviewCameraDistance = 10f;
    private List<ListItemEvents> mItems = new List<ListItemEvents>();
    public GameObject ItemList;
    public GameObject[] PositionList;
    public ListItemEvents Item_Normal;
    public ListItemEvents Item_Taken;
    public Json_LoginBonus[] DebugItems;
    public int DebugBonusCount;
    private int mLoginBonusCount;
    public GameObject BonusParticleEffect;
    public GameObject LastItem;
    public GameObject TodayItem;
    public GameObject TommorowItem;
    public RectTransform TodayBadge;
    public RectTransform TommorowBadge;
    public string[] DisabledFirstDayNames;
    public string TableID;
    public List<Toggle> WeakToggle;
    public Text GainLastItemMessage;
    public Text PopupMessage;
    public GameObject RemainingCounter;
    public Text RemainingCount;
    public Transform PreviewParent;
    public RawImage PreviewImage;
    public UnityEngine.Camera PreviewCamera;
    public bool IsConfigWindow;
    public string DebugNotifyUnitID;
    public LoginBonusWindow Message;
    public float MessageDelay;
    private UnitData mCurrentUnit;
    private UnitPreview mCurrentPreview;
    private RenderTexture mPreviewUnitRT;
    private LoginBonusWindow mMessageWindow;
    private int mCurrentWeak;
    private string[] mNotifyUnits;

    private void Awake()
    {
      if ((UnityEngine.Object) this.Item_Normal != (UnityEngine.Object) null)
        this.Item_Normal.gameObject.SetActive(false);
      if ((UnityEngine.Object) this.Item_Taken != (UnityEngine.Object) null)
        this.Item_Taken.gameObject.SetActive(false);
      if ((UnityEngine.Object) this.TodayBadge != (UnityEngine.Object) null)
        this.TodayBadge.gameObject.SetActive(false);
      if (!((UnityEngine.Object) this.TommorowBadge != (UnityEngine.Object) null))
        return;
      this.TommorowBadge.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
      GameUtility.DestroyGameObject((Component) this.mCurrentPreview);
      this.mCurrentPreview = (UnitPreview) null;
      if ((UnityEngine.Object) this.mPreviewUnitRT != (UnityEngine.Object) null)
      {
        RenderTexture.ReleaseTemporary(this.mPreviewUnitRT);
        this.mPreviewUnitRT = (RenderTexture) null;
      }
      if (!((UnityEngine.Object) this.mMessageWindow != (UnityEngine.Object) null))
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) this.mMessageWindow.gameObject);
      this.mMessageWindow = (LoginBonusWindow) null;
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

    private string GetPopupMessage(ItemData item)
    {
      if (item == null)
        return (string) null;
      return LocalizedText.Get("sys.LOGBO_POPUP_MESSAGE", (object) item.Param.name, (object) item.Num);
    }

    private string GetGainLastItemMessage(ItemData item)
    {
      if (item == null)
        return (string) null;
      return LocalizedText.Get("sys.LOGBO_GAIN_LASTITEM", new object[1]{ (object) item.Param.name });
    }

    private void Start()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      Json_LoginBonus[] jsonLoginBonusArray;
      if (this.IsConfigWindow)
      {
        jsonLoginBonusArray = instance.Player.LoginBonus28days.bonuses;
        this.mLoginBonusCount = instance.Player.LoginBonus28days.count;
        this.mNotifyUnits = instance.Player.LoginBonus28days.bonus_units;
      }
      else
      {
        jsonLoginBonusArray = instance.Player.FindLoginBonuses(this.TableID);
        this.mLoginBonusCount = instance.Player.LoginCountWithType(this.TableID);
        this.mNotifyUnits = instance.Player.GetLoginBonuseUnitIDs(this.TableID);
      }
      ItemData data1 = (ItemData) null;
      ItemData data2 = (ItemData) null;
      ItemData data3 = (ItemData) null;
      if (this.DebugItems != null && this.DebugItems.Length > 0)
      {
        jsonLoginBonusArray = this.DebugItems;
        this.mLoginBonusCount = this.DebugBonusCount;
      }
      if ((UnityEngine.Object) this.RemainingCount != (UnityEngine.Object) null)
        this.RemainingCount.text = Math.Max(28 - this.mLoginBonusCount, 0).ToString();
      this.mCurrentWeak = Math.Max(this.mLoginBonusCount - 1, 0) / 7;
      if (jsonLoginBonusArray != null && (UnityEngine.Object) this.Item_Normal != (UnityEngine.Object) null && (UnityEngine.Object) this.ItemList != (UnityEngine.Object) null)
      {
        for (int index1 = 0; index1 < jsonLoginBonusArray.Length; ++index1)
        {
          string key = jsonLoginBonusArray[index1].iname;
          int num1 = jsonLoginBonusArray[index1].num;
          if (string.IsNullOrEmpty(key) && jsonLoginBonusArray[index1].coin > 0)
          {
            key = "$COIN";
            num1 = jsonLoginBonusArray[index1].coin;
          }
          if (!string.IsNullOrEmpty(key))
          {
            ItemParam itemParam = instance.GetItemParam(key);
            if (itemParam != null)
            {
              LoginBonusData loginBonusData = new LoginBonusData();
              loginBonusData.DayNum = index1 + 1;
              if (index1 == this.mLoginBonusCount - 1)
              {
                data2 = (ItemData) loginBonusData;
                if (loginBonusData != null && loginBonusData.Param != null)
                {
                  if (loginBonusData.ItemType == EItemType.Ticket)
                    AnalyticsManager.TrackNonPremiumCurrencyObtain(AnalyticsManager.NonPremiumCurrencyType.SummonTicket, (long) loginBonusData.Num, "Login Bonus", loginBonusData.ItemID);
                  else
                    AnalyticsManager.TrackNonPremiumCurrencyObtain(AnalyticsManager.NonPremiumCurrencyType.Item, (long) loginBonusData.Num, "Login Bonus", loginBonusData.ItemID);
                }
                if (string.IsNullOrEmpty(key) && jsonLoginBonusArray[index1].coin > 0)
                  AnalyticsManager.TrackFreePremiumCurrencyObtain((long) jsonLoginBonusArray[index1].coin, "Login Bonus");
              }
              else if (index1 == this.mLoginBonusCount)
                data3 = (ItemData) loginBonusData;
              if (loginBonusData.Setup(0L, itemParam, num1))
              {
                int num2 = this.mLoginBonusCount - (!this.IsConfigWindow ? 1 : 0);
                ListItemEvents listItemEvents = UnityEngine.Object.Instantiate<ListItemEvents>(index1 >= num2 ? this.Item_Normal : this.Item_Taken);
                listItemEvents.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect);
                DataSource.Bind<ItemData>(listItemEvents.gameObject, (ItemData) loginBonusData);
                if ((UnityEngine.Object) this.TodayBadge != (UnityEngine.Object) null && index1 == this.mLoginBonusCount - 1)
                {
                  this.TodayBadge.SetParent(listItemEvents.transform, false);
                  this.TodayBadge.anchoredPosition = Vector2.zero;
                  this.TodayBadge.gameObject.SetActive(true);
                }
                else if ((UnityEngine.Object) this.TommorowBadge != (UnityEngine.Object) null && index1 == this.mLoginBonusCount)
                {
                  this.TommorowBadge.SetParent(listItemEvents.transform, false);
                  this.TommorowBadge.anchoredPosition = Vector2.zero;
                  this.TommorowBadge.gameObject.SetActive(true);
                }
                if (index1 < this.mLoginBonusCount - 1)
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
                int index2 = index1 % 7;
                if (this.PositionList != null && this.PositionList.Length > index2 && (UnityEngine.Object) this.PositionList[index2] != (UnityEngine.Object) null)
                  transform = this.PositionList[index2].transform;
                if (index1 == 0)
                  this.DisableFirstDayHiddenOject(listItemEvents.gameObject);
                int num3 = index1 / 7;
                listItemEvents.transform.SetParent(transform, false);
                listItemEvents.gameObject.SetActive(num3 == this.mCurrentWeak);
                this.mItems.Add(listItemEvents);
              }
            }
          }
        }
        data1 = DataSource.FindDataOfClass<ItemData>(this.mItems[this.mItems.Count - 1].gameObject, (ItemData) null);
      }
      if ((UnityEngine.Object) this.PopupMessage != (UnityEngine.Object) null)
        this.PopupMessage.text = this.GetPopupMessage(data2);
      bool flag = instance.Player.IsLastLoginBonus(this.TableID);
      if (jsonLoginBonusArray != null && this.mLoginBonusCount == jsonLoginBonusArray.Length)
        flag = true;
      if ((UnityEngine.Object) this.RemainingCounter != (UnityEngine.Object) null)
        this.RemainingCounter.SetActive(!flag);
      if ((UnityEngine.Object) this.TodayItem != (UnityEngine.Object) null)
      {
        DataSource.Bind<ItemData>(this.TodayItem.gameObject, data2);
        GameParameter.UpdateAll(this.TodayItem);
      }
      if ((UnityEngine.Object) this.TommorowItem != (UnityEngine.Object) null)
      {
        DataSource.Bind<ItemData>(this.TommorowItem.gameObject, data3);
        GameParameter.UpdateAll(this.TommorowItem);
      }
      if ((UnityEngine.Object) this.LastItem != (UnityEngine.Object) null)
      {
        DataSource.Bind<ItemData>(this.LastItem, data1);
        GameParameter.UpdateAll(this.LastItem);
      }
      if ((UnityEngine.Object) this.GainLastItemMessage != (UnityEngine.Object) null)
        this.GainLastItemMessage.text = this.GetGainLastItemMessage(data1);
      if (flag)
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 12);
      if (this.WeakToggle != null)
      {
        for (int index1 = 0; index1 < this.WeakToggle.Count; ++index1)
        {
          int index2 = index1 * 7 + 6;
          if (index2 < this.mItems.Count)
          {
            ItemData dataOfClass = DataSource.FindDataOfClass<ItemData>(this.mItems[index2].gameObject, (ItemData) null);
            DataSource.Bind<ItemData>(this.WeakToggle[index1].gameObject, dataOfClass);
          }
          int index = index1;
          this.WeakToggle[index1].isOn = index1 == this.mCurrentWeak;
          this.WeakToggle[index1].onValueChanged.AddListener((UnityAction<bool>) (value =>
          {
            if (!value)
              return;
            this.OnWeakSelect(this.WeakToggle[index].gameObject);
          }));
        }
      }
      string unit_iname = (string) null;
      if (this.mNotifyUnits != null && this.mNotifyUnits.Length > 0)
        unit_iname = this.mNotifyUnits[new System.Random().Next() % this.mNotifyUnits.Length];
      if (!string.IsNullOrEmpty(this.DebugNotifyUnitID))
        unit_iname = this.DebugNotifyUnitID;
      if (!string.IsNullOrEmpty(unit_iname))
      {
        this.mCurrentUnit = new UnitData();
        this.mCurrentUnit.Setup(unit_iname, 0, 0, 0, (string) null, 1, EElement.None);
        GameObject gameObject = new GameObject("Preview", new System.Type[1]{ typeof (UnitPreview) });
        this.mCurrentPreview = gameObject.GetComponent<UnitPreview>();
        this.mCurrentPreview.DefaultLayer = GameUtility.LayerHidden;
        this.mCurrentPreview.SetupUnit(this.mCurrentUnit, -1);
        gameObject.transform.SetParent(this.PreviewParent, false);
        if ((UnityEngine.Object) this.PreviewCamera != (UnityEngine.Object) null && (UnityEngine.Object) this.PreviewImage != (UnityEngine.Object) null)
        {
          int num = Mathf.FloorToInt((float) Screen.height * 0.8f);
          this.mPreviewUnitRT = RenderTexture.GetTemporary(num, num, 16, RenderTextureFormat.Default);
          this.PreviewCamera.targetTexture = this.mPreviewUnitRT;
          this.PreviewImage.texture = (Texture) this.mPreviewUnitRT;
        }
        GameUtility.SetLayer((Component) this.mCurrentPreview, GameUtility.LayerCH, true);
      }
      this.StartCoroutine(this.WaitLoadAsync());
    }

    [DebuggerHidden]
    private IEnumerator WaitLoadAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new LoginBonusWindow28days.\u003CWaitLoadAsync\u003Ec__Iterator10A() { \u003C\u003Ef__this = this };
    }

    public void Activated(int pinID)
    {
      if (pinID != 11)
        return;
      this.FlipTodaysItem();
    }

    private void FlipTodaysItem()
    {
      if (this.mLoginBonusCount <= 0 || this.mItems.Count < this.mLoginBonusCount)
        return;
      int index = Math.Max(this.mLoginBonusCount - 1, 0);
      ListItemEvents mItem = this.mItems[index];
      if ((UnityEngine.Object) this.BonusParticleEffect != (UnityEngine.Object) null)
        UIUtility.SpawnParticle(this.BonusParticleEffect, mItem.transform as RectTransform, new Vector2(0.5f, 0.5f));
      ItemData dataOfClass = DataSource.FindDataOfClass<ItemData>(mItem.gameObject, (ItemData) null);
      ListItemEvents listItemEvents = UnityEngine.Object.Instantiate<ListItemEvents>(this.Item_Taken);
      DataSource.Bind<ItemData>(listItemEvents.gameObject, dataOfClass);
      listItemEvents.transform.SetParent(mItem.transform.parent, false);
      listItemEvents.transform.SetSiblingIndex(mItem.transform.GetSiblingIndex());
      listItemEvents.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect);
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
      if ((UnityEngine.Object) this.Message != (UnityEngine.Object) null)
        this.StartCoroutine(this.DelayPopupMessage());
      else
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 13);
    }

    private void OnWeakSelect(GameObject go)
    {
      this.mCurrentWeak = this.WeakToggle.FindIndex((Predicate<Toggle>) (p => (UnityEngine.Object) p.gameObject == (UnityEngine.Object) go));
      if (this.WeakToggle != null)
      {
        for (int index = 0; index < this.WeakToggle.Count; ++index)
          this.WeakToggle[index].isOn = index == this.mCurrentWeak;
      }
      int index1 = 0;
      int num = 0;
      while (index1 < this.mItems.Count)
      {
        ListItemEvents mItem = this.mItems[index1];
        mItem.gameObject.SetActive(num == this.mCurrentWeak);
        Transform child = mItem.transform.FindChild(this.CheckName);
        if ((UnityEngine.Object) child != (UnityEngine.Object) null)
        {
          Animator component = child.GetComponent<Animator>();
          if ((UnityEngine.Object) component != (UnityEngine.Object) null)
            component.enabled = false;
        }
        ++index1;
        num = index1 / 7;
      }
    }

    private void OnItemSelect(GameObject go)
    {
      ItemData dataOfClass = DataSource.FindDataOfClass<ItemData>(go, (ItemData) null);
      if (dataOfClass == null)
        return;
      GlobalVars.SelectedItemID = dataOfClass.Param.iname;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 14);
    }

    [DebuggerHidden]
    private IEnumerator DelayPopupMessage()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new LoginBonusWindow28days.\u003CDelayPopupMessage\u003Ec__Iterator10B() { \u003C\u003Ef__this = this };
    }

    [DebuggerHidden]
    private IEnumerator WaitForDestroy()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new LoginBonusWindow28days.\u003CWaitForDestroy\u003Ec__Iterator10C() { \u003C\u003Ef__this = this };
    }
  }
}
