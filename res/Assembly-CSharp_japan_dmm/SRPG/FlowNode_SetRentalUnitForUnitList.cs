// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SetRentalUnitForUnitList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("UnitRental/SetRentalUnitForUnitList", 32741)]
  [FlowNode.Pin(1, "Set", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "Finish", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(20, "Error", FlowNode.PinTypes.Output, 20)]
  public class FlowNode_SetRentalUnitForUnitList : FlowNode
  {
    private const int PIN_IN_SET = 1;
    private const int PIN_OUT_TO_UNIT = 10;
    private const int PIN_OUT_ERROR = 20;

    public override void OnActivate(int pinID)
    {
      if (pinID == 1)
      {
        GameManager instance = MonoSingleton<GameManager>.Instance;
        if (!Object.op_Equality((Object) instance, (Object) null))
        {
          UnitData rentalUnit = instance.Player.GetRentalUnit();
          if (rentalUnit != null)
          {
            GlobalVars.IsUnitRentalUnit = true;
            GlobalVars.UnitDataForUnitList = rentalUnit;
            this.ActivateOutputLinks(10);
            return;
          }
        }
      }
      this.ActivateOutputLinks(20);
    }
  }
}
