// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGvGBattle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  public class ReqGvGBattle : WebAPI
  {
    public ReqGvGBattle(
      int id,
      int gid,
      int gvg_group_id,
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "gvg/btl";
      this.body = WebAPI.GetRequestString<ReqGvGBattle.RequestParam>(new ReqGvGBattle.RequestParam()
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
      public JSON_GvGPartyUnit[] offense;
      public int win_num;
      public int beat_num;
      public int seed;
      public JSON_GvGUsedUnitData[] used_units;
      public int cool_time;
      public int[] used_cards;
      public int[] used_artifacts;
      public int[] used_runes;
      public int maxActionNum;
    }
  }
}
