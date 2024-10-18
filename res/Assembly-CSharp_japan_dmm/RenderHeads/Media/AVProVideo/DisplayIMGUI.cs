// Decompiled with JetBrains decompiler
// Type: RenderHeads.Media.AVProVideo.DisplayIMGUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace RenderHeads.Media.AVProVideo
{
  [AddComponentMenu("AVPro Video/Display IMGUI", 200)]
  [HelpURL("http://renderheads.com/product/avpro-video/")]
  [ExecuteInEditMode]
  public class DisplayIMGUI : MonoBehaviour
  {
    private const string PropChromaTexName = "_ChromaTex";
    private const string PropYpCbCrTransformName = "_YpCbCrTransform";
    public MediaPlayer _mediaPlayer;
    public bool _displayInEditor = true;
    public ScaleMode _scaleMode = (ScaleMode) 2;
    public Color _color = Color.white;
    public bool _alphaBlend;
    [SerializeField]
    private bool _useDepth;
    public int _depth;
    public bool _fullScreen = true;
    [Range(0.0f, 1f)]
    public float _x;
    [Range(0.0f, 1f)]
    public float _y;
    [Range(0.0f, 1f)]
    public float _width = 1f;
    [Range(0.0f, 1f)]
    public float _height = 1f;
    private static int _propAlphaPack;
    private static int _propVertScale;
    private static int _propApplyGamma;
    private static int _propChromaTex;
    private static int _propYpCbCrTransform;
    private static Shader _shaderAlphaPacking;
    private Material _material;

    private void Awake()
    {
      if (DisplayIMGUI._propAlphaPack != 0)
        return;
      DisplayIMGUI._propAlphaPack = Shader.PropertyToID("AlphaPack");
      DisplayIMGUI._propVertScale = Shader.PropertyToID("_VertScale");
      DisplayIMGUI._propApplyGamma = Shader.PropertyToID("_ApplyGamma");
      DisplayIMGUI._propChromaTex = Shader.PropertyToID("_ChromaTex");
      DisplayIMGUI._propYpCbCrTransform = Shader.PropertyToID("_YpCbCrTransform");
    }

    private void Start()
    {
      if (!this._useDepth)
        this.useGUILayout = false;
      if (!Object.op_Equality((Object) DisplayIMGUI._shaderAlphaPacking, (Object) null))
        return;
      DisplayIMGUI._shaderAlphaPacking = Shader.Find("AVProVideo/IMGUI/Texture Transparent");
      if (!Object.op_Equality((Object) DisplayIMGUI._shaderAlphaPacking, (Object) null))
        return;
      Debug.LogWarning((object) "[AVProVideo] Missing shader AVProVideo/IMGUI/Transparent Packed");
    }

    private void OnDestroy()
    {
      if (!Object.op_Inequality((Object) this._material, (Object) null))
        return;
      Object.Destroy((Object) this._material);
      this._material = (Material) null;
    }

    private Shader GetRequiredShader()
    {
      Shader requiredShader = (Shader) null;
      switch (this._mediaPlayer.m_AlphaPacking)
      {
        case AlphaPacking.TopBottom:
        case AlphaPacking.LeftRight:
          requiredShader = DisplayIMGUI._shaderAlphaPacking;
          break;
      }
      if (Object.op_Equality((Object) requiredShader, (Object) null) && this._mediaPlayer.Info != null && QualitySettings.activeColorSpace == 1 && this._mediaPlayer.Info.PlayerSupportsLinearColorSpace())
        requiredShader = DisplayIMGUI._shaderAlphaPacking;
      if (Object.op_Equality((Object) requiredShader, (Object) null) && this._mediaPlayer.TextureProducer != null && this._mediaPlayer.TextureProducer.GetTextureCount() == 2)
        requiredShader = DisplayIMGUI._shaderAlphaPacking;
      return requiredShader;
    }

    private void Update()
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
          Object.Destroy((Object) this._material);
          this._material = (Material) null;
        }
        if (Object.op_Inequality((Object) requiredShader, (Object) null))
          this._material = new Material(requiredShader);
      }
      if (!Object.op_Inequality((Object) this._material, (Object) null))
        return;
      if (this._material.HasProperty(DisplayIMGUI._propAlphaPack))
        Helper.SetupAlphaPackedMaterial(this._material, this._mediaPlayer.m_AlphaPacking);
      if (!this._material.HasProperty(DisplayIMGUI._propApplyGamma) || this._mediaPlayer.Info == null)
        return;
      Helper.SetupGammaMaterial(this._material, this._mediaPlayer.Info.PlayerSupportsLinearColorSpace());
    }

    private void OnGUI()
    {
      if (Object.op_Equality((Object) this._mediaPlayer, (Object) null))
        return;
      bool flag = false;
      Texture texture1 = (Texture) null;
      if (!this._displayInEditor)
        ;
      if (this._mediaPlayer.Info != null && !this._mediaPlayer.Info.HasVideo())
        texture1 = (Texture) null;
      if (this._mediaPlayer.TextureProducer != null)
      {
        if (this._mediaPlayer.m_Resample)
        {
          if (this._mediaPlayer.FrameResampler.OutputTexture != null && Object.op_Inequality((Object) this._mediaPlayer.FrameResampler.OutputTexture[0], (Object) null))
          {
            texture1 = this._mediaPlayer.FrameResampler.OutputTexture[0];
            flag = this._mediaPlayer.TextureProducer.RequiresVerticalFlip();
          }
        }
        else if (Object.op_Inequality((Object) this._mediaPlayer.TextureProducer.GetTexture(), (Object) null))
        {
          texture1 = this._mediaPlayer.TextureProducer.GetTexture();
          flag = this._mediaPlayer.TextureProducer.RequiresVerticalFlip();
        }
        if (this._mediaPlayer.TextureProducer.GetTextureCount() == 2 && Object.op_Inequality((Object) this._material, (Object) null))
        {
          Texture texture2 = this._mediaPlayer.FrameResampler == null || this._mediaPlayer.FrameResampler.OutputTexture == null ? (Texture) null : this._mediaPlayer.FrameResampler.OutputTexture[1];
          Texture texture3 = !this._mediaPlayer.m_Resample ? this._mediaPlayer.TextureProducer.GetTexture(1) : texture2;
          this._material.SetTexture(DisplayIMGUI._propChromaTex, texture3);
          this._material.SetMatrix(DisplayIMGUI._propYpCbCrTransform, this._mediaPlayer.TextureProducer.GetYpCbCrTransform());
          this._material.EnableKeyword("USE_YPCBCR");
        }
      }
      if (!Object.op_Inequality((Object) texture1, (Object) null) || this._alphaBlend && (double) this._color.a <= 0.0)
        return;
      GUI.depth = this._depth;
      GUI.color = this._color;
      Rect rect = this.GetRect();
      if (Object.op_Inequality((Object) this._material, (Object) null))
      {
        if (flag)
          this._material.SetFloat(DisplayIMGUI._propVertScale, -1f);
        else
          this._material.SetFloat(DisplayIMGUI._propVertScale, 1f);
        Helper.DrawTexture(rect, texture1, this._scaleMode, this._mediaPlayer.m_AlphaPacking, this._material);
      }
      else
      {
        if (flag)
          GUIUtility.ScaleAroundPivot(new Vector2(1f, -1f), new Vector2(0.0f, ((Rect) ref rect).y + ((Rect) ref rect).height / 2f));
        GUI.DrawTexture(rect, texture1, this._scaleMode, this._alphaBlend);
      }
    }

    public Rect GetRect()
    {
      Rect rect;
      if (this._fullScreen)
      {
        // ISSUE: explicit constructor call
        ((Rect) ref rect).\u002Ector(0.0f, 0.0f, (float) Screen.width, (float) Screen.height);
      }
      else
      {
        // ISSUE: explicit constructor call
        ((Rect) ref rect).\u002Ector(this._x * (float) (Screen.width - 1), this._y * (float) (Screen.height - 1), this._width * (float) Screen.width, this._height * (float) Screen.height);
      }
      return rect;
    }
  }
}
