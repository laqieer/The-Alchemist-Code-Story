// Decompiled with JetBrains decompiler
// Type: SwitchDemoFrame
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
public class SwitchDemoFrame : MonoBehaviour
{
  [SerializeField]
  private GameObject Frame;

  private void Start()
  {
    if (!Object.op_Inequality((Object) this.Frame, (Object) null))
      return;
    Rect safeArea = SetCanvasBounds.GetSafeArea();
    int x = (int) SetCanvasBounds.GetAddFrame().x;
    if ((double) ((Rect) ref safeArea).width >= (double) Screen.width && x <= 0)
      return;
    this.Frame.SetActive(true);
  }
}
