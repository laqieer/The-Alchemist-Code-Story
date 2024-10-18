﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CheckVersion2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using Gsc.App;
using UnityEngine;

#nullable disable
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
        ((Behaviour) this).enabled = true;
      }
      else
      {
        ((Behaviour) this).enabled = false;
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
        if (!string.IsNullOrEmpty(jsonObject.body.environments.alchemist.master_digest))
          Network.MasterDigest = jsonObject.body.environments.alchemist.master_digest;
        if (!string.IsNullOrEmpty(jsonObject.body.environments.alchemist.quest_digest))
          Network.QuestDigest = jsonObject.body.environments.alchemist.quest_digest;
        Gsc.App.Environment.ProcessMsgPackEncryptionEnvFlags((Gsc.App.Environment.EnvironmentFlagBit) jsonObject.body.environments.alchemist.env_flg);
        if (!string.IsNullOrEmpty(jsonObject.body.environments.alchemist.env_flg2))
          Network.EnvFlg2 = Gsc.App.Environment.ProcessMsgPackEncryptionEnvFlags2(jsonObject.body.environments.alchemist.env_flg2);
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
        ((Behaviour) this).enabled = false;
      }
    }

    private class Json_Alchemist
    {
      public string assets;
      public string assets_ex;
      public int use_appguard;
      public string master_digest;
      public string quest_digest;
      public int env_flg;
      public string env_flg2;
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
