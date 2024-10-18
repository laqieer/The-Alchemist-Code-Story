// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqMasterParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.NodeType("System/Master/ReqMasterParam", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(2, "Failed", FlowNode.PinTypes.Output, 2)]
  public class FlowNode_ReqMasterParam : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (!AssetManager.UseDLC || GameUtility.Config_UseLocalData.Value)
      {
        MonoSingleton<GameManager>.Instance.ReloadMasterData((string) null, (string) null);
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
        int errCode = (int) Network.ErrCode;
        this.OnFailed();
      }
      else
      {
        WebAPI.JSON_BodyResponse<JSON_MasterParam> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_MasterParam>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        if (!MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body))
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
