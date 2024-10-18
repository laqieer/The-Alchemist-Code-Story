// Decompiled with JetBrains decompiler
// Type: SRPG.FriendPresentBadge
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class FriendPresentBadge : MonoBehaviour
  {
    public GameObject BadgeObject;
    [BitMask]
    public GameManager.BadgeTypes BadgeType;

    private void Start()
    {
      if (!Object.op_Inequality((Object) this.BadgeObject, (Object) null))
        return;
      this.BadgeObject.SetActive(false);
    }

    private void Update()
    {
      if (Object.op_Equality((Object) this.BadgeObject, (Object) null))
        return;
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (!Object.op_Inequality((Object) instanceDirect, (Object) null))
        return;
      bool flag = instanceDirect.CheckBadges(this.BadgeType);
      if (instanceDirect.Player != null)
        flag |= instanceDirect.Player.ValidFriendPresent;
      this.BadgeObject.SetActive(flag);
    }
  }
}
