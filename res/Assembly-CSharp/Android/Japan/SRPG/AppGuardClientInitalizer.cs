// Decompiled with JetBrains decompiler
// Type: SRPG.AppGuardClientInitalizer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;

namespace SRPG
{
  public class AppGuardClientInitalizer : MonoBehaviour
  {
    private void Awake()
    {
      AppGuardClient.Init(this.gameObject.name, new Action<string>(this.AppGuardClientInitalizer_OnDecideUniqueClientID).Method.Name);
    }

    public void AppGuardClientInitalizer_OnDecideUniqueClientID(string uniqueClientID)
    {
      string uniqueClientID1;
      if (string.IsNullOrEmpty(uniqueClientID))
      {
        uniqueClientID1 = MonoSingleton<GameManager>.Instance.AppGuardUniqueClientID;
      }
      else
      {
        string str = uniqueClientID;
        MonoSingleton<GameManager>.Instance.AppGuardUniqueClientID = str;
        uniqueClientID1 = str;
      }
      Network.RequestAPI((WebAPI) new ReqAppGuardAuthentication(uniqueClientID1, new Network.ResponseCallback(this.AuthenticationResponseCallback)), false);
    }

    private void OnApplicationPause(bool pauseStatus)
    {
      if (pauseStatus)
        AppGuardClient.Pause();
      else
        AppGuardClient.Resume();
    }

    private void OnDestroy()
    {
      AppGuardClient.Cleanup();
    }

    private void AuthenticationResponseCallback(WWWResult www)
    {
      if (FlowNode_Network.HasCommonError(www))
        return;
      if (Network.IsError)
      {
        int errCode = (int) Network.ErrCode;
        FlowNode_Network.Retry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<AppGuardClientInitalizer.Json_Authentication> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<AppGuardClientInitalizer.Json_Authentication>>(www.text);
        Network.RemoveAPI();
        if (jsonObject.body.status == 0)
        {
          Debug.Log((object) "Force Application Quit.");
          Application.Quit();
        }
        else
          Debug.Log((object) "Authentication Succeeded.");
      }
    }

    private class Json_Authentication
    {
      public int status;
    }
  }
}
