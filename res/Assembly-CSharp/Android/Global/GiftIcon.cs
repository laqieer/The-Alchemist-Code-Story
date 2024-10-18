// Decompiled with JetBrains decompiler
// Type: GiftIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using SRPG;
using UnityEngine;
using UnityEngine.UI;

public class GiftIcon : MonoBehaviour
{
  public GameManager.BadgeTypes BadgeType = GameManager.BadgeTypes.GiftBox;
  public GameManager.BadgeCountTypes BadgeCount = GameManager.BadgeCountTypes.GiftBox;
  public GameObject Badge_Gift;
  public Text Badge_Count;

  private void Start()
  {
    if (!((UnityEngine.Object) this.Badge_Gift != (UnityEngine.Object) null))
      return;
    this.Badge_Gift.SetActive(false);
  }

  private void Update()
  {
    GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
    if (!((UnityEngine.Object) instanceDirect != (UnityEngine.Object) null) || instanceDirect.CheckBusyBadges(this.BadgeType) || !((UnityEngine.Object) this.Badge_Gift != (UnityEngine.Object) null))
      return;
    this.Badge_Gift.SetActive(instanceDirect.CheckBadges(this.BadgeType));
    if (!((UnityEngine.Object) this.Badge_Count != (UnityEngine.Object) null))
      return;
    this.Badge_Count.text = instanceDirect.CheckBadgesNumber(this.BadgeCount).ToString();
  }
}
