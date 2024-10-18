// Decompiled with JetBrains decompiler
// Type: RenderPipeline
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using SRPG;
using System;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof (UnityEngine.Camera))]
public class RenderPipeline : MonoBehaviour
{
  private bool mApplyBloom = true;
  private bool mApplyVignette = true;
  [NonSerialized]
  public RenderPipeline.RenderModes RenderMode;
  private UnityEngine.Camera mCamera;
  private RenderTexture mRTBase;
  private RenderTexture mRTComposite;
  private RenderTexture mRTBloom0;
  private RenderTexture mRTBloom1;
  private GameObject mPassBG;
  private GameObject mPassCH;
  private GameObject mPassEffect;
  private GameObject mPassUI;
  private Material mUberPostEffect;
  private Material mBlitCopy;
  private Material mBlitAdd;
  private Material mBlitDodge;
  private Material mBlurEffect;
  private Material mBloomPrePass;
  private Material mBlendX;
  private Material mDilateErode;
  private Material mBlitFunc;
  private Material mBGImage;
  private Material mGradientFill;
  public RenderPipeline.SwapEffects SwapEffect;
  public float SwapEffectOpacity;
  public Texture BackgroundImage;
  public Texture ScreenFilter;
  public bool FlipHorizontally;
  private Quaternion mOldRotation;

  public bool EnableBloom
  {
    set
    {
      this.mApplyBloom = value;
    }
    get
    {
      return this.mApplyBloom;
    }
  }

  public bool EnableVignette
  {
    set
    {
      this.mApplyVignette = value;
    }
    get
    {
      return this.mApplyVignette;
    }
  }

  public float mBloomStrength
  {
    get
    {
      return GameSettings.Instance.PostEffect_BloomMaxStrength;
    }
  }

  public float BlurStrength
  {
    get
    {
      return GameSettings.Instance.PostEffect_BloomBlurStrength;
    }
  }

  public static void Setup(UnityEngine.Camera camera)
  {
    camera.gameObject.RequireComponent<RenderPipeline>();
  }

  private void Awake()
  {
    this.mCamera = this.GetComponent<UnityEngine.Camera>();
  }

  private void Start()
  {
    int[] values = (int[]) Enum.GetValues(typeof (RenderTextureFormat));
    if (GameUtility.IsDebugBuild)
    {
      string str = string.Empty;
      for (int index = 0; index < values.Length; ++index)
      {
        if (SystemInfo.SupportsRenderTextureFormat((RenderTextureFormat) index))
          str = str + ((Enum) (RenderTextureFormat) index).ToString() + " ";
      }
      Debug.Log((object) ("Supported RenderTexture Formats: " + str));
    }
    CameraHook.Inject();
  }

  private RenderTexture ReserveRT(ref RenderTexture rt, float w, float h, RenderTextureFormat format, int depth)
  {
    RenderTexture targetTexture = this.mCamera.targetTexture;
    int width1;
    int height1;
    if ((UnityEngine.Object) targetTexture != (UnityEngine.Object) null)
    {
      width1 = targetTexture.width;
      height1 = targetTexture.height;
    }
    else
    {
      width1 = Screen.width;
      height1 = Screen.height;
    }
    int width2 = Mathf.FloorToInt((float) width1 * w);
    int height2 = Mathf.FloorToInt((float) height1 * h);
    if ((UnityEngine.Object) rt == (UnityEngine.Object) null || rt.width != width2 || rt.height != height2)
    {
      if (!SystemInfo.SupportsRenderTextureFormat(format) && format == RenderTextureFormat.R8)
        format = RenderTextureFormat.Default;
      RenderTexture.ReleaseTemporary(rt);
      rt = RenderTexture.GetTemporary(width2, height2, depth, format, RenderTextureReadWrite.Linear);
      rt.set_generateMips(false);
    }
    return rt;
  }

  private void ReleaseRT(ref RenderTexture rt)
  {
    RenderTexture.ReleaseTemporary(rt);
    rt = (RenderTexture) null;
  }

  private void CreatePass(ref GameObject pass, string name, int priority, CameraCallback.CameraEvent onPreCull, CameraCallback.CameraEvent onPreRender, CameraCallback.CameraEvent onPostRender, CameraCallback.RenderImageEvent onRenderImage, int cullingMask, bool clearDepth)
  {
    if ((UnityEngine.Object) pass == (UnityEngine.Object) null)
    {
      pass = new GameObject(name, new System.Type[2]
      {
        typeof (UnityEngine.Camera),
        typeof (CameraCallback)
      });
      pass.hideFlags = HideFlags.DontSave;
      pass.transform.parent = this.transform;
      CameraCallback component = pass.GetComponent<CameraCallback>();
      component.OnCameraPreCull = onPreCull;
      component.OnCameraPreRender = onPreRender;
      component.OnCameraPostRender = onPostRender;
      component.OnCameraRenderImage = onRenderImage;
    }
    UnityEngine.Camera component1 = pass.GetComponent<UnityEngine.Camera>();
    component1.CopyFrom(this.mCamera);
    component1.depth = this.mCamera.depth + (float) priority;
    component1.cullingMask = cullingMask;
    component1.targetTexture = (RenderTexture) null;
    component1.clearFlags = !clearDepth ? CameraClearFlags.Nothing : CameraClearFlags.Depth;
  }

  private void DestroyPass(ref GameObject pass)
  {
    if ((UnityEngine.Object) pass != (UnityEngine.Object) null)
    {
      if (Application.isPlaying)
        UnityEngine.Object.Destroy((UnityEngine.Object) pass);
      else
        UnityEngine.Object.DestroyImmediate((UnityEngine.Object) pass);
    }
    pass = (GameObject) null;
  }

  private void ClearBackground(bool clearColor, bool clearDepth, Color bgColor, float depth)
  {
    GL.Clear(clearDepth, clearColor, bgColor, depth);
  }

  private Material LoadShader(string path)
  {
    Shader shader = Shader.Find(path);
    if ((UnityEngine.Object) shader == (UnityEngine.Object) null)
      throw new Exception("Can't find shader " + path);
    return new Material(shader);
  }

  private void OnEnable()
  {
    try
    {
      this.mUberPostEffect = this.LoadShader("Custom/PostEffect/UberPostEffect");
      this.mBlitCopy = this.LoadShader("Custom/PostEffect/BlitCopy");
      this.mBlitAdd = this.LoadShader("Custom/PostEffect/BlitAdd");
      this.mBlitDodge = this.LoadShader("Custom/PostEffect/BlitDodge");
      this.mBlurEffect = this.LoadShader("Custom/PostEffect/BlurPass");
      this.mBlendX = this.LoadShader("Custom/PostEffect/BlendX");
      this.mDilateErode = this.LoadShader("Custom/PostEffect/DilateErode");
      this.mBlitFunc = this.LoadShader("Custom/PostEffect/BlitFunc");
      this.mGradientFill = this.LoadShader("Custom/PostEffect/GradientFill");
      this.mBGImage = this.LoadShader("Custom/PostEffect/BGImage");
      this.mBloomPrePass = this.LoadShader("Custom/PostEffect/BloomPrePass");
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex.Message);
      this.enabled = false;
    }
  }

  private void OnDisable()
  {
    UnityEngine.Object.DestroyImmediate((UnityEngine.Object) this.mUberPostEffect);
    this.mUberPostEffect = (Material) null;
    UnityEngine.Object.DestroyImmediate((UnityEngine.Object) this.mBlitCopy);
    this.mBlitCopy = (Material) null;
    UnityEngine.Object.DestroyImmediate((UnityEngine.Object) this.mBlitAdd);
    this.mBlitAdd = (Material) null;
    UnityEngine.Object.DestroyImmediate((UnityEngine.Object) this.mBlitDodge);
    this.mBlitDodge = (Material) null;
    UnityEngine.Object.DestroyImmediate((UnityEngine.Object) this.mBlurEffect);
    this.mBlurEffect = (Material) null;
    UnityEngine.Object.DestroyImmediate((UnityEngine.Object) this.mBlendX);
    this.mBlendX = (Material) null;
    UnityEngine.Object.DestroyImmediate((UnityEngine.Object) this.mDilateErode);
    this.mDilateErode = (Material) null;
    UnityEngine.Object.DestroyImmediate((UnityEngine.Object) this.mBlitFunc);
    this.mBlitFunc = (Material) null;
    UnityEngine.Object.DestroyImmediate((UnityEngine.Object) this.mGradientFill);
    this.mGradientFill = (Material) null;
    UnityEngine.Object.DestroyImmediate((UnityEngine.Object) this.mBGImage);
    this.mBGImage = (Material) null;
    UnityEngine.Object.DestroyImmediate((UnityEngine.Object) this.mBloomPrePass);
    this.mBloomPrePass = (Material) null;
  }

  private void OnPreCull()
  {
    this.mOldRotation = this.transform.rotation;
    if (this.RenderMode != RenderPipeline.RenderModes.None)
      this.mCamera.cullingMask = -1 & ~(GameUtility.LayerMaskEffect | GameUtility.LayerMaskHidden | GameUtility.LayerMaskUI);
    else
      this.mCamera.cullingMask |= GameUtility.LayerMaskEffect | GameUtility.LayerMaskHidden | GameUtility.LayerMaskUI;
    this.mCamera.clearFlags = CameraClearFlags.Depth;
    this.mCamera.backgroundColor = new Color(0.0f, 0.0f, 0.0f);
  }

  private RenderTexture GetTemporaryRT(float scaleFactor, RenderTextureFormat format, int depthBpp)
  {
    return RenderTexture.GetTemporary(Mathf.FloorToInt((float) Screen.width * scaleFactor), Mathf.FloorToInt((float) Screen.height * scaleFactor), depthBpp, format);
  }

  private void OnPreRender()
  {
    if (this.RenderMode != RenderPipeline.RenderModes.None)
    {
      float scaleFactor = 0.75f;
      float num = 0.25f;
      this.mRTBase = this.GetTemporaryRT(scaleFactor, RenderTextureFormat.Default, 16);
      this.mRTBloom0 = this.GetTemporaryRT(scaleFactor * num, RenderTextureFormat.Default, 0);
      this.mRTBloom1 = this.GetTemporaryRT(scaleFactor * num, RenderTextureFormat.Default, 0);
      this.mRTComposite = this.GetTemporaryRT(scaleFactor, RenderTextureFormat.Default, 0);
      this.mCamera.SetTargetBuffers(this.mRTBase.colorBuffer, this.mRTBase.depthBuffer);
      Graphics.SetRenderTarget(this.mRTBase);
      GL.Clear(false, true, new Color(0.0f, 0.0f, 0.0f, 0.0f));
      Shader.EnableKeyword("BGMASK_ON");
      Graphics.SetRenderTarget(this.mRTBase);
      this.mBlitCopy.SetColor("_color", CameraHook.ColorMod);
      this.mBlitCopy.EnableKeyword("USE_COLOR");
      Graphics.Blit(this.BackgroundImage, this.mBlitCopy);
      this.mBlitCopy.DisableKeyword("USE_COLOR");
      this.mCamera.clearFlags = CameraClearFlags.Depth;
      Shader.SetGlobalTexture("_bgTex", this.BackgroundImage);
    }
    else
    {
      this.mRTBase = this.GetTemporaryRT(0.75f, RenderTextureFormat.Default, 16);
      this.mCamera.SetTargetBuffers(this.mRTBase.colorBuffer, this.mRTBase.depthBuffer);
      Graphics.SetRenderTarget(this.mRTBase);
      GL.Clear(false, true, new Color(0.0f, 0.0f, 0.0f, 0.0f));
      Graphics.SetRenderTarget(this.mRTBase);
      this.mBlitCopy.SetColor("_color", CameraHook.ColorMod);
      this.mBlitCopy.EnableKeyword("USE_COLOR");
      Graphics.Blit(this.BackgroundImage, this.mBlitCopy);
      this.mBlitCopy.DisableKeyword("USE_COLOR");
      this.mCamera.clearFlags = CameraClearFlags.Depth;
    }
  }

  private void OnPostRender()
  {
    if (this.RenderMode != RenderPipeline.RenderModes.None)
    {
      Shader.DisableKeyword("BGMASK_ON");
      if (this.mApplyBloom)
      {
        Texture texture = this.ApplyBloom();
        this.mUberPostEffect.SetFloat("_bloomStrength", this.mBloomStrength);
        this.mUberPostEffect.SetTexture("_bloomTex", texture);
        this.mUberPostEffect.SetTexture("_vignette", this.ScreenFilter);
        this.mUberPostEffect.EnableKeyword("BLOOM_ON");
      }
      else
        this.mUberPostEffect.DisableKeyword("BLOOM_ON");
      if (this.EnableVignette)
        this.mUberPostEffect.EnableKeyword("VIGNETTE_ON");
      else
        this.mUberPostEffect.DisableKeyword("VIGNETTE_ON");
      Graphics.Blit((Texture) this.mRTBase, this.mRTComposite, this.mUberPostEffect);
      GameObject gameObject = new GameObject();
      UnityEngine.Camera tempCam = gameObject.AddComponent<UnityEngine.Camera>();
      tempCam.CopyFrom(this.mCamera);
      RenderPipeline.RenderCamera(tempCam, GameUtility.LayerMaskEffect, this.mRTComposite.colorBuffer, this.mRTBase.depthBuffer);
      RenderPipeline.RenderCamera(tempCam, GameUtility.LayerMaskUI, this.mRTComposite.colorBuffer, this.mRTBase.depthBuffer);
      UnityEngine.Object.DestroyImmediate((UnityEngine.Object) gameObject);
      if (this.FlipHorizontally)
        this.mBlitCopy.SetVector("_scaleOffset", new Vector4(-1f, 1f, 1f, 0.0f));
      else
        this.mBlitCopy.SetVector("_scaleOffset", new Vector4(1f, 1f, 0.0f, 0.0f));
      Material mat = (Material) null;
      switch (this.SwapEffect)
      {
        case RenderPipeline.SwapEffects.Copy:
          mat = this.mBlitCopy;
          break;
        case RenderPipeline.SwapEffects.Dodge:
          mat = this.mBlitDodge;
          mat.SetFloat("_opacity", this.SwapEffectOpacity);
          break;
      }
      Graphics.SetRenderTarget((RenderTexture) null);
      GL.Viewport(new Rect(0.0f, 0.0f, (float) Screen.width, (float) Screen.height));
      switch (this.RenderMode)
      {
        case RenderPipeline.RenderModes.Default:
          Graphics.Blit((Texture) this.mRTComposite, mat);
          break;
        case RenderPipeline.RenderModes.Base:
          Graphics.Blit((Texture) this.mRTBase, mat);
          break;
        case RenderPipeline.RenderModes.Bloom0:
          Graphics.Blit((Texture) this.mRTBloom0, mat);
          break;
        case RenderPipeline.RenderModes.Bloom1:
          Graphics.Blit((Texture) this.mRTBloom1, mat);
          break;
      }
      RenderTexture.ReleaseTemporary(this.mRTBase);
      RenderTexture.ReleaseTemporary(this.mRTBloom0);
      RenderTexture.ReleaseTemporary(this.mRTBloom1);
      RenderTexture.ReleaseTemporary(this.mRTComposite);
      GL.Clear(true, false, new Color(1f, 1f, 1f, 1f));
      this.transform.rotation = this.mOldRotation;
      this.mCamera.targetTexture = (RenderTexture) null;
    }
    else
    {
      Graphics.SetRenderTarget((RenderTexture) null);
      GL.Viewport(new Rect(0.0f, 0.0f, (float) Screen.width, (float) Screen.height));
      this.mBlitCopy.SetVector("_scaleOffset", new Vector4(1f, 1f, 0.0f, 0.0f));
      Graphics.Blit((Texture) this.mRTBase, this.mBlitCopy);
      RenderTexture.ReleaseTemporary(this.mRTBase);
      GL.Clear(true, false, new Color(1f, 1f, 1f, 1f));
      this.transform.rotation = this.mOldRotation;
      this.mCamera.targetTexture = (RenderTexture) null;
    }
  }

  private static void RenderCamera(UnityEngine.Camera tempCam, int cullingMask, RenderBuffer colorBuffer, RenderBuffer depthBuffer)
  {
    tempCam.clearFlags = CameraClearFlags.Nothing;
    tempCam.cullingMask = cullingMask;
    tempCam.SetTargetBuffers(colorBuffer, depthBuffer);
    tempCam.Render();
  }

  private float[] CalcGaussianKernel(int count, float strength)
  {
    float[] numArray = new float[count];
    float num1 = 0.0f;
    for (int index = 0; index < count; ++index)
    {
      int num2 = index - count / 2;
      numArray[index] = Mathf.Exp(-0.5f * (float) (num2 * num2) / strength);
      num1 += numArray[index];
    }
    for (int index = 0; index < count; ++index)
      numArray[index] /= num1;
    return numArray;
  }

  private Texture ApplyBloom()
  {
    Graphics.Blit((Texture) this.mRTBase, this.mRTBloom0, this.mBloomPrePass);
    int count = 7;
    float blurStrength = this.BlurStrength;
    float num1 = 1.5f;
    Vector4 zero = Vector4.zero;
    float[] numArray = this.CalcGaussianKernel(count, blurStrength);
    for (int index = 0; index < count; ++index)
    {
      int num2 = index - count / 2;
      zero.x = (float) num2 * (1f / (float) this.mRTBloom0.width) * num1;
      zero.y = 0.0f;
      zero.z = numArray[index];
      this.mBlurEffect.SetVector("_offsetAndWeight" + (object) index, zero);
    }
    Graphics.Blit((Texture) this.mRTBloom0, this.mRTBloom1, this.mBlurEffect);
    for (int index = 0; index < count; ++index)
    {
      int num2 = index - count / 2;
      zero.y = (float) num2 * (1f / (float) this.mRTBloom0.height) * num1;
      zero.x = 0.0f;
      zero.z = numArray[index];
      this.mBlurEffect.SetVector("_offsetAndWeight" + (object) index, zero);
    }
    Graphics.Blit((Texture) this.mRTBloom1, this.mRTBloom0, this.mBlurEffect);
    return (Texture) this.mRTBloom0;
  }

  public enum RenderModes
  {
    Default,
    Base,
    Bloom0,
    Bloom1,
    None,
  }

  public enum SwapEffects
  {
    Copy,
    Dodge,
  }
}
