// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_DownloadRentalUnitAssets
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("UnitRental/Download", 32741)]
  [FlowNode.Pin(1, "Start", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(111, "End", FlowNode.PinTypes.Output, 111)]
  public class FlowNode_DownloadRentalUnitAssets : FlowNode
  {
    private const int PIN_IN_START = 1;
    private const int PIN_OUT_END = 111;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (Object.op_Equality((Object) instance, (Object) null))
      {
        this.ActivateOutputLinks(111);
      }
      else
      {
        UnitData rentalUnit = instance.Player.GetRentalUnit();
        if (rentalUnit == null)
        {
          this.ActivateOutputLinks(111);
        }
        else
        {
          DownloadUtility.DownloadUnit(rentalUnit.UnitParam);
          this.StartCoroutine(this.Download());
        }
      }
    }

    [DebuggerHidden]
    private IEnumerator Download()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_DownloadRentalUnitAssets.\u003CDownload\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }
  }
}
