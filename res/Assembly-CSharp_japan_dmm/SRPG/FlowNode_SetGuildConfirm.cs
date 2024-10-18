// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SetGuildConfirm
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Guild/SetConfirmType", 32741)]
  [FlowNode.Pin(1, "開始", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "完了", FlowNode.PinTypes.Output, 2)]
  public class FlowNode_SetGuildConfirm : FlowNode
  {
    private const int PIN_INPUT_START = 1;
    private const int PIN_OUTPUT_END = 2;
    [SerializeField]
    private SerializeValueBehaviour Object;
    [SerializeField]
    private GuildConfirm.eConfirmType ConfirmType;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      this.SetConfirmType();
      this.ActivateOutputLinks(2);
    }

    private void SetConfirmType()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.Object, (UnityEngine.Object) null))
        return;
      GuildConfirm.SetConfirmType(((Component) this.Object).gameObject, this.ConfirmType);
    }
  }
}
