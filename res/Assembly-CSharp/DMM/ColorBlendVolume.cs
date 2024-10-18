// Decompiled with JetBrains decompiler
// Type: ColorBlendVolume
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
[ExecuteInEditMode]
public class ColorBlendVolume : MonoBehaviour
{
  public static List<ColorBlendVolume> Volumes = new List<ColorBlendVolume>();
  public Color32 Color;
  public float Radius = 1.5f;
  [NonSerialized]
  public Bounds Bounds;
  private Bounds mBoundsInner;

  public void UpdateBounds()
  {
    Transform transform = ((Component) this).transform;
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

  private void OnDisable() => ColorBlendVolume.Volumes.Remove(this);

  public static ColorBlendVolume FindVolume(Vector3 point)
  {
    for (int index = 0; index < ColorBlendVolume.Volumes.Count; ++index)
    {
      if (((Bounds) ref ColorBlendVolume.Volumes[index].Bounds).Contains(point))
        return ColorBlendVolume.Volumes[index];
    }
    return (ColorBlendVolume) null;
  }

  public Color32 CalcBlendColor32(Vector3 point)
  {
    Vector3 vector3 = Vector3.op_Subtraction(point, ((Bounds) ref this.Bounds).center);
    float num1 = Mathf.Abs(Vector3.Dot(vector3, Vector3.right));
    float num2 = Mathf.Abs(Vector3.Dot(vector3, Vector3.up));
    float num3 = Mathf.Abs(Vector3.Dot(vector3, Vector3.forward));
    float num4 = num1 - ((Bounds) ref this.mBoundsInner).extents.x;
    float num5 = num2 - ((Bounds) ref this.mBoundsInner).extents.y;
    float num6 = num3 - ((Bounds) ref this.mBoundsInner).extents.z;
    float num7 = 1f - Mathf.Max(Mathf.Clamp01(Mathf.Max(num4, 0.0f) / this.Radius), Mathf.Max(Mathf.Clamp01(Mathf.Max(num5, 0.0f) / this.Radius), Mathf.Clamp01(Mathf.Max(num6, 0.0f) / this.Radius)));
    Color32 color = this.Color;
    color.a = (byte) Mathf.FloorToInt((float) byte.MaxValue * num7);
    return color;
  }
}
