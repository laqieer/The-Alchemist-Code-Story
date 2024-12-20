﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ShopSetItemListElement
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class ShopSetItemListElement : MonoBehaviour
  {
    public Text itemName;
    public GameObject ItemIcon;
    public GameObject ItemDetailWindow;
    public GameObject ArtifactIcon;
    public GameObject ArtifactDetailWindow;
    public GameObject ConceptCard;
    public GameObject ConceptCardDetailWindow;
    private GameObject mDetailWindow;
    private ShopItem mShopItem;
    private ItemData mItemData;
    private ArtifactParam mArtifactParam;
    private ConceptCardData mConceptCardData;

    public ItemData itemData
    {
      set
      {
        DataSource.Bind<ItemData>(((Component) this).gameObject, value);
        this.mItemData = value;
      }
      get => this.mItemData;
    }

    public ArtifactParam ArtifactParam
    {
      set
      {
        DataSource.Bind<ArtifactParam>(((Component) this).gameObject, value);
        this.mArtifactParam = value;
      }
      get => this.mArtifactParam;
    }

    public void SetupConceptCard(ConceptCardData conceptCardData)
    {
      this.mConceptCardData = conceptCardData;
      if (Object.op_Equality((Object) this.ConceptCard, (Object) null))
      {
        DebugUtility.LogError("ConceptCard == null");
      }
      else
      {
        ConceptCardIcon componentInChildren = this.ConceptCard.GetComponentInChildren<ConceptCardIcon>();
        if (!Object.op_Inequality((Object) componentInChildren, (Object) null))
          return;
        componentInChildren.Setup(conceptCardData);
      }
    }

    public void SetShopItemDesc(Json_ShopItemDesc item)
    {
      this.ItemIcon.SetActive(false);
      this.ArtifactIcon.SetActive(false);
      this.ConceptCard.SetActive(false);
      if (item.IsItem)
        this.ItemIcon.SetActive(true);
      else if (item.IsArtifact)
        this.ArtifactIcon.SetActive(true);
      else if (item.IsConceptCard)
        this.ConceptCard.SetActive(true);
      else
        DebugUtility.LogError(string.Format("不明な商品タイプが設定されています (item.iname({0}) => {1})", (object) item.iname, (object) item.itype));
      if (this.mShopItem == null)
        this.mShopItem = (ShopItem) new LimitedShopItem();
      this.mShopItem.num = item.num;
      this.mShopItem.iname = item.iname;
    }

    public void OnClickDetailArtifact()
    {
      if (Object.op_Inequality((Object) this.mDetailWindow, (Object) null))
        return;
      this.mDetailWindow = Object.Instantiate<GameObject>(this.ArtifactDetailWindow);
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
      if (Object.op_Inequality((Object) this.mDetailWindow, (Object) null))
        return;
      this.mDetailWindow = Object.Instantiate<GameObject>(this.ItemDetailWindow);
      DataSource.Bind<ItemData>(this.mDetailWindow, MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(this.mItemData.Param.iname));
      DataSource.Bind<ItemParam>(this.mDetailWindow, MonoSingleton<GameManager>.Instance.GetItemParam(this.mItemData.Param.iname));
      DataSource.Bind<ShopItem>(this.mDetailWindow, this.mShopItem);
    }

    public void OnClickDetailConceptCard()
    {
      if (Object.op_Inequality((Object) this.mDetailWindow, (Object) null))
        return;
      GlobalVars.SelectedConceptCardData.Set(this.mConceptCardData);
      this.mDetailWindow = Object.Instantiate<GameObject>(this.ConceptCardDetailWindow);
    }
  }
}
