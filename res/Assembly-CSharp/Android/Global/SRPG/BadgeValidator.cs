// Decompiled with JetBrains decompiler
// Type: SRPG.BadgeValidator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [DisallowMultipleComponent]
  public class BadgeValidator : MonoBehaviour
  {
    [BitMask]
    public GameManager.BadgeTypes BadgeType;

    private void Update()
    {
      this.UpdateBadge();
    }

    private void UpdateBadge()
    {
      if (this.BadgeType == ~GameManager.BadgeTypes.All)
        return;
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if ((UnityEngine.Object) instanceDirect == (UnityEngine.Object) null || instanceDirect.CheckBusyBadges(this.BadgeType))
        return;
      this.gameObject.SetActive(MonoSingleton<GameManager>.GetInstanceDirect().CheckBadges(this.BadgeType));
    }
  }
}
