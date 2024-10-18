// Decompiled with JetBrains decompiler
// Type: FixLineSpacing
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
[RequireComponent(typeof (Text))]
public class FixLineSpacing : MonoBehaviour
{
  public bool NoBestFit;

  private void OnEnable()
  {
  }

  private void Awake()
  {
    if (!((Behaviour) this).enabled)
      return;
    Text component = ((Component) this).GetComponent<Text>();
    component.lineSpacing *= 2f;
    if (this.NoBestFit)
      component.resizeTextForBestFit = false;
    ((Behaviour) this).enabled = false;
  }
}
