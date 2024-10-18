﻿// Decompiled with JetBrains decompiler
// Type: SRPG.EventTrigger
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
  public class EventTrigger
  {
    private EEventTrigger mTrigger;
    private EEventType mEventType;
    private EEventGimmick mGimmickType;
    private string mStrValue;
    private int mIntValue;
    private int mCount;
    private string mTag;

    public EEventTrigger Trigger => this.mTrigger;

    public EEventType EventType => this.mEventType;

    public EEventGimmick GimmickType => this.mGimmickType;

    public string StrValue => this.mStrValue;

    public int IntValue => this.mIntValue;

    public int Count
    {
      get => this.mCount;
      set => this.mCount = value;
    }

    public string Tag => this.mTag;

    public bool IsTriggerWithdraw
    {
      get
      {
        switch (this.mTrigger)
        {
          case EEventTrigger.WdHpDownRate:
          case EEventTrigger.WdHpDownValue:
          case EEventTrigger.WdElapsedTurn:
          case EEventTrigger.WdStandbyGrid:
          case EEventTrigger.WdDeadUnit:
          case EEventTrigger.WdUsedSkill:
            return true;
          default:
            return false;
        }
      }
    }

    public void Setup(EventTrigger src)
    {
      this.mTrigger = src.Trigger;
      this.mEventType = src.EventType;
      this.mGimmickType = src.GimmickType;
      this.mStrValue = src.StrValue;
      this.mIntValue = src.IntValue;
      this.mCount = src.Count;
      this.mTag = src.mTag;
    }

    public void CopyTo(EventTrigger dsc)
    {
      dsc.mTrigger = this.mTrigger;
      dsc.mEventType = this.mEventType;
      dsc.mGimmickType = this.mGimmickType;
      dsc.mStrValue = this.mStrValue;
      dsc.mIntValue = this.mIntValue;
      dsc.mCount = this.mCount;
      dsc.mTag = this.mTag;
    }

    public bool Deserialize(JSON_EventTrigger json)
    {
      if (json == null)
        return false;
      this.mTrigger = (EEventTrigger) json.trg;
      this.mEventType = (EEventType) json.type;
      this.mGimmickType = (EEventGimmick) json.detail;
      this.mStrValue = json.sval;
      this.mIntValue = json.ival;
      this.mCount = json.cnt;
      this.mTag = json.tag;
      return true;
    }

    public BuffAttachment MakeBuff(Unit gimmick, Unit target)
    {
      BuffAttachment buffAttachment = new BuffAttachment();
      switch (this.GimmickType)
      {
        case EEventGimmick.AtkUp:
        case EEventGimmick.DefUp:
        case EEventGimmick.MagUp:
        case EEventGimmick.MndUp:
        case EEventGimmick.RecUp:
        case EEventGimmick.SpdUp:
        case EEventGimmick.CriUp:
        case EEventGimmick.LukUp:
        case EEventGimmick.MovUp:
          FixParam fixParam = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
          int gemsBuffValue = (int) fixParam.GemsBuffValue;
          int gemsBuffTurn = (int) fixParam.GemsBuffTurn;
          SkillParamCalcTypes skillParamCalcTypes = SkillParamCalcTypes.Scale;
          if (this.GimmickType == EEventGimmick.AtkUp)
            buffAttachment.status.param.atk = (OShort) gemsBuffValue;
          if (this.GimmickType == EEventGimmick.DefUp)
            buffAttachment.status.param.def = (OShort) gemsBuffValue;
          if (this.GimmickType == EEventGimmick.MagUp)
            buffAttachment.status.param.mag = (OShort) gemsBuffValue;
          if (this.GimmickType == EEventGimmick.MndUp)
            buffAttachment.status.param.mnd = (OShort) gemsBuffValue;
          if (this.GimmickType == EEventGimmick.RecUp)
            buffAttachment.status.param.rec = (OShort) gemsBuffValue;
          if (this.GimmickType == EEventGimmick.SpdUp)
            buffAttachment.status.param.spd = (OShort) gemsBuffValue;
          if (this.GimmickType == EEventGimmick.CriUp)
            buffAttachment.status.param.cri = (OShort) gemsBuffValue;
          if (this.GimmickType == EEventGimmick.LukUp)
            buffAttachment.status.param.luk = (OShort) gemsBuffValue;
          if (this.GimmickType == EEventGimmick.MovUp)
          {
            buffAttachment.status.param.mov = (OShort) 2;
            skillParamCalcTypes = SkillParamCalcTypes.Add;
          }
          buffAttachment.user = gimmick;
          buffAttachment.BuffType = BuffTypes.Buff;
          buffAttachment.CalcType = skillParamCalcTypes;
          buffAttachment.CheckTarget = target;
          buffAttachment.CheckTiming = EffectCheckTimings.ActionStart;
          buffAttachment.UseCondition = ESkillCondition.None;
          buffAttachment.IsPassive = (OBool) false;
          buffAttachment.turn = (OInt) gemsBuffTurn;
          break;
      }
      return buffAttachment;
    }

    public void ExecuteGimmickEffect(Unit gimmick, Unit target, LogMapEvent log = null)
    {
      switch (this.GimmickType)
      {
        case EEventGimmick.Heal:
          int num = 0;
          if (!target.IsUnitCondition(EUnitCondition.DisableHeal) && !target.IsUseBossLongHP())
          {
            int hp = (int) target.MaximumStatus.param.hp;
            int val = hp * this.IntValue / 100;
            num = Math.Min(target.CalcParamRecover(val), hp - (int) target.CurrentStatus.param.hp);
          }
          target.Heal(num);
          if (log == null)
            break;
          log.heal = num;
          break;
        case EEventGimmick.AtkUp:
        case EEventGimmick.DefUp:
        case EEventGimmick.MagUp:
        case EEventGimmick.MndUp:
        case EEventGimmick.RecUp:
        case EEventGimmick.SpdUp:
        case EEventGimmick.CriUp:
        case EEventGimmick.LukUp:
        case EEventGimmick.MovUp:
          BuffAttachment buff = this.MakeBuff(gimmick, target);
          target.SetBuffAttachment(buff);
          if (log == null)
            break;
          BattleCore.SetBuffBits(buff.status, ref log.buff, ref log.debuff);
          break;
      }
    }
  }
}
