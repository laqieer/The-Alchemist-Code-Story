// Decompiled with JetBrains decompiler
// Type: SRPG.InspSkillCostParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class InspSkillCostParam
  {
    private InspSkillCostType cost_type;
    private int num;

    public InspSkillCostType Type => this.cost_type;

    public int Num => this.num;

    public bool Deserialize(JSON_InspSkillCostParam json)
    {
      if (json == null)
        return false;
      this.cost_type = (InspSkillCostType) json.item;
      this.num = json.num;
      return true;
    }

    public static bool Deserialize(
      JSON_InspSkillCostParam[] json,
      ref Dictionary<int, InspSkillCostParam> cost_param_dict)
    {
      if (json == null || json.Length <= 0)
        return false;
      cost_param_dict = new Dictionary<int, InspSkillCostParam>();
      foreach (JSON_InspSkillCostParam json1 in json)
      {
        if (!cost_param_dict.ContainsKey(json1.count))
        {
          InspSkillCostParam inspSkillCostParam = new InspSkillCostParam();
          if (inspSkillCostParam.Deserialize(json1))
            cost_param_dict.Add(json1.count, inspSkillCostParam);
        }
      }
      return true;
    }
  }
}
