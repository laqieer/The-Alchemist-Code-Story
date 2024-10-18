// Decompiled with JetBrains decompiler
// Type: ShowStatusWhenConnecting
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
public class ShowStatusWhenConnecting : MonoBehaviour
{
  public GUISkin Skin;

  private void OnGUI()
  {
    if (Object.op_Inequality((Object) this.Skin, (Object) null))
      GUI.skin = this.Skin;
    float num1 = 400f;
    float num2 = 100f;
    Rect rect;
    // ISSUE: explicit constructor call
    ((Rect) ref rect).\u002Ector((float) (((double) Screen.width - (double) num1) / 2.0), (float) (((double) Screen.height - (double) num2) / 2.0), num1, num2);
    GUILayout.BeginArea(rect, GUI.skin.box);
    GUILayout.Label("Connecting" + this.GetConnectingDots(), GUI.skin.customStyles[0], new GUILayoutOption[0]);
    GUILayout.Label("Status: " + (object) PhotonNetwork.connectionStateDetailed, new GUILayoutOption[0]);
    GUILayout.EndArea();
    if (!PhotonNetwork.inRoom)
      return;
    ((Behaviour) this).enabled = false;
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
