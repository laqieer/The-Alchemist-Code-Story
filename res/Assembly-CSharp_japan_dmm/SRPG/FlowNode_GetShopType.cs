﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_GetShopType
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/Shop/GetShopType", 32741)]
  [FlowNode.Pin(100, "Normal", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(101, "Tabi", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(102, "Kimagure", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(103, "Monozuki", FlowNode.PinTypes.Output, 3)]
  [FlowNode.Pin(104, "Arena", FlowNode.PinTypes.Output, 4)]
  [FlowNode.Pin(105, "Tour", FlowNode.PinTypes.Output, 5)]
  [FlowNode.Pin(106, "Multi", FlowNode.PinTypes.Output, 6)]
  [FlowNode.Pin(107, "AwakePiece", FlowNode.PinTypes.Output, 7)]
  [FlowNode.Pin(108, "Artifact", FlowNode.PinTypes.Output, 8)]
  [FlowNode.Pin(109, "Limited", FlowNode.PinTypes.Output, 9)]
  [FlowNode.Pin(111, "Port", FlowNode.PinTypes.Output, 111)]
  [FlowNode.Pin(1, "Test", FlowNode.PinTypes.Input, 1000)]
  public class FlowNode_GetShopType : FlowNode
  {
    private const int PIN_OUT_SHOP_PORT = 111;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      switch (GlobalVars.ShopType)
      {
        case EShopType.Tabi:
          pinID = 101;
          break;
        case EShopType.Kimagure:
          pinID = 102;
          break;
        case EShopType.Monozuki:
          pinID = 103;
          break;
        case EShopType.Tour:
          pinID = 105;
          break;
        case EShopType.Arena:
          pinID = 104;
          break;
        case EShopType.Multi:
          pinID = 106;
          break;
        case EShopType.AwakePiece:
          pinID = 107;
          break;
        case EShopType.Artifact:
          pinID = 108;
          break;
        case EShopType.Limited:
          pinID = 109;
          break;
        case EShopType.Port:
          pinID = 111;
          break;
        default:
          pinID = 100;
          break;
      }
      this.ActivateOutputLinks(pinID);
    }
  }
}
