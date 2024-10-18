﻿// Decompiled with JetBrains decompiler
// Type: SRPG.GachaUnitPreview
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Diagnostics;

namespace SRPG
{
  public class GachaUnitPreview : UnitController
  {
    public int DefaultLayer = GameUtility.LayerDefault;
    public const string ID_IDLE = "idle";
    public const string ID_ACTION = "action";
    public const string ID_BATTLE_CHANT = "B_CHA";
    public const string ID_BATTLE_SKILL = "B_SKL";
    public const string ID_BATTLE_BACKSTEP = "B_BS";
    public bool PlayAction;
    private bool mPlayingAction;
    public UnitData mGUnitData;
    private string mCurrentJobID;

    public UnitData GUnitData
    {
      get
      {
        return this.mGUnitData;
      }
      set
      {
        this.mGUnitData = value;
      }
    }

    protected override bool IsEventAllowed(AnimEvent e)
    {
      return false;
    }

    protected override void Start()
    {
      this.KeepUnitHidden = true;
      this.LoadEquipments = true;
      base.Start();
    }

    public void SetGachaUnitData(UnitData unit, string jobId)
    {
      this.mGUnitData = unit;
      this.mCurrentJobID = jobId;
    }

    protected override void PostSetup()
    {
      base.PostSetup();
      this.LoadUnitAnimationAsync("idle", "unit_info_idle0", true, false);
      this.LoadUnitAnimationAsync("action", "unit_info_act0", true, false);
      JobData jobData = Array.Find<JobData>(this.GUnitData.Jobs, (Predicate<JobData>) (p => p.JobID == this.mCurrentJobID));
      SkillSequence sequence = SkillSequence.FindSequence(jobData == null ? this.GUnitData.CurrentJob.GetAttackSkill().SkillParam.motion : jobData.GetAttackSkill().SkillParam.motion, false);
      if (sequence == null)
        return;
      if (sequence.SkillAnimation.Name.Length > 0)
        this.LoadUnitAnimationAsync("B_SKL", sequence.SkillAnimation.Name, false, false);
      this.StartCoroutine(this.LoadThread());
    }

    protected override void Update()
    {
      base.Update();
      if (this.IsLoading)
        return;
      if (this.PlayAction)
      {
        this.PlayAction = false;
        this.PlayAnimation("B_SKL", false);
        this.mPlayingAction = true;
      }
      else
      {
        if (!this.mPlayingAction || (double) this.GetRemainingTime("B_SKL") > 0.0)
          return;
        this.PlayAnimation("idle", true, 0.1f, 0.0f);
        this.mPlayingAction = false;
      }
    }

    [DebuggerHidden]
    private IEnumerator LoadThread()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GachaUnitPreview.\u003CLoadThread\u003Ec__Iterator0() { \u0024this = this };
    }
  }
}
