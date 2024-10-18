// Decompiled with JetBrains decompiler
// Type: AdjustGridLayout
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class AdjustGridLayout : MonoBehaviour
{
  [SerializeField]
  private float OffsetSize;
  private float lastOffsetSize;
  private float initSize;

  private void Start()
  {
    GridLayoutGroup component = ((Component) this).GetComponent<GridLayoutGroup>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    this.initSize = component.cellSize.x;
    this.SetGridSizeX();
  }

  private void SetGridSizeX()
  {
    GridLayoutGroup component = ((Component) this).GetComponent<GridLayoutGroup>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    Rect safeArea = SetCanvasBounds.GetSafeArea();
    float num = ((Rect) ref safeArea).width / (float) Screen.width;
    if ((double) num >= 1.0)
      return;
    Vector2 cellSize = component.cellSize;
    cellSize.x = this.initSize * num;
    cellSize.x += this.OffsetSize;
    component.cellSize = cellSize;
    this.lastOffsetSize = this.OffsetSize;
  }
}
