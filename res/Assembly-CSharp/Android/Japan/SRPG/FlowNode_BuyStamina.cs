﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_BuyStamina
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("System/Buy/BuyStamina", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(3, "スタミナ満タン", FlowNode.PinTypes.Output, 3)]
  [FlowNode.Pin(4, "コインが足りなかった", FlowNode.PinTypes.Output, 4)]
  [FlowNode.Pin(5, "購入回数制限", FlowNode.PinTypes.Output, 5)]
  public class FlowNode_BuyStamina : FlowNode_Network
  {
    public static GameObject ConfirmBoxObj;
    public bool Confirm;
    public bool ShowResult;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0 || this.enabled)
        return;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      if (this.Confirm)
      {
        FixParam fixParam = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
        FlowNode_BuyStamina.ConfirmBoxObj = UIUtility.ConfirmBox(LocalizedText.Get("sys.RESET_STAMINA", (object) player.GetStaminaRecoveryCost(false), (object) fixParam.StaminaAdd), new UIUtility.DialogResultEvent(this.OnBuy), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1, (string) null, (string) null);
      }
      else
        this.SendRequest();
    }

    private void OutOfCoin()
    {
      FixParam fixParam = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
      UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.OUTOFCOIN", (object) fixParam.BuyGoldCost, (object) fixParam.BuyGoldAmount), (UIUtility.DialogResultEvent) (go => {}), (GameObject) null, false, -1);
    }

    private void StaminaFull()
    {
      UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.STAMINAFULL"), (UIUtility.DialogResultEvent) (go => {}), (GameObject) null, false, -1);
    }

    private void OutOfBuyCount()
    {
      UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.STAMINA_BUY_LIMIT"), (UIUtility.DialogResultEvent) (go => {}), (GameObject) null, false, -1);
    }

    private void OnBuy(GameObject go)
    {
      this.SendRequest();
    }

    private void SendRequest()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      if (player.StaminaStockCap <= player.Stamina)
      {
        if (this.ShowResult)
          this.StaminaFull();
        this.enabled = false;
        this.ActivateOutputLinks(3);
      }
      else if (player.Coin < player.GetStaminaRecoveryCost(false))
      {
        if (this.ShowResult)
          this.OutOfCoin();
        this.enabled = false;
        this.ActivateOutputLinks(4);
      }
      else if (MonoSingleton<GameManager>.Instance.MasterParam.GetVipBuyStaminaLimit(player.VipRank) <= player.StaminaBuyNum)
      {
        if (this.ShowResult)
          this.OutOfBuyCount();
        this.enabled = false;
        this.ActivateOutputLinks(5);
      }
      else if (Network.Mode == Network.EConnectMode.Online)
      {
        this.ExecRequest((WebAPI) new ReqItemAddStmPaid(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        this.enabled = true;
      }
      else
      {
        player.DEBUG_CONSUME_COIN(player.GetStaminaRecoveryCost(false));
        player.DEBUG_REPAIR_STAMINA();
        this.Success();
      }
    }

    private void Success()
    {
      this.enabled = false;
      MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_0011", 0.0f);
      this.ActivateOutputLinks(1);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        if (Network.ErrCode == Network.EErrCode.StaminaCoinShort)
        {
          if (this.ShowResult)
            this.OutOfCoin();
          this.OnBack();
        }
        else
          this.OnRetry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.OnRetry();
        }
        else
        {
          int staminaRecoveryCost = MonoSingleton<GameManager>.Instance.Player.GetStaminaRecoveryCost(true);
          PlayerData.EDeserializeFlags flag = (PlayerData.EDeserializeFlags) (0 | 2 | 4);
          if (!MonoSingleton<GameManager>.Instance.Player.Deserialize(jsonObject.body.player, flag))
          {
            this.OnRetry();
          }
          else
          {
            Network.RemoveAPI();
            MyMetaps.TrackSpendCoin("BuyStamina", staminaRecoveryCost);
            if (this.ShowResult)
              UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.STAMINARECOVERED", new object[1]
              {
                (object) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.StaminaAdd
              }), (UIUtility.DialogResultEvent) (go => {}), (GameObject) null, false, -1);
            this.Success();
          }
        }
      }
    }
  }
}
