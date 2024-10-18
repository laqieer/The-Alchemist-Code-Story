// Decompiled with JetBrains decompiler
// Type: HoldGesture
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

#nullable disable
[AddComponentMenu("Event/Hold Gesture")]
public class HoldGesture : MonoBehaviour, IPointerDownHandler, IHoldGesture, IEventSystemHandler
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
