// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqColoReward
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.NodeType("Network/btl_colo_reward", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "Success", FlowNode.PinTypes.Output, 10)]
  public class FlowNode_ReqColoReward : FlowNode_Network
  {
    private const int PIN_IN_REQUEST = 0;
    private const int PIN_OUT_SUCCESS = 10;
    private const int PIN_OUT_DONOTHING = 100;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      this.ExecRequest((WebAPI) new ReqBtlColoReward(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
    }

    public override void OnSuccess(WWWResult www)
    {
      WebAPI.JSON_BodyResponse<Json_ArenaAward> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_ArenaAward>>(www.text);
      DebugUtility.Assert(jsonObject != null, "res == null");
      GlobalVars.ArenaAward = jsonObject.body;
      Network.RemoveAPI();
      this.ActivateOutputLinks(10);
    }
  }
}
