// Decompiled with JetBrains decompiler
// Type: RenderHeads.Media.AVProVideo.DisplayUGUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable disable
namespace RenderHeads.Media.AVProVideo
{
  [ExecuteInEditMode]
  [HelpURL("http://renderheads.com/product/avpro-video/")]
  [AddComponentMenu("AVPro Video/Display uGUI", 200)]
  public class DisplayUGUI : MaskableGraphic
  {
    [SerializeField]
    public MediaPlayer _mediaPlayer;
    [SerializeField]
    public Rect m_UVRect = new Rect(0.0f, 0.0f, 1f, 1f);
    [SerializeField]
    public bool _setNativeSize;
    [SerializeField]
    public ScaleMode _scaleMode = (ScaleMode) 2;
    [SerializeField]
    public bool _noDefaultDisplay = true;
    [SerializeField]
    public bool _displayInEditor = true;
    [SerializeField]
    public Texture _defaultTexture;
    private int _lastWidth;
    private int _lastHeight;
    private bool _flipY;
    private Texture _lastTexture;
    private static Shader _shaderStereoPacking;
    private static Shader _shaderAlphaPacking;
    private static int _propAlphaPack;
    private static int _propVertScale;
    private static int _propStereo;
    private static int _propApplyGamma;
    private static int _propUseYpCbCr;
    private const string PropChromaTexName = "_ChromaTex";
    private static int _propChromaTex;
    private const string PropYpCbCrTransformName = "_YpCbCrTransform";
    private static int _propYpCbCrTransform;
    private bool _userMaterial = true;
    private Material _material;
    private List<UIVertex> _vertices = new List<UIVertex>(4);
    private static List<int> QuadIndices = new List<int>((IEnumerable<int>) new int[6]
    {
      0,
      1,
      2,
      2,
      3,
      0
    });

    protected virtual void Awake()
    {
      if (DisplayUGUI._propAlphaPack == 0)
      {
        DisplayUGUI._propStereo = Shader.PropertyToID("Stereo");
        DisplayUGUI._propAlphaPack = Shader.PropertyToID("AlphaPack");
        DisplayUGUI._propVertScale = Shader.PropertyToID("_VertScale");
        DisplayUGUI._propApplyGamma = Shader.PropertyToID("_ApplyGamma");
        DisplayUGUI._propUseYpCbCr = Shader.PropertyToID("_UseYpCbCr");
        DisplayUGUI._propChromaTex = Shader.PropertyToID("_ChromaTex");
        DisplayUGUI._propUseYpCbCr = Shader.PropertyToID("_UseYpCbCr");
        DisplayUGUI._propYpCbCrTransform = Shader.PropertyToID("_YpCbCrTransform");
      }
      if (Object.op_Equality((Object) DisplayUGUI._shaderAlphaPacking, (Object) null))
      {
        DisplayUGUI._shaderAlphaPacking = Shader.Find("AVProVideo/UI/Transparent Packed");
        if (Object.op_Equality((Object) DisplayUGUI._shaderAlphaPacking, (Object) null))
          Debug.LogWarning((object) "[AVProVideo] Missing shader AVProVideo/UI/Transparent Packed");
      }
      if (Object.op_Equality((Object) DisplayUGUI._shaderStereoPacking, (Object) null))
      {
        DisplayUGUI._shaderStereoPacking = Shader.Find("AVProVideo/UI/Stereo");
        if (Object.op_Equality((Object) DisplayUGUI._shaderStereoPacking, (Object) null))
          Debug.LogWarning((object) "[AVProVideo] Missing shader AVProVideo/UI/Stereo");
      }
      ((UIBehaviour) this).Awake();
    }

    protected virtual void Start()
    {
      this._userMaterial = Object.op_Inequality((Object) ((Graphic) this).m_Material, (Object) null);
      ((UIBehaviour) this).Start();
    }

    protected virtual void OnDestroy()
    {
      if (Object.op_Inequality((Object) this._material, (Object) null))
      {
        ((Graphic) this).material = (Material) null;
        Object.Destroy((Object) this._material);
        this._material = (Material) null;
      }
      ((UIBehaviour) this).OnDestroy();
    }

    private Shader GetRequiredShader()
    {
      Shader requiredShader = (Shader) null;
      switch (this._mediaPlayer.m_StereoPacking)
      {
        case StereoPacking.TopBottom:
        case StereoPacking.LeftRight:
          requiredShader = DisplayUGUI._shaderStereoPacking;
          break;
      }
      switch (this._mediaPlayer.m_AlphaPacking)
      {
        case AlphaPacking.TopBottom:
        case AlphaPacking.LeftRight:
          requiredShader = DisplayUGUI._shaderAlphaPacking;
          break;
      }
      if (Object.op_Equality((Object) requiredShader, (Object) null) && this._mediaPlayer.Info != null && QualitySettings.activeColorSpace == 1 && !this._mediaPlayer.Info.PlayerSupportsLinearColorSpace())
        requiredShader = DisplayUGUI._shaderAlphaPacking;
      if (Object.op_Equality((Object) requiredShader, (Object) null) && this._mediaPlayer.TextureProducer != null && this._mediaPlayer.TextureProducer.GetTextureCount() == 2)
        requiredShader = DisplayUGUI._shaderAlphaPacking;
      return requiredShader;
    }

    public virtual Texture mainTexture
    {
      get
      {
        Texture mainTexture = (Texture) Texture2D.whiteTexture;
        if (this.HasValidTexture())
        {
          Texture texture = this._mediaPlayer.FrameResampler == null || this._mediaPlayer.FrameResampler.OutputTexture == null ? (Texture) null : this._mediaPlayer.FrameResampler.OutputTexture[0];
          mainTexture = !this._mediaPlayer.m_Resample ? this._mediaPlayer.TextureProducer.GetTexture() : texture;
        }
        else if (this._noDefaultDisplay)
          mainTexture = (Texture) null;
        else if (Object.op_Inequality((Object) this._defaultTexture, (Object) null))
          mainTexture = this._defaultTexture;
        return mainTexture;
      }
    }

    public bool HasValidTexture()
    {
      return Object.op_Inequality((Object) this._mediaPlayer, (Object) null) && this._mediaPlayer.TextureProducer != null && Object.op_Inequality((Object) this._mediaPlayer.TextureProducer.GetTexture(), (Object) null);
    }

    private void UpdateInternalMaterial()
    {
      if (!Object.op_Inequality((Object) this._mediaPlayer, (Object) null))
        return;
      Shader shader = (Shader) null;
      if (Object.op_Inequality((Object) this._material, (Object) null))
        shader = this._material.shader;
      Shader requiredShader = this.GetRequiredShader();
      if (Object.op_Inequality((Object) shader, (Object) requiredShader))
      {
        if (Object.op_Inequality((Object) this._material, (Object) null))
        {
          ((Graphic) this).material = (Material) null;
          Object.Destroy((Object) this._material);
          this._material = (Material) null;
        }
        if (Object.op_Inequality((Object) requiredShader, (Object) null))
          this._material = new Material(requiredShader);
      }
      ((Graphic) this).material = this._material;
    }

    private void LateUpdate()
    {
      if (this._setNativeSize)
        ((Graphic) this).SetNativeSize();
      if (Object.op_Inequality((Object) this._lastTexture, (Object) ((Graphic) this).mainTexture))
      {
        this._lastTexture = ((Graphic) this).mainTexture;
        ((Graphic) this).SetVerticesDirty();
        ((Graphic) this).SetMaterialDirty();
      }
      if (this.HasValidTexture() && Object.op_Inequality((Object) ((Graphic) this).mainTexture, (Object) null) && (((Graphic) this).mainTexture.width != this._lastWidth || ((Graphic) this).mainTexture.height != this._lastHeight))
      {
        this._lastWidth = ((Graphic) this).mainTexture.width;
        this._lastHeight = ((Graphic) this).mainTexture.height;
        ((Graphic) this).SetVerticesDirty();
        ((Graphic) this).SetMaterialDirty();
      }
      if (!this._userMaterial && Application.isPlaying)
        this.UpdateInternalMaterial();
      if (!Object.op_Inequality((Object) ((Graphic) this).material, (Object) null) || !Object.op_Inequality((Object) this._mediaPlayer, (Object) null))
        return;
      if (((Graphic) this).material.HasProperty(DisplayUGUI._propUseYpCbCr) && this._mediaPlayer.TextureProducer != null && this._mediaPlayer.TextureProducer.GetTextureCount() == 2)
      {
        ((Graphic) this).material.EnableKeyword("USE_YPCBCR");
        ((Graphic) this).material.SetMatrix(DisplayUGUI._propYpCbCrTransform, this._mediaPlayer.TextureProducer.GetYpCbCrTransform());
        Texture texture = this._mediaPlayer.FrameResampler == null || this._mediaPlayer.FrameResampler.OutputTexture == null ? (Texture) null : this._mediaPlayer.FrameResampler.OutputTexture[1];
        ((Graphic) this).material.SetTexture(DisplayUGUI._propChromaTex, !this._mediaPlayer.m_Resample ? this._mediaPlayer.TextureProducer.GetTexture(1) : texture);
      }
      if (((Graphic) this).material.HasProperty(DisplayUGUI._propAlphaPack))
      {
        Helper.SetupAlphaPackedMaterial(((Graphic) this).material, this._mediaPlayer.m_AlphaPacking);
        if (this._flipY && this._mediaPlayer.m_AlphaPacking != AlphaPacking.None)
          ((Graphic) this).material.SetFloat(DisplayUGUI._propVertScale, -1f);
        else
          ((Graphic) this).material.SetFloat(DisplayUGUI._propVertScale, 1f);
      }
      if (((Graphic) this).material.HasProperty(DisplayUGUI._propStereo))
        Helper.SetupStereoMaterial(((Graphic) this).material, this._mediaPlayer.m_StereoPacking, this._mediaPlayer.m_DisplayDebugStereoColorTint);
      if (!((Graphic) this).material.HasProperty(DisplayUGUI._propApplyGamma) || this._mediaPlayer.Info == null)
        return;
      Helper.SetupGammaMaterial(((Graphic) this).material, this._mediaPlayer.Info.PlayerSupportsLinearColorSpace());
    }

    public MediaPlayer CurrentMediaPlayer
    {
      get => this._mediaPlayer;
      set
      {
        if (!Object.op_Inequality((Object) this._mediaPlayer, (Object) value))
          return;
        this._mediaPlayer = value;
        ((Graphic) this).SetMaterialDirty();
      }
    }

    public Rect uvRect
    {
      get => this.m_UVRect;
      set
      {
        if (Rect.op_Equality(this.m_UVRect, value))
          return;
        this.m_UVRect = value;
        ((Graphic) this).SetVerticesDirty();
      }
    }

    [ContextMenu("Set Native Size")]
    public virtual void SetNativeSize()
    {
      Texture mainTexture = ((Graphic) this).mainTexture;
      if (!Object.op_Inequality((Object) mainTexture, (Object) null))
        return;
      double width1 = (double) mainTexture.width;
      Rect uvRect1 = this.uvRect;
      double width2 = (double) ((Rect) ref uvRect1).width;
      int num1 = Mathf.RoundToInt((float) (width1 * width2));
      double height1 = (double) mainTexture.height;
      Rect uvRect2 = this.uvRect;
      double height2 = (double) ((Rect) ref uvRect2).height;
      int num2 = Mathf.RoundToInt((float) (height1 * height2));
      if (Object.op_Inequality((Object) this._mediaPlayer, (Object) null))
      {
        if (this._mediaPlayer.m_AlphaPacking == AlphaPacking.LeftRight || this._mediaPlayer.m_StereoPacking == StereoPacking.LeftRight)
          num1 /= 2;
        else if (this._mediaPlayer.m_AlphaPacking == AlphaPacking.TopBottom || this._mediaPlayer.m_StereoPacking == StereoPacking.TopBottom)
          num2 /= 2;
      }
      ((Graphic) this).rectTransform.anchorMax = ((Graphic) this).rectTransform.anchorMin;
      ((Graphic) this).rectTransform.sizeDelta = new Vector2((float) num1, (float) num2);
    }

    protected virtual void OnPopulateMesh(VertexHelper vh)
    {
      vh.Clear();
      this._OnFillVBO(this._vertices);
      vh.AddUIVertexStream(this._vertices, DisplayUGUI.QuadIndices);
    }

    [Obsolete("This method is not called from Unity 5.2 and above")]
    protected virtual void OnFillVBO(List<UIVertex> vbo) => this._OnFillVBO(vbo);

    private void _OnFillVBO(List<UIVertex> vbo)
    {
      this._flipY = false;
      if (this.HasValidTexture())
        this._flipY = this._mediaPlayer.TextureProducer.RequiresVerticalFlip();
      Rect uvRect = this.m_UVRect;
      Vector4 drawingDimensions = this.GetDrawingDimensions(this._scaleMode, ref uvRect);
      vbo.Clear();
      UIVertex simpleVert = UIVertex.simpleVert;
      simpleVert.color = Color32.op_Implicit(((Graphic) this).color);
      simpleVert.position = Vector2.op_Implicit(new Vector2(drawingDimensions.x, drawingDimensions.y));
      simpleVert.uv0 = new Vector2(((Rect) ref uvRect).xMin, ((Rect) ref uvRect).yMin);
      if (this._flipY)
        simpleVert.uv0 = new Vector2(((Rect) ref uvRect).xMin, 1f - ((Rect) ref uvRect).yMin);
      vbo.Add(simpleVert);
      simpleVert.position = Vector2.op_Implicit(new Vector2(drawingDimensions.x, drawingDimensions.w));
      simpleVert.uv0 = new Vector2(((Rect) ref uvRect).xMin, ((Rect) ref uvRect).yMax);
      if (this._flipY)
        simpleVert.uv0 = new Vector2(((Rect) ref uvRect).xMin, 1f - ((Rect) ref uvRect).yMax);
      vbo.Add(simpleVert);
      simpleVert.position = Vector2.op_Implicit(new Vector2(drawingDimensions.z, drawingDimensions.w));
      simpleVert.uv0 = new Vector2(((Rect) ref uvRect).xMax, ((Rect) ref uvRect).yMax);
      if (this._flipY)
        simpleVert.uv0 = new Vector2(((Rect) ref uvRect).xMax, 1f - ((Rect) ref uvRect).yMax);
      vbo.Add(simpleVert);
      simpleVert.position = Vector2.op_Implicit(new Vector2(drawingDimensions.z, drawingDimensions.y));
      simpleVert.uv0 = new Vector2(((Rect) ref uvRect).xMax, ((Rect) ref uvRect).yMin);
      if (this._flipY)
        simpleVert.uv0 = new Vector2(((Rect) ref uvRect).xMax, 1f - ((Rect) ref uvRect).yMin);
      vbo.Add(simpleVert);
    }

    private Vector4 GetDrawingDimensions(ScaleMode scaleMode, ref Rect uvRect)
    {
      Vector4 zero1 = Vector4.zero;
      if (Object.op_Inequality((Object) ((Graphic) this).mainTexture, (Object) null))
      {
        Vector4 zero2 = Vector4.zero;
        Vector2 vector2;
        // ISSUE: explicit constructor call
        ((Vector2) ref vector2).\u002Ector((float) ((Graphic) this).mainTexture.width, (float) ((Graphic) this).mainTexture.height);
        if (Object.op_Inequality((Object) this._mediaPlayer, (Object) null))
        {
          if (this._mediaPlayer.m_AlphaPacking == AlphaPacking.LeftRight || this._mediaPlayer.m_StereoPacking == StereoPacking.LeftRight)
            vector2.x /= 2f;
          else if (this._mediaPlayer.m_AlphaPacking == AlphaPacking.TopBottom || this._mediaPlayer.m_StereoPacking == StereoPacking.TopBottom)
            vector2.y /= 2f;
        }
        Rect pixelAdjustedRect = ((Graphic) this).GetPixelAdjustedRect();
        int num1 = Mathf.RoundToInt(vector2.x);
        int num2 = Mathf.RoundToInt(vector2.y);
        Vector4 vector4;
        // ISSUE: explicit constructor call
        ((Vector4) ref vector4).\u002Ector(zero2.x / (float) num1, zero2.y / (float) num2, ((float) num1 - zero2.z) / (float) num1, ((float) num2 - zero2.w) / (float) num2);
        if ((double) ((Vector2) ref vector2).sqrMagnitude > 0.0)
        {
          if (scaleMode == 2)
          {
            float num3 = vector2.x / vector2.y;
            float num4 = ((Rect) ref pixelAdjustedRect).width / ((Rect) ref pixelAdjustedRect).height;
            if ((double) num3 > (double) num4)
            {
              float height = ((Rect) ref pixelAdjustedRect).height;
              ((Rect) ref pixelAdjustedRect).height = ((Rect) ref pixelAdjustedRect).width * (1f / num3);
              ref Rect local = ref pixelAdjustedRect;
              ((Rect) ref local).y = ((Rect) ref local).y + (height - ((Rect) ref pixelAdjustedRect).height) * ((Graphic) this).rectTransform.pivot.y;
            }
            else
            {
              float width = ((Rect) ref pixelAdjustedRect).width;
              ((Rect) ref pixelAdjustedRect).width = ((Rect) ref pixelAdjustedRect).height * num3;
              ref Rect local = ref pixelAdjustedRect;
              ((Rect) ref local).x = ((Rect) ref local).x + (width - ((Rect) ref pixelAdjustedRect).width) * ((Graphic) this).rectTransform.pivot.x;
            }
          }
          else if (scaleMode == 1)
          {
            float num5 = vector2.x / vector2.y;
            float num6 = ((Rect) ref pixelAdjustedRect).width / ((Rect) ref pixelAdjustedRect).height;
            if ((double) num6 > (double) num5)
            {
              float num7 = num5 / num6;
              // ISSUE: explicit constructor call
              ((Rect) ref uvRect).\u002Ector(((Rect) ref uvRect).xMin, (float) ((double) ((Rect) ref uvRect).yMin * (double) num7 + (1.0 - (double) num7) * 0.5), ((Rect) ref uvRect).width, num7 * ((Rect) ref uvRect).height);
            }
            else
            {
              float num8 = num6 / num5;
              // ISSUE: explicit constructor call
              ((Rect) ref uvRect).\u002Ector((float) ((double) ((Rect) ref uvRect).xMin * (double) num8 + (0.5 - (double) num8 * 0.5)), ((Rect) ref uvRect).yMin, num8 * ((Rect) ref uvRect).width, ((Rect) ref uvRect).height);
            }
          }
        }
        // ISSUE: explicit constructor call
        ((Vector4) ref zero1).\u002Ector(((Rect) ref pixelAdjustedRect).x + ((Rect) ref pixelAdjustedRect).width * vector4.x, ((Rect) ref pixelAdjustedRect).y + ((Rect) ref pixelAdjustedRect).height * vector4.y, ((Rect) ref pixelAdjustedRect).x + ((Rect) ref pixelAdjustedRect).width * vector4.z, ((Rect) ref pixelAdjustedRect).y + ((Rect) ref pixelAdjustedRect).height * vector4.w);
      }
      return zero1;
    }
  }
}
