// Decompiled with JetBrains decompiler
// Type: ExitGames.UtilityScripts.ButtonInsideScrollList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ExitGames.UtilityScripts
{
  public class ButtonInsideScrollList : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IEventSystemHandler
  {
    private ScrollRect scrollRect;

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

    private void Start()
    {
      this.scrollRect = this.GetComponentInParent<ScrollRect>();
    }
  }
}
