// Decompiled with JetBrains decompiler
// Type: SRPG.ReqSetConceptLeaderSkill_OverWrite
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  public class ReqSetConceptLeaderSkill_OverWrite : WebAPI
  {
    public ReqSetConceptLeaderSkill_OverWrite(
      long unit_iid,
      bool set,
      eOverWritePartyType party_type,
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "unit/concept/leaderskill/set_deck";
      this.body = WebAPI.GetRequestString<ReqSetConceptLeaderSkill_OverWrite.RequestParam>(new ReqSetConceptLeaderSkill_OverWrite.RequestParam()
      {
        unit_iid = unit_iid,
        is_set = !set ? 0 : 1,
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
      public int is_set;
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
