﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqRemoveChatBlackList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.NodeType("System/ReqRemoveChatBlackList", 32741)]
  public class FlowNode_ReqRemoveChatBlackList : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      this.ExecRequest((WebAPI) new ReqRemoveChatBlackList(FlowNode_Variable.Get("SelectUserID"), new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      this.enabled = true;
    }

    private void Success()
    {
      this.enabled = false;
      this.ActivateOutputLinks(1);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        Network.EErrCode errCode = Network.ErrCode;
      }
      else
      {
        WebAPI.JSON_BodyResponse<JSON_ChatBlackListRes> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_ChatBlackListRes>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        if (jsonObject.body.is_success != (byte) 1)
          return;
        this.Success();
      }
    }
  }
}
