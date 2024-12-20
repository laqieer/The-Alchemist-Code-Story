﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqChatChannelMaster
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.Pin(2, "Failed", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("System/ReqChatChannelMaster", 32741)]
  public class FlowNode_ReqChatChannelMaster : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      this.ExecRequest((WebAPI) new ReqChatChannelMaster(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      this.enabled = true;
    }

    private void Success()
    {
      this.enabled = false;
      this.ActivateOutputLinks(1);
    }

    private void Failure()
    {
      this.enabled = false;
      this.ActivateOutputLinks(2);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        Network.EErrCode errCode = Network.ErrCode;
      }
      WebAPI.JSON_BodyResponse<JSON_ChatChannelMaster> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_ChatChannelMaster>>(www.text);
      DebugUtility.Assert(jsonObject != null, "res == null");
      Network.RemoveAPI();
      if (!MonoSingleton<GameManager>.Instance.Deserialize(GameUtility.Config_Language, jsonObject.body))
        this.Failure();
      else
        this.Success();
    }
  }
}
