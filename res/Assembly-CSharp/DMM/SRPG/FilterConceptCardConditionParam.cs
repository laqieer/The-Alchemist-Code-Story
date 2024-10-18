// Decompiled with JetBrains decompiler
// Type: SRPG.FilterConceptCardConditionParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class FilterConceptCardConditionParam
  {
    public FilterConceptCardParam parent;
    public string cnds_iname;
    public string name;
    public int rarity;
    public EBirth birth;
    public string card_group;

    public FilterConceptCardConditionParam(FilterConceptCardParam _parent) => this.parent = _parent;

    public void Deserialize(JSON_FilterConceptCardConditionParam json)
    {
      this.cnds_iname = json.cnds_iname;
      this.name = json.name;
      this.rarity = json.rarity;
      this.birth = (EBirth) json.birth;
      this.card_group = json.card_group;
    }

    public string PrefsKey => FilterUtility.FilterPrefs.MakeKey(this.parent.iname, this.cnds_iname);
  }
}
