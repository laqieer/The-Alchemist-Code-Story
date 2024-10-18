// Decompiled with JetBrains decompiler
// Type: CircularLayoutGroup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
[AddComponentMenu("Layout/Circular Layout Group")]
public class CircularLayoutGroup : LayoutGroup
{
  [SerializeField]
  protected float m_AngleMin;
  [SerializeField]
  protected float m_AngleMax = 90f;
  [SerializeField]
  protected float m_Radius = 100f;
  [SerializeField]
  [Range(0.0f, 1f)]
  protected float m_Fraction = 0.5f;
  [SerializeField]
  protected bool m_UseFullRange = true;
  [SerializeField]
  protected float m_FixedStride;

  public float AngleMin
  {
    get => this.m_AngleMin;
    set => this.SetProperty<float>(ref this.m_AngleMin, value);
  }

  public float AngleMax
  {
    get => this.m_AngleMax;
    set => this.SetProperty<float>(ref this.m_AngleMax, value);
  }

  public float Radius
  {
    get => this.m_Radius;
    set => this.SetProperty<float>(ref this.m_Radius, value);
  }

  public float Fraction
  {
    get => this.m_Fraction;
    set => this.SetProperty<float>(ref this.m_Fraction, value);
  }

  public bool UseFullRange
  {
    get => this.m_UseFullRange;
    set => this.SetProperty<bool>(ref this.m_UseFullRange, value);
  }

  public float FixedStride
  {
    get => this.m_FixedStride;
    set => this.SetProperty<float>(ref this.m_FixedStride, value);
  }

  public virtual void CalculateLayoutInputHorizontal() => base.CalculateLayoutInputHorizontal();

  public virtual void CalculateLayoutInputVertical()
  {
  }

  public virtual void SetLayoutHorizontal()
  {
  }

  public virtual void SetLayoutVertical()
  {
    RectTransform transform = ((Component) this).transform as RectTransform;
    int childCount = ((Transform) transform).childCount;
    RectTransform[] rectTransformArray = new RectTransform[childCount];
    int num1 = 0;
    for (int index = 0; index < childCount; ++index)
    {
      RectTransform child = ((Transform) transform).GetChild(index) as RectTransform;
      if (((Component) child).gameObject.activeInHierarchy)
        rectTransformArray[num1++] = child;
    }
    float num2 = 0.0f;
    if (num1 > 0)
      num2 = (double) this.m_FixedStride <= 0.0 ? (!this.m_UseFullRange || num1 < 2 ? (this.AngleMax - this.AngleMin) / (float) num1 : (this.AngleMax - this.AngleMin) / (float) (num1 - 1)) : ((double) this.AngleMax >= (double) this.AngleMin ? this.m_FixedStride : -this.m_FixedStride);
    for (int index = 0; index < num1; ++index)
    {
      RectTransform rectTransform = rectTransformArray[index];
      float num3 = (float) (((double) this.AngleMin + (double) num2 * ((double) index + (double) this.m_Fraction)) * (Math.PI / 180.0));
      rectTransform.anchoredPosition = new Vector2()
      {
        x = Mathf.Cos(num3) * this.m_Radius,
        y = Mathf.Sin(num3) * this.m_Radius
      };
    }
  }
}
