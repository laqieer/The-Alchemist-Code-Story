// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqConvertPiece
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/Shop/ReqConvertPiece", 32741)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "Success", FlowNode.PinTypes.Output, 100)]
  public class FlowNode_ReqConvertPiece : FlowNode_Network
  {
    private const int PIN_IN_REQUEST = 1;
    private const int PIN_IN_SUCCESS = 100;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1 || !(FlowNode_ButtonEvent.currentValue is SerializeValueList currentValue))
        return;
      this.ExecRequest((WebAPI) new ReqConvertPiece(currentValue.GetString("consume"), currentValue.GetString("unit"), currentValue.GetInt("piecenum"), new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
    }

    public override void OnSuccess(WWWResult www)
    {
      MonoSingleton<GameManager>.Instance.Deserialize(JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqConvertPiece.Json_Response>>(www.text).body.items);
      Network.RemoveAPI();
      this.ActivateOutputLinks(100);
    }

    private class Json_Response
    {
      public Json_Item[] items;
    }
  }
}
