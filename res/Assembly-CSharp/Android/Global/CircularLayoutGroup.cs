// Decompiled with JetBrains decompiler
// Type: CircularLayoutGroup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Layout/Circular Layout Group")]
public class CircularLayoutGroup : LayoutGroup
{
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
  protected float m_AngleMin;
  [SerializeField]
  protected float m_FixedStride;

  public float AngleMin
  {
    get
    {
      return this.m_AngleMin;
    }
    set
    {
      this.SetProperty<float>(ref this.m_AngleMin, value);
    }
  }

  public float AngleMax
  {
    get
    {
      return this.m_AngleMax;
    }
    set
    {
      this.SetProperty<float>(ref this.m_AngleMax, value);
    }
  }

  public float Radius
  {
    get
    {
      return this.m_Radius;
    }
    set
    {
      this.SetProperty<float>(ref this.m_Radius, value);
    }
  }

  public float Fraction
  {
    get
    {
      return this.m_Fraction;
    }
    set
    {
      this.SetProperty<float>(ref this.m_Fraction, value);
    }
  }

  public bool UseFullRange
  {
    get
    {
      return this.m_UseFullRange;
    }
    set
    {
      this.SetProperty<bool>(ref this.m_UseFullRange, value);
    }
  }

  public float FixedStride
  {
    get
    {
      return this.m_FixedStride;
    }
    set
    {
      this.SetProperty<float>(ref this.m_FixedStride, value);
    }
  }

  public override void CalculateLayoutInputHorizontal()
  {
    base.CalculateLayoutInputHorizontal();
  }

  public override void CalculateLayoutInputVertical()
  {
  }

  public override void SetLayoutHorizontal()
  {
  }

  public override void SetLayoutVertical()
  {
    RectTransform transform = this.transform as RectTransform;
    int childCount = transform.childCount;
    RectTransform[] rectTransformArray = new RectTransform[childCount];
    int num1 = 0;
    for (int index = 0; index < childCount; ++index)
    {
      RectTransform child = transform.GetChild(index) as RectTransform;
      if (child.gameObject.activeInHierarchy)
        rectTransformArray[num1++] = child;
    }
    float num2 = 0.0f;
    if (num1 > 0)
      num2 = (double) this.m_FixedStride <= 0.0 ? (!this.m_UseFullRange || num1 < 2 ? (this.AngleMax - this.AngleMin) / (float) num1 : (this.AngleMax - this.AngleMin) / (float) (num1 - 1)) : ((double) this.AngleMax >= (double) this.AngleMin ? this.m_FixedStride : -this.m_FixedStride);
    for (int index = 0; index < num1; ++index)
    {
      RectTransform rectTransform = rectTransformArray[index];
      float f = (float) (((double) this.AngleMin + (double) num2 * ((double) index + (double) this.m_Fraction)) * (Math.PI / 180.0));
      rectTransform.anchoredPosition = new Vector2()
      {
        x = Mathf.Cos(f) * this.m_Radius,
        y = Mathf.Sin(f) * this.m_Radius
      };
    }
  }
}
