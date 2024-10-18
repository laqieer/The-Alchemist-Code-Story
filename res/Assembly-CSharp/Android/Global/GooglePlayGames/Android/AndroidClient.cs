// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Android.AndroidClient
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using Com.Google.Android.Gms.Common.Api;
using Com.Google.Android.Gms.Games.Stats;
using GooglePlayGames.BasicApi;
using GooglePlayGames.Native.PInvoke;
using GooglePlayGames.OurUtils;
using System;
using UnityEngine;

namespace GooglePlayGames.Android
{
  internal class AndroidClient : IClientImpl
  {
    private const string BridgeActivityClass = "com.google.games.bridge.NativeBridgeActivity";
    private const string LaunchBridgeMethod = "launchBridgeIntent";
    private const string LaunchBridgeSignature = "(Landroid/app/Activity;Landroid/content/Intent;)V";
    private AndroidTokenClient tokenClient;

    public PlatformConfiguration CreatePlatformConfiguration()
    {
      AndroidPlatformConfiguration platformConfiguration = AndroidPlatformConfiguration.Create();
      platformConfiguration.EnableAppState();
      using (AndroidJavaObject activity = AndroidTokenClient.GetActivity())
      {
        platformConfiguration.SetActivity(activity.GetRawObject());
        platformConfiguration.SetOptionalIntentHandlerForUI((Action<IntPtr>) (intent =>
        {
          IntPtr intentRef = AndroidJNI.NewGlobalRef(intent);
          PlayGamesHelperObject.RunOnGameThread((Action) (() =>
          {
            try
            {
              AndroidClient.LaunchBridgeIntent(intentRef);
            }
            finally
            {
              AndroidJNI.DeleteGlobalRef(intentRef);
            }
          }));
        }));
      }
      return (PlatformConfiguration) platformConfiguration;
    }

    public TokenClient CreateTokenClient()
    {
      if (this.tokenClient == null)
        this.tokenClient = new AndroidTokenClient();
      return (TokenClient) this.tokenClient;
    }

    private static void LaunchBridgeIntent(IntPtr bridgedIntent)
    {
      object[] args = new object[2];
      jvalue[] jniArgArray = AndroidJNIHelper.CreateJNIArgArray(args);
      try
      {
        using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.google.games.bridge.NativeBridgeActivity"))
        {
          using (AndroidJavaObject activity = AndroidTokenClient.GetActivity())
          {
            IntPtr staticMethodId = AndroidJNI.GetStaticMethodID(androidJavaClass.GetRawClass(), "launchBridgeIntent", "(Landroid/app/Activity;Landroid/content/Intent;)V");
            jniArgArray[0].l = activity.GetRawObject();
            jniArgArray[1].l = bridgedIntent;
            AndroidJNI.CallStaticVoidMethod(androidJavaClass.GetRawClass(), staticMethodId, jniArgArray);
          }
        }
      }
      finally
      {
        AndroidJNIHelper.DeleteJNIArgArray(args, jniArgArray);
      }
    }

    public void GetPlayerStats(IntPtr apiClient, Action<CommonStatusCodes, PlayGamesLocalUser.PlayerStats> callback)
    {
      GoogleApiClient arg_GoogleApiClient_1 = new GoogleApiClient(apiClient);
      AndroidClient.StatsResultCallback statsResultCallback;
      try
      {
        statsResultCallback = new AndroidClient.StatsResultCallback((Action<int, Com.Google.Android.Gms.Games.Stats.PlayerStats>) ((result, stats) =>
        {
          Debug.Log((object) ("Result for getStats: " + (object) result));
          PlayGamesLocalUser.PlayerStats playerStats = (PlayGamesLocalUser.PlayerStats) null;
          if (stats != null)
          {
            playerStats = new PlayGamesLocalUser.PlayerStats();
            playerStats.AvgSessonLength = stats.getAverageSessionLength();
            playerStats.DaysSinceLastPlayed = stats.getDaysSinceLastPlayed();
            playerStats.NumberOfPurchases = stats.getNumberOfPurchases();
            playerStats.NumOfSessions = stats.getNumberOfSessions();
            playerStats.SessPercentile = stats.getSessionPercentile();
            playerStats.SpendPercentile = stats.getSpendPercentile();
          }
          callback((CommonStatusCodes) result, playerStats);
        }));
      }
      catch (Exception ex)
      {
        Debug.LogException(ex);
        callback(CommonStatusCodes.DeveloperError, (PlayGamesLocalUser.PlayerStats) null);
        return;
      }
      Com.Google.Android.Gms.Games.Games.Stats.loadPlayerStats(arg_GoogleApiClient_1, true).setResultCallback((ResultCallback<Stats_LoadPlayerStatsResultObject>) statsResultCallback);
    }

    private class StatsResultCallback : ResultCallbackProxy<Stats_LoadPlayerStatsResultObject>
    {
      private Action<int, Com.Google.Android.Gms.Games.Stats.PlayerStats> callback;

      public StatsResultCallback(Action<int, Com.Google.Android.Gms.Games.Stats.PlayerStats> callback)
      {
        this.callback = callback;
      }

      public override void OnResult(Stats_LoadPlayerStatsResultObject arg_Result_1)
      {
        this.callback(arg_Result_1.getStatus().getStatusCode(), arg_Result_1.getPlayerStats());
      }
    }
  }
}
