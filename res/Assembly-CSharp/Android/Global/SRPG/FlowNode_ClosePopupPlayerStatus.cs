// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ClosePopupPlayerStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.NodeType("System/ClosePopup", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_ClosePopupPlayerStatus : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if ((UnityEngine.Object) FlowNode_BuyGold.ConfirmBoxObj != (UnityEngine.Object) null)
      {
        Win_Btn_DecideCancel_FL_C component = FlowNode_BuyGold.ConfirmBoxObj.GetComponent<Win_Btn_DecideCancel_FL_C>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null)
          component.BeginClose();
        FlowNode_BuyGold.ConfirmBoxObj = (GameObject) null;
      }
      if ((UnityEngine.Object) FlowNode_BuyStamina.ConfirmBoxObj != (UnityEngine.Object) null)
      {
        Win_Btn_DecideCancel_FL_C component = FlowNode_BuyStamina.ConfirmBoxObj.GetComponent<Win_Btn_DecideCancel_FL_C>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null)
          component.BeginClose();
        FlowNode_BuyStamina.ConfirmBoxObj = (GameObject) null;
      }
      this.ActivateOutputLinks(1);
    }
  }
}
