// Decompiled with JetBrains decompiler
// Type: SRPG.WorldRaidPartyWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class WorldRaidPartyWindow : PartyWindow2
  {
    protected override bool CheckMember(int numMainUnits)
    {
      if (!base.CheckMember(numMainUnits))
        return false;
      if (this.CurrentParty.Units[0] != null)
        return true;
      UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.LEADERNOTSET"), (UIUtility.DialogResultEvent) (dialog => { }));
      return false;
    }

    protected override void RegistPartyMember(
      List<UnitData> allUnits,
      bool heroesAvailable,
      bool selectedSlotIsEmpty,
      int numMainMembers)
    {
      for (int index = 0; index < allUnits.Count; ++index)
      {
        if ((heroesAvailable || !allUnits[index].UnitParam.IsHero()) && (this.CurrentParty.PartyData.SUBMEMBER_START > this.mSelectedSlotIndex || this.mSelectedSlotIndex > this.CurrentParty.PartyData.SUBMEMBER_END || allUnits[index] != this.CurrentParty.Units[0] || !selectedSlotIsEmpty || numMainMembers > 1))
          this.UnitList.AddItem(this.mOwnUnits.IndexOf(allUnits[index]) + 1);
      }
    }

    protected override void PostForwardPressed()
    {
      if (Object.op_Equality((Object) WorldRaidManager.Instance, (Object) null))
        DebugUtility.LogError("WorldRaidManager is NULL : WorldRaidPartyWindow.PostForwardPressed");
      else
        base.PostForwardPressed();
    }
  }
}
