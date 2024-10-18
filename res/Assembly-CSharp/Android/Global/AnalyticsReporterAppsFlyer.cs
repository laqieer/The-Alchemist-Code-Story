// Decompiled with JetBrains decompiler
// Type: AnalyticsReporterAppsFlyer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public class AnalyticsReporterAppsFlyer : AnalyticsReporterBase
{
  private const string APPSFLYER_DASHBOARD_DEV_KEY = "WMa4kPf8ZdvNhcpvdpwAvE";
  private const string APPSFLYER_ANDROID_APP_ID = "sg.gumi.alchemistww";
  private const string APPSFLYER_IOS_APP_ID = "1263809665";
  private bool IS_ANALYTICS_DEBUG_MODE;
  private bool IS_ANALYTICS_REPORT_TO_DEV;
  private string GCM_SENDER_ID;
  private static AppsFlyerTrackerCallbacks _appsflyerGameObject;
  private AnalyticsReporterAppsFlyer.AppsFlyerSetupState _AFSetupState;

  private void SetupAppsFlyer(string inUserID)
  {
    if (this._AFSetupState == AnalyticsReporterAppsFlyer.AppsFlyerSetupState.FULL)
      return;
    if (this._AFSetupState == AnalyticsReporterAppsFlyer.AppsFlyerSetupState.NONE)
    {
      if ((UnityEngine.Object) AnalyticsReporterAppsFlyer._appsflyerGameObject == (UnityEngine.Object) null)
      {
        AnalyticsReporterAppsFlyer._appsflyerGameObject = new GameObject("AppsFlyerTrackerCallbacks").AddComponent<AppsFlyerTrackerCallbacks>();
        UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) AnalyticsReporterAppsFlyer._appsflyerGameObject);
      }
      AppsFlyer.setAppsFlyerKey("WMa4kPf8ZdvNhcpvdpwAvE");
    }
    AppsFlyer.setIsDebug(this.IS_ANALYTICS_DEBUG_MODE);
    this.SetupAppsFlyerAndroid(inUserID, "sg.gumi.alchemistww", this.GCM_SENDER_ID, this._AFSetupState);
    this._AFSetupState = AnalyticsReporterAppsFlyer.AppsFlyerSetupState.FULL;
  }

  private void PartialSetupAppsFlyer()
  {
    if ((UnityEngine.Object) AnalyticsReporterAppsFlyer._appsflyerGameObject == (UnityEngine.Object) null)
    {
      AnalyticsReporterAppsFlyer._appsflyerGameObject = new GameObject("AppsFlyerTrackerCallbacks").AddComponent<AppsFlyerTrackerCallbacks>();
      UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) AnalyticsReporterAppsFlyer._appsflyerGameObject);
    }
    AppsFlyer.setAppsFlyerKey("WMa4kPf8ZdvNhcpvdpwAvE");
    AppsFlyer.setCollectIMEI(false);
    AppsFlyer.setAppID("sg.gumi.alchemistww");
  }

  private void SetupAppsFlyerAndroid(string inUserID, string inAndroidAppID, string inGCMID, AnalyticsReporterAppsFlyer.AppsFlyerSetupState inCurrentSetUpState)
  {
    if (inCurrentSetUpState == AnalyticsReporterAppsFlyer.AppsFlyerSetupState.NONE)
    {
      AppsFlyer.setCollectIMEI(false);
      AppsFlyer.setAppID(inAndroidAppID);
    }
    AppsFlyer.setCustomerUserID(inUserID);
    AppsFlyer.init("WMa4kPf8ZdvNhcpvdpwAvE", "AppsFlyerTrackerCallbacks");
    AppsFlyer.createValidateInAppListener("AppsFlyerTrackerCallbacks", "onInAppBillingSuccess", "onInAppBillingFailure");
    AppsFlyer.loadConversionData("AnalyticsManager");
    AppsFlyer.enableUninstallTracking(inGCMID);
  }

  private void AppsFlyerReport(string inReportingName, Dictionary<string, string> inReportingDictionary)
  {
    if (this.IS_ANALYTICS_DEBUG_MODE)
      Debug.LogWarning((object) ("[Analytics]AppsFlyer Reporting\n" + inReportingName + "\n" + inReportingDictionary.DictionaryToString<string, string>(true, false, false, string.Empty, string.Empty, string.Empty)));
    AppsFlyer.trackRichEvent(inReportingName, inReportingDictionary);
  }

  public override void Setup(string inUserID, bool inIsPayingPlayer, string inGCMID, bool inIsAnalyticsDebugMode, bool inIsAnalyticsReportToDev)
  {
    this.IS_ANALYTICS_DEBUG_MODE = inIsAnalyticsDebugMode;
    this.IS_ANALYTICS_REPORT_TO_DEV = inIsAnalyticsReportToDev;
    this.GCM_SENDER_ID = inGCMID;
    Debug.Log((object) string.Format("[Analytics] AppsFlyer {0}, {1} reporting", !this.IS_ANALYTICS_REPORT_TO_DEV ? (object) "Live" : (object) "Development", !this.IS_ANALYTICS_DEBUG_MODE ? (object) "NonDebug" : (object) "Debug"));
    this.SetupAppsFlyer(inUserID);
  }

  public override void TrackEvent(string inEventName, Dictionary<string, object> inTrackInfo)
  {
    if (this._AFSetupState == AnalyticsReporterAppsFlyer.AppsFlyerSetupState.NONE)
    {
      this._AFSetupState = AnalyticsReporterAppsFlyer.AppsFlyerSetupState.PARTIAL;
      this.PartialSetupAppsFlyer();
    }
    this.AppsFlyerReport(inEventName, inTrackInfo.ConvertValuesToString<string, object>());
  }

  public override void TrackLevelUpEvent(int inCurrentLevel)
  {
    this.AppsFlyerReport("level_achieved", new Dictionary<string, string>()
    {
      {
        "level_achieved",
        inCurrentLevel.ToString()
      }
    });
  }

  public override void TrackPurchaseEvent(string inProductID, string inCurrencyCode, double inPrice)
  {
    AppsFlyer.setCurrencyCode("USD");
    this.AppsFlyerReport("purchase", new Dictionary<string, string>()
    {
      {
        "product_id",
        inProductID
      },
      {
        "af_currency",
        inCurrencyCode
      },
      {
        "af_revenue",
        inPrice.ToString()
      }
    });
  }

  public override void TrackFreePremiumCurrencyTransaction(string inTransactionName, string inTransactionSource, long inAmount)
  {
  }

  public override void TrackPaidPremiumCurrencyTransaction(string inTransactionName, string inTransactionSource, long inAmount)
  {
  }

  public override void TrackNonPremiumCurrencyTransaction(string inTransactionEventName, string inTransactionSource, string inUniqueCurrencyKey, string inUniqueCurrencyID, long inAmount)
  {
  }

  public override void RecordNewPlayerLogin(string inUserID)
  {
  }

  public override void RecordGuestLogin(string inUserID)
  {
  }

  public override void RecordFacebookLogin(string inUserID)
  {
  }

  public override bool HasOfferwallToShow()
  {
    return false;
  }

  public override void AttemptToShowPlacement(string inMileStoneName, Action inCallbackForPlacementBeingShown)
  {
  }

  public override void AddPlacementFlow(Action<string> inPlacementWantedFlowChangeHandler)
  {
  }

  public override void RemovePlacementFlow(Action<string> inPlacementWantedFlowChangeHandler)
  {
  }

  public override void TrackSummonData(string inSummonMainType, string inSummonSubType, string inCurrencyUsed, int inCurrencyAmount, string inGateID, int inStepID, string inStepRewardID, int inStepRewardAmount)
  {
    this.AppsFlyerReport("summon", new Dictionary<string, string>()
    {
      {
        "main_type",
        inSummonMainType
      },
      {
        "sub_type",
        inSummonSubType
      },
      {
        "currency_used",
        inCurrencyUsed
      },
      {
        "currency_amount",
        inCurrencyAmount.ToString()
      },
      {
        "gate_id",
        inGateID
      },
      {
        "step_id",
        inStepID.ToString()
      },
      {
        "step_reward_id",
        inStepRewardID
      },
      {
        "step_reward_amount",
        inStepRewardAmount.ToString()
      }
    });
  }

  public override void OnApplicationFocus(bool inIsFocus)
  {
  }

  private enum AppsFlyerSetupState
  {
    NONE,
    PARTIAL,
    FULL,
  }
}
