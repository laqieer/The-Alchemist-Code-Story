// Decompiled with JetBrains decompiler
// Type: SRPG.MailData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class MailData
  {
    public long mid;
    public string msg;
    public GiftData[] gifts;
    public long read;
    public long post_at;
    public int period;

    public bool Deserialize(Json_Mail json)
    {
      this.mid = json.mid;
      this.msg = json.msg;
      this.gifts = new GiftData[json.gifts.Length];
      for (int index = 0; index < json.gifts.Length; ++index)
      {
        GiftData giftData = new GiftData();
        giftData.iname = json.gifts[index].iname;
        giftData.num = json.gifts[index].num;
        giftData.gold = json.gifts[index].gold;
        giftData.coin = json.gifts[index].coin;
        giftData.arenacoin = json.gifts[index].arenacoin;
        giftData.multicoin = json.gifts[index].multicoin;
        giftData.kakeracoin = json.gifts[index].kakeracoin;
        if (json.gifts[index].concept_card != null)
        {
          giftData.conceptCard = new GiftData.GiftConceptCard();
          giftData.conceptCard.iname = json.gifts[index].concept_card.iname;
          giftData.conceptCard.num = json.gifts[index].concept_card.num;
          giftData.conceptCard.get_unit = json.gifts[index].concept_card.get_unit;
          giftData.conceptCard.plus = json.gifts[index].concept_card.plus;
        }
        giftData.rarity = json.gifts[index].rare;
        giftData.UpdateGiftTypes();
        this.gifts[index] = giftData;
      }
      this.read = (long) json.read;
      this.post_at = json.post_at;
      this.period = json.period;
      return true;
    }

    public bool IsPeriod => this.period == 1;

    public bool IsReadMail() => this.read != 0L;

    public bool Contains(GiftTypes giftType)
    {
      if (this.gifts == null)
        return false;
      for (int index = 0; index < this.gifts.Length; ++index)
      {
        if (this.gifts[index].CheckGiftTypeIncluded(giftType))
          return true;
      }
      return false;
    }

    public GiftData Find(GiftTypes giftType)
    {
      if (this.gifts == null)
        return (GiftData) null;
      for (int index = 0; index < this.gifts.Length; ++index)
      {
        if (this.gifts[index].CheckGiftTypeIncluded(giftType))
          return this.gifts[index];
      }
      return (GiftData) null;
    }
  }
}
