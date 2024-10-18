// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ConceptCardNoSaleCheck
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("UI/ConceptCardNoSaleCheck", 32741)]
  [FlowNode.Pin(10, "入力", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(1000, "非売品が存在する", FlowNode.PinTypes.Output, 1000)]
  [FlowNode.Pin(1001, "非売品は無い", FlowNode.PinTypes.Output, 1001)]
  public class FlowNode_ConceptCardNoSaleCheck : FlowNode
  {
    private const int INPUT_CHECK = 10;
    private const int OUTPUT_EXIST = 1000;
    private const int OUTPUT_NO_EXIST = 1001;

    public override void OnActivate(int pinID)
    {
      if (pinID != 10)
        return;
      ConceptCardManager instance = ConceptCardManager.Instance;
      if (Object.op_Equality((Object) instance, (Object) null))
        return;
      foreach (ConceptCardData conceptCardData in instance.SelectedMaterials.GetList())
      {
        if (conceptCardData.Param.not_sale)
        {
          this.ActivateOutputLinks(1000);
          return;
        }
      }
      this.ActivateOutputLinks(1001);
    }
  }
}
