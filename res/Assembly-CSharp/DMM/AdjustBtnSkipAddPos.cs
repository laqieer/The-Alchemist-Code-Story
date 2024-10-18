// Decompiled with JetBrains decompiler
// Type: AdjustBtnSkipAddPos
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
public class AdjustBtnSkipAddPos : MonoBehaviour
{
  private void Awake()
  {
    RectTransform component = ((Component) this).GetComponent<RectTransform>();
    Vector2 anchoredPosition = component.anchoredPosition;
    int x = (int) SetCanvasBounds.GetAddFrame().x;
    anchoredPosition.x -= (float) x;
    component.anchoredPosition = anchoredPosition;
  }
}
