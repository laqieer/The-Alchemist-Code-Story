// Decompiled with JetBrains decompiler
// Type: GradientImage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UI/Gradient Image")]
public class GradientImage : Image
{
  public Color32 TopLeft = (Color32) Color.white;
  public Color32 TopRight = (Color32) Color.white;
  public Color32 BottomLeft = (Color32) Color.white;
  public Color32 BottomRight = (Color32) Color.white;

  protected override void OnPopulateMesh(VertexHelper vh)
  {
    vh.Clear();
    if (this.type == Image.Type.Simple)
    {
      Color32 color = (Color32) this.color;
      if ((Object) this.sprite != (Object) null)
      {
        Vector2[] vertices = this.sprite.vertices;
        Vector2[] uv = this.sprite.uv;
        vh.AddVert((Vector3) vertices[0], GradientImage.MultiplyColor(color, this.BottomLeft), uv[0]);
        vh.AddVert((Vector3) vertices[1], GradientImage.MultiplyColor(color, this.TopLeft), uv[1]);
        vh.AddVert((Vector3) vertices[2], GradientImage.MultiplyColor(color, this.TopRight), uv[2]);
        vh.AddVert((Vector3) vertices[3], GradientImage.MultiplyColor(color, this.BottomRight), uv[3]);
      }
      else
      {
        Rect rect = this.GetComponent<RectTransform>().rect;
        vh.AddVert((Vector3) rect.min, GradientImage.MultiplyColor(color, this.BottomLeft), new Vector2(0.0f, 0.0f));
        vh.AddVert((Vector3) new Vector2(rect.x, rect.yMax), GradientImage.MultiplyColor(color, this.TopLeft), new Vector2(0.0f, 1f));
        vh.AddVert((Vector3) rect.max, GradientImage.MultiplyColor(color, this.TopRight), new Vector2(1f, 1f));
        vh.AddVert((Vector3) new Vector2(rect.xMax, rect.y), GradientImage.MultiplyColor(color, this.BottomRight), new Vector2(1f, 0.0f));
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
