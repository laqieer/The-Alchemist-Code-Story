// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGvGHalfTime
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  public class ReqGvGHalfTime : WebAPI
  {
    public ReqGvGHalfTime(
      int gid,
      int gvg_group_id,
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "gvg/btl/half_time";
      this.body = WebAPI.GetRequestString<ReqGvGHalfTime.RequestParam>(new ReqGvGHalfTime.RequestParam()
      {
        gid = gid,
        gvg_group_id = gvg_group_id
      });
      this.callback = response;
      this.serializeCompressMethod = serializeCompressMethod;
    }

    [Serializable]
    public class RequestParam
    {
      public int gid;
      public int gvg_group_id;
    }

    [MessagePackObject(true)]
    [Serializable]
    public class Response
    {
      public JSON_GvGHalfTime[] half_times;
    }
  }
}
