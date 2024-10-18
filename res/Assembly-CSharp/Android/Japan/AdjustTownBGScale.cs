// Decompiled with JetBrains decompiler
// Type: AdjustTownBGScale
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class AdjustTownBGScale : MonoBehaviour
{
  [SerializeField]
  private float RevisionScaleValue = 0.1f;
  private Vector3 m_init_scale = Vector3.one;
  private bool m_set_scale_safe_area;
  private Rect m_safe_area;

  public float RevisionScale
  {
    get
    {
      return this.RevisionScaleValue;
    }
  }

  private void Start()
  {
    this.m_init_scale = this.transform.localScale;
    this.m_safe_area = SetCanvasBounds.GetSafeArea();
    if ((double) this.m_safe_area.width >= (double) Screen.width || this.m_set_scale_safe_area)
      return;
    this.SetScale();
  }

  private void SetScale()
  {
    this.m_set_scale_safe_area = !this.m_set_scale_safe_area;
    if (this.m_set_scale_safe_area)
    {
      float num = (float) (1.0 + (1.0 - (double) this.m_safe_area.width / (double) Screen.width)) + this.RevisionScaleValue;
      this.transform.localScale = new Vector3(num, num, 1f);
    }
    else
      this.transform.localScale = this.m_init_scale;
  }
}
