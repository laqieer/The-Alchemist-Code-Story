// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRuneDisassembly
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  public class ReqRuneDisassembly : WebAPI
  {
    public ReqRuneDisassembly(
      ReqRuneDisassembly.RequestParam rp,
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "unit/rune/disassemble";
      this.body = WebAPI.GetRequestString<ReqRuneDisassembly.RequestParam>(rp);
      this.callback = response;
      this.serializeCompressMethod = serializeCompressMethod;
    }

    [MessagePackObject(true)]
    [Serializable]
    public class RequestParam
    {
      public long[] rune_ids;

      public RequestParam()
      {
      }

      public RequestParam(long[] _rune_ids) => this.rune_ids = _rune_ids;
    }

    [MessagePackObject(true)]
    [Serializable]
    public class Response
    {
      public Json_Item[] items;
      public Json_PlayerData player;
      public ReqRuneDisassembly.Response.Rewards[] rewards;
      public long[] rune_ids;
      public string result;
      public int rune_storage_used;

      public ReqRuneDisassembly.Response.Result GetResult()
      {
        if (this.result == "great")
          return ReqRuneDisassembly.Response.Result.great;
        return this.result == "super" ? ReqRuneDisassembly.Response.Result.super : ReqRuneDisassembly.Response.Result.success;
      }

      public enum Result
      {
        success,
        great,
        super,
      }

      [MessagePackObject(true)]
      [Serializable]
      public class Rewards
      {
        public string iname;
        public int num;
      }
    }
  }
}
