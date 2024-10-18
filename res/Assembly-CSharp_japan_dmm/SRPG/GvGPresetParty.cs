// Decompiled with JetBrains decompiler
// Type: SRPG.GvGPresetParty
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class GvGPresetParty
  {
    public List<long> Uids { get; set; }

    public string Name { get; set; }

    public bool Deserialize(JSON_GvGPresetPartyData json)
    {
      if (json == null)
        return false;
      this.Name = json.name;
      this.Uids = new List<long>();
      if (json.u != null)
        this.Uids.AddRange((IEnumerable<long>) json.u);
      return true;
    }
  }
}
