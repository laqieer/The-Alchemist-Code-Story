// Decompiled with JetBrains decompiler
// Type: SRPG.ReqWorldRaidRanking
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  public class ReqWorldRaidRanking : WebAPI
  {
    public ReqWorldRaidRanking(
      string bossIname,
      int page,
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod = EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK)
    {
      this.name = "worldraid/ranking";
      this.body = WebAPI.GetRequestString<ReqWorldRaidRanking.RequestParam>(new ReqWorldRaidRanking.RequestParam()
      {
        boss_iname = bossIname,
        page = page
      });
      this.callback = response;
      this.serializeCompressMethod = serializeCompressMethod;
    }

    [Serializable]
    public class RequestParam
    {
      public string boss_iname;
      public int page;
    }

    [MessagePackObject(true)]
    [Serializable]
    public class Response
    {
      public JSON_WorldRaidRankingData[] worldraid_ranking;
      public JSON_WorldRaidRankingData my_info;
      public int total_page;
    }
  }
}
