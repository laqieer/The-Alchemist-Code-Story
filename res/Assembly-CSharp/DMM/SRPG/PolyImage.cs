// Decompiled with JetBrains decompiler
// Type: SRPG.PolyImage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class PolyImage : Image
  {
    public Quad[] Quads = new Quad[0];
    public bool Transparent;
    private RectTransform mRectTransform;

    protected virtual void Awake()
    {
      ((UIBehaviour) this).Awake();
      this.mRectTransform = ((Component) this).GetComponent<RectTransform>();
    }

    protected virtual void OnPopulateMesh(VertexHelper vh)
    {
      vh.Clear();
      if (Object.op_Equality((Object) this.sprite, (Object) null) && this.Transparent || (double) ((Graphic) this).color.a <= 0.0)
        return;
      UIVertex uiVertex = new UIVertex();
      Color32 color32 = Color32.op_Implicit(((Graphic) this).color);
      Rect rect1 = this.mRectTransform.rect;
      int num1 = 0;
      Sprite sprite = this.sprite;
      Rect rect2;
      if (Object.op_Inequality((Object) sprite, (Object) null))
      {
        float num2 = 1f / (float) ((Texture) sprite.texture).width;
        float num3 = 1f / (float) ((Texture) sprite.texture).height;
        Rect rect3 = sprite.rect;
        // ISSUE: explicit constructor call
        ((Rect) ref rect2).\u002Ector(((Rect) ref rect3).x * num2, ((Rect) ref rect3).y * num3, ((Rect) ref rect3).width * num2, ((Rect) ref rect3).height * num3);
      }
      else
      {
        // ISSUE: explicit constructor call
        ((Rect) ref rect2).\u002Ector(0.0f, 0.0f, 1f, 1f);
      }
      if (color32.r == byte.MaxValue && color32.g == byte.MaxValue && color32.b == byte.MaxValue && color32.a == byte.MaxValue)
      {
        for (int index = this.Quads.Length - 1; index >= 0; --index)
        {
          uiVertex.position.x = Mathf.Lerp(((Rect) ref rect1).xMin, ((Rect) ref rect1).xMax, this.Quads[index].v0.x);
          uiVertex.position.y = Mathf.Lerp(((Rect) ref rect1).yMin, ((Rect) ref rect1).yMax, this.Quads[index].v0.y);
          uiVertex.color = this.Quads[index].c0;
          uiVertex.uv0.x = Mathf.Lerp(((Rect) ref rect2).xMin, ((Rect) ref rect2).xMax, this.Quads[index].v0.x);
          uiVertex.uv0.y = Mathf.Lerp(((Rect) ref rect2).yMin, ((Rect) ref rect2).yMax, this.Quads[index].v0.y);
          vh.AddVert(uiVertex);
          uiVertex.position.x = Mathf.Lerp(((Rect) ref rect1).xMin, ((Rect) ref rect1).xMax, this.Quads[index].v1.x);
          uiVertex.position.y = Mathf.Lerp(((Rect) ref rect1).yMin, ((Rect) ref rect1).yMax, this.Quads[index].v1.y);
          uiVertex.color = this.Quads[index].c1;
          uiVertex.uv0.x = Mathf.Lerp(((Rect) ref rect2).xMin, ((Rect) ref rect2).xMax, this.Quads[index].v1.x);
          uiVertex.uv0.y = Mathf.Lerp(((Rect) ref rect2).yMin, ((Rect) ref rect2).yMax, this.Quads[index].v1.y);
          vh.AddVert(uiVertex);
          uiVertex.position.x = Mathf.Lerp(((Rect) ref rect1).xMin, ((Rect) ref rect1).xMax, this.Quads[index].v2.x);
          uiVertex.position.y = Mathf.Lerp(((Rect) ref rect1).yMin, ((Rect) ref rect1).yMax, this.Quads[index].v2.y);
          uiVertex.color = this.Quads[index].c2;
          uiVertex.uv0.x = Mathf.Lerp(((Rect) ref rect2).xMin, ((Rect) ref rect2).xMax, this.Quads[index].v2.x);
          uiVertex.uv0.y = Mathf.Lerp(((Rect) ref rect2).yMin, ((Rect) ref rect2).yMax, this.Quads[index].v2.y);
          vh.AddVert(uiVertex);
          uiVertex.position.x = Mathf.Lerp(((Rect) ref rect1).xMin, ((Rect) ref rect1).xMax, this.Quads[index].v3.x);
          uiVertex.position.y = Mathf.Lerp(((Rect) ref rect1).yMin, ((Rect) ref rect1).yMax, this.Quads[index].v3.y);
          uiVertex.color = this.Quads[index].c3;
          uiVertex.uv0.x = Mathf.Lerp(((Rect) ref rect2).xMin, ((Rect) ref rect2).xMax, this.Quads[index].v3.x);
          uiVertex.uv0.y = Mathf.Lerp(((Rect) ref rect2).yMin, ((Rect) ref rect2).yMax, this.Quads[index].v3.y);
          vh.AddVert(uiVertex);
          vh.AddTriangle(num1, num1 + 1, num1 + 2);
          vh.AddTriangle(num1 + 2, num1 + 3, num1);
          num1 += 4;
        }
      }
      else
      {
        for (int index = this.Quads.Length - 1; index >= 0; --index)
        {
          uiVertex.position.x = Mathf.Lerp(((Rect) ref rect1).xMin, ((Rect) ref rect1).xMax, this.Quads[index].v0.x);
          uiVertex.position.y = Mathf.Lerp(((Rect) ref rect1).yMin, ((Rect) ref rect1).yMax, this.Quads[index].v0.y);
          uiVertex.color.r = (byte) ((int) this.Quads[index].c0.r * (int) color32.r / (int) byte.MaxValue);
          uiVertex.color.g = (byte) ((int) this.Quads[index].c0.g * (int) color32.g / (int) byte.MaxValue);
          uiVertex.color.b = (byte) ((int) this.Quads[index].c0.b * (int) color32.b / (int) byte.MaxValue);
          uiVertex.color.a = (byte) ((int) this.Quads[index].c0.a * (int) color32.a / (int) byte.MaxValue);
          uiVertex.uv0.x = Mathf.Lerp(((Rect) ref rect2).xMin, ((Rect) ref rect2).xMax, this.Quads[index].v0.x);
          uiVertex.uv0.y = Mathf.Lerp(((Rect) ref rect2).yMin, ((Rect) ref rect2).yMax, this.Quads[index].v0.y);
          vh.AddVert(uiVertex);
          uiVertex.position.x = Mathf.Lerp(((Rect) ref rect1).xMin, ((Rect) ref rect1).xMax, this.Quads[index].v1.x);
          uiVertex.position.y = Mathf.Lerp(((Rect) ref rect1).yMin, ((Rect) ref rect1).yMax, this.Quads[index].v1.y);
          uiVertex.color.r = (byte) ((int) this.Quads[index].c1.r * (int) color32.r / (int) byte.MaxValue);
          uiVertex.color.g = (byte) ((int) this.Quads[index].c1.g * (int) color32.g / (int) byte.MaxValue);
          uiVertex.color.b = (byte) ((int) this.Quads[index].c1.b * (int) color32.b / (int) byte.MaxValue);
          uiVertex.color.a = (byte) ((int) this.Quads[index].c1.a * (int) color32.a / (int) byte.MaxValue);
          uiVertex.uv0.x = Mathf.Lerp(((Rect) ref rect2).xMin, ((Rect) ref rect2).xMax, this.Quads[index].v1.x);
          uiVertex.uv0.y = Mathf.Lerp(((Rect) ref rect2).yMin, ((Rect) ref rect2).yMax, this.Quads[index].v1.y);
          vh.AddVert(uiVertex);
          uiVertex.position.x = Mathf.Lerp(((Rect) ref rect1).xMin, ((Rect) ref rect1).xMax, this.Quads[index].v2.x);
          uiVertex.position.y = Mathf.Lerp(((Rect) ref rect1).yMin, ((Rect) ref rect1).yMax, this.Quads[index].v2.y);
          uiVertex.color.r = (byte) ((int) this.Quads[index].c2.r * (int) color32.r / (int) byte.MaxValue);
          uiVertex.color.g = (byte) ((int) this.Quads[index].c2.g * (int) color32.g / (int) byte.MaxValue);
          uiVertex.color.b = (byte) ((int) this.Quads[index].c2.b * (int) color32.b / (int) byte.MaxValue);
          uiVertex.color.a = (byte) ((int) this.Quads[index].c2.a * (int) color32.a / (int) byte.MaxValue);
          uiVertex.uv0.x = Mathf.Lerp(((Rect) ref rect2).xMin, ((Rect) ref rect2).xMax, this.Quads[index].v2.x);
          uiVertex.uv0.y = Mathf.Lerp(((Rect) ref rect2).yMin, ((Rect) ref rect2).yMax, this.Quads[index].v2.y);
          vh.AddVert(uiVertex);
          uiVertex.position.x = Mathf.Lerp(((Rect) ref rect1).xMin, ((Rect) ref rect1).xMax, this.Quads[index].v3.x);
          uiVertex.position.y = Mathf.Lerp(((Rect) ref rect1).yMin, ((Rect) ref rect1).yMax, this.Quads[index].v3.y);
          uiVertex.color.r = (byte) ((int) this.Quads[index].c3.r * (int) color32.r / (int) byte.MaxValue);
          uiVertex.color.g = (byte) ((int) this.Quads[index].c3.g * (int) color32.g / (int) byte.MaxValue);
          uiVertex.color.b = (byte) ((int) this.Quads[index].c3.b * (int) color32.b / (int) byte.MaxValue);
          uiVertex.color.a = (byte) ((int) this.Quads[index].c3.a * (int) color32.a / (int) byte.MaxValue);
          uiVertex.uv0.x = Mathf.Lerp(((Rect) ref rect2).xMin, ((Rect) ref rect2).xMax, this.Quads[index].v3.x);
          uiVertex.uv0.y = Mathf.Lerp(((Rect) ref rect2).yMin, ((Rect) ref rect2).yMax, this.Quads[index].v3.y);
          vh.AddVert(uiVertex);
          vh.AddTriangle(num1, num1 + 1, num1 + 2);
          vh.AddTriangle(num1 + 2, num1 + 3, num1);
          num1 += 4;
        }
      }
    }
  }
}
