// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqColoReward
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;

#nullable disable
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
