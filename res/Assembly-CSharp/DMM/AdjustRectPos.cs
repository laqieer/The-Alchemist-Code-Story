// Decompiled with JetBrains decompiler
// Type: AdjustRectPos
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
public class AdjustRectPos : MonoBehaviour
{
  [SerializeField]
  private Vector3 SetOffsetPos = Vector3.zero;
  private Rect lastSafeArea = new Rect(0.0f, 0.0f, 0.0f, 0.0f);
  private Vector3 initPos = Vector3.zero;
  private Vector3 lastSetOffsetPos = Vector3.zero;

  private void Start()
  {
    RectTransform component = ((Component) this).GetComponent<RectTransform>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    this.initPos = ((Transform) component).localPosition;
    this.ApplySafeAreaPos(SetCanvasBounds.GetSafeArea());
  }

  private void ApplySafeAreaPos(Rect area)
  {
    RectTransform component = ((Component) this).GetComponent<RectTransform>();
    if (Object.op_Inequality((Object) component, (Object) null))
    {
      if ((double) (((Rect) ref area).width / (float) Screen.width) < 1.0)
        ((Transform) component).localPosition = Vector3.op_Addition(this.initPos, this.SetOffsetPos);
      else
        ((Transform) component).localPosition = this.initPos;
    }
    this.lastSafeArea = area;
    this.lastSetOffsetPos = this.SetOffsetPos;
  }
}
