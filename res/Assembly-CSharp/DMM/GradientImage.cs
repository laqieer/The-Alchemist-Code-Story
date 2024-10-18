// Decompiled with JetBrains decompiler
// Type: GradientImage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
[AddComponentMenu("UI/Gradient Image")]
public class GradientImage : Image
{
  public Color32 TopLeft = Color32.op_Implicit(Color.white);
  public Color32 TopRight = Color32.op_Implicit(Color.white);
  public Color32 BottomLeft = Color32.op_Implicit(Color.white);
  public Color32 BottomRight = Color32.op_Implicit(Color.white);

  protected virtual void OnPopulateMesh(VertexHelper vh)
  {
    vh.Clear();
    if (this.type == null)
    {
      Color32 a = Color32.op_Implicit(((Graphic) this).color);
      if (Object.op_Inequality((Object) this.sprite, (Object) null))
      {
        Vector2[] vertices = this.sprite.vertices;
        Vector2[] uv = this.sprite.uv;
        vh.AddVert(Vector2.op_Implicit(vertices[0]), GradientImage.MultiplyColor(a, this.BottomLeft), uv[0]);
        vh.AddVert(Vector2.op_Implicit(vertices[1]), GradientImage.MultiplyColor(a, this.TopLeft), uv[1]);
        vh.AddVert(Vector2.op_Implicit(vertices[2]), GradientImage.MultiplyColor(a, this.TopRight), uv[2]);
        vh.AddVert(Vector2.op_Implicit(vertices[3]), GradientImage.MultiplyColor(a, this.BottomRight), uv[3]);
      }
      else
      {
        Rect rect = ((Component) this).GetComponent<RectTransform>().rect;
        vh.AddVert(Vector2.op_Implicit(((Rect) ref rect).min), GradientImage.MultiplyColor(a, this.BottomLeft), new Vector2(0.0f, 0.0f));
        vh.AddVert(Vector2.op_Implicit(new Vector2(((Rect) ref rect).x, ((Rect) ref rect).yMax)), GradientImage.MultiplyColor(a, this.TopLeft), new Vector2(0.0f, 1f));
        vh.AddVert(Vector2.op_Implicit(((Rect) ref rect).max), GradientImage.MultiplyColor(a, this.TopRight), new Vector2(1f, 1f));
        vh.AddVert(Vector2.op_Implicit(new Vector2(((Rect) ref rect).xMax, ((Rect) ref rect).y)), GradientImage.MultiplyColor(a, this.BottomRight), new Vector2(1f, 0.0f));
      }
      vh.AddTriangle(0, 1, 2);
      vh.AddTriangle(2, 3, 0);
    }
    else
      base.OnPopulateMesh(vh);
  }

  private static Color32 MultiplyColor(Color32 a, Color32 b)
  {
    a.r = (byte) ((int) a.r * (int) b.r / (int) byte.MaxValue);
    a.g = (byte) ((int) a.g * (int) b.g / (int) byte.MaxValue);
    a.b = (byte) ((int) a.b * (int) b.b / (int) byte.MaxValue);
    a.a = (byte) ((int) a.a * (int) b.a / (int) byte.MaxValue);
    return a;
  }
}
