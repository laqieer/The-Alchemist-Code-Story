// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_IsExistBuyCoinShopType
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/Buy/BuyCoinTypeExist", 32741)]
  [FlowNode.Pin(0, "Input", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "Exist", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "NotExist", FlowNode.PinTypes.Output, 11)]
  public class FlowNode_IsExistBuyCoinShopType : FlowNode
  {
    private const int PIN_INPUT = 0;
    private const int PIN_OUTPUT_EXIST = 10;
    private const int PIN_OUTPUT_NOT_EXIST = 11;
    [SerializeField]
    private BuyCoinManager.BuyCoinShopType mShopType;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (FlowNode_IsExistBuyCoinShopType.IsExistShopItem(this.mShopType))
        this.ActivateOutputLinks(10);
      else
        this.ActivateOutputLinks(11);
    }

    public static bool IsExistShopItem(BuyCoinManager.BuyCoinShopType type)
    {
      List<BuyCoinShopParam> buyCoinShopParam1 = MonoSingleton<GameManager>.Instance.MasterParam.GetBuyCoinShopParam();
      if (buyCoinShopParam1 == null)
        return false;
      for (int index = 0; index < buyCoinShopParam1.Count; ++index)
      {
        BuyCoinShopParam buyCoinShopParam2 = buyCoinShopParam1[index];
        if (type == buyCoinShopParam2.ShopType && MonoSingleton<GameManager>.Instance.MasterParam.IsBuyCoinShopOpen(buyCoinShopParam2) && (FlowNode_IsFgGBuy.IsFgGBuy() || buyCoinShopParam1[index].ShopType != BuyCoinManager.BuyCoinShopType.FGG))
          return true;
      }
      return false;
    }
  }
}
