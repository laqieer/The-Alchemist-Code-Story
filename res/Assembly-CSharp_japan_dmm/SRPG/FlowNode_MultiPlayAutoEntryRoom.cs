// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiPlayAutoEntryRoom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Multi/AutoEntryRoom", 32741)]
  [FlowNode.Pin(1, "開始", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "自動入室に成功", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "自動入室に失敗", FlowNode.PinTypes.Output, 102)]
  public class FlowNode_MultiPlayAutoEntryRoom : FlowNode
  {
    private const int PIN_INPUT_START = 1;
    private const int PIN_OUTPUT_SUCCESS = 101;
    private const int PIN_OUTPUT_FAILED = 102;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      this.AutoEntry();
    }

    private void AutoEntry()
    {
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      if (Object.op_Inequality((Object) instance, (Object) null) && instance.IsConnectedInRoom())
        this.ActivateOutputLinks(101);
      else
        this.ActivateOutputLinks(102);
    }
  }
}
