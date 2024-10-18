// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
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
    [SerializeField]
    private GameObject mRecommend;
    [SerializeField]
    private GameObject mDisableObject;
    [SerializeField]
    private GameObject mSelectObject;
    [SerializeField]
    private bool mIsIncludeOverWrite;
    private ConceptCardData mConceptCard;

    public ConceptCardData ConceptCard => this.mConceptCard;

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
      DataSource.Bind<ConceptCardData>(((Component) this).gameObject, this.mConceptCard);
      GameParameter.UpdateAll(((Component) this).gameObject);
      this.RefreshExistImage();
      this.RefreshIconImage();
      this.RefreshIconParam();
    }

    public UnitData GetOwner(bool is_include_over_write)
    {
      return this.mConceptCard == null ? (UnitData) null : this.mConceptCard.GetOwner(is_include_over_write);
    }

    private void RefreshExistImage()
    {
      bool flag = this.mConceptCard != null;
      if (Object.op_Inequality((Object) this.mExistSwitchOn, (Object) null))
        this.mExistSwitchOn.SetActive(flag);
      if (!Object.op_Inequality((Object) this.mExistSwitchOff, (Object) null))
        return;
      this.mExistSwitchOff.SetActive(!flag);
    }

    private void RefreshIconImage()
    {
      if (this.mConceptCard == null || Object.op_Equality((Object) this.mIconImage, (Object) null))
        MonoSingleton<GameManager>.Instance.CancelTextureLoadRequest(this.mIconImage);
      else
        MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.mIconImage, AssetPath.ConceptCardIcon(this.mConceptCard.Param));
    }

    private void RefreshIconParam()
    {
      if (Object.op_Inequality((Object) this.mRarityFrame, (Object) null))
        ((Component) this.mRarityFrame).gameObject.SetActive(false);
      if (this.mConceptCard == null)
      {
        if (Object.op_Inequality((Object) this.mLevelTitleText, (Object) null))
          ((Component) this.mLevelTitleText).gameObject.SetActive(false);
        if (Object.op_Inequality((Object) this.mLevelText, (Object) null))
          ((Component) this.mLevelText).gameObject.SetActive(false);
        if (!Object.op_Inequality((Object) this.mLevelCapText, (Object) null))
          return;
        ((Component) this.mLevelCapText).gameObject.SetActive(false);
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
        if (Object.op_Inequality((Object) this.mOwner, (Object) null))
        {
          if (this.GetOwner(this.mIsIncludeOverWrite) != null)
          {
            this.mOwner.SetActive(true);
            this.SetOwnerIcon(this.mOwnerIcon);
          }
          else
            this.mOwner.SetActive(false);
        }
        this.SetSameCardIcon();
      }
    }

    public void SetNameText(string name)
    {
      if (Object.op_Equality((Object) this.mNameText, (Object) null))
        return;
      this.mNameText.text = name.ToString();
    }

    public void SetLevelText(int lv)
    {
      if (Object.op_Equality((Object) this.mLevelText, (Object) null))
        return;
      this.mLevelText.text = lv.ToString();
      ((Component) this.mLevelText).gameObject.SetActive(true);
      if (!Object.op_Inequality((Object) this.mLevelTitleText, (Object) null))
        return;
      ((Component) this.mLevelTitleText).gameObject.SetActive(true);
    }

    public void SetLevelCapText(int lvcap)
    {
      if (Object.op_Equality((Object) this.mLevelCapText, (Object) null))
        return;
      this.mLevelCapText.text = lvcap.ToString();
      ((Component) this.mLevelCapText).gameObject.SetActive(true);
      if (!Object.op_Inequality((Object) this.mLevelTitleText, (Object) null))
        return;
      ((Component) this.mLevelTitleText).gameObject.SetActive(true);
    }

    public void SetTrustText(int trust)
    {
      if (Object.op_Equality((Object) this.mTrustText, (Object) null))
        return;
      ConceptCardManager.SubstituteTrustFormat(this.mConceptCard, this.mTrustText, trust);
    }

    public void SetNoRewardTrustText()
    {
      if (Object.op_Equality((Object) this.mTrustText, (Object) null))
        return;
      this.mTrustText.text = "---";
    }

    public void SetRarityImaget(int rare)
    {
      if (Object.op_Equality((Object) this.mRarityImage, (Object) null))
        return;
      this.mRarityImage.sprite = (Sprite) null;
      GameSettings instance = GameSettings.Instance;
      if (!Object.op_Inequality((Object) instance, (Object) null) || instance.ConceptCardIcon_Rarity.Length <= 0)
        return;
      this.mRarityImage.sprite = instance.ConceptCardIcon_Rarity[rare];
    }

    public void SetFavorite(bool favorite)
    {
      if (Object.op_Equality((Object) this.mFavorite, (Object) null))
        return;
      this.mFavorite.SetActive(favorite);
    }

    public void SetRarityFrame(int rarity)
    {
      if (Object.op_Equality((Object) this.mRarityFrame, (Object) null))
        return;
      ((Component) this.mRarityFrame).gameObject.SetActive(true);
      GameSettings instance = GameSettings.Instance;
      if (Object.op_Equality((Object) instance, (Object) null))
        return;
      this.mRarityFrame.sprite = instance.GetConceptCardFrame(rarity);
    }

    public void SetOwnerIcon(Image OwnerIcon)
    {
      if (!Object.op_Inequality((Object) OwnerIcon, (Object) null))
        return;
      UnitData owner = this.GetOwner(false);
      if (owner == null)
        return;
      SpriteSheet spriteSheet = AssetManager.Load<SpriteSheet>("ItemIcon/small");
      ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(owner.UnitParam.piece);
      OwnerIcon.sprite = spriteSheet.GetSprite(itemParam.icon);
    }

    public void SetSameCardIcon()
    {
      if (Object.op_Equality((Object) this.mSameCardIcon, (Object) null))
        return;
      this.mSameCardIcon.SetActive(false);
      if (Object.op_Equality((Object) ConceptCardManager.Instance, (Object) null) || ConceptCardManager.Instance.SelectedConceptCardData == null || !ConceptCardManager.Instance.IsEnhanceListActive)
        return;
      this.mSameCardIcon.SetActive(this.mConceptCard.Param.iname == ConceptCardManager.Instance.SelectedConceptCardData.Param.iname);
    }

    public void SetCardNum(int num)
    {
      if (Object.op_Equality((Object) this.mCardNum, (Object) null))
        return;
      this.mCardNum.text = num.ToString();
    }

    public void SetNotSellFlag(bool flag)
    {
      if (Object.op_Equality((Object) this.mNotSale, (Object) null))
        return;
      this.mNotSale.SetActive(flag);
    }

    public void SetRecommendFlag(bool flag)
    {
      if (Object.op_Equality((Object) this.mRecommend, (Object) null))
        return;
      this.mRecommend.SetActive(flag);
    }

    public void RefreshEnableParam(bool enable)
    {
      if (this.ConceptCard == null)
        return;
      if (Object.op_Inequality((Object) this.mDisableObject, (Object) null))
        this.mDisableObject.SetActive(!enable);
      Button component = ((Component) ((Component) this).transform).gameObject.GetComponent<Button>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      ((Selectable) component).interactable = enable;
    }

    public void RefreshSelectParam(bool selected)
    {
      if (this.ConceptCard == null || Object.op_Equality((Object) this.mSelectObject, (Object) null) || !Object.op_Inequality((Object) this.mSelectObject, (Object) null))
        return;
      this.mSelectObject.SetActive(selected);
    }
  }
}
