// Decompiled with JetBrains decompiler
// Type: SRPG.EventShopGiftWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class EventShopGiftWindow : MonoBehaviour
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
      EventShopData eventShopData = MonoSingleton<GameManager>.Instance.Player.GetEventShopData();
      if (eventShopData == null || eventShopData.items.Count <= 0 || (UnityEngine.Object) this.ItemTemplate == (UnityEngine.Object) null)
        return;
      int shopBuyIndex = GlobalVars.ShopBuyIndex;
      Transform parent = !((UnityEngine.Object) this.ItemParent != (UnityEngine.Object) null) ? this.ItemTemplate.transform.parent : this.ItemParent.transform;
      EventShopItem eventShopItem = eventShopData.items[shopBuyIndex];
      if (eventShopItem.IsArtifact)
      {
        eventShopItem.children = new Json_ShopItemDesc[1];
        eventShopItem.children[0] = new Json_ShopItemDesc();
        eventShopItem.children[0].iname = eventShopItem.iname;
        eventShopItem.children[0].num = eventShopItem.num;
      }
      if (eventShopItem.children != null && eventShopItem.children.Length > 0)
      {
        foreach (Json_ShopItemDesc child in eventShopItem.children)
        {
          string empty = string.Empty;
          GameObject gameObject;
          string name;
          if (child.IsArtifact)
          {
            ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(child.iname);
            if (artifactParam != null)
            {
              gameObject = this.InstantiateItem<ArtifactParam>(this.ItemTemplate, parent, artifactParam);
              name = artifactParam.name;
            }
            else
              continue;
          }
          else
          {
            ItemData itemData = new ItemData();
            if (itemData.Setup(0L, child.iname, child.num))
            {
              gameObject = this.InstantiateItem<ItemData>(this.ItemTemplate, parent, itemData);
              name = itemData.Param.name;
            }
            else
              continue;
          }
          EventShopGiftItem component = gameObject.GetComponent<EventShopGiftItem>();
          if ((UnityEngine.Object) component != (UnityEngine.Object) null)
            component.SetShopItemInfo(child, name);
        }
      }
      GameParameter.UpdateAll(parent.gameObject);
    }

    public GameObject InstantiateItem<BindType>(GameObject template, Transform parent, BindType item)
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(template);
      gameObject.transform.SetParent(parent, false);
      DataSource.Bind<BindType>(gameObject, item);
      this.mItems.Add(gameObject);
      gameObject.SetActive(true);
      return gameObject;
    }
  }
}
