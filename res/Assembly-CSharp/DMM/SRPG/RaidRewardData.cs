// Decompiled with JetBrains decompiler
// Type: SRPG.RaidRewardData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class RaidRewardData
  {
    private RaidRewardKind mKind;
    private GiftData[] mRewards;

    public RaidRewardKind Kind => this.mKind;

    public GiftData[] Rewards => this.mRewards;

    public bool Deserialize(RaidRewardKind kind, Json_Gift[] json_gifts)
    {
      this.mKind = kind;
      this.mRewards = new GiftData[json_gifts.Length];
      for (int index = 0; index < json_gifts.Length; ++index)
      {
        GiftData giftData = new GiftData();
        giftData.iname = json_gifts[index].iname;
        giftData.num = json_gifts[index].num;
        giftData.gold = json_gifts[index].gold;
        giftData.coin = json_gifts[index].coin;
        giftData.arenacoin = json_gifts[index].arenacoin;
        giftData.multicoin = json_gifts[index].multicoin;
        giftData.kakeracoin = json_gifts[index].kakeracoin;
        if (json_gifts[index].concept_card != null)
        {
          giftData.conceptCard = new GiftData.GiftConceptCard();
          giftData.conceptCard.iname = json_gifts[index].concept_card.iname;
          giftData.conceptCard.num = json_gifts[index].concept_card.num;
          giftData.conceptCard.get_unit = json_gifts[index].concept_card.get_unit;
        }
        giftData.rarity = json_gifts[index].rare;
        giftData.UpdateGiftTypes();
        this.mRewards[index] = giftData;
      }
      return true;
    }
  }
}
