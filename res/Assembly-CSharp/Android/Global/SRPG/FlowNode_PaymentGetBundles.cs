// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_PaymentGetBundles
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections;
using System.Diagnostics;

namespace SRPG
{
  [FlowNode.Pin(100, "Success", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(102, "WaitingForSetup", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(103, "Empty", FlowNode.PinTypes.Output, 103)]
  [FlowNode.Pin(0, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("Payment/GetBundles", 32741)]
  [FlowNode.Pin(101, "Failure", FlowNode.PinTypes.Output, 101)]
  public class FlowNode_PaymentGetBundles : FlowNode
  {
    private bool mSetDelegate;
    public static PaymentManager.Bundle[] Bundles;

    private void RemoveDelegate()
    {
      if (!this.mSetDelegate)
        return;
      MonoSingleton<PaymentManager>.Instance.OnShowBundles -= new PaymentManager.ShowBundlesDelegate(this.OnShowBundles);
      this.mSetDelegate = false;
    }

    ~FlowNode_PaymentGetBundles()
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
      if (pinID != 0)
        return;
      if (!this.mSetDelegate)
      {
        MonoSingleton<PaymentManager>.Instance.OnShowBundles += new PaymentManager.ShowBundlesDelegate(this.OnShowBundles);
        this.mSetDelegate = true;
      }
      this.enabled = true;
      if (MonoSingleton<PaymentManager>.Instance.ShowBundles())
        return;
      this.Failure();
    }

    private void Success()
    {
      DebugUtility.Log("GetBundles success");
      this.enabled = false;
      this.RemoveDelegate();
      this.ActivateOutputLinks(100);
    }

    private void Failure()
    {
      DebugUtility.LogWarning("GetBundles failure");
      this.enabled = false;
      this.RemoveDelegate();
      this.ActivateOutputLinks(101);
    }

    private void WaitingForSetup()
    {
      DebugUtility.LogWarning("GetBundles waiting for setup");
      this.enabled = false;
      this.RemoveDelegate();
      this.ActivateOutputLinks(102);
    }

    private void Empty()
    {
      DebugUtility.LogWarning("GetBundles empty");
      this.enabled = false;
      this.RemoveDelegate();
      this.ActivateOutputLinks(103);
    }

    public void OnShowBundles(PaymentManager.EShowItemsResult result, PaymentManager.Bundle[] bundles)
    {
      if (!MonoSingleton<PaymentManager>.Instance.IsBundleAvailable)
        this.WaitingForSetup();
      else if (bundles == null || bundles.Length <= 0)
        this.Empty();
      else if (result != PaymentManager.EShowItemsResult.SUCCESS)
      {
        this.Failure();
      }
      else
      {
        FlowNode_PaymentGetBundles.Bundles = bundles;
        this.StartCoroutine(this.LoadIcons(bundles));
      }
    }

    [DebuggerHidden]
    private IEnumerator LoadIcons(PaymentManager.Bundle[] bundles)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_PaymentGetBundles.\u003CLoadIcons\u003Ec__Iterator42() { bundles = bundles, \u003C\u0024\u003Ebundles = bundles, \u003C\u003Ef__this = this };
    }
  }
}
