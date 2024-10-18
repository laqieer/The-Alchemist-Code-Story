// Decompiled with JetBrains decompiler
// Type: SRPG.RawPolyImage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class RawPolyImage : RawImage
  {
    public Quad[] Quads = new Quad[0];
    public bool Transparent;
    public string Preview;
    private RectTransform mRectTransform;

    protected override void Awake()
    {
      base.Awake();
      this.mRectTransform = this.GetComponent<RectTransform>();
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
      vh.Clear();
      if ((UnityEngine.Object) this.texture == (UnityEngine.Object) null && this.Transparent || (double) this.color.a <= 0.0)
        return;
      UIVertex v = new UIVertex();
      Rect rect = this.mRectTransform.rect;
      Rect uvRect = this.uvRect;
      Color32 color = (Color32) this.color;
      int num = 0;
      if (color.r == byte.MaxValue && color.g == byte.MaxValue && (color.b == byte.MaxValue && color.a == byte.MaxValue))
      {
        for (int index = this.Quads.Length - 1; index >= 0; --index)
        {
          v.position.x = Mathf.Lerp(rect.xMin, rect.xMax, this.Quads[index].v0.x);
          v.position.y = Mathf.Lerp(rect.yMin, rect.yMax, this.Quads[index].v0.y);
          v.color = this.Quads[index].c0;
          v.uv0.x = Mathf.Lerp(uvRect.xMin, uvRect.xMax, this.Quads[index].v0.x);
          v.uv0.y = Mathf.Lerp(uvRect.yMin, uvRect.yMax, this.Quads[index].v0.y);
          vh.AddVert(v);
          v.position.x = Mathf.Lerp(rect.xMin, rect.xMax, this.Quads[index].v1.x);
          v.position.y = Mathf.Lerp(rect.yMin, rect.yMax, this.Quads[index].v1.y);
          v.color = this.Quads[index].c1;
          v.uv0.x = Mathf.Lerp(uvRect.xMin, uvRect.xMax, this.Quads[index].v1.x);
          v.uv0.y = Mathf.Lerp(uvRect.yMin, uvRect.yMax, this.Quads[index].v1.y);
          vh.AddVert(v);
          v.position.x = Mathf.Lerp(rect.xMin, rect.xMax, this.Quads[index].v2.x);
          v.position.y = Mathf.Lerp(rect.yMin, rect.yMax, this.Quads[index].v2.y);
          v.color = this.Quads[index].c2;
          v.uv0.x = Mathf.Lerp(uvRect.xMin, uvRect.xMax, this.Quads[index].v2.x);
          v.uv0.y = Mathf.Lerp(uvRect.yMin, uvRect.yMax, this.Quads[index].v2.y);
          vh.AddVert(v);
          v.position.x = Mathf.Lerp(rect.xMin, rect.xMax, this.Quads[index].v3.x);
          v.position.y = Mathf.Lerp(rect.yMin, rect.yMax, this.Quads[index].v3.y);
          v.color = this.Quads[index].c3;
          v.uv0.x = Mathf.Lerp(uvRect.xMin, uvRect.xMax, this.Quads[index].v3.x);
          v.uv0.y = Mathf.Lerp(uvRect.yMin, uvRect.yMax, this.Quads[index].v3.y);
          vh.AddVert(v);
          vh.AddTriangle(num, num + 1, num + 2);
          vh.AddTriangle(num + 2, num + 3, num);
          num += 4;
        }
      }
      else
      {
        for (int index = this.Quads.Length - 1; index >= 0; --index)
        {
          v.position.x = Mathf.Lerp(rect.xMin, rect.xMax, this.Quads[index].v0.x);
          v.position.y = Mathf.Lerp(rect.yMin, rect.yMax, this.Quads[index].v0.y);
          v.color.r = (byte) ((int) this.Quads[index].c0.r * (int) color.r / (int) byte.MaxValue);
          v.color.g = (byte) ((int) this.Quads[index].c0.g * (int) color.g / (int) byte.MaxValue);
          v.color.b = (byte) ((int) this.Quads[index].c0.b * (int) color.b / (int) byte.MaxValue);
          v.color.a = (byte) ((int) this.Quads[index].c0.a * (int) color.a / (int) byte.MaxValue);
          v.uv0.x = Mathf.Lerp(uvRect.xMin, uvRect.xMax, this.Quads[index].v0.x);
          v.uv0.y = Mathf.Lerp(uvRect.yMin, uvRect.yMax, this.Quads[index].v0.y);
          vh.AddVert(v);
          v.position.x = Mathf.Lerp(rect.xMin, rect.xMax, this.Quads[index].v1.x);
          v.position.y = Mathf.Lerp(rect.yMin, rect.yMax, this.Quads[index].v1.y);
          v.color.r = (byte) ((int) this.Quads[index].c1.r * (int) color.r / (int) byte.MaxValue);
          v.color.g = (byte) ((int) this.Quads[index].c1.g * (int) color.g / (int) byte.MaxValue);
          v.color.b = (byte) ((int) this.Quads[index].c1.b * (int) color.b / (int) byte.MaxValue);
          v.color.a = (byte) ((int) this.Quads[index].c1.a * (int) color.a / (int) byte.MaxValue);
          v.uv0.x = Mathf.Lerp(uvRect.xMin, uvRect.xMax, this.Quads[index].v1.x);
          v.uv0.y = Mathf.Lerp(uvRect.yMin, uvRect.yMax, this.Quads[index].v1.y);
          vh.AddVert(v);
          v.position.x = Mathf.Lerp(rect.xMin, rect.xMax, this.Quads[index].v2.x);
          v.position.y = Mathf.Lerp(rect.yMin, rect.yMax, this.Quads[index].v2.y);
          v.color.r = (byte) ((int) this.Quads[index].c2.r * (int) color.r / (int) byte.MaxValue);
          v.color.g = (byte) ((int) this.Quads[index].c2.g * (int) color.g / (int) byte.MaxValue);
          v.color.b = (byte) ((int) this.Quads[index].c2.b * (int) color.b / (int) byte.MaxValue);
          v.color.a = (byte) ((int) this.Quads[index].c2.a * (int) color.a / (int) byte.MaxValue);
          v.uv0.x = Mathf.Lerp(uvRect.xMin, uvRect.xMax, this.Quads[index].v2.x);
          v.uv0.y = Mathf.Lerp(uvRect.yMin, uvRect.yMax, this.Quads[index].v2.y);
          vh.AddVert(v);
          v.position.x = Mathf.Lerp(rect.xMin, rect.xMax, this.Quads[index].v3.x);
          v.position.y = Mathf.Lerp(rect.yMin, rect.yMax, this.Quads[index].v3.y);
          v.color.r = (byte) ((int) this.Quads[index].c3.r * (int) color.r / (int) byte.MaxValue);
          v.color.g = (byte) ((int) this.Quads[index].c3.g * (int) color.g / (int) byte.MaxValue);
          v.color.b = (byte) ((int) this.Quads[index].c3.b * (int) color.b / (int) byte.MaxValue);
          v.color.a = (byte) ((int) this.Quads[index].c3.a * (int) color.a / (int) byte.MaxValue);
          v.uv0.x = Mathf.Lerp(uvRect.xMin, uvRect.xMax, this.Quads[index].v3.x);
          v.uv0.y = Mathf.Lerp(uvRect.yMin, uvRect.yMax, this.Quads[index].v3.y);
          vh.AddVert(v);
          vh.AddTriangle(num, num + 1, num + 2);
          vh.AddTriangle(num + 2, num + 3, num);
          num += 4;
        }
      }
    }
  }
}
