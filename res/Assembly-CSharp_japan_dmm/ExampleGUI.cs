// Decompiled with JetBrains decompiler
// Type: ExampleGUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using com.adjust.sdk;
using System;
using UnityEngine;

#nullable disable
public class ExampleGUI : MonoBehaviour
{
  private int numberOfButtons = 8;
  private bool isEnabled;
  private bool showPopUp;
  private string txtSetEnabled = "Disable SDK";
  private string txtManualLaunch = "Manual Launch";
  private string txtSetOfflineMode = "Turn Offline Mode ON";

  private void OnGUI()
  {
    if (this.showPopUp)
    {
      // ISSUE: method pointer
      GUI.Window(0, new Rect((float) (Screen.width / 2 - 150), (float) (Screen.height / 2 - 65), 300f, 130f), new GUI.WindowFunction((object) this, __methodptr(ShowGUI)), "Is SDK enabled?");
    }
    if (GUI.Button(new Rect(0.0f, (float) (Screen.height * 0 / this.numberOfButtons), (float) Screen.width, (float) (Screen.height / this.numberOfButtons)), this.txtManualLaunch) && !string.Equals(this.txtManualLaunch, "SDK Launched", StringComparison.OrdinalIgnoreCase))
    {
      AdjustConfig adjustConfig = new AdjustConfig("2fm9gkqubvpc", AdjustEnvironment.Sandbox);
      adjustConfig.setLogLevel(AdjustLogLevel.Verbose);
      adjustConfig.setLogDelegate((Action<string>) (msg => Debug.Log((object) msg)));
      adjustConfig.setEventSuccessDelegate(new Action<AdjustEventSuccess>(this.EventSuccessCallback));
      adjustConfig.setEventFailureDelegate(new Action<AdjustEventFailure>(this.EventFailureCallback));
      adjustConfig.setSessionSuccessDelegate(new Action<AdjustSessionSuccess>(this.SessionSuccessCallback));
      adjustConfig.setSessionFailureDelegate(new Action<AdjustSessionFailure>(this.SessionFailureCallback));
      adjustConfig.setDeferredDeeplinkDelegate(new Action<string>(this.DeferredDeeplinkCallback));
      adjustConfig.setAttributionChangedDelegate(new Action<AdjustAttribution>(this.AttributionChangedCallback));
      Adjust.start(adjustConfig);
      this.isEnabled = true;
      this.txtManualLaunch = "SDK Launched";
    }
    if (GUI.Button(new Rect(0.0f, (float) (Screen.height / this.numberOfButtons), (float) Screen.width, (float) (Screen.height / this.numberOfButtons)), "Track Simple Event"))
      Adjust.trackEvent(new AdjustEvent("g3mfiw"));
    if (GUI.Button(new Rect(0.0f, (float) (Screen.height * 2 / this.numberOfButtons), (float) Screen.width, (float) (Screen.height / this.numberOfButtons)), "Track Revenue Event"))
    {
      AdjustEvent adjustEvent = new AdjustEvent("a4fd35");
      adjustEvent.setRevenue(0.25, "EUR");
      Adjust.trackEvent(adjustEvent);
    }
    if (GUI.Button(new Rect(0.0f, (float) (Screen.height * 3 / this.numberOfButtons), (float) Screen.width, (float) (Screen.height / this.numberOfButtons)), "Track Callback Event"))
    {
      AdjustEvent adjustEvent = new AdjustEvent("34vgg9");
      adjustEvent.addCallbackParameter("key", "value");
      adjustEvent.addCallbackParameter("foo", "bar");
      Adjust.trackEvent(adjustEvent);
    }
    if (GUI.Button(new Rect(0.0f, (float) (Screen.height * 4 / this.numberOfButtons), (float) Screen.width, (float) (Screen.height / this.numberOfButtons)), "Track Partner Event"))
    {
      AdjustEvent adjustEvent = new AdjustEvent("w788qs");
      adjustEvent.addPartnerParameter("key", "value");
      adjustEvent.addPartnerParameter("foo", "bar");
      Adjust.trackEvent(adjustEvent);
    }
    if (GUI.Button(new Rect(0.0f, (float) (Screen.height * 5 / this.numberOfButtons), (float) Screen.width, (float) (Screen.height / this.numberOfButtons)), this.txtSetOfflineMode))
    {
      if (string.Equals(this.txtSetOfflineMode, "Turn Offline Mode ON", StringComparison.OrdinalIgnoreCase))
      {
        Adjust.setOfflineMode(true);
        this.txtSetOfflineMode = "Turn Offline Mode OFF";
      }
      else
      {
        Adjust.setOfflineMode(false);
        this.txtSetOfflineMode = "Turn Offline Mode ON";
      }
    }
    if (GUI.Button(new Rect(0.0f, (float) (Screen.height * 6 / this.numberOfButtons), (float) Screen.width, (float) (Screen.height / this.numberOfButtons)), this.txtSetEnabled))
    {
      if (string.Equals(this.txtSetEnabled, "Disable SDK", StringComparison.OrdinalIgnoreCase))
      {
        Adjust.setEnabled(false);
        this.txtSetEnabled = "Enable SDK";
      }
      else
      {
        Adjust.setEnabled(true);
        this.txtSetEnabled = "Disable SDK";
      }
    }
    if (!GUI.Button(new Rect(0.0f, (float) (Screen.height * 7 / this.numberOfButtons), (float) Screen.width, (float) (Screen.height / this.numberOfButtons)), "Is SDK Enabled?"))
      return;
    this.isEnabled = Adjust.isEnabled();
    this.showPopUp = true;
  }

  private void ShowGUI(int windowID)
  {
    if (this.isEnabled)
      GUI.Label(new Rect(65f, 40f, 200f, 30f), "Adjust SDK is ENABLED!");
    else
      GUI.Label(new Rect(65f, 40f, 200f, 30f), "Adjust SDK is DISABLED!");
    if (!GUI.Button(new Rect(90f, 75f, 120f, 40f), "OK"))
      return;
    this.showPopUp = false;
  }

  public void HandleGooglePlayId(string adId)
  {
    Debug.Log((object) ("Google Play Ad ID = " + adId));
  }

  public void AttributionChangedCallback(AdjustAttribution attributionData)
  {
    Debug.Log((object) "Attribution changed!");
    if (attributionData.trackerName != null)
      Debug.Log((object) ("Tracker name: " + attributionData.trackerName));
    if (attributionData.trackerToken != null)
      Debug.Log((object) ("Tracker token: " + attributionData.trackerToken));
    if (attributionData.network != null)
      Debug.Log((object) ("Network: " + attributionData.network));
    if (attributionData.campaign != null)
      Debug.Log((object) ("Campaign: " + attributionData.campaign));
    if (attributionData.adgroup != null)
      Debug.Log((object) ("Adgroup: " + attributionData.adgroup));
    if (attributionData.creative != null)
      Debug.Log((object) ("Creative: " + attributionData.creative));
    if (attributionData.clickLabel != null)
      Debug.Log((object) ("Click label: " + attributionData.clickLabel));
    if (attributionData.adid == null)
      return;
    Debug.Log((object) ("ADID: " + attributionData.adid));
  }

  public void EventSuccessCallback(AdjustEventSuccess eventSuccessData)
  {
    Debug.Log((object) "Event tracked successfully!");
    if (eventSuccessData.Message != null)
      Debug.Log((object) ("Message: " + eventSuccessData.Message));
    if (eventSuccessData.Timestamp != null)
      Debug.Log((object) ("Timestamp: " + eventSuccessData.Timestamp));
    if (eventSuccessData.Adid != null)
      Debug.Log((object) ("Adid: " + eventSuccessData.Adid));
    if (eventSuccessData.EventToken != null)
      Debug.Log((object) ("EventToken: " + eventSuccessData.EventToken));
    if (eventSuccessData.CallbackId != null)
      Debug.Log((object) ("CallbackId: " + eventSuccessData.CallbackId));
    if (eventSuccessData.JsonResponse == null)
      return;
    Debug.Log((object) ("JsonResponse: " + eventSuccessData.GetJsonResponse()));
  }

  public void EventFailureCallback(AdjustEventFailure eventFailureData)
  {
    Debug.Log((object) "Event tracking failed!");
    if (eventFailureData.Message != null)
      Debug.Log((object) ("Message: " + eventFailureData.Message));
    if (eventFailureData.Timestamp != null)
      Debug.Log((object) ("Timestamp: " + eventFailureData.Timestamp));
    if (eventFailureData.Adid != null)
      Debug.Log((object) ("Adid: " + eventFailureData.Adid));
    if (eventFailureData.EventToken != null)
      Debug.Log((object) ("EventToken: " + eventFailureData.EventToken));
    if (eventFailureData.CallbackId != null)
      Debug.Log((object) ("CallbackId: " + eventFailureData.CallbackId));
    if (eventFailureData.JsonResponse != null)
      Debug.Log((object) ("JsonResponse: " + eventFailureData.GetJsonResponse()));
    Debug.Log((object) ("WillRetry: " + eventFailureData.WillRetry.ToString()));
  }

  public void SessionSuccessCallback(AdjustSessionSuccess sessionSuccessData)
  {
    Debug.Log((object) "Session tracked successfully!");
    if (sessionSuccessData.Message != null)
      Debug.Log((object) ("Message: " + sessionSuccessData.Message));
    if (sessionSuccessData.Timestamp != null)
      Debug.Log((object) ("Timestamp: " + sessionSuccessData.Timestamp));
    if (sessionSuccessData.Adid != null)
      Debug.Log((object) ("Adid: " + sessionSuccessData.Adid));
    if (sessionSuccessData.JsonResponse == null)
      return;
    Debug.Log((object) ("JsonResponse: " + sessionSuccessData.GetJsonResponse()));
  }

  public void SessionFailureCallback(AdjustSessionFailure sessionFailureData)
  {
    Debug.Log((object) "Session tracking failed!");
    if (sessionFailureData.Message != null)
      Debug.Log((object) ("Message: " + sessionFailureData.Message));
    if (sessionFailureData.Timestamp != null)
      Debug.Log((object) ("Timestamp: " + sessionFailureData.Timestamp));
    if (sessionFailureData.Adid != null)
      Debug.Log((object) ("Adid: " + sessionFailureData.Adid));
    if (sessionFailureData.JsonResponse != null)
      Debug.Log((object) ("JsonResponse: " + sessionFailureData.GetJsonResponse()));
    Debug.Log((object) ("WillRetry: " + sessionFailureData.WillRetry.ToString()));
  }

  private void DeferredDeeplinkCallback(string deeplinkURL)
  {
    Debug.Log((object) "Deferred deeplink reported!");
    if (deeplinkURL != null)
      Debug.Log((object) ("Deeplink URL: " + deeplinkURL));
    else
      Debug.Log((object) "Deeplink URL is null!");
  }
}
