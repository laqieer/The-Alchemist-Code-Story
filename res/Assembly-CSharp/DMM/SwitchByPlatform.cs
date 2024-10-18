// Decompiled with JetBrains decompiler
// Type: SwitchByPlatform
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
public class SwitchByPlatform : MonoBehaviour
{
  [SerializeField]
  public RuntimePlatform[] hides = new RuntimePlatform[0];

  private void Start()
  {
    foreach (int hide in this.hides)
    {
      if (Application.platform == (RuntimePlatform) hide)
        ((Component) this).gameObject.SetActive(false);
    }
  }
}
