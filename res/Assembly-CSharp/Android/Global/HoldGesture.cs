// Decompiled with JetBrains decompiler
// Type: HoldGesture
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[AddComponentMenu("Event/Hold Gesture")]
public class HoldGesture : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IHoldGesture
{
  public UnityEvent OnHoldStart;
  public UnityEvent OnHoldEnd;

  public void OnPointerHoldStart()
  {
    if (this.OnHoldStart == null)
      return;
    this.OnHoldStart.Invoke();
  }

  public void OnPointerHoldEnd()
  {
    if (this.OnHoldEnd == null)
      return;
    this.OnHoldEnd.Invoke();
  }

  public void OnPointerDown(PointerEventData eventData)
  {
    HoldGestureObserver.StartHoldGesture((IHoldGesture) this);
  }
}
