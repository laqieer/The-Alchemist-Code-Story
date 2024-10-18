// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRankMatchRanking
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
