// Decompiled with JetBrains decompiler
// Type: SRPG.LimitedShopList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(101, "限定ショップが選択された", FlowNode.PinTypes.Output, 101)]
  public class LimitedShopList : SRPG_ListBase, IFlowInterface
  {
    private List<LimitedShopListItem> limited_shop_list = new List<LimitedShopListItem>();
    [Description("リストアイテムとして使用するゲームオブジェクト")]
    public GameObject ItemTemplate;
    [Range(0.0f, 100f)]
    public int NowOpenShopCount;

    protected override void Start()
    {
      base.Start();
    }

    private void RefreshItems(JSON_ShopListArray.Shops[] shops)
    {
      this.NowOpenShopCount = shops.Length;
      for (int count = this.limited_shop_list.Count; count < this.NowOpenShopCount; ++count)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ItemTemplate);
        LimitedShopListItem component = gameObject.GetComponent<LimitedShopListItem>();
        component.SetShopList(shops[count]);
        gameObject.transform.SetParent(this.transform);
        gameObject.transform.localScale = this.ItemTemplate.transform.localScale;
        gameObject.GetComponent<ListItemEvents>().OnSelect = new ListItemEvents.ListItemEvent(this.OnSelectItem);
        gameObject.SetActive(true);
        this.limited_shop_list.Add(component);
      }
    }

    private void DestroyItems()
    {
      if (this.limited_shop_list.Count <= 0)
        return;
      for (int index = this.NowOpenShopCount - 1; index >= 0; --index)
      {
        this.limited_shop_list[index].GetComponent<ListItemEvents>().OnSelect = (ListItemEvents.ListItemEvent) null;
        UnityEngine.Object.Destroy((UnityEngine.Object) this.limited_shop_list[index]);
      }
      this.limited_shop_list.Clear();
    }

    private void OnSelectItem(GameObject go)
    {
      LimitedShopListItem component = go.GetComponent<LimitedShopListItem>();
      if ((UnityEngine.Object) component == (UnityEngine.Object) null)
        return;
      GlobalVars.LimitedShopItem = component;
      GlobalVars.ShopType = EShopType.Limited;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
    }

    public void Activated(int pinID)
    {
    }

    public void SetLimitedShopList(JSON_ShopListArray.Shops[] shops)
    {
      this.DestroyItems();
      this.RefreshItems(shops);
    }
  }
}
