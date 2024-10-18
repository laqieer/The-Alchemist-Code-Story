// Decompiled with JetBrains decompiler
// Type: SRPG.ReqSetConceptCard_OverWrite
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  public class ReqSetConceptCard_OverWrite : WebAPI
  {
    public ReqSetConceptCard_OverWrite(
      long[] card_iids,
      long unit_iid,
      eOverWritePartyType party_type,
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "unit/concept/set_deck";
      this.body = WebAPI.GetRequestString<ReqSetConceptCard_OverWrite.RequestParam>(new ReqSetConceptCard_OverWrite.RequestParam()
      {
        unit_iid = unit_iid,
        concept_iids = card_iids,
        ptype = UnitOverWriteUtility.OverWritePartyType2String(party_type)
      });
      this.callback = response;
      this.serializeCompressMethod = serializeCompressMethod;
    }

    [Serializable]
    public class RequestParam
    {
      public string ptype;
      public long unit_iid;
      public long[] concept_iids;
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
