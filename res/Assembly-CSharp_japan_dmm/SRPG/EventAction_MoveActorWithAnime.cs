﻿// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_MoveActorWithAnime
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [EventActionInfo("New/アクター/移動(アニメーション再生付)", "アクターを指定パスに沿って移動させます。", 6702148, 11158596)]
  public class EventAction_MoveActorWithAnime : EventAction_MoveActor2
  {
    [HideInInspector]
    public string m_AnimationName;
    [HideInInspector]
    public bool m_Loop;
    public EventAction_MoveActorWithAnime.AnimeType m_AnimeType;
    private string m_AnimationID;

    public override bool IsPreloadAssets => true;

    [DebuggerHidden]
    public override IEnumerator PreloadAssets()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new EventAction_MoveActorWithAnime.\u003CPreloadAssets\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    public override void Update()
    {
      Debug.Log((object) "mawa.update()");
      if (!this.mReady)
      {
        if (Object.op_Inequality((Object) this.mController, (Object) null) && this.mController.IsLoading)
          return;
        if (this.Async)
          this.ActivateNext(true);
        this.mReady = true;
      }
      if (!this.mMoving)
      {
        if (this.mController.IsLoading)
          return;
        if ((double) this.Delay > 0.0)
        {
          this.Delay -= Time.deltaTime;
        }
        else
        {
          this.StartMove();
          this.mMoving = true;
        }
      }
      else
      {
        if (this.LockRotation || this.LockMotion || !this.GroundSnap)
        {
          if (this.UpdateMove())
            return;
        }
        else if (this.mController.isMoving)
          return;
        if (this.GotoRealPosition)
          this.mController.AutoUpdateRotation = true;
        if (this.m_AnimeType == EventAction_MoveActorWithAnime.AnimeType.Custom && !string.IsNullOrEmpty(this.m_AnimationName))
        {
          this.mController.RootMotionMode = AnimationPlayer.RootMotionModes.Velocity;
          this.mController.PlayAnimation(this.m_AnimationID, this.m_Loop, 0.1f);
        }
        else if (this.m_AnimeType == EventAction_MoveActorWithAnime.AnimeType.Idel)
          this.mController.PlayIdle();
        if (!this.Async)
        {
          this.ActivateNext();
          this.mController.SetRunningSpeed(this.BackupRunSpeed);
          this.mController.CollideGround = this.mActorCollideGround;
        }
        else
        {
          this.enabled = false;
          this.mController.SetRunningSpeed(this.BackupRunSpeed);
          this.mController.CollideGround = this.mActorCollideGround;
        }
      }
    }

    protected override void OnDestroy()
    {
      if (!Object.op_Inequality((Object) this.mController, (Object) null) || string.IsNullOrEmpty(this.m_AnimationID))
        return;
      this.mController.UnloadAnimation(this.m_AnimationID);
    }

    public enum AnimeType
    {
      Custom,
      Idel,
    }
  }
}
