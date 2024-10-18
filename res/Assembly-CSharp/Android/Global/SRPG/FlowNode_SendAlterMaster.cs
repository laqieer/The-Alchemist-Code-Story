﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SendAlterMaster
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("System/SendAlterCheck", 32741)]
  [FlowNode.Pin(10, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "Failed", FlowNode.PinTypes.Output, 11)]
  public class FlowNode_SendAlterMaster : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (!string.IsNullOrEmpty(MonoSingleton<GameManager>.GetInstanceDirect().DigestHash) && !string.IsNullOrEmpty(MonoSingleton<GameManager>.GetInstanceDirect().AlterCheckHash))
      {
        this.ExecRequest((WebAPI) new ReqSendAlterData(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        this.enabled = true;
      }
      else
        this.Success();
    }

    private void Success()
    {
      MonoSingleton<GameManager>.GetInstanceDirect().AlterCheckHash = (string) null;
      MonoSingleton<GameManager>.GetInstanceDirect().DigestHash = (string) null;
      MonoSingleton<GameManager>.GetInstanceDirect().PrevCheckHash = (string) null;
      this.enabled = false;
      this.ActivateOutputLinks(10);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        Network.EErrCode errCode = Network.ErrCode;
      }
      Network.RemoveAPI();
      PlayerPrefsUtility.SetString(PlayerPrefsUtility.ALTER_PREV_CHECK_HASH, MonoSingleton<GameManager>.Instance.AlterCheckHash, false);
      this.Success();
    }
  }
}
