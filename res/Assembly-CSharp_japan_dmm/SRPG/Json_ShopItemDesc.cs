// Decompiled with JetBrains decompiler
// Type: SRPG.Json_ShopItemDesc
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class Json_ShopItemDesc
  {
    public string iname;
    public int num;
    public string itype;
    public int maxnum;
    public int boughtnum;
    public int step;
    public int has_count;

    public bool IsItem => this.itype == "item";

    public bool IsArtifact => this.itype == "artifact";

    public bool IsConceptCard => this.itype == "concept_card";
  }
}
