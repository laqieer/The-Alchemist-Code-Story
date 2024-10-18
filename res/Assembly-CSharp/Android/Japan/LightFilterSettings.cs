// Decompiled with JetBrains decompiler
// Type: LightFilterSettings
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("")]
public class LightFilterSettings : MonoBehaviour
{
  public float AOSampleRadius = 2f;
  public float AOExponent = 3f;
  public float AOStrength = 1f;
  public bool UseAmbientOcclusion = true;
  public Gradient AOGradient;

  public static LightFilterSettings Current
  {
    get
    {
      foreach (LightFilterSettings lightFilterSettings in Object.FindObjectsOfType<LightFilterSettings>())
      {
        if (lightFilterSettings.gameObject.activeInHierarchy)
          return lightFilterSettings;
      }
      return new GameObject(nameof (LightFilterSettings)).AddComponent<LightFilterSettings>();
    }
  }

  private void Awake()
  {
    this.transform.hideFlags = HideFlags.HideInInspector;
    this.enabled = false;
    this.tag = "EditorOnly";
    this.gameObject.hideFlags = HideFlags.HideInHierarchy;
    if (this.AOGradient != null && this.AOGradient.colorKeys.Length > 1)
      return;
    GradientColorKey[] colorKeys = new GradientColorKey[2]
    {
      new GradientColorKey(Color.black, 0.0f),
      new GradientColorKey(Color.white, 1f)
    };
    GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2]
    {
      new GradientAlphaKey(1f, 0.0f),
      new GradientAlphaKey(1f, 1f)
    };
    this.AOGradient = new Gradient();
    this.AOGradient.SetKeys(colorKeys, alphaKeys);
  }
}
