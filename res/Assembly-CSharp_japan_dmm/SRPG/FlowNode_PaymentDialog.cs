// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_PaymentDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Payment/Dialog", 32741)]
  [FlowNode.Pin(1, "OpenYesNo", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "Yes", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(3, "No", FlowNode.PinTypes.Output, 3)]
  [FlowNode.Pin(10, "OpenDetail", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(11, "CloseDetail", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(20, "OpenExpr", FlowNode.PinTypes.Input, 20)]
  [FlowNode.Pin(21, "CloseExpr", FlowNode.PinTypes.Output, 21)]
  public class FlowNode_PaymentDialog : FlowNode
  {
    private const int PIN_IN_YESNO_DIALOG = 1;
    private const int PIN_OUT_YES = 2;
    private const int PIN_OUT_NO = 3;
    private const int PIN_IN_DETAIL_DIALOG = 10;
    private const int PIN_OUT_DETAIL_CLOSE = 11;
    private const int PIN_IN_EXPR_DIALOG = 20;
    private const int PIN_OUT_EXPR_CLOSE = 21;
    [SerializeField]
    private string CoinShopTitle;
    [SerializeField]
    private string CoinShopText;
    [SerializeField]
    private string ExpansionShopTitle;
    [SerializeField]
    private string ExpansionShopText;
    [SerializeField]
    private string FgGShopTitle;
    [SerializeField]
    private string FgGShopText;
    [SerializeField]
    private bool richTag;
    private GameObject winGO;

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 1:
          this.OpenYesNoDialog();
          break;
        case 10:
          this.OpenDetailDialog();
          break;
        case 20:
          this.OpenExprDialog();
          break;
      }
    }

    private void GenerateProductMessage(ref string title, ref string text)
    {
      BuyCoinProductParam coinProductParam = MonoSingleton<GameManager>.Instance.MasterParam.GetBuyCoinProductParam(GlobalVars.SelectedProductIname);
      if (coinProductParam == null)
        return;
      switch (coinProductParam.GetProductShopType())
      {
        case BuyCoinManager.BuyCoinShopType.EXPANSION:
          text = LocalizedText.Get(this.ExpansionShopText);
          if (this.richTag)
            text = LocalizedText.ReplaceTag(text);
          title = LocalizedText.Get(this.ExpansionShopTitle);
          break;
        case BuyCoinManager.BuyCoinShopType.FGG:
          text = LocalizedText.Get(this.FgGShopText);
          if (this.richTag)
            text = LocalizedText.ReplaceTag(text);
          title = LocalizedText.Get(this.FgGShopTitle);
          break;
        default:
          text = LocalizedText.Get(this.CoinShopText);
          if (this.richTag)
            text = LocalizedText.ReplaceTag(text);
          title = LocalizedText.Get(this.CoinShopTitle);
          break;
      }
    }

    private void GenerateExprMessage(ref string title, ref string text)
    {
      BuyCoinProductParam coinProductParam = MonoSingleton<GameManager>.Instance.MasterParam.GetBuyCoinProductParam(GlobalVars.SelectedProductIname);
      if (coinProductParam == null)
        return;
      switch (coinProductParam.GetProductShopType())
      {
        case BuyCoinManager.BuyCoinShopType.EXPANSION:
          title = LocalizedText.Get(this.ExpansionShopTitle);
          break;
        case BuyCoinManager.BuyCoinShopType.FGG:
          title = LocalizedText.Get(this.FgGShopTitle);
          break;
        default:
          title = LocalizedText.Get(this.CoinShopTitle);
          break;
      }
      text = coinProductParam.Expr;
    }

    private void OpenYesNoDialog()
    {
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      this.GenerateProductMessage(ref empty1, ref empty2);
      if (string.IsNullOrEmpty(empty1))
        this.winGO = UIUtility.ConfirmBox(empty2, new UIUtility.DialogResultEvent(this.OnClickOK), new UIUtility.DialogResultEvent(this.OnClickCancel));
      else
        this.winGO = UIUtility.ConfirmBoxTitle(empty1, empty2, new UIUtility.DialogResultEvent(this.OnClickOK), new UIUtility.DialogResultEvent(this.OnClickCancel));
    }

    private void OnClickOK(GameObject go)
    {
      if (Object.op_Equality((Object) this.winGO, (Object) null))
        return;
      this.winGO = (GameObject) null;
      this.ActivateOutputLinks(2);
    }

    private void OnClickCancel(GameObject go)
    {
      if (Object.op_Equality((Object) this.winGO, (Object) null))
        return;
      this.winGO = (GameObject) null;
      this.ActivateOutputLinks(3);
    }

    private void OpenDetailDialog()
    {
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      this.GenerateProductMessage(ref empty1, ref empty2);
      this.winGO = UIUtility.SystemMessage(empty1, empty2, new UIUtility.DialogResultEvent(this.OnClickDetailOK));
    }

    private void OnClickDetailOK(GameObject go)
    {
      if (Object.op_Equality((Object) this.winGO, (Object) null))
        return;
      this.winGO = (GameObject) null;
      this.ActivateOutputLinks(11);
    }

    private void OpenExprDialog()
    {
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      this.GenerateExprMessage(ref empty1, ref empty2);
      if (string.IsNullOrEmpty(empty2))
        this.ActivateOutputLinks(21);
      else
        this.winGO = UIUtility.SystemMessage(empty1, empty2, new UIUtility.DialogResultEvent(this.OnClickExprOK));
    }

    private void OnClickExprOK(GameObject go)
    {
      if (Object.op_Equality((Object) this.winGO, (Object) null))
        return;
      this.winGO = (GameObject) null;
      this.ActivateOutputLinks(21);
    }
  }
}
