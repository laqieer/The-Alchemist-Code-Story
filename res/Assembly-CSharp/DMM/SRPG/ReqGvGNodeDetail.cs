// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGvGNodeDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  public class ReqGvGNodeDetail : WebAPI
  {
    public ReqGvGNodeDetail(
      int id,
      int gid,
      int gvg_group_id,
      int page,
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "gvg/node/detail";
      this.body = WebAPI.GetRequestString<ReqGvGNodeDetail.RequestParam>(new ReqGvGNodeDetail.RequestParam()
      {
        id = id,
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
      public int id;
      public int gid;
      public int gvg_group_id;
      public int page;
    }

    [MessagePackObject(true)]
    [Serializable]
    public class Response
    {
      public JSON_GvGParty[] defenses;
      public int totalPage;
      public int total_beat_num;
    }
  }
}
