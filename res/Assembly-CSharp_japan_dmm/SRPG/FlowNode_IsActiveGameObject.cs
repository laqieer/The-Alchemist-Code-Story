// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_IsActiveGameObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Common/IsActiveGameObject", 32741)]
  [FlowNode.Pin(1, "Check", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Active", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "NotActive", FlowNode.PinTypes.Output, 102)]
  public class FlowNode_IsActiveGameObject : FlowNode
  {
    [FlowNode.ShowInInfo]
    [FlowNode.DropTarget(typeof (GameObject), true)]
    public GameObject Target;
    [SerializeField]
    private bool CheckActiveInHierarchy;
    private const int PIN_IN_CHECK = 1;
    private const int PIN_OUT_ACTIVE = 101;
    private const int PIN_OUT_NOT_ACTIVE = 102;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      if (Object.op_Implicit((Object) this.Target))
      {
        if (this.CheckActiveInHierarchy)
        {
          if (this.Target.activeInHierarchy)
          {
            this.ActivateOutputLinks(101);
            return;
          }
        }
        else if (this.Target.activeSelf)
        {
          this.ActivateOutputLinks(101);
          return;
        }
      }
      this.ActivateOutputLinks(102);
    }
  }
}
