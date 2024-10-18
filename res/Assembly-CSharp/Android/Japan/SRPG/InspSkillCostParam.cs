// Decompiled with JetBrains decompiler
// Type: SRPG.InspSkillCostParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

namespace SRPG
{
  public class InspSkillCostParam
  {
    private InspSkillCostType cost_type;
    private int num;

    public InspSkillCostType Type
    {
      get
      {
        return this.cost_type;
      }
    }

    public int Num
    {
      get
      {
        return this.num;
      }
    }

    public bool Deserialize(JSON_InspSkillCostParam json)
    {
      if (json == null)
        return false;
      this.cost_type = (InspSkillCostType) json.item;
      this.num = json.num;
      return true;
    }

    public static bool Deserialize(JSON_InspSkillCostParam[] json, ref Dictionary<int, InspSkillCostParam> cost_param_list)
    {
      if (json == null || json.Length <= 0)
        return false;
      cost_param_list = new Dictionary<int, InspSkillCostParam>();
      foreach (JSON_InspSkillCostParam json1 in json)
      {
        if (!cost_param_list.ContainsKey(json1.count))
        {
          InspSkillCostParam inspSkillCostParam = new InspSkillCostParam();
          if (inspSkillCostParam.Deserialize(json1))
            cost_param_list.Add(json1.count, inspSkillCostParam);
        }
      }
      return true;
    }
  }
}
