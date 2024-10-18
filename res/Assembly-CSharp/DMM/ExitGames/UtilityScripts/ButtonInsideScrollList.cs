// Decompiled with JetBrains decompiler
// Type: ExitGames.UtilityScripts.ButtonInsideScrollList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable disable
namespace ExitGames.UtilityScripts
{
  public class ButtonInsideScrollList : 
    MonoBehaviour,
    IPointerDownHandler,
    IPointerUpHandler,
    IEventSystemHandler
  {
    private ScrollRect scrollRect;

    private void Start() => this.scrollRect = ((Component) this).GetComponentInParent<ScrollRect>();

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
      if (!Object.op_Inequality((Object) this.scrollRect, (Object) null))
        return;
      this.scrollRect.StopMovement();
      ((Behaviour) this.scrollRect).enabled = false;
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
      if (!Object.op_Inequality((Object) this.scrollRect, (Object) null) || ((Behaviour) this.scrollRect).enabled)
        return;
      ((Behaviour) this.scrollRect).enabled = true;
    }
  }
}
