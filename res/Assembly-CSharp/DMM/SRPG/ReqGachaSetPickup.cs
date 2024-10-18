// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGachaSetPickup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using System;

#nullable disable
namespace SRPG
{
  public class ReqGachaSetPickup : WebAPI
  {
    public ReqGachaSetPickup(
      ReqGachaSetPickup.RequestParam rp,
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "gacha/pickup/set";
      this.body = WebAPI.GetRequestString<ReqGachaSetPickup.RequestParam>(rp);
      this.callback = response;
      this.serializeCompressMethod = serializeCompressMethod;
    }

    [Serializable]
    public class RequestParam
    {
      public string gachaid;
      public ReqGachaSetPickup.PickupData[] pickups;
    }

    [Serializable]
    public class PickupData
    {
      public string itype;
      public string iname;
    }
  }
}
