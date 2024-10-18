// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqFggDevice
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.NodeType("Network/fgg_device", 32741)]
  [FlowNode.Pin(0, "RequestAdd", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(2, "Success", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(1, "RequestRelease", FlowNode.PinTypes.Input, 1)]
  public class FlowNode_ReqFggDevice : FlowNode_Network
  {
    public string HikkoshiFgGMailAddress;
    public string HikkoshiFgGPassword;

    private FlowNode_ReqFggDevice.API m_Api { get; set; }

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
        GameObject gameObject1 = GameObjectID.FindGameObject(this.HikkoshiFgGMailAddress);
        GameObject gameObject2 = GameObjectID.FindGameObject(this.HikkoshiFgGPassword);
        InputField input_mail;
        if ((UnityEngine.Object) gameObject1 == (UnityEngine.Object) null || (UnityEngine.Object) (input_mail = gameObject1.GetComponent<InputField>()) == (UnityEngine.Object) null)
        {
          DebugUtility.LogError("InputField doesn't exist.");
        }
        else
        {
          InputField input_pass;
          if ((UnityEngine.Object) gameObject2 == (UnityEngine.Object) null || (UnityEngine.Object) (input_pass = gameObject2.GetComponent<InputField>()) == (UnityEngine.Object) null)
            DebugUtility.LogError("InputField doesn't exist.");
          else
            Application.RequestAdvertisingIdentifierAsync((Application.AdvertisingIdentifierCallback) ((advertisingId, trackingEnabled, error) =>
            {
              this.ExecRequest((WebAPI) new ReqGAuthFgGDevice(advertisingId, input_mail.text, input_pass.text, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
              this.enabled = true;
              this.m_Api = FlowNode_ReqFggDevice.API.Add;
            }));
        }
      }
    }

    private void Success()
    {
      this.enabled = false;
      this.ActivateOutputLinks(2);
    }

    private void Failure(int pinID)
    {
      this.enabled = false;
      this.ActivateOutputLinks(pinID);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        Network.EErrCode errCode = Network.ErrCode;
        switch (errCode)
        {
          case Network.EErrCode.AcheiveMigrateIllcode:
          case Network.EErrCode.AcheiveMigrateLock:
            this.OnBack();
            break;
          default:
            if (errCode != Network.EErrCode.MigrateSameDev)
            {
              this.OnRetry();
              break;
            }
            goto case Network.EErrCode.AcheiveMigrateIllcode;
        }
      }
      else
      {
        if (this.m_Api == FlowNode_ReqFggDevice.API.Add)
        {
          WebAPI.JSON_BodyResponse<ReqGAuthFgGDevice.Json_FggDevice> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqGAuthFgGDevice.Json_FggDevice>>(www.text);
          DebugUtility.Assert(jsonObject != null, "res == null");
          if (jsonObject.body == null)
          {
            this.OnRetry();
            return;
          }
          MonoSingleton<GameManager>.Instance.SaveAuthWithKey(jsonObject.body.device_id, jsonObject.body.secret_key);
          MonoSingleton<GameManager>.Instance.InitAuth();
          GameUtility.ClearPreferences();
        }
        Network.RemoveAPI();
        this.Success();
      }
    }

    private enum API
    {
      Add,
      Release,
    }
  }
}
