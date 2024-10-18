// Decompiled with JetBrains decompiler
// Type: SRPG.FilterUnitParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class FilterUnitParam
  {
    public string iname;
    public string tab_name;
    public string name;
    public FilterUnitConditionParam[] conditions;
    private eFilterUnitTypes filter_type;

    public bool IsEnableFilterType(eFilterUnitTypes type) => this.filter_type == type;

    public void Deserialize(JSON_FilterUnitParam json)
    {
      this.iname = json.iname;
      this.tab_name = json.tab_name;
      this.name = json.name;
      this.filter_type = (eFilterUnitTypes) json.filter_type;
      if (json.cnds == null)
        return;
      this.conditions = new FilterUnitConditionParam[json.cnds.Length];
      for (int index = 0; index < json.cnds.Length; ++index)
      {
        FilterUnitConditionParam unitConditionParam = new FilterUnitConditionParam(this);
        unitConditionParam.Deserialize(json.cnds[index]);
        this.conditions[index] = unitConditionParam;
      }
    }

    public static void Deserialize(ref FilterUnitParam[] param, JSON_FilterUnitParam[] json)
    {
      if (json == null)
        return;
      param = new FilterUnitParam[json.Length];
      for (int index = 0; index < json.Length; ++index)
      {
        FilterUnitParam filterUnitParam = new FilterUnitParam();
        filterUnitParam.Deserialize(json[index]);
        param[index] = filterUnitParam;
      }
    }
  }
}
