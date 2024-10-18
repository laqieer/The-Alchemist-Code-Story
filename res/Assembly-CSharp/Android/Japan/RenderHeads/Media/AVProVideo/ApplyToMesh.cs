// Decompiled with JetBrains decompiler
// Type: RenderHeads.Media.AVProVideo.ApplyToMesh
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace RenderHeads.Media.AVProVideo
{
  [AddComponentMenu("AVPro Video/Apply To Mesh", 300)]
  [HelpURL("http://renderheads.com/product/avpro-video/")]
  public class ApplyToMesh : MonoBehaviour
  {
    [SerializeField]
    private string _texturePropertyName = "_MainTex";
    [SerializeField]
    private Vector2 _offset = Vector2.zero;
    [SerializeField]
    private Vector2 _scale = Vector2.one;
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
    private bool _isDirty;
    private Texture _lastTextureApplied;
    private int _propTexture;
    private static int _propStereo;
    private static int _propAlphaPack;
    private static int _propApplyGamma;
    private const string PropChromaTexName = "_ChromaTex";
    private static int _propChromaTex;
    private const string PropUseYpCbCrName = "_UseYpCbCr";
    private static int _propUseYpCbCr;

    public MediaPlayer Player
    {
      get
      {
        return this._media;
      }
      set
      {
        if (!((Object) this._media != (Object) value))
          return;
        this._media = value;
        this._isDirty = true;
      }
    }

    public Texture2D DefaultTexture
    {
      get
      {
        return this._defaultTexture;
      }
      set
      {
        if (!((Object) this._defaultTexture != (Object) value))
          return;
        this._defaultTexture = value;
        this._isDirty = true;
      }
    }

    public Renderer MeshRenderer
    {
      get
      {
        return this._mesh;
      }
      set
      {
        if (!((Object) this._mesh != (Object) value))
          return;
        this._mesh = value;
        this._isDirty = true;
      }
    }

    public string TexturePropertyName
    {
      get
      {
        return this._texturePropertyName;
      }
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
      get
      {
        return this._offset;
      }
      set
      {
        if (!(this._offset != value))
          return;
        this._offset = value;
        this._isDirty = true;
      }
    }

    public Vector2 Scale
    {
      get
      {
        return this._scale;
      }
      set
      {
        if (!(this._scale != value))
          return;
        this._scale = value;
        this._isDirty = true;
      }
    }

    public void ForceUpdate()
    {
      this._isDirty = true;
      this.LateUpdate();
    }

    private void Awake()
    {
      if (ApplyToMesh._propStereo == 0 || ApplyToMesh._propAlphaPack == 0)
      {
        ApplyToMesh._propStereo = Shader.PropertyToID("Stereo");
        ApplyToMesh._propAlphaPack = Shader.PropertyToID("AlphaPack");
        ApplyToMesh._propApplyGamma = Shader.PropertyToID("_ApplyGamma");
      }
      if (ApplyToMesh._propChromaTex == 0)
        ApplyToMesh._propChromaTex = Shader.PropertyToID("_ChromaTex");
      if (ApplyToMesh._propUseYpCbCr != 0)
        return;
      ApplyToMesh._propUseYpCbCr = Shader.PropertyToID("_UseYpCbCr");
    }

    private void LateUpdate()
    {
      bool flag = false;
      if ((Object) this._media != (Object) null && this._media.TextureProducer != null)
      {
        Texture texture1 = this._media.TextureProducer.GetTexture(0);
        if ((Object) texture1 != (Object) null)
        {
          if ((Object) texture1 != (Object) this._lastTextureApplied)
            this._isDirty = true;
          if (this._isDirty)
          {
            int textureCount = this._media.TextureProducer.GetTextureCount();
            for (int index = 0; index < textureCount; ++index)
            {
              Texture texture2 = this._media.TextureProducer.GetTexture(index);
              if ((Object) texture2 != (Object) null)
                this.ApplyMapping(texture2, this._media.TextureProducer.RequiresVerticalFlip(), index);
            }
          }
          flag = true;
        }
      }
      if (flag)
        return;
      if ((Object) this._defaultTexture != (Object) this._lastTextureApplied)
        this._isDirty = true;
      if (!this._isDirty)
        return;
      this.ApplyMapping((Texture) this._defaultTexture, false, 0);
    }

    private void ApplyMapping(Texture texture, bool requiresYFlip, int plane = 0)
    {
      if (!((Object) this._mesh != (Object) null))
        return;
      this._isDirty = false;
      Material[] materials = this._mesh.materials;
      if (materials == null)
        return;
      for (int index = 0; index < materials.Length; ++index)
      {
        Material material = materials[index];
        if ((Object) material != (Object) null)
        {
          switch (plane)
          {
            case 0:
              material.SetTexture(this._propTexture, texture);
              this._lastTextureApplied = texture;
              if ((Object) texture != (Object) null)
              {
                if (requiresYFlip)
                {
                  material.SetTextureScale(this._propTexture, new Vector2(this._scale.x, -this._scale.y));
                  material.SetTextureOffset(this._propTexture, Vector2.up + this._offset);
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
                if (requiresYFlip)
                {
                  material.SetTextureScale(ApplyToMesh._propChromaTex, new Vector2(this._scale.x, -this._scale.y));
                  material.SetTextureOffset(ApplyToMesh._propChromaTex, Vector2.up + this._offset);
                  break;
                }
                material.SetTextureScale(ApplyToMesh._propChromaTex, this._scale);
                material.SetTextureOffset(ApplyToMesh._propChromaTex, this._offset);
                break;
              }
              break;
          }
          if ((Object) this._media != (Object) null)
          {
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
      if ((Object) this._mesh == (Object) null)
      {
        this._mesh = (Renderer) this.GetComponent<UnityEngine.MeshRenderer>();
        if ((Object) this._mesh == (Object) null)
          Debug.LogWarning((object) "[AVProVideo] No mesh renderer set or found in gameobject");
      }
      this._propTexture = Shader.PropertyToID(this._texturePropertyName);
      this._isDirty = true;
      if (!((Object) this._mesh != (Object) null))
        return;
      this.LateUpdate();
    }

    private void OnDisable()
    {
      this.ApplyMapping((Texture) this._defaultTexture, false, 0);
    }
  }
}
