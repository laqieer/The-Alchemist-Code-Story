// Decompiled with JetBrains decompiler
// Type: ServerTime
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
public class ServerTime : MonoBehaviour
{
  private void OnGUI()
  {
    GUILayout.BeginArea(new Rect((float) (Screen.width / 2 - 100), 0.0f, 200f, 30f));
    GUILayout.Label(string.Format("Time Offset: {0}", (object) (PhotonNetwork.ServerTimestamp - System.Environment.TickCount)), new GUILayoutOption[0]);
    if (GUILayout.Button("fetch", new GUILayoutOption[0]))
      PhotonNetwork.FetchServerTimestamp();
    GUILayout.EndArea();
  }
}
