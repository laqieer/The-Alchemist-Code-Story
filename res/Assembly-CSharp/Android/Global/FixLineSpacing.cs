// Decompiled with JetBrains decompiler
// Type: FixLineSpacing
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
