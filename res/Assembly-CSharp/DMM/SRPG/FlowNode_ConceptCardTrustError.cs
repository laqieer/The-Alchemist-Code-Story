// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ConceptCardTrustError
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("UI/FlowNode_ConceptCardTrustError", 32741)]
  [FlowNode.Pin(10, "警告チェック開始", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(1000, "全部条件を満たしている", FlowNode.PinTypes.Output, 1000)]
  [FlowNode.Pin(1001, "境界値または限界突破が最大の素材が含まれている", FlowNode.PinTypes.Output, 1001)]
  [FlowNode.Pin(1002, "ベースの境界値または限界突破が最大を超えている", FlowNode.PinTypes.Output, 1002)]
  [FlowNode.Pin(1003, "素材に最大数値のものが含まれており、ベースの最大数値を超えている", FlowNode.PinTypes.Output, 1003)]
  [FlowNode.Pin(1004, "境界値数値が0より大きく未完のものがある", FlowNode.PinTypes.Output, 1004)]
  [FlowNode.Pin(1005, "ダイアログ登録処理", FlowNode.PinTypes.Output, 1005)]
  [FlowNode.Pin(1006, "条件なし", FlowNode.PinTypes.Output, 1006)]
  [FlowNode.Pin(11, "ビジョンクリア回数チェック開始", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(1010, "回数確認OK", FlowNode.PinTypes.Output, 1010)]
  [FlowNode.Pin(1011, "回数確認NO", FlowNode.PinTypes.Output, 1011)]
  public class FlowNode_ConceptCardTrustError : FlowNode
  {
    private const int INPUT_WARNINGCHECK = 10;
    private const int INPUT_TRUSTBONUSCHECK = 11;
    private const int OUTPUT_ALLERROR = 1000;
    private const int OUTPUT_MAXERROE = 1001;
    private const int OUTPUT_BASERROR = 1002;
    private const int OUTPUT_MAXBASERROR = 1003;
    private const int OUTPUT_TRUSTBONUSERROR = 1004;
    private const int OUTPUT_OPENDIALOG = 1005;
    private const int OUTPUT_SUCCESS = 1006;
    private const int OUTPUT_TRUSTBONUSOK = 1010;
    private const int OUTPUT_TRUSTBONUSNO = 1011;

    private void OnConfirmYes(GameObject go) => this.ActivateOutputLinks(1010);

    private void OnConfirmNo(GameObject go) => this.ActivateOutputLinks(1011);

    public override void OnActivate(int pinID)
    {
      ConceptCardManager instance = ConceptCardManager.Instance;
      if (Object.op_Equality((Object) instance, (Object) null))
        return;
      if (pinID == 11)
      {
        bool flag = false;
        int num1 = ConceptCardManager.CalcTotalTrust(instance.SelectedConceptCardData, instance.SelectedMaterials);
        int num2 = ConceptCardManager.CalcTotalTrustBonusMixCount(instance.SelectedConceptCardData, instance.SelectedMaterials);
        int num3 = num1 / (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardTrustMax - num2;
        if (instance.SelectedConceptCardData.TrustBonus < instance.SelectedConceptCardData.AwakeCountCap + 1 && num2 > 0 && num2 > instance.SelectedConceptCardData.TrustBonus)
          flag = true;
        if (flag)
        {
          string empty = string.Empty;
          if (num3 >= 0)
            empty = LocalizedText.Get("sys.CONCEPT_CARD_CONFIRMBONUS_CAUTION", (object) num3.ToString());
          int num4 = ConceptCardManager.CalcTotalAwakeCount(instance.SelectedConceptCardData, instance.SelectedMaterials) + 1;
          if (num4 > instance.SelectedConceptCardData.AwakeCountCap + 1)
            num4 = instance.SelectedConceptCardData.AwakeCountCap + 1;
          int num5 = num4 - (num3 + num2);
          if (num5 < 0)
            num5 = 0;
          UIUtility.ConfirmBox(empty + LocalizedText.Get("sys.CONCEPT_CARD_CONFIRMBONUSCOUNT_CAUTION", (object) num5.ToString()), new UIUtility.DialogResultEvent(this.OnConfirmYes), new UIUtility.DialogResultEvent(this.OnConfirmNo));
        }
        else
          this.ActivateOutputLinks(1010);
      }
      if (pinID != 10)
        return;
      bool flag1 = ConceptCardManager.CalcTotalUnacquiredTrustBonus(instance.SelectedConceptCardData, instance.SelectedMaterials);
      bool flag2 = ConceptCardManager.ContainsTrustMax(instance.SelectedConceptCardData, instance.SelectedMaterials);
      bool flag3 = ConceptCardManager.ContainsAwakeCountMax(instance.SelectedConceptCardData, instance.SelectedMaterials);
      bool flag4 = false;
      int num = ConceptCardManager.CalcTotalAwakeCount(instance.SelectedConceptCardData, instance.SelectedMaterials);
      if (num > instance.SelectedConceptCardData.AwakeCountCap)
        flag4 = true;
      bool flag5 = false;
      if (ConceptCardManager.CalcTotalTrust(instance.SelectedConceptCardData, instance.SelectedMaterials) > (num + 1) * (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardTrustMax)
        flag5 = true;
      if (flag1 && flag2 && flag3 && flag5 && flag4)
        this.ActivateOutputLinks(1000);
      if (flag1)
        this.ActivateOutputLinks(1004);
      if ((flag2 || flag3) && (flag5 || flag4))
        this.ActivateOutputLinks(1003);
      if (flag2 || flag3)
      {
        this.ActivateOutputLinks(1001);
        this.ActivateOutputLinks(1005);
      }
      else
      {
        if (flag5 || flag4)
          this.ActivateOutputLinks(1002);
        if (flag1 || flag2 || flag3 || flag5 || flag4)
          this.ActivateOutputLinks(1005);
        else
          this.ActivateOutputLinks(1006);
      }
    }
  }
}
