// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class ConceptCardIcon : MonoBehaviour
  {
    [SerializeField]
    private RawImage mIconImage;
    [SerializeField]
    private Text mNameText;
    [SerializeField]
    private Text mLevelTitleText;
    [SerializeField]
    private Text mLevelText;
    [SerializeField]
    private Text mLevelCapText;
    [SerializeField]
    private Image mRarityImage;
    [SerializeField]
    private ImageArray mRarityFrame;
    [SerializeField]
    private Text mTrustText;
    [SerializeField]
    private GameObject mFavorite;
    [SerializeField]
    private GameObject mOwner;
    [SerializeField]
    private Image mOwnerIcon;
    [SerializeField]
    private GameObject mSameCardIcon;
    [SerializeField]
    private GameObject mExistSwitchOn;
    [SerializeField]
    private GameObject mExistSwitchOff;
    [SerializeField]
    private Text mCardNum;
    [SerializeField]
    private GameObject mNotSale;
    private ConceptCardData mConceptCard;

    public ConceptCardData ConceptCard
    {
      get
      {
        return this.mConceptCard;
      }
    }

    public void Setup(ConceptCardData card)
    {
      this.mConceptCard = card;
      if (card != null)
        this.Refresh();
      else
        this.ResetIcon();
    }

    public void ResetIcon()
    {
      this.mConceptCard = (ConceptCardData) null;
      this.mIconImage.texture = (Texture) null;
      this.Refresh();
    }

    public void Refresh()
    {
      DataSource.Bind<ConceptCardData>(this.gameObject, this.mConceptCard, false);
      GameParameter.UpdateAll(this.gameObject);
      this.RefreshExistImage();
      this.RefreshIconImage();
      this.RefreshIconParam();
    }

    public UnitData GetOwner()
    {
      if (this.mConceptCard == null)
        return (UnitData) null;
      return this.mConceptCard.GetOwner();
    }

    private void RefreshExistImage()
    {
      bool flag = this.mConceptCard != null;
      if ((UnityEngine.Object) this.mExistSwitchOn != (UnityEngine.Object) null)
        this.mExistSwitchOn.SetActive(flag);
      if (!((UnityEngine.Object) this.mExistSwitchOff != (UnityEngine.Object) null))
        return;
      this.mExistSwitchOff.SetActive(!flag);
    }

    private void RefreshIconImage()
    {
      if (this.mConceptCard == null || (UnityEngine.Object) this.mIconImage == (UnityEngine.Object) null)
        MonoSingleton<GameManager>.Instance.CancelTextureLoadRequest(this.mIconImage);
      else
        MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.mIconImage, AssetPath.ConceptCardIcon(this.mConceptCard.Param));
    }

    private void RefreshIconParam()
    {
      if ((UnityEngine.Object) this.mRarityFrame != (UnityEngine.Object) null)
        this.mRarityFrame.gameObject.SetActive(false);
      if (this.mConceptCard == null)
      {
        if ((UnityEngine.Object) this.mLevelTitleText != (UnityEngine.Object) null)
          this.mLevelTitleText.gameObject.SetActive(false);
        if ((UnityEngine.Object) this.mLevelText != (UnityEngine.Object) null)
          this.mLevelText.gameObject.SetActive(false);
        if (!((UnityEngine.Object) this.mLevelCapText != (UnityEngine.Object) null))
          return;
        this.mLevelCapText.gameObject.SetActive(false);
      }
      else
      {
        this.SetNameText(this.mConceptCard.Param.name);
        this.SetLevelText((int) this.mConceptCard.Lv);
        this.SetLevelCapText((int) this.mConceptCard.CurrentLvCap);
        this.SetTrustText((int) this.mConceptCard.Trust);
        this.SetRarityImaget(this.mConceptCard.Param.rare);
        this.SetFavorite(this.mConceptCard.Favorite);
        this.SetRarityFrame((int) this.mConceptCard.Rarity);
        if ((UnityEngine.Object) this.mOwner != (UnityEngine.Object) null)
        {
          UnitData owner = this.GetOwner();
          if (owner != null)
          {
            this.mOwner.SetActive(true);
            this.SetOwnerIcon(this.mOwnerIcon, owner);
          }
          else
            this.mOwner.SetActive(false);
        }
        this.SetSameCardIcon();
      }
    }

    public void SetNameText(string name)
    {
      if ((UnityEngine.Object) this.mNameText == (UnityEngine.Object) null)
        return;
      this.mNameText.text = name.ToString();
    }

    public void SetLevelText(int lv)
    {
      if ((UnityEngine.Object) this.mLevelText == (UnityEngine.Object) null)
        return;
      this.mLevelText.text = lv.ToString();
      this.mLevelText.gameObject.SetActive(true);
      if (!((UnityEngine.Object) this.mLevelTitleText != (UnityEngine.Object) null))
        return;
      this.mLevelTitleText.gameObject.SetActive(true);
    }

    public void SetLevelCapText(int lvcap)
    {
      if ((UnityEngine.Object) this.mLevelCapText == (UnityEngine.Object) null)
        return;
      this.mLevelCapText.text = lvcap.ToString();
      this.mLevelCapText.gameObject.SetActive(true);
      if (!((UnityEngine.Object) this.mLevelTitleText != (UnityEngine.Object) null))
        return;
      this.mLevelTitleText.gameObject.SetActive(true);
    }

    public void SetTrustText(int trust)
    {
      if ((UnityEngine.Object) this.mTrustText == (UnityEngine.Object) null)
        return;
      ConceptCardManager.SubstituteTrustFormat(this.mConceptCard, this.mTrustText, trust, false);
    }

    public void SetNoRewardTrustText()
    {
      if ((UnityEngine.Object) this.mTrustText == (UnityEngine.Object) null)
        return;
      this.mTrustText.text = "---";
    }

    public void SetRarityImaget(int rare)
    {
      if ((UnityEngine.Object) this.mRarityImage == (UnityEngine.Object) null)
        return;
      this.mRarityImage.sprite = (Sprite) null;
      GameSettings instance = GameSettings.Instance;
      if (!((UnityEngine.Object) instance != (UnityEngine.Object) null) || instance.ConceptCardIcon_Rarity.Length <= 0)
        return;
      this.mRarityImage.sprite = instance.ConceptCardIcon_Rarity[rare];
    }

    public void SetFavorite(bool favorite)
    {
      if ((UnityEngine.Object) this.mFavorite == (UnityEngine.Object) null)
        return;
      this.mFavorite.SetActive(favorite);
    }

    public void SetRarityFrame(int rarity)
    {
      if ((UnityEngine.Object) this.mRarityFrame == (UnityEngine.Object) null)
        return;
      this.mRarityFrame.gameObject.SetActive(true);
      GameSettings instance = GameSettings.Instance;
      if ((UnityEngine.Object) instance == (UnityEngine.Object) null)
        return;
      this.mRarityFrame.sprite = instance.GetConceptCardFrame(rarity);
    }

    public void SetOwnerIcon(Image OwnerIcon, UnitData ownerUnit)
    {
      if ((UnityEngine.Object) OwnerIcon == (UnityEngine.Object) null)
        return;
      SpriteSheet spriteSheet = AssetManager.Load<SpriteSheet>("ItemIcon/small");
      ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(ownerUnit.UnitParam.piece);
      OwnerIcon.sprite = spriteSheet.GetSprite(itemParam.icon);
    }

    public void SetSameCardIcon()
    {
      if ((UnityEngine.Object) this.mSameCardIcon == (UnityEngine.Object) null)
        return;
      this.mSameCardIcon.SetActive(false);
      if ((UnityEngine.Object) ConceptCardManager.Instance == (UnityEngine.Object) null || ConceptCardManager.Instance.SelectedConceptCardData == null || !ConceptCardManager.Instance.IsEnhanceListActive)
        return;
      this.mSameCardIcon.SetActive(this.mConceptCard.Param.iname == ConceptCardManager.Instance.SelectedConceptCardData.Param.iname);
    }

    public void SetCardNum(int num)
    {
      if ((UnityEngine.Object) this.mCardNum == (UnityEngine.Object) null)
        return;
      this.mCardNum.text = num.ToString();
    }

    public void SetNotSellFlag(bool flag)
    {
      if ((UnityEngine.Object) this.mNotSale == (UnityEngine.Object) null)
        return;
      this.mNotSale.SetActive(flag);
    }
  }
}
