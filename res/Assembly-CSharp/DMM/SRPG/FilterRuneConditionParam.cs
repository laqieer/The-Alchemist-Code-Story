// Decompiled with JetBrains decompiler
// Type: SRPG.FilterRuneConditionParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class FilterRuneConditionParam
  {
    public FilterRuneParam parent;
    public string cnds_iname;
    public string name;
    public byte rarity;
    public byte slot_index;
    public byte set_eff;
    public byte evo_status;

    public FilterRuneConditionParam(FilterRuneParam _parent) => this.parent = _parent;

    public void Deserialize(JSON_FilterRuneConditionParam json)
    {
      this.cnds_iname = json.cnds_iname;
      this.name = json.name;
      this.rarity = (byte) json.rarity;
      this.slot_index = (byte) json.slot_index;
      this.set_eff = (byte) json.set_eff;
      this.evo_status = (byte) json.evo_status;
    }

    public string PrefsKey => FilterUtility.FilterPrefs.MakeKey(this.parent.iname, this.cnds_iname);
  }
}
