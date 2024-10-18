// Decompiled with JetBrains decompiler
// Type: SRPG.LimitedShopBuyWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "ClearItem", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(100, "アイテム選択単品", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "アイテム選択セット", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "アイテム更新", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(103, "武具選択単品", FlowNode.PinTypes.Output, 103)]
  [FlowNode.Pin(104, "商品の期限切れ警告表示", FlowNode.PinTypes.Output, 104)]
  public class LimitedShopBuyWindow : MonoBehaviour, IFlowInterface
  {
    private const int PIN_IN_REFRESH = 1;
    private const int PIN_IN_CLEAR_ITEM = 2;
    public RectTransform ItemLayoutParent;
    public GameObject ItemTemplate;
    public Button BtnUpdated;
    public Text TxtTitle;
    public Text TxtUpdateTime;
    public Text TxtUpdated;
    public GameObject Updated;
    public GameObject Update;
    public Text ShopName;
    public List<GameObject> mBuyItems = new List<GameObject>(12);
    public GameObject Lineup;
    [Space(5f)]
    [SerializeField]
    private GameObject GoPaidCoinParent;
    [SerializeField]
    private GameObject GoPaidCoinIcon;
    [SerializeField]
    private GameObject GoPaidCoinNum;
    [SerializeField]
    private GameObject GoNoSellMask;

    private void Awake()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ItemTemplate, (UnityEngine.Object) null) && this.ItemTemplate.activeInHierarchy)
        this.ItemTemplate.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BtnUpdated, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.BtnUpdated.onClick).AddListener(new UnityAction((object) this, __methodptr(OnItemUpdated)));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TxtTitle, (UnityEngine.Object) null))
        this.TxtTitle.text = LocalizedText.Get("sys.SHOP_BUY_TITLE");
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TxtUpdateTime, (UnityEngine.Object) null))
        this.TxtUpdateTime.text = LocalizedText.Get("sys.UPDATE_TIME");
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TxtUpdated, (UnityEngine.Object) null))
        return;
      this.TxtUpdated.text = LocalizedText.Get("sys.CMD_UPDATED");
    }

    public void Activated(int pinID)
    {
      if (pinID != 1)
      {
        if (pinID != 2)
          return;
        this.ClearItem();
      }
      else
        this.Refresh();
    }

    private void OnDestroy() => GlobalVars.TimeOutShopItems = (List<ShopItem>) null;

    private void ClearItem()
    {
      for (int index = 0; index < this.mBuyItems.Count; ++index)
      {
        GameObject mBuyItem = this.mBuyItems[index];
        DataSource.Clear(mBuyItem);
        mBuyItem.SetActive(false);
      }
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.GoPaidCoinParent))
        this.GoPaidCoinParent.SetActive(false);
      GameUtility.SetGameObjectActive(this.GoNoSellMask, false);
    }

    private void Refresh()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ItemTemplate, (UnityEngine.Object) null))
        return;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      player.UpdateEventCoin();
      LimitedShopData limitedShopData = player.GetLimitedShopData();
      DebugUtility.Assert(limitedShopData != null, "ショップ情報が存在しない");
      this.ShopName.text = GlobalVars.LimitedShopItem.shops.info.title;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Updated, (UnityEngine.Object) null))
        this.Updated.SetActive(GlobalVars.LimitedShopItem.btn_update);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Update, (UnityEngine.Object) null))
        this.Update.SetActive(GlobalVars.LimitedShopItem.btn_update);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Lineup, (UnityEngine.Object) null))
        this.Lineup.SetActive(GlobalVars.LimitedShopItem.btn_update);
      for (int index = 0; index < this.mBuyItems.Count; ++index)
        this.mBuyItems[index].gameObject.SetActive(false);
      int count = limitedShopData.items.Count;
      DateTime serverTime = TimeManager.ServerTime;
      List<LimitedShopItem> nearbyTimeout = new List<LimitedShopItem>();
      LimitedShopItem event_coin_item = (LimitedShopItem) null;
      for (int index = 0; index < count; ++index)
      {
        LimitedShopItem data1 = limitedShopData.items[index];
        if (data1.end != 0L)
        {
          DateTime dateTime = TimeManager.FromUnixTime(data1.end);
          if (!(serverTime >= dateTime))
          {
            if ((dateTime - serverTime).TotalHours < 1.0)
              nearbyTimeout.Add(data1);
          }
          else
            continue;
        }
        if (index >= this.mBuyItems.Count)
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ItemTemplate);
          gameObject.transform.SetParent((Transform) this.ItemLayoutParent, false);
          this.mBuyItems.Add(gameObject);
        }
        GameObject mBuyItem = this.mBuyItems[index];
        LimitedShopBuyList componentInChildren = mBuyItem.GetComponentInChildren<LimitedShopBuyList>();
        componentInChildren.limitedShopItem = data1;
        DataSource.Bind<LimitedShopItem>(mBuyItem, data1);
        componentInChildren.amount.SetActive(!data1.IsSet);
        if (data1.IsArtifact)
        {
          ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(data1.iname);
          DataSource.Bind<ArtifactParam>(mBuyItem, artifactParam);
        }
        else if (data1.IsConceptCard)
        {
          ConceptCardData cardDataForDisplay = ConceptCardData.CreateConceptCardDataForDisplay(data1.iname);
          componentInChildren.SetupConceptCard(cardDataForDisplay);
        }
        else if (data1.IsItem || data1.IsSet)
        {
          ItemData data2 = new ItemData();
          data2.Setup(0L, data1.iname, data1.num);
          DataSource.Bind<ItemData>(mBuyItem, data2);
          DataSource.Bind<ItemParam>(mBuyItem, MonoSingleton<GameManager>.Instance.GetItemParam(data1.iname));
        }
        else
          DebugUtility.LogError(string.Format("不明な商品タイプが設定されています (shopitem.iname({0}) => {1})", (object) data1.iname, (object) data1.ShopItemType));
        if (data1.saleType == ESaleType.EventCoin && event_coin_item == null)
          event_coin_item = data1;
        ListItemEvents component1 = mBuyItem.GetComponent<ListItemEvents>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null))
          component1.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelect);
        Button component2 = mBuyItem.GetComponent<Button>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component2, (UnityEngine.Object) null))
          ((Selectable) component2).interactable = !data1.is_soldout;
        mBuyItem.SetActive(true);
      }
      bool flag = true;
      if (GlobalVars.ShopType == EShopType.Port && UnityEngine.Object.op_Inequality((UnityEngine.Object) GlobalVars.LimitedShopItem, (UnityEngine.Object) null) && GlobalVars.LimitedShopItem.shops != null && GlobalVars.LimitedShopItem.shops.info != null)
        flag = GlobalVars.LimitedShopItem.shops.info.unlock != 0;
      if (flag)
      {
        string warned_key = PlayerPrefsUtility.WARNED_LIMITEDSHOP_ITEM_TIMEOUT;
        if (GlobalVars.ShopType == EShopType.Port)
          warned_key = PlayerPrefsUtility.WARNED_PORTSHOP_ITEM_TIMEOUT;
        this.ShowAndSaveTimeOutItem(warned_key, nearbyTimeout);
      }
      GameParameter.UpdateAll(((Component) this).gameObject);
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.GoPaidCoinParent))
        this.GoPaidCoinParent.SetActive(false);
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.GoPaidCoinParent) && UnityEngine.Object.op_Implicit((UnityEngine.Object) this.GoPaidCoinIcon) && UnityEngine.Object.op_Implicit((UnityEngine.Object) this.GoPaidCoinNum) && event_coin_item != null)
      {
        ItemData itemData = player.Items.Find((Predicate<ItemData>) (t => t.Param.iname.Equals(event_coin_item.cost_iname)));
        if (itemData == null)
        {
          itemData = new ItemData();
          itemData.Setup(0L, event_coin_item.cost_iname, 0);
        }
        if (itemData != null)
        {
          DataSource.Bind<ItemParam>(this.GoPaidCoinIcon, itemData.Param);
          DataSource.Bind<EventCoinData>(this.GoPaidCoinNum, new EventCoinData()
          {
            iname = itemData.ItemID,
            param = itemData.Param,
            have = itemData
          });
          this.GoPaidCoinParent.SetActive(true);
        }
      }
      if (GlobalVars.ShopType != EShopType.Port || !UnityEngine.Object.op_Implicit((UnityEngine.Object) this.GoNoSellMask))
        return;
      this.GoNoSellMask.SetActive(false);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) GlobalVars.LimitedShopItem, (UnityEngine.Object) null) || GlobalVars.LimitedShopItem.shops == null || GlobalVars.LimitedShopItem.shops.info == null)
        return;
      this.GoNoSellMask.SetActive(GlobalVars.LimitedShopItem.shops.info.unlock == 0);
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.BtnUpdated))
        return;
      ((Selectable) this.BtnUpdated).interactable = GlobalVars.LimitedShopItem.shops.info.unlock != 0;
    }

    private void ShowAndSaveTimeOutItem(string warned_key, List<LimitedShopItem> nearbyTimeout)
    {
      GlobalVars.TimeOutShopItems = new List<ShopItem>();
      if (nearbyTimeout.Count <= 0)
        return;
      if (PlayerPrefsUtility.HasKey(warned_key))
      {
        ShopTimeOutItemInfoArray outItemInfoArray = JsonUtility.FromJson<ShopTimeOutItemInfoArray>(PlayerPrefsUtility.GetString(warned_key, string.Empty));
        ShopTimeOutItemInfo[] source1 = outItemInfoArray.Infos != null ? ((IEnumerable<ShopTimeOutItemInfo>) outItemInfoArray.Infos).Where<ShopTimeOutItemInfo>((Func<ShopTimeOutItemInfo, bool>) (t => t.ShopId == GlobalVars.LimitedShopItem.shops.gname)).ToArray<ShopTimeOutItemInfo>() : new ShopTimeOutItemInfo[0];
        List<LimitedShopItem> source2 = new List<LimitedShopItem>();
        List<LimitedShopItem> source3 = new List<LimitedShopItem>();
        bool flag = false;
        foreach (LimitedShopItem limitedShopItem in nearbyTimeout)
        {
          LimitedShopItem item = limitedShopItem;
          ShopTimeOutItemInfo shopTimeOutItemInfo = ((IEnumerable<ShopTimeOutItemInfo>) source1).FirstOrDefault<ShopTimeOutItemInfo>((Func<ShopTimeOutItemInfo, bool>) (t => t.ItemId == item.id));
          if (shopTimeOutItemInfo != null)
          {
            if (item.end > shopTimeOutItemInfo.End)
            {
              source3.Add(item);
              shopTimeOutItemInfo.End = item.end;
              flag = true;
            }
          }
          else
          {
            source3.Add(item);
            source2.Add(item);
            flag = true;
          }
        }
        if (flag)
        {
          JSON_ShopListArray.Shops shop = GlobalVars.LimitedShopItem.shops;
          IEnumerable<ShopTimeOutItemInfo> shopTimeOutItemInfos = source2.Select<LimitedShopItem, ShopTimeOutItemInfo>((Func<LimitedShopItem, ShopTimeOutItemInfo>) (target => new ShopTimeOutItemInfo(shop.gname, target.id, target.end)));
          if (outItemInfoArray.Infos != null)
            shopTimeOutItemInfos = shopTimeOutItemInfos.Concat<ShopTimeOutItemInfo>((IEnumerable<ShopTimeOutItemInfo>) outItemInfoArray.Infos);
          string json = JsonUtility.ToJson((object) new ShopTimeOutItemInfoArray(shopTimeOutItemInfos.ToArray<ShopTimeOutItemInfo>()));
          PlayerPrefsUtility.SetString(warned_key, json);
        }
        GlobalVars.TimeOutShopItems = source3.Cast<ShopItem>().ToList<ShopItem>();
        if (source3.Count <= 0)
          return;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 104);
      }
      else
      {
        JSON_ShopListArray.Shops shop = GlobalVars.LimitedShopItem.shops;
        string json = JsonUtility.ToJson((object) new ShopTimeOutItemInfoArray(nearbyTimeout.Select<LimitedShopItem, ShopTimeOutItemInfo>((Func<LimitedShopItem, ShopTimeOutItemInfo>) (item => new ShopTimeOutItemInfo(shop.gname, item.id, item.end))).ToArray<ShopTimeOutItemInfo>()));
        PlayerPrefsUtility.SetString(warned_key, json);
        GlobalVars.TimeOutShopItems = nearbyTimeout.Cast<ShopItem>().ToList<ShopItem>();
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 104);
      }
    }

    private void OnSelect(GameObject go)
    {
      LimitedShopBuyList component = this.mBuyItems[this.mBuyItems.FindIndex((Predicate<GameObject>) (p => UnityEngine.Object.op_Equality((UnityEngine.Object) p, (UnityEngine.Object) go)))].GetComponent<LimitedShopBuyList>();
      GlobalVars.ShopBuyIndex = component.limitedShopItem.id;
      if (GlobalVars.ShopType == EShopType.Port && UnityEngine.Object.op_Inequality((UnityEngine.Object) GlobalVars.LimitedShopItem, (UnityEngine.Object) null) && GlobalVars.LimitedShopItem.shops != null && GlobalVars.LimitedShopItem.shops.info != null && GlobalVars.LimitedShopItem.shops.info.unlock == 0)
        this.PopupUnlockInfo(GlobalVars.LimitedShopItem.shops);
      else if (component.limitedShopItem.IsArtifact)
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 103);
      else if (!component.limitedShopItem.IsSet)
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      else
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
    }

    private void OnItemUpdated()
    {
      if (GlobalVars.ShopType == EShopType.Port && UnityEngine.Object.op_Inequality((UnityEngine.Object) GlobalVars.LimitedShopItem, (UnityEngine.Object) null) && GlobalVars.LimitedShopItem.shops != null && GlobalVars.LimitedShopItem.shops.info != null && GlobalVars.LimitedShopItem.shops.info.unlock == 0)
        return;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
    }

    private bool PopupUnlockInfo(JSON_ShopListArray.Shops shop)
    {
      if (shop == null)
        return false;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) instance))
        return false;
      int shopListIndex = AlchemyShopManager.GetShopListIndex(shop);
      if (shopListIndex < 0)
        return false;
      GuildFacilityParam fromFacilityType = instance.MasterParam.GetGuildFacilityFromFacilityType(GuildFacilityParam.eFacilityType.GUILD_SHOP);
      if (fromFacilityType == null)
        return false;
      int requiredUnlockShop = fromFacilityType.GetGuildShopLevelRequiredUnlockShop(shopListIndex + 1);
      if (requiredUnlockShop == 0)
        return false;
      UIUtility.SystemMessage(string.Format(LocalizedText.Get("sys.GUILDFACILITY_SHOP_UNLOCK_NOTICE"), (object) requiredUnlockShop), (UIUtility.DialogResultEvent) null);
      return true;
    }
  }
}
