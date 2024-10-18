// Decompiled with JetBrains decompiler
// Type: SRPG.ReqCoinBuyUseBonusProgress
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class ReqCoinBuyUseBonusProgress : WebAPI
  {
    public ReqCoinBuyUseBonusProgress(Network.ResponseCallback response)
    {
      this.name = "coin_bonuse";
      this.body = WebAPI.GetRequestString(string.Empty);
      this.callback = response;
    }

    [Serializable]
    public class Response
    {
      public JSON_PlayerCoinBuyUseBonusState[] bonus_stats;
    }
  }
}
