// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqColoEnemy
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.NodeType("Network/btl_colo_enemy", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_ReqColoEnemy : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      this.ExecRequest((WebAPI) new ReqBtlColoEnemies(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
    }

    public override void OnSuccess(WWWResult www)
    {
      WebAPI.JSON_BodyResponse<Json_ArenaEnemies> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_ArenaEnemies>>(www.text);
      DebugUtility.Assert(jsonObject != null, "res == null");
      if (jsonObject.body == null)
      {
        this.OnRetry();
      }
      else
      {
        GameManager instance = MonoSingleton<GameManager>.Instance;
        if (!instance.Deserialize(jsonObject.body))
        {
          this.OnFailed();
        }
        else
        {
          Network.RemoveAPI();
          instance.Player.UpdateArenaRankTrophyStates(-1, -1);
          this.ActivateOutputLinks(1);
        }
      }
    }
  }
}
