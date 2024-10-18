﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ShopSetItemListElement
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class ShopSetItemListElement : MonoBehaviour
  {
    public Text itemName;
    public GameObject ItemIcon;
    public GameObject ArtifactIcon;
    private ItemData mItemData;
    private ArtifactParam mArtifactParam;
    private GameObject mDetailWindow;
    public GameObject ArtifactDetailWindow;
    public GameObject ItemDetailWindow;
    private ShopItem mShopItem;

    public ItemData itemData
    {
      set
      {
        DataSource.Bind<ItemData>(this.gameObject, value);
        this.mItemData = value;
      }
      get
      {
        return this.mItemData;
      }
    }

    public ArtifactParam ArtifactParam
    {
      set
      {
        DataSource.Bind<ArtifactParam>(this.gameObject, value);
        this.mArtifactParam = value;
      }
      get
      {
        return this.mArtifactParam;
      }
    }

    public void SetShopItemDesc(Json_ShopItemDesc item)
    {
      this.ItemIcon.SetActive(!item.IsArtifact);
      this.ArtifactIcon.SetActive(item.IsArtifact);
      if (this.mShopItem == null)
        this.mShopItem = (ShopItem) new LimitedShopItem();
      this.mShopItem.num = item.num;
      this.mShopItem.iname = item.iname;
    }

    public void OnClickDetailArtifact()
    {
      if ((UnityEngine.Object) this.mDetailWindow != (UnityEngine.Object) null)
        return;
      this.mDetailWindow = UnityEngine.Object.Instantiate<GameObject>(this.ArtifactDetailWindow);
      ArtifactData data = new ArtifactData();
      data.Deserialize(new Json_Artifact()
      {
        iname = this.mArtifactParam.iname,
        rare = this.mArtifactParam.rareini
      });
      DataSource.Bind<ArtifactData>(this.mDetailWindow, data);
    }

    public void OnClickDetailItem()
    {
      if ((UnityEngine.Object) this.mDetailWindow != (UnityEngine.Object) null)
        return;
      this.mDetailWindow = UnityEngine.Object.Instantiate<GameObject>(this.ItemDetailWindow);
      DataSource.Bind<ItemData>(this.mDetailWindow, MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(this.mItemData.Param.iname));
      DataSource.Bind<ItemParam>(this.mDetailWindow, MonoSingleton<GameManager>.Instance.GetItemParam(this.mItemData.Param.iname));
      DataSource.Bind<ShopItem>(this.mDetailWindow, this.mShopItem);
    }
  }
}
