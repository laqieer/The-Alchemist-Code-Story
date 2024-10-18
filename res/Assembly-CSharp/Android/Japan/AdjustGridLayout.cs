// Decompiled with JetBrains decompiler
// Type: AdjustGridLayout
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class AdjustGridLayout : MonoBehaviour
{
  [SerializeField]
  private float OffsetSize;
  private float lastOffsetSize;
  private float initSize;

  private void Start()
  {
    GridLayoutGroup component = this.GetComponent<GridLayoutGroup>();
    if (!((Object) component != (Object) null))
      return;
    this.initSize = component.cellSize.x;
    this.SetGridSizeX();
  }

  private void SetGridSizeX()
  {
    GridLayoutGroup component = this.GetComponent<GridLayoutGroup>();
    if (!((Object) component != (Object) null))
      return;
    float num = SetCanvasBounds.GetSafeArea().width / (float) Screen.width;
    if ((double) num >= 1.0)
      return;
    Vector2 cellSize = component.cellSize;
    cellSize.x = this.initSize * num;
    cellSize.x += this.OffsetSize;
    component.cellSize = cellSize;
    this.lastOffsetSize = this.OffsetSize;
  }
}
