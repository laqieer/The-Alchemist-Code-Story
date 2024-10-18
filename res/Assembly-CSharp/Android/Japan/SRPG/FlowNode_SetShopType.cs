// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SetShopType
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.NodeType("System/Shop/SetShopType", 32741)]
  [FlowNode.Pin(100, "Normal", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(101, "Tabi", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(102, "Kimagure", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(103, "Monozuki", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(104, "Arena", FlowNode.PinTypes.Input, 4)]
  [FlowNode.Pin(105, "Tour", FlowNode.PinTypes.Input, 5)]
  [FlowNode.Pin(106, "Multi", FlowNode.PinTypes.Input, 6)]
  [FlowNode.Pin(107, "AwakePiece", FlowNode.PinTypes.Input, 7)]
  [FlowNode.Pin(108, "Artifact", FlowNode.PinTypes.Input, 8)]
  [FlowNode.Pin(109, "Artifact", FlowNode.PinTypes.Input, 9)]
  [FlowNode.Pin(110, "Gurrilla", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1000)]
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
        case 110:
          GlobalVars.ShopType = EShopType.Guerrilla;
          break;
        default:
          GlobalVars.ShopType = EShopType.Normal;
          break;
      }
      this.enabled = false;
      this.ActivateOutputLinks(1);
    }
  }
}
