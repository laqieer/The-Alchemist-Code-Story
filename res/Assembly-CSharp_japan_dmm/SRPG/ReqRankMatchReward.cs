// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRankMatchReward
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class ReqRankMatchReward : WebAPI
  {
    public ReqRankMatchReward(Network.ResponseCallback response)
    {
      this.name = "vs/rankmatch/reward";
      this.body = WebAPI.GetRequestString(string.Empty);
      this.callback = response;
    }

    [Serializable]
    public class RwardResponse
    {
      public string ranking;
      public string type;
    }

    [Serializable]
    public class Response
    {
      public int schedule_id;
      public int score;
      public int rank;
      public int type;
      public ReqRankMatchReward.RwardResponse reward;
    }
  }
}
