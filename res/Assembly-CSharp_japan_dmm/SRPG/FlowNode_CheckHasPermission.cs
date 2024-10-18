// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CheckHasPermission
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Native/Permission/CheckHasPermission", 16750080)]
  [FlowNode.Pin(0, "判定", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "許可", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "不許可", FlowNode.PinTypes.Output, 101)]
  public class FlowNode_CheckHasPermission : FlowNode
  {
    public const int INPUT_CHECK = 0;
    public const int OUTPUT_APPROVED = 100;
    public const int OUTPUT_REJECTED = 101;
    [SerializeField]
    private ePermissionTypes m_CheckPermissionType;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (MonoSingleton<PermissionManager>.Instance.hasPermission(this.m_CheckPermissionType))
        this.ActivateOutputLinks(100);
      else
        this.ActivateOutputLinks(101);
    }
  }
}
