// Decompiled with JetBrains decompiler
// Type: AdjustRectPos
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class AdjustRectPos : MonoBehaviour
{
  [SerializeField]
  private Vector3 SetOffsetPos = Vector3.zero;
  private Rect lastSafeArea = new Rect(0.0f, 0.0f, 0.0f, 0.0f);
  private Vector3 initPos = Vector3.zero;
  private Vector3 lastSetOffsetPos = Vector3.zero;

  private void Start()
  {
    RectTransform component = this.GetComponent<RectTransform>();
    if (!((Object) component != (Object) null))
      return;
    this.initPos = component.localPosition;
    this.ApplySafeAreaPos(SetCanvasBounds.GetSafeArea());
  }

  private void ApplySafeAreaPos(Rect area)
  {
    RectTransform component = this.GetComponent<RectTransform>();
    if ((Object) component != (Object) null)
    {
      if ((double) (area.width / (float) Screen.width) < 1.0)
        component.localPosition = this.initPos + this.SetOffsetPos;
      else
        component.localPosition = this.initPos;
    }
    this.lastSafeArea = area;
    this.lastSetOffsetPos = this.SetOffsetPos;
  }
}
