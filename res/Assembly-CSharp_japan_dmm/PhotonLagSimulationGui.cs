// Decompiled with JetBrains decompiler
// Type: PhotonLagSimulationGui
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using ExitGames.Client.Photon;
using UnityEngine;

#nullable disable
public class PhotonLagSimulationGui : MonoBehaviour
{
  public Rect WindowRect = new Rect(0.0f, 100f, 120f, 100f);
  public int WindowId = 101;
  public bool Visible = true;

  public PhotonPeer Peer { get; set; }

  public void Start() => this.Peer = (PhotonPeer) PhotonNetwork.networkingPeer;

  public void OnGUI()
  {
    if (!this.Visible)
      return;
    if (this.Peer == null)
    {
      // ISSUE: method pointer
      this.WindowRect = GUILayout.Window(this.WindowId, this.WindowRect, new GUI.WindowFunction((object) this, __methodptr(NetSimHasNoPeerWindow)), "Netw. Sim.", new GUILayoutOption[0]);
    }
    else
    {
      // ISSUE: method pointer
      this.WindowRect = GUILayout.Window(this.WindowId, this.WindowRect, new GUI.WindowFunction((object) this, __methodptr(NetSimWindow)), "Netw. Sim.", new GUILayoutOption[0]);
    }
  }

  private void NetSimHasNoPeerWindow(int windowId)
  {
    GUILayout.Label("No peer to communicate with. ", new GUILayoutOption[0]);
  }

  private void NetSimWindow(int windowId)
  {
    GUILayout.Label(string.Format("Rtt:{0,4} +/-{1,3}", (object) this.Peer.RoundTripTime, (object) this.Peer.RoundTripTimeVariance), new GUILayoutOption[0]);
    bool simulationEnabled = this.Peer.IsSimulationEnabled;
    bool flag = GUILayout.Toggle(simulationEnabled, "Simulate", new GUILayoutOption[0]);
    if (flag != simulationEnabled)
      this.Peer.IsSimulationEnabled = flag;
    float incomingLag = (float) this.Peer.NetworkSimulationSettings.IncomingLag;
    GUILayout.Label("Lag " + (object) incomingLag, new GUILayoutOption[0]);
    float num1 = GUILayout.HorizontalSlider(incomingLag, 0.0f, 500f, new GUILayoutOption[0]);
    this.Peer.NetworkSimulationSettings.IncomingLag = (int) num1;
    this.Peer.NetworkSimulationSettings.OutgoingLag = (int) num1;
    float incomingJitter = (float) this.Peer.NetworkSimulationSettings.IncomingJitter;
    GUILayout.Label("Jit " + (object) incomingJitter, new GUILayoutOption[0]);
    float num2 = GUILayout.HorizontalSlider(incomingJitter, 0.0f, 100f, new GUILayoutOption[0]);
    this.Peer.NetworkSimulationSettings.IncomingJitter = (int) num2;
    this.Peer.NetworkSimulationSettings.OutgoingJitter = (int) num2;
    float incomingLossPercentage = (float) this.Peer.NetworkSimulationSettings.IncomingLossPercentage;
    GUILayout.Label("Loss " + (object) incomingLossPercentage, new GUILayoutOption[0]);
    float num3 = GUILayout.HorizontalSlider(incomingLossPercentage, 0.0f, 10f, new GUILayoutOption[0]);
    this.Peer.NetworkSimulationSettings.IncomingLossPercentage = (int) num3;
    this.Peer.NetworkSimulationSettings.OutgoingLossPercentage = (int) num3;
    if (GUI.changed)
      ((Rect) ref this.WindowRect).height = 100f;
    GUI.DragWindow();
  }
}
