// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqGachaDiscount
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/Gacha/Discount", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "CanNotDiscount", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(12, "AlreadyDiscount", FlowNode.PinTypes.Output, 12)]
  public class FlowNode_ReqGachaDiscount : FlowNode_Network
  {
    private const int PIN_IN_REQUEST = 0;
    private const int PIN_OT_SUCCESS = 10;
    private const int PIN_OT_CANNOT_DISCOUNT = 11;
    private const int PIN_OT_ALREADY_DISCOUNT = 12;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0 || ((Behaviour) this).enabled)
        return;
      string selectDiscountGachaId = GachaWindow.SelectDiscountGachaID;
      string selectDiscountGroupId = GachaWindow.SelectDiscountGroupID;
      if (string.IsNullOrEmpty(selectDiscountGachaId) || string.IsNullOrEmpty(selectDiscountGroupId))
        return;
      string discount_item_iname = FlowNode_Variable.Get("USE_TICKET_INAME");
      this.ExecRequest((WebAPI) new ReqGachaDiscount(selectDiscountGachaId, selectDiscountGroupId, discount_item_iname, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      ((Behaviour) this).enabled = true;
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.CanNotApplyDiscount:
            ((Behaviour) this).enabled = false;
            Network.RemoveAPI();
            Network.ResetError();
            this.ActivateOutputLinks(11);
            break;
          case Network.EErrCode.AlreadyApplyDiscount:
            ((Behaviour) this).enabled = false;
            Network.RemoveAPI();
            Network.ResetError();
            this.ActivateOutputLinks(12);
            break;
          default:
            this.OnRetry();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<ReqGachaDiscount.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqGachaDiscount.Response>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        if (jsonObject.body == null)
          return;
        try
        {
          Json_GachaList json = new Json_GachaList();
          json.gachas = jsonObject.body.gachas;
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.items);
          MonoSingleton<GameManager>.Instance.Deserialize(json, true);
        }
        catch (Exception ex)
        {
          DebugUtility.LogError(ex.ToString());
          return;
        }
        this.Success();
      }
    }

    private void Success()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(10);
    }
  }
}
