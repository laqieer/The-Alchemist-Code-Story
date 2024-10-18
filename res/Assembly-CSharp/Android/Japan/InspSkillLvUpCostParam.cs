// Decompiled with JetBrains decompiler
// Type: InspSkillLvUpCostParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

public class InspSkillLvUpCostParam
{
  public List<InspSkillLvUpCostParam.Cost> costs = new List<InspSkillLvUpCostParam.Cost>();
  public int id;

  public static void Desirialize(JSON_InspSkillLvUpCostParam[] json, ref List<InspSkillLvUpCostParam> cost_params)
  {
    cost_params = new List<InspSkillLvUpCostParam>();
    if (json == null || json.Length <= 0)
      return;
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
