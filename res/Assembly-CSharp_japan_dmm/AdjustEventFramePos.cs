// Decompiled with JetBrains decompiler
// Type: AdjustEventFramePos
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
public class AdjustEventFramePos : MonoBehaviour
{
  private void Awake()
  {
    RectTransform component = ((Component) this).GetComponent<RectTransform>();
    Rect safeArea = SetCanvasBounds.GetSafeArea();
    float num1 = ((Rect) ref safeArea).width / (float) Screen.width;
    int num2 = 0;
    if ((double) num1 < 1.0)
      num2 = (int) ((Rect) ref safeArea).x;
    int x = (int) SetCanvasBounds.GetAddFrame().x;
    int num3 = num2 + x;
    component.sizeDelta = new Vector2((float) num3, (float) Screen.height);
  }
}
