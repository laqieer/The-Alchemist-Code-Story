// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SetBoolPlayerPrefs
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
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
