// Decompiled with JetBrains decompiler
// Type: SRPG.TobiraCondsParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class TobiraCondsParam
  {
    private string mUnitIname;
    private TobiraParam.Category mCategory;
    private List<TobiraConditionParam> mConditions = new List<TobiraConditionParam>();

    public string UnitIname => this.mUnitIname;

    public TobiraParam.Category TobiraCategory => this.mCategory;

    public TobiraConditionParam[] Conditions => this.mConditions.ToArray();

    public void Deserialize(JSON_TobiraCondsParam json)
    {
      if (json == null)
        return;
      this.mUnitIname = json.unit_iname;
      this.mCategory = (TobiraParam.Category) json.category;
      this.mConditions.Clear();
      if (json.conds == null)
        return;
      for (int index = 0; index < json.conds.Length; ++index)
      {
        TobiraConditionParam tobiraConditionParam = new TobiraConditionParam();
        tobiraConditionParam.Deserialize(json.conds[index]);
        this.mConditions.Add(tobiraConditionParam);
      }
    }
  }
}
