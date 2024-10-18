// Decompiled with JetBrains decompiler
// Type: ExitGames.UtilityScripts.ButtonInsideScrollList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ExitGames.UtilityScripts
{
  public class ButtonInsideScrollList : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IEventSystemHandler
  {
    private ScrollRect scrollRect;

    private void Start()
    {
      this.scrollRect = this.GetComponentInParent<ScrollRect>();
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
      if (!((Object) this.scrollRect != (Object) null))
        return;
      this.scrollRect.StopMovement();
      this.scrollRect.enabled = false;
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
      if (!((Object) this.scrollRect != (Object) null) || this.scrollRect.enabled)
        return;
      this.scrollRect.enabled = true;
    }
  }
}
