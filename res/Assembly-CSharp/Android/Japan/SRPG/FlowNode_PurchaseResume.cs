// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_PurchaseResume
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using Gsc.Purchase;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("Payment/PurchaseResume", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "Failed", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(100, "ConnectingDialogOpen", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "ConnectingDialogClose", FlowNode.PinTypes.Output, 101)]
  public class FlowNode_PurchaseResume : FlowNode
  {
    private bool mSetDelegate;
    private float wait_time;
    private bool succeeded;

    private void RemoveDelegate()
    {
      if (!this.mSetDelegate)
        return;
      MonoSingleton<PaymentManager>.Instance.OnRequestSucceeded -= new PaymentManager.RequestSucceededDelegate(this.OnRequestSucceeded);
      this.mSetDelegate = false;
    }

    ~FlowNode_PurchaseResume()
    {
      this.RemoveDelegate();
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
      this.wait_time = 5f;
      this.succeeded = false;
      if (!this.mSetDelegate)
      {
        MonoSingleton<PaymentManager>.Instance.OnRequestSucceeded += new PaymentManager.RequestSucceededDelegate(this.OnRequestSucceeded);
        this.mSetDelegate = true;
      }
      this.enabled = true;
      MonoSingleton<PurchaseManager>.Instance.Resume();
      this.OpenPurchaseMsg();
    }

    public void Update()
    {
      if (this.succeeded)
      {
        this.enabled = false;
        this.ClosePurchaseMsg();
        this.ActivateOutputLinks(10);
        this.RemoveDelegate();
      }
      else
      {
        this.wait_time -= Time.unscaledDeltaTime;
        if ((double) this.wait_time > 0.0)
          return;
        this.enabled = false;
        this.ClosePurchaseMsg();
        this.ActivateOutputLinks(11);
        this.RemoveDelegate();
      }
    }

    public void OnRequestSucceeded()
    {
      this.succeeded = true;
    }

    public void OpenPurchaseMsg()
    {
      this.ActivateOutputLinks(100);
    }

    public void ClosePurchaseMsg()
    {
      this.ActivateOutputLinks(101);
    }
  }
}
