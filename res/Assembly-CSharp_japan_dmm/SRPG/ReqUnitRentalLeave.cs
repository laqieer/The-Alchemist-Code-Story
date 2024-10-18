// Decompiled with JetBrains decompiler
// Type: SRPG.ReqUnitRentalLeave
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  public class ReqUnitRentalLeave : WebAPI
  {
    public ReqUnitRentalLeave(
      string rental_iname,
      string unit_iname,
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "unit/rental/leave";
      this.body = WebAPI.GetRequestString<ReqUnitRentalLeave.RequestParam>(new ReqUnitRentalLeave.RequestParam()
      {
        rental_iname = rental_iname,
        unit_iname = unit_iname
      });
      this.callback = response;
      this.serializeCompressMethod = serializeCompressMethod;
    }

    [Serializable]
    public class RequestParam
    {
      public string rental_iname;
      public string unit_iname;
    }

    [MessagePackObject(true)]
    [Serializable]
    public class Response
    {
      public string leave_unit_iname;
      public Json_Item[] return_items;
      public Json_Item[] items;
    }
  }
}
