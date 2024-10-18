// Decompiled with JetBrains decompiler
// Type: SRPG.GvGNPCPartyParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class GvGNPCPartyParam : GvGMasterParam<JSON_GvGNPCPartyParam>
  {
    public int Id { get; private set; }

    public List<GvGNPCPartyDetailParam> Party { get; private set; }

    public override bool Deserialize(JSON_GvGNPCPartyParam json)
    {
      if (json == null)
        return false;
      this.Id = json.id;
      this.Party = new List<GvGNPCPartyDetailParam>();
      if (json.party != null)
      {
        for (int index = 0; index < json.party.Length; ++index)
        {
          GvGNPCPartyDetailParam partyDetailParam = new GvGNPCPartyDetailParam();
          if (partyDetailParam.Deserialize(json.party[index]))
            this.Party.Add(partyDetailParam);
        }
      }
      return true;
    }
  }
}
