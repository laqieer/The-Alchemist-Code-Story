// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_PaymentFgGDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Payment/FgGDialog", 32741)]
  [FlowNode.Pin(1, "OpenYesNo", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "Cancel", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(3, "FgG連携を開く", FlowNode.PinTypes.Output, 3)]
  [FlowNode.Pin(10, "処理なし", FlowNode.PinTypes.Output, 10)]
  public class FlowNode_PaymentFgGDialog : FlowNode
  {
    private const int PIN_IN_OPEN_DIALOG = 1;
    private const int PIN_OUT_CANCEL = 2;
    private const int PIN_OUT_OPEN_FGG = 3;
    private const int PIN_OUT_NOTING = 10;
    [SerializeField]
    private string mSysTitle = "sys.TITLE_FGG_PURCHASE";
    [SerializeField]
    private string mSysText = "sys.REGIST_FGG_COOPERATION";
    [SerializeField]
    private string mSysAuthButton = "sys.BUTTON_FGG_SYNC";
    [SerializeField]
    private string mSysCancelButton = "sys.CMD_CANCEL";
    private GameObject winGO;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      BuyCoinProductParam coinProductParam = MonoSingleton<GameManager>.Instance.MasterParam.GetBuyCoinProductParam(GlobalVars.SelectedProductIname);
      if (coinProductParam != null && coinProductParam.GetProductShopType() == BuyCoinManager.BuyCoinShopType.FGG && !MonoSingleton<GameManager>.Instance.IsFgGAuthSync())
        this.OpenDialog();
      else
        this.ActivateOutputLinks(10);
    }

    private void GenerateProductMessage(
      ref string title,
      ref string text,
      ref string auth,
      ref string cancel)
    {
      title = LocalizedText.Get(this.mSysTitle);
      text = LocalizedText.Get(this.mSysText);
      auth = LocalizedText.Get(this.mSysAuthButton);
      cancel = LocalizedText.Get(this.mSysCancelButton);
    }

    private void OpenDialog()
    {
      string text = (string) null;
      string title = (string) null;
      string auth = (string) null;
      string cancel = (string) null;
      this.GenerateProductMessage(ref title, ref text, ref auth, ref cancel);
      if (string.IsNullOrEmpty(title))
        this.winGO = UIUtility.ConfirmBox(text, new UIUtility.DialogResultEvent(this.OnClickOK), new UIUtility.DialogResultEvent(this.OnClickCancel));
      else
        this.winGO = UIUtility.ConfirmBoxTitle(title, text, new UIUtility.DialogResultEvent(this.OnClickOK), new UIUtility.DialogResultEvent(this.OnClickCancel), yesText: auth, noText: cancel);
    }

    private void OnClickOK(GameObject go)
    {
      if (Object.op_Equality((Object) this.winGO, (Object) null))
        return;
      this.winGO = (GameObject) null;
      this.ActivateOutputLinks(3);
    }

    private void OnClickCancel(GameObject go)
    {
      if (Object.op_Equality((Object) this.winGO, (Object) null))
        return;
      this.winGO = (GameObject) null;
      this.ActivateOutputLinks(2);
    }
  }
}
