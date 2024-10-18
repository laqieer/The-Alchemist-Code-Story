﻿// Decompiled with JetBrains decompiler
// Type: RenderPipeline
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using SRPG;
using System;
using UnityEngine;

#nullable disable
[RequireComponent(typeof (Camera))]
[DisallowMultipleComponent]
public class RenderPipeline : MonoBehaviour
{
  [NonSerialized]
  public RenderPipeline.RenderModes RenderMode;
  private Camera mCamera;
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
  private bool mApplyBloom = true;
  private bool mApplyVignette = true;
  private RenderForWindows mRenderForWindows;
  private Quaternion mOldRotation;

  public bool EnableBloom
  {
    set => this.mApplyBloom = value;
    get => this.mApplyBloom;
  }

  public bool EnableVignette
  {
    set => this.mApplyVignette = value;
    get => this.mApplyVignette;
  }

  public float mBloomStrength => GameSettings.Instance.PostEffect_BloomMaxStrength;

  public float BlurStrength => GameSettings.Instance.PostEffect_BloomBlurStrength;

  public static void Setup(Camera camera)
  {
    ((Component) camera).gameObject.RequireComponent<RenderPipeline>();
  }

  private void Awake()
  {
    this.mCamera = ((Component) this).GetComponent<Camera>();
    this.mRenderForWindows = GameUtility.RequireComponent<RenderForWindows>(((Component) this).gameObject);
    this.mRenderForWindows.SetTargetType(RenderForWindows.eTargetType.MAIN_CAMERA);
    this.mRenderForWindows.IsDontCreteRenderTexture = true;
  }

  private void Start()
  {
    RenderTextureFormat[] values = (RenderTextureFormat[]) Enum.GetValues(typeof (RenderTextureFormat));
    if (GameUtility.IsDebugBuild)
    {
      string str = string.Empty;
      for (int index = 0; index < values.Length; ++index)
      {
        RenderTextureFormat renderTextureFormat = (RenderTextureFormat) (int) values[index];
        if (SystemInfo.SupportsRenderTextureFormat(renderTextureFormat))
          str = str + renderTextureFormat.ToString() + " ";
      }
      Debug.Log((object) ("Supported RenderTexture Formats: " + str));
    }
    CameraHook.Inject();
  }

  private RenderTexture ReserveRT(
    ref RenderTexture rt,
    float w,
    float h,
    RenderTextureFormat format,
    int depth)
  {
    RenderTexture targetTexture = this.mCamera.targetTexture;
    int num1;
    int num2;
    if (UnityEngine.Object.op_Inequality((UnityEngine.Object) targetTexture, (UnityEngine.Object) null))
    {
      num1 = ((Texture) targetTexture).width;
      num2 = ((Texture) targetTexture).height;
    }
    else
    {
      num1 = 1920;
      num2 = 1080;
    }
    int num3 = Mathf.FloorToInt((float) num1 * w);
    int num4 = Mathf.FloorToInt((float) num2 * h);
    if (UnityEngine.Object.op_Equality((UnityEngine.Object) rt, (UnityEngine.Object) null) || ((Texture) rt).width != num3 || ((Texture) rt).height != num4)
    {
      if (!SystemInfo.SupportsRenderTextureFormat(format) && format == 16)
        format = (RenderTextureFormat) 7;
      RenderTexture.ReleaseTemporary(rt);
      rt = RenderTexture.GetTemporary(num3, num4, depth, format, (RenderTextureReadWrite) 1);
      rt.autoGenerateMips = false;
    }
    return rt;
  }

  private void ReleaseRT(ref RenderTexture rt)
  {
    RenderTexture.ReleaseTemporary(rt);
    rt = (RenderTexture) null;
  }

  private void CreatePass(
    ref GameObject pass,
    string name,
    int priority,
    CameraCallback.CameraEvent onPreCull,
    CameraCallback.CameraEvent onPreRender,
    CameraCallback.CameraEvent onPostRender,
    CameraCallback.RenderImageEvent onRenderImage,
    int cullingMask,
    bool clearDepth)
  {
    if (UnityEngine.Object.op_Equality((UnityEngine.Object) pass, (UnityEngine.Object) null))
    {
      pass = new GameObject(name, new System.Type[2]
      {
        typeof (Camera),
        typeof (CameraCallback)
      });
      ((UnityEngine.Object) pass).hideFlags = (HideFlags) 52;
      pass.transform.parent = ((Component) this).transform;
      CameraCallback component = pass.GetComponent<CameraCallback>();
      component.OnCameraPreCull = onPreCull;
      component.OnCameraPreRender = onPreRender;
      component.OnCameraPostRender = onPostRender;
      component.OnCameraRenderImage = onRenderImage;
    }
    Camera component1 = pass.GetComponent<Camera>();
    component1.CopyFrom(this.mCamera);
    component1.depth = this.mCamera.depth + (float) priority;
    component1.cullingMask = cullingMask;
    component1.targetTexture = (RenderTexture) null;
    component1.clearFlags = !clearDepth ? (CameraClearFlags) 4 : (CameraClearFlags) 3;
  }

  private void DestroyPass(ref GameObject pass)
  {
    if (UnityEngine.Object.op_Inequality((UnityEngine.Object) pass, (UnityEngine.Object) null))
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
    return !UnityEngine.Object.op_Equality((UnityEngine.Object) shader, (UnityEngine.Object) null) ? new Material(shader) : throw new Exception("Can't find shader " + path);
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
      ((Behaviour) this).enabled = false;
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
    if (StreamingMovie.IsPlaying)
      return;
    this.mOldRotation = ((Component) this).transform.rotation;
    if (this.RenderMode != RenderPipeline.RenderModes.None)
      this.mCamera.cullingMask = -1 & ~(GameUtility.LayerMaskEffect | GameUtility.LayerMaskHidden | GameUtility.LayerMaskUI);
    else
      this.mCamera.cullingMask |= GameUtility.LayerMaskEffect | GameUtility.LayerMaskHidden | GameUtility.LayerMaskUI;
    this.mCamera.clearFlags = (CameraClearFlags) 3;
    this.mCamera.backgroundColor = new Color(0.0f, 0.0f, 0.0f);
  }

  private RenderTexture GetTemporaryRT(float scaleFactor, RenderTextureFormat format, int depthBpp)
  {
    return RenderTexture.GetTemporary(Mathf.FloorToInt(1920f * scaleFactor), Mathf.FloorToInt(1080f * scaleFactor), depthBpp, format);
  }

  private void OnPreRender()
  {
    if (StreamingMovie.IsPlaying)
      return;
    if (this.RenderMode != RenderPipeline.RenderModes.None)
    {
      float scaleFactor = 0.75f;
      float num = 0.25f;
      this.mRTBase = this.GetTemporaryRT(scaleFactor, (RenderTextureFormat) 7, 16);
      this.mRTBloom0 = this.GetTemporaryRT(scaleFactor * num, (RenderTextureFormat) 7, 0);
      this.mRTBloom1 = this.GetTemporaryRT(scaleFactor * num, (RenderTextureFormat) 7, 0);
      this.mRTComposite = this.GetTemporaryRT(scaleFactor, (RenderTextureFormat) 7, 0);
      this.mCamera.SetTargetBuffers(this.mRTBase.colorBuffer, this.mRTBase.depthBuffer);
      this.mRenderForWindows.SetRenderTexture(this.mRTComposite);
      Graphics.SetRenderTarget(this.mRTBase);
      GL.Clear(false, true, new Color(0.0f, 0.0f, 0.0f, 0.0f));
      Shader.EnableKeyword("BGMASK_ON");
      Graphics.SetRenderTarget(this.mRTBase);
      this.mBlitCopy.SetColor("_color", CameraHook.ColorMod);
      this.mBlitCopy.EnableKeyword("USE_COLOR");
      Graphics.Blit(this.BackgroundImage, this.mBlitCopy);
      this.mBlitCopy.DisableKeyword("USE_COLOR");
      this.mCamera.clearFlags = (CameraClearFlags) 3;
      Shader.SetGlobalTexture("_bgTex", this.BackgroundImage);
    }
    else
    {
      this.mRTBase = this.GetTemporaryRT(0.75f, (RenderTextureFormat) 7, 16);
      this.mCamera.SetTargetBuffers(this.mRTBase.colorBuffer, this.mRTBase.depthBuffer);
      this.mRenderForWindows.SetRenderTexture(this.mRTBase);
      Graphics.SetRenderTarget(this.mRTBase);
      GL.Clear(false, true, new Color(0.0f, 0.0f, 0.0f, 0.0f));
      Graphics.SetRenderTarget(this.mRTBase);
      this.mBlitCopy.SetColor("_color", CameraHook.ColorMod);
      this.mBlitCopy.EnableKeyword("USE_COLOR");
      Graphics.Blit(this.BackgroundImage, this.mBlitCopy);
      this.mBlitCopy.DisableKeyword("USE_COLOR");
      this.mCamera.clearFlags = (CameraClearFlags) 3;
    }
  }

  private void OnPostRender()
  {
    if (StreamingMovie.IsPlaying)
      return;
    if (this.RenderMode != RenderPipeline.RenderModes.None)
    {
      Shader.DisableKeyword("BGMASK_ON");
      if (this.mApplyBloom)
      {
        Texture texture = this.ApplyBloom();
        this.mUberPostEffect.EnableKeyword("BLOOM_ON");
        this.mUberPostEffect.SetFloat("_bloomStrength", this.mBloomStrength);
        this.mUberPostEffect.SetTexture("_bloomTex", texture);
        this.mUberPostEffect.SetTexture("_vignette", this.ScreenFilter);
      }
      else
        this.mUberPostEffect.DisableKeyword("BLOOM_ON");
      if (this.EnableVignette)
        this.mUberPostEffect.EnableKeyword("VIGNETTE_ON");
      else
        this.mUberPostEffect.DisableKeyword("VIGNETTE_ON");
      Graphics.Blit((Texture) this.mRTBase, this.mRTComposite, this.mUberPostEffect);
      GameObject gameObject = new GameObject();
      Camera tempCam = gameObject.AddComponent<Camera>();
      tempCam.CopyFrom(this.mCamera);
      RenderPipeline.RenderCamera(tempCam, GameUtility.LayerMaskEffect, this.mRTComposite.colorBuffer, this.mRTBase.depthBuffer);
      RenderPipeline.RenderCamera(tempCam, GameUtility.LayerMaskUI, this.mRTComposite.colorBuffer, this.mRTBase.depthBuffer);
      UnityEngine.Object.DestroyImmediate((UnityEngine.Object) gameObject);
      if (this.FlipHorizontally)
        this.mBlitCopy.SetVector("_scaleOffset", new Vector4(-1f, 1f, 1f, 0.0f));
      else
        this.mBlitCopy.SetVector("_scaleOffset", new Vector4(1f, 1f, 0.0f, 0.0f));
      Material material = (Material) null;
      switch (this.SwapEffect)
      {
        case RenderPipeline.SwapEffects.Copy:
          material = this.mBlitCopy;
          break;
        case RenderPipeline.SwapEffects.Dodge:
          material = this.mBlitDodge;
          material.SetFloat("_opacity", this.SwapEffectOpacity);
          break;
      }
      Graphics.SetRenderTarget((RenderTexture) null);
      GL.Viewport(new Rect(0.0f, 0.0f, 1920f, 1080f));
      switch (this.RenderMode)
      {
        case RenderPipeline.RenderModes.Default:
          Graphics.Blit((Texture) this.mRTComposite, material);
          break;
        case RenderPipeline.RenderModes.Base:
          Graphics.Blit((Texture) this.mRTBase, material);
          break;
        case RenderPipeline.RenderModes.Bloom0:
          Graphics.Blit((Texture) this.mRTBloom0, material);
          break;
        case RenderPipeline.RenderModes.Bloom1:
          Graphics.Blit((Texture) this.mRTBloom1, material);
          break;
      }
      RenderTexture.ReleaseTemporary(this.mRTBase);
      RenderTexture.ReleaseTemporary(this.mRTBloom0);
      RenderTexture.ReleaseTemporary(this.mRTBloom1);
      RenderTexture.ReleaseTemporary(this.mRTComposite);
      GL.Clear(true, false, new Color(1f, 1f, 1f, 1f));
      ((Component) this).transform.rotation = this.mOldRotation;
      this.mCamera.targetTexture = (RenderTexture) null;
    }
    else
    {
      Graphics.SetRenderTarget((RenderTexture) null);
      GL.Viewport(new Rect(0.0f, 0.0f, 1920f, 1080f));
      this.mBlitCopy.SetVector("_scaleOffset", new Vector4(1f, 1f, 0.0f, 0.0f));
      Graphics.Blit((Texture) this.mRTBase, this.mBlitCopy);
      RenderTexture.ReleaseTemporary(this.mRTBase);
      GL.Clear(true, false, new Color(1f, 1f, 1f, 1f));
      ((Component) this).transform.rotation = this.mOldRotation;
      this.mCamera.targetTexture = (RenderTexture) null;
    }
  }

  private static void RenderCamera(
    Camera tempCam,
    int cullingMask,
    RenderBuffer colorBuffer,
    RenderBuffer depthBuffer)
  {
    tempCam.clearFlags = (CameraClearFlags) 4;
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
    Vector4[] vector4Array = new Vector4[count];
    float[] numArray = this.CalcGaussianKernel(count, blurStrength);
    for (int index = 0; index < count; ++index)
    {
      int num2 = index - count / 2;
      vector4Array[index].x = (float) num2 * (1f / (float) ((Texture) this.mRTBloom0).width) * num1;
      vector4Array[index].y = 0.0f;
      vector4Array[index].z = numArray[index];
    }
    this.mBlurEffect.SetVectorArray("_offsetAndWeight", vector4Array);
    Graphics.Blit((Texture) this.mRTBloom0, this.mRTBloom1, this.mBlurEffect);
    for (int index = 0; index < count; ++index)
    {
      int num3 = index - count / 2;
      vector4Array[index].y = (float) num3 * (1f / (float) ((Texture) this.mRTBloom0).height) * num1;
      vector4Array[index].x = 0.0f;
      vector4Array[index].z = numArray[index];
    }
    this.mBlurEffect.SetVectorArray("_offsetAndWeight", vector4Array);
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
