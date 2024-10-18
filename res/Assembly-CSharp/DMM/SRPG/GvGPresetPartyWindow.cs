// Decompiled with JetBrains decompiler
// Type: SRPG.GvGPresetPartyWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class GvGPresetPartyWindow : PartyWindow2
  {
    protected override void SetItemSlot(int slotIndex, ItemData item)
    {
    }

    protected override void OnForwardOrBackButtonClick(SRPG_Button button)
    {
      if (!((Selectable) button).IsInteractable())
        return;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) button, (UnityEngine.Object) this.BackButton))
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 3);
      }
      else
      {
        long[] array = new long[3];
        for (int index = 0; index < this.CurrentParty.Units.Length; ++index)
        {
          if (this.CurrentParty.Units[index] != null)
            array[index] = this.CurrentParty.Units[index].UniqueID;
        }
        if (GvGManager.Instance.GvGPeriod != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) GvGDefenseSettings.Instance, (UnityEngine.Object) null))
        {
          long[] all = Array.FindAll<long>(array, (Predicate<long>) (unit_uid => unit_uid != 0L));
          if (all == null || all.Length < GvGManager.Instance.GvGPeriod.DefenseUnitMin)
          {
            UIUtility.SystemMessage(string.Format(LocalizedText.Get("sys.GVG_DEFENSE_UNITCOUNT_NOT_ENOUGH"), (object) GvGManager.Instance.GvGPeriod.DefenseUnitMin), (UIUtility.DialogResultEvent) null);
            return;
          }
        }
        if (!Array.Exists<long>(array, (Predicate<long>) (unit_uid => unit_uid != 0L)))
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 3);
        else
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 1);
      }
    }
  }
}
