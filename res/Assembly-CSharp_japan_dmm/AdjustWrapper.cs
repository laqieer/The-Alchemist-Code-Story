// Decompiled with JetBrains decompiler
// Type: AdjustWrapper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using com.adjust.sdk;
using System;
using UnityEngine;

#nullable disable
public class AdjustWrapper
{
  private const string APP_TOKEN = "urt8vz8z0hkw";
  public const string EVENTTOKEN_PAYMENT = "q8j451";
  public const string EVENTTOKEN_TEST = "jgiyg4";
  public const string EVENTTOKEN_TUTORIAL_END = "unh23k";
  private static bool m_enable;

  public static bool IsEnable => AdjustWrapper.m_enable;

  public static long[] AppSecrets => (long[]) null;

  public static void Setup()
  {
    if (AdjustWrapper.IsEnable)
    {
      DebugUtility.LogWarning("Adjust is Already Setuped!");
    }
    else
    {
      AdjustConfig adjustConfig = new AdjustConfig("urt8vz8z0hkw", AdjustEnvironment.Production);
      adjustConfig.setLogLevel(AdjustLogLevel.Verbose);
      long[] appSecrets = AdjustWrapper.AppSecrets;
      if (appSecrets != null)
        adjustConfig.setAppSecret(appSecrets[0], appSecrets[1], appSecrets[2], appSecrets[3], appSecrets[4]);
      adjustConfig.setLogDelegate((Action<string>) (msg => Debug.Log((object) msg)));
      adjustConfig.setSendInBackground(false);
      adjustConfig.setLaunchDeferredDeeplink(true);
      Adjust.start(adjustConfig);
      AdjustWrapper.m_enable = Adjust.isEnabled();
    }
  }

  public static void TrackRevenue(
    string ev_token,
    string currency,
    double price,
    string transactionId)
  {
    if (!AdjustWrapper.IsEnable || string.IsNullOrEmpty(ev_token) || string.IsNullOrEmpty(currency) || price <= 0.0)
      return;
    AdjustEvent adjustEvent = new AdjustEvent(ev_token);
    adjustEvent.setRevenue(price, currency);
    adjustEvent.setTransactionId(transactionId);
    Adjust.trackEvent(adjustEvent);
  }

  public static void TrackEventSimple(string ev_token)
  {
    if (!AdjustWrapper.IsEnable || string.IsNullOrEmpty(ev_token))
      return;
    Adjust.trackEvent(new AdjustEvent(ev_token));
  }
}
