// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CheckUnitRentalQuestActive
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("UnitRental/Check/RentalQuestActive", 32741)]
  [FlowNode.Pin(1, "IsActive", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(111, "Yes", FlowNode.PinTypes.Output, 111)]
  [FlowNode.Pin(112, "No", FlowNode.PinTypes.Output, 112)]
  public class FlowNode_CheckUnitRentalQuestActive : FlowNode
  {
    private const int PIN_IN_IS_ACTIVE = 1;
    private const int PIN_OUT_YES = 111;
    private const int PIN_OUT_NO = 112;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (Object.op_Implicit((Object) instance))
      {
        UnitData rentalUnit = instance.Player.GetRentalUnit();
        if (rentalUnit != null)
        {
          UnitRentalParam unitRentalParam = UnitRentalParam.GetParam(rentalUnit.RentalIname);
          int rentalFavoritePoint = rentalUnit.RentalFavoritePoint;
          if (unitRentalParam != null)
          {
            foreach (RentalQuestInfo rentalQuestInfo in unitRentalParam.UnitQuestInfo)
            {
              if (rentalQuestInfo != null && rentalQuestInfo.Quest != null && (int) rentalQuestInfo.Point <= rentalFavoritePoint && rentalQuestInfo.Quest.IsAvailable() && rentalQuestInfo.Quest.state != QuestStates.Cleared)
              {
                this.ActivateOutputLinks(111);
                return;
              }
            }
          }
        }
      }
      this.ActivateOutputLinks(112);
    }
  }
}
