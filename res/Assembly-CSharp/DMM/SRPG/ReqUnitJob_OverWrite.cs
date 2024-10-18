// Decompiled with JetBrains decompiler
// Type: SRPG.ReqUnitJob_OverWrite
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  public class ReqUnitJob_OverWrite : WebAPI
  {
    public ReqUnitJob_OverWrite(
      long iid,
      long iid_job,
      eOverWritePartyType party_type,
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "unit/job/set_deck";
      this.body = WebAPI.GetRequestString<ReqUnitJob_OverWrite.RequestParam>(new ReqUnitJob_OverWrite.RequestParam()
      {
        iid = iid,
        iid_job = iid_job,
        ptype = UnitOverWriteUtility.OverWritePartyType2String(party_type)
      });
      this.callback = response;
      this.serializeCompressMethod = serializeCompressMethod;
    }

    [Serializable]
    public class RequestParam
    {
      public string ptype;
      public long iid;
      public long iid_job;
    }

    [MessagePackObject(true)]
    [Serializable]
    public class Response
    {
      public Json_PlayerData player;
      public JSON_PartyOverWrite[] party_decks;
    }
  }
}
