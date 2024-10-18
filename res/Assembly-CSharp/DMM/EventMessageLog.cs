// Decompiled with JetBrains decompiler
// Type: EventMessageLog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using SRPG;
using UnityEngine;

#nullable disable
public class EventMessageLog : MonoBehaviour
{
  private void Awake()
  {
    RectTransform component = ((Component) this).GetComponent<RectTransform>();
    Rect safeArea = SetCanvasBounds.GetSafeArea();
    if ((double) (((Rect) ref safeArea).width / (float) Screen.width) >= 1.0)
      return;
    float num = (float) Screen.height - ((Rect) ref safeArea).height;
    component.anchoredPosition = new Vector2(component.anchoredPosition.x, component.anchoredPosition.y + num);
  }

  public void OnClick()
  {
    if (EventScript.OnBackLogButtonClicked != null)
      EventScript.OnBackLogButtonClicked();
    EventScript.OpenBackLog();
  }

  private void OnDisable() => EventScript.CloseBackLog();
}
