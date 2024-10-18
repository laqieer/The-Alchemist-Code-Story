// Decompiled with JetBrains decompiler
// Type: AdjustBtnSkipPos
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
public class AdjustBtnSkipPos : MonoBehaviour
{
  private void Start()
  {
    RectTransform component = ((Component) this).GetComponent<RectTransform>();
    Rect safeArea = SetCanvasBounds.GetSafeArea();
    float num1 = ((Rect) ref safeArea).width / (float) Screen.width;
    Vector2 anchoredPosition = component.anchoredPosition;
    float num2 = 0.0f;
    if ((double) num1 < 1.0)
      num2 -= ((Rect) ref safeArea).x;
    float num3 = num2 - SetCanvasBounds.GetAddFrame().x;
    if (Object.op_Inequality((Object) ((Component) this).transform.parent, (Object) null))
      num3 /= ((Component) this).transform.parent.localScale.x;
    anchoredPosition.x += num3;
    component.anchoredPosition = anchoredPosition;
  }
}
