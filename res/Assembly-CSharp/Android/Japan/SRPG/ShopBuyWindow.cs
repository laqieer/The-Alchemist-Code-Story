﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ShopBuyWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "Refresh完了", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(100, "アイテム選択単品", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "アイテム選択セット", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "アイテム更新", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(103, "武具選択単品", FlowNode.PinTypes.Output, 103)]
  public class ShopBuyWindow : MonoBehaviour, IFlowInterface
  {
    public List<GameObject> mBuyItems = new List<GameObject>(12);
    public RectTransform ItemLayoutParent;
    public GameObject ItemTemplate;
    public Button BtnUpdated;
    public Text TxtTitle;
    public Text TxtUpdateTime;
    public Text TxtUpdated;
    public GameObject Updated;
    public GameObject Update;
    public GameObject TitleObj;
    public GameObject Lineup;

    private void Awake()
    {
      if ((UnityEngine.Object) this.ItemTemplate != (UnityEngine.Object) null && this.ItemTemplate.activeInHierarchy)
        this.ItemTemplate.SetActive(false);
      if ((UnityEngine.Object) this.BtnUpdated != (UnityEngine.Object) null)
        this.BtnUpdated.onClick.AddListener(new UnityAction(this.OnItemUpdated));
      if ((UnityEngine.Object) this.TxtTitle != (UnityEngine.Object) null)
        this.TxtTitle.text = LocalizedText.Get("sys.SHOP_BUY_TITLE");
      if ((UnityEngine.Object) this.TxtUpdateTime != (UnityEngine.Object) null)
        this.TxtUpdateTime.text = LocalizedText.Get("sys.UPDATE_TIME");
      if ((UnityEngine.Object) this.TxtUpdated != (UnityEngine.Object) null)
        this.TxtUpdated.text = LocalizedText.Get("sys.CMD_UPDATED");
      switch (GlobalVars.ShopType)
      {
        case EShopType.Normal:
        case EShopType.Tabi:
        case EShopType.Kimagure:
        case EShopType.Guerrilla:
          this.TitleObj.SetActive(true);
          break;
        default:
          this.TitleObj.SetActive(false);
          break;
      }
    }

    public void Activated(int pinID)
    {
      this.Refresh();
    }

    private void Refresh()
    {
      if ((UnityEngine.Object) this.ItemTemplate == (UnityEngine.Object) null)
        return;
      ShopData shopData = MonoSingleton<GameManager>.Instance.Player.GetShopData(GlobalVars.ShopType);
      DebugUtility.Assert(shopData != null, "ショップ情報が存在しない");
      if ((UnityEngine.Object) this.Updated != (UnityEngine.Object) null)
        this.Updated.SetActive(shopData.btn_update);
      if ((UnityEngine.Object) this.Update != (UnityEngine.Object) null)
        this.Update.SetActive(shopData.btn_update);
      if ((UnityEngine.Object) this.Lineup != (UnityEngine.Object) null)
        this.Lineup.SetActive(shopData.btn_update);
      for (int index = 0; index < this.mBuyItems.Count; ++index)
        this.mBuyItems[index].gameObject.SetActive(false);
      int count = shopData.items.Count;
      for (int index = 0; index < count; ++index)
      {
        ShopItem data1 = shopData.items[index];
        if (index >= this.mBuyItems.Count)
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ItemTemplate);
          gameObject.transform.SetParent((Transform) this.ItemLayoutParent, false);
          this.mBuyItems.Add(gameObject);
        }
        GameObject mBuyItem = this.mBuyItems[index];
        ShopBuyList componentInChildren = mBuyItem.GetComponentInChildren<ShopBuyList>();
        componentInChildren.ShopItem = data1;
        componentInChildren.SetupDiscountUI();
        DataSource.Bind<ShopItem>(mBuyItem, data1, false);
        if (data1.IsArtifact)
        {
          ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(data1.iname);
          DataSource.Bind<ArtifactParam>(mBuyItem, artifactParam, false);
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
          DataSource.Bind<ItemData>(mBuyItem, data2, false);
          DataSource.Bind<ItemParam>(mBuyItem, MonoSingleton<GameManager>.Instance.GetItemParam(data1.iname), false);
        }
        else
          DebugUtility.LogError(string.Format("不明な商品タイプが設定されています (shopitem.iname({0}) => {1})", (object) data1.iname, (object) data1.ShopItemType));
        ListItemEvents component1 = mBuyItem.GetComponent<ListItemEvents>();
        if ((UnityEngine.Object) component1 != (UnityEngine.Object) null)
          component1.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelect);
        Button component2 = mBuyItem.GetComponent<Button>();
        if ((UnityEngine.Object) component2 != (UnityEngine.Object) null)
          component2.interactable = !data1.is_soldout;
        mBuyItem.SetActive(true);
      }
      GameParameter.UpdateAll(this.gameObject);
      if (GlobalVars.ShopType == EShopType.AwakePiece)
        GameParameter.UpdateValuesOfType(GameParameter.ParameterTypes.GLOBAL_PLAYER_PIECEPOINT);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 2);
    }

    private void OnSelect(GameObject go)
    {
      ShopBuyList component = this.mBuyItems[this.mBuyItems.FindIndex((Predicate<GameObject>) (p => (UnityEngine.Object) p == (UnityEngine.Object) go))].GetComponent<ShopBuyList>();
      GlobalVars.ShopBuyIndex = component.ShopItem.id;
      if (component.ShopItem.IsArtifact)
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 103);
      else if (!component.ShopItem.IsSet)
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      else
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
    }

    private void OnItemUpdated()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
    }
  }
}
