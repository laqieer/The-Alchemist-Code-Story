// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_PaymentCheckChargeLimit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;

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
  public class FlowNode_PaymentCheckChargeLimit : FlowNode_Network
  {
    private bool mSetDelegate;

    private void RemoveDelegate()
    {
    }

    ~FlowNode_PaymentCheckChargeLimit()
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
      if (Network.Mode == Network.EConnectMode.Online)
      {
        this.ExecRequest((WebAPI) new ReqChargeCheck(new PaymentManager.Product[1]
        {
          MonoSingleton<PaymentManager>.Instance.GetProduct(GlobalVars.SelectedProductID)
        }, true, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        this.enabled = true;
      }
      else
        this.Success();
    }

    private void Success()
    {
      DebugUtility.Log("CheckChargeLimit success");
      this.enabled = false;
      this.RemoveDelegate();
      this.ActivateOutputLinks(100);
    }

    private void Failure()
    {
      DebugUtility.Log("CheckChargeLimit failure");
      this.enabled = false;
      this.RemoveDelegate();
      this.ActivateOutputLinks(200);
    }

    private void NonAge()
    {
      DebugUtility.Log("CheckChargeLimit nonage");
      this.enabled = false;
      this.RemoveDelegate();
      this.ActivateOutputLinks(201);
    }

    private void NeedCheck()
    {
      DebugUtility.Log("CheckChargeLimit needcheck");
      this.enabled = false;
      this.RemoveDelegate();
      this.ActivateOutputLinks(202);
    }

    private void NeedBirthday()
    {
      DebugUtility.Log("CheckChargeLimit needbirthday");
      this.enabled = false;
      this.RemoveDelegate();
      this.ActivateOutputLinks(203);
    }

    private void LimitOver()
    {
      DebugUtility.Log("CheckChargeLimit limitover");
      this.enabled = false;
      this.RemoveDelegate();
      this.ActivateOutputLinks(204);
    }

    private void VipRemains()
    {
      DebugUtility.Log("CheckChargeLimit vipremains");
      this.enabled = false;
      this.RemoveDelegate();
      this.ActivateOutputLinks(205);
    }

    private void PremiumRemains()
    {
      DebugUtility.Log("CheckChargeLimit premium remains");
      this.enabled = false;
      this.RemoveDelegate();
      this.ActivateOutputLinks(206);
    }

    private void PremiumInvalid()
    {
      DebugUtility.Log("CheckChargeLimit premium invalid");
      this.enabled = false;
      this.RemoveDelegate();
      this.ActivateOutputLinks(207);
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
        switch (Network.ErrCode)
        {
          case Network.EErrCode.ChargeAge000:
            Network.RemoveAPI();
            Network.ResetError();
            this.NeedBirthday();
            return;
          case Network.EErrCode.ChargeVipRemains:
            Network.RemoveAPI();
            Network.ResetError();
            this.VipRemains();
            return;
          case Network.EErrCode.ChargePremiumRemains:
            Network.RemoveAPI();
            Network.ResetError();
            this.PremiumRemains();
            break;
          case Network.EErrCode.ChargePremiumInvalid:
            Network.RemoveAPI();
            Network.ResetError();
            this.PremiumInvalid();
            return;
          default:
            this.Failure();
            return;
        }
      }
      WebAPI.JSON_BodyResponse<JSON_ChargeCheckResponse> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_ChargeCheckResponse>>(www.text);
      DebugUtility.Assert(jsonObject != null, "res == null");
      Network.RemoveAPI();
      ChargeCheckData chargeCheckData = new ChargeCheckData();
      if (!chargeCheckData.Deserialize(jsonObject.body))
      {
        this.Failure();
      }
      else
      {
        PaymentManager instance = MonoSingleton<PaymentManager>.Instance;
        if (0 < chargeCheckData.RejectIds.Length)
          this.LimitOver();
        else if (20 > chargeCheckData.Age)
          this.NonAge();
        else if (!instance.IsAgree())
          this.NeedCheck();
        else
          this.Success();
      }
    }
  }
}
