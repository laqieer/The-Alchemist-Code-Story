﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRuneEvo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  public class ReqRuneEvo : WebAPI
  {
    public ReqRuneEvo(
      ReqRuneEvo.RequestParam rp,
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "unit/rune/evo";
      this.body = WebAPI.GetRequestString<ReqRuneEvo.RequestParam>(rp);
      this.callback = response;
      this.serializeCompressMethod = serializeCompressMethod;
    }

    [MessagePackObject(true)]
    [Serializable]
    public class RequestParam
    {
      public long rune_id;

      public RequestParam()
      {
      }

      public RequestParam(long _rune_id) => this.rune_id = _rune_id;
    }

    [MessagePackObject(true)]
    [Serializable]
    public class Response
    {
      public Json_RuneData[] runes;
      public Json_Item[] items;
      public Json_PlayerData player;
    }
  }
}
