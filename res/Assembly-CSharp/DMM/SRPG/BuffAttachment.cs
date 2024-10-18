// Decompiled with JetBrains decompiler
// Type: SRPG.BuffAttachment
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  public class BuffAttachment
  {
    public Unit user;
    public Unit CheckTarget;
    private OInt mCheckTiming;
    private OInt mUseCondition;
    public OInt turn;
    public OBool IsPassive;
    public SkillData skill;
    public SkillEffectTargets skilltarget;
    private BuffEffect mBuffEffect;
    public BaseStatus status = new BaseStatus();
    private OInt mBuffType = (OInt) 0;
    public bool IsNegativeValueIsBuff;
    private OInt mCalcType = (OInt) 0;
    public int DuplicateCount;
    public bool IsCalculated;
    public uint LinkageID;
    public OInt UpBuffCount = (OInt) 0;
    public List<Unit> AagTargetLists = new List<Unit>();
    public List<BuffAttachment.ResistStatusBuff> ResistStatusBuffList;
    public bool IsPrevApply;

    public BuffAttachment()
    {
    }

    public BuffAttachment(BuffEffect buff_effect) => this.mBuffEffect = buff_effect;

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

    public BuffEffect BuffEffect => this.mBuffEffect;

    public BuffEffectParam Param
    {
      get => this.mBuffEffect != null ? this.mBuffEffect.param : (BuffEffectParam) null;
    }

    public BuffTypes BuffType
    {
      get => (BuffTypes) (int) this.mBuffType;
      set => this.mBuffType = (OInt) (int) value;
    }

    public SkillParamCalcTypes CalcType
    {
      get => (SkillParamCalcTypes) (int) this.mCalcType;
      set => this.mCalcType = (OInt) (int) value;
    }

    public void CopyTo(BuffAttachment dsc)
    {
      dsc.user = this.user;
      dsc.turn = this.turn;
      dsc.IsPassive = this.IsPassive;
      dsc.skill = this.skill;
      dsc.skilltarget = this.skilltarget;
      dsc.mBuffEffect = this.mBuffEffect;
      dsc.BuffType = this.BuffType;
      dsc.IsNegativeValueIsBuff = this.IsNegativeValueIsBuff;
      dsc.CalcType = this.CalcType;
      dsc.CheckTarget = this.CheckTarget;
      dsc.CheckTiming = this.CheckTiming;
      dsc.UseCondition = this.UseCondition;
      dsc.DuplicateCount = this.DuplicateCount;
      dsc.LinkageID = this.LinkageID;
      dsc.UpBuffCount = this.UpBuffCount;
      this.status.CopyTo(dsc.status);
    }

    public bool IsCalcLaterCondition
    {
      get
      {
        return this.UseCondition == ESkillCondition.Dying || this.UseCondition == ESkillCondition.JudgeHP;
      }
    }

    public void EntryResistStatusBuffList(int[] status_buffs)
    {
      if (status_buffs == null || status_buffs.Length != StatusParam.MAX_STATUS)
        return;
      if (this.ResistStatusBuffList == null)
        this.ResistStatusBuffList = new List<BuffAttachment.ResistStatusBuff>(StatusParam.MAX_STATUS);
      this.ResistStatusBuffList.Clear();
      for (int index = 0; index < status_buffs.Length; ++index)
      {
        StatusTypes st = (StatusTypes) index;
        switch (st)
        {
          case StatusTypes.Mp:
          case StatusTypes.MpIni:
            continue;
          default:
            if (status_buffs[index] != 0)
            {
              this.ResistStatusBuffList.Add(new BuffAttachment.ResistStatusBuff(st, status_buffs[index]));
              continue;
            }
            continue;
        }
      }
    }

    public static int[] GetResistStatusBuffList(List<BuffAttachment.ResistStatusBuff> rsb_list)
    {
      int[] resistStatusBuffList = new int[StatusParam.MAX_STATUS];
      for (int index = 0; index < resistStatusBuffList.Length; ++index)
        resistStatusBuffList[index] = 0;
      if (rsb_list != null)
      {
        for (int index = 0; index < rsb_list.Count; ++index)
        {
          BuffAttachment.ResistStatusBuff rsb = rsb_list[index];
          int mType = (int) rsb.mType;
          if (mType >= 0 && mType < StatusParam.MAX_STATUS)
            resistStatusBuffList[mType] = (int) rsb.mVal;
        }
      }
      return resistStatusBuffList;
    }

    [MessagePackObject(false)]
    public class ResistStatusBuff
    {
      [Key(0)]
      public StatusTypes mType;
      [Key(1)]
      public OInt mVal;

      [SerializationConstructor]
      public ResistStatusBuff(StatusTypes st, int val)
      {
        this.mType = st;
        this.mVal = (OInt) val;
      }
    }
  }
}
