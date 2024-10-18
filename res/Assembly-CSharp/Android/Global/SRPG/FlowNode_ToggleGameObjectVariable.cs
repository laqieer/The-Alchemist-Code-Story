// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ToggleGameObjectVariable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 2)]
  [FlowNode.NodeType("Toggle/GameObjectVariable", 32741)]
  [FlowNode.Pin(10, "Variable", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_ToggleGameObjectVariable : FlowNode
  {
    [FlowNode.ShowInInfo]
    [FlowNode.DropTarget(typeof (GameObject), true)]
    public GameObject Target;
    public FlowNode_ToggleGameObjectVariable.VariableType variable_type;
    public FlowNode_ToggleGameObjectVariable.EnableType enable_type;

    public override void OnActivate(int pinID)
    {
      this.SetupVisible();
      this.ActivateOutputLinks(1);
    }

    public void SetupVisible()
    {
      bool flag = false;
      switch (this.variable_type)
      {
        case FlowNode_ToggleGameObjectVariable.VariableType.None:
          return;
        case FlowNode_ToggleGameObjectVariable.VariableType.IsEventQuest:
          if (GlobalVars.SelectedSection.Get().Equals("WD_DAILY"))
          {
            flag = true;
            break;
          }
          break;
        case FlowNode_ToggleGameObjectVariable.VariableType.Hide:
          flag = false;
          break;
        case FlowNode_ToggleGameObjectVariable.VariableType.Show:
          flag = true;
          break;
      }
      if (!((UnityEngine.Object) this.Target != (UnityEngine.Object) null))
        return;
      this.Target.SetActive(flag);
    }

    public enum VariableType
    {
      None,
      IsEventQuest,
      Hide,
      Show,
    }

    public enum EnableType
    {
      None,
      IsEventQuest,
      Disable,
      Enable,
    }
  }
}
