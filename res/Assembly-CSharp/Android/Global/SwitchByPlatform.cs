// Decompiled with JetBrains decompiler
// Type: SwitchByPlatform
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class SwitchByPlatform : MonoBehaviour
{
  [SerializeField]
  public RuntimePlatform[] hides = new RuntimePlatform[0];
  public bool CheckAmazonFlag;

  private void Start()
  {
    foreach (RuntimePlatform hide in this.hides)
    {
      if (Application.platform == hide)
        this.gameObject.SetActive(false);
    }
    if (this.CheckAmazonFlag)
      ;
  }
}
