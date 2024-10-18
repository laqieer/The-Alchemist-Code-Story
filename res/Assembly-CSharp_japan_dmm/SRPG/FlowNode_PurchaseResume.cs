﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_PurchaseResume
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using Gsc.Purchase;
using UnityEngine;

#nullable disable
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
      this.wait_time = 5f;
      this.succeeded = false;
      if (!this.mSetDelegate)
      {
        MonoSingleton<PaymentManager>.Instance.OnRequestSucceeded += new PaymentManager.RequestSucceededDelegate(this.OnRequestSucceeded);
        this.mSetDelegate = true;
      }
      ((Behaviour) this).enabled = true;
      MonoSingleton<PurchaseManager>.Instance.Resume();
      this.OpenPurchaseMsg();
    }

    public void Update()
    {
      if (this.succeeded)
      {
        ((Behaviour) this).enabled = false;
        this.ClosePurchaseMsg();
        this.ActivateOutputLinks(10);
        this.RemoveDelegate();
      }
      else
      {
        this.wait_time -= Time.unscaledDeltaTime;
        if ((double) this.wait_time > 0.0)
          return;
        ((Behaviour) this).enabled = false;
        this.ClosePurchaseMsg();
        this.ActivateOutputLinks(11);
        this.RemoveDelegate();
      }
    }

    public void OnRequestSucceeded() => this.succeeded = true;

    public void OpenPurchaseMsg() => this.ActivateOutputLinks(100);

    public void ClosePurchaseMsg() => this.ActivateOutputLinks(101);
  }
}
