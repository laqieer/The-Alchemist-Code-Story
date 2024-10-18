// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_RequestShopItems
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/Shop/RequestShopItems", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(100, "ショップ期間外", FlowNode.PinTypes.Output, 100)]
  public class FlowNode_RequestShopItems : FlowNode_Network
  {
    private const int PIN_OUT_SHOP_BUY_OUTOF_PERIOD = 100;
    private EShopType mShopType;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0 || ((Behaviour) this).enabled)
        return;
      if (Network.Mode == Network.EConnectMode.Online)
      {
        this.mShopType = GlobalVars.ShopType;
        string str = this.mShopType.ToString();
        if (this.mShopType == EShopType.Guerrilla)
          this.ExecRequest((WebAPI) new ReqItemGuerrillaShop(str, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        else
          this.ExecRequest((WebAPI) new ReqItemShop(str, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        ((Behaviour) this).enabled = true;
      }
      else
        this.Success();
    }

    private void Success()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(1);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        if (Network.ErrCode == Network.EErrCode.ShopBuyOutofPeriod)
        {
          UIUtility.SystemMessage((string) null, Network.ErrMsg, (UIUtility.DialogResultEvent) (go => this.ActivateOutputLinks(100)));
          Network.RemoveAPI();
          Network.ResetError();
        }
        else
          this.OnRetry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_ShopResponse> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_ShopResponse>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.OnRetry();
        }
        else
        {
          Network.RemoveAPI();
          ShopData shop = MonoSingleton<GameManager>.Instance.Player.GetShopData(this.mShopType) ?? new ShopData();
          if (!shop.Deserialize(jsonObject.body))
          {
            this.OnFailed();
          }
          else
          {
            MonoSingleton<GameManager>.Instance.Player.SetShopData(this.mShopType, shop);
            this.Success();
          }
        }
      }
    }
  }
}
