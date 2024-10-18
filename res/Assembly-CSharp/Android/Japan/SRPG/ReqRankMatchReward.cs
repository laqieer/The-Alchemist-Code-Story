// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRankMatchReward
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
