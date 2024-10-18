// Decompiled with JetBrains decompiler
// Type: SRPG.VersusStartUp
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class VersusStartUp : MonoBehaviour
  {
    private void Start()
    {
      int lastSelectionIndex;
      List<PartyEditData> teams = PartyUtility.LoadTeamPresets(PlayerPartyTypes.Versus, out lastSelectionIndex, false);
      if (teams == null || teams.Count <= lastSelectionIndex)
        return;
      PartyEditData partyEditData = teams[lastSelectionIndex];
      UnitData[] src = new UnitData[partyEditData.PartyData.MAX_UNIT];
      for (int index = 0; index < partyEditData.Units.Length && index < partyEditData.PartyData.VSWAITMEMBER_START; ++index)
        src[index] = partyEditData.Units[index];
      partyEditData.SetUnits(src);
      PartyUtility.SaveTeamPresets(PartyWindow2.EditPartyTypes.Versus, lastSelectionIndex, teams, false);
    }
  }
}
