// Decompiled with JetBrains decompiler
// Type: SafeAreaDisplay
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class SafeAreaDisplay : MonoBehaviour
{
  private float mDeltaTime;
  public Text SafeArea;

  private void Start() => Object.Destroy((Object) ((Component) this).gameObject);

  private void Update()
  {
    Rect safeArea = SetCanvasBounds.GetSafeArea();
    string empty = string.Empty;
    string str = "00ff00";
    this.SafeArea.text = string.Format("<color=#{0}>rect_x = {1}</color>\n", (object) str, (object) ((Rect) ref safeArea).x) + string.Format("<color=#{0}>rect_y = {1}</color>\n", (object) str, (object) ((Rect) ref safeArea).y) + string.Format("<color=#{0}>rect_w = {1}</color>\n", (object) str, (object) ((Rect) ref safeArea).width) + string.Format("<color=#{0}>rect_h = {1}</color>\n", (object) str, (object) ((Rect) ref safeArea).height);
  }
}
