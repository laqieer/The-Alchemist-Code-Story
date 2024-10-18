// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ClosePopupPlayerStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/Buy/ClosePopupPlayerStatus", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_ClosePopupPlayerStatus : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (Object.op_Inequality((Object) FlowNode_BuyGold.ConfirmBoxObj, (Object) null))
      {
        Win_Btn_DecideCancel_FL_C component = FlowNode_BuyGold.ConfirmBoxObj.GetComponent<Win_Btn_DecideCancel_FL_C>();
        if (Object.op_Inequality((Object) component, (Object) null))
          component.BeginClose();
        FlowNode_BuyGold.ConfirmBoxObj = (GameObject) null;
      }
      if (Object.op_Inequality((Object) FlowNode_BuyStamina.ConfirmBoxObj, (Object) null))
      {
        Win_Btn_DecideCancel_FL_C component = FlowNode_BuyStamina.ConfirmBoxObj.GetComponent<Win_Btn_DecideCancel_FL_C>();
        if (Object.op_Inequality((Object) component, (Object) null))
          component.BeginClose();
        FlowNode_BuyStamina.ConfirmBoxObj = (GameObject) null;
      }
      this.ActivateOutputLinks(1);
    }
  }
}
