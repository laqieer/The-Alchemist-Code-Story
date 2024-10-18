// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SetTowerWebURL
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Tower/SetTowerWebURL", 32741)]
  [FlowNode.Pin(0, "Set", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Succes", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(2, "Error", FlowNode.PinTypes.Output, 0)]
  public class FlowNode_SetTowerWebURL : FlowNode
  {
    [SerializeField]
    private string URL;
    [SerializeField]
    private string Value;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      TowerParam tower = MonoSingleton<GameManager>.Instance.FindTower(GlobalVars.SelectedTowerID);
      if (tower == null)
      {
        this.ActivateOutputLinks(2);
      }
      else
      {
        FlowNode_Variable.Set(this.Value, this.URL + tower.URL);
        this.ActivateOutputLinks(1);
      }
    }
  }
}
