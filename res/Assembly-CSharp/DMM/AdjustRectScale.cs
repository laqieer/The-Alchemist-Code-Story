// Decompiled with JetBrains decompiler
// Type: AdjustRectScale
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
public class AdjustRectScale : MonoBehaviour
{
  [SerializeField]
  private float SetScale = 1f;
  private Rect lastSafeArea = new Rect(0.0f, 0.0f, 0.0f, 0.0f);
  private Vector3 initScale = Vector3.one;
  private float lastSetScale;

  private void Start()
  {
    RectTransform component = ((Component) this).GetComponent<RectTransform>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    this.initScale = ((Component) component).transform.localScale;
    this.ApplySafeAreaScale(SetCanvasBounds.GetSafeArea(true));
  }

  private void ApplySafeAreaScale(Rect area)
  {
    RectTransform component = ((Component) this).GetComponent<RectTransform>();
    if (Object.op_Inequality((Object) component, (Object) null))
    {
      if ((double) (((Rect) ref area).width / (float) Screen.width) < 1.0)
        ((Transform) component).localScale = new Vector3(this.SetScale, this.SetScale, this.SetScale);
      else
        ((Transform) component).localScale = this.initScale;
    }
    this.lastSafeArea = area;
    this.lastSetScale = this.SetScale;
  }
}
