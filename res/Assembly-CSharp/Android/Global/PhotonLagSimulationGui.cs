// Decompiled with JetBrains decompiler
// Type: PhotonLagSimulationGui
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using ExitGames.Client.Photon;
using UnityEngine;

public class PhotonLagSimulationGui : MonoBehaviour
{
  public Rect WindowRect = new Rect(0.0f, 100f, 120f, 100f);
  public int WindowId = 101;
  public bool Visible = true;

  public PhotonPeer Peer { get; set; }

  public void Start()
  {
    this.Peer = (PhotonPeer) PhotonNetwork.networkingPeer;
  }

  public void OnGUI()
  {
    if (!this.Visible)
      return;
    if (this.Peer == null)
      this.WindowRect = GUILayout.Window(this.WindowId, this.WindowRect, new GUI.WindowFunction(this.NetSimHasNoPeerWindow), "Netw. Sim.");
    else
      this.WindowRect = GUILayout.Window(this.WindowId, this.WindowRect, new GUI.WindowFunction(this.NetSimWindow), "Netw. Sim.");
  }

  private void NetSimHasNoPeerWindow(int windowId)
  {
    GUILayout.Label("No peer to communicate with. ");
  }

  private void NetSimWindow(int windowId)
  {
    GUILayout.Label(string.Format("Rtt:{0,4} +/-{1,3}", (object) this.Peer.RoundTripTime, (object) this.Peer.RoundTripTimeVariance));
    bool simulationEnabled = this.Peer.IsSimulationEnabled;
    bool flag = GUILayout.Toggle(simulationEnabled, "Simulate");
    if (flag != simulationEnabled)
      this.Peer.IsSimulationEnabled = flag;
    float incomingLag = (float) this.Peer.NetworkSimulationSettings.IncomingLag;
    GUILayout.Label("Lag " + (object) incomingLag);
    float num1 = GUILayout.HorizontalSlider(incomingLag, 0.0f, 500f);
    this.Peer.NetworkSimulationSettings.IncomingLag = (int) num1;
    this.Peer.NetworkSimulationSettings.OutgoingLag = (int) num1;
    float incomingJitter = (float) this.Peer.NetworkSimulationSettings.IncomingJitter;
    GUILayout.Label("Jit " + (object) incomingJitter);
    float num2 = GUILayout.HorizontalSlider(incomingJitter, 0.0f, 100f);
    this.Peer.NetworkSimulationSettings.IncomingJitter = (int) num2;
    this.Peer.NetworkSimulationSettings.OutgoingJitter = (int) num2;
    float incomingLossPercentage = (float) this.Peer.NetworkSimulationSettings.IncomingLossPercentage;
    GUILayout.Label("Loss " + (object) incomingLossPercentage);
    float num3 = GUILayout.HorizontalSlider(incomingLossPercentage, 0.0f, 10f);
    this.Peer.NetworkSimulationSettings.IncomingLossPercentage = (int) num3;
    this.Peer.NetworkSimulationSettings.OutgoingLossPercentage = (int) num3;
    if (GUI.changed)
      this.WindowRect.height = 100f;
    GUI.DragWindow();
  }
}
