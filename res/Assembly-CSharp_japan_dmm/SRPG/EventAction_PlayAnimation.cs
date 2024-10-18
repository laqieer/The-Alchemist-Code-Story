﻿// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_PlayAnimation
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [EventActionInfo("アクター/アニメーション再生", "ユニットにアニメーションを再生させます。", 6702148, 11158596)]
  public class EventAction_PlayAnimation : EventAction
  {
    [StringIsActorID]
    public string ActorID;
    [HideInInspector]
    public string AnimationName;
    public EventAction_PlayAnimation.AnimationTypes AnimationType;
    [HideInInspector]
    public bool Loop;
    public bool Async;
    private string mAnimationID;
    private TacticsUnitController mController;

    public override bool IsPreloadAssets => true;

    [DebuggerHidden]
    public override IEnumerator PreloadAssets()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new EventAction_PlayAnimation.\u003CPreloadAssets\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    public override void OnActivate()
    {
      if (Object.op_Inequality((Object) this.mController, (Object) null))
      {
        if (!string.IsNullOrEmpty(this.mAnimationID) && this.AnimationType == EventAction_PlayAnimation.AnimationTypes.Custom && !string.IsNullOrEmpty(this.AnimationName))
        {
          this.mController.RootMotionMode = AnimationPlayer.RootMotionModes.Velocity;
          this.mController.PlayAnimation(this.mAnimationID, this.Loop, 0.1f);
          if (!this.Async)
            return;
          this.ActivateNext();
          return;
        }
        if (this.AnimationType == EventAction_PlayAnimation.AnimationTypes.Idle)
          this.mController.PlayIdle();
      }
      this.ActivateNext();
    }

    public override void Update()
    {
      if ((double) this.mController.GetRemainingTime(this.mAnimationID) > 0.0)
        return;
      this.ActivateNext();
    }

    protected override void OnDestroy()
    {
      if (!Object.op_Inequality((Object) this.mController, (Object) null) || string.IsNullOrEmpty(this.mAnimationID))
        return;
      this.mController.UnloadAnimation(this.mAnimationID);
    }

    public override void GoToEndState()
    {
      if (!Object.op_Inequality((Object) this.mController, (Object) null))
        return;
      if (!string.IsNullOrEmpty(this.mAnimationID) && this.AnimationType == EventAction_PlayAnimation.AnimationTypes.Custom && !string.IsNullOrEmpty(this.AnimationName))
      {
        this.mController.PlayAnimation(this.mAnimationID, this.Loop, -0.1f);
        this.mController.SkipToAnimationEnd();
      }
      else
      {
        if (this.AnimationType != EventAction_PlayAnimation.AnimationTypes.Idle)
          return;
        this.mController.PlayIdle(-1f);
      }
    }

    public enum AnimationTypes
    {
      Custom,
      Idle,
    }
  }
}
