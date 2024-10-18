// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_RequestLimitedShopItems
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("System/RequestLimitedShopItems", 32741)]
  [FlowNode.Pin(11, "Period", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_RequestLimitedShopItems : FlowNode_Network
  {
    public const string ErrorWindowPrefabPath = "e/UI/NetworkErrorWindowEx";

    public override void OnActivate(int pinID)
    {
      if (pinID != 0 || this.enabled)
        return;
      if (Network.Mode == Network.EConnectMode.Online)
      {
        this.ExecRequest((WebAPI) new ReqLimitedShopItemList(GlobalVars.LimitedShopItem.shops.gname, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        this.enabled = true;
      }
      else
        this.Success();
    }

    private void Success()
    {
      this.enabled = false;
      this.ActivateOutputLinks(1);
    }

    private void Period()
    {
      if (Network.IsImmediateMode)
        return;
      UnityEngine.Object.Instantiate<NetworkErrorWindowEx>(Resources.Load<NetworkErrorWindowEx>("e/UI/NetworkErrorWindowEx")).Body = Network.ErrMsg;
    }

    private void OnPeriod()
    {
      this.Period();
      Network.RemoveAPI();
      Network.ResetError();
      this.enabled = false;
      this.ActivateOutputLinks(11);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        if (Network.ErrCode == Network.EErrCode.LimitedShopOutOfPeriod)
          this.OnPeriod();
        else
          this.OnRetry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_LimitedShopResponse> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_LimitedShopResponse>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.OnRetry();
        }
        else
        {
          Network.RemoveAPI();
          LimitedShopData shop = MonoSingleton<GameManager>.Instance.Player.GetLimitedShopData() ?? new LimitedShopData();
          if (!shop.Deserialize(jsonObject.body))
          {
            this.OnFailed();
          }
          else
          {
            MonoSingleton<GameManager>.Instance.Player.SetLimitedShopData(shop);
            this.Success();
          }
        }
      }
    }
  }
}
