﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_GsccInit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using Gsc.App;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("GscSystem/GsccInit", 32741)]
  [FlowNode.Pin(100, "Start Online", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(101, "Start Offline", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "Success Online", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "Success Offline", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(12, "Reset to Title", FlowNode.PinTypes.Output, 12)]
  [FlowNode.Pin(13, "バージョンが古い", FlowNode.PinTypes.Output, 13)]
  [FlowNode.Pin(14, "直接起動している", FlowNode.PinTypes.Output, 14)]
  [FlowNode.Pin(1001, "Different Assets", FlowNode.PinTypes.Output, 1001)]
  public class FlowNode_GsccInit : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 100)
        return;
      if (BootLoader.BootStates == BootLoader.BootState.SUCCESS)
      {
        this.enabled = false;
        this.ActivateOutputLinks(10);
      }
      else
      {
        if (GameUtility.Config_UseDevServer.Value)
          Network.SetDefaultHostConfigured("http://localhost:5000/");
        else if (GameUtility.Config_UseAwsServer.Value)
          Network.SetDefaultHostConfigured("https://alchemist.gu3.jp/");
        Network.Mode = Network.EConnectMode.Online;
        string str = "grdev";
        TextAsset textAsset = Resources.Load<TextAsset>("networkver");
        if ((UnityEngine.Object) textAsset != (UnityEngine.Object) null)
          str = textAsset.text;
        Network.Version = str;
        this.enabled = true;
        BootLoader.GscInit();
      }
    }

    private void Update()
    {
      switch (BootLoader.BootStates)
      {
        case BootLoader.BootState.SUCCESS:
          if (Network.IsNoVersion)
          {
            Network.ErrCode = Network.EErrCode.NoVersion;
            this.enabled = false;
            this.ActivateOutputLinks(13);
            break;
          }
          if (FlowNode_GsccInit.SettingAssets(Network.GetEnvironment.Assets, Network.GetEnvironment.AssetsEx) && MonoSingleton<GameManager>.Instance.IsRelogin)
          {
            MonoSingleton<GameManager>.Instance.IsRelogin = false;
            this.ActivateOutputLinks(1001);
          }
          else
          {
            this.Success();
            this.ActivateOutputLinks(10);
          }
          this.enabled = false;
          break;
        case BootLoader.BootState.FAILED:
          this.enabled = false;
          this.ActivateOutputLinks(14);
          break;
      }
    }

    private void Success()
    {
      MonoSingleton<GameManager>.Instance.UseAppGuardAuthentication = Network.GetEnvironment.UseAppguard != 0;
      NewsUtility.setNewsState(Network.Pub, Network.PubU, MonoSingleton<GameManager>.Instance.Player.IsFirstLogin);
      MonoSingleton<GameManager>.Instance.InitAlterHash(Network.Digest);
    }

    public static bool SettingAssets(string version, string version_ex)
    {
      bool flag = false;
      if (!string.IsNullOrEmpty(version))
      {
        if (Network.AssetVersion != version)
          flag = true;
        string dlHost = Network.DLHost;
        Network.AssetVersion = version;
        Network.AssetVersionEx = version_ex;
        AssetDownloader.DownloadURL = dlHost + "/assets/" + version + "/";
        AssetDownloader.StreamingURL = dlHost + "/";
        AssetDownloader.SetBaseDownloadURL(dlHost + "/assets/");
        AssetDownloader.ExDownloadURL = !string.IsNullOrEmpty(version_ex) ? dlHost + "/assets_ex/" + version_ex + "/" : dlHost + "/assets/demo_ex/";
      }
      return flag;
    }
  }
}
