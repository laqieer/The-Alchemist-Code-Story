// Decompiled with JetBrains decompiler
// Type: Donuts
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.UI;

public class Donuts : Image
{
  public float MaxAngle = 90f;
  public float RadiusMin = 100f;
  public float RadiusMax = 110f;
  public float Step = 5f;
  public float Tiling = 1f;
  public float MinAngle;

  public void SetRange(float angleMin, float angleMax)
  {
    this.MinAngle = angleMin;
    this.MaxAngle = angleMax;
    this.SetVerticesDirty();
    this.UpdateGeometry();
  }

  protected override void OnPopulateMesh(VertexHelper vh)
  {
    vh.Clear();
    if ((double) Mathf.Abs(this.MinAngle - this.MaxAngle) <= 1.0 || (double) this.Step <= 1.40129846432482E-45)
      return;
    UIVertex v = new UIVertex();
    CanvasRenderer canvasRenderer = this.canvasRenderer;
    Sprite sprite = this.sprite;
    Vector2 zero1 = Vector2.zero;
    Vector2 zero2 = Vector2.zero;
    float num1;
    if ((Object) sprite != (Object) null)
    {
      float num2 = 1f / (float) sprite.texture.width;
      float num3 = 1f / (float) sprite.texture.height;
      Rect rect = sprite.rect;
      zero1.x = rect.min.x * num2;
      zero1.y = rect.min.y * num3;
      zero2.x = rect.max.x * num2;
      zero2.y = rect.max.y * num3;
      num1 = rect.width;
    }
    else
      num1 = float.MaxValue;
    float step = this.Step;
    float num4;
    float num5;
    if ((double) this.MinAngle < (double) this.MaxAngle)
    {
      num4 = this.MinAngle;
      num5 = this.MaxAngle;
    }
    else
    {
      num4 = this.MaxAngle;
      num5 = this.MinAngle;
    }
    int num6 = 0;
    float f1 = num4 * ((float) Math.PI / 180f);
    v.position = (Vector3) new Vector2(Mathf.Cos(f1) * this.RadiusMax, Mathf.Sin(f1) * this.RadiusMax);
    v.color = (Color32) canvasRenderer.GetColor();
    v.uv0.x = zero1.x;
    v.uv0.y = zero2.y;
    vh.AddVert(v);
    v.position = (Vector3) new Vector2(Mathf.Cos(f1) * this.RadiusMin, Mathf.Sin(f1) * this.RadiusMin);
    v.color = (Color32) canvasRenderer.GetColor();
    v.uv0.x = zero1.x;
    v.uv0.y = zero1.y;
    vh.AddVert(v);
    float num7 = 0.0f;
    float num8 = num4;
    float num9 = (float) (Math.PI / 180.0 * (double) this.RadiusMin * 2.0) * this.Tiling;
    while (true)
    {
      float num2 = num8 + step;
      float num3 = (num8 - num4) * num9;
      float num10 = (num2 - num4) * num9;
      num7 += num10 - num3;
      bool flag = false;
      if ((double) num7 >= (double) num1)
      {
        float num11 = (float) ((double) num7 / (double) num1 - 1.0);
        num2 = num8 + step * num11;
        num7 = 0.0f;
        flag = true;
      }
      num8 = num2;
      if ((double) num8 < (double) num5)
      {
        float f2 = num8 * ((float) Math.PI / 180f);
        float num11 = num7 / num1;
        v.position = (Vector3) new Vector2(Mathf.Cos(f2) * this.RadiusMin, Mathf.Sin(f2) * this.RadiusMin);
        v.color = (Color32) canvasRenderer.GetColor();
        v.uv0.x = !flag ? (zero2.x - zero1.x) * num11 + zero1.x : zero2.x;
        v.uv0.y = zero1.y;
        vh.AddVert(v);
        v.position = (Vector3) new Vector2(Mathf.Cos(f2) * this.RadiusMax, Mathf.Sin(f2) * this.RadiusMax);
        v.color = (Color32) canvasRenderer.GetColor();
        v.uv0.y = zero2.y;
        vh.AddVert(v);
        vh.AddTriangle(num6, num6 + 1, num6 + 2);
        vh.AddTriangle(num6 + 2, num6 + 3, num6);
        num6 += 4;
        if (flag)
          v.uv0.x = zero1.x;
        v.position = (Vector3) new Vector2(Mathf.Cos(f2) * this.RadiusMax, Mathf.Sin(f2) * this.RadiusMax);
        v.color = (Color32) canvasRenderer.GetColor();
        v.uv0.y = zero2.y;
        vh.AddVert(v);
        v.position = (Vector3) new Vector2(Mathf.Cos(f2) * this.RadiusMin, Mathf.Sin(f2) * this.RadiusMin);
        v.color = (Color32) canvasRenderer.GetColor();
        v.uv0.y = zero1.y;
        vh.AddVert(v);
      }
      else
        break;
    }
    float f3 = num5 * ((float) Math.PI / 180f);
    float num12 = (num7 + (num5 - num8) * num9) / num1;
    v.uv0.x = (zero2.x - zero1.x) * num12 + zero1.x;
    v.position = (Vector3) new Vector2(Mathf.Cos(f3) * this.RadiusMin, Mathf.Sin(f3) * this.RadiusMin);
    v.color = (Color32) canvasRenderer.GetColor();
    v.uv0.y = zero1.y;
    vh.AddVert(v);
    v.position = (Vector3) new Vector2(Mathf.Cos(f3) * this.RadiusMax, Mathf.Sin(f3) * this.RadiusMax);
    v.color = (Color32) canvasRenderer.GetColor();
    v.uv0.y = zero2.y;
    vh.AddVert(v);
    vh.AddTriangle(num6, num6 + 1, num6 + 2);
    vh.AddTriangle(num6 + 2, num6 + 3, num6);
    int num13 = num6 + 4;
  }

  public override bool Raycast(Vector2 sp, Camera eventCamera)
  {
    return false;
  }
}
