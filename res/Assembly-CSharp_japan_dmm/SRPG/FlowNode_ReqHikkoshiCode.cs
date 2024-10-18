// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqHikkoshiCode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Network/gauth_passcode", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_ReqHikkoshiCode : FlowNode_Network
  {
    public Text HikkoshiCodeText;
    public Text ExpireTimeText;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (Network.Mode == Network.EConnectMode.Offline)
      {
        this.Success();
      }
      else
      {
        this.ExecRequest((WebAPI) new ReqGAuthPasscode(MonoSingleton<GameManager>.Instance.SecretKey, MonoSingleton<GameManager>.Instance.DeviceId, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        ((Behaviour) this).enabled = true;
      }
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
        int errCode = (int) Network.ErrCode;
        this.OnRetry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<JSON_PassCode> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_PassCode>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.OnRetry();
        }
        else
        {
          Network.RemoveAPI();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.HikkoshiCodeText, (UnityEngine.Object) null))
            this.HikkoshiCodeText.text = jsonObject.body.passcode;
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ExpireTimeText, (UnityEngine.Object) null))
          {
            DateTime dateTime = DateTime.Now.AddSeconds((double) jsonObject.body.expires_in);
            this.ExpireTimeText.text = string.Format(LocalizedText.Get("sys.HIKKOSHICODE_EXPIRETIME"), (object) dateTime.Year, (object) dateTime.Month, (object) dateTime.Day, (object) dateTime.Hour, (object) dateTime.Minute, (object) dateTime.Second);
          }
          this.Success();
        }
      }
    }
  }
}
