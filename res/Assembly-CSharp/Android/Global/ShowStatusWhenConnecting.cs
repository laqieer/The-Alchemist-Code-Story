// Decompiled with JetBrains decompiler
// Type: ShowStatusWhenConnecting
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
