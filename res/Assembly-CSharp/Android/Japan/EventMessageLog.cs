// Decompiled with JetBrains decompiler
// Type: EventMessageLog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using SRPG;
using UnityEngine;

public class EventMessageLog : MonoBehaviour
{
  private void Awake()
  {
    RectTransform component = this.GetComponent<RectTransform>();
    float num = (float) Screen.height - SetCanvasBounds.GetSafeArea().height;
    component.anchoredPosition = new Vector2(component.anchoredPosition.x, component.anchoredPosition.y + num);
  }

  public void OnClick()
  {
    if (EventScript.OnBackLogButtonClicked != null)
      EventScript.OnBackLogButtonClicked();
    EventScript.OpenBackLog();
  }

  private void OnDisable()
  {
    EventScript.CloseBackLog();
  }
}
