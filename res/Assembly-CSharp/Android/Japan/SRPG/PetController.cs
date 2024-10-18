// Decompiled with JetBrains decompiler
// Type: SRPG.PetController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  public class PetController : AnimationPlayer
  {
    public const string PetPath = "Pets/";
    public const string PetAnimationPath = "CHM/";
    private const string ID_IDLE = "IDLE";
    private const string ID_RUN = "RUN";
    public string PetID;
    public GameObject Owner;
    private GameObject mTarget;
    private bool mLoading;
    private StateMachine<PetController> mStateMachine;
    private Vector3 mVelocity;
    private Vector3 mAcceleration;

    protected override void Start()
    {
      base.Start();
      this.StartCoroutine(this.AsyncSetup());
      this.mStateMachine = new StateMachine<PetController>(this);
    }

    public override bool IsLoading
    {
      get
      {
        if (!base.IsLoading)
          return this.mLoading;
        return true;
      }
    }

    [DebuggerHidden]
    private IEnumerator AsyncSetup()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new PetController.\u003CAsyncSetup\u003Ec__Iterator0() { \u0024this = this };
    }

    protected override void Update()
    {
      base.Update();
      this.mStateMachine.Update();
    }

    private class State : SRPG.State<PetController>
    {
    }

    private class State_Idle : PetController.State
    {
      public override void Begin(PetController self)
      {
        self.PlayAnimation("IDLE", true);
      }
    }

    private class State_Move : PetController.State
    {
    }
  }
}
