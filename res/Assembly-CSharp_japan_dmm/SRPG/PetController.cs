// Decompiled with JetBrains decompiler
// Type: SRPG.PetController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

#nullable disable
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

    public override bool IsLoading => base.IsLoading || this.mLoading;

    [DebuggerHidden]
    private IEnumerator AsyncSetup()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new PetController.\u003CAsyncSetup\u003Ec__Iterator0()
      {
        \u0024this = this
      };
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
      public override void Begin(PetController self) => self.PlayAnimation("IDLE", true);
    }

    private class State_Move : PetController.State
    {
    }
  }
}
