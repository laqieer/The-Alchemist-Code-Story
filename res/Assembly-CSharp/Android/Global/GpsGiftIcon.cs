// Decompiled with JetBrains decompiler
// Type: GpsGiftIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using SRPG;
using UnityEngine;
using UnityEngine.UI;

public class GpsGiftIcon : MonoBehaviour
{
  private Image m_Image;

  private void Start()
  {
    this.m_Image = this.gameObject.GetComponent<Image>();
    if (!((UnityEngine.Object) this.m_Image != (UnityEngine.Object) null))
      return;
    this.m_Image.enabled = false;
  }

  private void Update()
  {
    if ((UnityEngine.Object) this.m_Image == (UnityEngine.Object) null)
      return;
    GameManager instance = MonoSingleton<GameManager>.Instance;
    if (!((UnityEngine.Object) instance != (UnityEngine.Object) null) || instance.Player == null)
      return;
    if (instance.Player.ValidGpsGift)
      this.m_Image.enabled = true;
    else
      this.m_Image.enabled = false;
  }
}
