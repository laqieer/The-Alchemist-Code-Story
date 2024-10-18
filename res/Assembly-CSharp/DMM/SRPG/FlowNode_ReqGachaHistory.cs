// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqGachaHistory
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Network/GachaHistory", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_ReqGachaHistory : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      this.ExecRequest((WebAPI) new ReqGachaHistory(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      ((Behaviour) this).enabled = true;
    }

    private void Success()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(1);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        int errCode = (int) Network.ErrCode;
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_GachaHistory> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_GachaHistory>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        GachaHistoryWindow component = ((Component) this).GetComponent<GachaHistoryWindow>();
        if (Object.op_Inequality((Object) component, (Object) null))
          component.SetGachaHistoryData(jsonObject.body.log);
        this.Success();
      }
    }
  }
}
