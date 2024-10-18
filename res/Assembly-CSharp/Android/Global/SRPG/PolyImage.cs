// Decompiled with JetBrains decompiler
// Type: SRPG.PolyImage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class PolyImage : Image
  {
    public Quad[] Quads = new Quad[0];
    public bool Transparent;
    private RectTransform mRectTransform;

    protected override void Awake()
    {
      base.Awake();
      this.mRectTransform = this.GetComponent<RectTransform>();
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
      vh.Clear();
      if ((UnityEngine.Object) this.sprite == (UnityEngine.Object) null && this.Transparent || (double) this.color.a <= 0.0)
        return;
      UIVertex v = new UIVertex();
      Color32 color = (Color32) this.color;
      Rect rect1 = this.mRectTransform.rect;
      int num1 = 0;
      Sprite sprite = this.sprite;
      Rect rect2;
      if ((UnityEngine.Object) sprite != (UnityEngine.Object) null)
      {
        float num2 = 1f / (float) sprite.texture.width;
        float num3 = 1f / (float) sprite.texture.height;
        Rect rect3 = sprite.rect;
        rect2 = new Rect(rect3.x * num2, rect3.y * num3, rect3.width * num2, rect3.height * num3);
      }
      else
        rect2 = new Rect(0.0f, 0.0f, 1f, 1f);
      if (color.r == byte.MaxValue && color.g == byte.MaxValue && (color.b == byte.MaxValue && color.a == byte.MaxValue))
      {
        for (int index = this.Quads.Length - 1; index >= 0; --index)
        {
          v.position.x = Mathf.Lerp(rect1.xMin, rect1.xMax, this.Quads[index].v0.x);
          v.position.y = Mathf.Lerp(rect1.yMin, rect1.yMax, this.Quads[index].v0.y);
          v.color = this.Quads[index].c0;
          v.uv0.x = Mathf.Lerp(rect2.xMin, rect2.xMax, this.Quads[index].v0.x);
          v.uv0.y = Mathf.Lerp(rect2.yMin, rect2.yMax, this.Quads[index].v0.y);
          vh.AddVert(v);
          v.position.x = Mathf.Lerp(rect1.xMin, rect1.xMax, this.Quads[index].v1.x);
          v.position.y = Mathf.Lerp(rect1.yMin, rect1.yMax, this.Quads[index].v1.y);
          v.color = this.Quads[index].c1;
          v.uv0.x = Mathf.Lerp(rect2.xMin, rect2.xMax, this.Quads[index].v1.x);
          v.uv0.y = Mathf.Lerp(rect2.yMin, rect2.yMax, this.Quads[index].v1.y);
          vh.AddVert(v);
          v.position.x = Mathf.Lerp(rect1.xMin, rect1.xMax, this.Quads[index].v2.x);
          v.position.y = Mathf.Lerp(rect1.yMin, rect1.yMax, this.Quads[index].v2.y);
          v.color = this.Quads[index].c2;
          v.uv0.x = Mathf.Lerp(rect2.xMin, rect2.xMax, this.Quads[index].v2.x);
          v.uv0.y = Mathf.Lerp(rect2.yMin, rect2.yMax, this.Quads[index].v2.y);
          vh.AddVert(v);
          v.position.x = Mathf.Lerp(rect1.xMin, rect1.xMax, this.Quads[index].v3.x);
          v.position.y = Mathf.Lerp(rect1.yMin, rect1.yMax, this.Quads[index].v3.y);
          v.color = this.Quads[index].c3;
          v.uv0.x = Mathf.Lerp(rect2.xMin, rect2.xMax, this.Quads[index].v3.x);
          v.uv0.y = Mathf.Lerp(rect2.yMin, rect2.yMax, this.Quads[index].v3.y);
          vh.AddVert(v);
          vh.AddTriangle(num1, num1 + 1, num1 + 2);
          vh.AddTriangle(num1 + 2, num1 + 3, num1);
          num1 += 4;
        }
      }
      else
      {
        for (int index = this.Quads.Length - 1; index >= 0; --index)
        {
          v.position.x = Mathf.Lerp(rect1.xMin, rect1.xMax, this.Quads[index].v0.x);
          v.position.y = Mathf.Lerp(rect1.yMin, rect1.yMax, this.Quads[index].v0.y);
          v.color.r = (byte) ((int) this.Quads[index].c0.r * (int) color.r / (int) byte.MaxValue);
          v.color.g = (byte) ((int) this.Quads[index].c0.g * (int) color.g / (int) byte.MaxValue);
          v.color.b = (byte) ((int) this.Quads[index].c0.b * (int) color.b / (int) byte.MaxValue);
          v.color.a = (byte) ((int) this.Quads[index].c0.a * (int) color.a / (int) byte.MaxValue);
          v.uv0.x = Mathf.Lerp(rect2.xMin, rect2.xMax, this.Quads[index].v0.x);
          v.uv0.y = Mathf.Lerp(rect2.yMin, rect2.yMax, this.Quads[index].v0.y);
          vh.AddVert(v);
          v.position.x = Mathf.Lerp(rect1.xMin, rect1.xMax, this.Quads[index].v1.x);
          v.position.y = Mathf.Lerp(rect1.yMin, rect1.yMax, this.Quads[index].v1.y);
          v.color.r = (byte) ((int) this.Quads[index].c1.r * (int) color.r / (int) byte.MaxValue);
          v.color.g = (byte) ((int) this.Quads[index].c1.g * (int) color.g / (int) byte.MaxValue);
          v.color.b = (byte) ((int) this.Quads[index].c1.b * (int) color.b / (int) byte.MaxValue);
          v.color.a = (byte) ((int) this.Quads[index].c1.a * (int) color.a / (int) byte.MaxValue);
          v.uv0.x = Mathf.Lerp(rect2.xMin, rect2.xMax, this.Quads[index].v1.x);
          v.uv0.y = Mathf.Lerp(rect2.yMin, rect2.yMax, this.Quads[index].v1.y);
          vh.AddVert(v);
          v.position.x = Mathf.Lerp(rect1.xMin, rect1.xMax, this.Quads[index].v2.x);
          v.position.y = Mathf.Lerp(rect1.yMin, rect1.yMax, this.Quads[index].v2.y);
          v.color.r = (byte) ((int) this.Quads[index].c2.r * (int) color.r / (int) byte.MaxValue);
          v.color.g = (byte) ((int) this.Quads[index].c2.g * (int) color.g / (int) byte.MaxValue);
          v.color.b = (byte) ((int) this.Quads[index].c2.b * (int) color.b / (int) byte.MaxValue);
          v.color.a = (byte) ((int) this.Quads[index].c2.a * (int) color.a / (int) byte.MaxValue);
          v.uv0.x = Mathf.Lerp(rect2.xMin, rect2.xMax, this.Quads[index].v2.x);
          v.uv0.y = Mathf.Lerp(rect2.yMin, rect2.yMax, this.Quads[index].v2.y);
          vh.AddVert(v);
          v.position.x = Mathf.Lerp(rect1.xMin, rect1.xMax, this.Quads[index].v3.x);
          v.position.y = Mathf.Lerp(rect1.yMin, rect1.yMax, this.Quads[index].v3.y);
          v.color.r = (byte) ((int) this.Quads[index].c3.r * (int) color.r / (int) byte.MaxValue);
          v.color.g = (byte) ((int) this.Quads[index].c3.g * (int) color.g / (int) byte.MaxValue);
          v.color.b = (byte) ((int) this.Quads[index].c3.b * (int) color.b / (int) byte.MaxValue);
          v.color.a = (byte) ((int) this.Quads[index].c3.a * (int) color.a / (int) byte.MaxValue);
          v.uv0.x = Mathf.Lerp(rect2.xMin, rect2.xMax, this.Quads[index].v3.x);
          v.uv0.y = Mathf.Lerp(rect2.yMin, rect2.yMax, this.Quads[index].v3.y);
          vh.AddVert(v);
          vh.AddTriangle(num1, num1 + 1, num1 + 2);
          vh.AddTriangle(num1 + 2, num1 + 3, num1);
          num1 += 4;
        }
      }
    }
  }
}
