// Decompiled with JetBrains decompiler
// Type: SRPG.BaseIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SRPG
{
  public class BaseIcon : MonoBehaviour, IGameParameter, IPointerDownHandler, IHoldGesture, IEventSystemHandler
  {
    public virtual bool HasTooltip
    {
      get
      {
        return true;
      }
    }

    protected virtual void ShowTooltip(Vector2 screen)
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
      HoldGestureObserver.StartHoldGesture((IHoldGesture) this);
    }

    public void OnPointerHoldStart()
    {
      if (!this.HasTooltip)
        return;
      RectTransform transform = (RectTransform) this.transform;
      Vector2 screen = (Vector2) transform.TransformPoint((Vector3) Vector2.zero);
      CanvasScaler componentInParent = transform.GetComponentInParent<CanvasScaler>();
      if ((UnityEngine.Object) componentInParent != (UnityEngine.Object) null)
      {
        Vector3 localScale = componentInParent.transform.localScale;
        screen.x /= localScale.x;
        screen.y /= localScale.y;
      }
      this.ShowTooltip(screen);
    }

    public void OnPointerHoldEnd()
    {
    }

    public virtual void UpdateValue()
    {
    }
  }
}
