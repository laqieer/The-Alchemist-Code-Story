// Decompiled with JetBrains decompiler
// Type: SRPG.ProductPremium
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

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
      this.enabled = true;
      if (!((UnityEngine.Object) this.PremiumButton != (UnityEngine.Object) null) || !this.PremiumButton.gameObject.activeInHierarchy)
        return;
      this.PremiumButton.gameObject.SetActive(false);
    }

    private void Start()
    {
      this.RefreshItems(true);
    }

    private void RefreshItems(bool is_start)
    {
      PaymentManager.Product[] products = FlowNode_PaymentGetProducts.Products;
      if ((UnityEngine.Object) this.PremiumButton == (UnityEngine.Object) null || products == null)
        return;
      for (int index = 0; index < products.Length; ++index)
      {
        if (products[index].productID == (string) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.PremiumProduct)
        {
          DataSource.Bind<PaymentManager.Product>(this.PremiumButton.gameObject, products[index], false);
          if (!is_start)
          {
            ListItemEvents component = this.PremiumButton.gameObject.GetComponent<ListItemEvents>();
            if ((UnityEngine.Object) component != (UnityEngine.Object) null)
              component.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelectItem);
          }
          this.PremiumButton.gameObject.gameObject.SetActive(true);
        }
      }
    }

    private void OnSelectItem(GameObject go)
    {
      PaymentManager.Product dataOfClass = DataSource.FindDataOfClass<PaymentManager.Product>(go, (PaymentManager.Product) null);
      if (dataOfClass == null)
        return;
      GlobalVars.SelectedProductID = dataOfClass.productID;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }

    private void Update()
    {
    }
  }
}
