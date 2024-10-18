﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqHikkoshi
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "RequestFgG", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "Success", FlowNode.PinTypes.Output, 2)]
  [FlowNode.NodeType("Network/gauth_migrate", 32741)]
  public class FlowNode_ReqHikkoshi : FlowNode_Network
  {
    public string HikkoshiCodeInputFieldID;
    public string HikkoshiFgGMailAddress;
    public string HikkoshiFgGPassword;

    private FlowNode_ReqHikkoshi.API m_Api { get; set; }

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 0:
          if (Network.Mode == Network.EConnectMode.Offline)
          {
            this.Success();
            break;
          }
          GameObject gameObject1 = GameObjectID.FindGameObject(this.HikkoshiCodeInputFieldID);
          InputField component1;
          if ((UnityEngine.Object) gameObject1 == (UnityEngine.Object) null || (UnityEngine.Object) (component1 = gameObject1.GetComponent<InputField>()) == (UnityEngine.Object) null)
          {
            DebugUtility.LogError("InputField doesn't exist.");
            break;
          }
          this.ExecRequest((WebAPI) new ReqGAuthMigrate(MonoSingleton<GameManager>.Instance.SecretKey, MonoSingleton<GameManager>.Instance.DeviceId, component1.text, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          this.enabled = true;
          this.m_Api = FlowNode_ReqHikkoshi.API.Normal;
          break;
        case 1:
          if (Network.Mode == Network.EConnectMode.Offline)
          {
            this.Success();
            break;
          }
          GameObject gameObject2 = GameObjectID.FindGameObject(this.HikkoshiFgGMailAddress);
          GameObject gameObject3 = GameObjectID.FindGameObject(this.HikkoshiFgGPassword);
          InputField component2;
          if ((UnityEngine.Object) gameObject2 == (UnityEngine.Object) null || (UnityEngine.Object) (component2 = gameObject2.GetComponent<InputField>()) == (UnityEngine.Object) null)
          {
            DebugUtility.LogError("InputField doesn't exist.");
            break;
          }
          InputField component3;
          if ((UnityEngine.Object) gameObject3 == (UnityEngine.Object) null || (UnityEngine.Object) (component3 = gameObject3.GetComponent<InputField>()) == (UnityEngine.Object) null)
          {
            DebugUtility.LogError("InputField doesn't exist.");
            break;
          }
          this.ExecRequest((WebAPI) new ReqGAuthMigrateFgG(MonoSingleton<GameManager>.Instance.SecretKey, MonoSingleton<GameManager>.Instance.DeviceId, component2.text, component3.text, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          this.enabled = true;
          this.m_Api = FlowNode_ReqHikkoshi.API.FgG;
          break;
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
        switch (Network.ErrCode)
        {
          case Network.EErrCode.HikkoshiNoToken:
          case Network.EErrCode.MigrateIllCode:
          case Network.EErrCode.MigrateSameDev:
          case Network.EErrCode.MigrateLockCode:
            this.OnBack();
            break;
          case Network.EErrCode.AcheiveMigrateIllcode:
          case Network.EErrCode.AcheiveMigrateNoCoop:
          case Network.EErrCode.AcheiveMigrateLock:
          case Network.EErrCode.AcheiveMigrateAuthorize:
            this.OnBack();
            break;
          default:
            this.OnRetry();
            break;
        }
      }
      else
      {
        if (this.m_Api == FlowNode_ReqHikkoshi.API.Normal)
        {
          WebAPI.JSON_BodyResponse<JSON_Migrate> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_Migrate>>(www.text);
          DebugUtility.Assert(jsonObject != null, "res == null");
          if (jsonObject.body == null)
          {
            this.OnRetry();
            return;
          }
          MonoSingleton<GameManager>.Instance.SaveAuth(jsonObject.body.old_device_id);
        }
        else if (this.m_Api == FlowNode_ReqHikkoshi.API.FgG)
        {
          WebAPI.JSON_BodyResponse<JSON_Migrate> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_Migrate>>(www.text);
          DebugUtility.Assert(jsonObject != null, "res == null");
          if (jsonObject.body == null)
          {
            this.OnRetry();
            return;
          }
          MonoSingleton<GameManager>.Instance.SaveAuth(jsonObject.body.old_device_id);
        }
        MonoSingleton<GameManager>.Instance.InitAuth();
        GameUtility.ClearPreferences();
        Network.RemoveAPI();
        this.Success();
      }
    }

    private enum API
    {
      Normal,
      FgG,
    }
  }
}
