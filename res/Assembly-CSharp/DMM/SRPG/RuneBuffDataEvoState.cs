// Decompiled with JetBrains decompiler
// Type: SRPG.RuneBuffDataEvoState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  [Serializable]
  public class RuneBuffDataEvoState : RuneBuffData
  {
    public RuneLotteryEvoState evo_lot;
    public short val;
    public short slot;

    public bool Deserialize(Json_RuneBuffData json)
    {
      if (json == null)
        return false;
      if (MonoSingleton<GameManager>.Instance.MasterParam == null)
      {
        DebugUtility.LogError("GameManager.Instance.MasterParam が読み込まれる前に、RuneDataのデシリアライズが動いています");
        return false;
      }
      RuneLotteryEvoState runeLotteryEvoState = MonoSingleton<GameManager>.Instance.MasterParam.GetRuneLotteryEvoState(json.iname);
      if (runeLotteryEvoState == null)
      {
        DebugUtility.LogError("RuneLotteryEvoState に該当しない値" + json.iname + "が返却されました");
        return false;
      }
      this.evo_lot = runeLotteryEvoState;
      this.val = (short) json.val;
      this.slot = (short) json.slot;
      return true;
    }

    public Json_RuneBuffData Serialize()
    {
      if (this.evo_lot == null)
        return (Json_RuneBuffData) null;
      if (string.IsNullOrEmpty(this.evo_lot.iname))
        return (Json_RuneBuffData) null;
      return new Json_RuneBuffData()
      {
        val = (int) this.val,
        slot = (int) this.slot,
        iname = this.evo_lot.iname
      };
    }

    public void CreateBaseStatus(
      ref BaseStatus addStatus,
      ref BaseStatus scaleStatus,
      bool isDrawBaseStatus)
    {
      this.CreateBaseStatus(EElement.None, ref addStatus, ref scaleStatus, isDrawBaseStatus);
    }

    public void CreateBaseStatus(
      EElement element,
      ref BaseStatus addStatus,
      ref BaseStatus scaleStatus,
      bool isDrawBaseStatus)
    {
      if (this.evo_lot == null)
        return;
      if (this.evo_lot.calc == SkillParamCalcTypes.Add)
        addStatus = BuffEffect.CreateBaseStatus((int) this.val, this.evo_lot.type, BuffMethodTypes.Highest, element, isDrawBaseStatus);
      else if (this.evo_lot.calc == SkillParamCalcTypes.Scale)
        scaleStatus = BuffEffect.CreateBaseStatus((int) this.val, this.evo_lot.type, BuffMethodTypes.Highest, element, isDrawBaseStatus);
      else
        DebugUtility.LogError("直値は指定できません");
    }

    public float PowerPercentage()
    {
      return this.evo_lot == null ? 0.0f : this.PowerPercentage(this.evo_lot.type, this.val, (RuneLotteryState) this.evo_lot);
    }
  }
}
