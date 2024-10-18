// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRankMatchReward
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;

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
