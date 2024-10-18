// Decompiled with JetBrains decompiler
// Type: ChargeIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
public class ChargeIcon : MonoBehaviour
{
  public GameObject ChargeIconPrefab;
  private GameObject mChargeIcon;

  private void Start()
  {
    if (!Object.op_Implicit((Object) this.ChargeIconPrefab))
      return;
    this.mChargeIcon = Object.Instantiate<GameObject>(this.ChargeIconPrefab, ((Component) this).transform.position, ((Component) this).transform.rotation);
    if (!Object.op_Inequality((Object) this.mChargeIcon, (Object) null))
      return;
    this.mChargeIcon.transform.SetParent(((Component) this).transform);
    this.mChargeIcon.SetActive(false);
  }

  public void Open()
  {
    if (!Object.op_Inequality((Object) this.mChargeIcon, (Object) null) || this.mChargeIcon.activeSelf)
      return;
    this.mChargeIcon.SetActive(true);
  }

  public void Close()
  {
    if (!Object.op_Inequality((Object) this.mChargeIcon, (Object) null) || !this.mChargeIcon.activeSelf)
      return;
    this.mChargeIcon.SetActive(false);
  }
}
