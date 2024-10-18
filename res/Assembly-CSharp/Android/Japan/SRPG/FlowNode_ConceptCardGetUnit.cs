﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ConceptCardGetUnit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

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
      return (IEnumerator) new FlowNode_ConceptCardGetUnit.\u003CStartEffects\u003Ec__Iterator0() { \u0024this = this };
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
      return (IEnumerator) new FlowNode_ConceptCardGetUnit.\u003CEffectRoutine\u003Ec__Iterator2() { conceptCardData = conceptCardData };
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
