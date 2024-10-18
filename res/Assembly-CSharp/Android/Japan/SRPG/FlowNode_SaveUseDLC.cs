// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SaveUseDLC
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("SRPG/SaveUseDLC", 32741)]
  [FlowNode.Pin(101, "Save", FlowNode.PinTypes.Input, 101)]
  [FlowNode.Pin(1001, "End", FlowNode.PinTypes.Output, 1001)]
  public class FlowNode_SaveUseDLC : FlowNode
  {
    [SerializeField]
    private bool IsWritePrefs = true;
    private const int PIN_INPUT_SAVE = 101;
    private const int PIN_OUTPUT_END = 1001;
    [SerializeField]
    private int NewValue;

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
