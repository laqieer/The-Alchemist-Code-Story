﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRaidRankingGuild
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class ReqRaidRankingGuild : WebAPI
  {
    public ReqRaidRankingGuild(Network.ResponseCallback response)
    {
      this.name = "raidboss/ranking/guild";
      this.body = WebAPI.GetRequestString(string.Empty);
      this.callback = response;
    }

    [Serializable]
    public class Response
    {
      public Json_RaidRankingGuildList beat;
      public Json_RaidRankingGuildList rescue;
    }
  }
}
