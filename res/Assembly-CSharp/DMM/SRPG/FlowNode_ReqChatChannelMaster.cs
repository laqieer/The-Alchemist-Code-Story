﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqChatChannelMaster
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/ReqChat/ReqChatChannelMaster", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(2, "Failed", FlowNode.PinTypes.Output, 2)]
  public class FlowNode_ReqChatChannelMaster : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      this.ExecRequest((WebAPI) new ReqChatChannelMaster(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      ((Behaviour) this).enabled = true;
    }

    private void Success()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(1);
    }

    private void Failure()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(2);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        int errCode = (int) Network.ErrCode;
      }
      WebAPI.JSON_BodyResponse<JSON_ChatChannelMaster> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_ChatChannelMaster>>(www.text);
      DebugUtility.Assert(jsonObject != null, "res == null");
      Network.RemoveAPI();
      if (!MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body))
        this.Failure();
      else
        this.Success();
    }
  }
}