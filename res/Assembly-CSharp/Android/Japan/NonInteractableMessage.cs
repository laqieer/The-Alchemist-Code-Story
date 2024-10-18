// Decompiled with JetBrains decompiler
// Type: NonInteractableMessage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof (Selectable))]
public class NonInteractableMessage : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
{
  public string Caption;
  public string Message;

  public void OnPointerClick(PointerEventData eventData)
  {
    if (this.GetComponent<Selectable>().interactable)
      return;
    UIUtility.NegativeSystemMessage(LocalizedText.Get(this.Caption), LocalizedText.Get(this.Message), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
  }
}
