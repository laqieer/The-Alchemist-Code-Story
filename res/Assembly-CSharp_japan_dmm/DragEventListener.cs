// Decompiled with JetBrains decompiler
// Type: DragEventListener
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.EventSystems;

#nullable disable
public class DragEventListener : 
  MonoBehaviour,
  IDragHandler,
  IBeginDragHandler,
  IEndDragHandler,
  IEventSystemHandler
{
  public DragEventListener.BeginDragDelegate BeginDrag;
  public DragEventListener.BeginDragDelegate Drag;
  public DragEventListener.BeginDragDelegate EndDrag;

  public void OnBeginDrag(PointerEventData eventData)
  {
    if (this.BeginDrag == null)
      return;
    this.BeginDrag(((Component) this).gameObject, eventData);
  }

  public void OnDrag(PointerEventData eventData)
  {
    if (this.Drag == null)
      return;
    this.Drag(((Component) this).gameObject, eventData);
  }

  public void OnEndDrag(PointerEventData eventData)
  {
    if (this.EndDrag == null)
      return;
    this.EndDrag(((Component) this).gameObject, eventData);
  }

  public delegate void BeginDragDelegate(GameObject sender, PointerEventData eventData);

  public delegate void DragDelegate(GameObject sender, PointerEventData eventData);

  public delegate void EndDragDelegate(GameObject sender, PointerEventData eventData);
}
