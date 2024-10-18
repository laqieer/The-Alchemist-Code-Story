// Decompiled with JetBrains decompiler
// Type: AnalyticsReporterTapjoy
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using SRPG;
using System;
using System.Collections.Generic;
using TapjoyUnity;
using TapjoyUnity.Internal;
using UnityEngine;

public class AnalyticsReporterTapjoy : AnalyticsReporterBase
{
  private List<TJPlacement> _placementsToPreload = new List<TJPlacement>();
  private Dictionary<TJPlacement, Action> _placementAndCallbackDictionary = new Dictionary<TJPlacement, Action>();
  private List<string> _namesOfPlacementsToPreload = new List<string>()
  {
    "title",
    "homescreen",
    "freegemsofferwall"
  };
  private List<string> _namesOfPlacementsToReload = new List<string>()
  {
    "freegemsofferwall"
  };
  private const string TAPJOY_IOS_CONNECTION_KEY = "mIWtlPi5Snuo44UzPUwj3AEBHA4XIpTkQloTrx5LsHaTs-OdMfsMItJmHi5R";
  private const string TAPJOY_ANDROID_CONNECTION_KEY = "8KX9gHMyQoe1uZ44k2UZFAECmkrdHkw3JeTIaSi9Gh54pEK7mTONyurmoPRt";
  private bool IS_ANALYTICS_DEBUG_MODE;
  private bool IS_ANALYTICS_REPORT_TO_DEV;
  private string GCM_SENDER_ID;
  private TapjoyComponent _tapjoyGameObject;
  private Action _tapjoyActionsSavedBeforeConnectSuccess;
  private Action<string> _placementWantedFlowChangeHandler;
  private string _lastKnownUserID;
  private bool hasPlayerSeenFreeGemOfferWallOnce;

  private void SetupTapJoy(string inUserID)
  {
    DebugUtility.LogWarning("Entered Set up");
    if ((UnityEngine.Object) this._tapjoyGameObject == (UnityEngine.Object) null)
    {
      this._tapjoyGameObject = UnityEngine.Object.FindObjectOfType<TapjoyComponent>();
      if ((UnityEngine.Object) this._tapjoyGameObject == (UnityEngine.Object) null)
        throw new Exception("Scene does not contain a precreated Tapjoy tracker object");
    }
    Tapjoy.SetDebugEnabled(this.IS_ANALYTICS_DEBUG_MODE);
    Tapjoy.SetGcmSender(this.GCM_SENDER_ID);
    Tapjoy.Connect("8KX9gHMyQoe1uZ44k2UZFAECmkrdHkw3JeTIaSi9Gh54pEK7mTONyurmoPRt");
    this._lastKnownUserID = inUserID;
    // ISSUE: method pointer
    Tapjoy.add_OnConnectSuccess(new Tapjoy.OnConnectSuccessHandler((object) this, __methodptr(HandleConnectSuccess)));
    // ISSUE: method pointer
    Tapjoy.add_OnConnectFailure(new Tapjoy.OnConnectFailureHandler((object) this, __methodptr(HandleConnectFailure)));
  }

  private void SetPlayerIsPaying(bool inIsPayingPlayer)
  {
    this.TapjoyRerouteActionBasedOnConnectStatus((Action) (() =>
    {
      if (!inIsPayingPlayer)
        return;
      Tapjoy.AddUserTag("Paying");
    }));
  }

  private void HandleConnectSuccess()
  {
    Tapjoy.SetUserID(this._lastKnownUserID);
    Tapjoy.SetAppDataVersion(MyApplicationPlugin.version);
    Tapjoy.SetPushNotificationDisabled(false);
    if (this.IS_ANALYTICS_REPORT_TO_DEV)
      Tapjoy.SetUserCohortVariable(0, "Development");
    else
      Tapjoy.SetUserCohortVariable(0, "Release");
    // ISSUE: method pointer
    TJPlacement.add_OnContentReady(new TJPlacement.OnContentReadyHandler((object) this, __methodptr(HandlePlacementContentReady)));
    // ISSUE: method pointer
    TJPlacement.add_OnContentShow(new TJPlacement.OnContentShowHandler((object) this, __methodptr(HandlePlacementContentShow)));
    // ISSUE: method pointer
    TJPlacement.add_OnPurchaseRequest(new TJPlacement.OnPurchaseRequestHandler((object) this, __methodptr(HandlePurchaseRequest)));
    // ISSUE: method pointer
    TJPlacement.add_OnContentDismiss(new TJPlacement.OnContentDismissHandler((object) this, __methodptr(HandleContentDismiss)));
    if (this._placementAndCallbackDictionary != null)
    {
      using (Dictionary<TJPlacement, Action>.Enumerator enumerator = this._placementAndCallbackDictionary.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          KeyValuePair<TJPlacement, Action> current = enumerator.Current;
          current.Key.RequestContent();
          this._namesOfPlacementsToPreload.Remove(current.Key.GetName());
        }
      }
    }
    if (this._namesOfPlacementsToPreload != null)
    {
      using (List<string>.Enumerator enumerator = this._namesOfPlacementsToPreload.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          TJPlacement placement = TJPlacement.CreatePlacement(enumerator.Current);
          placement.RequestContent();
          this._placementsToPreload.Add(placement);
        }
      }
    }
    if (this._tapjoyActionsSavedBeforeConnectSuccess == null)
      return;
    this._tapjoyActionsSavedBeforeConnectSuccess();
    this._tapjoyActionsSavedBeforeConnectSuccess = (Action) null;
  }

  private void HandleConnectFailure()
  {
    Debug.Log((object) "Tapjoy Connect Failed");
    // ISSUE: method pointer
    Tapjoy.remove_OnConnectSuccess(new Tapjoy.OnConnectSuccessHandler((object) this, __methodptr(HandleConnectSuccess)));
    // ISSUE: method pointer
    Tapjoy.remove_OnConnectFailure(new Tapjoy.OnConnectFailureHandler((object) this, __methodptr(HandleConnectFailure)));
    this.SetupTapJoy(this._lastKnownUserID);
  }

  private void HandlePurchaseRequest(TJPlacement inPlacement, TJActionRequest inRequest, string inProductId)
  {
    Debug.Log((object) "We have a purchase callback");
    if (this._placementWantedFlowChangeHandler != null)
    {
      Debug.Log((object) ("We are sending back our placement flow: " + inProductId));
      this._placementWantedFlowChangeHandler(inProductId);
    }
    inRequest?.Completed();
  }

  private void HandlePlacementContentShow(TJPlacement inPlacement)
  {
    if (!this._placementAndCallbackDictionary.ContainsKey(inPlacement))
      return;
    Action placementAndCallback = this._placementAndCallbackDictionary[inPlacement];
    if (placementAndCallback != null)
      placementAndCallback();
    this._placementAndCallbackDictionary.Remove(inPlacement);
  }

  private void HandlePlacementContentReady(TJPlacement inPlacement)
  {
    if (!this._placementAndCallbackDictionary.ContainsKey(inPlacement))
      return;
    inPlacement.ShowContent();
  }

  private void HandleContentDismiss(TJPlacement inPlacement)
  {
    string placementName = inPlacement.GetName();
    if (this._namesOfPlacementsToReload.Find((Predicate<string>) (entry => entry.Equals(placementName))) == null)
      return;
    this._placementsToPreload.Add(inPlacement);
    inPlacement.RequestContent();
  }

  private void TrackTapjoyEvent(string inEventName, long inValue = 0)
  {
    if (this.IS_ANALYTICS_DEBUG_MODE)
      Debug.LogWarning((object) ("[Analytics]Tapjoy Reporting\n{" + inEventName + " " + (object) inValue + "}\n"));
    this.TapjoyRerouteActionBasedOnConnectStatus((Action) (() => Tapjoy.TrackEvent(inEventName, inValue)));
  }

  private void TrackTapjoyEvent(string category, string inEventName, long inValue = 0)
  {
    if (this.IS_ANALYTICS_DEBUG_MODE)
      Debug.LogWarning((object) ("[Analytics]Tapjoy Reporting\n{" + category + "}\n{" + inEventName + "}\n{" + (object) inValue + "}\n"));
    this.TapjoyRerouteActionBasedOnConnectStatus((Action) (() => Tapjoy.TrackEvent(category, inEventName, inValue)));
  }

  private void TrackTapjoyEvent(string category, string inEventName, string inEventCategory, long inValue = 0)
  {
    if (this.IS_ANALYTICS_DEBUG_MODE)
      Debug.LogWarning((object) ("[Analytics]Tapjoy Reporting\n{" + category + "}\n{" + inEventName + "}\n{" + inEventCategory + "}\n{" + (object) inValue + "}\n"));
    this.TapjoyRerouteActionBasedOnConnectStatus((Action) (() => Tapjoy.TrackEvent(category, inEventName, inEventCategory, (string) null, inValue)));
  }

  private void TrackTapjoyEvent(string category, string inEventName, string inEventCategory1, string inEventCategory2, string inValueName1, long inValue1)
  {
    if (this.IS_ANALYTICS_DEBUG_MODE)
      Debug.LogWarning((object) ("[Analytics]Tapjoy Reporting\n{" + category + "}\n{" + inEventName + "}\n{" + inEventCategory1 + "}\n{" + inEventCategory2 + "}\n{" + inValueName1 + "}\n{" + (object) inValue1 + "}\n"));
    this.TapjoyRerouteActionBasedOnConnectStatus((Action) (() => Tapjoy.TrackEvent(category, inEventName, inEventCategory1, inEventCategory2, inValueName1, inValue1, (string) null, 0L, (string) null, 0L)));
  }

  private void TrackTapjoyEvent(string category, string inEventName, string inEventCategory1, string inEventCategory2, string inValueName1, long inValue1, string inValueName2, long inValue2, string inValueName3, long inValue3)
  {
    if (this.IS_ANALYTICS_DEBUG_MODE)
      Debug.LogWarning((object) ("[Analytics]Tapjoy Reporting\n{" + category + "}\n{" + inEventName + "}\n{" + inEventCategory1 + "}\n{" + inEventCategory2 + "}\n{" + inValueName1 + ", " + (object) inValue1 + "}\n{" + inValueName2 + ", " + (object) inValue2 + "}\n{" + inValueName3 + ", " + (object) inValue3 + "}\n"));
    this.TapjoyRerouteActionBasedOnConnectStatus((Action) (() => Tapjoy.TrackEvent(category, inEventName, inEventCategory1, inEventCategory2, inValueName1, inValue1, inValueName2, inValue2, inValueName3, inValue3)));
  }

  private void RecordTapjoyUserAttribute(string inAttributeKey, string inAttributeValue)
  {
    if (this.IS_ANALYTICS_DEBUG_MODE)
      Debug.LogWarning((object) ("[Analytics]Tapjoy Recording Tag\n" + inAttributeKey + "," + inAttributeValue));
    this.TapjoyRerouteActionBasedOnConnectStatus((Action) (() => Tapjoy.AddUserTag(inAttributeKey + "," + inAttributeValue)));
  }

  private void TapjoyRerouteActionBasedOnConnectStatus(Action inTapjoyAction)
  {
    if (Tapjoy.get_IsConnected())
      inTapjoyAction();
    else
      this._tapjoyActionsSavedBeforeConnectSuccess += inTapjoyAction;
  }

  public override void Setup(string inUserID, bool inIsPayingPlayer, string inGCMID, bool inIsAnalyticsDebugMode, bool inIsAnalyticsReportToDev)
  {
    this.IS_ANALYTICS_DEBUG_MODE = inIsAnalyticsDebugMode;
    this.IS_ANALYTICS_REPORT_TO_DEV = inIsAnalyticsReportToDev;
    this.GCM_SENDER_ID = inGCMID;
    Debug.Log((object) string.Format("[Analytics] Tapjoy {0}, {1} reporting", !this.IS_ANALYTICS_REPORT_TO_DEV ? (object) "Live" : (object) "Development", !this.IS_ANALYTICS_DEBUG_MODE ? (object) "NonDebug" : (object) "Debug"));
    this.SetupTapJoy(inUserID);
    this.SetPlayerIsPaying(inIsPayingPlayer);
  }

  public override void TrackSummonData(string inSummonMainType, string inSummonSubType, string inCurrencyUsed, int inCurrencyAmount, string inGateID, int inStepID, string inStepRewardID, int inStepRewardAmount)
  {
    this.TrackTapjoyEvent("summon", inSummonMainType, inGateID, inCurrencyUsed, "currency_amount", (long) inCurrencyAmount, "currency_used", (long) inStepRewardAmount, "step_id", (long) inStepID);
  }

  public override void RecordNewPlayerLogin(string inUserID)
  {
    this.TrackTapjoyEvent("login", "user.new", inUserID, 0L);
    this.RecordTapjoyUserAttribute("Start_Version", MyApplicationPlugin.version);
  }

  public override void RecordGuestLogin(string inUserID)
  {
    this.TrackTapjoyEvent("login", "user.account.guest", inUserID, 0L);
    this.RecordTapjoyUserAttribute("Account_Type", "guest");
  }

  public override void RecordFacebookLogin(string inUserID)
  {
    this.TrackTapjoyEvent("login", "user.account.facebook", GlobalVars.CustomerID, 0L);
    this.RecordTapjoyUserAttribute("Account_Type", "facebook");
  }

  public override void TrackEvent(string inEventName, Dictionary<string, object> inTrackInfo)
  {
    int count = inTrackInfo.Count;
    List<string> stringList = new List<string>((IEnumerable<string>) inTrackInfo.Keys);
    List<object> objectList = new List<object>((IEnumerable<object>) inTrackInfo.Values);
    string category = !inEventName.Contains("currency") ? (!inEventName.Contains("tutorial") ? string.Empty : "tutorial") : "economy";
    if (count != 1)
      throw new IndexOutOfRangeException("AnalyticsReporterTapjoy Exception\n" + inTrackInfo.DictionaryToString<string, object>(true, true, false, string.Empty, string.Empty, string.Empty));
    if (objectList[0] is long)
      this.TrackTapjoyEvent(category, inEventName, string.Empty, string.Empty, stringList[0], (long) objectList[0]);
    else
      this.TrackTapjoyEvent(category, inEventName, objectList[0].ToString(), 0L);
  }

  public override void TrackLevelUpEvent(int inCurrentLevel)
  {
    this.TapjoyRerouteActionBasedOnConnectStatus((Action) (() => Tapjoy.SetUserLevel(inCurrentLevel)));
    this.TrackTapjoyEvent("level_achieved", (long) inCurrentLevel);
  }

  public override void TrackPurchaseEvent(string inProductID, string inCurrencyCode, double inPrice)
  {
    if (!PlayerPrefs.HasKey("FIRST_PURCHASE"))
    {
      this.TrackTapjoyEvent("user.paying.first", (string) null, 0L);
      this.TrackTapjoyEvent("revenue.paying.first", (string) null, 0L);
    }
    this.TapjoyRerouteActionBasedOnConnectStatus((Action) (() => Tapjoy.TrackPurchaseInGooglePlayStore(inProductID, (string) null, (string) null, (string) null)));
    this.TrackTapjoyEvent(string.Empty, "IAPPurchase", inProductID, inCurrencyCode, string.Empty, (long) inPrice);
  }

  public override void TrackFreePremiumCurrencyTransaction(string inTransactionName, string inTransactionSource, long inAmount)
  {
    this.TrackTapjoyEvent("economy", inTransactionName, inTransactionSource, "Free Gem", string.Empty, inAmount);
  }

  public override void TrackPaidPremiumCurrencyTransaction(string inTransactionName, string inTransactionSource, long inAmount)
  {
    this.TrackTapjoyEvent("economy", inTransactionName, inTransactionSource, "Paid Gem", string.Empty, inAmount);
  }

  public override void TrackNonPremiumCurrencyTransaction(string inTransactionEventName, string inTransactionSource, string inUniqueCurrencyKey, string inUniqueCurrencyID, long inAmount)
  {
    if (string.IsNullOrEmpty(inUniqueCurrencyID) || string.IsNullOrEmpty(inUniqueCurrencyKey))
      this.TrackTapjoyEvent("economy", inTransactionEventName, inTransactionSource, inAmount);
    else
      this.TrackTapjoyEvent("economy", inTransactionEventName, inTransactionSource, inUniqueCurrencyKey, inUniqueCurrencyID, inAmount);
  }

  public override bool HasOfferwallToShow()
  {
    TJPlacement tjPlacement = this._placementsToPreload.Find((Predicate<TJPlacement>) (placement => placement.GetName().Equals("freegemsofferwall")));
    if (tjPlacement != null)
      return tjPlacement.IsContentReady();
    return false;
  }

  public override void AttemptToShowPlacement(string inMileStoneName, Action inCallbackForPlacementBeingShown)
  {
    TJPlacement tjPlacement = this._placementsToPreload.Find((Predicate<TJPlacement>) (placement => placement.GetName().Equals(inMileStoneName)));
    TJPlacement key = tjPlacement == null ? TJPlacement.CreatePlacement(inMileStoneName) : tjPlacement;
    this._placementsToPreload.Remove(key);
    this._placementAndCallbackDictionary.Add(key, inCallbackForPlacementBeingShown);
    if (key.IsContentReady())
    {
      key.ShowContent();
    }
    else
    {
      if (!Tapjoy.get_IsConnected())
        return;
      key.RequestContent();
    }
  }

  public override void AddPlacementFlow(Action<string> inPlacementWantedFlowChangeHandler)
  {
    this._placementWantedFlowChangeHandler += inPlacementWantedFlowChangeHandler;
  }

  public override void RemovePlacementFlow(Action<string> inPlacementWantedFlowChangeHandler)
  {
    this._placementWantedFlowChangeHandler -= inPlacementWantedFlowChangeHandler;
  }

  public override void OnApplicationFocus(bool inIsFocus)
  {
  }
}
