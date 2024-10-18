// Decompiled with JetBrains decompiler
// Type: SRPG.PurchaseListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class PurchaseListItem : MonoBehaviour
  {
    [SerializeField]
    private GameObject PurchaseBefore;
    [SerializeField]
    private GameObject Purchased;
    [SerializeField]
    private GameObject Purchasable;
    [SerializeField]
    private GameObject PurchasedExpansion;
    [SerializeField]
    private GameObject PurchaseIcon;
    [SerializeField]
    private RewardListItem PurchaseIconList;
    [SerializeField]
    private GameObject PurchaseBadge;
    [SerializeField]
    private ImageArray PurchaseBadgeImage;
    [SerializeField]
    private Text PurchaseBuyCountTitle;
    [SerializeField]
    private Text PurchaseBuyCountText;
    [SerializeField]
    private GameObject PurchaseBuyCountObject;
    [SerializeField]
    private GameObject PurchaseSoldOut;
    [SerializeField]
    private GameObject BonusItem;
    [SerializeField]
    private ImageArray CoinIcon;
    [SerializeField]
    private GameObject BuyCoinItemDetailWindow;
    [SerializeField]
    private int RepurchaseRemainDay;
    [SerializeField]
    private GameObject LockObject;
    private bool mIsVip;
    private bool mIsPremium;
    private bool mIsPurchased;
    private PaymentManager.Product mProduct;

    public bool IsVip => this.mIsVip;

    public bool IsPremium => this.mIsPremium;

    public bool IsPurchased => this.mIsPurchased;

    public int CoinNum => this.mProduct != null ? this.mProduct.numFree + this.mProduct.numPaid : 0;

    public int CoinNumPaid => this.mProduct != null ? this.mProduct.numPaid : 0;

    public PaymentManager.Product ProductID => this.mProduct;

    public void Init(PaymentManager.Product product)
    {
      this.mProduct = product;
      string productId = product.productID;
      if (productId == (string) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.VipCardProduct)
      {
        this.mIsVip = true;
        this.mIsPurchased = MonoSingleton<GameManager>.Instance.Player.CheckEnableVipCard();
        this.Purchasable.SetActive(false);
        this.PurchaseBefore.SetActive(false);
        this.Purchased.SetActive(false);
        if (this.mIsPurchased)
        {
          if ((MonoSingleton<GameManager>.Instance.Player.VipExpiredAt - TimeManager.FromUnixTime(Network.GetServerTime())).Days <= 3)
          {
            this.mIsPurchased = false;
            this.Purchasable.SetActive(true);
          }
          else
            this.Purchased.SetActive(true);
        }
        else
          this.PurchaseBefore.SetActive(true);
      }
      else if (productId == (string) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.PremiumProduct)
      {
        this.mIsPremium = true;
        this.mIsPurchased = MonoSingleton<GameManager>.Instance.Player.CheckEnablePremiumMember();
        this.PurchaseBefore.SetActive(!this.mIsPurchased);
        this.Purchased.SetActive(this.mIsPurchased);
      }
      else
      {
        GameManager instance = MonoSingleton<GameManager>.Instance;
        GameObject gameObject = (GameObject) null;
        BuyCoinProductParam coinProductParam1 = instance.MasterParam.GetBuyCoinProductParam(product.ID);
        if (coinProductParam1 != null)
        {
          DataSource.Bind<BuyCoinProductParam>(((Component) this).gameObject, coinProductParam1);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PurchaseIcon, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PurchaseIconList, (UnityEngine.Object) null))
          {
            BuyCoinRewardParam buyCoinRewardParam = instance.MasterParam.GetBuyCoinRewardParam(coinProductParam1.Reward);
            if (buyCoinRewardParam != null)
            {
              switch (buyCoinRewardParam.DrawType)
              {
                case BuyCoinManager.PremiumRewadType.Item:
                  ItemParam itemParam = instance.GetItemParam(buyCoinRewardParam.DrawIname);
                  if (itemParam != null)
                  {
                    gameObject = this.PurchaseIconList.RewardItem;
                    DataSource.Bind<ItemParam>(gameObject, itemParam);
                    break;
                  }
                  break;
                case BuyCoinManager.PremiumRewadType.Gold:
                  gameObject = this.PurchaseIconList.RewardGold;
                  break;
                case BuyCoinManager.PremiumRewadType.Coin:
                  gameObject = this.PurchaseIconList.RewardCoin;
                  break;
                case BuyCoinManager.PremiumRewadType.Artifact:
                  ArtifactParam artifactParam = instance.MasterParam.GetArtifactParam(buyCoinRewardParam.DrawIname);
                  if (artifactParam != null)
                  {
                    gameObject = this.PurchaseIconList.RewardArtifact;
                    DataSource.Bind<ArtifactParam>(gameObject, artifactParam);
                    break;
                  }
                  break;
                case BuyCoinManager.PremiumRewadType.ConceptCard:
                  ConceptCardData cardDataForDisplay = ConceptCardData.CreateConceptCardDataForDisplay(buyCoinRewardParam.DrawIname);
                  if (cardDataForDisplay != null)
                  {
                    gameObject = this.PurchaseIconList.RewardCard;
                    ConceptCardIcon component = gameObject.GetComponent<ConceptCardIcon>();
                    if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
                    {
                      component.Setup(cardDataForDisplay);
                      break;
                    }
                    break;
                  }
                  break;
                case BuyCoinManager.PremiumRewadType.Unit:
                  UnitParam unitParam = instance.GetUnitParam(buyCoinRewardParam.DrawIname);
                  if (unitParam != null)
                  {
                    gameObject = this.PurchaseIconList.RewardUnit;
                    DataSource.Bind<UnitParam>(gameObject, unitParam);
                    break;
                  }
                  break;
              }
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
              {
                gameObject.transform.SetParent(this.PurchaseIconList.RewardList, false);
                gameObject.SetActive(true);
              }
            }
          }
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PurchaseBadge, (UnityEngine.Object) null))
            this.PurchaseBadge.SetActive(false);
          if (coinProductParam1.Badge > 0 && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PurchaseBadge, (UnityEngine.Object) null))
          {
            this.PurchaseBadge.SetActive(true);
            this.SetBadgeId(coinProductParam1.Badge - 1);
          }
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PurchaseBuyCountObject, (UnityEngine.Object) null))
            this.PurchaseBuyCountObject.SetActive(false);
          if (product.remainNum == 0)
          {
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PurchaseSoldOut, (UnityEngine.Object) null))
              this.PurchaseSoldOut.SetActive(true);
          }
          else if (product.remainNum > 0)
          {
            switch (coinProductParam1.Type)
            {
              case BuyCoinManager.PremiumRestrictionType.AllBuy:
                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PurchaseBuyCountObject, (UnityEngine.Object) null))
                  this.PurchaseBuyCountObject.SetActive(true);
                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PurchaseBuyCountTitle, (UnityEngine.Object) null))
                  this.PurchaseBuyCountTitle.text = LocalizedText.Get("sys.BUYCOIN_LIMITED");
                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PurchaseBuyCountText, (UnityEngine.Object) null))
                  this.PurchaseBuyCountText.text = product.remainNum.ToString();
                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PurchaseBuyCountObject, (UnityEngine.Object) null))
                {
                  ImageArray component = this.PurchaseBuyCountObject.GetComponent<ImageArray>();
                  if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
                  {
                    component.ImageIndex = 0;
                    break;
                  }
                  break;
                }
                break;
              case BuyCoinManager.PremiumRestrictionType.DayBuy:
                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PurchaseBuyCountObject, (UnityEngine.Object) null))
                  this.PurchaseBuyCountObject.SetActive(true);
                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PurchaseBuyCountTitle, (UnityEngine.Object) null))
                  this.PurchaseBuyCountTitle.text = LocalizedText.Get("sys.BUYCOIN_DAY");
                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PurchaseBuyCountText, (UnityEngine.Object) null))
                  this.PurchaseBuyCountText.text = product.remainNum.ToString();
                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PurchaseBuyCountObject, (UnityEngine.Object) null))
                {
                  ImageArray component = this.PurchaseBuyCountObject.GetComponent<ImageArray>();
                  if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
                  {
                    component.ImageIndex = 1;
                    break;
                  }
                  break;
                }
                break;
            }
          }
          if (coinProductParam1.IsExpansionShop())
          {
            long expansionGroupExpiredAt = MonoSingleton<GameManager>.Instance.Player.GetExpansionGroupExpiredAt(coinProductParam1);
            this.mIsPurchased = expansionGroupExpiredAt > 0L;
            GameUtility.SetGameObjectActive(this.PurchaseBefore, false);
            GameUtility.SetGameObjectActive(this.Purchased, false);
            GameUtility.SetGameObjectActive(this.Purchasable, false);
            GameUtility.SetGameObjectActive(this.PurchasedExpansion, false);
            if (this.mIsPurchased)
            {
              if (!coinProductParam1.IsShopOpen())
              {
                Button component = ((Component) this).GetComponent<Button>();
                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
                  ((Selectable) component).interactable = false;
                GameUtility.SetGameObjectActive(this.PurchasedExpansion, true);
              }
              else if ((TimeManager.FromUnixTime(expansionGroupExpiredAt) - TimeManager.FromUnixTime(Network.GetServerTime())).Days <= this.RepurchaseRemainDay)
              {
                this.mIsPurchased = false;
                GameUtility.SetGameObjectActive(this.Purchasable, true);
              }
              else
              {
                Button component = ((Component) this).GetComponent<Button>();
                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
                  ((Selectable) component).interactable = false;
                GameUtility.SetGameObjectActive(this.Purchased, true);
              }
            }
            else
              GameUtility.SetGameObjectActive(this.PurchaseBefore, true);
          }
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BonusItem, (UnityEngine.Object) null))
          this.BonusItem.SetActive(product.numFree > 0);
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.LockObject, (UnityEngine.Object) null))
          return;
        BuyCoinProductParam coinProductParam2 = MonoSingleton<GameManager>.Instance.MasterParam.GetBuyCoinProductParam(product.ID);
        if (coinProductParam2 == null)
          return;
        ExpansionPurchaseParam paramForProductId = MonoSingleton<GameManager>.Instance.MasterParam.GetExpansionPurchaseParamForProductId(coinProductParam2.Iname);
        GameUtility.SetGameObjectActive(this.LockObject, paramForProductId == null || paramForProductId.ExpansionType != ExpansionPurchaseParam.eExpansionType.ExtraCount ? coinProductParam2.UnlockPlayerLevel > 0 && MonoSingleton<GameManager>.Instance.Player.Lv < coinProductParam2.UnlockPlayerLevel : !this.IsExtraQuestsAvailable());
      }
    }

    public void SetSpriteId(int index)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.CoinIcon, (UnityEngine.Object) null))
        return;
      if (index < 0 || index >= this.CoinIcon.Images.Length)
        ((Behaviour) this.CoinIcon).enabled = false;
      else
        this.CoinIcon.ImageIndex = index;
    }

    public void SetBadgeId(int index)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.PurchaseBadgeImage, (UnityEngine.Object) null))
        return;
      if (index < 0)
        index = 0;
      if (index >= this.PurchaseBadgeImage.Images.Length)
        index = this.PurchaseBadgeImage.Images.Length - 1;
      this.PurchaseBadgeImage.ImageIndex = index;
    }

    public void OnButtonHelpClick()
    {
      BuyCoinProductParam coinProductParam = MonoSingleton<GameManager>.Instance.MasterParam.GetBuyCoinProductParam(this.mProduct.ID);
      if (coinProductParam == null || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BuyCoinItemDetailWindow, (UnityEngine.Object) null) || !BuyCoinManager.Instance.GetProductBuyConfirm(this.mProduct))
        return;
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.BuyCoinItemDetailWindow);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
        return;
      DataSource.Bind<BuyCoinProductParam>(gameObject, coinProductParam);
      DataSource.Bind<PaymentManager.Product>(gameObject, this.mProduct);
      gameObject.SetActive(true);
    }

    private bool IsExtraQuestsAvailable()
    {
      bool flag = false;
      List<QuestParam> questParamList = new List<QuestParam>((IEnumerable<QuestParam>) MonoSingleton<GameManager>.Instance.Player.AvailableQuests);
      if (questParamList == null || questParamList.Count <= 0)
        return flag;
      List<QuestParam> all = questParamList.FindAll((Predicate<QuestParam>) (q => q.type == QuestTypes.StoryExtra));
      if (all == null || all.Count <= 0)
        return flag;
      for (int index = 0; index < all.Count; ++index)
      {
        if (all[index].IsOpenUnitHave())
        {
          flag = true;
          break;
        }
      }
      return flag;
    }
  }
}
