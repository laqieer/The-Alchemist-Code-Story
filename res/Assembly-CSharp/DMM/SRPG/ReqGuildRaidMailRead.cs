// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGuildRaidMailRead
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  public class ReqGuildRaidMailRead : WebAPI
  {
    public ReqGuildRaidMailRead(
      int[] mailids,
      int page,
      int gid,
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "guildraid/mail/read";
      this.body = WebAPI.GetRequestString<ReqGuildRaidMailRead.RequestParam>(new ReqGuildRaidMailRead.RequestParam()
      {
        mailids = mailids,
        page = page,
        gid = gid
      });
      this.callback = response;
      this.serializeCompressMethod = serializeCompressMethod;
    }

    [Serializable]
    public class RequestParam
    {
      public int[] mailids;
      public int page;
      public int gid;
    }

    [MessagePackObject(true)]
    [Serializable]
    public class Response
    {
      public JSON_GuildRaidMail mails;
      public JSON_GuildRaidMailListItem[] processed;
      public JSON_TrophyProgress[] trophyprogs;
      public JSON_TrophyProgress[] bingoprogs;
      public Json_PlayerData player;
      public Json_Item[] items;
      public Json_Unit[] units;
      public Json_Artifact[] artifacts;
      public JSON_ConceptCard[] cards;
      public int[] gift_mailids;
    }
  }
}
