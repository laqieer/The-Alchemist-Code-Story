﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_GpsGift
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/WebApi/GpsGift", 32741)]
  [FlowNode.Pin(100, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(20, "GPSギフト受け取った", FlowNode.PinTypes.Output, 20)]
  [FlowNode.Pin(21, "GPSギフト受け取り済み", FlowNode.PinTypes.Output, 21)]
  [FlowNode.Pin(22, "GPSギフト受け取れなかった", FlowNode.PinTypes.Output, 22)]
  public class FlowNode_GpsGift : FlowNode_Network
  {
    private FlowNode_GpsGift.RecieveStatus m_RecieveStatus = FlowNode_GpsGift.RecieveStatus.FAILED_NOTRECEIVE;

    public override void OnActivate(int pinID)
    {
      if (pinID != 100)
        return;
      if (Network.Mode == Network.EConnectMode.Online)
      {
        this.ExecRequest((WebAPI) new GpsGift(GlobalVars.Location, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        ((Behaviour) this).enabled = true;
      }
      else
      {
        this.m_RecieveStatus = FlowNode_GpsGift.RecieveStatus.FAILED_NOTRECEIVE;
        this._Failed();
      }
    }

    private void _Success()
    {
      this.ActivateOutputLinks(20);
      ((Behaviour) this).enabled = false;
    }

    private void _Failed()
    {
      switch (this.m_RecieveStatus)
      {
        case FlowNode_GpsGift.RecieveStatus.FAILED_RECEIVED:
          this.ActivateOutputLinks(21);
          break;
        case FlowNode_GpsGift.RecieveStatus.FAILED_NOTRECEIVE:
          this.ActivateOutputLinks(22);
          break;
      }
      Network.RemoveAPI();
      Network.ResetError();
      ((Behaviour) this).enabled = false;
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.NotLocation:
            this.OnBack();
            break;
          case Network.EErrCode.NotGpsMail:
            this.m_RecieveStatus = FlowNode_GpsGift.RecieveStatus.FAILED_NOTRECEIVE;
            this._Failed();
            break;
          case Network.EErrCode.ReceivedGpsMail:
            this.m_RecieveStatus = FlowNode_GpsGift.RecieveStatus.FAILED_RECEIVED;
            this._Failed();
            break;
          default:
            this.OnRetry();
            break;
        }
      }
      else
      {
        Network.RemoveAPI();
        MonoSingleton<GameManager>.Instance.Player.UnreadMailPeriod = true;
        MonoSingleton<GameManager>.Instance.RequestUpdateBadges(GameManager.BadgeTypes.GiftBox);
        this.m_RecieveStatus = FlowNode_GpsGift.RecieveStatus.SUCCESS_RECEIVE;
        this._Success();
      }
    }

    private enum RecieveStatus
    {
      SUCCESS_RECEIVE,
      FAILED_RECEIVED,
      FAILED_NOTRECEIVE,
    }
  }
}