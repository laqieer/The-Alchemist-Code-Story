// Decompiled with JetBrains decompiler
// Type: SRPG.CharacterRenderEffect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [RequireComponent(typeof (UnityEngine.Camera))]
  [ExecuteInEditMode]
  public class CharacterRenderEffect : MonoBehaviour
  {
    public Material RenderMaterial;

    private void OnPreRender()
    {
      Shader.DisableKeyword("ALPHA_EMISSIVE");
      Shader.EnableKeyword("ALPHA_DEPTH");
    }

    private void OnPostRender()
    {
      Shader.EnableKeyword("ALPHA_EMISSIVE");
      Shader.DisableKeyword("ALPHA_DEPTH");
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
      if (!((UnityEngine.Object) this.RenderMaterial != (UnityEngine.Object) null))
        return;
      Graphics.Blit((Texture) src, dest, this.RenderMaterial);
    }
  }
}
