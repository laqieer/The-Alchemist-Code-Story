// Decompiled with JetBrains decompiler
// Type: ColorBlendVolume
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ColorBlendVolume : MonoBehaviour
{
  public static List<ColorBlendVolume> Volumes = new List<ColorBlendVolume>();
  public float Radius = 1.5f;
  public Color32 Color;
  [NonSerialized]
  public Bounds Bounds;
  private Bounds mBoundsInner;

  public void UpdateBounds()
  {
    Transform transform = this.transform;
    Vector3 localScale = transform.localScale;
    localScale.x += this.Radius * 2f;
    localScale.y += this.Radius * 2f;
    localScale.z += this.Radius * 2f;
    this.Bounds = new Bounds(transform.position, localScale);
    this.mBoundsInner = new Bounds(transform.position, transform.localScale);
  }

  private void OnEnable()
  {
    ColorBlendVolume.Volumes.Add(this);
    this.UpdateBounds();
  }

  private void OnDisable()
  {
    ColorBlendVolume.Volumes.Remove(this);
  }

  public static ColorBlendVolume FindVolume(Vector3 point)
  {
    for (int index = 0; index < ColorBlendVolume.Volumes.Count; ++index)
    {
      if (ColorBlendVolume.Volumes[index].Bounds.Contains(point))
        return ColorBlendVolume.Volumes[index];
    }
    return (ColorBlendVolume) null;
  }

  public Color32 CalcBlendColor32(Vector3 point)
  {
    Vector3 lhs = point - this.Bounds.center;
    float num = 1f - Mathf.Max(Mathf.Clamp01(Mathf.Max(Mathf.Abs(Vector3.Dot(lhs, Vector3.right)) - this.mBoundsInner.extents.x, 0.0f) / this.Radius), Mathf.Max(Mathf.Clamp01(Mathf.Max(Mathf.Abs(Vector3.Dot(lhs, Vector3.up)) - this.mBoundsInner.extents.y, 0.0f) / this.Radius), Mathf.Clamp01(Mathf.Max(Mathf.Abs(Vector3.Dot(lhs, Vector3.forward)) - this.mBoundsInner.extents.z, 0.0f) / this.Radius)));
    Color32 color = this.Color;
    color.a = (byte) Mathf.FloorToInt((float) byte.MaxValue * num);
    return color;
  }
}
