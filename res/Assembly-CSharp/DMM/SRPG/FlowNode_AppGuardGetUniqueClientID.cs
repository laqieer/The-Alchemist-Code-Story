// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_AppGuardGetUniqueClientID
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("AppGuard/GetUniqueClientID", 32741)]
  [FlowNode.Pin(10, "Start", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(100, "Finish", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(20, "GameGuard初期化失敗（二重起動）", FlowNode.PinTypes.Output, 20)]
  [FlowNode.Pin(21, "GameGuard初期化失敗（汎用）", FlowNode.PinTypes.Output, 21)]
  [FlowNode.Pin(22, "GameGuard初期化失敗（接続や空き容量不足など）", FlowNode.PinTypes.Output, 22)]
  public class FlowNode_AppGuardGetUniqueClientID : FlowNode_Network
  {
    private const int PIN_INPUT_START = 10;
    private const int PIN_OUTPUT_FINISH = 100;
    private const int PIN_OUTPUT_GAMEGUARD_ALREADY_RUNNING = 20;
    private const int PIN_OUTPUT_GAMEGUARD_INIT_FAILED = 21;
    private const int PIN_OUTPUT_GAMEGUARD_ERROR_CONNECTION_SPACE = 22;

    public override void OnActivate(int pinID)
    {
      if (pinID != 10)
        return;
      if (AppGuardClient.GameGuardState != AppGuardClient.EGameGuardStatus.NPGAMEMON_SUCCESS)
      {
        AppGuardClient.SendErrorLog();
        switch (AppGuardClient.GameGuardState)
        {
          case AppGuardClient.EGameGuardStatus.CUSTOM_UNDEFINED:
          case AppGuardClient.EGameGuardStatus.NPGAMEMON_ERROR_NPGMUP:
          case AppGuardClient.EGameGuardStatus.NPGMUP_ERROR_DOWNCFG:
          case AppGuardClient.EGameGuardStatus.NPGMUP_ERROR_ABORT:
          case AppGuardClient.EGameGuardStatus.NPGMUP_ERROR_AUTH:
          case AppGuardClient.EGameGuardStatus.NPGMUP_ERROR_AUTH_INI:
          case AppGuardClient.EGameGuardStatus.NPGMUP_ERROR_CONNECT:
            this.ActivateOutputLinks(22);
            break;
          case AppGuardClient.EGameGuardStatus.NPGAMEMON_ERROR_GAME_EXIST:
            this.ActivateOutputLinks(20);
            break;
          default:
            this.ActivateOutputLinks(21);
            break;
        }
      }
      else if (MonoSingleton<GameManager>.Instance.UseAppGuardAuthentication)
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
