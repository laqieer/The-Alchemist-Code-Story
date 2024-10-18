// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CheckUnitRentalMaxFavorite
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("UnitRental/Check/MaxFavorite", 32741)]
  [FlowNode.Pin(1, "Check", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(111, "Max", FlowNode.PinTypes.Output, 111)]
  [FlowNode.Pin(112, "NoMax", FlowNode.PinTypes.Output, 112)]
  [FlowNode.Pin(113, "NotFoundUnit", FlowNode.PinTypes.Output, 113)]
  public class FlowNode_CheckUnitRentalMaxFavorite : FlowNode
  {
    private const int PIN_IN_COMP = 1;
    private const int PIN_OUT_MAX = 111;
    private const int PIN_OUT_NO_MAX = 112;
    private const int PIN_OUT_NOT_FOUND_UNIT = 113;
    [SerializeField]
    private bool IsCheckExpire;

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
          if (unitRentalParam != null)
          {
            bool flag = false;
            if (this.IsCheckExpire && !unitRentalParam.IsWithinPeriod())
              flag = true;
            if (!flag)
            {
              if (rentalUnit.RentalFavoritePoint >= (int) unitRentalParam.PtMax)
              {
                this.ActivateOutputLinks(111);
                return;
              }
              this.ActivateOutputLinks(112);
              return;
            }
          }
        }
      }
      this.ActivateOutputLinks(113);
    }
  }
}
