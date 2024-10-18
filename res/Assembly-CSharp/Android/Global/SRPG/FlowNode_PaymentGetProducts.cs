// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_PaymentGetProducts
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.Pin(101, "Failure", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(103, "Empty", FlowNode.PinTypes.Output, 103)]
  [FlowNode.Pin(102, "WaitingForSetup", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(1, "ClearList", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "Success", FlowNode.PinTypes.Output, 100)]
  [FlowNode.NodeType("Payment/GetProducts", 32741)]
  [FlowNode.Pin(0, "Start", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_PaymentGetProducts : FlowNode
  {
    private bool mSetDelegate;
    public static PaymentManager.Product[] Products;

    private void RemoveDelegate()
    {
      if (!this.mSetDelegate)
        return;
      MonoSingleton<PaymentManager>.Instance.OnShowItems -= new PaymentManager.ShowItemsDelegate(this.OnShowItems);
      this.mSetDelegate = false;
    }

    ~FlowNode_PaymentGetProducts()
    {
      try
      {
        this.RemoveDelegate();
      }
      finally
      {
        // ISSUE: explicit finalizer call
        // ISSUE: explicit non-virtual call
        __nonvirtual (((object) this).Finalize());
      }
    }

    protected override void OnDestroy()
    {
      this.RemoveDelegate();
      base.OnDestroy();
    }

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 0:
          if (!this.mSetDelegate)
          {
            MonoSingleton<PaymentManager>.Instance.OnShowItems += new PaymentManager.ShowItemsDelegate(this.OnShowItems);
            this.mSetDelegate = true;
          }
          this.enabled = true;
          if (MonoSingleton<PaymentManager>.Instance.ShowItems())
            break;
          this.Failure();
          break;
        case 1:
          this.enabled = false;
          FlowNode_PaymentGetProducts.Products = (PaymentManager.Product[]) null;
          this.ActivateOutputLinks(100);
          break;
      }
    }

    private void Success()
    {
      DebugUtility.Log("GetProducts success");
      this.enabled = false;
      this.RemoveDelegate();
      this.ActivateOutputLinks(100);
    }

    private void Failure()
    {
      DebugUtility.LogWarning("GetProducts failure");
      this.enabled = false;
      this.RemoveDelegate();
      this.ActivateOutputLinks(101);
    }

    private void WaitingForSetup()
    {
      DebugUtility.LogWarning("GetProducts waiting for setup");
      this.enabled = false;
      this.RemoveDelegate();
      this.ActivateOutputLinks(102);
    }

    private void Empty()
    {
      DebugUtility.LogWarning("GetProducts empty");
      this.enabled = false;
      this.RemoveDelegate();
      this.ActivateOutputLinks(103);
    }

    public void OnShowItems(PaymentManager.EShowItemsResult result, PaymentManager.Product[] products)
    {
      if (!MonoSingleton<PaymentManager>.Instance.IsAvailable)
        this.WaitingForSetup();
      else if (products == null || products.Length <= 0)
        this.Empty();
      else if (result != PaymentManager.EShowItemsResult.SUCCESS)
      {
        this.Failure();
      }
      else
      {
        FlowNode_PaymentGetProducts.Products = products;
        this.Success();
      }
    }
  }
}
