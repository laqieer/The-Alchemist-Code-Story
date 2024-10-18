// Decompiled with JetBrains decompiler
// Type: GiftIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using SRPG;
using UnityEngine;

public class GiftIcon : MonoBehaviour
{
  public GameObject Badge_Gift;

  private void Start()
  {
    if (!((UnityEngine.Object) this.Badge_Gift != (UnityEngine.Object) null))
      return;
    this.Badge_Gift.SetActive(false);
  }

  private void Update()
  {
    GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
    if (!((UnityEngine.Object) instanceDirect != (UnityEngine.Object) null) || instanceDirect.CheckBusyBadges(GameManager.BadgeTypes.GiftBox) || !((UnityEngine.Object) this.Badge_Gift != (UnityEngine.Object) null))
      return;
    this.Badge_Gift.SetActive(instanceDirect.CheckBadges(GameManager.BadgeTypes.GiftBox));
  }
}
