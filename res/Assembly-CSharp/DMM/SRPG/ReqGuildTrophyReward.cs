// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGuildTrophyReward
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  public class ReqGuildTrophyReward : WebAPI
  {
    public ReqGuildTrophyReward(
      ReqGuildTrophyReward.RequestParam rp,
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "guild/trophy/reward";
      this.body = WebAPI.GetRequestString<ReqGuildTrophyReward.RequestParam>(rp);
      this.callback = response;
      this.serializeCompressMethod = serializeCompressMethod;
    }

    [Serializable]
    public class RequestTrophy
    {
      public string iname;
      public int ymd;

      public RequestTrophy(string _iname, int _ymd)
      {
        this.iname = _iname;
        this.ymd = _ymd;
      }
    }

    [Serializable]
    public class RequestParam
    {
      public ReqGuildTrophyReward.RequestTrophy[] guild_trophies;

      public RequestParam()
      {
      }

      public RequestParam(
        ReqGuildTrophyReward.RequestTrophy[] request_trophies)
      {
        this.guild_trophies = request_trophies;
      }
    }

    [MessagePackObject(true)]
    [Serializable]
    public class Response
    {
      public JSON_TrophyProgress[] guild_trophies;
      public Json_TrophyPlayerData player;
      public Json_Unit[] units;
      public Json_Item[] items;
      public Json_TrophyConceptCards cards;
      public Json_Artifact[] artifacts;
    }
  }
}
