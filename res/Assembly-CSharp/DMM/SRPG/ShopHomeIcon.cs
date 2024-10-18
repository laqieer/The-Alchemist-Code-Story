// Decompiled with JetBrains decompiler
// Type: SRPG.ShopHomeIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(0, "Refresh", FlowNode.PinTypes.Input, 0)]
  public class ShopHomeIcon : MonoBehaviour, IFlowInterface
  {
    public GameObject ShopIcon;
    public GameObject GuerrillaIcon;
    private static ShopHomeIcon mInstance;

    public static ShopHomeIcon Instance => ShopHomeIcon.mInstance;

    private void Awake() => ShopHomeIcon.mInstance = this;

    private void OnDestroy() => ShopHomeIcon.mInstance = (ShopHomeIcon) null;

    private void Start() => this.Refresh();

    public void Refresh()
    {
      bool flag = MonoSingleton<GameManager>.Instance.Player.IsGuerrillaShopOpen();
      if (Object.op_Inequality((Object) this.ShopIcon, (Object) null))
        this.ShopIcon.SetActive(!flag);
      if (!Object.op_Inequality((Object) this.GuerrillaIcon, (Object) null))
        return;
      this.GuerrillaIcon.SetActive(flag);
    }

    public void Activated(int pinID)
    {
      if (pinID != 0)
        return;
      this.Refresh();
    }
  }
}
