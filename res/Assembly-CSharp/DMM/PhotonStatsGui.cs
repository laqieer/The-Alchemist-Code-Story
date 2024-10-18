// Decompiled with JetBrains decompiler
// Type: PhotonStatsGui
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using ExitGames.Client.Photon;
using UnityEngine;

#nullable disable
public class PhotonStatsGui : MonoBehaviour
{
  public bool statsWindowOn = true;
  public bool statsOn = true;
  public bool healthStatsVisible;
  public bool trafficStatsOn;
  public bool buttonsOn;
  public Rect statsRect = new Rect(0.0f, 100f, 200f, 50f);
  public int WindowId = 100;

  public void Start()
  {
    if ((double) ((Rect) ref this.statsRect).x > 0.0)
      return;
    ((Rect) ref this.statsRect).x = (float) Screen.width - ((Rect) ref this.statsRect).width;
  }

  public void Update()
  {
    if (!Input.GetKeyDown((KeyCode) 9) || !Input.GetKey((KeyCode) 304))
      return;
    this.statsWindowOn = !this.statsWindowOn;
    this.statsOn = true;
  }

  public void OnGUI()
  {
    if (PhotonNetwork.networkingPeer.TrafficStatsEnabled != this.statsOn)
      PhotonNetwork.networkingPeer.TrafficStatsEnabled = this.statsOn;
    if (!this.statsWindowOn)
      return;
    // ISSUE: method pointer
    this.statsRect = GUILayout.Window(this.WindowId, this.statsRect, new GUI.WindowFunction((object) this, __methodptr(TrafficStatsWindow)), "Messages (shift+tab)", new GUILayoutOption[0]);
  }

  public void TrafficStatsWindow(int windowID)
  {
    bool flag = false;
    TrafficStatsGameLevel trafficStatsGameLevel = PhotonNetwork.networkingPeer.TrafficStatsGameLevel;
    long num = PhotonNetwork.networkingPeer.TrafficStatsElapsedMs / 1000L;
    if (num == 0L)
      num = 1L;
    GUILayout.BeginHorizontal(new GUILayoutOption[0]);
    this.buttonsOn = GUILayout.Toggle(this.buttonsOn, "buttons", new GUILayoutOption[0]);
    this.healthStatsVisible = GUILayout.Toggle(this.healthStatsVisible, "health", new GUILayoutOption[0]);
    this.trafficStatsOn = GUILayout.Toggle(this.trafficStatsOn, "traffic", new GUILayoutOption[0]);
    GUILayout.EndHorizontal();
    string str1 = string.Format("Out {0,4} | In {1,4} | Sum {2,4}", (object) trafficStatsGameLevel.TotalOutgoingMessageCount, (object) trafficStatsGameLevel.TotalIncomingMessageCount, (object) trafficStatsGameLevel.TotalMessageCount);
    string str2 = string.Format("{0}sec average:", (object) num);
    string str3 = string.Format("Out {0,4} | In {1,4} | Sum {2,4}", (object) ((long) trafficStatsGameLevel.TotalOutgoingMessageCount / num), (object) ((long) trafficStatsGameLevel.TotalIncomingMessageCount / num), (object) ((long) trafficStatsGameLevel.TotalMessageCount / num));
    GUILayout.Label(str1, new GUILayoutOption[0]);
    GUILayout.Label(str2, new GUILayoutOption[0]);
    GUILayout.Label(str3, new GUILayoutOption[0]);
    if (this.buttonsOn)
    {
      GUILayout.BeginHorizontal(new GUILayoutOption[0]);
      this.statsOn = GUILayout.Toggle(this.statsOn, "stats on", new GUILayoutOption[0]);
      if (GUILayout.Button("Reset", new GUILayoutOption[0]))
      {
        PhotonNetwork.networkingPeer.TrafficStatsReset();
        PhotonNetwork.networkingPeer.TrafficStatsEnabled = true;
      }
      flag = GUILayout.Button("To Log", new GUILayoutOption[0]);
      GUILayout.EndHorizontal();
    }
    string str4 = string.Empty;
    string str5 = string.Empty;
    if (this.trafficStatsOn)
    {
      GUILayout.Box("Traffic Stats", new GUILayoutOption[0]);
      str4 = "Incoming: \n" + PhotonNetwork.networkingPeer.TrafficStatsIncoming.ToString();
      str5 = "Outgoing: \n" + PhotonNetwork.networkingPeer.TrafficStatsOutgoing.ToString();
      GUILayout.Label(str4, new GUILayoutOption[0]);
      GUILayout.Label(str5, new GUILayoutOption[0]);
    }
    string str6 = string.Empty;
    if (this.healthStatsVisible)
    {
      GUILayout.Box("Health Stats", new GUILayoutOption[0]);
      str6 = string.Format("ping: {6}[+/-{7}]ms resent:{8} \n\nmax ms between\nsend: {0,4} \ndispatch: {1,4} \n\nlongest dispatch for: \nev({3}):{2,3}ms \nop({5}):{4,3}ms", (object) trafficStatsGameLevel.LongestDeltaBetweenSending, (object) trafficStatsGameLevel.LongestDeltaBetweenDispatching, (object) trafficStatsGameLevel.LongestEventCallback, (object) trafficStatsGameLevel.LongestEventCallbackCode, (object) trafficStatsGameLevel.LongestOpResponseCallback, (object) trafficStatsGameLevel.LongestOpResponseCallbackOpCode, (object) PhotonNetwork.networkingPeer.RoundTripTime, (object) PhotonNetwork.networkingPeer.RoundTripTimeVariance, (object) PhotonNetwork.networkingPeer.ResentReliableCommands);
      GUILayout.Label(str6, new GUILayoutOption[0]);
    }
    if (flag)
      Debug.Log((object) string.Format("{0}\n{1}\n{2}\n{3}\n{4}\n{5}", (object) str1, (object) str2, (object) str3, (object) str4, (object) str5, (object) str6));
    if (GUI.changed)
      ((Rect) ref this.statsRect).height = 100f;
    GUI.DragWindow();
  }
}
