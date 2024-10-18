// Decompiled with JetBrains decompiler
// Type: SRPG.RawPolyImage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class RawPolyImage : RawImage
  {
    public Quad[] Quads = new Quad[0];
    public bool Transparent;
    public string Preview;
    private RectTransform mRectTransform;

    protected virtual void Awake()
    {
      ((UIBehaviour) this).Awake();
      this.mRectTransform = ((Component) this).GetComponent<RectTransform>();
    }

    protected virtual void OnPopulateMesh(VertexHelper vh)
    {
      vh.Clear();
      if (Object.op_Equality((Object) this.texture, (Object) null) && this.Transparent || (double) ((Graphic) this).color.a <= 0.0)
        return;
      UIVertex uiVertex = new UIVertex();
      Rect rect = this.mRectTransform.rect;
      Rect uvRect = this.uvRect;
      Color32 color32 = Color32.op_Implicit(((Graphic) this).color);
      int num = 0;
      if (color32.r == byte.MaxValue && color32.g == byte.MaxValue && color32.b == byte.MaxValue && color32.a == byte.MaxValue)
      {
        for (int index = this.Quads.Length - 1; index >= 0; --index)
        {
          uiVertex.position.x = Mathf.Lerp(((Rect) ref rect).xMin, ((Rect) ref rect).xMax, this.Quads[index].v0.x);
          uiVertex.position.y = Mathf.Lerp(((Rect) ref rect).yMin, ((Rect) ref rect).yMax, this.Quads[index].v0.y);
          uiVertex.color = this.Quads[index].c0;
          uiVertex.uv0.x = Mathf.Lerp(((Rect) ref uvRect).xMin, ((Rect) ref uvRect).xMax, this.Quads[index].v0.x);
          uiVertex.uv0.y = Mathf.Lerp(((Rect) ref uvRect).yMin, ((Rect) ref uvRect).yMax, this.Quads[index].v0.y);
          vh.AddVert(uiVertex);
          uiVertex.position.x = Mathf.Lerp(((Rect) ref rect).xMin, ((Rect) ref rect).xMax, this.Quads[index].v1.x);
          uiVertex.position.y = Mathf.Lerp(((Rect) ref rect).yMin, ((Rect) ref rect).yMax, this.Quads[index].v1.y);
          uiVertex.color = this.Quads[index].c1;
          uiVertex.uv0.x = Mathf.Lerp(((Rect) ref uvRect).xMin, ((Rect) ref uvRect).xMax, this.Quads[index].v1.x);
          uiVertex.uv0.y = Mathf.Lerp(((Rect) ref uvRect).yMin, ((Rect) ref uvRect).yMax, this.Quads[index].v1.y);
          vh.AddVert(uiVertex);
          uiVertex.position.x = Mathf.Lerp(((Rect) ref rect).xMin, ((Rect) ref rect).xMax, this.Quads[index].v2.x);
          uiVertex.position.y = Mathf.Lerp(((Rect) ref rect).yMin, ((Rect) ref rect).yMax, this.Quads[index].v2.y);
          uiVertex.color = this.Quads[index].c2;
          uiVertex.uv0.x = Mathf.Lerp(((Rect) ref uvRect).xMin, ((Rect) ref uvRect).xMax, this.Quads[index].v2.x);
          uiVertex.uv0.y = Mathf.Lerp(((Rect) ref uvRect).yMin, ((Rect) ref uvRect).yMax, this.Quads[index].v2.y);
          vh.AddVert(uiVertex);
          uiVertex.position.x = Mathf.Lerp(((Rect) ref rect).xMin, ((Rect) ref rect).xMax, this.Quads[index].v3.x);
          uiVertex.position.y = Mathf.Lerp(((Rect) ref rect).yMin, ((Rect) ref rect).yMax, this.Quads[index].v3.y);
          uiVertex.color = this.Quads[index].c3;
          uiVertex.uv0.x = Mathf.Lerp(((Rect) ref uvRect).xMin, ((Rect) ref uvRect).xMax, this.Quads[index].v3.x);
          uiVertex.uv0.y = Mathf.Lerp(((Rect) ref uvRect).yMin, ((Rect) ref uvRect).yMax, this.Quads[index].v3.y);
          vh.AddVert(uiVertex);
          vh.AddTriangle(num, num + 1, num + 2);
          vh.AddTriangle(num + 2, num + 3, num);
          num += 4;
        }
      }
      else
      {
        for (int index = this.Quads.Length - 1; index >= 0; --index)
        {
          uiVertex.position.x = Mathf.Lerp(((Rect) ref rect).xMin, ((Rect) ref rect).xMax, this.Quads[index].v0.x);
          uiVertex.position.y = Mathf.Lerp(((Rect) ref rect).yMin, ((Rect) ref rect).yMax, this.Quads[index].v0.y);
          uiVertex.color.r = (byte) ((int) this.Quads[index].c0.r * (int) color32.r / (int) byte.MaxValue);
          uiVertex.color.g = (byte) ((int) this.Quads[index].c0.g * (int) color32.g / (int) byte.MaxValue);
          uiVertex.color.b = (byte) ((int) this.Quads[index].c0.b * (int) color32.b / (int) byte.MaxValue);
          uiVertex.color.a = (byte) ((int) this.Quads[index].c0.a * (int) color32.a / (int) byte.MaxValue);
          uiVertex.uv0.x = Mathf.Lerp(((Rect) ref uvRect).xMin, ((Rect) ref uvRect).xMax, this.Quads[index].v0.x);
          uiVertex.uv0.y = Mathf.Lerp(((Rect) ref uvRect).yMin, ((Rect) ref uvRect).yMax, this.Quads[index].v0.y);
          vh.AddVert(uiVertex);
          uiVertex.position.x = Mathf.Lerp(((Rect) ref rect).xMin, ((Rect) ref rect).xMax, this.Quads[index].v1.x);
          uiVertex.position.y = Mathf.Lerp(((Rect) ref rect).yMin, ((Rect) ref rect).yMax, this.Quads[index].v1.y);
          uiVertex.color.r = (byte) ((int) this.Quads[index].c1.r * (int) color32.r / (int) byte.MaxValue);
          uiVertex.color.g = (byte) ((int) this.Quads[index].c1.g * (int) color32.g / (int) byte.MaxValue);
          uiVertex.color.b = (byte) ((int) this.Quads[index].c1.b * (int) color32.b / (int) byte.MaxValue);
          uiVertex.color.a = (byte) ((int) this.Quads[index].c1.a * (int) color32.a / (int) byte.MaxValue);
          uiVertex.uv0.x = Mathf.Lerp(((Rect) ref uvRect).xMin, ((Rect) ref uvRect).xMax, this.Quads[index].v1.x);
          uiVertex.uv0.y = Mathf.Lerp(((Rect) ref uvRect).yMin, ((Rect) ref uvRect).yMax, this.Quads[index].v1.y);
          vh.AddVert(uiVertex);
          uiVertex.position.x = Mathf.Lerp(((Rect) ref rect).xMin, ((Rect) ref rect).xMax, this.Quads[index].v2.x);
          uiVertex.position.y = Mathf.Lerp(((Rect) ref rect).yMin, ((Rect) ref rect).yMax, this.Quads[index].v2.y);
          uiVertex.color.r = (byte) ((int) this.Quads[index].c2.r * (int) color32.r / (int) byte.MaxValue);
          uiVertex.color.g = (byte) ((int) this.Quads[index].c2.g * (int) color32.g / (int) byte.MaxValue);
          uiVertex.color.b = (byte) ((int) this.Quads[index].c2.b * (int) color32.b / (int) byte.MaxValue);
          uiVertex.color.a = (byte) ((int) this.Quads[index].c2.a * (int) color32.a / (int) byte.MaxValue);
          uiVertex.uv0.x = Mathf.Lerp(((Rect) ref uvRect).xMin, ((Rect) ref uvRect).xMax, this.Quads[index].v2.x);
          uiVertex.uv0.y = Mathf.Lerp(((Rect) ref uvRect).yMin, ((Rect) ref uvRect).yMax, this.Quads[index].v2.y);
          vh.AddVert(uiVertex);
          uiVertex.position.x = Mathf.Lerp(((Rect) ref rect).xMin, ((Rect) ref rect).xMax, this.Quads[index].v3.x);
          uiVertex.position.y = Mathf.Lerp(((Rect) ref rect).yMin, ((Rect) ref rect).yMax, this.Quads[index].v3.y);
          uiVertex.color.r = (byte) ((int) this.Quads[index].c3.r * (int) color32.r / (int) byte.MaxValue);
          uiVertex.color.g = (byte) ((int) this.Quads[index].c3.g * (int) color32.g / (int) byte.MaxValue);
          uiVertex.color.b = (byte) ((int) this.Quads[index].c3.b * (int) color32.b / (int) byte.MaxValue);
          uiVertex.color.a = (byte) ((int) this.Quads[index].c3.a * (int) color32.a / (int) byte.MaxValue);
          uiVertex.uv0.x = Mathf.Lerp(((Rect) ref uvRect).xMin, ((Rect) ref uvRect).xMax, this.Quads[index].v3.x);
          uiVertex.uv0.y = Mathf.Lerp(((Rect) ref uvRect).yMin, ((Rect) ref uvRect).yMax, this.Quads[index].v3.y);
          vh.AddVert(uiVertex);
          vh.AddTriangle(num, num + 1, num + 2);
          vh.AddTriangle(num + 2, num + 3, num);
          num += 4;
        }
      }
    }
  }
}
