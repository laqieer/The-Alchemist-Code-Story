// Decompiled with JetBrains decompiler
// Type: SRPG.GvGNPCPartyDetailParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class GvGNPCPartyDetailParam
  {
    public int Order { get; private set; }

    public int Unit1Id { get; private set; }

    public int Unit2Id { get; private set; }

    public int Unit3Id { get; private set; }

    public bool Deserialize(JSON_GvGNPCPartyDetailParam json)
    {
      if (json == null)
        return false;
      this.Order = json.order;
      this.Unit1Id = json.unit1_id;
      this.Unit2Id = json.unit2_id;
      this.Unit3Id = json.unit3_id;
      return true;
    }
  }
}
