// Decompiled with JetBrains decompiler
// Type: SRPG.BadgeValidator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
