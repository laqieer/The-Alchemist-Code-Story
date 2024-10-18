// Decompiled with JetBrains decompiler
// Type: SRPG.ChallengeMissionRewardIconSelection
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class ChallengeMissionRewardIconSelection : MonoBehaviour
  {
    [SerializeField]
    private GameObject IconLarge;
    [SerializeField]
    private GameObject IconSmall;

    public void SetLarge(bool isLarge)
    {
      if (Object.op_Inequality((Object) this.IconLarge, (Object) null))
        this.IconLarge.SetActive(isLarge);
      if (!Object.op_Inequality((Object) this.IconSmall, (Object) null))
        return;
      this.IconSmall.SetActive(!isLarge);
    }
  }
}
