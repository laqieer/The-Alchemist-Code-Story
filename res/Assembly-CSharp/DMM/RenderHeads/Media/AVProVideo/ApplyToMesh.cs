// Decompiled with JetBrains decompiler
// Type: RenderHeads.Media.AVProVideo.ApplyToMesh
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.Events;

#nullable disable
namespace RenderHeads.Media.AVProVideo
{
  [AddComponentMenu("AVPro Video/Apply To Mesh", 300)]
  [HelpURL("http://renderheads.com/product/avpro-video/")]
  public class ApplyToMesh : MonoBehaviour
  {
    [Header("Media Source")]
    [SerializeField]
    private MediaPlayer _media;
    [Tooltip("Default texture to display when the video texture is preparing")]
    [SerializeField]
    private Texture2D _defaultTexture;
    [Space(8f)]
    [Header("Renderer Target")]
    [SerializeField]
    private Renderer _mesh;
    [SerializeField]
    private string _texturePropertyName = "_MainTex";
    [SerializeField]
    private Vector2 _offset = Vector2.zero;
    [SerializeField]
    private Vector2 _scale = Vector2.one;
    private bool _isDirty;
    private Texture _lastTextureApplied;
    private int _propTexture;
    private static int _propStereo;
    private static int _propAlphaPack;
    private static int _propApplyGamma;
    private static int _propLayout;
    private const string PropChromaTexName = "_ChromaTex";
    private static int _propChromaTex;
    private const string PropYpCbCrTransformName = "_YpCbCrTransform";
    private static int _propYpCbCrTransform;
    private const string PropUseYpCbCrName = "_UseYpCbCr";
    private static int _propUseYpCbCr;

    public MediaPlayer Player
    {
      get => this._media;
      set => this.ChangeMediaPlayer(value);
    }

    public Texture2D DefaultTexture
    {
      get => this._defaultTexture;
      set
      {
        if (!Object.op_Inequality((Object) this._defaultTexture, (Object) value))
          return;
        this._defaultTexture = value;
        this._isDirty = true;
      }
    }

    public Renderer MeshRenderer
    {
      get => this._mesh;
      set
      {
        if (!Object.op_Inequality((Object) this._mesh, (Object) value))
          return;
        this._mesh = value;
        this._isDirty = true;
      }
    }

    public string TexturePropertyName
    {
      get => this._texturePropertyName;
      set
      {
        if (!(this._texturePropertyName != value))
          return;
        this._texturePropertyName = value;
        this._propTexture = Shader.PropertyToID(this._texturePropertyName);
        this._isDirty = true;
      }
    }

    public Vector2 Offset
    {
      get => this._offset;
      set
      {
        if (!Vector2.op_Inequality(this._offset, value))
          return;
        this._offset = value;
        this._isDirty = true;
      }
    }

    public Vector2 Scale
    {
      get => this._scale;
      set
      {
        if (!Vector2.op_Inequality(this._scale, value))
          return;
        this._scale = value;
        this._isDirty = true;
      }
    }

    private void Awake()
    {
      if (ApplyToMesh._propStereo == 0)
        ApplyToMesh._propStereo = Shader.PropertyToID("Stereo");
      if (ApplyToMesh._propAlphaPack == 0)
        ApplyToMesh._propAlphaPack = Shader.PropertyToID("AlphaPack");
      if (ApplyToMesh._propApplyGamma == 0)
        ApplyToMesh._propApplyGamma = Shader.PropertyToID("_ApplyGamma");
      if (ApplyToMesh._propLayout == 0)
        ApplyToMesh._propLayout = Shader.PropertyToID("Layout");
      if (ApplyToMesh._propChromaTex == 0)
        ApplyToMesh._propChromaTex = Shader.PropertyToID("_ChromaTex");
      if (ApplyToMesh._propYpCbCrTransform == 0)
        ApplyToMesh._propYpCbCrTransform = Shader.PropertyToID("_YpCbCrTransform");
      if (ApplyToMesh._propUseYpCbCr == 0)
        ApplyToMesh._propUseYpCbCr = Shader.PropertyToID("_UseYpCbCr");
      if (!Object.op_Inequality((Object) this._media, (Object) null))
        return;
      // ISSUE: method pointer
      this._media.Events.AddListener(new UnityAction<MediaPlayer, MediaPlayerEvent.EventType, ErrorCode>((object) this, __methodptr(OnMediaPlayerEvent)));
    }

    public void ForceUpdate()
    {
      this._isDirty = true;
      this.LateUpdate();
    }

    private void OnMediaPlayerEvent(
      MediaPlayer mp,
      MediaPlayerEvent.EventType et,
      ErrorCode errorCode)
    {
      if (et != MediaPlayerEvent.EventType.FirstFrameReady && et != MediaPlayerEvent.EventType.PropertiesChanged)
        return;
      this.ForceUpdate();
    }

    private void ChangeMediaPlayer(MediaPlayer player)
    {
      if (!Object.op_Inequality((Object) this._media, (Object) player))
        return;
      if (Object.op_Inequality((Object) this._media, (Object) null))
      {
        // ISSUE: method pointer
        this._media.Events.RemoveListener(new UnityAction<MediaPlayer, MediaPlayerEvent.EventType, ErrorCode>((object) this, __methodptr(OnMediaPlayerEvent)));
      }
      this._media = player;
      if (Object.op_Inequality((Object) this._media, (Object) null))
      {
        // ISSUE: method pointer
        this._media.Events.AddListener(new UnityAction<MediaPlayer, MediaPlayerEvent.EventType, ErrorCode>((object) this, __methodptr(OnMediaPlayerEvent)));
      }
      this._isDirty = true;
    }

    private void LateUpdate()
    {
      bool flag = false;
      if (Object.op_Inequality((Object) this._media, (Object) null) && this._media.TextureProducer != null)
      {
        Texture texture1 = this._media.FrameResampler == null || this._media.FrameResampler.OutputTexture == null ? (Texture) null : this._media.FrameResampler.OutputTexture[0];
        Texture texture2 = !this._media.m_Resample ? this._media.TextureProducer.GetTexture() : texture1;
        if (Object.op_Inequality((Object) texture2, (Object) null))
        {
          if (Object.op_Inequality((Object) texture2, (Object) this._lastTextureApplied))
            this._isDirty = true;
          if (this._isDirty)
          {
            int num = !this._media.m_Resample ? this._media.TextureProducer.GetTextureCount() : 1;
            for (int index = 0; index < num; ++index)
            {
              Texture texture3 = this._media.FrameResampler == null || this._media.FrameResampler.OutputTexture == null ? (Texture) null : this._media.FrameResampler.OutputTexture[index];
              Texture texture4 = !this._media.m_Resample ? this._media.TextureProducer.GetTexture(index) : texture3;
              if (Object.op_Inequality((Object) texture4, (Object) null))
                this.ApplyMapping(texture4, this._media.TextureProducer.RequiresVerticalFlip(), index);
            }
          }
          flag = true;
        }
      }
      if (flag)
        return;
      if (Object.op_Inequality((Object) this._defaultTexture, (Object) this._lastTextureApplied))
        this._isDirty = true;
      if (!this._isDirty)
        return;
      this.ApplyMapping((Texture) this._defaultTexture, false);
    }

    private void ApplyMapping(Texture texture, bool requiresYFlip, int plane = 0)
    {
      if (!Object.op_Inequality((Object) this._mesh, (Object) null))
        return;
      this._isDirty = false;
      Material[] materials = this._mesh.materials;
      if (materials == null)
        return;
      for (int index = 0; index < materials.Length; ++index)
      {
        Material material = materials[index];
        if (Object.op_Inequality((Object) material, (Object) null))
        {
          switch (plane)
          {
            case 0:
              material.SetTexture(this._propTexture, texture);
              this._lastTextureApplied = texture;
              if (Object.op_Inequality((Object) texture, (Object) null))
              {
                if (requiresYFlip)
                {
                  material.SetTextureScale(this._propTexture, new Vector2(this._scale.x, -this._scale.y));
                  material.SetTextureOffset(this._propTexture, Vector2.op_Addition(Vector2.up, this._offset));
                  break;
                }
                material.SetTextureScale(this._propTexture, this._scale);
                material.SetTextureOffset(this._propTexture, this._offset);
                break;
              }
              break;
            case 1:
              if (material.HasProperty(ApplyToMesh._propUseYpCbCr) && material.HasProperty(ApplyToMesh._propChromaTex))
              {
                material.EnableKeyword("USE_YPCBCR");
                material.SetTexture(ApplyToMesh._propChromaTex, texture);
                material.SetMatrix(ApplyToMesh._propYpCbCrTransform, this._media.TextureProducer.GetYpCbCrTransform());
                if (requiresYFlip)
                {
                  material.SetTextureScale(ApplyToMesh._propChromaTex, new Vector2(this._scale.x, -this._scale.y));
                  material.SetTextureOffset(ApplyToMesh._propChromaTex, Vector2.op_Addition(Vector2.up, this._offset));
                  break;
                }
                material.SetTextureScale(ApplyToMesh._propChromaTex, this._scale);
                material.SetTextureOffset(ApplyToMesh._propChromaTex, this._offset);
                break;
              }
              break;
          }
          if (Object.op_Inequality((Object) this._media, (Object) null))
          {
            if (material.HasProperty(ApplyToMesh._propLayout))
              Helper.SetupLayoutMaterial(material, this._media.VideoLayoutMapping);
            if (material.HasProperty(ApplyToMesh._propStereo))
              Helper.SetupStereoMaterial(material, this._media.m_StereoPacking, this._media.m_DisplayDebugStereoColorTint);
            if (material.HasProperty(ApplyToMesh._propAlphaPack))
              Helper.SetupAlphaPackedMaterial(material, this._media.m_AlphaPacking);
            if (material.HasProperty(ApplyToMesh._propApplyGamma) && this._media.Info != null)
              Helper.SetupGammaMaterial(material, this._media.Info.PlayerSupportsLinearColorSpace());
          }
        }
      }
    }

    private void OnEnable()
    {
      if (Object.op_Equality((Object) this._mesh, (Object) null))
      {
        this._mesh = (Renderer) ((Component) this).GetComponent<UnityEngine.MeshRenderer>();
        if (Object.op_Equality((Object) this._mesh, (Object) null))
          Debug.LogWarning((object) "[AVProVideo] No mesh renderer set or found in gameobject");
      }
      this._propTexture = Shader.PropertyToID(this._texturePropertyName);
      this._isDirty = true;
      if (!Object.op_Inequality((Object) this._mesh, (Object) null))
        return;
      this.LateUpdate();
    }

    private void OnDisable() => this.ApplyMapping((Texture) this._defaultTexture, false);
  }
}
