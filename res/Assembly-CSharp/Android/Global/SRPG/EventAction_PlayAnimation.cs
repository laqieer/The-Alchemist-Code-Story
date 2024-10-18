﻿// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_PlayAnimation
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

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

    public override bool IsPreloadAssets
    {
      get
      {
        return true;
      }
    }

    [DebuggerHidden]
    public override IEnumerator PreloadAssets()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new EventAction_PlayAnimation.\u003CPreloadAssets\u003Ec__IteratorA6() { \u003C\u003Ef__this = this };
    }

    public override void OnActivate()
    {
      if ((UnityEngine.Object) this.mController != (UnityEngine.Object) null)
      {
        if (!string.IsNullOrEmpty(this.mAnimationID) && this.AnimationType == EventAction_PlayAnimation.AnimationTypes.Custom && !string.IsNullOrEmpty(this.AnimationName))
        {
          this.mController.RootMotionMode = AnimationPlayer.RootMotionModes.Velocity;
          this.mController.PlayAnimation(this.mAnimationID, this.Loop, 0.1f, 0.0f);
          if (!this.Async)
            return;
          this.ActivateNext();
          return;
        }
        if (this.AnimationType == EventAction_PlayAnimation.AnimationTypes.Idle)
          this.mController.PlayIdle(0.0f);
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
      if (!((UnityEngine.Object) this.mController != (UnityEngine.Object) null) || string.IsNullOrEmpty(this.mAnimationID))
        return;
      this.mController.UnloadAnimation(this.mAnimationID);
    }

    public override void GoToEndState()
    {
      if (!((UnityEngine.Object) this.mController != (UnityEngine.Object) null))
        return;
      if (!string.IsNullOrEmpty(this.mAnimationID) && this.AnimationType == EventAction_PlayAnimation.AnimationTypes.Custom && !string.IsNullOrEmpty(this.AnimationName))
      {
        this.mController.PlayAnimation(this.mAnimationID, this.Loop, -0.1f, 0.0f);
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
