// Decompiled with JetBrains decompiler
// Type: KuroObi
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class KuroObi : Graphic
{
  public static float CalcObiSize()
  {
    if (!SRPG_CanvasScaler.UseKuroObi)
      return 0.0f;
    float num1 = (float) Screen.width / (float) Screen.height;
    float num2 = 1.6f;
    if ((double) num1 >= (double) num2)
      return 0.0f;
    return (float) ((750.0 / (double) (num1 / num2) - 750.0) * 0.5);
  }

  public override bool Raycast(Vector2 sp, Camera eventCamera)
  {
    RectTransform transform = this.transform as RectTransform;
    Rect rect = transform.rect;
    float num = KuroObi.CalcObiSize();
    Vector2 localPoint;
    RectTransformUtility.ScreenPointToLocalPointInRectangle(transform, sp, (Camera) null, out localPoint);
    return (double) localPoint.y < (double) rect.yMin + (double) num || (double) rect.yMax - (double) num < (double) localPoint.y;
  }

  protected override void OnPopulateMesh(VertexHelper vh)
  {
    vh.Clear();
    Color32 color = (Color32) this.color;
    Rect rect = (this.transform as RectTransform).rect;
    UIVertex v = new UIVertex();
    float num = KuroObi.CalcObiSize();
    v.position = (Vector3) new Vector2(rect.xMin, rect.yMax);
    v.color = color;
    vh.AddVert(v);
    v.position = (Vector3) new Vector2(rect.xMax, rect.yMax);
    v.color = color;
    vh.AddVert(v);
    float y1 = rect.yMax - num;
    v.position = (Vector3) new Vector2(rect.xMax, y1);
    v.color = color;
    vh.AddVert(v);
    v.position = (Vector3) new Vector2(rect.xMin, y1);
    v.color = color;
    vh.AddVert(v);
    float y2 = rect.yMin + num;
    v.position = (Vector3) new Vector2(rect.xMin, y2);
    v.color = color;
    vh.AddVert(v);
    v.position = (Vector3) new Vector2(rect.xMax, y2);
    v.color = color;
    vh.AddVert(v);
    v.position = (Vector3) new Vector2(rect.xMax, rect.yMin);
    v.color = color;
    vh.AddVert(v);
    v.position = (Vector3) new Vector2(rect.xMin, rect.yMin);
    v.color = color;
    vh.AddVert(v);
  }
}
