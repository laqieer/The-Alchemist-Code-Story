// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRankMatchRanking
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class ReqRankMatchRanking : WebAPI
  {
    public ReqRankMatchRanking(Network.ResponseCallback response)
    {
      this.name = "vs/rankmatch/ranking";
      this.body = WebAPI.GetRequestString(string.Empty);
      this.callback = response;
    }

    [Serializable]
    public class ResponceRanking
    {
      public int type;
      public int score;
      public int rank;
      public Json_Friend enemy;
    }

    [Serializable]
    public class Response
    {
      public ReqRankMatchRanking.ResponceRanking[] rankings;
    }
  }
}
