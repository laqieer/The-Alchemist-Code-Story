﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_PaymentCheckChargeLimit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Payment/CheckChargeLimit", 32741)]
  [FlowNode.Pin(0, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "Success", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(200, "Error", FlowNode.PinTypes.Output, 200)]
  [FlowNode.Pin(201, "NonAge", FlowNode.PinTypes.Output, 201)]
  [FlowNode.Pin(202, "NeedCheck", FlowNode.PinTypes.Output, 202)]
  [FlowNode.Pin(203, "NeedBirthday", FlowNode.PinTypes.Output, 203)]
  [FlowNode.Pin(204, "LimitOver", FlowNode.PinTypes.Output, 204)]
  [FlowNode.Pin(205, "VipRemains", FlowNode.PinTypes.Output, 205)]
  [FlowNode.Pin(206, "PremiumRemains", FlowNode.PinTypes.Output, 206)]
  [FlowNode.Pin(207, "PremiumInvaid", FlowNode.PinTypes.Output, 207)]
  [FlowNode.Pin(208, "ExpansionTermRemains", FlowNode.PinTypes.Output, 208)]
  [FlowNode.Pin(209, "CanNotBuyUnlockLv", FlowNode.PinTypes.Output, 209)]
  public class FlowNode_PaymentCheckChargeLimit : FlowNode_Network
  {
    private bool mSetDelegate;

    private void RemoveDelegate()
    {
    }

    ~FlowNode_PaymentCheckChargeLimit()
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
      if (Network.Mode == Network.EConnectMode.Online)
      {
        this.ExecRequest((WebAPI) new ReqChargeCheck(new PaymentManager.Product[1]
        {
          MonoSingleton<PaymentManager>.Instance.GetProduct(GlobalVars.SelectedProductID)
        }, true, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        ((Behaviour) this).enabled = true;
      }
      else
        this.Success();
    }

    private void Success()
    {
      DebugUtility.Log("CheckChargeLimit success");
      ((Behaviour) this).enabled = false;
      this.RemoveDelegate();
      this.ActivateOutputLinks(100);
    }

    private void Failure()
    {
      DebugUtility.Log("CheckChargeLimit failure");
      ((Behaviour) this).enabled = false;
      this.RemoveDelegate();
      this.ActivateOutputLinks(200);
    }

    private void NonAge()
    {
      DebugUtility.Log("CheckChargeLimit nonage");
      ((Behaviour) this).enabled = false;
      this.RemoveDelegate();
      this.ActivateOutputLinks(201);
    }

    private void NeedCheck()
    {
      DebugUtility.Log("CheckChargeLimit needcheck");
      ((Behaviour) this).enabled = false;
      this.RemoveDelegate();
      this.ActivateOutputLinks(202);
    }

    private void NeedBirthday()
    {
      DebugUtility.Log("CheckChargeLimit needbirthday");
      ((Behaviour) this).enabled = false;
      this.RemoveDelegate();
      this.ActivateOutputLinks(203);
    }

    private void LimitOver()
    {
      DebugUtility.Log("CheckChargeLimit limitover");
      ((Behaviour) this).enabled = false;
      this.RemoveDelegate();
      this.ActivateOutputLinks(204);
    }

    private void VipRemains()
    {
      DebugUtility.Log("CheckChargeLimit vipremains");
      ((Behaviour) this).enabled = false;
      this.RemoveDelegate();
      this.ActivateOutputLinks(205);
    }

    private void PremiumRemains()
    {
      DebugUtility.Log("CheckChargeLimit premium remains");
      ((Behaviour) this).enabled = false;
      this.RemoveDelegate();
      this.ActivateOutputLinks(206);
    }

    private void PremiumInvalid()
    {
      DebugUtility.Log("CheckChargeLimit premium invalid");
      ((Behaviour) this).enabled = false;
      this.RemoveDelegate();
      this.ActivateOutputLinks(207);
    }

    private void ExpansionTermRemains()
    {
      DebugUtility.Log("CheckChargeLimit expansion term remains");
      ((Behaviour) this).enabled = false;
      this.RemoveDelegate();
      this.ActivateOutputLinks(208);
    }

    private void CanNotBuyUnlockLv()
    {
      DebugUtility.Log("CheckChargeLimit can not buy unlocklv");
      ((Behaviour) this).enabled = false;
      this.RemoveDelegate();
      this.ActivateOutputLinks(209);
    }

    public void OnCheckChargeLimit(PaymentManager.ECheckChargeLimitResult result)
    {
      switch (result)
      {
        case PaymentManager.ECheckChargeLimitResult.SUCCESS:
          this.Success();
          break;
        case PaymentManager.ECheckChargeLimitResult.NONAGE:
          this.NonAge();
          break;
        case PaymentManager.ECheckChargeLimitResult.NEED_CHECK:
          this.NeedCheck();
          break;
        case PaymentManager.ECheckChargeLimitResult.NEED_BIRTHDAY:
          this.NeedBirthday();
          break;
        case PaymentManager.ECheckChargeLimitResult.LIMIT_OVER:
          this.LimitOver();
          break;
        default:
          this.Failure();
          break;
      }
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        FlowNode_SendLogMessage.PurchaseFlow(string.Empty, "charge/check", "Network.IsError:" + (object) Network.ErrCode);
        Network.EErrCode errCode = Network.ErrCode;
        switch (errCode)
        {
          case Network.EErrCode.ChargePremiumRemains:
            Network.RemoveAPI();
            Network.ResetError();
            this.PremiumRemains();
            break;
          case Network.EErrCode.ChargePremiumInvalid:
            Network.RemoveAPI();
            Network.ResetError();
            this.PremiumInvalid();
            break;
          case Network.EErrCode.ExpansionTermRemains:
            Network.RemoveAPI();
            Network.ResetError();
            this.ExpansionTermRemains();
            break;
          case Network.EErrCode.CanNotBuyUnlockLv:
            Network.RemoveAPI();
            Network.ResetError();
            this.CanNotBuyUnlockLv();
            break;
          default:
            if (errCode != Network.EErrCode.ChargeAge000)
            {
              if (errCode == Network.EErrCode.ChargeVipRemains)
              {
                Network.RemoveAPI();
                Network.ResetError();
                this.VipRemains();
                break;
              }
              Network.RemoveAPI();
              Network.ResetError();
              this.Failure();
              break;
            }
            Network.RemoveAPI();
            Network.ResetError();
            this.NeedBirthday();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<JSON_ChargeCheckResponse> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_ChargeCheckResponse>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        ChargeCheckData chargeCheckData = new ChargeCheckData();
        if (!chargeCheckData.Deserialize(jsonObject.body))
        {
          FlowNode_SendLogMessage.PurchaseFlow(GlobalVars.SelectedProductID, "charge/check", "Deserialize Error");
          this.Failure();
        }
        else
        {
          PaymentManager instance = MonoSingleton<PaymentManager>.Instance;
          if (0 < chargeCheckData.RejectIds.Length)
          {
            FlowNode_SendLogMessage.PurchaseFlow(GlobalVars.SelectedProductID, "charge/check", "LimitOver");
            this.LimitOver();
          }
          else if (20 > chargeCheckData.Age)
          {
            FlowNode_SendLogMessage.PurchaseFlow(string.Empty, "charge/check", "NonAge");
            this.NonAge();
          }
          else if (!instance.IsAgree())
          {
            FlowNode_SendLogMessage.PurchaseFlow(string.Empty, "charge/check", "NeedCheck");
            this.NeedCheck();
          }
          else
          {
            FlowNode_SendLogMessage.PurchaseFlow(string.Empty, "charge/check", "Success");
            this.Success();
          }
        }
      }
    }
  }
}
