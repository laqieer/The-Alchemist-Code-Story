// Decompiled with JetBrains decompiler
// Type: MaterialEffect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
[ExecuteInEditMode]
public class MaterialEffect : MonoBehaviour
{
  public Material Material;

  private void OnRenderImage(RenderTexture src, RenderTexture dest)
  {
    if (Object.op_Inequality((Object) this.Material, (Object) null))
      Graphics.Blit((Texture) src, dest, this.Material);
    else
      Graphics.Blit((Texture) src, dest);
  }
}
