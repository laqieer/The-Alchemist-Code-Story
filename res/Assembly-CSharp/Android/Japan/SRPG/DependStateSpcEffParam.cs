// Decompiled with JetBrains decompiler
// Type: SRPG.DependStateSpcEffParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace SRPG
{
  public class DependStateSpcEffParam
  {
    private string mIname;
    private DependStateSpcEffParam.Flags mFlags;
    private List<ParamTypes> mBuffList;
    private List<EUnitCondition> mCondList;
    private int mInvSaRate;

    private bool mIsAnd
    {
      get
      {
        return (this.mFlags & DependStateSpcEffParam.Flags.IsAnd) != (DependStateSpcEffParam.Flags) 0;
      }
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

    public string Iname
    {
      get
      {
        return this.mIname;
      }
    }

    public int InvSaRate
    {
      get
      {
        return this.mInvSaRate;
      }
    }

    public void Deserialize(JSON_DependStateSpcEffParam json)
    {
      if (json == null)
        return;
      this.mIname = json.iname;
      this.mBuffList = (List<ParamTypes>) null;
      if (json.buff_ids != null)
      {
        this.mBuffList = new List<ParamTypes>(json.buff_ids.Length);
        for (int index = 0; index < json.buff_ids.Length; ++index)
          this.mBuffList.Add((ParamTypes) json.buff_ids[index]);
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

    private bool IsExistBuff(BuffAttachment ba, ParamTypes pt)
    {
      if (ba == null || ba.Param == null || (ba.Param.buffs == null || pt == ParamTypes.None))
        return false;
      for (int index = 0; index < ba.Param.buffs.Length; ++index)
      {
        BuffEffectParam.Buff buff = ba.Param.buffs[index];
        if (buff != null && buff.type == pt)
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

    public static void Deserialize(ref List<DependStateSpcEffParam> list, JSON_DependStateSpcEffParam[] json)
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
  }
}
