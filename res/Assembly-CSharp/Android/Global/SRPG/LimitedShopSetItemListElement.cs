﻿// Decompiled with JetBrains decompiler
// Type: SRPG.LimitedShopSetItemListElement
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class LimitedShopSetItemListElement : MonoBehaviour
  {
    public Text itemName;
    public GameObject ItemIcon;
    public GameObject ArtifactIcon;
    private ItemData mItemData;
    private ArtifactParam mArtifactParam;
    private GameObject mDetailWindow;
    public GameObject ArtifactDetailWindow;
    public GameObject ItemDetailWindow;
    private LimitedShopItem mLimitedShopItem;

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
      if (this.mLimitedShopItem == null)
        this.mLimitedShopItem = new LimitedShopItem();
      this.mLimitedShopItem.num = item.num;
      this.mLimitedShopItem.iname = item.iname;
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
      DataSource.Bind<LimitedShopItem>(this.mDetailWindow, this.mLimitedShopItem);
    }
  }
}