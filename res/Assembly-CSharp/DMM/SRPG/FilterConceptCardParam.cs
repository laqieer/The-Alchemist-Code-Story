// Decompiled with JetBrains decompiler
// Type: SRPG.FilterConceptCardParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class FilterConceptCardParam
  {
    public string iname;
    public string tab_name;
    public string name;
    public eConceptCardFilterTypes filter_type;
    public FilterConceptCardConditionParam[] conditions;

    public bool IsEnableFilterType(eConceptCardFilterTypes type) => this.filter_type == type;

    public void Deserialize(JSON_FilterConceptCardParam json)
    {
      this.iname = json.iname;
      this.tab_name = json.tab_name;
      this.name = json.name;
      this.filter_type = (eConceptCardFilterTypes) json.filter_type;
      if (json.cnds == null)
        return;
      this.conditions = new FilterConceptCardConditionParam[json.cnds.Length];
      for (int index = 0; index < json.cnds.Length; ++index)
      {
        FilterConceptCardConditionParam cardConditionParam = new FilterConceptCardConditionParam(this);
        cardConditionParam.Deserialize(json.cnds[index]);
        this.conditions[index] = cardConditionParam;
      }
    }

    public static void Deserialize(
      ref FilterConceptCardParam[] param,
      JSON_FilterConceptCardParam[] json)
    {
      if (json == null)
        return;
      param = new FilterConceptCardParam[json.Length];
      for (int index = 0; index < json.Length; ++index)
      {
        FilterConceptCardParam conceptCardParam = new FilterConceptCardParam();
        conceptCardParam.Deserialize(json[index]);
        param[index] = conceptCardParam;
      }
    }
  }
}
