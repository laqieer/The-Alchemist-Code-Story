// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRankMatchRanking
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;

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
