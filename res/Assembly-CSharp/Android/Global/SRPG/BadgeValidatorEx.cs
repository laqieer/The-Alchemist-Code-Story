// Decompiled with JetBrains decompiler
// Type: SRPG.BadgeValidatorEx
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [DisallowMultipleComponent]
  public class BadgeValidatorEx : BadgeValidator
  {
    [BitMask]
    public GameManager.BadgeTypes PriorityBadgeType;

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
      int priorityBadgeType = (int) this.PriorityBadgeType;
      bool flag = instanceDirect.CheckBadges(this.BadgeType);
      if (priorityBadgeType != 0 && instanceDirect.CheckBadges(this.PriorityBadgeType))
        flag = false;
      this.gameObject.SetActive(flag);
    }
  }
}
