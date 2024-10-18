// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SaveSelectedQuestId
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

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
