// Decompiled with JetBrains decompiler
// Type: AmbientLightSettings
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AmbientLightSettings : MonoBehaviour
{
  public static List<AmbientLightSettings> Volumes = new List<AmbientLightSettings>();
  [Header("Ambient Light")]
  public Color AmbientLightColor;
  [Header("Fog")]
  public Color FogColor;
  public float FogStartDistance;
  public float FogEndDistance;

  public Bounds Bounds
  {
    get
    {
      Transform transform = this.transform;
      return new Bounds(transform.position, transform.localScale);
    }
  }

  public static AmbientLightSettings FindVolume(Vector3 pos)
  {
    for (int index = AmbientLightSettings.Volumes.Count - 1; index >= 0; --index)
    {
      if (AmbientLightSettings.Volumes[index].Bounds.Contains(pos))
        return AmbientLightSettings.Volumes[index];
    }
    return (AmbientLightSettings) null;
  }

  private void OnEnable()
  {
    AmbientLightSettings.Volumes.Add(this);
  }

  private void OnDisable()
  {
    AmbientLightSettings.Volumes.Remove(this);
  }

  public static implicit operator AmbientLightSettings.State(AmbientLightSettings src)
  {
    AmbientLightSettings.State state = new AmbientLightSettings.State();
    if ((Object) src != (Object) null)
    {
      state.AmbientLightColor = src.AmbientLightColor;
      state.FogColor = src.FogColor;
      state.FogStartDistance = src.FogStartDistance;
      state.FogEndDistance = src.FogEndDistance;
    }
    else
    {
      state.AmbientLightColor = Color.black;
      state.FogStartDistance = 0.0f;
      state.FogEndDistance = 0.0f;
    }
    return state;
  }

  public struct State
  {
    public Color AmbientLightColor;
    public Color FogColor;
    public float FogStartDistance;
    public float FogEndDistance;

    public static AmbientLightSettings.State Lerp(AmbientLightSettings.State a, AmbientLightSettings.State b, float t)
    {
      AmbientLightSettings.State state = new AmbientLightSettings.State();
      t = Mathf.Clamp01(t);
      state.AmbientLightColor = Color.Lerp(a.AmbientLightColor, b.AmbientLightColor, t);
      state.FogColor = Color.Lerp(a.FogColor, b.FogColor, t);
      state.FogStartDistance = Mathf.Lerp(a.FogStartDistance, b.FogStartDistance, t);
      state.FogEndDistance = Mathf.Lerp(a.FogEndDistance, b.FogEndDistance, t);
      return state;
    }
  }
}
