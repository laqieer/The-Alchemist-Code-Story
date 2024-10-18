// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiUnitRank
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Multi/Ranking", 32741)]
  [FlowNode.Pin(100, "Request", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(200, "Finish", FlowNode.PinTypes.Output, 200)]
  [FlowNode.Pin(201, "Error", FlowNode.PinTypes.Output, 201)]
  public class FlowNode_MultiUnitRank : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 100)
        return;
      ((Behaviour) this).enabled = true;
      this.ExecRequest((WebAPI) new ReqMultiRank(GlobalVars.SelectedQuestID, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
    }

    private void Failure()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(201);
    }

    private void Success()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(200);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        int errCode = (int) Network.ErrCode;
        Network.RemoveAPI();
        this.Failure();
      }
      else
      {
        WebAPI.JSON_BodyResponse<ReqMultiRank.Json_MultiRank> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqMultiRank.Json_MultiRank>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res is null");
        Network.RemoveAPI();
        MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body);
        this.Success();
      }
    }
  }
}
