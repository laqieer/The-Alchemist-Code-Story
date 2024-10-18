// Decompiled with JetBrains decompiler
// Type: SRPG.FilterRuneParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class FilterRuneParam
  {
    public string iname;
    public string tab_name;
    public string name;
    public eRuneFilterTypes filter_type;
    public FilterRuneConditionParam[] conditions;

    public bool IsEnableFilterType(eRuneFilterTypes type) => this.filter_type == type;

    public void Deserialize(JSON_FilterRuneParam json)
    {
      this.iname = json.iname;
      this.tab_name = json.tab_name;
      this.name = json.name;
      this.filter_type = (eRuneFilterTypes) json.filter_type;
      if (json.cnds == null)
        return;
      this.conditions = new FilterRuneConditionParam[json.cnds.Length];
      for (int index = 0; index < json.cnds.Length; ++index)
      {
        FilterRuneConditionParam runeConditionParam = new FilterRuneConditionParam(this);
        runeConditionParam.Deserialize(json.cnds[index]);
        this.conditions[index] = runeConditionParam;
      }
    }

    public static void Deserialize(ref FilterRuneParam[] param, JSON_FilterRuneParam[] json)
    {
      if (json == null)
        return;
      param = new FilterRuneParam[json.Length];
      for (int index = 0; index < json.Length; ++index)
      {
        FilterRuneParam filterRuneParam = new FilterRuneParam();
        filterRuneParam.Deserialize(json[index]);
        param[index] = filterRuneParam;
      }
    }
  }
}
