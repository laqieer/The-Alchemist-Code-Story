// Decompiled with JetBrains decompiler
// Type: RenderHeads.Media.AVProVideo.ApplyToMaterial
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace RenderHeads.Media.AVProVideo
{
  [AddComponentMenu("AVPro Video/Apply To Material", 300)]
  [HelpURL("http://renderheads.com/product/avpro-video/")]
  public class ApplyToMaterial : MonoBehaviour
  {
    public Vector2 _offset = Vector2.zero;
    public Vector2 _scale = Vector2.one;
    private Vector2 _originalScale = Vector2.one;
    private Vector2 _originalOffset = Vector2.zero;
    public Material _material;
    public string _texturePropertyName;
    public MediaPlayer _media;
    public Texture2D _defaultTexture;
    private Texture _originalTexture;
    private static int _propStereo;
    private static int _propAlphaPack;
    private static int _propApplyGamma;
    private const string PropChromaTexName = "_ChromaTex";
    private static int _propChromaTex;
    private const string PropUseYpCbCrName = "_UseYpCbCr";
    private static int _propUseYpCbCr;

    private void Awake()
    {
      if (ApplyToMaterial._propStereo == 0 || ApplyToMaterial._propAlphaPack == 0)
      {
        ApplyToMaterial._propStereo = Shader.PropertyToID("Stereo");
        ApplyToMaterial._propAlphaPack = Shader.PropertyToID("AlphaPack");
        ApplyToMaterial._propApplyGamma = Shader.PropertyToID("_ApplyGamma");
      }
      if (ApplyToMaterial._propChromaTex == 0)
        ApplyToMaterial._propChromaTex = Shader.PropertyToID("_ChromaTex");
      if (ApplyToMaterial._propUseYpCbCr != 0)
        return;
      ApplyToMaterial._propUseYpCbCr = Shader.PropertyToID("_UseYpCbCr");
    }

    private void LateUpdate()
    {
      bool flag = false;
      if ((Object) this._media != (Object) null && this._media.TextureProducer != null)
      {
        int textureCount = this._media.TextureProducer.GetTextureCount();
        for (int index = 0; index < textureCount; ++index)
        {
          Texture texture = this._media.TextureProducer.GetTexture(index);
          if ((Object) texture != (Object) null)
          {
            this.ApplyMapping(texture, this._media.TextureProducer.RequiresVerticalFlip(), index);
            flag = true;
          }
        }
      }
      if (flag)
        return;
      this.ApplyMapping((Texture) this._defaultTexture, false, 0);
    }

    private void ApplyMapping(Texture texture, bool requiresYFlip, int plane = 0)
    {
      if (!((Object) this._material != (Object) null))
        return;
      switch (plane)
      {
        case 0:
          if (string.IsNullOrEmpty(this._texturePropertyName))
          {
            this._material.mainTexture = texture;
            if ((Object) texture != (Object) null)
            {
              if (requiresYFlip)
              {
                this._material.mainTextureScale = new Vector2(this._scale.x, -this._scale.y);
                this._material.mainTextureOffset = Vector2.up + this._offset;
                break;
              }
              this._material.mainTextureScale = this._scale;
              this._material.mainTextureOffset = this._offset;
              break;
            }
            break;
          }
          this._material.SetTexture(this._texturePropertyName, texture);
          if ((Object) texture != (Object) null)
          {
            if (requiresYFlip)
            {
              this._material.SetTextureScale(this._texturePropertyName, new Vector2(this._scale.x, -this._scale.y));
              this._material.SetTextureOffset(this._texturePropertyName, Vector2.up + this._offset);
              break;
            }
            this._material.SetTextureScale(this._texturePropertyName, this._scale);
            this._material.SetTextureOffset(this._texturePropertyName, this._offset);
            break;
          }
          break;
        case 1:
          if (this._material.HasProperty(ApplyToMaterial._propUseYpCbCr))
            this._material.EnableKeyword("USE_YPCBCR");
          if (this._material.HasProperty(ApplyToMaterial._propChromaTex))
          {
            this._material.SetTexture(ApplyToMaterial._propChromaTex, texture);
            if ((Object) texture != (Object) null)
            {
              if (requiresYFlip)
              {
                this._material.SetTextureScale(ApplyToMaterial._propChromaTex, new Vector2(this._scale.x, -this._scale.y));
                this._material.SetTextureOffset(ApplyToMaterial._propChromaTex, Vector2.up + this._offset);
                break;
              }
              this._material.SetTextureScale(ApplyToMaterial._propChromaTex, this._scale);
              this._material.SetTextureOffset(ApplyToMaterial._propChromaTex, this._offset);
              break;
            }
            break;
          }
          break;
      }
      if (!((Object) this._media != (Object) null))
        return;
      if (this._material.HasProperty(ApplyToMaterial._propStereo))
        Helper.SetupStereoMaterial(this._material, this._media.m_StereoPacking, this._media.m_DisplayDebugStereoColorTint);
      if (this._material.HasProperty(ApplyToMaterial._propAlphaPack))
        Helper.SetupAlphaPackedMaterial(this._material, this._media.m_AlphaPacking);
      if (!this._material.HasProperty(ApplyToMaterial._propApplyGamma) || this._media.Info == null)
        return;
      Helper.SetupGammaMaterial(this._material, this._media.Info.PlayerSupportsLinearColorSpace());
    }

    private void OnEnable()
    {
      if (string.IsNullOrEmpty(this._texturePropertyName))
      {
        this._originalTexture = this._material.mainTexture;
        this._originalScale = this._material.mainTextureScale;
        this._originalOffset = this._material.mainTextureOffset;
      }
      else
      {
        this._originalTexture = this._material.GetTexture(this._texturePropertyName);
        this._originalScale = this._material.GetTextureScale(this._texturePropertyName);
        this._originalOffset = this._material.GetTextureOffset(this._texturePropertyName);
      }
      this.LateUpdate();
    }

    private void OnDisable()
    {
      if (string.IsNullOrEmpty(this._texturePropertyName))
      {
        this._material.mainTexture = this._originalTexture;
        this._material.mainTextureScale = this._originalScale;
        this._material.mainTextureOffset = this._originalOffset;
      }
      else
      {
        this._material.SetTexture(this._texturePropertyName, this._originalTexture);
        this._material.SetTextureScale(this._texturePropertyName, this._originalScale);
        this._material.SetTextureOffset(this._texturePropertyName, this._originalOffset);
      }
    }
  }
}
