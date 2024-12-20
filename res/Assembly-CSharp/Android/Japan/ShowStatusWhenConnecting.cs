﻿// Decompiled with JetBrains decompiler
// Type: ShowStatusWhenConnecting
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class ShowStatusWhenConnecting : MonoBehaviour
{
  public GUISkin Skin;

  private void OnGUI()
  {
    if ((Object) this.Skin != (Object) null)
      GUI.skin = this.Skin;
    float width = 400f;
    float height = 100f;
    GUILayout.BeginArea(new Rect((float) (((double) Screen.width - (double) width) / 2.0), (float) (((double) Screen.height - (double) height) / 2.0), width, height), GUI.skin.box);
    GUILayout.Label("Connecting" + this.GetConnectingDots(), GUI.skin.customStyles[0], new GUILayoutOption[0]);
    GUILayout.Label("Status: " + (object) PhotonNetwork.connectionStateDetailed);
    GUILayout.EndArea();
    if (!PhotonNetwork.inRoom)
      return;
    this.enabled = false;
  }

  private string GetConnectingDots()
  {
    string empty = string.Empty;
    int num = Mathf.FloorToInt((float) ((double) Time.timeSinceLevelLoad * 3.0 % 4.0));
    for (int index = 0; index < num; ++index)
      empty += " .";
    return empty;
  }
}
