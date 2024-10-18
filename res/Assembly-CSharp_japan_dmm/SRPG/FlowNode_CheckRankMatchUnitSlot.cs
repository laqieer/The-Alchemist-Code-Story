// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CheckRankMatchUnitSlot
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Multi/CheckRankMatchUnitSlot", 32741)]
  [FlowNode.Pin(120, "Check Unit Slot", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(200, "Unit Slot OK", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(201, "Unit Slot NG", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(202, "Unit Place NG", FlowNode.PinTypes.Output, 12)]
  [FlowNode.Pin(203, "Unit Same NG", FlowNode.PinTypes.Output, 13)]
  public class FlowNode_CheckRankMatchUnitSlot : FlowNode
  {
    public const int PINID_CHECK_UNIT_SLOT = 120;
    public const int PINID_UNIT_SLOT_OK = 200;
    public const int PINID_UNIT_SLOT_NG = 201;
    public const int PINID_UNIT_PLACE_NG = 202;
    public const int PINID_UNIT_SAME_NG = 203;

    public override void OnActivate(int pinID)
    {
      if (pinID != 120)
        return;
      int lastSelectionIndex;
      PartyEditData loadTeamPreset = PartyUtility.LoadTeamPresets(PlayerPartyTypes.RankMatch, out lastSelectionIndex)[lastSelectionIndex];
      for (int index = 0; index < loadTeamPreset.PartyData.VSWAITMEMBER_START; ++index)
      {
        if (index + 1 > loadTeamPreset.Units.Length || loadTeamPreset.Units[index] == null)
        {
          this.ActivateOutputLinks(201);
          return;
        }
      }
      List<int> intList = new List<int>();
      for (int index = 0; index < loadTeamPreset.PartyData.VSWAITMEMBER_START; ++index)
      {
        int num = PlayerPrefsUtility.GetInt(PlayerPrefsUtility.RANKMATCH_ID_KEY + (object) index, -1);
        if (num >= 0)
        {
          if (intList.Contains(num))
          {
            this.ActivateOutputLinks(202);
            return;
          }
          intList.Add(num);
        }
      }
      if (loadTeamPreset.Units != null)
      {
        List<UnitSameGroupParam> unitSameGroupParamList = UnitSameGroupParam.IsSameUnitInParty(loadTeamPreset.Units);
        if (unitSameGroupParamList != null)
        {
          string empty = string.Empty;
          for (int index = 0; index < unitSameGroupParamList.Count; ++index)
          {
            if (unitSameGroupParamList[index] != null)
            {
              if (index != 0)
                empty += LocalizedText.Get("sys.PARTYEDITOR_SAMEUNIT_PLUS");
              empty += unitSameGroupParamList[index].GetGroupUnitAllNameText();
            }
          }
          if (!string.IsNullOrEmpty(empty))
          {
            UIUtility.NegativeSystemMessage((string) null, string.Format(LocalizedText.Get("sys.PARTY_SAMEUNIT_INPARTY"), (object) empty), (UIUtility.DialogResultEvent) (dialog => this.ActivateOutputLinks(203)));
            return;
          }
        }
      }
      this.ActivateOutputLinks(200);
    }
  }
}
