// Decompiled with JetBrains decompiler
// Type: SRPG.CoinBuyUseBonusParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class CoinBuyUseBonusParam
  {
    private string iname;
    private eCoinBuyUseBonusType type;
    private eCoinBuyUseBonusTrigger trigger;
    private string reward_set;
    private DateTime begin_at;
    private DateTime end_at;
    private CoinBuyUseBonusRewardSetParam mRewardSet;

    public string Iname => this.iname;

    public eCoinBuyUseBonusType Type => this.type;

    public eCoinBuyUseBonusTrigger Trigger => this.trigger;

    public DateTime BeginAt => this.begin_at;

    public DateTime EndAt => this.end_at;

    public CoinBuyUseBonusRewardSetParam RewardSet => this.mRewardSet;

    public bool IsEnable
    {
      get => this.begin_at <= TimeManager.ServerTime && TimeManager.ServerTime <= this.end_at;
    }

    public void Deserialize(
      JSON_CoinBuyUseBonusParam json,
      CoinBuyUseBonusRewardSetParam[] reward_set_params)
    {
      this.iname = json.iname;
      this.type = (eCoinBuyUseBonusType) json.type;
      this.trigger = (eCoinBuyUseBonusTrigger) json.trigger;
      this.reward_set = json.reward_set;
      this.begin_at = DateTime.Parse(json.begin_at);
      this.end_at = DateTime.Parse(json.end_at);
      this.mRewardSet = Array.Find<CoinBuyUseBonusRewardSetParam>(reward_set_params, (Predicate<CoinBuyUseBonusRewardSetParam>) (param => param.Iname == this.reward_set));
    }

    public static void Deserialize(
      ref CoinBuyUseBonusParam[] param,
      JSON_CoinBuyUseBonusParam[] json,
      CoinBuyUseBonusRewardSetParam[] reward_set_params)
    {
      if (json == null)
        return;
      param = new CoinBuyUseBonusParam[json.Length];
      for (int index = 0; index < json.Length; ++index)
      {
        CoinBuyUseBonusParam buyUseBonusParam = new CoinBuyUseBonusParam();
        buyUseBonusParam.Deserialize(json[index], reward_set_params);
        param[index] = buyUseBonusParam;
      }
    }
  }
}
