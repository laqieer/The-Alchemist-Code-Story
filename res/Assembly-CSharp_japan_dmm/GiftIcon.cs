// Decompiled with JetBrains decompiler
// Type: GiftIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using SRPG;
using UnityEngine;

#nullable disable
public class GiftIcon : MonoBehaviour
{
  public GameObject Badge_Gift;

  private void Start()
  {
    if (!Object.op_Inequality((Object) this.Badge_Gift, (Object) null))
      return;
    this.Badge_Gift.SetActive(false);
  }

  private void Update()
  {
    GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
    if (!Object.op_Inequality((Object) instanceDirect, (Object) null) || instanceDirect.CheckBusyBadges(GameManager.BadgeTypes.GiftBox) || !Object.op_Inequality((Object) this.Badge_Gift, (Object) null))
      return;
    this.Badge_Gift.SetActive(instanceDirect.CheckBadges(GameManager.BadgeTypes.GiftBox));
  }
}
