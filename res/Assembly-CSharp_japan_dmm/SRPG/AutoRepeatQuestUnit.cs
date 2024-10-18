// Decompiled with JetBrains decompiler
// Type: SRPG.AutoRepeatQuestUnit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class AutoRepeatQuestUnit : UnitController
  {
    private const string ID_IDLE = "idle";
    private const string ID_ACTION = "action";
    private const string ID_RUN = "RUN";
    private string mCurrentAnimId;
    private float mElapsedTime;
    private Projector mShadow;
    private bool mIsDispShadow;
    private AutoRepeatQuestUnit.UnitAnimationParam mParam;
    private AutoRepeatQuestUnit.eState mState;

    protected override bool IsEventAllowed(AnimEvent e) => false;

    protected override void Start()
    {
      this.KeepUnitHidden = true;
      this.LoadEquipments = true;
      base.Start();
    }

    public void SetUnitData(
      AutoRepeatQuestUnit.UnitAnimationParam param,
      bool is_finished,
      bool is_disp_shadow)
    {
      this.mParam = param;
      this.mState = !is_finished ? AutoRepeatQuestUnit.eState.Running : AutoRepeatQuestUnit.eState.Finished;
      this.mIsDispShadow = is_disp_shadow;
      switch (this.mState)
      {
        case AutoRepeatQuestUnit.eState.Running:
          this.Setup_Running();
          break;
        case AutoRepeatQuestUnit.eState.Finished:
          this.Setup_Finished();
          break;
      }
    }

    private void CreateShadow()
    {
      if (this.mUnitObjectLists.Count > 1)
        return;
      CharacterSettings component = this.UnitObject.GetComponent<CharacterSettings>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component.ShadowProjector, (UnityEngine.Object) null))
        return;
      GameObject gameObject = ((Component) component.ShadowProjector).gameObject;
      this.mShadow = UnityEngine.Object.Instantiate<GameObject>(gameObject, Vector3.op_Addition(((Component) this).transform.position, gameObject.transform.position), gameObject.transform.rotation).GetComponent<Projector>();
      ((Component) this.mShadow).transform.SetParent(this.GetCharacterRoot(), true);
      this.mShadow.ignoreLayers = ~(1 << LayerMask.NameToLayer("BG"));
      GameUtility.SetLayer((Component) this.mShadow, GameUtility.LayerHidden, true);
    }

    protected override void PostSetup()
    {
      base.PostSetup();
      if (this.mIsDispShadow)
        this.CreateShadow();
      switch (this.mState)
      {
        case AutoRepeatQuestUnit.eState.Running:
          this.LoadAnimation_Running();
          break;
        case AutoRepeatQuestUnit.eState.Finished:
          this.LoadAnimation_Finished();
          break;
      }
      this.StartCoroutine(this.LoadThread());
    }

    [DebuggerHidden]
    private IEnumerator LoadThread()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new AutoRepeatQuestUnit.\u003CLoadThread\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    protected override void Update()
    {
      base.Update();
      switch (this.mState)
      {
        case AutoRepeatQuestUnit.eState.Running:
          this.Update_StateRunning();
          break;
        case AutoRepeatQuestUnit.eState.Finished:
          this.Update_StateFinished();
          break;
      }
    }

    private void Update_StateRunning()
    {
    }

    private void Update_StateFinished()
    {
      if (this.mCurrentAnimId == "idle")
      {
        this.mElapsedTime += Time.deltaTime;
        if ((double) this.mElapsedTime >= (double) this.mParam.IdleTime)
        {
          this.PlayUnitAnimation("action", false);
          this.mElapsedTime = 0.0f;
          return;
        }
      }
      if (!(this.mCurrentAnimId == "action") || (double) this.GetNormalizedTime(this.mCurrentAnimId) < 1.0)
        return;
      this.PlayUnitAnimation("idle", true);
    }

    private void PlayUnitAnimation(string anim_id, bool loop, float startTime = 0.0f)
    {
      float speed = 0.0f;
      switch (anim_id)
      {
        case "RUN":
          speed = this.mParam.RunningSpeed;
          break;
        case "idle":
          speed = this.mParam.IdleSpeed;
          break;
        case "action":
          speed = this.mParam.ActionSpeed;
          break;
      }
      this.PlayAnimation(anim_id, loop, 0.0f, startTime);
      this.SetSpeed(anim_id, speed);
      this.mCurrentAnimId = anim_id;
    }

    private void Setup_Running()
    {
      ((Component) this).transform.SetParent(this.mParam.RunningPos, false);
    }

    private void Setup_Finished()
    {
      ((Component) this).transform.SetParent(this.mParam.FinishedPos, false);
    }

    private void LoadAnimation_Running()
    {
      this.LoadUnitAnimationAsync("RUN", TacticsUnitController.ANIM_RUN_FIELD, true);
    }

    private void LoadAnimation_Finished()
    {
      this.LoadUnitAnimationAsync("idle", "unit_info_idle0", true);
      this.LoadUnitAnimationAsync("action", "unit_info_act0", true);
    }

    public enum eState
    {
      None,
      Running,
      Finished,
    }

    [Serializable]
    public class UnitAnimationParam
    {
      [SerializeField]
      private Transform mRunningPos;
      [SerializeField]
      private Transform mFinishedPos;
      [SerializeField]
      private float mRunningSpeed;
      [SerializeField]
      private float mIdleSpeed;
      [SerializeField]
      private float mActionSpeed;
      [SerializeField]
      private float mIdleTime;
      [SerializeField]
      private float mRunAnimStartTime;

      public Transform RunningPos => this.mRunningPos;

      public Transform FinishedPos => this.mFinishedPos;

      public float RunningSpeed => this.mRunningSpeed;

      public float IdleSpeed => this.mIdleSpeed;

      public float ActionSpeed => this.mActionSpeed;

      public float IdleTime => this.mIdleTime;

      public float RunAnimStartTime => this.mRunAnimStartTime;
    }
  }
}
