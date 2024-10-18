// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRuneFavorite
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  public class ReqRuneFavorite : WebAPI
  {
    public ReqRuneFavorite(
      ReqRuneFavorite.RequestParam rp,
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "unit/rune/favorite";
      this.body = WebAPI.GetRequestString<ReqRuneFavorite.RequestParam>(rp);
      this.callback = response;
      this.serializeCompressMethod = serializeCompressMethod;
    }

    [MessagePackObject(true)]
    [Serializable]
    public class RequestParam
    {
      public long rune_id;
      public int favorite;

      public RequestParam()
      {
      }

      public RequestParam(long runeId, bool isFavorite)
      {
        this.rune_id = runeId;
        this.favorite = !isFavorite ? 0 : 1;
      }
    }

    [MessagePackObject(true)]
    [Serializable]
    public class Response
    {
      public Json_RuneData rune;
    }
  }
}
