// Decompiled with JetBrains decompiler
// Type: RenderHeads.Media.AVProVideo.DisplayUGUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RenderHeads.Media.AVProVideo
{
  [ExecuteInEditMode]
  [HelpURL("http://renderheads.com/product/avpro-video/")]
  [AddComponentMenu("AVPro Video/Display uGUI", 200)]
  public class DisplayUGUI : MaskableGraphic
  {
    private static List<int> QuadIndices = new List<int>((IEnumerable<int>) new int[6]
    {
      0,
      1,
      2,
      2,
      3,
      0
    });
    [SerializeField]
    public Rect m_UVRect = new Rect(0.0f, 0.0f, 1f, 1f);
    [SerializeField]
    public UnityEngine.ScaleMode _scaleMode = UnityEngine.ScaleMode.ScaleToFit;
    [SerializeField]
    public bool _noDefaultDisplay = true;
    [SerializeField]
    public bool _displayInEditor = true;
    private bool _userMaterial = true;
    private List<UIVertex> _vertices = new List<UIVertex>(4);
    private const string PropChromaTexName = "_ChromaTex";
    [SerializeField]
    public MediaPlayer _mediaPlayer;
    [SerializeField]
    public bool _setNativeSize;
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
    private static int _propChromaTex;
    private Material _material;

    protected override void Awake()
    {
      if (DisplayUGUI._propAlphaPack == 0)
      {
        DisplayUGUI._propStereo = Shader.PropertyToID("Stereo");
        DisplayUGUI._propAlphaPack = Shader.PropertyToID("AlphaPack");
        DisplayUGUI._propVertScale = Shader.PropertyToID("_VertScale");
        DisplayUGUI._propApplyGamma = Shader.PropertyToID("_ApplyGamma");
        DisplayUGUI._propUseYpCbCr = Shader.PropertyToID("_UseYpCbCr");
        DisplayUGUI._propChromaTex = Shader.PropertyToID("_ChromaTex");
      }
      if ((UnityEngine.Object) DisplayUGUI._shaderAlphaPacking == (UnityEngine.Object) null)
      {
        DisplayUGUI._shaderAlphaPacking = Shader.Find("AVProVideo/UI/Transparent Packed");
        if ((UnityEngine.Object) DisplayUGUI._shaderAlphaPacking == (UnityEngine.Object) null)
          Debug.LogWarning((object) "[AVProVideo] Missing shader AVProVideo/UI/Transparent Packed");
      }
      if ((UnityEngine.Object) DisplayUGUI._shaderStereoPacking == (UnityEngine.Object) null)
      {
        DisplayUGUI._shaderStereoPacking = Shader.Find("AVProVideo/UI/Stereo");
        if ((UnityEngine.Object) DisplayUGUI._shaderStereoPacking == (UnityEngine.Object) null)
          Debug.LogWarning((object) "[AVProVideo] Missing shader AVProVideo/UI/Stereo");
      }
      base.Awake();
    }

    protected override void Start()
    {
      this._userMaterial = (UnityEngine.Object) this.m_Material != (UnityEngine.Object) null;
      base.Start();
    }

    protected override void OnDestroy()
    {
      if ((UnityEngine.Object) this._material != (UnityEngine.Object) null)
      {
        this.material = (Material) null;
        UnityEngine.Object.Destroy((UnityEngine.Object) this._material);
        this._material = (Material) null;
      }
      base.OnDestroy();
    }

    private Shader GetRequiredShader()
    {
      Shader shader = (Shader) null;
      switch (this._mediaPlayer.m_StereoPacking)
      {
        case StereoPacking.TopBottom:
        case StereoPacking.LeftRight:
          shader = DisplayUGUI._shaderStereoPacking;
          break;
      }
      switch (this._mediaPlayer.m_AlphaPacking)
      {
        case AlphaPacking.TopBottom:
        case AlphaPacking.LeftRight:
          shader = DisplayUGUI._shaderAlphaPacking;
          break;
      }
      if ((UnityEngine.Object) shader == (UnityEngine.Object) null && this._mediaPlayer.TextureProducer != null && this._mediaPlayer.TextureProducer.GetTextureCount() == 2)
        shader = DisplayUGUI._shaderAlphaPacking;
      return shader;
    }

    public override Texture mainTexture
    {
      get
      {
        Texture texture = (Texture) Texture2D.whiteTexture;
        if (this.HasValidTexture())
          texture = this._mediaPlayer.TextureProducer.GetTexture(0);
        else if (this._noDefaultDisplay)
          texture = (Texture) null;
        else if ((UnityEngine.Object) this._defaultTexture != (UnityEngine.Object) null)
          texture = this._defaultTexture;
        return texture;
      }
    }

    public bool HasValidTexture()
    {
      if ((UnityEngine.Object) this._mediaPlayer != (UnityEngine.Object) null && this._mediaPlayer.TextureProducer != null)
        return (UnityEngine.Object) this._mediaPlayer.TextureProducer.GetTexture(0) != (UnityEngine.Object) null;
      return false;
    }

    private void UpdateInternalMaterial()
    {
      if (!((UnityEngine.Object) this._mediaPlayer != (UnityEngine.Object) null))
        return;
      Shader shader = (Shader) null;
      if ((UnityEngine.Object) this._material != (UnityEngine.Object) null)
        shader = this._material.shader;
      Shader requiredShader = this.GetRequiredShader();
      if ((UnityEngine.Object) shader != (UnityEngine.Object) requiredShader)
      {
        if ((UnityEngine.Object) this._material != (UnityEngine.Object) null)
        {
          this.material = (Material) null;
          UnityEngine.Object.Destroy((UnityEngine.Object) this._material);
          this._material = (Material) null;
        }
        if ((UnityEngine.Object) requiredShader != (UnityEngine.Object) null)
          this._material = new Material(requiredShader);
      }
      this.material = this._material;
    }

    private void LateUpdate()
    {
      if (this._setNativeSize)
        this.SetNativeSize();
      if ((UnityEngine.Object) this._lastTexture != (UnityEngine.Object) this.mainTexture)
      {
        this._lastTexture = this.mainTexture;
        this.SetVerticesDirty();
      }
      if (this.HasValidTexture() && (UnityEngine.Object) this.mainTexture != (UnityEngine.Object) null && (this.mainTexture.width != this._lastWidth || this.mainTexture.height != this._lastHeight))
      {
        this._lastWidth = this.mainTexture.width;
        this._lastHeight = this.mainTexture.height;
        this.SetVerticesDirty();
      }
      if (!this._userMaterial && Application.isPlaying)
        this.UpdateInternalMaterial();
      if ((UnityEngine.Object) this.material != (UnityEngine.Object) null && (UnityEngine.Object) this._mediaPlayer != (UnityEngine.Object) null)
      {
        if (this.material.HasProperty(DisplayUGUI._propUseYpCbCr) && this._mediaPlayer.TextureProducer != null && this._mediaPlayer.TextureProducer.GetTextureCount() == 2)
        {
          this.material.EnableKeyword("USE_YPCBCR");
          this.material.SetTexture(DisplayUGUI._propChromaTex, this._mediaPlayer.TextureProducer.GetTexture(1));
        }
        if (this.material.HasProperty(DisplayUGUI._propAlphaPack))
        {
          Helper.SetupAlphaPackedMaterial(this.material, this._mediaPlayer.m_AlphaPacking);
          if (this._flipY && this._mediaPlayer.m_AlphaPacking != AlphaPacking.None)
            this.material.SetFloat(DisplayUGUI._propVertScale, -1f);
          else
            this.material.SetFloat(DisplayUGUI._propVertScale, 1f);
        }
        if (this.material.HasProperty(DisplayUGUI._propStereo))
          Helper.SetupStereoMaterial(this.material, this._mediaPlayer.m_StereoPacking, this._mediaPlayer.m_DisplayDebugStereoColorTint);
        DisplayUGUI._propApplyGamma |= 0;
      }
      this.SetMaterialDirty();
    }

    public MediaPlayer CurrentMediaPlayer
    {
      get
      {
        return this._mediaPlayer;
      }
      set
      {
        if (!((UnityEngine.Object) this._mediaPlayer != (UnityEngine.Object) value))
          return;
        this._mediaPlayer = value;
        this.SetMaterialDirty();
      }
    }

    public Rect uvRect
    {
      get
      {
        return this.m_UVRect;
      }
      set
      {
        if (this.m_UVRect == value)
          return;
        this.m_UVRect = value;
        this.SetVerticesDirty();
      }
    }

    [ContextMenu("Set Native Size")]
    public override void SetNativeSize()
    {
      Texture mainTexture = this.mainTexture;
      if (!((UnityEngine.Object) mainTexture != (UnityEngine.Object) null))
        return;
      int num1 = Mathf.RoundToInt((float) mainTexture.width * this.uvRect.width);
      int num2 = Mathf.RoundToInt((float) mainTexture.height * this.uvRect.height);
      if ((UnityEngine.Object) this._mediaPlayer != (UnityEngine.Object) null)
      {
        if (this._mediaPlayer.m_AlphaPacking == AlphaPacking.LeftRight || this._mediaPlayer.m_StereoPacking == StereoPacking.LeftRight)
          num1 /= 2;
        else if (this._mediaPlayer.m_AlphaPacking == AlphaPacking.TopBottom || this._mediaPlayer.m_StereoPacking == StereoPacking.TopBottom)
          num2 /= 2;
      }
      this.rectTransform.anchorMax = this.rectTransform.anchorMin;
      this.rectTransform.sizeDelta = new Vector2((float) num1, (float) num2);
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
      vh.Clear();
      this._OnFillVBO(this._vertices);
      vh.AddUIVertexStream(this._vertices, DisplayUGUI.QuadIndices);
    }

    [Obsolete("This method is not called from Unity 5.2 and above")]
    protected virtual void OnFillVBO(List<UIVertex> vbo)
    {
      this._OnFillVBO(vbo);
    }

    private void _OnFillVBO(List<UIVertex> vbo)
    {
      this._flipY = false;
      if (this.HasValidTexture())
        this._flipY = this._mediaPlayer.TextureProducer.RequiresVerticalFlip();
      Rect uvRect = this.m_UVRect;
      Vector4 drawingDimensions = this.GetDrawingDimensions(this._scaleMode, ref uvRect);
      vbo.Clear();
      UIVertex simpleVert = UIVertex.simpleVert;
      simpleVert.color = (Color32) this.color;
      simpleVert.position = (Vector3) new Vector2(drawingDimensions.x, drawingDimensions.y);
      simpleVert.uv0 = new Vector2(uvRect.xMin, uvRect.yMin);
      if (this._flipY)
        simpleVert.uv0 = new Vector2(uvRect.xMin, 1f - uvRect.yMin);
      vbo.Add(simpleVert);
      simpleVert.position = (Vector3) new Vector2(drawingDimensions.x, drawingDimensions.w);
      simpleVert.uv0 = new Vector2(uvRect.xMin, uvRect.yMax);
      if (this._flipY)
        simpleVert.uv0 = new Vector2(uvRect.xMin, 1f - uvRect.yMax);
      vbo.Add(simpleVert);
      simpleVert.position = (Vector3) new Vector2(drawingDimensions.z, drawingDimensions.w);
      simpleVert.uv0 = new Vector2(uvRect.xMax, uvRect.yMax);
      if (this._flipY)
        simpleVert.uv0 = new Vector2(uvRect.xMax, 1f - uvRect.yMax);
      vbo.Add(simpleVert);
      simpleVert.position = (Vector3) new Vector2(drawingDimensions.z, drawingDimensions.y);
      simpleVert.uv0 = new Vector2(uvRect.xMax, uvRect.yMin);
      if (this._flipY)
        simpleVert.uv0 = new Vector2(uvRect.xMax, 1f - uvRect.yMin);
      vbo.Add(simpleVert);
    }

    private Vector4 GetDrawingDimensions(UnityEngine.ScaleMode scaleMode, ref Rect uvRect)
    {
      Vector4 vector4_1 = Vector4.zero;
      if ((UnityEngine.Object) this.mainTexture != (UnityEngine.Object) null)
      {
        Vector4 zero = Vector4.zero;
        Vector2 vector2 = new Vector2((float) this.mainTexture.width, (float) this.mainTexture.height);
        if ((UnityEngine.Object) this._mediaPlayer != (UnityEngine.Object) null)
        {
          if (this._mediaPlayer.m_AlphaPacking == AlphaPacking.LeftRight || this._mediaPlayer.m_StereoPacking == StereoPacking.LeftRight)
            vector2.x /= 2f;
          else if (this._mediaPlayer.m_AlphaPacking == AlphaPacking.TopBottom || this._mediaPlayer.m_StereoPacking == StereoPacking.TopBottom)
            vector2.y /= 2f;
        }
        Rect pixelAdjustedRect = this.GetPixelAdjustedRect();
        int num1 = Mathf.RoundToInt(vector2.x);
        int num2 = Mathf.RoundToInt(vector2.y);
        Vector4 vector4_2 = new Vector4(zero.x / (float) num1, zero.y / (float) num2, ((float) num1 - zero.z) / (float) num1, ((float) num2 - zero.w) / (float) num2);
        if ((double) vector2.sqrMagnitude > 0.0)
        {
          switch (scaleMode)
          {
            case UnityEngine.ScaleMode.ScaleAndCrop:
              float num3 = vector2.x / vector2.y;
              float num4 = pixelAdjustedRect.width / pixelAdjustedRect.height;
              if ((double) num4 > (double) num3)
              {
                float height = num3 / num4;
                uvRect = new Rect(0.0f, (float) ((1.0 - (double) height) * 0.5), 1f, height);
                break;
              }
              float width1 = num4 / num3;
              uvRect = new Rect((float) (0.5 - (double) width1 * 0.5), 0.0f, width1, 1f);
              break;
            case UnityEngine.ScaleMode.ScaleToFit:
              float num5 = vector2.x / vector2.y;
              float num6 = pixelAdjustedRect.width / pixelAdjustedRect.height;
              if ((double) num5 > (double) num6)
              {
                float height = pixelAdjustedRect.height;
                pixelAdjustedRect.height = pixelAdjustedRect.width * (1f / num5);
                pixelAdjustedRect.y += (height - pixelAdjustedRect.height) * this.rectTransform.pivot.y;
                break;
              }
              float width2 = pixelAdjustedRect.width;
              pixelAdjustedRect.width = pixelAdjustedRect.height * num5;
              pixelAdjustedRect.x += (width2 - pixelAdjustedRect.width) * this.rectTransform.pivot.x;
              break;
          }
        }
        vector4_1 = new Vector4(pixelAdjustedRect.x + pixelAdjustedRect.width * vector4_2.x, pixelAdjustedRect.y + pixelAdjustedRect.height * vector4_2.y, pixelAdjustedRect.x + pixelAdjustedRect.width * vector4_2.z, pixelAdjustedRect.y + pixelAdjustedRect.height * vector4_2.w);
      }
      return vector4_1;
    }
  }
}
