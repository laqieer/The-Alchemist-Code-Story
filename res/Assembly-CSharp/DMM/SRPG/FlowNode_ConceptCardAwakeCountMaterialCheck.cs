// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ConceptCardAwakeCountMaterialCheck
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("UI/ConceptCardAwakeCountMaterialCheck", 32741)]
  [FlowNode.Pin(10, "入力", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(1000, "限界突破が最大の素材が含まれている", FlowNode.PinTypes.Output, 1000)]
  [FlowNode.Pin(1001, "限界突破が最大の素材が含まれていない", FlowNode.PinTypes.Output, 1001)]
  public class FlowNode_ConceptCardAwakeCountMaterialCheck : FlowNode
  {
    private const int INPUT_CHECK = 10;
    private const int OUTPUT_HIGH = 1000;
    private const int OUTPUT_LOW = 1001;

    public override void OnActivate(int pinID)
    {
      if (pinID != 10)
        return;
      ConceptCardManager instance = ConceptCardManager.Instance;
      if (Object.op_Equality((Object) instance, (Object) null))
        return;
      if (ConceptCardManager.ContainsAwakeCountMax(instance.SelectedConceptCardData, instance.SelectedMaterials))
        this.ActivateOutputLinks(1000);
      else
        this.ActivateOutputLinks(1001);
    }
  }
}
