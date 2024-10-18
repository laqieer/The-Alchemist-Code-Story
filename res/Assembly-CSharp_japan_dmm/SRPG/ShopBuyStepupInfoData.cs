// Decompiled with JetBrains decompiler
// Type: SRPG.ShopBuyStepupInfoData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;

#nullable disable
namespace SRPG
{
  public class ShopBuyStepupInfoData
  {
    private static ShopBuyStepupInfoData mInstance = new ShopBuyStepupInfoData();
    private bool mIsSet;
    private string mItemIname;
    private string mItemType;
    private int mSoldCount;
    private int mPriceBefore;
    private int mPriceAfter;
    private string mCurrency;
    private string mCurrencyUnit;

    public static bool IsSet => ShopBuyStepupInfoData.mInstance.mIsSet;

    public static string ItemName
    {
      get
      {
        switch (ShopBuyStepupInfoData.mInstance.mItemType)
        {
          case "item":
            ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(ShopBuyStepupInfoData.mInstance.mItemIname);
            if (itemParam != null)
              return itemParam.name;
            break;
          case "artifact":
            ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(ShopBuyStepupInfoData.mInstance.mItemIname);
            if (artifactParam != null)
              return artifactParam.name;
            break;
          case "concept_card":
            ConceptCardParam conceptCardParam = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(ShopBuyStepupInfoData.mInstance.mItemIname);
            if (conceptCardParam != null)
              return conceptCardParam.name;
            break;
        }
        return string.Empty;
      }
    }

    public static string ItemIname => ShopBuyStepupInfoData.mInstance.mItemIname;

    public static int SoldCount => ShopBuyStepupInfoData.mInstance.mSoldCount;

    public static int PriceBefore => ShopBuyStepupInfoData.mInstance.mPriceBefore;

    public static int PriceAfter => ShopBuyStepupInfoData.mInstance.mPriceAfter;

    public static string Currency => ShopBuyStepupInfoData.mInstance.mCurrency;

    public static string CurrencyUnit => ShopBuyStepupInfoData.mInstance.mCurrencyUnit;

    public static void Set(
      string itemIname,
      string itemType,
      int soldCount,
      int priceBefore,
      int priceAfter,
      ESaleType saleType)
    {
      ShopBuyStepupInfoData.mInstance.mIsSet = true;
      ShopBuyStepupInfoData.mInstance.mItemIname = itemIname;
      ShopBuyStepupInfoData.mInstance.mItemType = itemType;
      ShopBuyStepupInfoData.mInstance.mSoldCount = soldCount;
      ShopBuyStepupInfoData.mInstance.mPriceBefore = priceBefore;
      ShopBuyStepupInfoData.mInstance.mPriceAfter = priceAfter;
      switch (saleType)
      {
        case ESaleType.Gold:
          ShopBuyStepupInfoData.mInstance.mCurrency = string.Empty;
          ShopBuyStepupInfoData.mInstance.mCurrencyUnit = LocalizedText.Get("sys.GOLD");
          break;
        case ESaleType.Coin:
        case ESaleType.Coin_P:
          ShopBuyStepupInfoData.mInstance.mCurrency = LocalizedText.Get("sys.COIN");
          ShopBuyStepupInfoData.mInstance.mCurrencyUnit = LocalizedText.Get("sys.ITEM_TANI_1");
          break;
        case ESaleType.TourCoin:
          ShopBuyStepupInfoData.mInstance.mCurrency = LocalizedText.Get("sys.TOUR_COIN");
          ShopBuyStepupInfoData.mInstance.mCurrencyUnit = LocalizedText.Get("sys.ITEM_TANI_2");
          break;
        case ESaleType.ArenaCoin:
          ShopBuyStepupInfoData.mInstance.mCurrency = LocalizedText.Get("sys.ARENA_COIN");
          ShopBuyStepupInfoData.mInstance.mCurrencyUnit = LocalizedText.Get("sys.ITEM_TANI_2");
          break;
        case ESaleType.PiecePoint:
          ShopBuyStepupInfoData.mInstance.mCurrency = LocalizedText.Get("sys.PIECE_POINT");
          ShopBuyStepupInfoData.mInstance.mCurrencyUnit = LocalizedText.Get("sys.ITEM_TANI_3");
          break;
        case ESaleType.MultiCoin:
          ShopBuyStepupInfoData.mInstance.mCurrency = LocalizedText.Get("sys.MULTI_COIN");
          ShopBuyStepupInfoData.mInstance.mCurrencyUnit = LocalizedText.Get("sys.ITEM_TANI_2");
          break;
        case ESaleType.EventCoin:
          ShopBuyStepupInfoData.mInstance.mCurrency = LocalizedText.Get("sys.EVENT_COIN");
          ShopBuyStepupInfoData.mInstance.mCurrencyUnit = LocalizedText.Get("sys.ITEM_TANI_2");
          break;
        default:
          ShopBuyStepupInfoData.mInstance.mCurrency = string.Empty;
          ShopBuyStepupInfoData.mInstance.mCurrencyUnit = LocalizedText.Get("sys.ITEM_TANI_1");
          break;
      }
    }

    public static void Reset() => ShopBuyStepupInfoData.mInstance.mIsSet = false;
  }
}
