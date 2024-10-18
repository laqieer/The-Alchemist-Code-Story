// Decompiled with JetBrains decompiler
// Type: com.adjust.sdk.Adjust
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace com.adjust.sdk
{
  public class Adjust : MonoBehaviour
  {
    private const string errorMsgEditor = "Adjust: SDK can not be used in Editor.";
    private const string errorMsgStart = "Adjust: SDK not started. Start it manually using the 'start' method.";
    private const string errorMsgPlatform = "Adjust: SDK can only be used in Android, iOS, Windows Phone 8.1, Windows Store or Universal Windows apps.";
    public bool startManually = true;
    public bool eventBuffering;
    public bool sendInBackground;
    public bool launchDeferredDeeplink = true;
    public string appToken = "{Your App Token}";
    public AdjustLogLevel logLevel = AdjustLogLevel.Info;
    public AdjustEnvironment environment;

    private void Awake()
    {
      if (Adjust.IsEditor())
        return;
      Object.DontDestroyOnLoad((Object) ((Component) ((Component) this).transform).gameObject);
      if (this.startManually)
        return;
      AdjustConfig adjustConfig = new AdjustConfig(this.appToken, this.environment, this.logLevel == AdjustLogLevel.Suppress);
      adjustConfig.setLogLevel(this.logLevel);
      adjustConfig.setSendInBackground(this.sendInBackground);
      adjustConfig.setEventBufferingEnabled(this.eventBuffering);
      adjustConfig.setLaunchDeferredDeeplink(this.launchDeferredDeeplink);
      Adjust.start(adjustConfig);
    }

    private void OnApplicationPause(bool pauseStatus)
    {
      if (Adjust.IsEditor())
        return;
      Debug.Log((object) "Adjust: SDK can only be used in Android, iOS, Windows Phone 8.1, Windows Store or Universal Windows apps.");
    }

    public static void start(AdjustConfig adjustConfig)
    {
      if (Adjust.IsEditor())
        return;
      if (adjustConfig == null)
        Debug.Log((object) "Adjust: Missing config to start.");
      else
        Debug.Log((object) "Adjust: SDK can only be used in Android, iOS, Windows Phone 8.1, Windows Store or Universal Windows apps.");
    }

    public static void trackEvent(AdjustEvent adjustEvent)
    {
      if (Adjust.IsEditor())
        return;
      if (adjustEvent == null)
        Debug.Log((object) "Adjust: Missing event to track.");
      else
        Debug.Log((object) "Adjust: SDK can only be used in Android, iOS, Windows Phone 8.1, Windows Store or Universal Windows apps.");
    }

    public static void setEnabled(bool enabled)
    {
      if (Adjust.IsEditor())
        return;
      Debug.Log((object) "Adjust: SDK can only be used in Android, iOS, Windows Phone 8.1, Windows Store or Universal Windows apps.");
    }

    public static bool isEnabled()
    {
      if (Adjust.IsEditor())
        return false;
      Debug.Log((object) "Adjust: SDK can only be used in Android, iOS, Windows Phone 8.1, Windows Store or Universal Windows apps.");
      return false;
    }

    public static void setOfflineMode(bool enabled)
    {
      if (Adjust.IsEditor())
        return;
      Debug.Log((object) "Adjust: SDK can only be used in Android, iOS, Windows Phone 8.1, Windows Store or Universal Windows apps.");
    }

    public static void setDeviceToken(string deviceToken)
    {
      if (Adjust.IsEditor())
        return;
      Debug.Log((object) "Adjust: SDK can only be used in Android, iOS, Windows Phone 8.1, Windows Store or Universal Windows apps.");
    }

    public static void gdprForgetMe()
    {
      Debug.Log((object) "Adjust: SDK can only be used in Android, iOS, Windows Phone 8.1, Windows Store or Universal Windows apps.");
    }

    public static void appWillOpenUrl(string url)
    {
      if (Adjust.IsEditor())
        return;
      Debug.Log((object) "Adjust: SDK can only be used in Android, iOS, Windows Phone 8.1, Windows Store or Universal Windows apps.");
    }

    public static void sendFirstPackages()
    {
      if (Adjust.IsEditor())
        return;
      Debug.Log((object) "Adjust: SDK can only be used in Android, iOS, Windows Phone 8.1, Windows Store or Universal Windows apps.");
    }

    public static void addSessionPartnerParameter(string key, string value)
    {
      if (Adjust.IsEditor())
        return;
      Debug.Log((object) "Adjust: SDK can only be used in Android, iOS, Windows Phone 8.1, Windows Store or Universal Windows apps.");
    }

    public static void addSessionCallbackParameter(string key, string value)
    {
      if (Adjust.IsEditor())
        return;
      Debug.Log((object) "Adjust: SDK can only be used in Android, iOS, Windows Phone 8.1, Windows Store or Universal Windows apps.");
    }

    public static void removeSessionPartnerParameter(string key)
    {
      if (Adjust.IsEditor())
        return;
      Debug.Log((object) "Adjust: SDK can only be used in Android, iOS, Windows Phone 8.1, Windows Store or Universal Windows apps.");
    }

    public static void removeSessionCallbackParameter(string key)
    {
      if (Adjust.IsEditor())
        return;
      Debug.Log((object) "Adjust: SDK can only be used in Android, iOS, Windows Phone 8.1, Windows Store or Universal Windows apps.");
    }

    public static void resetSessionPartnerParameters()
    {
      if (Adjust.IsEditor())
        return;
      Debug.Log((object) "Adjust: SDK can only be used in Android, iOS, Windows Phone 8.1, Windows Store or Universal Windows apps.");
    }

    public static void resetSessionCallbackParameters()
    {
      if (Adjust.IsEditor())
        return;
      Debug.Log((object) "Adjust: SDK can only be used in Android, iOS, Windows Phone 8.1, Windows Store or Universal Windows apps.");
    }

    public static string getAdid()
    {
      if (Adjust.IsEditor())
        return string.Empty;
      Debug.Log((object) "Adjust: SDK can only be used in Android, iOS, Windows Phone 8.1, Windows Store or Universal Windows apps.");
      return string.Empty;
    }

    public static AdjustAttribution getAttribution()
    {
      if (Adjust.IsEditor())
        return (AdjustAttribution) null;
      Debug.Log((object) "Adjust: SDK can only be used in Android, iOS, Windows Phone 8.1, Windows Store or Universal Windows apps.");
      return (AdjustAttribution) null;
    }

    public static string getWinAdid()
    {
      if (Adjust.IsEditor())
        return string.Empty;
      Debug.Log((object) "Adjust: SDK can only be used in Android, iOS, Windows Phone 8.1, Windows Store or Universal Windows apps.");
      return string.Empty;
    }

    public static string getIdfa()
    {
      if (Adjust.IsEditor())
        return string.Empty;
      Debug.Log((object) "Adjust: SDK can only be used in Android, iOS, Windows Phone 8.1, Windows Store or Universal Windows apps.");
      return string.Empty;
    }

    public static string getSdkVersion()
    {
      if (Adjust.IsEditor())
        return string.Empty;
      Debug.Log((object) "Adjust: SDK can only be used in Android, iOS, Windows Phone 8.1, Windows Store or Universal Windows apps.");
      return string.Empty;
    }

    [Obsolete("This method is intended for testing purposes only. Do not use it.")]
    public static void setReferrer(string referrer)
    {
      if (Adjust.IsEditor())
        return;
      Debug.Log((object) "Adjust: SDK can only be used in Android, iOS, Windows Phone 8.1, Windows Store or Universal Windows apps.");
    }

    public static void getGoogleAdId(Action<string> onDeviceIdsRead)
    {
      if (Adjust.IsEditor())
        return;
      Debug.Log((object) "Adjust: SDK can only be used in Android, iOS, Windows Phone 8.1, Windows Store or Universal Windows apps.");
    }

    public static string getAmazonAdId()
    {
      if (Adjust.IsEditor())
        return string.Empty;
      Debug.Log((object) "Adjust: SDK can only be used in Android, iOS, Windows Phone 8.1, Windows Store or Universal Windows apps.");
      return string.Empty;
    }

    private static bool IsEditor() => false;

    public static void SetTestOptions(Dictionary<string, string> testOptions)
    {
      if (Adjust.IsEditor())
        return;
      Debug.Log((object) "Cannot run integration tests. None of the supported platforms selected.");
    }
  }
}
