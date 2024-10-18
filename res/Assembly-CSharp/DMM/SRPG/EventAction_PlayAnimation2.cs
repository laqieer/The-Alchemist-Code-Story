// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_PlayAnimation2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [EventActionInfo("New/アクター/アニメーション再生", "ユニットにアニメーションを再生させます。", 6702148, 11158596)]
  public class EventAction_PlayAnimation2 : EventAction
  {
    [StringIsActorList]
    public string ActorID;
    [HideInInspector]
    public string AnimationName;
    public EventAction_PlayAnimation2.AnimationTypes AnimationType;
    public float Delay;
    public float Interp = 0.1f;
    [HideInInspector]
    public bool Loop;
    public bool Async;
    [HideInInspector]
    public bool ApplyRootBoneAtEnd;
    private string mAnimationID;
    private TacticsUnitController mController;

    public override bool IsPreloadAssets => true;

    [DebuggerHidden]
    public override IEnumerator PreloadAssets()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new EventAction_PlayAnimation2.\u003CPreloadAssets\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    public override void OnActivate()
    {
      if (Object.op_Inequality((Object) this.mController, (Object) null))
      {
        if (!string.IsNullOrEmpty(this.mAnimationID) && this.AnimationType == EventAction_PlayAnimation2.AnimationTypes.Custom && !string.IsNullOrEmpty(this.AnimationName))
        {
          if ((double) this.Delay > 0.0 && this.Async)
          {
            this.ActivateNext(true);
            return;
          }
          if ((double) this.Delay <= 0.0)
          {
            this.mController.RootMotionMode = AnimationPlayer.RootMotionModes.Velocity;
            this.mController.PlayAnimation(this.mAnimationID, this.Loop, this.Interp);
          }
          if (!this.Async)
            return;
          this.ActivateNext();
          return;
        }
        if (this.AnimationType == EventAction_PlayAnimation2.AnimationTypes.Idle)
        {
          if ((double) this.Delay > 0.0 && this.Async)
          {
            this.ActivateNext(true);
            return;
          }
          if ((double) this.Delay <= 0.0)
            this.mController.PlayIdle();
          if (!this.Async)
            return;
          this.ActivateNext();
          return;
        }
      }
      this.ActivateNext();
    }

    public override void Update()
    {
      if ((double) this.Delay > 0.0)
      {
        this.Delay -= Time.deltaTime;
        if ((double) this.Delay > 0.0)
          return;
        if (!string.IsNullOrEmpty(this.mAnimationID) && this.AnimationType == EventAction_PlayAnimation2.AnimationTypes.Custom && !string.IsNullOrEmpty(this.AnimationName))
        {
          this.mController.RootMotionMode = AnimationPlayer.RootMotionModes.Velocity;
          this.mController.PlayAnimation(this.mAnimationID, this.Loop, this.Interp);
        }
        else
        {
          if (this.AnimationType != EventAction_PlayAnimation2.AnimationTypes.Idle)
            return;
          this.mController.PlayIdle();
          if (!this.Async)
            this.ActivateNext();
          else
            this.enabled = false;
        }
      }
      else
      {
        if ((double) this.mController.GetRemainingTime(this.mAnimationID) > 0.0)
          return;
        if (this.ApplyRootBoneAtEnd && !this.Loop)
        {
          this.mController.StopAnimation(this.mAnimationID);
          Transform childRecursively = GameUtility.findChildRecursively(((Component) this.mController).transform, this.mController.RootMotionBoneName);
          ((Component) this.mController).transform.position = childRecursively.position;
          childRecursively.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
          ((Component) this.mController).transform.rotation = Quaternion.op_Multiply(childRecursively.rotation, Quaternion.Euler(90f, 0.0f, 0.0f));
          childRecursively.localRotation = Quaternion.Euler(270f, 0.0f, 0.0f);
        }
        if (!this.Async)
          this.ActivateNext();
        else
          this.enabled = false;
      }
    }

    protected override void OnDestroy()
    {
      if (!Object.op_Inequality((Object) this.mController, (Object) null) || string.IsNullOrEmpty(this.mAnimationID))
        return;
      this.mController.UnloadAnimation(this.mAnimationID);
    }

    public enum AnimationTypes
    {
      Custom,
      Idle,
    }
  }
}
