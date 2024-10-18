// Decompiled with JetBrains decompiler
// Type: SRPG.AppGuardClientInitalizer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class AppGuardClientInitalizer : MonoBehaviour
  {
    private void Awake()
    {
      DebugUtility.Log("GfW init");
      this.StartCoroutine(this.InitGameGuardWaitForWindowProc());
    }

    [DebuggerHidden]
    private IEnumerator InitGameGuardWaitForWindowProc()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new AppGuardClientInitalizer.\u003CInitGameGuardWaitForWindowProc\u003Ec__Iterator0()
      {
        \u0024this = this
      };
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
      Network.RequestAPI((WebAPI) new ReqAppGuardAuthentication(uniqueClientID1, new Network.ResponseCallback(this.AuthenticationResponseCallback)));
    }

    private void OnApplicationQuit() => this.CloseGameGuard();

    public void CloseGameGuard() => AppGuardClient.OnApplicationQuit();

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
