// Decompiled with JetBrains decompiler
// Type: DragEventListener
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.EventSystems;

public class DragEventListener : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IEventSystemHandler
{
  public DragEventListener.BeginDragDelegate BeginDrag;
  public DragEventListener.BeginDragDelegate Drag;
  public DragEventListener.BeginDragDelegate EndDrag;

  public void OnBeginDrag(PointerEventData eventData)
  {
    if (this.BeginDrag == null)
      return;
    this.BeginDrag(this.gameObject, eventData);
  }

  public void OnDrag(PointerEventData eventData)
  {
    if (this.Drag == null)
      return;
    this.Drag(this.gameObject, eventData);
  }

  public void OnEndDrag(PointerEventData eventData)
  {
    if (this.EndDrag == null)
      return;
    this.EndDrag(this.gameObject, eventData);
  }

  public delegate void BeginDragDelegate(GameObject sender, PointerEventData eventData);

  public delegate void DragDelegate(GameObject sender, PointerEventData eventData);

  public delegate void EndDragDelegate(GameObject sender, PointerEventData eventData);
}
