// Decompiled with JetBrains decompiler
// Type: SRPG.Json_ShopItemDesc
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class Json_ShopItemDesc
  {
    public string iname;
    public int num;
    public string itype;
    public int maxnum;
    public int boughtnum;
    public int has_count;

    public bool IsItem
    {
      get
      {
        return this.itype == "item";
      }
    }

    public bool IsArtifact
    {
      get
      {
        return this.itype == "artifact";
      }
    }

    public bool IsConceptCard
    {
      get
      {
        return this.itype == "concept_card";
      }
    }
  }
}
