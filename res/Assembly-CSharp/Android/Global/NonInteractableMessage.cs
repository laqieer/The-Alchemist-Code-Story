// Decompiled with JetBrains decompiler
// Type: NonInteractableMessage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
