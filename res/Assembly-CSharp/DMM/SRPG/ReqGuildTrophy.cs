// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGuildTrophy
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  public class ReqGuildTrophy : WebAPI
  {
    public ReqGuildTrophy(
      ReqGuildTrophy.RequestParam rp,
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "guild/trophy";
      this.body = WebAPI.GetRequestString<ReqGuildTrophy.RequestParam>(rp);
      this.callback = response;
      this.serializeCompressMethod = serializeCompressMethod;
    }

    public class RequestParam
    {
    }

    [MessagePackObject(true)]
    [Serializable]
    public class Response
    {
      public JSON_TrophyProgress[] guild_trophies;
    }
  }
}
