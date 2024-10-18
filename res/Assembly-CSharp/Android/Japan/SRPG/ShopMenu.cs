// Decompiled with JetBrains decompiler
// Type: SRPG.ShopMenu
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "通常ショップ", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "秘密の店", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(3, "コイン交換所", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(4, "魂の交換所", FlowNode.PinTypes.Input, 4)]
  public class ShopMenu : MonoBehaviour, IFlowInterface
  {
    public Button ShopButton;
    public Button GuerrillaShopButton;
    public Button LimitedShopButton;
    public Button CoinShopButton;
    public Button KakeraShopButton;
    [Space(10f)]
    public GameObject ActiveShop;
    public GameObject ActiveGuerrilla;
    public GameObject ActiveLimited;
    public GameObject ActiveCoin;
    public GameObject ActiveKakera;
    public GameObject GuerrillaBallon;

    public void Activated(int pinID)
    {
      bool flag1 = false;
      bool flag2 = false;
      bool flag3 = false;
      bool flag4 = false;
      switch (pinID)
      {
        case 1:
          flag1 = true;
          break;
        case 2:
          flag2 = true;
          break;
        case 3:
          flag3 = true;
          break;
        case 4:
          flag4 = true;
          break;
      }
      if ((UnityEngine.Object) this.ShopButton != (UnityEngine.Object) null)
      {
        this.ShopButton.gameObject.SetActive(true);
        this.ShopButton.interactable = !flag1;
      }
      if ((UnityEngine.Object) this.ActiveShop != (UnityEngine.Object) null)
        this.ActiveShop.SetActive(flag1);
      if ((UnityEngine.Object) this.GuerrillaBallon != (UnityEngine.Object) null)
        this.GuerrillaBallon.SetActive(MonoSingleton<GameManager>.Instance.Player.IsGuerrillaShopOpen());
      if ((UnityEngine.Object) this.LimitedShopButton != (UnityEngine.Object) null)
        this.LimitedShopButton.interactable = !flag2;
      if ((UnityEngine.Object) this.ActiveLimited != (UnityEngine.Object) null)
        this.ActiveLimited.SetActive(flag2);
      if ((UnityEngine.Object) this.CoinShopButton != (UnityEngine.Object) null)
        this.CoinShopButton.interactable = !flag3;
      if ((UnityEngine.Object) this.ActiveCoin != (UnityEngine.Object) null)
        this.ActiveCoin.SetActive(flag3);
      if ((UnityEngine.Object) this.KakeraShopButton != (UnityEngine.Object) null)
        this.KakeraShopButton.interactable = !flag4;
      if (!((UnityEngine.Object) this.ActiveKakera != (UnityEngine.Object) null))
        return;
      this.ActiveKakera.SetActive(flag4);
    }
  }
}
