// Decompiled with JetBrains decompiler
// Type: SRPG.FriendPresentBadge
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  public class FriendPresentBadge : MonoBehaviour
  {
    public GameObject BadgeObject;
    [BitMask]
    public GameManager.BadgeTypes BadgeType;

    private void Start()
    {
      if (!((UnityEngine.Object) this.BadgeObject != (UnityEngine.Object) null))
        return;
      this.BadgeObject.SetActive(false);
    }

    private void Update()
    {
      if ((UnityEngine.Object) this.BadgeObject == (UnityEngine.Object) null)
        return;
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (!((UnityEngine.Object) instanceDirect != (UnityEngine.Object) null))
        return;
      bool flag = instanceDirect.CheckBadges(this.BadgeType);
      if (instanceDirect.Player != null)
        flag |= instanceDirect.Player.ValidFriendPresent;
      this.BadgeObject.SetActive(flag);
    }
  }
}
