// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SetBoolPlayerPrefs
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.NodeType("System/PlayerPrefs/SetBoolPlayerPrefs", 32741)]
  [FlowNode.Pin(1, "SetTrue", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(0, "SetFalse", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(2, "Result", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(3, "Finish", FlowNode.PinTypes.Output, 3)]
  public class FlowNode_SetBoolPlayerPrefs : FlowNode
  {
    public string Name;
    private const int SET_FALSE = 0;
    private const int SET_TRUE = 1;

    public override void OnActivate(int pinID)
    {
      bool flag = false;
      switch (pinID)
      {
        case 0:
          flag = PlayerPrefsUtility.SetInt(this.Name, 0, true);
          break;
        case 1:
          flag = PlayerPrefsUtility.SetInt(this.Name, 1, true);
          break;
      }
      if (!flag)
        DebugUtility.Log("PlayerPrefsの設定に失敗しました");
      this.ActivateOutputLinks(3);
    }
  }
}
