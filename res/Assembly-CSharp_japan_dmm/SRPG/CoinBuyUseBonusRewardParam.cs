// Decompiled with JetBrains decompiler
// Type: SRPG.CoinBuyUseBonusRewardParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class CoinBuyUseBonusRewardParam
  {
    private string iname;
    private CoinBuyUseBonusItemParam[] rewards;

    public string Iname => this.iname;

    public CoinBuyUseBonusItemParam[] Rewards => this.rewards;

    public void Deserialize(JSON_CoinBuyUseBonusRewardParam json)
    {
      this.iname = json.iname;
      if (json.rewards == null)
        return;
      this.rewards = new CoinBuyUseBonusItemParam[json.rewards.Length];
      for (int index = 0; index < json.rewards.Length; ++index)
      {
        CoinBuyUseBonusItemParam useBonusItemParam = new CoinBuyUseBonusItemParam();
        useBonusItemParam.Deserialize(json.rewards[index]);
        this.rewards[index] = useBonusItemParam;
      }
    }

    public static void Deserialize(
      ref CoinBuyUseBonusRewardParam[] param,
      JSON_CoinBuyUseBonusRewardParam[] json)
    {
      if (json == null)
        return;
      param = new CoinBuyUseBonusRewardParam[json.Length];
      for (int index = 0; index < json.Length; ++index)
      {
        CoinBuyUseBonusRewardParam bonusRewardParam = new CoinBuyUseBonusRewardParam();
        bonusRewardParam.Deserialize(json[index]);
        param[index] = bonusRewardParam;
      }
    }
  }
}
