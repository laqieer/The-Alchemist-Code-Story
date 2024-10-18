// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CheckVersion2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using Gsc.App;

namespace SRPG
{
  [FlowNode.NodeType("System/Version/CheckVersion2", 32741)]
  [FlowNode.Pin(100, "Start Online", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "Success Online", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "Success Offline", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(12, "Reset to Title", FlowNode.PinTypes.Output, 12)]
  [FlowNode.Pin(1000, "No Version", FlowNode.PinTypes.Output, 1000)]
  [FlowNode.Pin(1001, "Different Assets", FlowNode.PinTypes.Output, 1001)]
  public class FlowNode_CheckVersion2 : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 100)
        return;
      if (BootLoader.BootStates == BootLoader.BootState.SUCCESS)
      {
        this.ExecRequest((WebAPI) new ReqCheckVersion2(Network.Version, new Network.ResponseCallback(this.CheckVersionResponseCallback)));
        this.enabled = true;
      }
      else
      {
        this.enabled = false;
        this.ActivateOutputLinks(10);
      }
    }

    public void CheckVersionResponseCallback(WWWResult www)
    {
      if (Network.IsNoVersion)
      {
        Network.RemoveAPI();
        Network.ResetError();
        this.ActivateOutputLinks(1000);
      }
      else
      {
        if (FlowNode_Network.HasCommonError(www))
          return;
        this.OnSuccess(www);
      }
    }

    public override void OnSuccess(WWWResult www)
    {
      WebAPI.JSON_BodyResponse<FlowNode_CheckVersion2.Json_VersionInfo> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_CheckVersion2.Json_VersionInfo>>(www.text);
      DebugUtility.Assert(jsonObject != null, "res == null");
      if (Network.IsError)
      {
        int errCode = (int) Network.ErrCode;
        this.OnRetry();
      }
      else if (jsonObject.body == null)
      {
        this.OnRetry();
      }
      else
      {
        Network.RemoveAPI();
        if (jsonObject.body.environments == null || jsonObject.body.environments.alchemist == null)
          return;
        if (FlowNode_GsccInit.SettingAssets(jsonObject.body.environments.alchemist.assets, jsonObject.body.environments.alchemist.assets_ex) && MonoSingleton<GameManager>.Instance.IsRelogin)
        {
          MonoSingleton<GameManager>.Instance.IsRelogin = false;
          this.ActivateOutputLinks(1001);
        }
        else
        {
          MonoSingleton<GameManager>.Instance.UseAppGuardAuthentication = jsonObject.body.environments.alchemist.use_appguard != 0;
          this.ActivateOutputLinks(10);
        }
        this.enabled = false;
      }
    }

    private class Json_Alchemist
    {
      public string assets;
      public string assets_ex;
      public int use_appguard;
    }

    private class Json_Environment
    {
      public FlowNode_CheckVersion2.Json_Alchemist alchemist;
    }

    private class Json_VersionInfo
    {
      public FlowNode_CheckVersion2.Json_Environment environments;
    }
  }
}
