// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CollaboQuestList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("UI/CollaboQuestList")]
  public class FlowNode_CollaboQuestList : FlowNode_GUI
  {
    protected override void OnInstanceCreate()
    {
      base.OnInstanceCreate();
      CollaboSkillQuestList componentInChildren = this.Instance.GetComponentInChildren<CollaboSkillQuestList>();
      if (Object.op_Equality((Object) componentInChildren, (Object) null))
        return;
      CollaboSkillParam.Pair collaboSkillPair = GlobalVars.SelectedCollaboSkillPair;
      if (collaboSkillPair == null)
      {
        DebugUtility.LogError("CollaboSkillParam.Pair が セットされていない");
      }
      else
      {
        componentInChildren.CurrentUnit1 = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueParam(collaboSkillPair.UnitParam1);
        componentInChildren.CurrentUnit2 = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueParam(collaboSkillPair.UnitParam2);
        if (componentInChildren.CurrentUnit1 == null)
          DebugUtility.LogError("window.CurrentUnit1 == null");
        else if (componentInChildren.CurrentUnit2 == null)
        {
          DebugUtility.LogError("window.CurrentUnit2 == null");
        }
        else
        {
          ((Component) componentInChildren).GetComponent<WindowController>().SetCollision(false);
          ((Component) componentInChildren).GetComponent<WindowController>().OnWindowStateChange = new WindowController.WindowStateChangeEvent(this.OnBack);
          WindowController.OpenIfAvailable((Component) componentInChildren);
        }
      }
    }

    private void OnBack(GameObject go, bool visible)
    {
      if (visible)
        return;
      CollaboSkillQuestList componentInChildren = this.Instance.GetComponentInChildren<CollaboSkillQuestList>();
      if (Object.op_Equality((Object) componentInChildren, (Object) null) || visible)
        return;
      ((Component) componentInChildren).GetComponent<WindowController>().SetCollision(true);
      ((Component) componentInChildren).GetComponent<WindowController>().OnWindowStateChange = (WindowController.WindowStateChangeEvent) null;
      Object.Destroy((Object) ((Component) componentInChildren).gameObject);
    }
  }
}
