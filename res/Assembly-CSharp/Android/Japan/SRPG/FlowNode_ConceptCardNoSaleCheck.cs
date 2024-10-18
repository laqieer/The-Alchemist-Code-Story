// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ConceptCardNoSaleCheck
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
      if ((UnityEngine.Object) instance == (UnityEngine.Object) null)
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
