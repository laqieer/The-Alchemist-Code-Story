﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SetShopType
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.Pin(106, "Multi", FlowNode.PinTypes.Input, 6)]
  [FlowNode.NodeType("System/SetShopType", 32741)]
  [FlowNode.Pin(100, "Normal", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(101, "Tabi", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(102, "Kimagure", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(103, "Monozuki", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(104, "Arena", FlowNode.PinTypes.Input, 4)]
  [FlowNode.Pin(105, "Tour", FlowNode.PinTypes.Input, 5)]
  [FlowNode.Pin(107, "AwakePiece", FlowNode.PinTypes.Input, 7)]
  [FlowNode.Pin(108, "Artifact", FlowNode.PinTypes.Input, 8)]
  [FlowNode.Pin(109, "Artifact", FlowNode.PinTypes.Input, 9)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 10)]
  public class FlowNode_SetShopType : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 101:
          GlobalVars.ShopType = EShopType.Tabi;
          break;
        case 102:
          GlobalVars.ShopType = EShopType.Kimagure;
          break;
        case 103:
          GlobalVars.ShopType = EShopType.Monozuki;
          break;
        case 104:
          GlobalVars.ShopType = EShopType.Arena;
          break;
        case 105:
          GlobalVars.ShopType = EShopType.Tour;
          break;
        case 106:
          GlobalVars.ShopType = EShopType.Multi;
          break;
        case 107:
          GlobalVars.ShopType = EShopType.AwakePiece;
          break;
        case 108:
          GlobalVars.ShopType = EShopType.Artifact;
          break;
        case 109:
          GlobalVars.ShopType = EShopType.Limited;
          break;
        default:
          GlobalVars.ShopType = EShopType.Normal;
          break;
      }
      this.enabled = false;
      if ((MonoSingleton<GameManager>.Instance.Player.TutorialFlags & 1L) == 0L)
        return;
      this.ActivateOutputLinks(1);
    }
  }
}
