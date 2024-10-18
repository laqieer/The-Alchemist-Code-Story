// Decompiled with JetBrains decompiler
// Type: SRPG.ReqAllEquipExpAdd
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  public class ReqAllEquipExpAdd : WebAPI
  {
    public ReqAllEquipExpAdd(
      long iid,
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "unit/job/equip/bulk_enforce_max";
      this.body = WebAPI.GetRequestString<ReqAllEquipExpAdd.RequestParam>(new ReqAllEquipExpAdd.RequestParam()
      {
        iid = iid
      });
      this.callback = response;
      this.serializeCompressMethod = serializeCompressMethod;
    }

    [Serializable]
    public class RequestParam
    {
      public long iid;
    }

    [MessagePackObject(true)]
    [Serializable]
    public class Response
    {
      public Json_PlayerData player;
      public Json_Unit[] units;
      public Json_Item[] items;
    }
  }
}
