// Decompiled with JetBrains decompiler
// Type: SRPG.ReqArtifactSet_OverWrite
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  public class ReqArtifactSet_OverWrite : WebAPI
  {
    public ReqArtifactSet_OverWrite(
      long iid_unit,
      long iid_job,
      long[] iid_artifact,
      eOverWritePartyType party_type,
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "unit/job/artifact/set_deck";
      this.body = WebAPI.GetRequestString<ReqArtifactSet_OverWrite.RequestParam>(new ReqArtifactSet_OverWrite.RequestParam()
      {
        iid_unit = iid_unit,
        iid_job = iid_job,
        iid_artifacts = iid_artifact,
        ptype = UnitOverWriteUtility.OverWritePartyType2String(party_type)
      });
      this.callback = response;
      this.serializeCompressMethod = serializeCompressMethod;
    }

    [Serializable]
    public class RequestParam
    {
      public string ptype;
      public long iid_unit;
      public long iid_job;
      public long[] iid_artifacts;
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
