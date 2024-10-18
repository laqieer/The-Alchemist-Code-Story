// Decompiled with JetBrains decompiler
// Type: SRPG.ProductPremium
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [AddComponentMenu("Payment/ProductPremium")]
  [FlowNode.Pin(0, "表示", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "選択された", FlowNode.PinTypes.Output, 0)]
  public class ProductPremium : MonoBehaviour, IFlowInterface
  {
    [Description("ボタンとして使用するゲームオブジェクト")]
    public Button PremiumButton;

    public void Activated(int pinID)
    {
      if (pinID != 0)
        return;
      this.RefreshItems(false);
    }

    private void Awake()
    {
      ((Behaviour) this).enabled = true;
      if (!Object.op_Inequality((Object) this.PremiumButton, (Object) null) || !((Component) this.PremiumButton).gameObject.activeInHierarchy)
        return;
      ((Component) this.PremiumButton).gameObject.SetActive(false);
    }

    private void Start() => this.RefreshItems(true);

    private void RefreshItems(bool is_start)
    {
      PaymentManager.Product[] product = BuyCoinManager.Instance.GetProduct();
      if (Object.op_Equality((Object) this.PremiumButton, (Object) null) || product == null)
        return;
      for (int index = 0; index < product.Length; ++index)
      {
        if (product[index].productID == (string) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.PremiumProduct)
        {
          DataSource.Bind<PaymentManager.Product>(((Component) this.PremiumButton).gameObject, product[index]);
          if (!is_start)
          {
            ListItemEvents component = ((Component) this.PremiumButton).gameObject.GetComponent<ListItemEvents>();
            if (Object.op_Inequality((Object) component, (Object) null))
              component.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelectItem);
          }
          ((Component) this.PremiumButton).gameObject.gameObject.SetActive(true);
        }
      }
    }

    private void OnSelectItem(GameObject go)
    {
      PaymentManager.Product dataOfClass = DataSource.FindDataOfClass<PaymentManager.Product>(go, (PaymentManager.Product) null);
      if (!BuyCoinManager.Instance.GetProductBuyConfirm(dataOfClass))
        return;
      GlobalVars.SelectedProductID = dataOfClass.productID;
      GlobalVars.SelectedProductIname = dataOfClass.ID;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }

    private void Update()
    {
    }
  }
}
