// Decompiled with JetBrains decompiler
// Type: SRPG.BaseIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class BaseIcon : 
    MonoBehaviour,
    IGameParameter,
    IPointerDownHandler,
    IHoldGesture,
    IEventSystemHandler
  {
    public virtual bool HasTooltip => true;

    protected virtual void ShowTooltip(Vector2 screen)
    {
    }

    protected virtual void ShowTooltipByTap(Vector2 _screen)
    {
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
      HoldGestureObserver.StartHoldGesture((IHoldGesture) this);
    }

    public void OnPointerHoldStart()
    {
      if (!this.HasTooltip)
        return;
      RectTransform transform = (RectTransform) ((Component) this).transform;
      Vector2 screen = Vector2.op_Implicit(((Transform) transform).TransformPoint(Vector2.op_Implicit(Vector2.zero)));
      CanvasScaler componentInParent = ((Component) transform).GetComponentInParent<CanvasScaler>();
      if (Object.op_Inequality((Object) componentInParent, (Object) null))
      {
        Vector3 localScale = ((Component) componentInParent).transform.localScale;
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
