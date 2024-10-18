// Decompiled with JetBrains decompiler
// Type: FixLineSpacing
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof (Text))]
public class FixLineSpacing : MonoBehaviour
{
  public bool NoBestFit;

  private void OnEnable()
  {
  }

  private void Awake()
  {
    if (!this.enabled)
      return;
    Text component = this.GetComponent<Text>();
    component.lineSpacing *= 2f;
    if (this.NoBestFit)
      component.resizeTextForBestFit = false;
    this.enabled = false;
  }
}
