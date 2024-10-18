﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_NewGameEmailDevice
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using Gsc.Auth;
using System;

namespace SRPG
{
  [FlowNode.Pin(0, "Chain New Account", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(11, "Failed", FlowNode.PinTypes.Output, 11)]
  [FlowNode.NodeType("System/NewGameEmailDevice", 32741)]
  [FlowNode.Pin(10, "Success", FlowNode.PinTypes.Output, 10)]
  public class FlowNode_NewGameEmailDevice : FlowNode
  {
    private const int PIN_INPUT = 0;
    private const int PIN_SUCCESS = 10;
    private const int PIN_FAILED = 11;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      Session.DefaultSession.AddDeviceWithEmailAddressAndPassword(FlowNode_NewGameRegister.gEmail, FlowNode_NewGameRegister.gPassword, new Action<AddDeviceWithEmailAddressAndPasswordResult>(this.AddUserResponse));
    }

    private void AddUserResponse(AddDeviceWithEmailAddressAndPasswordResult res)
    {
      switch (res.ResultCode)
      {
        case AddDeviceWithEmailAddressAndPasswordResultCode.Success:
          GameUtility.ClearPreferences();
          this.ActivateOutputLinks(10);
          break;
        default:
          this.ActivateOutputLinks(11);
          break;
      }
    }
  }
}
