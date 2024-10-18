// Decompiled with JetBrains decompiler
// Type: NonInteractableMessage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable disable
[RequireComponent(typeof (Selectable))]
public class NonInteractableMessage : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
{
  public string Caption;
  public string Message;

  public void OnPointerClick(PointerEventData eventData)
  {
    if (((Component) this).GetComponent<Selectable>().interactable)
      return;
    UIUtility.NegativeSystemMessage(LocalizedText.Get(this.Caption), LocalizedText.Get(this.Message), (UIUtility.DialogResultEvent) null);
  }
}
