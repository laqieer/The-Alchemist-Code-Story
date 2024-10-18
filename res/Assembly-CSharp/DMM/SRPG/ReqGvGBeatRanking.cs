// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGvGBeatRanking
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  public class ReqGvGBeatRanking : WebAPI
  {
    public ReqGvGBeatRanking(
      int gid,
      int gvg_group_id,
      int page,
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "gvg/ranking/beat";
      this.body = WebAPI.GetRequestString<ReqGvGBeatRanking.RequestParam>(new ReqGvGBeatRanking.RequestParam()
      {
        gid = gid,
        gvg_group_id = gvg_group_id,
        page = page
      });
      this.callback = response;
      this.serializeCompressMethod = serializeCompressMethod;
    }

    [Serializable]
    public class RequestParam
    {
      public int gid;
      public int gvg_group_id;
      public int page;
    }

    [MessagePackObject(true)]
    [Serializable]
    public class Response
    {
      public JSON_GvGScoreRanking[] ranking_beat;
      public int totalPage;
    }
  }
}
