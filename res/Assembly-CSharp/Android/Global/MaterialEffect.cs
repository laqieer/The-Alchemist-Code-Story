// Decompiled with JetBrains decompiler
// Type: MaterialEffect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

[ExecuteInEditMode]
public class MaterialEffect : MonoBehaviour
{
  public Material Material;

  private void OnRenderImage(RenderTexture src, RenderTexture dest)
  {
    if ((Object) this.Material != (Object) null)
      Graphics.Blit((Texture) src, dest, this.Material);
    else
      Graphics.Blit((Texture) src, dest);
  }
}
