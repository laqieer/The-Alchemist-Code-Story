// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqQuestParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System;

namespace SRPG
{
  [FlowNode.Pin(2, "Failed", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("System/ReqQuestParam", 32741)]
  public class FlowNode_ReqQuestParam : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (Network.Mode == Network.EConnectMode.Online && !GameUtility.Config_UseAssetBundles.Value && !GameUtility.Config_UseLocalData.Value)
      {
        this.ExecRequest((WebAPI) new ReqQuestParam(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        this.enabled = true;
      }
      else
        this.Success();
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
      if (Network.IsError)
      {
        Network.EErrCode errCode = Network.ErrCode;
        this.OnFailed();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_QuestList> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_QuestList>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        try
        {
          MonoSingleton<GameManager>.Instance.Deserialize(GameUtility.Config_Language, jsonObject.body);
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          this.Failure();
          return;
        }
        this.Success();
      }
    }
  }
}
