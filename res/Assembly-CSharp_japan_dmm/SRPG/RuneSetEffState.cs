// Decompiled with JetBrains decompiler
// Type: SRPG.RuneSetEffState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  [Serializable]
  public class RuneSetEffState
  {
    public ParamTypes type;
    public SkillParamCalcTypes calc;
    public short vone;

    public bool Deserialize(JSON_RuneSetEffState json)
    {
      this.type = (ParamTypes) json.type;
      this.calc = (SkillParamCalcTypes) json.calc;
      this.vone = (short) json.vone;
      return true;
    }

    public void CreateBaseStatus(
      EElement element,
      ref BaseStatus addStatus,
      ref BaseStatus scaleStatus,
      bool isDrawBaseStatus)
    {
      if (this.calc == SkillParamCalcTypes.Add)
        addStatus = BuffEffect.CreateBaseStatus((int) this.vone, this.type, BuffMethodTypes.Highest, element, isDrawBaseStatus);
      else if (this.calc == SkillParamCalcTypes.Scale)
        scaleStatus = BuffEffect.CreateBaseStatus((int) this.vone, this.type, BuffMethodTypes.Highest, element, isDrawBaseStatus);
      else
        DebugUtility.LogError("直値は指定できません");
    }
  }
}
