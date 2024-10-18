// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_PlayAnimation4
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [EventActionInfo("New/アクター/アニメーション再生(複数)", "ユニットにアニメーションを再生させます。", 6702148, 11158596)]
  public class EventAction_PlayAnimation4 : EventAction
  {
    [StringIsActorList]
    public string ActorID;
    public bool Async;
    private const string MOVIE_PATH = "Movies/";
    private const string DEMO_PATH = "Demo/";
    [HideInInspector]
    public EventAction_PlayAnimation4.AnimationData[] AnimationDataArray = new EventAction_PlayAnimation4.AnimationData[1]
    {
      new EventAction_PlayAnimation4.AnimationData()
    };
    private bool foldout = true;
    private List<string> mAnimationIDList = new List<string>();
    private TacticsUnitController mController;
    private int idx;
    private float mDelay;
    private bool isPlay;

    public override bool IsPreloadAssets => true;

    [DebuggerHidden]
    public override IEnumerator PreloadAssets()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new EventAction_PlayAnimation4.\u003CPreloadAssets\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    public override void OnActivate()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mController, (UnityEngine.Object) null))
      {
        this.idx = 0;
        this.mDelay = this.AnimationDataArray[this.idx].Delay;
        if (!this.Async)
          return;
        this.ActivateNext(true);
      }
      else
        this.ActivateNext();
    }

    public override void Update()
    {
      if ((double) this.mDelay > 0.0)
        this.mDelay -= Time.deltaTime;
      else if (this.isPlay)
      {
        if ((double) this.mController.GetRemainingTime(this.mAnimationIDList[this.idx]) > 0.0)
          return;
        if (this.AnimationDataArray[this.idx].ApplyRootBoneAtEnd && !this.AnimationDataArray[this.idx].Loop)
        {
          this.mController.StopAnimation(this.mAnimationIDList[this.idx]);
          Transform childRecursively = GameUtility.findChildRecursively(((Component) this.mController).transform, this.mController.RootMotionBoneName);
          ((Component) this.mController).transform.position = childRecursively.position;
          childRecursively.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
          ((Component) this.mController).transform.rotation = Quaternion.op_Multiply(childRecursively.rotation, Quaternion.Euler(90f, 0.0f, 0.0f));
          childRecursively.localRotation = Quaternion.Euler(270f, 0.0f, 0.0f);
        }
        this.isPlay = false;
        ++this.idx;
        if (this.idx < this.AnimationDataArray.Length)
          this.mDelay = this.AnimationDataArray[this.idx].Delay;
        else if (this.Async)
          this.enabled = false;
        else
          this.ActivateNext();
      }
      else
      {
        this.isPlay = true;
        if (this.AnimationDataArray[this.idx].Type == EventAction_PlayAnimation4.AnimationTypes.Custom)
        {
          if (string.IsNullOrEmpty(this.mAnimationIDList[this.idx]))
            return;
          this.mController.RootMotionMode = AnimationPlayer.RootMotionModes.Velocity;
          this.mController.PlayAnimation(this.mAnimationIDList[this.idx], this.AnimationDataArray[this.idx].Loop, this.AnimationDataArray[this.idx].Interp);
        }
        else
        {
          this.mController.PlayIdle();
          if (this.Async)
            this.enabled = false;
          else
            this.ActivateNext();
        }
      }
    }

    protected override void OnDestroy()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mController, (UnityEngine.Object) null))
        return;
      for (int index = 0; index < this.mAnimationIDList.Count; ++index)
        this.mController.UnloadAnimation(this.mAnimationIDList[index]);
    }

    public enum AnimationTypes
    {
      Custom,
      Idle,
    }

    [Serializable]
    public class AnimationData
    {
      public EventAction_PlayAnimation4.AnimationTypes Type;
      public string Name;
      public float Delay;
      public float Interp = 0.1f;
      public bool ApplyRootBoneAtEnd;
      public bool Loop;
      public EventAction_PlayAnimation4.AnimationData.PREFIX_PATH Path;

      public enum PREFIX_PATH
      {
        Demo,
        Movie,
        Default,
      }
    }
  }
}
