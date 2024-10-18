// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGvGBattleCapture
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  public class ReqGvGBattleCapture : WebAPI
  {
    public ReqGvGBattleCapture(
      int id,
      int gid,
      int gvg_group_id,
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "gvg/btl/capture";
      this.body = WebAPI.GetRequestString<ReqGvGBattleCapture.RequestParam>(new ReqGvGBattleCapture.RequestParam()
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
      public JSON_GvGUsedUnitData[] used_units;
      public int refresh_wait_sec;
      public int auto_refresh_wait_sec;
    }
  }
}
