// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGachaPickup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  public class ReqGachaPickup : WebAPI
  {
    public ReqGachaPickup(
      ReqGachaPickup.RequestParam rp,
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "gacha/pickup";
      this.body = WebAPI.GetRequestString<ReqGachaPickup.RequestParam>(rp);
      this.callback = response;
      this.serializeCompressMethod = serializeCompressMethod;
    }

    [Serializable]
    public class RequestParam
    {
      public string gachaid;
    }

    [MessagePackObject(true)]
    [Serializable]
    public class Response
    {
      public Json_GachaPickups[] pickups;
      public int pickup_select_num;
    }
  }
}
