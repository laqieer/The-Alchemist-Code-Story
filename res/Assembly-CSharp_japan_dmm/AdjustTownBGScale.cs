// Decompiled with JetBrains decompiler
// Type: AdjustTownBGScale
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
public class AdjustTownBGScale : MonoBehaviour
{
  [SerializeField]
  private float RevisionScaleValue = 0.1f;
  private bool m_set_scale_safe_area;
  private Vector3 m_init_scale = Vector3.one;
  private Rect m_safe_area;

  public float RevisionScale => this.RevisionScaleValue;

  private void Start()
  {
    this.m_init_scale = ((Component) this).transform.localScale;
    this.m_safe_area = SetCanvasBounds.GetSafeArea(true);
    if ((double) ((Rect) ref this.m_safe_area).width >= (double) Screen.width || this.m_set_scale_safe_area)
      return;
    this.SetScale();
  }

  private void SetScale()
  {
    this.m_set_scale_safe_area = !this.m_set_scale_safe_area;
    if (this.m_set_scale_safe_area)
    {
      float num = (float) (1.0 + (1.0 - (double) ((Rect) ref this.m_safe_area).width / (double) Screen.width)) + this.RevisionScaleValue;
      Vector3 vector3;
      // ISSUE: explicit constructor call
      ((Vector3) ref vector3).\u002Ector(num, num, 1f);
      ((Component) this).transform.localScale = vector3;
    }
    else
      ((Component) this).transform.localScale = this.m_init_scale;
  }
}
