// Decompiled with JetBrains decompiler
// Type: PhotonStatsGui
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using ExitGames.Client.Photon;
using UnityEngine;

public class PhotonStatsGui : MonoBehaviour
{
  public bool statsWindowOn = true;
  public bool statsOn = true;
  public Rect statsRect = new Rect(0.0f, 100f, 200f, 50f);
  public int WindowId = 100;
  public bool healthStatsVisible;
  public bool trafficStatsOn;
  public bool buttonsOn;

  public void Start()
  {
    if ((double) this.statsRect.x > 0.0)
      return;
    this.statsRect.x = (float) Screen.width - this.statsRect.width;
  }

  public void Update()
  {
    if (!Input.GetKeyDown(KeyCode.Tab) || !Input.GetKey(KeyCode.LeftShift))
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
    this.statsRect = GUILayout.Window(this.WindowId, this.statsRect, new GUI.WindowFunction(this.TrafficStatsWindow), "Messages (shift+tab)");
  }

  public void TrafficStatsWindow(int windowID)
  {
    bool flag = false;
    TrafficStatsGameLevel trafficStatsGameLevel = PhotonNetwork.networkingPeer.TrafficStatsGameLevel;
    long num = PhotonNetwork.networkingPeer.TrafficStatsElapsedMs / 1000L;
    if (num == 0L)
      num = 1L;
    GUILayout.BeginHorizontal();
    this.buttonsOn = GUILayout.Toggle(this.buttonsOn, "buttons");
    this.healthStatsVisible = GUILayout.Toggle(this.healthStatsVisible, "health");
    this.trafficStatsOn = GUILayout.Toggle(this.trafficStatsOn, "traffic");
    GUILayout.EndHorizontal();
    string text1 = string.Format("Out {0,4} | In {1,4} | Sum {2,4}", (object) trafficStatsGameLevel.TotalOutgoingMessageCount, (object) trafficStatsGameLevel.TotalIncomingMessageCount, (object) trafficStatsGameLevel.TotalMessageCount);
    string text2 = string.Format("{0}sec average:", (object) num);
    string text3 = string.Format("Out {0,4} | In {1,4} | Sum {2,4}", (object) ((long) trafficStatsGameLevel.TotalOutgoingMessageCount / num), (object) ((long) trafficStatsGameLevel.TotalIncomingMessageCount / num), (object) ((long) trafficStatsGameLevel.TotalMessageCount / num));
    GUILayout.Label(text1);
    GUILayout.Label(text2);
    GUILayout.Label(text3);
    if (this.buttonsOn)
    {
      GUILayout.BeginHorizontal();
      this.statsOn = GUILayout.Toggle(this.statsOn, "stats on");
      if (GUILayout.Button("Reset"))
      {
        PhotonNetwork.networkingPeer.TrafficStatsReset();
        PhotonNetwork.networkingPeer.TrafficStatsEnabled = true;
      }
      flag = GUILayout.Button("To Log");
      GUILayout.EndHorizontal();
    }
    string text4 = string.Empty;
    string text5 = string.Empty;
    if (this.trafficStatsOn)
    {
      GUILayout.Box("Traffic Stats");
      text4 = "Incoming: \n" + PhotonNetwork.networkingPeer.TrafficStatsIncoming.ToString();
      text5 = "Outgoing: \n" + PhotonNetwork.networkingPeer.TrafficStatsOutgoing.ToString();
      GUILayout.Label(text4);
      GUILayout.Label(text5);
    }
    string text6 = string.Empty;
    if (this.healthStatsVisible)
    {
      GUILayout.Box("Health Stats");
      text6 = string.Format("ping: {6}[+/-{7}]ms resent:{8} \n\nmax ms between\nsend: {0,4} \ndispatch: {1,4} \n\nlongest dispatch for: \nev({3}):{2,3}ms \nop({5}):{4,3}ms", (object) trafficStatsGameLevel.LongestDeltaBetweenSending, (object) trafficStatsGameLevel.LongestDeltaBetweenDispatching, (object) trafficStatsGameLevel.LongestEventCallback, (object) trafficStatsGameLevel.LongestEventCallbackCode, (object) trafficStatsGameLevel.LongestOpResponseCallback, (object) trafficStatsGameLevel.LongestOpResponseCallbackOpCode, (object) PhotonNetwork.networkingPeer.RoundTripTime, (object) PhotonNetwork.networkingPeer.RoundTripTimeVariance, (object) PhotonNetwork.networkingPeer.ResentReliableCommands);
      GUILayout.Label(text6);
    }
    if (flag)
      Debug.Log((object) string.Format("{0}\n{1}\n{2}\n{3}\n{4}\n{5}", (object) text1, (object) text2, (object) text3, (object) text4, (object) text5, (object) text6));
    if (GUI.changed)
      this.statsRect.height = 100f;
    GUI.DragWindow();
  }
}
