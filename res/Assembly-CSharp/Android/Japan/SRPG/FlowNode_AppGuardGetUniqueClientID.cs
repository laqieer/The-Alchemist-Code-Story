// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_AppGuardGetUniqueClientID
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.NodeType("AppGuard/GetUniqueClientID", 32741)]
  [FlowNode.Pin(10, "Start", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(100, "Finish", FlowNode.PinTypes.Output, 100)]
  public class FlowNode_AppGuardGetUniqueClientID : FlowNode_Network
  {
    private const int PIN_INPUT_START = 10;
    private const int PIN_OUTPUT_FINISH = 100;

    public override void OnActivate(int pinID)
    {
      if (pinID != 10)
        return;
      if (MonoSingleton<GameManager>.Instance.UseAppGuardAuthentication)
        this.ExecRequest((WebAPI) new ReqAppGuardUniqueClientID(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      else
        this.ActivateOutputLinks(100);
    }

    public override void OnSuccess(WWWResult www)
    {
      WebAPI.JSON_BodyResponse<FlowNode_AppGuardGetUniqueClientID.Json_AppGuardClientID> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_AppGuardGetUniqueClientID.Json_AppGuardClientID>>(www.text);
      Network.RemoveAPI();
      string uniqueClientId = jsonObject.body.unique_client_id;
      MonoSingleton<GameManager>.Instance.AppGuardUniqueClientID = !(uniqueClientId == "null") ? uniqueClientId : (string) null;
      AppGuardClient.SetUniqueClientID(MonoSingleton<GameManager>.Instance.AppGuardUniqueClientID);
      this.ActivateOutputLinks(100);
    }

    private class Json_AppGuardClientID
    {
      public string unique_client_id;
    }
  }
}
