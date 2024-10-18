// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRaidRankingBeat
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class ReqRaidRankingBeat : WebAPI
  {
    public ReqRaidRankingBeat(Network.ResponseCallback response)
    {
      this.name = "raidboss/ranking/beat";
      this.body = WebAPI.GetRequestString(string.Empty);
      this.callback = response;
    }

    [Serializable]
    public class Response
    {
      public Json_RaidRankingList beat;
      public Json_RaidRankingList rescue;
    }
  }
}
