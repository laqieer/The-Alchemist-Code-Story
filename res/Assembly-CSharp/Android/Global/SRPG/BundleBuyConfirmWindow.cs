// Decompiled with JetBrains decompiler
// Type: SRPG.BundleBuyConfirmWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  public class BundleBuyConfirmWindow : MonoBehaviour, IFlowInterface
  {
    private List<LimitedShopSetItemListElement> limited_shop_item_set_list = new List<LimitedShopSetItemListElement>();
    [Description("GameObject to use as list item")]
    public GameObject ItemTemplate;
    public GameObject ItemParent;
    private int itemCount;

    private void Awake()
    {
    }

    private void Start()
    {
      this.Refresh();
    }

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.Refresh();
    }

    private void Refresh()
    {
      List<BundleParam> bundleParam1 = MonoSingleton<GameManager>.Instance.Player.GetBundleParam();
      BundleParam bundleParam2 = bundleParam1[0];
      using (List<BundleParam>.Enumerator enumerator = bundleParam1.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          BundleParam current = enumerator.Current;
          if (current.ProductId == GlobalVars.SelectedProductID)
          {
            bundleParam2 = current;
            break;
          }
        }
      }
      ItemData itemDataByItemId = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(bundleParam2.ProductId);
      this.limited_shop_item_set_list.Clear();
      this.itemCount = 0;
      if (bundleParam2 != null)
      {
        if (bundleParam2.AdditionalPaidCoin > 0)
          this.AddGemContents(bundleParam2, false);
        if (bundleParam2.AdditionalFreeCoin > 0)
          this.AddGemContents(bundleParam2, true);
        if (bundleParam2.Contents.Items != null)
        {
          for (int index = 0; index < bundleParam2.Contents.Items.Count; ++index)
            this.AddItemContents(bundleParam2.Contents.Items[index], false);
        }
        if (bundleParam2.Contents.Units != null)
        {
          for (int index = 0; index < bundleParam2.Contents.Units.Count; ++index)
            this.AddItemContents(bundleParam2.Contents.Units[index], true);
        }
        if (bundleParam2.Contents.Equipments != null)
        {
          for (int index = 0; index < bundleParam2.Contents.Equipments.Count; ++index)
            this.AddArtifactContents(bundleParam2.Contents.Equipments[index]);
        }
      }
      DataSource.Bind<BundleParam>(this.gameObject, bundleParam2);
      DataSource.Bind<ItemData>(this.gameObject, itemDataByItemId);
      GameParameter.UpdateAll(this.gameObject);
    }

    private void AddItemContents(BundleParam.BundleItemInfo itemInfo, bool isUnit = false)
    {
      GameObject gameObject = this.itemCount >= this.limited_shop_item_set_list.Count ? UnityEngine.Object.Instantiate<GameObject>(this.ItemTemplate) : this.limited_shop_item_set_list[this.itemCount].gameObject;
      if ((UnityEngine.Object) gameObject != (UnityEngine.Object) null)
      {
        gameObject.SetActive(true);
        Vector3 localScale = gameObject.transform.localScale;
        gameObject.transform.SetParent(this.ItemParent.transform);
        gameObject.transform.localScale = localScale;
        LimitedShopSetItemListElement component = gameObject.GetComponent<LimitedShopSetItemListElement>();
        ItemData itemData = new ItemData();
        itemData.Setup(0L, itemInfo.Name, itemInfo.Quantity);
        StringBuilder stringBuilder = GameUtility.GetStringBuilder();
        if (isUnit)
        {
          UnitParam unitParam = MonoSingleton<GameManager>.Instance.GetUnitParam(itemInfo.Name);
          stringBuilder.Append(unitParam.name);
        }
        else if (itemData != null)
          stringBuilder.Append(itemData.Param.name);
        stringBuilder.Append(" × ");
        stringBuilder.Append(itemInfo.Quantity.ToString());
        component.itemName.text = stringBuilder.ToString();
        component.itemData = itemData;
        this.limited_shop_item_set_list.Add(component);
      }
      ++this.itemCount;
    }

    private void AddArtifactContents(BundleParam.BundleItemInfo itemInfo)
    {
      GameObject gameObject = this.itemCount >= this.limited_shop_item_set_list.Count ? UnityEngine.Object.Instantiate<GameObject>(this.ItemTemplate) : this.limited_shop_item_set_list[this.itemCount].gameObject;
      if ((UnityEngine.Object) gameObject != (UnityEngine.Object) null)
      {
        gameObject.SetActive(true);
        Vector3 localScale = gameObject.transform.localScale;
        gameObject.transform.SetParent(this.ItemParent.transform);
        gameObject.transform.localScale = localScale;
        LimitedShopSetItemListElement component = gameObject.GetComponent<LimitedShopSetItemListElement>();
        ArtifactParam artifactParam1 = new ArtifactParam();
        ArtifactParam artifactParam2 = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetArtifactParam(itemInfo.Name);
        ItemData itemData = new ItemData();
        StringBuilder stringBuilder = GameUtility.GetStringBuilder();
        if (itemData != null)
          stringBuilder.Append(artifactParam2.name);
        stringBuilder.Append(" × ");
        stringBuilder.Append(itemInfo.Quantity.ToString());
        component.itemName.text = stringBuilder.ToString();
        component.itemData = itemData;
        component.ArtifactParam = artifactParam2;
        this.limited_shop_item_set_list.Add(component);
      }
      ++this.itemCount;
    }

    private void AddGemContents(BundleParam bundleInfo, bool isFreeGems = false)
    {
      GameObject gameObject = this.itemCount >= this.limited_shop_item_set_list.Count ? UnityEngine.Object.Instantiate<GameObject>(this.ItemTemplate) : this.limited_shop_item_set_list[this.itemCount].gameObject;
      if ((UnityEngine.Object) gameObject != (UnityEngine.Object) null)
      {
        gameObject.SetActive(true);
        Vector3 localScale = gameObject.transform.localScale;
        gameObject.transform.SetParent(this.ItemParent.transform);
        gameObject.transform.localScale = localScale;
        LimitedShopSetItemListElement component = gameObject.GetComponent<LimitedShopSetItemListElement>();
        ItemData itemData = new ItemData();
        StringBuilder stringBuilder = GameUtility.GetStringBuilder();
        if (isFreeGems)
        {
          itemData.Setup(0L, "$COIN", bundleInfo.AdditionalFreeCoin);
          stringBuilder.Append(LocalizedText.Get("sys.CONFIG_CHECKCOIN_FREE"));
          stringBuilder.Append(" × ");
          stringBuilder.Append(bundleInfo.AdditionalFreeCoin.ToString());
        }
        else
        {
          itemData.Setup(0L, "$COIN", bundleInfo.AdditionalPaidCoin);
          stringBuilder.Append(LocalizedText.Get("sys.CONFIG_CHECKCOIN_PAY"));
          stringBuilder.Append(" × ");
          stringBuilder.Append(bundleInfo.AdditionalPaidCoin.ToString());
        }
        component.itemName.text = stringBuilder.ToString();
        component.itemData = itemData;
        this.limited_shop_item_set_list.Add(component);
      }
      ++this.itemCount;
    }
  }
}
