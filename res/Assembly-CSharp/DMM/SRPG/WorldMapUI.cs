// Decompiled with JetBrains decompiler
// Type: SRPG.WorldMapUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.EventSystems;

#nullable disable
namespace SRPG
{
  public class WorldMapUI : MonoBehaviour
  {
    public Camera TargetCamera;
    private bool mDragging;
    public float ScrollSpeed = 0.01f;

    private void Start()
    {
      UIEventListener uiEventListener = ((Component) this).gameObject.AddComponent<UIEventListener>();
      uiEventListener.onBeginDrag = new UIEventListener.PointerEvent(this.OnBeginDrag);
      uiEventListener.onEndDrag = new UIEventListener.PointerEvent(this.OnEndDrag);
      uiEventListener.onDrag = new UIEventListener.PointerEvent(this.OnDrag);
    }

    private void OnBeginDrag(PointerEventData eventData) => this.mDragging = true;

    private void OnEndDrag(PointerEventData eventData) => this.mDragging = false;

    private void OnDrag(PointerEventData eventData)
    {
      if (!this.mDragging || !Object.op_Inequality((Object) this.TargetCamera, (Object) null))
        return;
      Transform transform = ((Component) this.TargetCamera).transform;
      transform.position = Vector3.op_Subtraction(transform.position, new Vector3(eventData.delta.x * this.ScrollSpeed, 0.0f, eventData.delta.y * this.ScrollSpeed));
    }
  }
}
