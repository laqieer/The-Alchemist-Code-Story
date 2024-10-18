// Decompiled with JetBrains decompiler
// Type: HoldGesture
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

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
