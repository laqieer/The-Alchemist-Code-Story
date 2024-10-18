// Decompiled with JetBrains decompiler
// Type: SRPG.RuneBuffDataBaseState
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
  public class RuneBuffDataBaseState : RuneBuffData
  {
    public RuneLotteryBaseState base_lot;
    public short val;
    public short slot;

    public bool Deserialize(Json_RuneBuffData json)
    {
      if (json == null)
      {
        DebugUtility.LogError("RuneBuffDataBaseState にnullが指定されました、基本パラメータがないものは無い仕様のはず");
        return false;
      }
      if (MonoSingleton<GameManager>.Instance.MasterParam == null)
      {
        DebugUtility.LogError("GameManager.Instance.MasterParam が読み込まれる前に、RuneDataのデシリアライズが動いています");
        return false;
      }
      RuneLotteryBaseState lotteryBaseState = MonoSingleton<GameManager>.Instance.MasterParam.GetRuneLotteryBaseState(json.iname);
      if (lotteryBaseState == null)
      {
        DebugUtility.LogError("RuneLotteryBaseState に該当しない値" + json.iname + "が返却されました");
        return false;
      }
      this.val = (short) json.val;
      this.slot = (short) json.slot;
      this.base_lot = lotteryBaseState;
      return true;
    }

    public Json_RuneBuffData Serialize()
    {
      if (this.base_lot == null)
        return (Json_RuneBuffData) null;
      if (string.IsNullOrEmpty(this.base_lot.iname))
        return (Json_RuneBuffData) null;
      return new Json_RuneBuffData()
      {
        val = (int) this.val,
        slot = (int) this.slot,
        iname = this.base_lot.iname
      };
    }

    public void CreateBaseStatus(
      int enforce,
      ref BaseStatus addStatus,
      ref BaseStatus scaleStatus,
      bool isDrawBaseStatus)
    {
      this.CreateBaseStatus(enforce, EElement.None, ref addStatus, ref scaleStatus, isDrawBaseStatus);
    }

    public void CreateBaseStatus(
      int enforce,
      EElement element,
      ref BaseStatus addStatus,
      ref BaseStatus scaleStatus,
      bool isDrawBaseStatus)
    {
      if (this.base_lot == null)
        return;
      short num = this.val;
      if (enforce == 0)
        num = this.val;
      else if (enforce <= this.base_lot.enh_param.Length)
        num = (short) ((int) this.val + (int) this.base_lot.enh_param[enforce - 1]);
      if (this.base_lot.calc == SkillParamCalcTypes.Add)
        addStatus = BuffEffect.CreateBaseStatus((int) num, this.base_lot.type, BuffMethodTypes.Highest, element, isDrawBaseStatus);
      else if (this.base_lot.calc == SkillParamCalcTypes.Scale)
        scaleStatus = BuffEffect.CreateBaseStatus((int) num, this.base_lot.type, BuffMethodTypes.Highest, element, isDrawBaseStatus);
      else
        DebugUtility.LogError("直値は指定できません");
    }

    public void CreateOnlyBaseStatus(
      int enforce,
      ref BaseStatus addOnlyBaseStatus,
      ref BaseStatus scaleOnlyBaseStatus,
      bool isDrawBaseStatus)
    {
      this.CreateOnlyBaseStatus(enforce, EElement.None, ref addOnlyBaseStatus, ref scaleOnlyBaseStatus, isDrawBaseStatus);
    }

    public void CreateOnlyBaseStatus(
      int enforce,
      EElement element,
      ref BaseStatus addOnlyBaseStatus,
      ref BaseStatus scaleOnlyBaseStatus,
      bool isDrawBaseStatus)
    {
      if (this.base_lot == null)
        return;
      if (this.base_lot.calc == SkillParamCalcTypes.Add)
        addOnlyBaseStatus = BuffEffect.CreateBaseStatus((int) this.val, this.base_lot.type, BuffMethodTypes.Highest, element, isDrawBaseStatus);
      else if (this.base_lot.calc == SkillParamCalcTypes.Scale)
        scaleOnlyBaseStatus = BuffEffect.CreateBaseStatus((int) this.val, this.base_lot.type, BuffMethodTypes.Highest, element, isDrawBaseStatus);
      else
        DebugUtility.LogError("直値は指定できません");
    }

    public float PowerPercentage()
    {
      return this.base_lot == null ? 0.0f : this.PowerPercentage(this.base_lot.type, this.val, (RuneLotteryState) this.base_lot);
    }
  }
}
