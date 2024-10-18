// Decompiled with JetBrains decompiler
// Type: GpsGiftIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using SRPG;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class GpsGiftIcon : MonoBehaviour
{
  private Image m_Image;

  private void Start()
  {
    this.m_Image = ((Component) this).gameObject.GetComponent<Image>();
    if (Object.op_Inequality((Object) this.m_Image, (Object) null))
      ((Behaviour) this.m_Image).enabled = false;
    ((Component) this).gameObject.SetActive(false);
  }

  private void Update()
  {
    if (Object.op_Equality((Object) this.m_Image, (Object) null))
      return;
    GameManager instance = MonoSingleton<GameManager>.Instance;
    if (!Object.op_Inequality((Object) instance, (Object) null) || instance.Player == null)
      return;
    if (instance.Player.ValidGpsGift)
      ((Behaviour) this.m_Image).enabled = true;
    else
      ((Behaviour) this.m_Image).enabled = false;
  }
}
