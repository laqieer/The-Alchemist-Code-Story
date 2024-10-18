// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_InitEnvironment
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.NodeType("System/Init", 65535)]
  [FlowNode.Pin(0, "In", FlowNode.PinTypes.Input, 0)]
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
      GameSettings.Reset();
      AssetManager.UnloadAll();
      AssetDownloader.Reset();
      Network.Reset();
      gameManager = MonoSingleton<GameManager>.Instance;
      GameUtility.ForceSetDefaultSleepSetting();
      if (GameUtility.IsStripBuild)
        GameUtility.Config_UseAssetBundles.Value = true;
      LocalizedText.UnloadAllTables();
      SRPG_InputField.ResetInput();
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
