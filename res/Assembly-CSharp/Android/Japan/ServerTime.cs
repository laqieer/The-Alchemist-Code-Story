// Decompiled with JetBrains decompiler
// Type: ServerTime
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
