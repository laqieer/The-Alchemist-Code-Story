// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CollaboQuestList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("UI/CollaboQuestList")]
  public class FlowNode_CollaboQuestList : FlowNode_GUI
  {
    protected override void OnInstanceCreate()
    {
      base.OnInstanceCreate();
      CollaboSkillQuestList componentInChildren = this.Instance.GetComponentInChildren<CollaboSkillQuestList>();
      if ((UnityEngine.Object) componentInChildren == (UnityEngine.Object) null)
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
          componentInChildren.GetComponent<WindowController>().SetCollision(false);
          componentInChildren.GetComponent<WindowController>().OnWindowStateChange = new WindowController.WindowStateChangeEvent(this.OnBack);
          WindowController.OpenIfAvailable((Component) componentInChildren);
        }
      }
    }

    private void OnBack(GameObject go, bool visible)
    {
      if (visible)
        return;
      CollaboSkillQuestList componentInChildren = this.Instance.GetComponentInChildren<CollaboSkillQuestList>();
      if ((UnityEngine.Object) componentInChildren == (UnityEngine.Object) null || visible)
        return;
      componentInChildren.GetComponent<WindowController>().SetCollision(true);
      componentInChildren.GetComponent<WindowController>().OnWindowStateChange = (WindowController.WindowStateChangeEvent) null;
      UnityEngine.Object.Destroy((UnityEngine.Object) componentInChildren.gameObject);
    }
  }
}
