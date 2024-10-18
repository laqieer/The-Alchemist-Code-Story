// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SetBoolPlayerPrefs
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.Pin(2, "Result", FlowNode.PinTypes.Output, 2)]
  [FlowNode.NodeType("System/SetBoolPlayerPrefs", 32741)]
  [FlowNode.Pin(1, "SetTrue", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(0, "SetFalse", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_SetBoolPlayerPrefs : FlowNode
  {
    private const int SET_FALSE = 0;
    private const int SET_TRUE = 1;
    public string Name;

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
        return;
      DebugUtility.Log("PlayerPrefsの設定に失敗しました");
    }
  }
}
