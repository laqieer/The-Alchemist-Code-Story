// Decompiled with JetBrains decompiler
// Type: KuroObi
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
[ExecuteInEditMode]
public class KuroObi : Graphic
{
  public static float CalcObiSize()
  {
    if (!SRPG_CanvasScaler.UseKuroObi)
      return 0.0f;
    float num1 = (float) Screen.width / (float) Screen.height;
    float num2 = 1.77866662f;
    return (double) num1 >= (double) num2 ? 0.0f : (float) ((750.0 / (double) (num1 / num2) - 750.0) * 0.5);
  }

  public virtual bool Raycast(Vector2 sp, Camera eventCamera)
  {
    RectTransform transform = ((Component) this).transform as RectTransform;
    Rect rect = transform.rect;
    float num = KuroObi.CalcObiSize();
    Vector2 vector2;
    RectTransformUtility.ScreenPointToLocalPointInRectangle(transform, sp, (Camera) null, ref vector2);
    return (double) vector2.y < (double) ((Rect) ref rect).yMin + (double) num || (double) ((Rect) ref rect).yMax - (double) num < (double) vector2.y;
  }

  protected virtual void OnPopulateMesh(VertexHelper vh)
  {
    vh.Clear();
    Color32 color32 = Color32.op_Implicit(this.color);
    Rect rect = (((Component) this).transform as RectTransform).rect;
    UIVertex uiVertex = new UIVertex();
    float num1 = KuroObi.CalcObiSize();
    uiVertex.position = Vector2.op_Implicit(new Vector2(((Rect) ref rect).xMin, ((Rect) ref rect).yMax));
    uiVertex.color = color32;
    vh.AddVert(uiVertex);
    uiVertex.position = Vector2.op_Implicit(new Vector2(((Rect) ref rect).xMax, ((Rect) ref rect).yMax));
    uiVertex.color = color32;
    vh.AddVert(uiVertex);
    float num2 = ((Rect) ref rect).yMax - num1;
    uiVertex.position = Vector2.op_Implicit(new Vector2(((Rect) ref rect).xMax, num2));
    uiVertex.color = color32;
    vh.AddVert(uiVertex);
    uiVertex.position = Vector2.op_Implicit(new Vector2(((Rect) ref rect).xMin, num2));
    uiVertex.color = color32;
    vh.AddVert(uiVertex);
    float num3 = ((Rect) ref rect).yMin + num1;
    uiVertex.position = Vector2.op_Implicit(new Vector2(((Rect) ref rect).xMin, num3));
    uiVertex.color = color32;
    vh.AddVert(uiVertex);
    uiVertex.position = Vector2.op_Implicit(new Vector2(((Rect) ref rect).xMax, num3));
    uiVertex.color = color32;
    vh.AddVert(uiVertex);
    uiVertex.position = Vector2.op_Implicit(new Vector2(((Rect) ref rect).xMax, ((Rect) ref rect).yMin));
    uiVertex.color = color32;
    vh.AddVert(uiVertex);
    uiVertex.position = Vector2.op_Implicit(new Vector2(((Rect) ref rect).xMin, ((Rect) ref rect).yMin));
    uiVertex.color = color32;
    vh.AddVert(uiVertex);
  }
}
