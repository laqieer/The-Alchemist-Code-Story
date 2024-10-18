// Decompiled with JetBrains decompiler
// Type: SRPG.LimitedShopGiftWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SRPG
{
  public class LimitedShopGiftWindow : MonoBehaviour
  {
    private List<GameObject> mItems = new List<GameObject>();
    [SerializeField]
    private GameObject ItemParent;
    [SerializeField]
    private GameObject ItemTemplate;

    private void Start()
    {
      if ((UnityEngine.Object) this.ItemTemplate != (UnityEngine.Object) null)
        this.ItemTemplate.SetActive(false);
      this.Refresh();
    }

    private void Refresh()
    {
      GameUtility.DestroyGameObjects(this.mItems);
      this.mItems.Clear();
      LimitedShopData limitedShopData = MonoSingleton<GameManager>.Instance.Player.GetLimitedShopData();
      if (limitedShopData == null || limitedShopData.items.Count <= 0 || (UnityEngine.Object) this.ItemTemplate == (UnityEngine.Object) null)
        return;
      int shopdata_index = GlobalVars.ShopBuyIndex;
      Transform parent = !((UnityEngine.Object) this.ItemParent != (UnityEngine.Object) null) ? this.ItemTemplate.transform.parent : this.ItemParent.transform;
      LimitedShopItem limitedShopItem = limitedShopData.items.FirstOrDefault<LimitedShopItem>((Func<LimitedShopItem, bool>) (item => item.id == shopdata_index));
      List<Json_ShopItemDesc> jsonShopItemDescList = new List<Json_ShopItemDesc>();
      if (limitedShopItem.IsArtifact)
        jsonShopItemDescList.Add(new Json_ShopItemDesc()
        {
          iname = limitedShopItem.iname,
          itype = ShopData.ShopItemType2String(limitedShopItem.ShopItemType),
          num = limitedShopItem.num
        });
      else if (limitedShopItem.children != null && limitedShopItem.children.Length > 0)
        jsonShopItemDescList.AddRange((IEnumerable<Json_ShopItemDesc>) limitedShopItem.children);
      if (jsonShopItemDescList.Count > 0)
      {
        for (int index = 0; index < jsonShopItemDescList.Count; ++index)
        {
          Json_ShopItemDesc shop_item_desc = jsonShopItemDescList[index];
          string empty = string.Empty;
          GameObject gameObject;
          string name;
          if (shop_item_desc.IsArtifact)
          {
            ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(shop_item_desc.iname);
            if (artifactParam != null)
            {
              gameObject = this.InstantiateItem<ArtifactParam>(this.ItemTemplate, parent, artifactParam);
              name = artifactParam.name;
            }
            else
              continue;
          }
          else if (shop_item_desc.IsConceptCard)
          {
            ConceptCardData cardDataForDisplay = ConceptCardData.CreateConceptCardDataForDisplay(shop_item_desc.iname);
            if (cardDataForDisplay != null)
            {
              gameObject = this.InstantiateItem<ConceptCardData>(this.ItemTemplate, parent, cardDataForDisplay);
              ConceptCardIcon componentInChildren = gameObject.GetComponentInChildren<ConceptCardIcon>();
              if ((UnityEngine.Object) componentInChildren != (UnityEngine.Object) null)
                componentInChildren.Setup(cardDataForDisplay);
              name = cardDataForDisplay.Param.name;
            }
            else
              continue;
          }
          else
          {
            ItemData itemData = new ItemData();
            if (itemData.Setup(0L, shop_item_desc.iname, shop_item_desc.num))
            {
              gameObject = this.InstantiateItem<ItemData>(this.ItemTemplate, parent, itemData);
              name = itemData.Param.name;
            }
            else
              continue;
          }
          LimitedShopGiftItem component = gameObject.GetComponent<LimitedShopGiftItem>();
          if ((UnityEngine.Object) component != (UnityEngine.Object) null)
            component.SetShopItemInfo(shop_item_desc, name, GlobalVars.ShopBuyAmount);
        }
      }
      GameParameter.UpdateAll(parent.gameObject);
    }

    public GameObject InstantiateItem<BindType>(GameObject template, Transform parent, BindType item)
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(template);
      gameObject.transform.SetParent(parent, false);
      DataSource.Bind<BindType>(gameObject, item, false);
      this.mItems.Add(gameObject);
      gameObject.SetActive(true);
      return gameObject;
    }
  }
}
