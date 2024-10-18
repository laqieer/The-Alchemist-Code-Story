// Decompiled with JetBrains decompiler
// Type: ChargeIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class ChargeIcon : MonoBehaviour
{
  public GameObject ChargeIconPrefab;
  private GameObject mChargeIcon;

  private void Start()
  {
    if (!(bool) ((Object) this.ChargeIconPrefab))
      return;
    this.mChargeIcon = Object.Instantiate((Object) this.ChargeIconPrefab, this.transform.position, this.transform.rotation) as GameObject;
    if (!((Object) this.mChargeIcon != (Object) null))
      return;
    this.mChargeIcon.transform.SetParent(this.transform);
    this.mChargeIcon.SetActive(false);
  }

  public void Open()
  {
    if (!((Object) this.mChargeIcon != (Object) null) || this.mChargeIcon.activeSelf)
      return;
    this.mChargeIcon.SetActive(true);
  }

  public void Close()
  {
    if (!((Object) this.mChargeIcon != (Object) null) || !this.mChargeIcon.activeSelf)
      return;
    this.mChargeIcon.SetActive(false);
  }
}
