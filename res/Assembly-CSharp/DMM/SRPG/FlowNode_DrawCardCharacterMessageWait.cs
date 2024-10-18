// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_DrawCardCharacterMessageWait
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("DrawCard/Character/MessageWait", 32741)]
  [FlowNode.Pin(1, "Start", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "Output", FlowNode.PinTypes.Output, 10)]
  public class FlowNode_DrawCardCharacterMessageWait : FlowNode
  {
    private const int PIN_IN_START = 1;
    private const int PIN_OUT_END = 10;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      ((Behaviour) this).enabled = true;
    }

    private void Update()
    {
      if (DrawCardCharacterMessage.IsMessaging)
        return;
      this.ActivateOutputLinks(10);
      ((Behaviour) this).enabled = false;
    }
  }
}
