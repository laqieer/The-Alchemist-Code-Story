// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqUpdateSelectAward
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/Award/ReqUpdateSelectAward", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "Failure", FlowNode.PinTypes.Output, 11)]
  public class FlowNode_ReqUpdateSelectAward : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      string iname = FlowNode_Variable.Get("CONFIRM_SELECT_AWARD");
      if (MonoSingleton<GameManager>.GetInstanceDirect().Player.SelectedAward != iname)
      {
        this.ExecRequest((WebAPI) new ReqUpdateSelectAward(iname, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        ((Behaviour) this).enabled = true;
      }
      else
        this.Success();
    }

    private void Success()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(10);
    }

    private void Failure() => ((Behaviour) this).enabled = false;

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        int errCode = (int) Network.ErrCode;
      }
      WebAPI.JSON_BodyResponse<Json_ResSelectAward> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_ResSelectAward>>(www.text);
      DebugUtility.Assert(jsonObject != null, "res == null");
      Network.RemoveAPI();
      if (jsonObject == null)
      {
        this.Failure();
      }
      else
      {
        MonoSingleton<GameManager>.Instance.Player.SelectedAward = jsonObject.body.selected_award;
        this.Success();
      }
    }
  }
}
