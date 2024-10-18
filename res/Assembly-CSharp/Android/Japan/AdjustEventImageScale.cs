// Decompiled with JetBrains decompiler
// Type: AdjustEventImageScale
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class AdjustEventImageScale : MonoBehaviour
{
  [SerializeField]
  private float RevisionScaleValue;

  private void Start()
  {
    this.SetRect();
  }

  private void SetRect()
  {
    Rect safeArea = SetCanvasBounds.GetSafeArea();
    if ((double) safeArea.width >= (double) Screen.width)
      return;
    RectTransform component = this.gameObject.GetComponent<RectTransform>();
    float num = (float) (1.0 + (1.0 - (double) safeArea.width / (double) Screen.width)) + this.RevisionScaleValue;
    Vector2 vector2 = new Vector2(component.sizeDelta.x * num, component.sizeDelta.y * num);
    component.sizeDelta = vector2;
  }
}
