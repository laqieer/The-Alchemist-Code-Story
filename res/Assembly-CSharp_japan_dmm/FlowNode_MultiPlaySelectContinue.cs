// Decompiled with JetBrains decompiler
// Type: FlowNode_MultiPlaySelectContinue
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using SRPG;
using System;
using System.Collections.Generic;

#nullable disable
[FlowNode.NodeType("Multi/MultiPlaySelectContinue", 32741)]
[FlowNode.Pin(100, "する", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(200, "しない", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(1, "Out", FlowNode.PinTypes.Output, 0)]
public class FlowNode_MultiPlaySelectContinue : FlowNode
{
  public override void OnActivate(int pinID)
  {
    switch (pinID)
    {
      case 100:
        GlobalVars.SelectedMultiPlayerUnitIDs = new List<int>();
        SceneBattle instance = SceneBattle.Instance;
        BattleCore battle = !UnityEngine.Object.op_Equality((UnityEngine.Object) instance, (UnityEngine.Object) null) ? instance.Battle : (BattleCore) null;
        if (battle != null)
        {
          foreach (Unit unit1 in battle.Units)
          {
            Unit unit = unit1;
            if (unit.OwnerPlayerIndex > 0 && unit.IsDead)
            {
              int index = battle.AllUnits.FindIndex((Predicate<Unit>) (u => u == unit));
              GlobalVars.SelectedMultiPlayerUnitIDs.Add(index);
            }
          }
        }
        GlobalVars.SelectedMultiPlayContinue = GlobalVars.EMultiPlayContinue.CONTINUE;
        this.ActivateOutputLinks(1);
        break;
      case 200:
        GlobalVars.SelectedMultiPlayContinue = GlobalVars.EMultiPlayContinue.CANCEL;
        GlobalVars.SelectedMultiPlayerUnitIDs = new List<int>();
        this.ActivateOutputLinks(1);
        break;
    }
  }
}
