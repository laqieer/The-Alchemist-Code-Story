// Decompiled with JetBrains decompiler
// Type: AdjustRectScale
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class AdjustRectScale : MonoBehaviour
{
  [SerializeField]
  private float SetScale = 1f;
  private Rect lastSafeArea = new Rect(0.0f, 0.0f, 0.0f, 0.0f);
  private Vector3 initScale = Vector3.one;
  private float lastSetScale;

  private void Start()
  {
    RectTransform component = this.GetComponent<RectTransform>();
    if (!((Object) component != (Object) null))
      return;
    this.initScale = component.transform.localScale;
    this.ApplySafeAreaScale(SetCanvasBounds.GetSafeArea());
  }

  private void ApplySafeAreaScale(Rect area)
  {
    RectTransform component = this.GetComponent<RectTransform>();
    if ((Object) component != (Object) null)
    {
      if ((double) (area.width / (float) Screen.width) < 1.0)
        component.localScale = new Vector3(this.SetScale, this.SetScale, this.SetScale);
      else
        component.localScale = this.initScale;
    }
    this.lastSafeArea = area;
    this.lastSetScale = this.SetScale;
  }
}
