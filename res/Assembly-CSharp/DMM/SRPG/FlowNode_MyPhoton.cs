// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MyPhoton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Multi/MyPhoton", 32741)]
  [FlowNode.Pin(0, "Reset", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "Reset Complete", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_MyPhoton : FlowNode
  {
    private const int PIN_IN_MYPHOTON_RESET = 0;
    private const int PIN_OUT_MYPHOTON_RESETED = 10;

    public override void OnActivate(int pinID)
    {
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      if (Object.op_Equality((Object) instance, (Object) null) || pinID != 0)
        return;
      instance.Reset();
      this.ActivateOutputLinks(10);
    }
  }
}
