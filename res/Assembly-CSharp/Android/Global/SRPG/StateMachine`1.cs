﻿// Decompiled with JetBrains decompiler
// Type: SRPG.StateMachine`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class StateMachine<T>
  {
    private T mOwner;
    private State<T> mState;

    public StateMachine(T owner)
    {
      this.mOwner = owner;
    }

    public string StateName
    {
      get
      {
        if (this.mState != null)
          return this.mState.GetType().Name;
        return "NULL";
      }
    }

    public System.Type CurrentState
    {
      get
      {
        if (this.mState != null)
          return this.mState.GetType();
        return (System.Type) null;
      }
    }

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
      this.mState = (State<T>) Activator.CreateInstance(stateType);
      this.mState.self = this.mOwner;
      this.mState.Begin(this.mOwner);
    }

    public void GotoState<StateType>() where StateType : State<T>, new()
    {
      if (this.mState != null)
        this.mState.End(this.mOwner);
      this.mState = (State<T>) Activator.CreateInstance<StateType>();
      this.mState.self = this.mOwner;
      this.mState.Begin(this.mOwner);
    }

    public bool IsInState<StateType>() where StateType : State<T>
    {
      if (this.mState != null)
        return (object) this.mState.GetType() == (object) typeof (StateType);
      return false;
    }

    public bool IsInKindOfState<StateType>() where StateType : State<T>
    {
      if (this.mState != null)
        return this.mState is StateType;
      return false;
    }

    public void Command(string cmd)
    {
      if (this.mState == null)
        return;
      this.mState.Command(this.mOwner, cmd);
    }
  }
}
