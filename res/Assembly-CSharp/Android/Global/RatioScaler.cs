// Decompiled with JetBrains decompiler
// Type: RatioScaler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class RatioScaler : MonoBehaviour
{
  private const double ratioToBeFix = 2.0;

  private void Awake()
  {
    if ((double) Screen.width / (double) Screen.height < 2.0)
      return;
    if ((double) this.transform.localScale.x < 1.0 || (double) this.transform.localScale.y < 1.0)
      this.transform.localScale = new Vector3(1.2f, 1.2f, this.transform.localScale.z * 1f);
    else
      this.transform.localScale = new Vector3(this.transform.localScale.x * 1.2f, this.transform.localScale.y * 1.2f, this.transform.localScale.z * 1f);
    for (Transform transform = this.transform; (Object) transform != (Object) null; transform = transform.parent)
    {
      Debug.Log((object) transform.name);
      if (transform.name.ToLower().Contains("demo_cut") || transform.name.ToLower().Contains("splash_base"))
      {
        Debug.Log((object) "found demo cut");
        this.GetComponent<SRPG_CanvasScaler>().matchWidthOrHeight = 0.5f;
        this.transform.localScale = new Vector3(1f, 1f, 1f);
        break;
      }
    }
  }
}
