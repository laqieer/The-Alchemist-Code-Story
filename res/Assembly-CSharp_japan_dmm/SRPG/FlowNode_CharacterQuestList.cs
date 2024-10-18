// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CharacterQuestList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("UI/CharacterQuestList")]
  public class FlowNode_CharacterQuestList : FlowNode_GUI
  {
    protected override void OnInstanceCreate()
    {
      base.OnInstanceCreate();
      UnitCharacterQuestWindow componentInChildren = this.Instance.GetComponentInChildren<UnitCharacterQuestWindow>();
      if (Object.op_Equality((Object) componentInChildren, (Object) null))
        return;
      componentInChildren.CurrentUnit = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) GlobalVars.PreBattleUnitUniqueID);
      ((Component) componentInChildren).GetComponent<WindowController>().SetCollision(false);
      ((Component) componentInChildren).GetComponent<WindowController>().OnWindowStateChange = new WindowController.WindowStateChangeEvent(this.OnBack);
      WindowController.OpenIfAvailable((Component) componentInChildren);
    }

    private void OnBack(GameObject go, bool visible)
    {
      if (visible)
        return;
      UnitCharacterQuestWindow componentInChildren = this.Instance.GetComponentInChildren<UnitCharacterQuestWindow>();
      if (Object.op_Equality((Object) componentInChildren, (Object) null) || visible)
        return;
      ((Component) componentInChildren).GetComponent<WindowController>().SetCollision(true);
      ((Component) componentInChildren).GetComponent<WindowController>().OnWindowStateChange = (WindowController.WindowStateChangeEvent) null;
      Object.Destroy((Object) ((Component) componentInChildren).gameObject);
    }
  }
}
