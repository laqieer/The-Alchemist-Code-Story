﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiPlayEnterLobby
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Multi/MultiPlayEnterLobby", 32741)]
  [FlowNode.Pin(0, "EnterLobby", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(2, "Failure", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(3, "EnterLobby(autoJoinlobby)", FlowNode.PinTypes.Input, 3)]
  public class FlowNode_MultiPlayEnterLobby : FlowNode
  {
    private StateMachine<FlowNode_MultiPlayEnterLobby> mStateMachine;
    public float TimeOutSec = 10f;
    public bool FlushRoomMsg = true;
    public bool DisconnectIfSendFailed = true;
    public bool SortRoomMsg = true;

    private bool IsEqual(string s0, string s1)
    {
      return string.IsNullOrEmpty(s0) ? string.IsNullOrEmpty(s1) : s0.Equals(s1);
    }

    public override void OnActivate(int pinID)
    {
      if (pinID != 0 && pinID != 3)
        return;
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      instance.TimeOutSec = this.TimeOutSec;
      instance.SendRoomMessageFlush = this.FlushRoomMsg;
      instance.DisconnectIfSendRoomMessageFailed = this.DisconnectIfSendFailed;
      instance.SortRoomMessage = this.SortRoomMsg;
      if (instance.CurrentState == MyPhoton.MyState.LOBBY)
      {
        DebugUtility.Log("already enter lobby");
        this.Success();
      }
      else
      {
        ((Behaviour) this).enabled = true;
        if (instance.CurrentState != MyPhoton.MyState.NOP)
          instance.Disconnect();
        this.mStateMachine = new StateMachine<FlowNode_MultiPlayEnterLobby>(this);
        if (pinID == 0)
          this.mStateMachine.GotoState<FlowNode_MultiPlayEnterLobby.State_ConnectLobby>();
        else
          this.mStateMachine.GotoState<FlowNode_MultiPlayEnterLobby.State_ConnectLobbyAuto>();
      }
    }

    private void Update()
    {
      if (this.mStateMachine == null)
        return;
      this.mStateMachine.Update();
    }

    private void Success()
    {
      DebugUtility.Log("Enter Lobby.");
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(1);
    }

    private void Failure()
    {
      DebugUtility.Log("Enter Lobby Failure.");
      ((Behaviour) this).enabled = false;
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      if (instance.CurrentState != MyPhoton.MyState.NOP)
        instance.Disconnect();
      this.ActivateOutputLinks(2);
    }

    public void GotoState<StateType>() where StateType : State<FlowNode_MultiPlayEnterLobby>, new()
    {
      if (this.mStateMachine == null)
        return;
      this.mStateMachine.GotoState<StateType>();
    }

    private class State_ConnectLobby : State<FlowNode_MultiPlayEnterLobby>
    {
      protected readonly int MAX_RETRY_CNT = 1;
      protected int ReqCnt;

      public override void Begin(FlowNode_MultiPlayEnterLobby self)
      {
        if (this.ReqConnect(self))
          return;
        self.Failure();
      }

      public override void Update(FlowNode_MultiPlayEnterLobby self)
      {
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        if (!((Behaviour) self).enabled)
          return;
        switch (instance.CurrentState)
        {
          case MyPhoton.MyState.CONNECTING:
            break;
          case MyPhoton.MyState.LOBBY:
            self.Success();
            break;
          default:
            if (!instance.IsDisconnected() || this.ReqConnect(self))
              break;
            self.Failure();
            break;
        }
      }

      public override void End(FlowNode_MultiPlayEnterLobby self)
      {
      }

      public bool ReqConnect(FlowNode_MultiPlayEnterLobby self, bool autoJoin = false)
      {
        if (this.ReqCnt++ > this.MAX_RETRY_CNT)
          return false;
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        DebugUtility.Log("start connect:" + GlobalVars.SelectedMultiPlayPhotonAppID);
        instance.ResetLastError();
        return !instance.IsDisconnected() || instance.StartConnect(GlobalVars.SelectedMultiPlayPhotonAppID, autoJoin);
      }
    }

    private class State_ConnectLobbyAuto : FlowNode_MultiPlayEnterLobby.State_ConnectLobby
    {
      public override void Begin(FlowNode_MultiPlayEnterLobby self)
      {
        if (this.ReqConnect(self, true))
          return;
        self.Failure();
      }

      public override void Update(FlowNode_MultiPlayEnterLobby self)
      {
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        if (!((Behaviour) self).enabled)
          return;
        switch (instance.CurrentState)
        {
          case MyPhoton.MyState.CONNECTING:
            break;
          case MyPhoton.MyState.LOBBY:
            if (!instance.IsRoomListUpdated)
              break;
            self.Success();
            break;
          default:
            if (!instance.IsDisconnected() || this.ReqConnect(self, true))
              break;
            self.Failure();
            break;
        }
      }
    }
  }
}
