// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ConceptCardGetUnit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("ConceptCard/ConceptCardGetUnit")]
  [FlowNode.Pin(0, "開始", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "終了", FlowNode.PinTypes.Output, 10)]
  public class FlowNode_ConceptCardGetUnit : FlowNode
  {
    private static List<ConceptCardData> s_ConceptCards = new List<ConceptCardData>();

    public static void AddConceptCardData(ConceptCardData conceptCardData)
    {
      FlowNode_ConceptCardGetUnit.s_ConceptCards.Add(conceptCardData);
    }

    [DebuggerHidden]
    private IEnumerator StartEffects()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_ConceptCardGetUnit.\u003CStartEffects\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    [DebuggerHidden]
    private IEnumerator DownloadRoutine()
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      FlowNode_ConceptCardGetUnit.\u003CDownloadRoutine\u003Ec__Iterator1 routineCIterator1 = new FlowNode_ConceptCardGetUnit.\u003CDownloadRoutine\u003Ec__Iterator1();
      return (IEnumerator) routineCIterator1;
    }

    [DebuggerHidden]
    private IEnumerator EffectRoutine(ConceptCardData conceptCardData)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_ConceptCardGetUnit.\u003CEffectRoutine\u003Ec__Iterator2()
      {
        conceptCardData = conceptCardData
      };
    }

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (FlowNode_ConceptCardGetUnit.s_ConceptCards != null && FlowNode_ConceptCardGetUnit.s_ConceptCards.Count > 0)
        this.StartCoroutine(this.StartEffects());
      else
        this.ActivateOutputLinks(10);
    }
  }
}
