// Decompiled with JetBrains decompiler
// Type: SRPG.WorldMapUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.EventSystems;

namespace SRPG
{
  public class WorldMapUI : MonoBehaviour
  {
    public float ScrollSpeed = 0.01f;
    public UnityEngine.Camera TargetCamera;
    private bool mDragging;

    private void Start()
    {
      UIEventListener uiEventListener = this.gameObject.AddComponent<UIEventListener>();
      uiEventListener.onBeginDrag = new UIEventListener.PointerEvent(this.OnBeginDrag);
      uiEventListener.onEndDrag = new UIEventListener.PointerEvent(this.OnEndDrag);
      uiEventListener.onDrag = new UIEventListener.PointerEvent(this.OnDrag);
    }

    private void OnBeginDrag(PointerEventData eventData)
    {
      this.mDragging = true;
    }

    private void OnEndDrag(PointerEventData eventData)
    {
      this.mDragging = false;
    }

    private void OnDrag(PointerEventData eventData)
    {
      if (!this.mDragging || !((UnityEngine.Object) this.TargetCamera != (UnityEngine.Object) null))
        return;
      this.TargetCamera.transform.position -= new Vector3(eventData.delta.x * this.ScrollSpeed, 0.0f, eventData.delta.y * this.ScrollSpeed);
    }
  }
}
