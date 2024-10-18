// Decompiled with JetBrains decompiler
// Type: SRPG.SkillAbilityDeriveTriggerParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  [MessagePackObject(false)]
  public class SkillAbilityDeriveTriggerParam
  {
    [Key(0)]
    public ESkillAbilityDeriveConds m_TriggerType;
    [Key(1)]
    public string m_TriggerIname;

    [SerializationConstructor]
    public SkillAbilityDeriveTriggerParam(ESkillAbilityDeriveConds triggerType, string triggerIname)
    {
      this.m_TriggerIname = triggerIname;
      this.m_TriggerType = triggerType;
    }

    public static List<SkillAbilityDeriveTriggerParam[]> CreateCombination(
      SkillAbilityDeriveTriggerParam[] triggerParams)
    {
      List<SkillAbilityDeriveTriggerParam[]> combination = new List<SkillAbilityDeriveTriggerParam[]>();
      Stack<SkillAbilityDeriveTriggerParam> deriveTriggerParamStack = new Stack<SkillAbilityDeriveTriggerParam>();
      for (int index1 = 0; index1 < triggerParams.Length; ++index1)
      {
        for (int index2 = index1; index2 < triggerParams.Length; ++index2)
        {
          deriveTriggerParamStack.Push(triggerParams[index2]);
          combination.Add(deriveTriggerParamStack.ToArray());
        }
        deriveTriggerParamStack.Clear();
      }
      return combination;
    }
  }
}
