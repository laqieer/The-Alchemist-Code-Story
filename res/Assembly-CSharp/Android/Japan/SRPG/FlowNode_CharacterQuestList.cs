// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CharacterQuestList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("UI/CharacterQuestList")]
  public class FlowNode_CharacterQuestList : FlowNode_GUI
  {
    protected override void OnInstanceCreate()
    {
      base.OnInstanceCreate();
      UnitCharacterQuestWindow componentInChildren = this.Instance.GetComponentInChildren<UnitCharacterQuestWindow>();
      if ((UnityEngine.Object) componentInChildren == (UnityEngine.Object) null)
        return;
      componentInChildren.CurrentUnit = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) GlobalVars.PreBattleUnitUniqueID);
      componentInChildren.GetComponent<WindowController>().SetCollision(false);
      componentInChildren.GetComponent<WindowController>().OnWindowStateChange = new WindowController.WindowStateChangeEvent(this.OnBack);
      WindowController.OpenIfAvailable((Component) componentInChildren);
    }

    private void OnBack(GameObject go, bool visible)
    {
      if (visible)
        return;
      UnitCharacterQuestWindow componentInChildren = this.Instance.GetComponentInChildren<UnitCharacterQuestWindow>();
      if ((UnityEngine.Object) componentInChildren == (UnityEngine.Object) null || visible)
        return;
      componentInChildren.GetComponent<WindowController>().SetCollision(true);
      componentInChildren.GetComponent<WindowController>().OnWindowStateChange = (WindowController.WindowStateChangeEvent) null;
      UnityEngine.Object.Destroy((UnityEngine.Object) componentInChildren.gameObject);
    }
  }
}
