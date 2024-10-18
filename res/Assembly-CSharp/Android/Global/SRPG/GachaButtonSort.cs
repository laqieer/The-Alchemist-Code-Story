// Decompiled with JetBrains decompiler
// Type: SRPG.GachaButtonSort
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  public class GachaButtonSort : MonoBehaviour
  {
    [BitMask]
    public GameManager.BadgeTypes BadgeType;

    private void Update()
    {
      this.UpdateButtonPlacement();
    }

    private void UpdateButtonPlacement()
    {
      if (this.BadgeType == ~GameManager.BadgeTypes.All)
        return;
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if ((UnityEngine.Object) instanceDirect == (UnityEngine.Object) null || instanceDirect.CheckBusyBadges(this.BadgeType))
        return;
      if (this.BadgeType == GameManager.BadgeTypes.RareGacha)
      {
        if (MonoSingleton<GameManager>.GetInstanceDirect().CheckBadges(this.BadgeType))
          this.transform.SetAsFirstSibling();
        else
          this.transform.SetSiblingIndex(this.transform.parent.childCount - 3);
      }
      else
      {
        if (this.BadgeType != GameManager.BadgeTypes.GoldGacha)
          return;
        if (MonoSingleton<GameManager>.GetInstanceDirect().CheckBadges(this.BadgeType))
        {
          if (MonoSingleton<GameManager>.GetInstanceDirect().CheckBadges(GameManager.BadgeTypes.RareGacha))
            this.transform.SetSiblingIndex(1);
          else
            this.transform.SetAsFirstSibling();
        }
        else
          this.transform.SetSiblingIndex(this.transform.parent.childCount - 2);
      }
    }
  }
}
