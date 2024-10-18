// Decompiled with JetBrains decompiler
// Type: SRPG.AspectRatioScaleFitter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [ExecuteInEditMode]
  public class AspectRatioScaleFitter : MonoBehaviour
  {
    [SerializeField]
    private Vector2 Padding = Vector2.zero;
    [SerializeField]
    private float ScaleForSafeArea = 1f;
    private Rect lastSafeArea = new Rect(0.0f, 0.0f, 0.0f, 0.0f);
    private Vector3 initScale = Vector3.one;
    private float lastSetScale;
    private int lastScreenWidth;
    private int lastScreenHeight;

    private void Start()
    {
      RectTransform component = ((Component) this).GetComponent<RectTransform>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      this.initScale = ((Component) component).transform.localScale;
    }

    private void OnRectTransformDimensionsChange()
    {
      this.Refresh(SetCanvasBounds.GetSafeArea(true));
    }

    private void Refresh(Rect safeArea)
    {
      this.lastScreenWidth = Screen.width;
      this.lastScreenHeight = Screen.height;
      if (this.ApplySafeAreaScale(safeArea))
        return;
      this.Rescale();
    }

    private void Rescale()
    {
      RectTransform component = ((Component) this).GetComponent<RectTransform>();
      Transform parent = ((Component) this).transform.parent;
      if (Object.op_Equality((Object) parent, (Object) null))
        return;
      Rect rect1 = (parent as RectTransform).rect;
      float width = ((Rect) ref rect1).width;
      Rect rect2 = component.rect;
      float num1 = ((Rect) ref rect2).width + this.Padding.x + this.Padding.y;
      float num2 = width / num1;
      ((Transform) component).localScale = new Vector3(num2, num2, 1f);
    }

    private bool ApplySafeAreaScale(Rect area)
    {
      RectTransform component = ((Component) this).GetComponent<RectTransform>();
      bool flag = false;
      if (Object.op_Inequality((Object) component, (Object) null))
      {
        if ((double) (((Rect) ref area).width / (float) Screen.width) < 1.0)
        {
          flag = true;
          ((Transform) component).localScale = new Vector3(this.ScaleForSafeArea, this.ScaleForSafeArea, this.ScaleForSafeArea);
        }
        else
          ((Transform) component).localScale = this.initScale;
      }
      this.lastSafeArea = area;
      this.lastSetScale = this.ScaleForSafeArea;
      return flag;
    }
  }
}
