// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SaveUseDLC
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("SRPG/SaveUseDLC", 32741)]
  [FlowNode.Pin(101, "Save", FlowNode.PinTypes.Input, 101)]
  [FlowNode.Pin(1001, "End", FlowNode.PinTypes.Output, 1001)]
  public class FlowNode_SaveUseDLC : FlowNode
  {
    private const int PIN_INPUT_SAVE = 101;
    private const int PIN_OUTPUT_END = 1001;
    [SerializeField]
    private int NewValue;
    [SerializeField]
    private bool IsWritePrefs = true;

    public override void OnActivate(int pinID)
    {
      if (pinID != 101)
        return;
      this.Save();
      this.ActivateOutputLinks(1001);
    }

    private void Save()
    {
      AssetManager.UseDLC = this.NewValue > 0;
      if (!this.IsWritePrefs)
        return;
      PlayerPrefsUtility.SetInt(PlayerPrefsUtility.PREFS_KEY_USE_DLC, this.NewValue, true);
      if (!PlayerPrefsUtility.HasKey(PlayerPrefsUtility.PREFS_KEY_TUTORIAL_CLEARED))
        return;
      PlayerPrefsUtility.SetInt(PlayerPrefsUtility.PREFS_KEY_TUTORIAL_CLEARED, this.NewValue, true);
    }
  }
}
