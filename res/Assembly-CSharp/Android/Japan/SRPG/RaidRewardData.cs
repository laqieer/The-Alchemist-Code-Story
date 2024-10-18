// Decompiled with JetBrains decompiler
// Type: SRPG.RaidRewardData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class RaidRewardData
  {
    private RaidRewardKind mKind;
    private GiftData[] mRewards;

    public RaidRewardKind Kind
    {
      get
      {
        return this.mKind;
      }
    }

    public GiftData[] Rewards
    {
      get
      {
        return this.mRewards;
      }
    }

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
