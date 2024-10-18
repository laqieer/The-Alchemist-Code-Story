// Decompiled with JetBrains decompiler
// Type: RadialBlurEffect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
[ExecuteInEditMode]
public class RadialBlurEffect : MonoBehaviour
{
  public Material BlurMaterial;
  [Range(0.0f, 1f)]
  public float Strength;
  public Vector2 Focus = new Vector2(0.5f, 0.5f);

  private void OnRenderImage(RenderTexture src, RenderTexture dest)
  {
    if (Object.op_Inequality((Object) this.BlurMaterial, (Object) null) && (double) this.Strength > 0.0)
    {
      this.BlurMaterial.SetVector("_focus", Vector4.op_Implicit(this.Focus));
      this.BlurMaterial.SetFloat("_strength", this.Strength);
      Graphics.Blit((Texture) src, dest, this.BlurMaterial);
    }
    else
      Graphics.Blit((Texture) src, dest);
  }
}
