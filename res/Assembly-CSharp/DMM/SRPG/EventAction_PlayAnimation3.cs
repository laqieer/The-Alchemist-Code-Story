// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_PlayAnimation3
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [EventActionInfo("New/アクター/アニメーション再生2", "ユニットにアニメーションを再生させます。", 6702148, 11158596)]
  public class EventAction_PlayAnimation3 : EventAction
  {
    [StringIsActorList]
    public string ActorID;
    [HideInInspector]
    public EventAction_PlayAnimation3.PREFIX_PATH Path;
    [HideInInspector]
    public string AnimationName;
    public EventAction_PlayAnimation3.AnimationTypes AnimationType;
    public float Delay;
    [HideInInspector]
    public bool Loop;
    public bool Async;
    private string mAnimationID;
    private TacticsUnitController mController;
    private const string MOVIE_PATH = "Movies/";
    private const string DEMO_PATH = "Demo/";

    public override bool IsPreloadAssets => true;

    [DebuggerHidden]
    public override IEnumerator PreloadAssets()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new EventAction_PlayAnimation3.\u003CPreloadAssets\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    public override void OnActivate()
    {
      if (Object.op_Inequality((Object) this.mController, (Object) null))
      {
        if (!string.IsNullOrEmpty(this.mAnimationID) && this.AnimationType == EventAction_PlayAnimation3.AnimationTypes.Custom && !string.IsNullOrEmpty(this.AnimationName))
        {
          if ((double) this.Delay > 0.0 && this.Async)
          {
            this.ActivateNext(true);
            return;
          }
          if ((double) this.Delay <= 0.0)
          {
            this.mController.RootMotionMode = AnimationPlayer.RootMotionModes.Velocity;
            this.mController.PlayAnimation(this.mAnimationID, this.Loop, 0.1f);
          }
          if (!this.Async)
            return;
          this.ActivateNext(true);
          return;
        }
        if (this.AnimationType == EventAction_PlayAnimation3.AnimationTypes.Idle)
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
        if (!string.IsNullOrEmpty(this.mAnimationID) && this.AnimationType == EventAction_PlayAnimation3.AnimationTypes.Custom && !string.IsNullOrEmpty(this.AnimationName))
        {
          this.mController.RootMotionMode = AnimationPlayer.RootMotionModes.Velocity;
          this.mController.PlayAnimation(this.mAnimationID, this.Loop, 0.1f);
        }
        else
        {
          if (this.AnimationType != EventAction_PlayAnimation3.AnimationTypes.Idle)
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

    public enum PREFIX_PATH
    {
      Demo,
      Movie,
      Default,
    }

    public enum AnimationTypes
    {
      Custom,
      Idle,
    }
  }
}
