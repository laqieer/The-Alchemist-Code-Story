// Decompiled with JetBrains decompiler
// Type: Donuts
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class Donuts : Image
{
  public float MinAngle;
  public float MaxAngle = 90f;
  public float RadiusMin = 100f;
  public float RadiusMax = 110f;
  public float Step = 5f;
  public float Tiling = 1f;

  public void SetRange(float angleMin, float angleMax)
  {
    this.MinAngle = angleMin;
    this.MaxAngle = angleMax;
    ((Graphic) this).SetVerticesDirty();
    ((Graphic) this).UpdateGeometry();
  }

  protected virtual void OnPopulateMesh(VertexHelper vh)
  {
    vh.Clear();
    if ((double) Mathf.Abs(this.MinAngle - this.MaxAngle) <= 1.0 || (double) this.Step <= 1.4012984643248171E-45)
      return;
    UIVertex uiVertex = new UIVertex();
    CanvasRenderer canvasRenderer = ((Graphic) this).canvasRenderer;
    Sprite sprite = this.sprite;
    Vector2 zero1 = Vector2.zero;
    Vector2 zero2 = Vector2.zero;
    float num1;
    if (Object.op_Inequality((Object) sprite, (Object) null))
    {
      float num2 = 1f / (float) ((Texture) sprite.texture).width;
      float num3 = 1f / (float) ((Texture) sprite.texture).height;
      Rect rect = sprite.rect;
      zero1.x = ((Rect) ref rect).min.x * num2;
      zero1.y = ((Rect) ref rect).min.y * num3;
      zero2.x = ((Rect) ref rect).max.x * num2;
      zero2.y = ((Rect) ref rect).max.y * num3;
      num1 = ((Rect) ref rect).width;
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
    float num7 = num4 * ((float) Math.PI / 180f);
    uiVertex.position = Vector2.op_Implicit(new Vector2(Mathf.Cos(num7) * this.RadiusMax, Mathf.Sin(num7) * this.RadiusMax));
    uiVertex.color = Color32.op_Implicit(canvasRenderer.GetColor());
    uiVertex.uv0.x = zero1.x;
    uiVertex.uv0.y = zero2.y;
    vh.AddVert(uiVertex);
    uiVertex.position = Vector2.op_Implicit(new Vector2(Mathf.Cos(num7) * this.RadiusMin, Mathf.Sin(num7) * this.RadiusMin));
    uiVertex.color = Color32.op_Implicit(canvasRenderer.GetColor());
    uiVertex.uv0.x = zero1.x;
    uiVertex.uv0.y = zero1.y;
    vh.AddVert(uiVertex);
    float num8 = 0.0f;
    float num9 = num4;
    float num10 = (float) (Math.PI / 180.0 * (double) this.RadiusMin * 2.0) * this.Tiling;
    while (true)
    {
      float num11 = num9 + step;
      float num12 = (num9 - num4) * num10;
      float num13 = (num11 - num4) * num10;
      num8 += num13 - num12;
      bool flag = false;
      if ((double) num8 >= (double) num1)
      {
        float num14 = (float) ((double) num8 / (double) num1 - 1.0);
        num11 = num9 + step * num14;
        num8 = 0.0f;
        flag = true;
      }
      num9 = num11;
      if ((double) num9 < (double) num5)
      {
        float num15 = num9 * ((float) Math.PI / 180f);
        float num16 = num8 / num1;
        uiVertex.position = Vector2.op_Implicit(new Vector2(Mathf.Cos(num15) * this.RadiusMin, Mathf.Sin(num15) * this.RadiusMin));
        uiVertex.color = Color32.op_Implicit(canvasRenderer.GetColor());
        uiVertex.uv0.x = !flag ? (zero2.x - zero1.x) * num16 + zero1.x : zero2.x;
        uiVertex.uv0.y = zero1.y;
        vh.AddVert(uiVertex);
        uiVertex.position = Vector2.op_Implicit(new Vector2(Mathf.Cos(num15) * this.RadiusMax, Mathf.Sin(num15) * this.RadiusMax));
        uiVertex.color = Color32.op_Implicit(canvasRenderer.GetColor());
        uiVertex.uv0.y = zero2.y;
        vh.AddVert(uiVertex);
        vh.AddTriangle(num6, num6 + 1, num6 + 2);
        vh.AddTriangle(num6 + 2, num6 + 3, num6);
        num6 += 4;
        if (flag)
          uiVertex.uv0.x = zero1.x;
        uiVertex.position = Vector2.op_Implicit(new Vector2(Mathf.Cos(num15) * this.RadiusMax, Mathf.Sin(num15) * this.RadiusMax));
        uiVertex.color = Color32.op_Implicit(canvasRenderer.GetColor());
        uiVertex.uv0.y = zero2.y;
        vh.AddVert(uiVertex);
        uiVertex.position = Vector2.op_Implicit(new Vector2(Mathf.Cos(num15) * this.RadiusMin, Mathf.Sin(num15) * this.RadiusMin));
        uiVertex.color = Color32.op_Implicit(canvasRenderer.GetColor());
        uiVertex.uv0.y = zero1.y;
        vh.AddVert(uiVertex);
      }
      else
        break;
    }
    float num17 = num5 * ((float) Math.PI / 180f);
    float num18 = (num8 + (num5 - num9) * num10) / num1;
    uiVertex.uv0.x = (zero2.x - zero1.x) * num18 + zero1.x;
    uiVertex.position = Vector2.op_Implicit(new Vector2(Mathf.Cos(num17) * this.RadiusMin, Mathf.Sin(num17) * this.RadiusMin));
    uiVertex.color = Color32.op_Implicit(canvasRenderer.GetColor());
    uiVertex.uv0.y = zero1.y;
    vh.AddVert(uiVertex);
    uiVertex.position = Vector2.op_Implicit(new Vector2(Mathf.Cos(num17) * this.RadiusMax, Mathf.Sin(num17) * this.RadiusMax));
    uiVertex.color = Color32.op_Implicit(canvasRenderer.GetColor());
    uiVertex.uv0.y = zero2.y;
    vh.AddVert(uiVertex);
    vh.AddTriangle(num6, num6 + 1, num6 + 2);
    vh.AddTriangle(num6 + 2, num6 + 3, num6);
    int num19 = num6 + 4;
  }

  public virtual bool Raycast(Vector2 sp, Camera eventCamera) => false;
}
