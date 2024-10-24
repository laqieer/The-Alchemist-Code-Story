﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRuneEquip
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  public class ReqRuneEquip : WebAPI
  {
    public ReqRuneEquip(
      ReqRuneEquip.RequestParam rp,
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "unit/rune/set";
      this.body = WebAPI.GetRequestString<ReqRuneEquip.RequestParam>(rp);
      this.callback = response;
      this.serializeCompressMethod = serializeCompressMethod;
    }

    [MessagePackObject(true)]
    [Serializable]
    public class RequestParam
    {
      public long unit_id;
      public long[] rune_ids;

      public RequestParam()
      {
      }

      public RequestParam(long _unit_id, long[] _rune_ids)
      {
        this.unit_id = _unit_id;
        this.rune_ids = _rune_ids;
      }
    }

    [MessagePackObject(true)]
    [Serializable]
    public class Response
    {
      public Json_Unit[] units;
      public int rune_storage_used;
    }
  }
}
