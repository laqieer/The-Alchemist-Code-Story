// Decompiled with JetBrains decompiler
// Type: SRPG.StateMachine`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class StateMachine<T>
  {
    private T mOwner;
    private SRPG.State<T> mState;

    public StateMachine(T owner)
    {
      this.mOwner = owner;
    }

    public SRPG.State<T> State
    {
      get
      {
        return this.mState;
      }
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

    public Type CurrentState
    {
      get
      {
        if (this.mState != null)
          return this.mState.GetType();
        return (Type) null;
      }
    }

    public void Update()
    {
      if (this.mState == null)
        return;
      this.mState.Update(this.mOwner);
    }

    public void GotoState(Type stateType)
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
      this.mState = (SRPG.State<T>) Activator.CreateInstance<StateType>();
      this.mState.self = this.mOwner;
      this.mState.Begin(this.mOwner);
    }

    public bool IsInState<StateType>() where StateType : SRPG.State<T>
    {
      if (this.mState != null)
        return this.mState.GetType() == typeof (StateType);
      return false;
    }

    public bool IsInKindOfState<StateType>() where StateType : SRPG.State<T>
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
