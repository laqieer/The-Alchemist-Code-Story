// Decompiled with JetBrains decompiler
// Type: SRPG.CharacterRenderEffect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [RequireComponent(typeof (Camera))]
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
      if (!Object.op_Inequality((Object) this.RenderMaterial, (Object) null))
        return;
      Graphics.Blit((Texture) src, dest, this.RenderMaterial);
    }
  }
}
