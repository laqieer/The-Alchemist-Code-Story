// Decompiled with JetBrains decompiler
// Type: AppGuardUnityManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using SRPG;
using UnityEngine;

#nullable disable
public class AppGuardUnityManager : MonoBehaviour
{
  private static AppGuardUnityManager _instance;
  private int killCode;
  private bool showDeathSentence;

  public void onS2AuthTryCallback(string data)
  {
    string[] strArray = data.Split(',');
    switch (int.Parse(strArray[0]))
    {
      case 1:
        DebugUtility.Log("S2AUTH_RESULT_SUCCESS(UniqueID: " + strArray[1] + ")");
        break;
      case 2:
        DebugUtility.Log("S2AUTH_RESULT_RETRY");
        break;
      case 3:
        DebugUtility.Log("S2AUTH_RESULT_FAIL");
        break;
    }
  }

  public void onViolationCallback(string data)
  {
    if (int.Parse(data) > 0)
    {
      this.killCode = Mathf.Abs(int.Parse(data));
      if (this.killCode == 21)
        DebugUtility.LogError("AppGuard onViolationCallback error: DETECT_RUNNING_BAD_APPLICATION (" + (object) 21 + ")");
      else
        DebugUtility.LogError("AppGuard onViolationCallback error: " + data);
      this.StartToDie();
    }
    else
      DebugUtility.LogWarning("AppGuard onViolationCallback warning: " + data);
  }

  public void start()
  {
  }

  private void StartToDie()
  {
    this.Invoke("DieNow", 30f);
    this.showDeathSentence = true;
  }

  private void DieNow() => Application.Quit();

  private void Update()
  {
    if (!this.showDeathSentence)
      return;
    this.showDeathSentence = false;
    EmbedSystemMessage.Create(LocalizedText.Get("embed.GAMEGUARD_ERROR"), (EmbedSystemMessage.SystemMessageEvent) (yes => Application.Quit()), true);
  }

  public string getAppGuardUdid() => "Not supported on this platform";

  public void setUserId(string userid)
  {
  }

  public void setUniqueClientId(string uniqueId, long retryTimeSec)
  {
  }

  public static AppGuardUnityManager Instance
  {
    get
    {
      if (Object.op_Equality((Object) null, (Object) AppGuardUnityManager._instance))
      {
        AppGuardUnityManager._instance = Object.FindObjectOfType(typeof (AppGuardUnityManager)) as AppGuardUnityManager;
        if (Object.op_Equality((Object) null, (Object) AppGuardUnityManager._instance))
        {
          AppGuardUnityManager._instance = new GameObject(nameof (AppGuardUnityManager)).AddComponent<AppGuardUnityManager>();
          Object.DontDestroyOnLoad((Object) AppGuardUnityManager._instance);
        }
      }
      return AppGuardUnityManager._instance;
    }
  }
}
