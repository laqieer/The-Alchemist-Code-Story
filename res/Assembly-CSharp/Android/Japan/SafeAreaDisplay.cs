// Decompiled with JetBrains decompiler
// Type: SafeAreaDisplay
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class SafeAreaDisplay : MonoBehaviour
{
  private float mDeltaTime;
  public Text SafeArea;

  private void Start()
  {
    Object.Destroy((Object) this.gameObject);
  }

  private void Update()
  {
    Rect safeArea = SetCanvasBounds.GetSafeArea();
    string empty = string.Empty;
    string str = "00ff00";
    this.SafeArea.text = string.Format("<color=#{0}>rect_x = {1}</color>\n", (object) str, (object) safeArea.x) + string.Format("<color=#{0}>rect_y = {1}</color>\n", (object) str, (object) safeArea.y) + string.Format("<color=#{0}>rect_w = {1}</color>\n", (object) str, (object) safeArea.width) + string.Format("<color=#{0}>rect_h = {1}</color>\n", (object) str, (object) safeArea.height);
  }
}
