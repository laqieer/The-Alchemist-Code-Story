// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_LogoutFacebook
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using Facebook.Unity;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("System/LogoutFacebook", 32741)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_LogoutFacebook : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (AccessToken.get_CurrentAccessToken() != null)
      {
        PlayerPrefs.SetInt("AccountLinked", 0);
        FB.LogOut();
      }
      this.ActivateOutputLinks(1);
    }
  }
}
