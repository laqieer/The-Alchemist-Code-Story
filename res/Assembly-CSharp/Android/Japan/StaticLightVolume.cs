// Decompiled with JetBrains decompiler
// Type: StaticLightVolume
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class StaticLightVolume : MonoBehaviour
{
  public static List<StaticLightVolume> Volumes = new List<StaticLightVolume>();
  [Range(0.0f, 1f)]
  public float AmbientLitToDirectLit = 0.2f;
  [Range(0.0f, 1f)]
  public float AmbientLitToIndirectLit = 0.1f;
  [Range(0.0f, 1f)]
  public float PointLitToDirectLit = 0.1f;
  [Range(0.0f, 1f)]
  public float PointLitToIndirectLit = 1f;
  [Range(0.1f, 4f)]
  public float DirectLightingScale = 1f;
  [Range(0.1f, 4f)]
  public float IndirectLightingScale = 1f;
  [SerializeField]
  [HideInInspector]
  private StaticLightVolume.LightProbe[] mVoxel = new StaticLightVolume.LightProbe[0];
  [Range(1f, 16f)]
  public int XSize = 5;
  [Range(1f, 16f)]
  public int YSize = 5;
  [Range(1f, 16f)]
  public int ZSize = 5;
  [NonSerialized]
  public Bounds Bounds;
  private const float GizmoColorR = 0.2f;
  private const float GizmoColorG = 0.6f;
  private const float GizmoColorB = 0.6f;

  public static StaticLightVolume FindVolume(Vector3 pos)
  {
    for (int index = StaticLightVolume.Volumes.Count - 1; index >= 0; --index)
    {
      if (StaticLightVolume.Volumes[index].isActiveAndEnabled && StaticLightVolume.Volumes[index].Bounds.Contains(pos))
        return StaticLightVolume.Volumes[index];
    }
    return (StaticLightVolume) null;
  }

  private Bounds CalcBounds()
  {
    Transform transform = this.transform;
    return new Bounds(transform.position, transform.localScale);
  }

  private void OnEnable()
  {
    StaticLightVolume.Volumes.Add(this);
    this.Bounds = this.CalcBounds();
  }

  private void OnDisable()
  {
    StaticLightVolume.Volumes.Remove(this);
  }

  public void CalcLightColor(Vector3 position, out Color directLit, out Color indirectLit)
  {
    position = Vector3.Max(position, this.Bounds.min);
    position = Vector3.Min(position, this.Bounds.max);
    float num1 = this.Bounds.size.x / (float) this.XSize;
    float num2 = this.Bounds.size.y / (float) this.YSize;
    float num3 = this.Bounds.size.z / (float) this.ZSize;
    Vector3 vector3 = position - this.Bounds.min - Vector3.one * 0.5f;
    int num4 = Mathf.Clamp(Mathf.FloorToInt(vector3.x / num1), 0, this.XSize - 1);
    int num5 = Mathf.Clamp(Mathf.FloorToInt(vector3.y / num2), 0, this.YSize - 1);
    int num6 = Mathf.Clamp(Mathf.FloorToInt(vector3.z / num3), 0, this.ZSize - 1);
    int num7 = Mathf.Min(num4 + 1, this.XSize - 1);
    int num8 = Mathf.Min(num5 + 1, this.YSize - 1);
    int num9 = Mathf.Min(num6 + 1, this.ZSize - 1);
    float t1 = vector3.x % num1 / num1;
    float t2 = vector3.y % num2 / num2;
    float t3 = vector3.z % num3 / num3;
    int num10 = this.XSize * this.YSize;
    int index1 = num4 + this.XSize * num5 + num10 * num6;
    int index2 = num7 + this.XSize * num8 + num10 * num6;
    int index3 = num4 + this.XSize * num5 + num10 * num6;
    int index4 = num7 + this.XSize * num8 + num10 * num6;
    int index5 = num4 + this.XSize * num5 + num10 * num9;
    int index6 = num7 + this.XSize * num8 + num10 * num9;
    int index7 = num4 + this.XSize * num5 + num10 * num9;
    int index8 = num7 + this.XSize * num8 + num10 * num9;
    directLit = Color.Lerp(Color.Lerp(Color.Lerp((Color) this.mVoxel[index1].DirectLightColor, (Color) this.mVoxel[index2].DirectLightColor, t1), Color.Lerp((Color) this.mVoxel[index3].DirectLightColor, (Color) this.mVoxel[index4].DirectLightColor, t1), t2), Color.Lerp(Color.Lerp((Color) this.mVoxel[index5].DirectLightColor, (Color) this.mVoxel[index6].DirectLightColor, t1), Color.Lerp((Color) this.mVoxel[index7].DirectLightColor, (Color) this.mVoxel[index8].DirectLightColor, t1), t2), t3);
    indirectLit = Color.Lerp(Color.Lerp(Color.Lerp((Color) this.mVoxel[index1].IndirectLightColor, (Color) this.mVoxel[index2].IndirectLightColor, t1), Color.Lerp((Color) this.mVoxel[index3].IndirectLightColor, (Color) this.mVoxel[index4].IndirectLightColor, t1), t2), Color.Lerp(Color.Lerp((Color) this.mVoxel[index5].IndirectLightColor, (Color) this.mVoxel[index6].IndirectLightColor, t1), Color.Lerp((Color) this.mVoxel[index7].IndirectLightColor, (Color) this.mVoxel[index8].IndirectLightColor, t1), t2), t3);
  }

  private void OnValidate()
  {
    int length = this.XSize * this.YSize * this.ZSize;
    if (this.mVoxel.Length == length)
      return;
    this.mVoxel = new StaticLightVolume.LightProbe[length];
    for (int index1 = 0; index1 < this.XSize; ++index1)
    {
      for (int index2 = 0; index2 < this.YSize; ++index2)
      {
        for (int index3 = 0; index3 < this.ZSize; ++index3)
        {
          int index4 = index1 + index2 * this.XSize + index3 * this.XSize * this.YSize;
          this.mVoxel[index4] = new StaticLightVolume.LightProbe();
          this.mVoxel[index4].DirectLightColor = (Color32) Color.black;
          this.mVoxel[index4].IndirectLightColor = (Color32) Color.black;
        }
      }
    }
  }

  private Vector3 CalcCenter(int x, int y, int z)
  {
    return new Vector3(Mathf.Lerp(this.Bounds.min.x, this.Bounds.max.x, ((float) x + 0.5f) / (float) this.XSize), Mathf.Lerp(this.Bounds.min.y, this.Bounds.max.y, ((float) y + 0.5f) / (float) this.YSize), Mathf.Lerp(this.Bounds.min.z, this.Bounds.max.z, ((float) z + 0.5f) / (float) this.ZSize));
  }

  public void Bake()
  {
    Light[] objectsOfType = Object.FindObjectsOfType<Light>();
    Light[] all1 = Array.FindAll<Light>(objectsOfType, (Predicate<Light>) (lit => lit.type == LightType.Directional));
    Light[] all2 = Array.FindAll<Light>(objectsOfType, (Predicate<Light>) (lit => lit.type == LightType.Point));
    Color black = Color.black;
    foreach (Light light in all1)
      black += light.color * light.intensity;
    Color color1 = black * this.DirectLightingScale;
    color1.a = 1f;
    foreach (ColorBlendVolume volume in ColorBlendVolume.Volumes)
      volume.UpdateBounds();
    for (int x = 0; x < this.XSize; ++x)
    {
      for (int y = 0; y < this.YSize; ++y)
      {
        for (int z = 0; z < this.ZSize; ++z)
        {
          Vector3 vector3 = this.CalcCenter(x, y, z);
          Color color2 = new Color(0.0f, 0.0f, 0.0f);
          Color color3 = new Color(0.0f, 0.0f, 0.0f);
          int index1 = x + y * this.XSize + z * this.XSize * this.YSize;
          AmbientLightSettings volume1 = AmbientLightSettings.FindVolume(vector3);
          color2 = color1 + volume1.AmbientLightColor * this.AmbientLitToDirectLit;
          for (int index2 = 0; index2 < all2.Length; ++index2)
          {
            float num = Mathf.Clamp01((float) (1.0 - (double) (vector3 - all2[index2].transform.position).magnitude / (double) all2[index2].range)) * all2[index2].intensity;
            color2.r += all2[index2].color.r * num * this.PointLitToDirectLit;
            color2.g += all2[index2].color.g * num * this.PointLitToDirectLit;
            color2.b += all2[index2].color.b * num * this.PointLitToDirectLit;
            color3.r += all2[index2].color.r * num * this.PointLitToIndirectLit;
            color3.g += all2[index2].color.g * num * this.PointLitToIndirectLit;
            color3.b += all2[index2].color.b * num * this.PointLitToIndirectLit;
          }
          color3 *= this.IndirectLightingScale;
          color3 += volume1.AmbientLightColor * this.AmbientLitToIndirectLit;
          ColorBlendVolume volume2 = ColorBlendVolume.FindVolume(vector3);
          if ((Object) volume2 != (Object) null)
          {
            color2 *= (Color) volume2.Color;
            color3 *= (Color) volume2.Color;
          }
          color2.a = 1f;
          color3.a = 1f;
          this.mVoxel[index1].DirectLightColor = (Color32) color2;
          this.mVoxel[index1].IndirectLightColor = (Color32) color3;
        }
      }
    }
  }

  [Serializable]
  public struct LightProbe
  {
    public Color32 DirectLightColor;
    public Color32 IndirectLightColor;
  }
}
