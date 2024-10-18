﻿// Decompiled with JetBrains decompiler
// Type: UIEventListener
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.EventSystems;

public class UIEventListener : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IScrollHandler, IPointerDownHandler, IPointerClickHandler, ISelectHandler, IPointerUpHandler, IDropHandler, IPointerExitHandler, IEventSystemHandler, IPointerEnterHandler, IUpdateSelectedHandler, IDeselectHandler, IMoveHandler
{
  public UIEventListener.PointerEvent onPointerEnter;
  public UIEventListener.PointerEvent onPointerExit;
  public UIEventListener.PointerEvent onPointerDown;
  public UIEventListener.PointerEvent onPointerUp;
  public UIEventListener.PointerEvent onPointerClick;
  public UIEventListener.PointerEvent onDrag;
  public UIEventListener.PointerEvent onBeginDrag;
  public UIEventListener.PointerEvent onEndDrag;
  public UIEventListener.PointerEvent onDrop;
  public UIEventListener.PointerEvent onScroll;
  public UIEventListener.BaseEvent onSelect;
  public UIEventListener.BaseEvent onDeselect;
  public UIEventListener.BaseEvent onUpdateSelected;
  public UIEventListener.AxisEvent onMove;

  public static UIEventListener Get(GameObject go)
  {
    UIEventListener component = go.GetComponent<UIEventListener>();
    if ((Object) component != (Object) null)
      return component;
    return go.AddComponent<UIEventListener>();
  }

  public static UIEventListener Get(Component go)
  {
    return UIEventListener.Get(go.gameObject);
  }

  public void OnPointerEnter(PointerEventData eventData)
  {
    if (this.onPointerEnter == null)
      return;
    this.onPointerEnter(eventData);
  }

  public void OnPointerExit(PointerEventData eventData)
  {
    if (this.onPointerExit == null)
      return;
    this.onPointerExit(eventData);
  }

  public void OnPointerDown(PointerEventData eventData)
  {
    if (this.onPointerDown == null)
      return;
    this.onPointerDown(eventData);
  }

  public void OnPointerUp(PointerEventData eventData)
  {
    if (this.onPointerUp == null)
      return;
    this.onPointerUp(eventData);
  }

  public void OnPointerClick(PointerEventData eventData)
  {
    if (this.onPointerClick == null)
      return;
    this.onPointerClick(eventData);
  }

  public void OnBeginDrag(PointerEventData eventData)
  {
    if (this.onBeginDrag == null)
      return;
    this.onBeginDrag(eventData);
  }

  public void OnEndDrag(PointerEventData eventData)
  {
    if (this.onEndDrag == null)
      return;
    this.onEndDrag(eventData);
  }

  public void OnDrag(PointerEventData eventData)
  {
    if (this.onDrag == null)
      return;
    this.onDrag(eventData);
  }

  public void OnDrop(PointerEventData eventData)
  {
    if (this.onDrop == null)
      return;
    this.onDrop(eventData);
  }

  public void OnScroll(PointerEventData eventData)
  {
    if (this.onScroll == null)
      return;
    this.onScroll(eventData);
  }

  public void OnUpdateSelected(BaseEventData eventData)
  {
    if (this.onUpdateSelected == null)
      return;
    this.onUpdateSelected(eventData);
  }

  public void OnSelect(BaseEventData eventData)
  {
    if (this.onSelect == null)
      return;
    this.onSelect(eventData);
  }

  public void OnDeselect(BaseEventData eventData)
  {
    if (this.onDeselect == null)
      return;
    this.onDeselect(eventData);
  }

  public void OnMove(AxisEventData eventData)
  {
    if (this.onMove == null)
      return;
    this.onMove(eventData);
  }

  public delegate void PointerEvent(PointerEventData eventData);

  public delegate void BaseEvent(BaseEventData eventData);

  public delegate void AxisEvent(AxisEventData eventData);
}
