// Decompiled with JetBrains decompiler
// Type: SRPG.DependStateSpcEffParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class DependStateSpcEffParam
  {
    private string mIname;
    private DependStateSpcEffParam.Flags mFlags;
    private List<DependStateSpcEffParam.BuffInfo> mBuffList;
    private List<EUnitCondition> mCondList;
    private int mInvSaRate;

    private bool mIsAnd
    {
      get => (this.mFlags & DependStateSpcEffParam.Flags.IsAnd) != (DependStateSpcEffParam.Flags) 0;
    }

    private bool mIsInvTargetBuff
    {
      get
      {
        return (this.mFlags & DependStateSpcEffParam.Flags.IsInvTargetBuff) != (DependStateSpcEffParam.Flags) 0;
      }
    }

    private bool mIsInvTargetCond
    {
      get
      {
        return (this.mFlags & DependStateSpcEffParam.Flags.IsInvTargetCond) != (DependStateSpcEffParam.Flags) 0;
      }
    }

    private bool mIsInvSelfBuff
    {
      get
      {
        return (this.mFlags & DependStateSpcEffParam.Flags.IsInvSelfBuff) != (DependStateSpcEffParam.Flags) 0;
      }
    }

    private bool mIsInvSelfCond
    {
      get
      {
        return (this.mFlags & DependStateSpcEffParam.Flags.IsInvSelfCond) != (DependStateSpcEffParam.Flags) 0;
      }
    }

    public string Iname => this.mIname;

    public int InvSaRate => this.mInvSaRate;

    public void Deserialize(JSON_DependStateSpcEffParam json)
    {
      if (json == null)
        return;
      this.mIname = json.iname;
      this.mBuffList = (List<DependStateSpcEffParam.BuffInfo>) null;
      if (json.buff_ids != null)
      {
        this.mBuffList = new List<DependStateSpcEffParam.BuffInfo>(json.buff_ids.Length);
        for (int index = 0; index < json.buff_ids.Length; ++index)
        {
          ParamTypes buffId = (ParamTypes) json.buff_ids[index];
          DependStateSpcEffParam.CondBuffTypes cbt = DependStateSpcEffParam.CondBuffTypes.All;
          if (json.buff_types != null && index < json.buff_types.Length)
            cbt = (DependStateSpcEffParam.CondBuffTypes) json.buff_types[index];
          this.mBuffList.Add(new DependStateSpcEffParam.BuffInfo(buffId, cbt));
        }
      }
      this.mCondList = (List<EUnitCondition>) null;
      if (json.cond_ids != null)
      {
        this.mCondList = new List<EUnitCondition>(json.cond_ids.Length);
        for (int index = 0; index < json.cond_ids.Length; ++index)
          this.mCondList.Add((EUnitCondition) (1L << json.cond_ids[index]));
      }
      this.mInvSaRate = json.inv_tkrate;
      this.mFlags = (DependStateSpcEffParam.Flags) 0;
      if (json.is_and != 0)
        this.mFlags |= DependStateSpcEffParam.Flags.IsAnd;
      if (json.is_inv_t_buff != 0)
        this.mFlags |= DependStateSpcEffParam.Flags.IsInvTargetBuff;
      if (json.is_inv_t_cond != 0)
        this.mFlags |= DependStateSpcEffParam.Flags.IsInvTargetCond;
      if (json.is_inv_s_buff != 0)
        this.mFlags |= DependStateSpcEffParam.Flags.IsInvSelfBuff;
      if (json.is_inv_s_cond == 0)
        return;
      this.mFlags |= DependStateSpcEffParam.Flags.IsInvSelfCond;
    }

    private bool IsExistBuff(BuffAttachment ba, DependStateSpcEffParam.BuffInfo bi)
    {
      if (ba == null || ba.BuffEffect == null || ba.BuffEffect.targets == null || bi == null || bi.mParamType == ParamTypes.None)
        return false;
      switch (bi.mCondBuffType)
      {
        case DependStateSpcEffParam.CondBuffTypes.Buff:
          if (ba.BuffType != BuffTypes.Buff)
            return false;
          break;
        case DependStateSpcEffParam.CondBuffTypes.Debuff:
          if (ba.BuffType != BuffTypes.Debuff)
            return false;
          break;
      }
      for (int index = 0; index < ba.BuffEffect.targets.Count; ++index)
      {
        BuffEffect.BuffTarget target = ba.BuffEffect.targets[index];
        if (target != null && (bi.mCondBuffType == DependStateSpcEffParam.CondBuffTypes.All || (bi.mCondBuffType != DependStateSpcEffParam.CondBuffTypes.Buff || target.buffType == BuffTypes.Buff) && (bi.mCondBuffType != DependStateSpcEffParam.CondBuffTypes.Debuff || target.buffType == BuffTypes.Debuff)) && target.paramType == bi.mParamType)
          return true;
      }
      return false;
    }

    public bool IsSatisfyCondition(Unit target)
    {
      if (target == null)
        return false;
      List<bool> boolList = new List<bool>();
      if (this.mBuffList != null)
      {
        for (int idx = 0; idx < this.mBuffList.Count; ++idx)
        {
          BuffAttachment buffAttachment = target.BuffAttachments.Find((Predicate<BuffAttachment>) (tba => this.IsExistBuff(tba, this.mBuffList[idx])));
          boolList.Add(buffAttachment != null);
        }
      }
      if (this.mCondList != null)
      {
        for (int index = 0; index < this.mCondList.Count; ++index)
          boolList.Add(target.IsUnitCondition(this.mCondList[index]));
      }
      if (boolList.Count == 0)
        return true;
      return !this.mIsAnd ? boolList.Contains(true) : !boolList.Contains(false);
    }

    public bool IsApplyBuff(Unit target, SkillEffectTargets buff_target)
    {
      if (target == null)
        return false;
      switch (buff_target)
      {
        case SkillEffectTargets.Target:
          if (!this.mIsInvTargetBuff)
            return true;
          break;
        case SkillEffectTargets.Self:
          if (!this.mIsInvSelfBuff)
            return true;
          break;
      }
      return this.IsSatisfyCondition(target);
    }

    public bool IsApplyBuff(List<Unit> target_list, SkillEffectTargets buff_target)
    {
      if (target_list == null)
        return false;
      bool flag = false;
      for (int index = 0; index < target_list.Count; ++index)
      {
        if (this.IsApplyBuff(target_list[index], buff_target))
        {
          flag = true;
          break;
        }
      }
      return flag;
    }

    public bool IsApplyCond(Unit target, SkillEffectTargets cond_target)
    {
      if (target == null)
        return false;
      switch (cond_target)
      {
        case SkillEffectTargets.Target:
          if (!this.mIsInvTargetCond)
            return true;
          break;
        case SkillEffectTargets.Self:
          if (!this.mIsInvSelfCond)
            return true;
          break;
      }
      return this.IsSatisfyCondition(target);
    }

    public bool IsApplyCond(List<Unit> target_list, SkillEffectTargets cond_target)
    {
      if (target_list == null)
        return false;
      bool flag = false;
      for (int index = 0; index < target_list.Count; ++index)
      {
        if (this.IsApplyCond(target_list[index], cond_target))
        {
          flag = true;
          break;
        }
      }
      return flag;
    }

    public static void Deserialize(
      ref List<DependStateSpcEffParam> list,
      JSON_DependStateSpcEffParam[] json)
    {
      if (json == null)
        return;
      if (list == null)
        list = new List<DependStateSpcEffParam>(json.Length);
      list.Clear();
      for (int index = 0; index < json.Length; ++index)
      {
        DependStateSpcEffParam stateSpcEffParam = new DependStateSpcEffParam();
        stateSpcEffParam.Deserialize(json[index]);
        list.Add(stateSpcEffParam);
      }
    }

    public enum Flags
    {
      IsAnd = 1,
      IsInvTargetBuff = 2,
      IsInvTargetCond = 4,
      IsInvSelfBuff = 8,
      IsInvSelfCond = 16, // 0x00000010
    }

    public enum CondBuffTypes
    {
      All,
      Buff,
      Debuff,
    }

    public class BuffInfo
    {
      public ParamTypes mParamType;
      public DependStateSpcEffParam.CondBuffTypes mCondBuffType;

      public BuffInfo(ParamTypes pt, DependStateSpcEffParam.CondBuffTypes cbt)
      {
        this.mParamType = pt;
        this.mCondBuffType = cbt;
      }
    }
  }
}
