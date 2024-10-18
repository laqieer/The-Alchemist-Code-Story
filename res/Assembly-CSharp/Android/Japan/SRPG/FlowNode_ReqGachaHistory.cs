// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqGachaHistory
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;

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
        int errCode = (int) Network.ErrCode;
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_GachaHistory> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_GachaHistory>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        GachaHistoryWindow component = this.GetComponent<GachaHistoryWindow>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null)
          component.SetGachaHistoryData(jsonObject.body.log);
        this.Success();
      }
    }
  }
}
