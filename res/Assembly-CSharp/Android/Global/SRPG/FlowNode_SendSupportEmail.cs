// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SendSupportEmail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.NodeType("Network/SendSupportEmail", 32741)]
  [FlowNode.Pin(1, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(2, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(3, "Failure", FlowNode.PinTypes.Output, 2)]
  public class FlowNode_SendSupportEmail : FlowNode_Network
  {
    [SerializeField]
    private Text messageSubject;
    [SerializeField]
    private Text messageBody;
    [SerializeField]
    private Text email;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      string str = string.Format("<br>Device Language: {0}<br>Connection Type:{1}", (object) ((Enum) Application.systemLanguage).ToString(), (object) ((Enum) Application.internetReachability).ToString());
      string text = this.messageBody.text;
      string message;
      if (PlayerPrefs.HasKey("PlayerName"))
        message = "App Version: " + MyApplicationPlugin.version + "<br>Device Model: " + SystemInfo.deviceModel + "<br>OS: " + SystemInfo.operatingSystem + "<br>Player Name: " + PlayerPrefs.GetString("PlayerName") + str + "<br><br>" + this.messageBody.text;
      else
        message = "App Version: " + MyApplicationPlugin.version + "<br>Device Model: " + SystemInfo.deviceModel + "<br>OS: " + SystemInfo.operatingSystem + str + "<br><br>" + this.messageBody.text;
      this.ExecRequest((WebAPI) new ReqSendSupportEmail(this.messageSubject.text, message, this.email.text, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
    }

    private void Failure()
    {
      Network.RemoveAPI();
      this.ActivateOutputLinks(3);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        this.OnRetry();
      }
      else
      {
        Network.RemoveAPI();
        this.ActivateOutputLinks(2);
        this.enabled = false;
      }
    }
  }
}
