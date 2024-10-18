// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGvGBattleExec
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  public class ReqGvGBattleExec : WebAPI
  {
    public ReqGvGBattleExec(
      int id,
      int gid,
      int gvg_group_id,
      JSON_GvGBattleEndParam btlendparam,
      Network.ResponseCallback response)
    {
      this.name = "gvg/btl/exec";
      this.body = WebAPI.GetRequestString<ReqGvGBattleExec.RequestParam>(new ReqGvGBattleExec.RequestParam()
      {
        id = id,
        btlendparam = btlendparam,
        gid = gid,
        gvg_group_id = gvg_group_id
      });
      this.callback = response;
      this.serializeCompressMethod = this.serializeCompressMethod;
    }

    [Serializable]
    public class RequestParam
    {
      public int id;
      public JSON_GvGBattleEndParam btlendparam;
      public int gid;
      public int gvg_group_id;
    }

    [MessagePackObject(true)]
    [Serializable]
    public class Response
    {
      public int is_capture;
      public JSON_TrophyProgress[] guild_trophies;
    }
  }
}
