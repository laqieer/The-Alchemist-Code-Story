﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_NewGameEmailDevice
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using Gsc.Auth;
using System;

namespace SRPG
{
  [FlowNode.NodeType("System/NewGame/NewGameEmailDevice", 32741)]
  [FlowNode.Pin(0, "Chain New Account", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "Failed", FlowNode.PinTypes.Output, 11)]
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
