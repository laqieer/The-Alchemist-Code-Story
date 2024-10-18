﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_GetCurrentScene
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.NodeType("Scene/GetCurrentScene", 32741)]
  [FlowNode.Pin(0, "Test", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "Other", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(101, "Single", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(102, "Multi", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(103, "HomeMulti", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(104, "Home", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(105, "Title", FlowNode.PinTypes.Output, 0)]
  public class FlowNode_GetCurrentScene : FlowNode
  {
    public static bool IsAfterLogin()
    {
      switch (GameUtility.GetCurrentScene())
      {
        case GameUtility.EScene.BATTLE:
        case GameUtility.EScene.BATTLE_MULTI:
        case GameUtility.EScene.HOME:
        case GameUtility.EScene.HOME_MULTI:
          return true;
        default:
          return false;
      }
    }

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      switch (GameUtility.GetCurrentScene())
      {
        case GameUtility.EScene.BATTLE:
          this.ActivateOutputLinks(101);
          break;
        case GameUtility.EScene.BATTLE_MULTI:
          this.ActivateOutputLinks(102);
          break;
        case GameUtility.EScene.HOME:
          this.ActivateOutputLinks(104);
          break;
        case GameUtility.EScene.HOME_MULTI:
          this.ActivateOutputLinks(103);
          break;
        case GameUtility.EScene.TITLE:
          this.ActivateOutputLinks(105);
          break;
        default:
          this.ActivateOutputLinks(100);
          break;
      }
    }
  }
}
