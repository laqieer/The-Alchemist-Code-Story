// Decompiled with JetBrains decompiler
// Type: SRPG.CondAttachment
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using MessagePack;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  public class CondAttachment
  {
    public Unit user;
    public Unit CheckTarget;
    private OInt mCheckTiming;
    private OInt mUseCondition;
    public OInt turn;
    public OBool IsPassive;
    public SkillData skill;
    public SkillEffectTargets skilltarget;
    public string CondId;
    private CondEffectParam mParam;
    private OInt mCondType;
    private OLong mCondition;
    public BuffEffect LinkageBuff;
    public uint LinkageID;

    public CondAttachment()
    {
    }

    public CondAttachment(CondEffectParam param) => this.mParam = param;

    public EffectCheckTimings CheckTiming
    {
      get => (EffectCheckTimings) (int) this.mCheckTiming;
      set => this.mCheckTiming = (OInt) (int) value;
    }

    public ESkillCondition UseCondition
    {
      get => (ESkillCondition) (int) this.mUseCondition;
      set => this.mUseCondition = (OInt) (int) value;
    }

    public CondEffectParam Param => this.mParam;

    public ConditionEffectTypes CondType
    {
      get => (ConditionEffectTypes) (int) this.mCondType;
      set => this.mCondType = (OInt) (int) value;
    }

    public EUnitCondition Condition
    {
      get => (EUnitCondition) (long) this.mCondition;
      set => this.mCondition = (OLong) (long) value;
    }

    public bool IsCurse { get; set; }

    public void CopyTo(CondAttachment dsc)
    {
      dsc.user = this.user;
      dsc.skill = this.skill;
      dsc.skilltarget = this.skilltarget;
      dsc.CondId = this.CondId;
      dsc.mParam = this.mParam;
      dsc.turn = this.turn;
      dsc.IsPassive = this.IsPassive;
      dsc.CondType = this.CondType;
      dsc.Condition = this.Condition;
      dsc.CheckTarget = this.CheckTarget;
      dsc.CheckTiming = this.CheckTiming;
      dsc.UseCondition = this.UseCondition;
    }

    public bool IsFailCondition()
    {
      switch (this.CondType)
      {
        case ConditionEffectTypes.FailCondition:
        case ConditionEffectTypes.ForcedFailCondition:
        case ConditionEffectTypes.RandomFailCondition:
          return true;
        default:
          return false;
      }
    }

    public bool ContainsCondition(EUnitCondition condition)
    {
      return ((EUnitCondition) (long) this.mCondition & condition) != (EUnitCondition) 0;
    }

    public bool IsSame(CondAttachment dsc, bool is_ignore_timing = false)
    {
      return dsc != null && this.skill != null && dsc.skill != null && this.skill.SkillID == dsc.skill.SkillID && (bool) this.IsPassive == (bool) dsc.IsPassive && this.CondType == dsc.CondType && this.Condition == dsc.Condition && (is_ignore_timing || this.CheckTiming == dsc.CheckTiming) && this.UseCondition == dsc.UseCondition;
    }

    public void SetupLinkageBuff()
    {
      this.LinkageBuff = (BuffEffect) null;
      if (this.skill == null || this.mParam == null)
        return;
      string linkageBuffId = this.mParam.GetLinkageBuffId(this.Condition);
      if (string.IsNullOrEmpty(linkageBuffId))
        return;
      this.LinkageBuff = BuffEffect.CreateBuffEffect(MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetBuffEffectParam(linkageBuffId), this.skill.Rank, this.skill.GetRankCap());
    }
  }
}
