// Decompiled with JetBrains decompiler
// Type: SRPG.LoginBonusWindow28days
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(10, "Load Complete", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(11, "Take Bonus", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(12, "Last Day", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(13, "Taked", FlowNode.PinTypes.Output, 3)]
  [FlowNode.Pin(14, "詳細表示（アイテム）", FlowNode.PinTypes.Output, 4)]
  [FlowNode.Pin(15, "詳細表示（真理念装）", FlowNode.PinTypes.Output, 5)]
  [FlowNode.Pin(16, "Message Closed", FlowNode.PinTypes.Output, 0)]
  public class LoginBonusWindow28days : MonoBehaviour, IFlowInterface
  {
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
    public GameObject LastItem;
    public GameObject TodayItem;
    public GameObject TommorowItem;
    public RectTransform TodayBadge;
    public RectTransform TommorowBadge;
    public string CheckName = "CHECK";
    public string[] DisabledFirstDayNames;
    public string TableID;
    public List<Toggle> WeakToggle;
    public Text GainLastItemMessage;
    public Text PopupMessage;
    public GameObject RemainingCounter;
    public Text RemainingCount;
    [HeaderBar("▼3Dモデル表示用")]
    public Transform PreviewParent;
    public RawImage PreviewImage;
    public Camera PreviewCamera;
    public float PreviewCameraDistance = 10f;
    public bool IsConfigWindow;
    public string DebugNotifyUnitID;
    public LoginBonusWindow Message;
    public float MessageDelay;
    private UnitData mCurrentUnit;
    private UnitPreview mCurrentPreview;
    private RenderTexture mPreviewUnitRT;
    private LoginBonusWindow mMessageWindow;
    private List<ListItemEvents> mItems = new List<ListItemEvents>();
    private int mCurrentWeak;
    private string[] mNotifyUnits;

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

    private void OnDestroy()
    {
      GameUtility.DestroyGameObject((Component) this.mCurrentPreview);
      this.mCurrentPreview = (UnitPreview) null;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPreviewUnitRT, (UnityEngine.Object) null))
      {
        RenderTexture.ReleaseTemporary(this.mPreviewUnitRT);
        this.mPreviewUnitRT = (RenderTexture) null;
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mMessageWindow, (UnityEngine.Object) null))
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) this.mMessageWindow).gameObject);
      this.mMessageWindow = (LoginBonusWindow) null;
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

    private string GetPopupMessage(GiftRecieveItemData item)
    {
      if (item == null)
        return (string) null;
      return LocalizedText.Get("sys.LOGBO_POPUP_MESSAGE", (object) item.name, (object) item.num);
    }

    private string GetGainLastItemMessage(GiftRecieveItemData item)
    {
      if (item == null)
        return (string) null;
      return LocalizedText.Get("sys.LOGBO_GAIN_LASTITEM", (object) item.name);
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
      GiftRecieveItemData data1 = (GiftRecieveItemData) null;
      GiftRecieveItemData data2 = (GiftRecieveItemData) null;
      GiftRecieveItemData data3 = (GiftRecieveItemData) null;
      List<GiftRecieveItemData> giftRecieveItemDataList = new List<GiftRecieveItemData>();
      if (this.DebugItems != null && this.DebugItems.Length > 0)
      {
        jsonLoginBonusArray = this.DebugItems;
        this.mLoginBonusCount = this.DebugBonusCount;
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RemainingCount, (UnityEngine.Object) null))
        this.RemainingCount.text = Math.Max(28 - this.mLoginBonusCount, 0).ToString();
      this.mCurrentWeak = Math.Max(this.mLoginBonusCount - 1, 0) / 7;
      if (jsonLoginBonusArray != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Item_Normal, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ItemList, (UnityEngine.Object) null))
      {
        for (int index1 = 0; index1 < jsonLoginBonusArray.Length; ++index1)
        {
          GiftRecieveItemData data4 = new GiftRecieveItemData();
          giftRecieveItemDataList.Add(data4);
          string str = jsonLoginBonusArray[index1].iname;
          int num1 = jsonLoginBonusArray[index1].num;
          if (string.IsNullOrEmpty(str) && jsonLoginBonusArray[index1].coin > 0)
          {
            str = "$COIN";
            num1 = jsonLoginBonusArray[index1].coin;
          }
          if (!string.IsNullOrEmpty(str))
          {
            ItemParam itemParam = instance.MasterParam.GetItemParam(str, false);
            if (itemParam != null)
            {
              data4.Set(str, GiftTypes.Item, itemParam.rare, num1);
              data4.name = itemParam.name;
            }
            ConceptCardParam conceptCardParam = instance.MasterParam.GetConceptCardParam(str);
            if (conceptCardParam != null)
            {
              data4.Set(str, GiftTypes.ConceptCard, conceptCardParam.rare, num1);
              data4.name = conceptCardParam.name;
            }
            if (itemParam == null && conceptCardParam == null)
              DebugUtility.LogError(string.Format("不明な識別子が報酬として設定されています。itemID => {0}", (object) str));
            int num2 = this.mLoginBonusCount - (!this.IsConfigWindow ? 1 : 0);
            ListItemEvents listItemEvents = UnityEngine.Object.Instantiate<ListItemEvents>(index1 >= num2 ? this.Item_Normal : this.Item_Taken);
            listItemEvents.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect);
            DataSource.Bind<GiftRecieveItemData>(((Component) listItemEvents).gameObject, data4);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TodayBadge, (UnityEngine.Object) null) && index1 == this.mLoginBonusCount - 1)
            {
              ((Transform) this.TodayBadge).SetParent(((Component) listItemEvents).transform, false);
              this.TodayBadge.anchoredPosition = Vector2.zero;
              ((Component) this.TodayBadge).gameObject.SetActive(true);
            }
            else if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TommorowBadge, (UnityEngine.Object) null) && index1 == this.mLoginBonusCount)
            {
              ((Transform) this.TommorowBadge).SetParent(((Component) listItemEvents).transform, false);
              this.TommorowBadge.anchoredPosition = Vector2.zero;
              ((Component) this.TommorowBadge).gameObject.SetActive(true);
            }
            if (index1 < this.mLoginBonusCount - 1)
            {
              Transform transform = ((Component) listItemEvents).transform.Find(this.CheckName);
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) transform, (UnityEngine.Object) null))
              {
                Animator component = ((Component) transform).GetComponent<Animator>();
                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
                  ((Behaviour) component).enabled = false;
              }
            }
            Transform transform1 = this.ItemList.transform;
            int index2 = index1 % 7;
            if (this.PositionList != null && this.PositionList.Length > index2 && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PositionList[index2], (UnityEngine.Object) null))
              transform1 = this.PositionList[index2].transform;
            if (index1 == 0)
              this.DisableFirstDayHiddenOject(((Component) listItemEvents).gameObject);
            int num3 = index1 / 7;
            ((Component) listItemEvents).transform.SetParent(transform1, false);
            ((Component) listItemEvents).gameObject.SetActive(num3 == this.mCurrentWeak);
            if (num3 == this.mCurrentWeak)
              ((Component) listItemEvents).GetComponentInChildren<GiftRecieveItem>().UpdateValue();
            this.mItems.Add(listItemEvents);
          }
        }
        int index = this.mLoginBonusCount - 1;
        int mLoginBonusCount = this.mLoginBonusCount;
        if (index >= 0 && index < jsonLoginBonusArray.Length)
          data2 = giftRecieveItemDataList[index];
        if (mLoginBonusCount >= 0 && mLoginBonusCount < jsonLoginBonusArray.Length)
          data3 = giftRecieveItemDataList[mLoginBonusCount];
        data1 = DataSource.FindDataOfClass<GiftRecieveItemData>(((Component) this.mItems[this.mItems.Count - 1]).gameObject, (GiftRecieveItemData) null);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PopupMessage, (UnityEngine.Object) null))
        this.PopupMessage.text = this.GetPopupMessage(data2);
      bool flag = instance.Player.IsLastLoginBonus(this.TableID);
      if (jsonLoginBonusArray != null && this.mLoginBonusCount == jsonLoginBonusArray.Length)
        flag = true;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RemainingCounter, (UnityEngine.Object) null))
        this.RemainingCounter.SetActive(!flag);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TodayItem, (UnityEngine.Object) null))
      {
        DataSource.Bind<GiftRecieveItemData>(this.TodayItem.gameObject, data2);
        this.TodayItem.gameObject.GetComponentInChildren<GiftRecieveItem>().UpdateValue();
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TommorowItem, (UnityEngine.Object) null))
      {
        DataSource.Bind<GiftRecieveItemData>(this.TommorowItem.gameObject, data3);
        this.TommorowItem.gameObject.GetComponentInChildren<GiftRecieveItem>().UpdateValue();
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.LastItem, (UnityEngine.Object) null))
      {
        DataSource.Bind<GiftRecieveItemData>(this.LastItem.gameObject, data1);
        this.LastItem.gameObject.GetComponentInChildren<GiftRecieveItem>().UpdateValue();
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.GainLastItemMessage, (UnityEngine.Object) null))
        this.GainLastItemMessage.text = this.GetGainLastItemMessage(data1);
      if (flag)
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 12);
      if (this.WeakToggle != null)
      {
        for (int index3 = 0; index3 < this.WeakToggle.Count; ++index3)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          LoginBonusWindow28days.\u003CStart\u003Ec__AnonStorey3 startCAnonStorey3 = new LoginBonusWindow28days.\u003CStart\u003Ec__AnonStorey3();
          // ISSUE: reference to a compiler-generated field
          startCAnonStorey3.\u0024this = this;
          int index4 = index3 * 7 + 6;
          if (index4 < this.mItems.Count)
          {
            ItemData dataOfClass = DataSource.FindDataOfClass<ItemData>(((Component) this.mItems[index4]).gameObject, (ItemData) null);
            DataSource.Bind<ItemData>(((Component) this.WeakToggle[index3]).gameObject, dataOfClass);
          }
          // ISSUE: reference to a compiler-generated field
          startCAnonStorey3.index = index3;
          this.WeakToggle[index3].isOn = index3 == this.mCurrentWeak;
          // ISSUE: method pointer
          ((UnityEvent<bool>) this.WeakToggle[index3].onValueChanged).AddListener(new UnityAction<bool>((object) startCAnonStorey3, __methodptr(\u003C\u003Em__0)));
        }
      }
      string unit_iname = (string) null;
      if (this.mNotifyUnits != null && this.mNotifyUnits.Length > 0)
        unit_iname = this.mNotifyUnits[new Random().Next() % this.mNotifyUnits.Length];
      if (!string.IsNullOrEmpty(this.DebugNotifyUnitID))
        unit_iname = this.DebugNotifyUnitID;
      if (!string.IsNullOrEmpty(unit_iname))
      {
        this.mCurrentUnit = new UnitData();
        this.mCurrentUnit.Setup(unit_iname, 0, 0, 0);
        GameObject gameObject = new GameObject("Preview", new System.Type[1]
        {
          typeof (UnitPreview)
        });
        this.mCurrentPreview = gameObject.GetComponent<UnitPreview>();
        this.mCurrentPreview.DefaultLayer = GameUtility.LayerHidden;
        this.mCurrentPreview.SetupUnit(this.mCurrentUnit);
        gameObject.transform.SetParent(this.PreviewParent, false);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PreviewCamera, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PreviewImage, (UnityEngine.Object) null))
        {
          int num = Mathf.FloorToInt((float) Screen.height * 0.8f);
          this.mPreviewUnitRT = RenderTexture.GetTemporary(num, num, 16, (RenderTextureFormat) 7);
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
      return (IEnumerator) new LoginBonusWindow28days.\u003CWaitLoadAsync\u003Ec__Iterator0()
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
      if (this.mLoginBonusCount <= 0 || this.mItems.Count < this.mLoginBonusCount)
        return;
      int index = Math.Max(this.mLoginBonusCount - 1, 0);
      ListItemEvents mItem = this.mItems[index];
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BonusParticleEffect, (UnityEngine.Object) null))
        UIUtility.SpawnParticle(this.BonusParticleEffect, ((Component) mItem).transform as RectTransform, new Vector2(0.5f, 0.5f));
      GiftRecieveItemData dataOfClass = DataSource.FindDataOfClass<GiftRecieveItemData>(((Component) mItem).gameObject, (GiftRecieveItemData) null);
      ListItemEvents listItemEvents = UnityEngine.Object.Instantiate<ListItemEvents>(this.Item_Taken);
      DataSource.Bind<GiftRecieveItemData>(((Component) listItemEvents).gameObject, dataOfClass);
      ((Component) listItemEvents).transform.SetParent(((Component) mItem).transform.parent, false);
      ((Component) listItemEvents).transform.SetSiblingIndex(((Component) mItem).transform.GetSiblingIndex());
      listItemEvents.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect);
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
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Message, (UnityEngine.Object) null))
        this.StartCoroutine(this.DelayPopupMessage());
      else
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 13);
    }

    private void OnWeakSelect(GameObject go)
    {
      this.mCurrentWeak = this.WeakToggle.FindIndex((Predicate<Toggle>) (p => UnityEngine.Object.op_Equality((UnityEngine.Object) ((Component) p).gameObject, (UnityEngine.Object) go)));
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
        ((Component) mItem).gameObject.SetActive(num == this.mCurrentWeak);
        Transform transform = ((Component) mItem).transform.Find(this.CheckName);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) transform, (UnityEngine.Object) null))
        {
          Animator component = ((Component) transform).GetComponent<Animator>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
            ((Behaviour) component).enabled = false;
        }
        ++index1;
        num = index1 / 7;
      }
    }

    private void OnItemSelect(GameObject go)
    {
      GiftRecieveItemData dataOfClass = DataSource.FindDataOfClass<GiftRecieveItemData>(go, (GiftRecieveItemData) null);
      if (dataOfClass == null)
        return;
      if (dataOfClass.type == GiftTypes.Item)
      {
        GlobalVars.SelectedItemID = dataOfClass.iname;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 14);
      }
      else if (dataOfClass.type == GiftTypes.ConceptCard)
      {
        ConceptCardData cardDataForDisplay = ConceptCardData.CreateConceptCardDataForDisplay(dataOfClass.iname);
        GlobalVars.SelectedConceptCardData.Set(cardDataForDisplay);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 15);
      }
      else
        DebugUtility.LogError(string.Format("不明な種類のログインボーナスが設定されています。{0} => {1}個", (object) dataOfClass.iname, (object) dataOfClass.num));
    }

    [DebuggerHidden]
    private IEnumerator DelayPopupMessage()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new LoginBonusWindow28days.\u003CDelayPopupMessage\u003Ec__Iterator1()
      {
        \u0024this = this
      };
    }

    [DebuggerHidden]
    private IEnumerator WaitForDestroy()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new LoginBonusWindow28days.\u003CWaitForDestroy\u003Ec__Iterator2()
      {
        \u0024this = this
      };
    }
  }
}
