// Decompiled with JetBrains decompiler
// Type: AdjustBtnSkipPos
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class AdjustBtnSkipPos : MonoBehaviour
{
  [SerializeField]
  private int OffsetPtx;

  private void Awake()
  {
    RectTransform component = this.GetComponent<RectTransform>();
    Rect safeArea = SetCanvasBounds.GetSafeArea();
    if ((double) (safeArea.width / (float) Screen.width) >= 1.0)
      return;
    Vector2 anchoredPosition = component.anchoredPosition;
    anchoredPosition.x -= safeArea.x / 3f * (float) (2 + this.OffsetPtx);
    component.anchoredPosition = anchoredPosition;
  }
}
