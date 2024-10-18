// Decompiled with JetBrains decompiler
// Type: InspSkillLvUpCostParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
public class InspSkillLvUpCostParam
{
  public int id;
  public List<InspSkillLvUpCostParam.Cost> costs = new List<InspSkillLvUpCostParam.Cost>();

  public static void Desirialize(
    JSON_InspSkillLvUpCostParam[] json,
    ref List<InspSkillLvUpCostParam> cost_params)
  {
    if (json == null || json.Length <= 0)
      return;
    cost_params = new List<InspSkillLvUpCostParam>();
    foreach (JSON_InspSkillLvUpCostParam skillLvUpCostParam1 in json)
    {
      if (skillLvUpCostParam1.costs != null && skillLvUpCostParam1.costs.Length > 0)
      {
        InspSkillLvUpCostParam skillLvUpCostParam2 = new InspSkillLvUpCostParam();
        skillLvUpCostParam2.id = skillLvUpCostParam1.id;
        skillLvUpCostParam2.costs = new List<InspSkillLvUpCostParam.Cost>();
        foreach (JSON_InspSkillLvUpCostParam.JSON_CostData cost in skillLvUpCostParam1.costs)
          skillLvUpCostParam2.costs.Add(new InspSkillLvUpCostParam.Cost()
          {
            gold = cost.gold
          });
        cost_params.Add(skillLvUpCostParam2);
      }
    }
  }

  public class Cost
  {
    public int gold;
  }
}
