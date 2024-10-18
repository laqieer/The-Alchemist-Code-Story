// Decompiled with JetBrains decompiler
// Type: SRPG.EventShopList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(101, "更新あり：イベントショップが選択された", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(104, "マルチショップが選択された", FlowNode.PinTypes.Output, 104)]
  [FlowNode.Pin(103, "アリーナショップが選択された", FlowNode.PinTypes.Output, 103)]
  [FlowNode.Pin(0, "指定イベントショップの商品を開く", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "所持コイン更新", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(102, "更新なし：イベントショップが選択された", FlowNode.PinTypes.Output, 102)]
  public class EventShopList : SRPG_ListBase, IFlowInterface
  {
    [Description("リストアイテムとして使用するゲームオブジェクト")]
    public GameObject ItemTemplate;
    public GameObject ArenaShopTemplate;
    public GameObject MultiShopTemplate;

    public void Activated(int pinID)
    {
      if (pinID == 0)
      {
        switch (GlobalVars.SelectionCoinListType)
        {
          case GlobalVars.CoinListSelectionType.EventShop:
            if ((UnityEngine.Object) GlobalVars.SelectionEventShop != (UnityEngine.Object) null)
            {
              GlobalVars.EventShopItem = GlobalVars.SelectionEventShop;
              GlobalVars.ShopType = EShopType.Event;
              if (GlobalVars.EventShopItem.btn_update)
              {
                FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
                break;
              }
              FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
              break;
            }
            break;
          case GlobalVars.CoinListSelectionType.ArenaShop:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 103);
            break;
          case GlobalVars.CoinListSelectionType.MultiShop:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 104);
            break;
        }
      }
      if (pinID != 1)
        return;
      GameParameter.UpdateAll(this.gameObject);
    }

    protected override void Start()
    {
      if ((bool) ((UnityEngine.Object) this.ItemTemplate))
        this.ItemTemplate.SetActive(false);
      if ((bool) ((UnityEngine.Object) this.ArenaShopTemplate))
        this.ArenaShopTemplate.SetActive(false);
      if ((bool) ((UnityEngine.Object) this.MultiShopTemplate))
        this.MultiShopTemplate.SetActive(false);
      base.Start();
    }

    public void DestroyItems()
    {
      if (GlobalVars.EventShopListItems.Count <= 0)
        return;
      for (int index = GlobalVars.EventShopListItems.Count - 1; index >= 0; --index)
      {
        if ((UnityEngine.Object) GlobalVars.EventShopListItems[index] != (UnityEngine.Object) null)
        {
          GlobalVars.EventShopListItems[index].GetComponent<ListItemEvents>().OnSelect = (ListItemEvents.ListItemEvent) null;
          UnityEngine.Object.Destroy((UnityEngine.Object) GlobalVars.EventShopListItems[index]);
        }
      }
      GlobalVars.EventShopListItems.Clear();
    }

    private void OnSelectItem(GameObject go)
    {
      EventShopListItem component = go.GetComponent<EventShopListItem>();
      if ((UnityEngine.Object) component == (UnityEngine.Object) null)
        return;
      GlobalVars.EventShopItem = component;
      GlobalVars.ShopType = EShopType.Event;
      if (GlobalVars.EventShopItem.btn_update)
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
      else
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
    }

    public void AddEventShopList(JSON_ShopListArray.Shops[] shops)
    {
      for (int index = 0; index < shops.Length; ++index)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ItemTemplate);
        EventShopListItem component = gameObject.GetComponent<EventShopListItem>();
        component.SetShopList(shops[index]);
        gameObject.transform.SetParent(this.transform);
        gameObject.transform.localScale = this.ItemTemplate.transform.localScale;
        gameObject.GetComponent<ListItemEvents>().OnSelect = new ListItemEvents.ListItemEvent(this.OnSelectItem);
        gameObject.SetActive(true);
        GlobalVars.EventShopListItems.Add(component);
      }
    }

    private void OnSelectArenaItem(GameObject go)
    {
      GlobalVars.ShopType = EShopType.Arena;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 103);
    }

    public void AddArenaShopList()
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ArenaShopTemplate);
      gameObject.transform.SetParent(this.transform);
      gameObject.transform.localScale = this.ItemTemplate.transform.localScale;
      gameObject.GetComponent<ListItemEvents>().OnSelect = new ListItemEvents.ListItemEvent(this.OnSelectArenaItem);
      gameObject.SetActive(true);
    }

    private void OnSelectMultiItem(GameObject go)
    {
      GlobalVars.ShopType = EShopType.Multi;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 104);
    }

    public void AddMultiShopList()
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.MultiShopTemplate);
      gameObject.transform.SetParent(this.transform);
      gameObject.transform.localScale = this.ItemTemplate.transform.localScale;
      gameObject.GetComponent<ListItemEvents>().OnSelect = new ListItemEvents.ListItemEvent(this.OnSelectMultiItem);
      gameObject.SetActive(true);
    }
  }
}
