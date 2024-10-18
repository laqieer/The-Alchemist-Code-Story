// Decompiled with JetBrains decompiler
// Type: RadialBlurEffect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[ExecuteInEditMode]
public class RadialBlurEffect : MonoBehaviour
{
  public Vector2 Focus = new Vector2(0.5f, 0.5f);
  public Material BlurMaterial;
  [Range(0.0f, 1f)]
  public float Strength;

  private void OnRenderImage(RenderTexture src, RenderTexture dest)
  {
    if ((Object) this.BlurMaterial != (Object) null && (double) this.Strength > 0.0)
    {
      this.BlurMaterial.SetVector("_focus", (Vector4) this.Focus);
      this.BlurMaterial.SetFloat("_strength", this.Strength);
      Graphics.Blit((Texture) src, dest, this.BlurMaterial);
    }
    else
      Graphics.Blit((Texture) src, dest);
  }
}
