// Decompiled with JetBrains decompiler
// Type: RenderHeads.Media.AVProVideo.DisplayBackground
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace RenderHeads.Media.AVProVideo
{
  [HelpURL("http://renderheads.com/product/avpro-video/")]
  [AddComponentMenu("AVPro Video/Display Background", 200)]
  [ExecuteInEditMode]
  public class DisplayBackground : MonoBehaviour
  {
    public IMediaProducer _source;
    public Texture2D _texture;
    public Material _material;

    private void OnRenderObject()
    {
      if ((Object) this._material == (Object) null || (Object) this._texture == (Object) null)
        return;
      Vector4 vector4 = new Vector4(0.0f, 0.0f, 1f, 1f);
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
