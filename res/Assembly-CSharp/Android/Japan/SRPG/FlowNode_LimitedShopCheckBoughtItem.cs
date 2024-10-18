// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_LimitedShopCheckBoughtItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Linq;

namespace SRPG
{
  [FlowNode.NodeType("SRPG/LimitedShopCheckBoughtItem", 32741)]
  [FlowNode.Pin(1, "", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "SetItem", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "Item", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(12, "Error", FlowNode.PinTypes.Output, 12)]
  public class FlowNode_LimitedShopCheckBoughtItem : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      this.SetResult();
    }

    private void SetResult()
    {
      LimitedShopData limitedShopData = MonoSingleton<GameManager>.Instance.Player.GetLimitedShopData();
      if (limitedShopData == null || limitedShopData.items.Count <= 0)
      {
        this.ActivateOutputLinks(12);
      }
      else
      {
        int shopdata_index = GlobalVars.ShopBuyIndex;
        LimitedShopItem limitedShopItem = limitedShopData.items.FirstOrDefault<LimitedShopItem>((Func<LimitedShopItem, bool>) (item => item.id == shopdata_index));
        if (limitedShopItem == null)
          this.ActivateOutputLinks(12);
        else if (limitedShopItem.IsSet)
          this.ActivateOutputLinks(10);
        else
          this.ActivateOutputLinks(11);
      }
    }
  }
}
