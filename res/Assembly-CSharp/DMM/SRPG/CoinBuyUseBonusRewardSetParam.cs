// Decompiled with JetBrains decompiler
// Type: SRPG.CoinBuyUseBonusRewardSetParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class CoinBuyUseBonusRewardSetParam
  {
    private string iname;
    private CoinBuyUseBonusContentParam[] contents;

    public string Iname => this.iname;

    public CoinBuyUseBonusContentParam[] Contents => this.contents;

    public void Deserialize(
      JSON_CoinBuyUseBonusRewardSetParam json,
      CoinBuyUseBonusRewardParam[] reward_params)
    {
      this.iname = json.iname;
      if (json.contents == null)
        return;
      this.contents = new CoinBuyUseBonusContentParam[json.contents.Length];
      for (int index = 0; index < json.contents.Length; ++index)
      {
        CoinBuyUseBonusContentParam bonusContentParam = new CoinBuyUseBonusContentParam();
        bonusContentParam.Deserialize(json.contents[index], reward_params);
        this.contents[index] = bonusContentParam;
      }
    }

    public static void Deserialize(
      ref CoinBuyUseBonusRewardSetParam[] param,
      JSON_CoinBuyUseBonusRewardSetParam[] json,
      CoinBuyUseBonusRewardParam[] reward_params)
    {
      if (json == null)
        return;
      param = new CoinBuyUseBonusRewardSetParam[json.Length];
      for (int index = 0; index < json.Length; ++index)
      {
        CoinBuyUseBonusRewardSetParam bonusRewardSetParam = new CoinBuyUseBonusRewardSetParam();
        bonusRewardSetParam.Deserialize(json[index], reward_params);
        param[index] = bonusRewardSetParam;
      }
    }
  }
}
