// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGetRune
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  public class ReqGetRune : WebAPI
  {
    public ReqGetRune(
      ReqGetRune.RequestParam rp,
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "unit/rune";
      this.body = WebAPI.GetRequestString<ReqGetRune.RequestParam>(rp);
      this.callback = response;
      this.serializeCompressMethod = serializeCompressMethod;
    }

    [MessagePackObject(true)]
    [Serializable]
    public class RequestParam
    {
      public long last_iid;

      public RequestParam()
      {
      }

      public RequestParam(long _last_iid) => this.last_iid = _last_iid;
    }

    [MessagePackObject(true)]
    [Serializable]
    public class Response
    {
      public Json_RuneData[] runes;
      public Json_RuneEnforceGaugeData[] rune_enforce_gauge;
      public int rune_storage;
      public int rune_storage_used;
    }
  }
}
