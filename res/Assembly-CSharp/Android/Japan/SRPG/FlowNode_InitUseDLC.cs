// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_InitUseDLC
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.NodeType("SRPG/InitUseDLC", 32741)]
  [FlowNode.Pin(101, "Start", FlowNode.PinTypes.Input, 101)]
  [FlowNode.Pin(1001, "End", FlowNode.PinTypes.Output, 1001)]
  public class FlowNode_InitUseDLC : FlowNode
  {
    private const int PIN_INPUT_START = 101;
    private const int PIN_OUTPUT_END = 1001;

    public override void OnActivate(int pinID)
    {
      if (pinID != 101)
        return;
      this.Init();
      this.ActivateOutputLinks(1001);
    }

    private void Init()
    {
      if (PlayerPrefsUtility.HasKey(PlayerPrefsUtility.PREFS_KEY_USE_DLC))
      {
        int num = PlayerPrefsUtility.GetInt(PlayerPrefsUtility.PREFS_KEY_USE_DLC, -1);
        if (num >= 0)
        {
          AssetManager.UseDLC = num > 0;
          return;
        }
      }
      MonoSingleton<GameManager>.Instance.InitAuth();
      this.Save(MonoSingleton<GameManager>.Instance.IsDeviceId());
    }

    private void Save(bool use_dlc)
    {
      AssetManager.UseDLC = use_dlc;
      int num = !AssetManager.UseDLC ? 0 : 1;
      PlayerPrefsUtility.SetInt(PlayerPrefsUtility.PREFS_KEY_USE_DLC, num, true);
    }
  }
}
