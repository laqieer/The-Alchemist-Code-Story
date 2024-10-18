// Decompiled with JetBrains decompiler
// Type: SRPG.VersusStartUp
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class VersusStartUp : MonoBehaviour
  {
    private void Start()
    {
      int lastSelectionIndex;
      List<PartyEditData> teams = PartyUtility.LoadTeamPresets(PlayerPartyTypes.Versus, out lastSelectionIndex);
      if (teams == null || teams.Count <= lastSelectionIndex)
        return;
      PartyEditData partyEditData = teams[lastSelectionIndex];
      UnitData[] src = new UnitData[partyEditData.PartyData.MAX_UNIT];
      for (int index = 0; index < partyEditData.Units.Length && index < partyEditData.PartyData.VSWAITMEMBER_START; ++index)
        src[index] = partyEditData.Units[index];
      partyEditData.SetUnits(src);
      PartyUtility.SaveTeamPresets(PartyWindow2.EditPartyTypes.Versus, lastSelectionIndex, teams);
    }
  }
}
