// Decompiled with JetBrains decompiler
// Type: RenderHeads.Media.AVProVideo.DisplayBackground
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace RenderHeads.Media.AVProVideo
{
  [AddComponentMenu("AVPro Video/Display Background", 200)]
  [HelpURL("http://renderheads.com/product/avpro-video/")]
  [ExecuteInEditMode]
  public class DisplayBackground : MonoBehaviour
  {
    public IMediaProducer _source;
    public Texture2D _texture;
    public Material _material;

    private void OnRenderObject()
    {
      if (Object.op_Equality((Object) this._material, (Object) null) || Object.op_Equality((Object) this._texture, (Object) null))
        return;
      Vector4 vector4;
      // ISSUE: explicit constructor call
      ((Vector4) ref vector4).\u002Ector(0.0f, 0.0f, 1f, 1f);
      this._material.SetPass(0);
      GL.PushMatrix();
      GL.LoadOrtho();
      GL.Begin(7);
      GL.TexCoord2(vector4.x, vector4.y);
      GL.Vertex3(0.0f, 0.0f, 0.1f);
      GL.TexCoord2(vector4.z, vector4.y);
      GL.Vertex3(1f, 0.0f, 0.1f);
      GL.TexCoord2(vector4.z, vector4.w);
      GL.Vertex3(1f, 1f, 0.1f);
      GL.TexCoord2(vector4.x, vector4.w);
      GL.Vertex3(0.0f, 1f, 0.1f);
      GL.End();
      GL.PopMatrix();
    }
  }
}
