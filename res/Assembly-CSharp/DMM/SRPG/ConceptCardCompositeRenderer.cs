// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardCompositeRenderer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class ConceptCardCompositeRenderer : MonoBehaviour
  {
    [SerializeField]
    private RawImage_Transparent Image;
    [SerializeField]
    private GameObject Message;
    [SerializeField]
    private Text MessageText;
    [SerializeField]
    private GameObject OverlayImageTemplate;
    [SerializeField]
    private Camera Camera;
    private RenderTexture mRenderTexture;

    public RenderTexture RenderTexture
    {
      get
      {
        if (Object.op_Equality((Object) this.mRenderTexture, (Object) null))
        {
          if (Object.op_Equality((Object) this.Camera, (Object) null))
            return (RenderTexture) null;
          this.mRenderTexture = RenderTexture.GetTemporary(1024, 1024, 24, (RenderTextureFormat) 0, (RenderTextureReadWrite) 1);
          this.mRenderTexture.antiAliasing = 1;
          ((Texture) this.mRenderTexture).dimension = (TextureDimension) 2;
          ((Texture) this.mRenderTexture).wrapMode = (TextureWrapMode) 1;
          ((Texture) this.mRenderTexture).filterMode = (FilterMode) 1;
          this.mRenderTexture.autoGenerateMips = false;
          this.Camera.targetTexture = this.mRenderTexture;
        }
        return this.mRenderTexture;
      }
    }

    public void Setup(ConceptCardParam param)
    {
      if (Object.op_Inequality((Object) this.OverlayImageTemplate, (Object) null))
        this.OverlayImageTemplate.gameObject.SetActive(false);
      if (Object.op_Inequality((Object) this.Message, (Object) null))
        this.Message.gameObject.SetActive(false);
      if (!Object.op_Inequality((Object) this.Image, (Object) null) || param == null)
        return;
      ConceptCardUnitImageSettings.ComposeUnitConceptCardImage(param, (RawImage) this.Image, this.OverlayImageTemplate, this.Message, this.MessageText);
    }

    private void OnDestroy()
    {
      if (!Object.op_Inequality((Object) this.mRenderTexture, (Object) null))
        return;
      RenderTexture.ReleaseTemporary(this.mRenderTexture);
      this.mRenderTexture = (RenderTexture) null;
    }
  }
}
