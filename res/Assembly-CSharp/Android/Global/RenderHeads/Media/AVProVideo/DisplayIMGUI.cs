// Decompiled with JetBrains decompiler
// Type: RenderHeads.Media.AVProVideo.DisplayIMGUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace RenderHeads.Media.AVProVideo
{
  [HelpURL("http://renderheads.com/product/avpro-video/")]
  [AddComponentMenu("AVPro Video/Display IMGUI", 200)]
  [ExecuteInEditMode]
  public class DisplayIMGUI : MonoBehaviour
  {
    public bool _displayInEditor = true;
    public ScaleMode _scaleMode = ScaleMode.ScaleToFit;
    public Color _color = Color.white;
    public bool _fullScreen = true;
    [Range(0.0f, 1f)]
    public float _width = 1f;
    [Range(0.0f, 1f)]
    public float _height = 1f;
    public MediaPlayer _mediaPlayer;
    public bool _alphaBlend;
    [SerializeField]
    private bool _useDepth;
    public int _depth;
    [Range(0.0f, 1f)]
    public float _x;
    [Range(0.0f, 1f)]
    public float _y;
    private static int _propAlphaPack;
    private static int _propVertScale;
    private static int _propApplyGamma;
    private static Shader _shaderAlphaPacking;
    private Material _material;

    private void Awake()
    {
      if (DisplayIMGUI._propAlphaPack != 0)
        return;
      DisplayIMGUI._propAlphaPack = Shader.PropertyToID("AlphaPack");
      DisplayIMGUI._propVertScale = Shader.PropertyToID("_VertScale");
      DisplayIMGUI._propApplyGamma = Shader.PropertyToID("_ApplyGamma");
    }

    private void Start()
    {
      if (!this._useDepth)
        this.useGUILayout = false;
      if (!((Object) DisplayIMGUI._shaderAlphaPacking == (Object) null))
        return;
      DisplayIMGUI._shaderAlphaPacking = Shader.Find("AVProVideo/IMGUI/Texture Transparent");
      if (!((Object) DisplayIMGUI._shaderAlphaPacking == (Object) null))
        return;
      Debug.LogWarning((object) "[AVProVideo] Missing shader AVProVideo/IMGUI/Transparent Packed");
    }

    private void OnDestroy()
    {
      if (!((Object) this._material != (Object) null))
        return;
      Object.Destroy((Object) this._material);
      this._material = (Material) null;
    }

    private Shader GetRequiredShader()
    {
      Shader shader = (Shader) null;
      switch (this._mediaPlayer.m_AlphaPacking)
      {
        case AlphaPacking.TopBottom:
        case AlphaPacking.LeftRight:
          shader = DisplayIMGUI._shaderAlphaPacking;
          break;
      }
      if ((Object) shader == (Object) null && this._mediaPlayer.TextureProducer != null && this._mediaPlayer.TextureProducer.GetTextureCount() == 2)
        shader = DisplayIMGUI._shaderAlphaPacking;
      return shader;
    }

    private void Update()
    {
      if (!((Object) this._mediaPlayer != (Object) null))
        return;
      Shader shader = (Shader) null;
      if ((Object) this._material != (Object) null)
        shader = this._material.shader;
      Shader requiredShader = this.GetRequiredShader();
      if ((Object) shader != (Object) requiredShader)
      {
        if ((Object) this._material != (Object) null)
        {
          Object.Destroy((Object) this._material);
          this._material = (Material) null;
        }
        if ((Object) requiredShader != (Object) null)
          this._material = new Material(requiredShader);
      }
      if (!((Object) this._material != (Object) null))
        return;
      if (this._material.HasProperty(DisplayIMGUI._propAlphaPack))
        Helper.SetupAlphaPackedMaterial(this._material, this._mediaPlayer.m_AlphaPacking);
      DisplayIMGUI._propApplyGamma |= 0;
    }

    private void OnGUI()
    {
      if ((Object) this._mediaPlayer == (Object) null)
        return;
      bool flag = false;
      Texture texture = (Texture) null;
      if (!this._displayInEditor)
        ;
      if (this._mediaPlayer.Info != null && !this._mediaPlayer.Info.HasVideo())
        texture = (Texture) null;
      if (this._mediaPlayer.TextureProducer != null)
      {
        if ((Object) this._mediaPlayer.TextureProducer.GetTexture(0) != (Object) null)
        {
          texture = this._mediaPlayer.TextureProducer.GetTexture(0);
          flag = this._mediaPlayer.TextureProducer.RequiresVerticalFlip();
        }
        if (this._mediaPlayer.TextureProducer.GetTextureCount() == 2 && (Object) this._material != (Object) null)
        {
          this._material.SetTexture("_ChromaTex", this._mediaPlayer.TextureProducer.GetTexture(1));
          this._material.EnableKeyword("USE_YPCBCR");
        }
      }
      if (!((Object) texture != (Object) null) || this._alphaBlend && (double) this._color.a <= 0.0)
        return;
      GUI.depth = this._depth;
      GUI.color = this._color;
      Rect rect = this.GetRect();
      if ((Object) this._material != (Object) null)
      {
        if (flag)
          this._material.SetFloat(DisplayIMGUI._propVertScale, -1f);
        else
          this._material.SetFloat(DisplayIMGUI._propVertScale, 1f);
        Helper.DrawTexture(rect, texture, this._scaleMode, this._mediaPlayer.m_AlphaPacking, this._material);
      }
      else
      {
        if (flag)
          GUIUtility.ScaleAroundPivot(new Vector2(1f, -1f), new Vector2(0.0f, rect.y + rect.height / 2f));
        GUI.DrawTexture(rect, texture, this._scaleMode, this._alphaBlend);
      }
    }

    public Rect GetRect()
    {
      return !this._fullScreen ? new Rect(this._x * (float) (Screen.width - 1), this._y * (float) (Screen.height - 1), this._width * (float) Screen.width, this._height * (float) Screen.height) : new Rect(0.0f, 0.0f, (float) Screen.width, (float) Screen.height);
    }
  }
}
