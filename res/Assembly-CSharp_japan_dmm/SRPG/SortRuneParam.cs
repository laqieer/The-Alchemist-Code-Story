// Decompiled with JetBrains decompiler
// Type: SRPG.SortRuneParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class SortRuneParam
  {
    public string iname;
    public string tab_name;
    public string name;
    public SortRuneConditionParam[] conditions;

    public void Deserialize(JSON_SortRuneParam json)
    {
      this.iname = json.iname;
      this.tab_name = json.tab_name;
      this.name = json.name;
      if (json.cnds == null)
        return;
      this.conditions = new SortRuneConditionParam[json.cnds.Length];
      for (int index = 0; index < json.cnds.Length; ++index)
      {
        SortRuneConditionParam runeConditionParam = new SortRuneConditionParam(this);
        runeConditionParam.Deserialize(json.cnds[index]);
        this.conditions[index] = runeConditionParam;
      }
    }

    public static void Deserialize(ref SortRuneParam[] param, JSON_SortRuneParam[] json)
    {
      if (json == null)
        return;
      param = new SortRuneParam[json.Length];
      for (int index = 0; index < json.Length; ++index)
      {
        SortRuneParam sortRuneParam = new SortRuneParam();
        sortRuneParam.Deserialize(json[index]);
        param[index] = sortRuneParam;
      }
    }
  }
}
