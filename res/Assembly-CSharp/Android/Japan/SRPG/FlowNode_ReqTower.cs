// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqTower
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.NodeType("Tower/Req/ReqTower", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 10)]
  public class FlowNode_ReqTower : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (Network.Mode == Network.EConnectMode.Online)
      {
        this.ExecRequest((WebAPI) new ReqTower(GlobalVars.SelectedTowerID, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        this.enabled = true;
      }
      else
      {
        GlobalVars.SelectedTowerID = "QE_TW_BABEL";
        this.Success();
      }
    }

    private void Success()
    {
      this.enabled = false;
      this.ActivateOutputLinks(1);
    }

    private void Failure()
    {
      this.enabled = false;
      this.ActivateOutputLinks(2);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (TowerErrorHandle.Error((FlowNode_Network) this))
        return;
      WebAPI.JSON_BodyResponse<JSON_ReqTowerResuponse> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_ReqTowerResuponse>>(www.text);
      DebugUtility.Assert(jsonObject != null, "res == null");
      Network.RemoveAPI();
      this.Deserialize(jsonObject.body);
      this.Success();
    }

    private void Deserialize(JSON_ReqTowerResuponse json)
    {
      if (json == null)
        return;
      MonoSingleton<GameManager>.Instance.Deserialize(json);
    }
  }
}
