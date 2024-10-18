// Decompiled with JetBrains decompiler
// Type: GpsGiftIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
    if ((UnityEngine.Object) this.m_Image != (UnityEngine.Object) null)
      this.m_Image.enabled = false;
    this.gameObject.SetActive(false);
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
