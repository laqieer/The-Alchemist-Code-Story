// Decompiled with JetBrains decompiler
// Type: SRPG.EventShopBuyWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(103, "武具選択単品", FlowNode.PinTypes.Output, 103)]
  [FlowNode.Pin(101, "アイテム選択セット", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(102, "アイテム更新", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(100, "アイテム選択単品", FlowNode.PinTypes.Output, 100)]
  public class EventShopBuyWindow : MonoBehaviour, IFlowInterface
  {
    public List<GameObject> mBuyItems = new List<GameObject>(12);
    public RectTransform ItemLayoutParent;
    public GameObject ItemTemplate;
    public Button BtnUpdated;
    public GameObject ObjUpdateBtn;
    public GameObject ObjUpdateTime;
    public GameObject ObjLineup;
    public GameObject ObjItemNumLimit;
    public Text TxtPossessionCoinNum;
    public GameObject ImgEventCoinType;
    public Text ShopName;

    private void Awake()
    {
      if ((UnityEngine.Object) this.ItemTemplate != (UnityEngine.Object) null && this.ItemTemplate.activeInHierarchy)
        this.ItemTemplate.SetActive(false);
      if ((UnityEngine.Object) this.BtnUpdated != (UnityEngine.Object) null)
        this.BtnUpdated.onClick.AddListener(new UnityAction(this.OnItemUpdated));
      bool btnUpdate = GlobalVars.EventShopItem.btn_update;
      if ((UnityEngine.Object) this.ObjUpdateBtn != (UnityEngine.Object) null)
        this.ObjUpdateBtn.SetActive(btnUpdate);
      if ((UnityEngine.Object) this.ObjUpdateTime != (UnityEngine.Object) null)
        this.ObjUpdateTime.SetActive(btnUpdate);
      if ((UnityEngine.Object) this.ObjLineup != (UnityEngine.Object) null)
        this.ObjLineup.SetActive(btnUpdate);
      if ((UnityEngine.Object) this.ObjItemNumLimit != (UnityEngine.Object) null)
        this.ObjItemNumLimit.SetActive(!btnUpdate);
      if (!((UnityEngine.Object) this.ShopName != (UnityEngine.Object) null))
        return;
      this.ShopName.text = LocalizedText.Get(GlobalVars.EventShopItem.shops.info.title);
    }

    private void Start()
    {
    }

    public void Activated(int pinID)
    {
      this.Refresh();
    }

    private void Refresh()
    {
      MonoSingleton<GameManager>.Instance.Player.UpdateEventCoin();
      if ((UnityEngine.Object) this.ItemTemplate == (UnityEngine.Object) null)
        return;
      this.SetPossessionCoinParam();
      EventShopData eventShopData = MonoSingleton<GameManager>.Instance.Player.GetEventShopData();
      DebugUtility.Assert(eventShopData != null, "ショップ情報が存在しない");
      for (int index = 0; index < this.mBuyItems.Count; ++index)
        this.mBuyItems[index].gameObject.SetActive(false);
      int count = eventShopData.items.Count;
      for (int index = 0; index < count; ++index)
      {
        EventShopItem data1 = eventShopData.items[index];
        if (index >= this.mBuyItems.Count)
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ItemTemplate);
          gameObject.transform.SetParent((Transform) this.ItemLayoutParent, false);
          this.mBuyItems.Add(gameObject);
        }
        GameObject mBuyItem = this.mBuyItems[index];
        mBuyItem.GetComponentInChildren<EventShopBuyList>().eventShopItem = data1;
        DataSource.Bind<EventShopItem>(mBuyItem, data1);
        if (data1.IsArtifact)
        {
          ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(data1.iname);
          DataSource.Bind<ArtifactParam>(mBuyItem, artifactParam);
        }
        else
        {
          ItemData data2 = new ItemData();
          data2.Setup(0L, data1.iname, data1.num);
          DataSource.Bind<ItemData>(mBuyItem, data2);
          DataSource.Bind<ItemParam>(mBuyItem, MonoSingleton<GameManager>.Instance.GetItemParam(data1.iname));
        }
        ListItemEvents component1 = mBuyItem.GetComponent<ListItemEvents>();
        if ((UnityEngine.Object) component1 != (UnityEngine.Object) null)
          component1.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelect);
        Button component2 = mBuyItem.GetComponent<Button>();
        if ((UnityEngine.Object) component2 != (UnityEngine.Object) null)
          component2.interactable = !data1.is_soldout;
        mBuyItem.SetActive(true);
      }
      GameParameter.UpdateAll(this.gameObject);
    }

    private void OnSelect(GameObject go)
    {
      GlobalVars.ShopBuyIndex = this.mBuyItems.FindIndex((Predicate<GameObject>) (p => (UnityEngine.Object) p == (UnityEngine.Object) go));
      EventShopBuyList component = this.mBuyItems[GlobalVars.ShopBuyIndex].GetComponent<EventShopBuyList>();
      if (component.eventShopItem.IsArtifact)
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 103);
      else if (!component.eventShopItem.IsSet)
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      else
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
    }

    private void OnItemUpdated()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
    }

    private void SetPossessionCoinParam()
    {
      if ((UnityEngine.Object) this.ImgEventCoinType != (UnityEngine.Object) null)
        DataSource.Bind<ItemParam>(this.ImgEventCoinType, MonoSingleton<GameManager>.Instance.GetItemParam(GlobalVars.EventShopItem.shop_cost_iname));
      if (!((UnityEngine.Object) this.TxtPossessionCoinNum != (UnityEngine.Object) null))
        return;
      DataSource.Bind<EventCoinData>(this.TxtPossessionCoinNum.gameObject, MonoSingleton<GameManager>.Instance.Player.EventCoinList.Find((Predicate<EventCoinData>) (f => f.iname.Equals(GlobalVars.EventShopItem.shop_cost_iname))));
    }
  }
}
