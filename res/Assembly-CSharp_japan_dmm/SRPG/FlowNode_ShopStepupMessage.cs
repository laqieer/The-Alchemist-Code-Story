// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ShopStepupMessage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Shop/ShopStepupMessage", 32741)]
  [FlowNode.Pin(1, "Start", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "Finished", FlowNode.PinTypes.Output, 2)]
  public class FlowNode_ShopStepupMessage : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      if (!ShopBuyStepupInfoData.IsSet)
      {
        this.ActivateOutputLinks(2);
      }
      else
      {
        ShopBuyStepupInfoData.Reset();
        if (string.IsNullOrEmpty(ShopBuyStepupInfoData.ItemName))
          this.ActivateOutputLinks(2);
        else
          UIUtility.SystemMessage(string.Format(LocalizedText.Get("sys.SHOP_STEP_UP_MESSAGE"), (object) ShopBuyStepupInfoData.SoldCount, (object) ShopBuyStepupInfoData.ItemName, (object) ShopBuyStepupInfoData.PriceBefore, (object) ShopBuyStepupInfoData.PriceAfter, (object) ShopBuyStepupInfoData.CurrencyUnit, (object) ShopBuyStepupInfoData.Currency), (UIUtility.DialogResultEvent) (go => this.ActivateOutputLinks(2)), systemModal: true);
      }
    }
  }
}
