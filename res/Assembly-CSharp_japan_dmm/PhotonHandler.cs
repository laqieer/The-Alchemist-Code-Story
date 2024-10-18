// Decompiled with JetBrains decompiler
// Type: PhotonHandler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using ExitGames.Client.Photon;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

#nullable disable
internal class PhotonHandler : MonoBehaviour
{
  public static PhotonHandler SP;
  public int updateInterval;
  public int updateIntervalOnSerialize;
  private int nextSendTickCount;
  private int nextSendTickCountOnSerialize;
  private static bool sendThreadShouldRun;
  private static Stopwatch timerToStopConnectionInBackground;
  protected internal static bool AppQuits;
  protected internal static System.Type PingImplementation;
  private const string PlayerPrefsKey = "PUNCloudBestRegion";

  protected void Awake()
  {
    if (Object.op_Inequality((Object) PhotonHandler.SP, (Object) null) && Object.op_Inequality((Object) PhotonHandler.SP, (Object) this) && Object.op_Inequality((Object) ((Component) PhotonHandler.SP).gameObject, (Object) null))
      Object.DestroyImmediate((Object) ((Component) PhotonHandler.SP).gameObject);
    PhotonHandler.SP = this;
    Object.DontDestroyOnLoad((Object) ((Component) this).gameObject);
    this.updateInterval = 1000 / PhotonNetwork.sendRate;
    this.updateIntervalOnSerialize = 1000 / PhotonNetwork.sendRateOnSerialize;
    PhotonHandler.StartFallbackSendAckThread();
  }

  protected void Start()
  {
    // ISSUE: reference to a compiler-generated field
    if (PhotonHandler.\u003C\u003Ef__am\u0024cache0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      PhotonHandler.\u003C\u003Ef__am\u0024cache0 = new UnityAction<Scene, LoadSceneMode>((object) null, __methodptr(\u003CStart\u003Em__0));
    }
    // ISSUE: reference to a compiler-generated field
    SceneManager.sceneLoaded += PhotonHandler.\u003C\u003Ef__am\u0024cache0;
  }

  protected void OnApplicationQuit()
  {
    PhotonHandler.AppQuits = true;
    PhotonHandler.StopFallbackSendAckThread();
    PhotonNetwork.Disconnect();
  }

  protected void OnApplicationPause(bool pause)
  {
    if ((double) PhotonNetwork.BackgroundTimeout <= 0.10000000149011612)
      return;
    if (PhotonHandler.timerToStopConnectionInBackground == null)
      PhotonHandler.timerToStopConnectionInBackground = new Stopwatch();
    PhotonHandler.timerToStopConnectionInBackground.Reset();
    if (pause)
      PhotonHandler.timerToStopConnectionInBackground.Start();
    else
      PhotonHandler.timerToStopConnectionInBackground.Stop();
  }

  protected void OnDestroy() => PhotonHandler.StopFallbackSendAckThread();

  protected void Update()
  {
    if (PhotonNetwork.networkingPeer == null)
    {
      Debug.LogError((object) "NetworkPeer broke!");
    }
    else
    {
      if (PhotonNetwork.connectionStateDetailed == ClientState.PeerCreated || PhotonNetwork.connectionStateDetailed == ClientState.Disconnected || PhotonNetwork.offlineMode || !PhotonNetwork.isMessageQueueRunning)
        return;
      bool flag1 = true;
      while (PhotonNetwork.isMessageQueueRunning && flag1)
        flag1 = PhotonNetwork.networkingPeer.DispatchIncomingCommands();
      int num1 = (int) ((double) Time.realtimeSinceStartup * 1000.0);
      if (PhotonNetwork.isMessageQueueRunning && num1 > this.nextSendTickCountOnSerialize)
      {
        PhotonNetwork.networkingPeer.RunViewUpdate();
        this.nextSendTickCountOnSerialize = num1 + this.updateIntervalOnSerialize;
        this.nextSendTickCount = 0;
      }
      int num2 = (int) ((double) Time.realtimeSinceStartup * 1000.0);
      if (num2 <= this.nextSendTickCount)
        return;
      bool flag2 = true;
      while (PhotonNetwork.isMessageQueueRunning && flag2)
        flag2 = PhotonNetwork.networkingPeer.SendOutgoingCommands();
      this.nextSendTickCount = num2 + this.updateInterval;
    }
  }

  protected void OnJoinedRoom() => PhotonNetwork.networkingPeer.LoadLevelIfSynced();

  protected void OnCreatedRoom()
  {
    PhotonNetwork.networkingPeer.SetLevelInPropsIfSynced((object) SceneManagerHelper.ActiveSceneName, false);
  }

  public static void StartFallbackSendAckThread()
  {
    if (PhotonHandler.sendThreadShouldRun)
      return;
    PhotonHandler.sendThreadShouldRun = true;
    // ISSUE: reference to a compiler-generated field
    if (PhotonHandler.\u003C\u003Ef__mg\u0024cache0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      PhotonHandler.\u003C\u003Ef__mg\u0024cache0 = new Func<bool>(PhotonHandler.FallbackSendAckThread);
    }
    // ISSUE: reference to a compiler-generated field
    int num = (int) SupportClass.StartBackgroundCalls(PhotonHandler.\u003C\u003Ef__mg\u0024cache0, 100, string.Empty);
  }

  public static void StopFallbackSendAckThread() => PhotonHandler.sendThreadShouldRun = false;

  public static bool FallbackSendAckThread()
  {
    if (PhotonHandler.sendThreadShouldRun && !PhotonNetwork.offlineMode && PhotonNetwork.networkingPeer != null)
    {
      if (PhotonHandler.timerToStopConnectionInBackground != null && (double) PhotonNetwork.BackgroundTimeout > 0.10000000149011612 && (double) PhotonHandler.timerToStopConnectionInBackground.ElapsedMilliseconds > (double) PhotonNetwork.BackgroundTimeout * 1000.0)
      {
        if (PhotonNetwork.connected)
          PhotonNetwork.Disconnect();
        PhotonHandler.timerToStopConnectionInBackground.Stop();
        PhotonHandler.timerToStopConnectionInBackground.Reset();
        return PhotonHandler.sendThreadShouldRun;
      }
      if (!PhotonNetwork.isMessageQueueRunning || PhotonNetwork.networkingPeer.ConnectionTime - PhotonNetwork.networkingPeer.LastSendOutgoingTime > 200)
        PhotonNetwork.networkingPeer.SendAcksOnly();
    }
    return PhotonHandler.sendThreadShouldRun;
  }

  internal static CloudRegionCode BestRegionCodeInPreferences
  {
    get
    {
      string codeAsString = PlayerPrefs.GetString("PUNCloudBestRegion", string.Empty);
      return !string.IsNullOrEmpty(codeAsString) ? Region.Parse(codeAsString) : CloudRegionCode.none;
    }
    set
    {
      if (value == CloudRegionCode.none)
        PlayerPrefs.DeleteKey("PUNCloudBestRegion");
      else
        PlayerPrefs.SetString("PUNCloudBestRegion", value.ToString());
    }
  }

  protected internal static void PingAvailableRegionsAndConnectToBest()
  {
    PhotonHandler.SP.StartCoroutine(PhotonHandler.SP.PingAvailableRegionsCoroutine(true));
  }

  [DebuggerHidden]
  internal IEnumerator PingAvailableRegionsCoroutine(bool connectToBest)
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new PhotonHandler.\u003CPingAvailableRegionsCoroutine\u003Ec__Iterator0()
    {
      connectToBest = connectToBest
    };
  }
}
