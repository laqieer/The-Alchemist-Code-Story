// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_AbilityRankUp
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;

namespace SRPG
{
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("System/AbilityRankUp", 32741)]
  [FlowNode.Pin(4, "アビリティポイントが不足", FlowNode.PinTypes.Output, 4)]
  [FlowNode.Pin(3, "アビリティ成長可能回数が不足", FlowNode.PinTypes.Output, 3)]
  [FlowNode.Pin(2, "アビリティ成長限界に達している", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_AbilityRankUp : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      UnitData unitDataByUniqueId = player.FindUnitDataByUniqueID((long) GlobalVars.SelectedUnitUniqueID);
      AbilityData abilityData = unitDataByUniqueId.GetAbilityData((long) GlobalVars.SelectedAbilityUniqueID);
      if (abilityData.Rank >= abilityData.GetRankCap())
      {
        this.enabled = false;
        this.ActivateOutputLinks(2);
      }
      if (player.AbilityRankUpCountNum == 0)
      {
        this.enabled = false;
        this.ActivateOutputLinks(3);
      }
      player.RankUpAbility(abilityData);
      Dictionary<long, int> abilitiesRankUp = GlobalVars.AbilitiesRankUp;
      if (!abilitiesRankUp.ContainsKey(abilityData.UniqueID))
        abilitiesRankUp[abilityData.UniqueID] = 0;
      Dictionary<long, int> dictionary;
      long uniqueId;
      (dictionary = abilitiesRankUp)[uniqueId = abilityData.UniqueID] = dictionary[uniqueId] + 1;
      MonoSingleton<GameManager>.Instance.Player.OnAbilityPowerUp(unitDataByUniqueId.UnitID, abilityData.AbilityID, abilityData.Rank, false);
      List<string> learningSkillList = abilityData.GetLearningSkillList(abilityData.Rank);
      if (learningSkillList != null && learningSkillList.Count > 0)
        FlowNode_Variable.Set("LEARNING_SKILL", "1");
      this.enabled = false;
      this.ActivateOutputLinks(1);
    }
  }
}
