// Decompiled with JetBrains decompiler
// Type: SRPG.StateMachine`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class StateMachine<T>
  {
    private T mOwner;
    private SRPG.State<T> mState;

    public StateMachine(T owner) => this.mOwner = owner;

    public SRPG.State<T> State => this.mState;

    public string StateName => this.mState != null ? this.mState.GetType().Name : "NULL";

    public System.Type CurrentState => this.mState != null ? this.mState.GetType() : (System.Type) null;

    public void Update()
    {
      if (this.mState == null)
        return;
      this.mState.Update(this.mOwner);
    }

    public void GotoState(System.Type stateType)
    {
      if (this.mState != null)
        this.mState.End(this.mOwner);
      this.mState = (SRPG.State<T>) Activator.CreateInstance(stateType);
      this.mState.self = this.mOwner;
      this.mState.Begin(this.mOwner);
    }

    public void GotoState<StateType>() where StateType : SRPG.State<T>, new()
    {
      if (this.mState != null)
        this.mState.End(this.mOwner);
      this.mState = (SRPG.State<T>) new StateType();
      this.mState.self = this.mOwner;
      this.mState.Begin(this.mOwner);
    }

    public bool IsInState<StateType>() where StateType : SRPG.State<T>
    {
      return this.mState != null && (object) this.mState.GetType() == (object) typeof (StateType);
    }

    public bool IsInKindOfState<StateType>() where StateType : SRPG.State<T>
    {
      return this.mState != null && this.mState is StateType;
    }

    public void Command(string cmd)
    {
      if (this.mState == null)
        return;
      this.mState.Command(this.mOwner, cmd);
    }
  }
}
