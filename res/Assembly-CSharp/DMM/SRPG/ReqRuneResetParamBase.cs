// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRuneResetParamBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  public class ReqRuneResetParamBase : WebAPI
  {
    public ReqRuneResetParamBase(
      ReqRuneResetParamBase.RequestParam rp,
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "unit/rune/state/base/param/reset";
      this.body = WebAPI.GetRequestString<ReqRuneResetParamBase.RequestParam>(rp);
      this.callback = response;
      this.serializeCompressMethod = serializeCompressMethod;
    }

    [MessagePackObject(true)]
    [Serializable]
    public class RequestParam
    {
      public long rune_id;
      public int cost_index;

      public RequestParam()
      {
      }

      public RequestParam(long _rune_id, int _cost_index)
      {
        this.rune_id = _rune_id;
        this.cost_index = _cost_index;
      }
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
