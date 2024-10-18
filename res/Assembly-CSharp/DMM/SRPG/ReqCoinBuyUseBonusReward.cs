// Decompiled with JetBrains decompiler
// Type: SRPG.ReqCoinBuyUseBonusReward
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class ReqCoinBuyUseBonusReward : WebAPI
  {
    public ReqCoinBuyUseBonusReward(
      string reward_iname,
      int count,
      Network.ResponseCallback response)
    {
      this.name = "coin_bonuse/reward";
      this.body = WebAPI.GetRequestString<ReqCoinBuyUseBonusReward.RequestParam>(new ReqCoinBuyUseBonusReward.RequestParam()
      {
        iname = reward_iname,
        num = count
      });
      this.callback = response;
    }

    [Serializable]
    public class RequestParam
    {
      public string iname;
      public int num;
    }

    [Serializable]
    public class Response
    {
      public JSON_PlayerCoinBuyUseBonusRewardState[] bonus_rewards;
    }
  }
}
