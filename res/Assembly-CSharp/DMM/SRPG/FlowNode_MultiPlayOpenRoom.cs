// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiPlayOpenRoom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Multi/OpenRoom", 32741)]
  [FlowNode.Pin(10, "部屋オープン", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(1010, "完了", FlowNode.PinTypes.Output, 1010)]
  public class FlowNode_MultiPlayOpenRoom : FlowNode
  {
    private const int PIN_INPUT_OPEN = 10;
    private const int PIN_OUTPUT_FINISH = 1010;

    public override void OnActivate(int pinID)
    {
      if (pinID != 10)
        return;
      this.OpenRoom();
    }

    private void OpenRoom()
    {
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      if (Object.op_Inequality((Object) instance, (Object) null) && instance.IsConnectedInRoom() && instance.IsCreatedRoom())
        instance.OpenRoom();
      this.ActivateOutputLinks(1010);
    }
  }
}
