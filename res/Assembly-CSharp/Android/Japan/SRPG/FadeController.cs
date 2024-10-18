// Decompiled with JetBrains decompiler
// Type: SRPG.FadeController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [AddComponentMenu("")]
  public class FadeController : MonoBehaviour
  {
    private Color[] mCurrentColor = new Color[3];
    private Color[] mStartColor = new Color[3];
    private Color[] mEndColor = new Color[3];
    private float[] mCurrentTime = new float[3];
    private float[] mDuration = new float[3];
    private Canvas[] mCanvas = new Canvas[3];
    private RawImage[] mImage = new RawImage[3];
    private bool[] mInitialized = new bool[3];
    private const int FADE_TYPE_MAX = 3;
    private Color mSceneFadeStart;
    private Color mSceneFadeEnd;
    private float mSceneFadeDuration;
    private float mSceneFadeTime;
    private TacticsUnitController[] mSceneFadeExcluders;
    private TacticsUnitController[] mSceneFadeIncluders;
    private static FadeController mInstance;

    public static bool InstanceExists
    {
      get
      {
        return (UnityEngine.Object) FadeController.mInstance != (UnityEngine.Object) null;
      }
    }

    public static FadeController Instance
    {
      get
      {
        if ((UnityEngine.Object) FadeController.mInstance == (UnityEngine.Object) null)
          FadeController.mInstance = new GameObject(nameof (FadeController), new System.Type[1]
          {
            typeof (FadeController)
          }).GetComponent<FadeController>();
        return FadeController.mInstance;
      }
    }

    private void Awake()
    {
      UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) this.gameObject);
      Array values = Enum.GetValues(typeof (FadeController.LayerType));
      string[] strArray = new string[3]{ string.Empty, "Custom/Particle/UnlitAdd NoZTest (TwoSided)", "Custom/Particle/UnlitAlpha NoZTest (TwoSided)" };
      for (int index = 0; index < 3; ++index)
      {
        this.mCurrentColor[index] = new Color(0.0f, 0.0f, 0.0f);
        this.mStartColor[index] = new Color(0.0f, 0.0f, 0.0f);
        this.mEndColor[index] = new Color(0.0f, 0.0f, 0.0f);
        GameObject gameObject = new GameObject(values.GetValue(index).ToString(), new System.Type[2]{ typeof (Canvas), typeof (RawImage) });
        this.mCanvas[index] = gameObject.GetComponent<Canvas>();
        this.mCanvas[index].sortingOrder = 9999 - index;
        this.mCanvas[index].renderMode = UnityEngine.RenderMode.ScreenSpaceOverlay;
        this.mCanvas[index].enabled = false;
        this.mImage[index] = gameObject.GetComponent<RawImage>();
        this.mImage[index].color = this.mEndColor[index];
        if (!string.IsNullOrEmpty(strArray[index]))
        {
          Shader shader = Shader.Find(strArray[index]);
          if ((UnityEngine.Object) shader != (UnityEngine.Object) null)
          {
            this.mImage[index].material = new Material(shader);
            this.mImage[index].material.SetColor("_Color", Color.white);
          }
        }
        gameObject.transform.SetParent(this.gameObject.transform);
      }
    }

    public bool IsScreenFaded(int layer = 0)
    {
      return (double) this.mImage[layer].color.a >= 1.0;
    }

    public bool IsFading(int layer = 0)
    {
      return (double) this.mCurrentTime[layer] < (double) this.mDuration[layer];
    }

    public void FadeTo(Color dest, float time, int layer = 0)
    {
      if (!this.mInitialized[layer])
      {
        this.mCurrentColor[layer] = dest;
        this.mCurrentColor[layer].a = 1f - this.mCurrentColor[layer].a;
        this.mInitialized[layer] = true;
        this.mImage[layer].color = this.mCurrentColor[layer];
      }
      if ((double) time > 0.0)
      {
        this.mStartColor[layer] = this.mCurrentColor[layer];
        this.mEndColor[layer] = dest;
        this.mCurrentTime[layer] = 0.0f;
        this.mDuration[layer] = time;
        this.mCanvas[layer].enabled = true;
      }
      else
      {
        this.mCurrentColor[layer] = dest;
        this.mCurrentTime[layer] = 0.0f;
        this.mDuration[layer] = 0.0f;
        this.mImage[layer].color = this.mCurrentColor[layer];
        this.mCanvas[layer].enabled = (double) this.mCurrentColor[layer].a > 0.0;
      }
    }

    public void ResetSceneFade(float time)
    {
      this.mSceneFadeEnd = Color.white;
      this.mSceneFadeStart = CameraHook.ColorMod;
      this.mSceneFadeDuration = time;
      this.mSceneFadeTime = 0.0f;
      if ((double) this.mSceneFadeDuration > 0.0)
        return;
      this.ApplySceneFade(this.mSceneFadeEnd);
    }

    public void BeginSceneFade(Color dest, float time, TacticsUnitController[] excludes, TacticsUnitController[] includes)
    {
      this.mSceneFadeStart = CameraHook.ColorMod;
      this.mSceneFadeEnd = dest;
      this.mSceneFadeDuration = time;
      this.mSceneFadeTime = 0.0f;
      this.mSceneFadeExcluders = excludes;
      this.mSceneFadeIncluders = includes;
      if ((double) this.mSceneFadeDuration > 0.0)
        return;
      this.ApplySceneFade(dest);
    }

    private void ApplySceneFade(Color fadeColor)
    {
      CameraHook.ColorMod = fadeColor;
      if (this.mSceneFadeIncluders != null && this.mSceneFadeExcluders != null)
      {
        for (int index = TacticsUnitController.Instances.Count - 1; index >= 0; --index)
        {
          TacticsUnitController instance = TacticsUnitController.Instances[index];
          if (Array.IndexOf<TacticsUnitController>(this.mSceneFadeIncluders, instance) >= 0 && Array.IndexOf<TacticsUnitController>(this.mSceneFadeExcluders, instance) < 0)
            instance.ColorMod = fadeColor;
          else
            instance.ResetColorMod();
        }
      }
      else if (this.mSceneFadeIncluders != null)
      {
        for (int index = TacticsUnitController.Instances.Count - 1; index >= 0; --index)
        {
          TacticsUnitController instance = TacticsUnitController.Instances[index];
          if (Array.IndexOf<TacticsUnitController>(this.mSceneFadeIncluders, instance) >= 0)
            instance.ColorMod = fadeColor;
          else
            instance.ResetColorMod();
        }
      }
      else if (this.mSceneFadeExcluders != null)
      {
        for (int index = TacticsUnitController.Instances.Count - 1; index >= 0; --index)
        {
          TacticsUnitController instance = TacticsUnitController.Instances[index];
          if (Array.IndexOf<TacticsUnitController>(this.mSceneFadeExcluders, instance) < 0)
            instance.ColorMod = fadeColor;
          else
            instance.ResetColorMod();
        }
      }
      else
      {
        for (int index = TacticsUnitController.Instances.Count - 1; index >= 0; --index)
          TacticsUnitController.Instances[index].ColorMod = fadeColor;
      }
    }

    public bool IsSceneFading
    {
      get
      {
        return (double) this.mSceneFadeTime < (double) this.mSceneFadeDuration;
      }
    }

    private void UpdateSceneFade()
    {
      if ((double) this.mSceneFadeTime >= (double) this.mSceneFadeDuration)
        return;
      this.mSceneFadeTime += Time.deltaTime;
      this.ApplySceneFade(Color.Lerp(this.mSceneFadeStart, this.mSceneFadeEnd, Mathf.Clamp01(this.mSceneFadeTime / this.mSceneFadeDuration)));
    }

    private void Update()
    {
      this.UpdateSceneFade();
      for (int index = 0; index < 3; ++index)
      {
        if ((double) this.mCurrentTime[index] >= (double) this.mDuration[index])
        {
          if ((double) this.mCurrentColor[index].a <= 0.0 && this.mCanvas[index].enabled)
            this.mCanvas[index].enabled = false;
        }
        else
        {
          this.mCurrentTime[index] += Time.unscaledDeltaTime;
          float t = Mathf.Clamp01(this.mCurrentTime[index] / this.mDuration[index]);
          this.mCurrentColor[index] = Color.Lerp(this.mStartColor[index], this.mEndColor[index], t);
          this.mImage[index].color = this.mCurrentColor[index];
        }
      }
    }

    public enum LayerType
    {
      Normal,
      Add,
      AlphaBlend,
    }
  }
}
