// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqColo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.NodeType("Network/btl_colo", 32741)]
  public class FlowNode_ReqColo : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0 || this.enabled)
        return;
      if (Network.Mode == Network.EConnectMode.Offline)
      {
        this.Success();
      }
      else
      {
        this.ExecRequest((WebAPI) new ReqBtlColo(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        this.enabled = true;
      }
    }

    private void Success()
    {
      this.ActivateOutputLinks(1);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        Network.EErrCode errCode = Network.ErrCode;
        this.OnRetry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_ArenaPlayers> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_ArenaPlayers>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.OnRetry();
        }
        else
        {
          this.enabled = false;
          GameManager instance = MonoSingleton<GameManager>.Instance;
          if (!instance.Deserialize(jsonObject.body))
          {
            this.OnFailed();
          }
          else
          {
            Network.RemoveAPI();
            instance.Player.UpdateArenaRankTrophyStates(-1, -1);
            this.Success();
          }
        }
      }
    }
  }
}
