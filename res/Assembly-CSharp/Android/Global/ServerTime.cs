// Decompiled with JetBrains decompiler
// Type: ServerTime
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;

public class ServerTime : MonoBehaviour
{
  private void OnGUI()
  {
    GUILayout.BeginArea(new Rect((float) (Screen.width / 2 - 100), 0.0f, 200f, 30f));
    GUILayout.Label(string.Format("Time Offset: {0}", (object) (PhotonNetwork.ServerTimestamp - Environment.TickCount)));
    if (GUILayout.Button("fetch"))
      PhotonNetwork.FetchServerTimestamp();
    GUILayout.EndArea();
  }
}
