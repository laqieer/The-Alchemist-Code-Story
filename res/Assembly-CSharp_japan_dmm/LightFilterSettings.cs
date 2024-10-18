// Decompiled with JetBrains decompiler
// Type: LightFilterSettings
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
[ExecuteInEditMode]
[AddComponentMenu("")]
public class LightFilterSettings : MonoBehaviour
{
  public float AOSampleRadius = 2f;
  public float AOExponent = 3f;
  public float AOStrength = 1f;
  public Gradient AOGradient;
  public bool UseAmbientOcclusion = true;

  public static LightFilterSettings Current
  {
    get
    {
      foreach (LightFilterSettings current in Object.FindObjectsOfType<LightFilterSettings>())
      {
        if (((Component) current).gameObject.activeInHierarchy)
          return current;
      }
      return new GameObject(nameof (LightFilterSettings)).AddComponent<LightFilterSettings>();
    }
  }

  private void Awake()
  {
    ((Object) ((Component) this).transform).hideFlags = (HideFlags) 2;
    ((Behaviour) this).enabled = false;
    ((Component) this).tag = "EditorOnly";
    ((Object) ((Component) this).gameObject).hideFlags = (HideFlags) 1;
    if (this.AOGradient != null && this.AOGradient.colorKeys.Length > 1)
      return;
    GradientColorKey[] gradientColorKeyArray = new GradientColorKey[2]
    {
      new GradientColorKey(Color.black, 0.0f),
      new GradientColorKey(Color.white, 1f)
    };
    GradientAlphaKey[] gradientAlphaKeyArray = new GradientAlphaKey[2]
    {
      new GradientAlphaKey(1f, 0.0f),
      new GradientAlphaKey(1f, 1f)
    };
    this.AOGradient = new Gradient();
    this.AOGradient.SetKeys(gradientColorKeyArray, gradientAlphaKeyArray);
  }
}
