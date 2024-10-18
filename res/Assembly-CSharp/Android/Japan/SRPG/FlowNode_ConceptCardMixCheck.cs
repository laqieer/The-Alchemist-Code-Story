// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ConceptCardMixCheck
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.NodeType("UI/ConceptCardMixCheck", 32741)]
  [FlowNode.Pin(10, "合成可能かチェック", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(11, "一括強化による合成可能かチェック", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(1000, "合成可能", FlowNode.PinTypes.Output, 1000)]
  [FlowNode.Pin(1001, "ゼニー不足", FlowNode.PinTypes.Output, 1001)]
  [FlowNode.Pin(20, "境界値が越えているかのチェック", FlowNode.PinTypes.Input, 20)]
  [FlowNode.Pin(2000, "境界値の限界突破", FlowNode.PinTypes.Output, 2000)]
  public class FlowNode_ConceptCardMixCheck : FlowNode
  {
    private const int INPUT_MIX_CHECK = 10;
    private const int INPUT_BULK_MIX_CHECK = 11;
    private const int OUTPUT_MIX_OK = 1000;
    private const int OUTPUT_MIX_ZENY_NG = 1001;
    private const int INPUT_MIX_OVERTRUST = 20;
    private const int OUTPUT_MIX_OVERTRUST = 2000;

    public override void OnActivate(int pinID)
    {
      if (pinID == 10 || pinID == 11)
      {
        ConceptCardManager instance = ConceptCardManager.Instance;
        if ((UnityEngine.Object) instance == (UnityEngine.Object) null)
          return;
        int totalMixZeny = 0;
        if (pinID == 10)
          ConceptCardManager.GalcTotalMixZeny(instance.SelectedMaterials, out totalMixZeny);
        else
          ConceptCardManager.GalcTotalMixZenyMaterialData(out totalMixZeny);
        if (totalMixZeny > MonoSingleton<GameManager>.Instance.Player.Gold)
        {
          this.ActivateOutputLinks(1001);
          return;
        }
        this.ActivateOutputLinks(1000);
      }
      if (pinID != 20)
        return;
      ConceptCardManager instance1 = ConceptCardManager.Instance;
      if ((UnityEngine.Object) instance1 == (UnityEngine.Object) null)
        return;
      int mixTotalExp;
      int mixTrustExp;
      ConceptCardManager.CalcTotalExpTrustMaterialData(out mixTotalExp, out mixTrustExp);
      if ((int) instance1.SelectedConceptCardData.Trust + mixTrustExp > MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardTrustMax(instance1.SelectedConceptCardData))
        this.ActivateOutputLinks(2000);
      else
        this.ActivateOutputLinks(1000);
    }
  }
}
