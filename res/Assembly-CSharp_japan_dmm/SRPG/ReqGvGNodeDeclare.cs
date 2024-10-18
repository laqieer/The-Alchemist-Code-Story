// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGvGNodeDeclare
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  public class ReqGvGNodeDeclare : WebAPI
  {
    public ReqGvGNodeDeclare(
      int id,
      int gid,
      int gvg_group_id,
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "gvg/node/declare";
      this.body = WebAPI.GetRequestString<ReqGvGNodeDeclare.RequestParam>(new ReqGvGNodeDeclare.RequestParam()
      {
        id = id,
        gid = gid,
        gvg_group_id = gvg_group_id
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
    }

    [MessagePackObject(true)]
    [Serializable]
    public class Response
    {
      public JSON_GvGNodeData[] nodes;
      public int declare_num;
      public int refresh_wait_sec;
      public int auto_refresh_wait_sec;
      public int declare_cool_time;
    }
  }
}
