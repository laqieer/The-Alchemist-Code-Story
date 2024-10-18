// Decompiled with JetBrains decompiler
// Type: ChargeIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class ChargeIcon : MonoBehaviour
{
  public GameObject ChargeIconPrefab;
  private GameObject mChargeIcon;

  private void Start()
  {
    if (!(bool) ((Object) this.ChargeIconPrefab))
      return;
    this.mChargeIcon = Object.Instantiate<GameObject>(this.ChargeIconPrefab, this.transform.position, this.transform.rotation);
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
