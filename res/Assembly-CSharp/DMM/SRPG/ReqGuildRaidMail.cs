// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGuildRaidMail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  public class ReqGuildRaidMail : WebAPI
  {
    public ReqGuildRaidMail(
      int page,
      int gid,
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "guildraid/mail";
      this.body = WebAPI.GetRequestString<ReqGuildRaidMail.RequestParam>(new ReqGuildRaidMail.RequestParam()
      {
        page = page,
        gid = gid
      });
      this.callback = response;
      this.serializeCompressMethod = serializeCompressMethod;
    }

    [Serializable]
    public class RequestParam
    {
      public int page;
      public int gid;
    }

    [MessagePackObject(true)]
    [Serializable]
    public class Response
    {
      public JSON_GuildRaidMail mails;
    }
  }
}
