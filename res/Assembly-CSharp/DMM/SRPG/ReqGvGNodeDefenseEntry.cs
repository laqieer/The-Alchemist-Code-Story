// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGvGNodeDefenseEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  public class ReqGvGNodeDefenseEntry : WebAPI
  {
    public ReqGvGNodeDefenseEntry(
      int id,
      long[] party,
      int gid,
      int gvg_group_id,
      int page,
      int is_force,
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "gvg/node/defense/entry";
      this.body = WebAPI.GetRequestString<ReqGvGNodeDefenseEntry.RequestParam>(new ReqGvGNodeDefenseEntry.RequestParam()
      {
        id = id,
        party = party,
        gid = gid,
        gvg_group_id = gvg_group_id,
        page = page,
        is_force = is_force
      });
      this.callback = response;
      this.serializeCompressMethod = serializeCompressMethod;
    }

    [Serializable]
    public class RequestParam
    {
      public int id;
      public long[] party;
      public int gid;
      public int gvg_group_id;
      public int page;
      public int is_force;
    }

    [MessagePackObject(true)]
    [Serializable]
    public class Response
    {
      public JSON_GvGParty[] defenses;
      public JSON_GvGUsedUnitData[] used_units;
      public int totalPage;
      public int total_beat_num;
      public JSON_GvGUsedItems[] unavailable_items;
    }
  }
}
