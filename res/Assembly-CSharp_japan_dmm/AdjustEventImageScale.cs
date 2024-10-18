// Decompiled with JetBrains decompiler
// Type: AdjustEventImageScale
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
public class AdjustEventImageScale : MonoBehaviour
{
  [SerializeField]
  private float RevisionScaleValue;

  private void Start() => this.SetRect();

  private void SetRect()
  {
    Rect safeArea = SetCanvasBounds.GetSafeArea();
    if ((double) ((Rect) ref safeArea).width >= (double) Screen.width)
      return;
    RectTransform component = ((Component) this).gameObject.GetComponent<RectTransform>();
    float num = (float) (1.0 + (1.0 - (double) ((Rect) ref safeArea).width / (double) Screen.width)) + this.RevisionScaleValue;
    Vector2 vector2;
    // ISSUE: explicit constructor call
    ((Vector2) ref vector2).\u002Ector(component.sizeDelta.x * num, component.sizeDelta.y * num);
    component.sizeDelta = vector2;
  }
}
