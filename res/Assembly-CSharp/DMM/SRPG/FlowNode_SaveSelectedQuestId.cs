// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SaveSelectedQuestId
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/PlayerPrefs/SaveSelectedQuestId")]
  [FlowNode.Pin(1, "In", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(11, "Success", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(21, "Failed", FlowNode.PinTypes.Output, 21)]
  public class FlowNode_SaveSelectedQuestId : FlowNode
  {
    private const int PIN_IN = 1;
    private const int PIN_OUT_SUCCESS = 11;
    private const int PIN_OUT_FAILED = 21;
    [SerializeField]
    private string mKeyName;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      if (string.IsNullOrEmpty(this.mKeyName) || string.IsNullOrEmpty(GlobalVars.SelectedQuestID))
      {
        this.ActivateOutputLinks(21);
      }
      else
      {
        PlayerPrefsUtility.SetString(this.mKeyName, GlobalVars.SelectedQuestID, true);
        this.ActivateOutputLinks(11);
      }
    }
  }
}
