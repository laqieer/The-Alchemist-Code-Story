// Decompiled with JetBrains decompiler
// Type: FlowNode_MultiPlaySelectContinue
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using SRPG;
using System;
using System.Collections.Generic;

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
        BattleCore battleCore = !((UnityEngine.Object) instance == (UnityEngine.Object) null) ? instance.Battle : (BattleCore) null;
        if (battleCore != null)
        {
          foreach (Unit unit1 in battleCore.Units)
          {
            Unit unit = unit1;
            if (unit.OwnerPlayerIndex > 0 && unit.IsDead)
            {
              int index = battleCore.AllUnits.FindIndex((Predicate<Unit>) (u => u == unit));
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
