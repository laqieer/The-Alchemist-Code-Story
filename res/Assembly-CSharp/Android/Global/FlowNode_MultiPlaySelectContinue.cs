// Decompiled with JetBrains decompiler
// Type: FlowNode_MultiPlaySelectContinue
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using SRPG;
using System;
using System.Collections.Generic;

[FlowNode.Pin(1, "Out", FlowNode.PinTypes.Output, 0)]
[FlowNode.Pin(200, "しない", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(100, "する", FlowNode.PinTypes.Input, 0)]
[FlowNode.NodeType("Multi/MultiPlaySelectContinue", 32741)]
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
          using (List<Unit>.Enumerator enumerator = battleCore.Units.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              Unit unit = enumerator.Current;
              if (unit.OwnerPlayerIndex > 0 && unit.IsDead)
              {
                int index = battleCore.AllUnits.FindIndex((Predicate<Unit>) (u => u == unit));
                GlobalVars.SelectedMultiPlayerUnitIDs.Add(index);
              }
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
