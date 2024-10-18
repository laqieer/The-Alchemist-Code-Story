// Decompiled with JetBrains decompiler
// Type: SRPG.LimitedShopSetItemListElement
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class LimitedShopSetItemListElement : MonoBehaviour
  {
    public Text itemName;
    public GameObject ItemIcon;
    public GameObject ItemDetailWindow;
    public GameObject ArtifactIcon;
    public GameObject ArtifactDetailWindow;
    public GameObject ConceptCard;
    public GameObject ConceptCardDetailWindow;
    private GameObject mDetailWindow;
    private LimitedShopItem mLimitedShopItem;
    private ItemData mItemData;
    private ArtifactParam mArtifactParam;
    private ConceptCardData mConceptCardData;

    public ItemData itemData
    {
      set
      {
        DataSource.Bind<ItemData>(this.gameObject, value, false);
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
        DataSource.Bind<ArtifactParam>(this.gameObject, value, false);
        this.mArtifactParam = value;
      }
      get
      {
        return this.mArtifactParam;
      }
    }

    public void SetupConceptCard(ConceptCardData conceptCardData)
    {
      this.mConceptCardData = conceptCardData;
      if ((UnityEngine.Object) this.ConceptCard == (UnityEngine.Object) null)
      {
        DebugUtility.LogError("ConceptCard == null");
      }
      else
      {
        ConceptCardIcon componentInChildren = this.ConceptCard.GetComponentInChildren<ConceptCardIcon>();
        if (!((UnityEngine.Object) componentInChildren != (UnityEngine.Object) null))
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
      DataSource.Bind<ArtifactData>(this.mDetailWindow, data, false);
    }

    public void OnClickDetailItem()
    {
      if ((UnityEngine.Object) this.mDetailWindow != (UnityEngine.Object) null)
        return;
      this.mDetailWindow = UnityEngine.Object.Instantiate<GameObject>(this.ItemDetailWindow);
      DataSource.Bind<ItemData>(this.mDetailWindow, MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(this.mItemData.Param.iname), false);
      DataSource.Bind<ItemParam>(this.mDetailWindow, MonoSingleton<GameManager>.Instance.GetItemParam(this.mItemData.Param.iname), false);
      DataSource.Bind<LimitedShopItem>(this.mDetailWindow, this.mLimitedShopItem, false);
    }

    public void OnClickDetailConceptCard()
    {
      if ((UnityEngine.Object) this.mDetailWindow != (UnityEngine.Object) null)
        return;
      GlobalVars.SelectedConceptCardData.Set(this.mConceptCardData);
      this.mDetailWindow = UnityEngine.Object.Instantiate<GameObject>(this.ConceptCardDetailWindow);
    }
  }
}
