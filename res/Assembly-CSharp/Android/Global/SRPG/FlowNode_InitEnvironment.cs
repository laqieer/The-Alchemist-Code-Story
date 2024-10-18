// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_InitEnvironment
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.Pin(0, "In", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("System/Init", 65535)]
  [FlowNode.Pin(1, "Out", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_InitEnvironment : FlowNode
  {
    private void Init()
    {
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      GameManager gameManager;
      if ((UnityEngine.Object) instanceDirect != (UnityEngine.Object) null)
      {
        if (instanceDirect.IsRelogin)
        {
          instanceDirect.ResetData();
        }
        else
        {
          UnityEngine.Object.DestroyImmediate((UnityEngine.Object) instanceDirect);
          gameManager = (GameManager) null;
        }
      }
      CriticalSection.ForceReset();
      ButtonEvent.Reset();
      SRPG_TouchInputModule.UnlockInput(true);
      PunMonoSingleton<MyPhoton>.Instance.Disconnect();
      UIUtility.PopCanvasAll();
      AssetManager.UnloadAll();
      AssetDownloader.Reset();
      AssetDownloader.ResetTextSetting();
      Network.Reset();
      gameManager = MonoSingleton<GameManager>.Instance;
      GameUtility.ForceSetDefaultSleepSetting();
      if (GameUtility.IsStripBuild)
        GameUtility.Config_UseAssetBundles.Value = true;
      LocalizedText.UnloadAllTables();
    }

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      this.Init();
      this.ActivateOutputLinks(1);
    }
  }
}
