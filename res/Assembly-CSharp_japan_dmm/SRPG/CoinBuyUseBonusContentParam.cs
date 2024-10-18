// Decompiled with JetBrains decompiler
// Type: SRPG.CoinBuyUseBonusContentParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class CoinBuyUseBonusContentParam
  {
    private string name;
    private int num;
    private string reward_iname;
    private string gift_msg;
    private CoinBuyUseBonusRewardParam mRewardParam;

    public string Name => this.name;

    public int Num => this.num;

    public string RewardIname => this.reward_iname;

    public string GiftMsg => this.gift_msg;

    public CoinBuyUseBonusRewardParam RewardParam => this.mRewardParam;

    public void Deserialize(
      JSON_CoinBuyUseBonusContentParam json,
      CoinBuyUseBonusRewardParam[] reward_params)
    {
      this.name = json.name;
      this.num = json.num;
      this.reward_iname = json.reward_iname;
      this.gift_msg = json.gift_msg;
      this.mRewardParam = Array.Find<CoinBuyUseBonusRewardParam>(reward_params, (Predicate<CoinBuyUseBonusRewardParam>) (param => param.Iname == this.reward_iname));
    }
  }
}
