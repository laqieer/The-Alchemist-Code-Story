// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqMasterParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(2, "Failed", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("System/ReqMasterParam", 32741)]
  public class FlowNode_ReqMasterParam : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (GameUtility.Config_UseLocalData.Value)
      {
        MonoSingleton<GameManager>.Instance.ReloadMasterDataWithLanguage(GameUtility.Config_Language, (string) null, (string) null);
        this.Success();
      }
      else if (Network.Mode == Network.EConnectMode.Online && !GameUtility.Config_UseAssetBundles.Value)
      {
        this.ExecRequest((WebAPI) new ReqMasterParam(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
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
        WebAPI.JSON_BodyResponse<JSON_MasterParam> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_MasterParam>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        if (!MonoSingleton<GameManager>.Instance.Deserialize(GameUtility.Config_Language, jsonObject.body))
        {
          this.Failure();
        }
        else
        {
          MonoSingleton<GameManager>.Instance.MasterParam.DumpLoadedLog();
          this.Success();
        }
      }
    }
  }
}
