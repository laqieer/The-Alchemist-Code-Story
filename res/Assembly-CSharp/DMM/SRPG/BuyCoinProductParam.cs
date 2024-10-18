// Decompiled with JetBrains decompiler
// Type: SRPG.BuyCoinProductParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;

#nullable disable
namespace SRPG
{
  public class BuyCoinProductParam
  {
    private const string EXPR_SUFFIX = "_EXPR";
    private string mIname;
    private string mProductId;
    private string mShopId;
    private BuyCoinManager.PremiumRestrictionType mType;
    private int mVal;
    private bool mIsPlatformCommon;
    private string mReward;
    private string mTitle;
    private string mDescription;
    private int mBadge;
    private int mImgArrayIndex;
    private int mUnlockPlayerLevel;
    private string mTemplateName;

    public string Iname => this.mIname;

    public string ProductId => this.mProductId;

    public string ShopId => this.mShopId;

    public BuyCoinManager.PremiumRestrictionType Type => this.mType;

    public int Val => this.mVal;

    public bool FlagPlatformCommon => this.mIsPlatformCommon;

    public string Reward => this.mReward;

    public string Title => this.mTitle;

    public string Description => this.mDescription;

    public int Badge => this.mBadge;

    public int ImageArrayIndex => this.mImgArrayIndex;

    public int UnlockPlayerLevel => this.mUnlockPlayerLevel;

    public string TemplateName => this.mTemplateName;

    public string Expr => this.GetText("external_BuyCoinProduct", this.mIname + "_EXPR");

    public bool Deserialize(JSON_BuyCoinProductParam json)
    {
      if (json == null)
        return false;
      this.mIname = json.iname;
      this.mProductId = json.product_id;
      this.mShopId = json.shop_id;
      this.mType = (BuyCoinManager.PremiumRestrictionType) json.type;
      this.mVal = json.val;
      this.mIsPlatformCommon = json.is_platform_common != 0;
      this.mReward = json.reward;
      this.mTitle = json.title;
      if (!string.IsNullOrEmpty(this.mTitle))
        this.mTitle = this.mTitle.Replace("<br>", "\n");
      this.mDescription = json.description;
      if (!string.IsNullOrEmpty(this.mDescription))
        this.mDescription = this.mDescription.Replace("<br>", "\n");
      this.mBadge = json.badge;
      this.mImgArrayIndex = json.img_array_idx;
      this.mUnlockPlayerLevel = json.unlock_lv;
      this.mTemplateName = json.temp_name;
      return true;
    }

    public BuyCoinShopParam GetBuyCoinShopParam()
    {
      return MonoSingleton<GameManager>.Instance.MasterParam.GetBuyCoinShopParam()?.Find((Predicate<BuyCoinShopParam>) (x => x.ShopId == this.mShopId));
    }

    public BuyCoinManager.BuyCoinShopType GetProductShopType()
    {
      BuyCoinShopParam buyCoinShopParam = this.GetBuyCoinShopParam();
      return buyCoinShopParam == null ? BuyCoinManager.BuyCoinShopType.ALL : buyCoinShopParam.ShopType;
    }

    public bool IsShopOpen()
    {
      BuyCoinShopParam buyCoinShopParam = this.GetBuyCoinShopParam();
      return buyCoinShopParam != null && MonoSingleton<GameManager>.Instance.MasterParam.IsBuyCoinShopOpen(buyCoinShopParam);
    }

    public bool IsExpansionShop()
    {
      return this.GetProductShopType() == BuyCoinManager.BuyCoinShopType.EXPANSION;
    }

    public string GetText(string table, string key)
    {
      string str = LocalizedText.Get(table + "." + key);
      return str.Equals(key) ? string.Empty : str;
    }
  }
}
