﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ReqBtlColoRanking
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class ReqBtlColoRanking : WebAPI
  {
    public ReqBtlColoRanking(ReqBtlColoRanking.RankingTypes type, Network.ResponseCallback response)
    {
      this.name = "btl/colo/ranking/" + type.ToString();
      this.body = WebAPI.GetRequestString(WebAPI.GetStringBuilder().ToString());
      this.callback = response;
    }

    public enum RankingTypes
    {
      world,
      friend,
    }
  }
}
