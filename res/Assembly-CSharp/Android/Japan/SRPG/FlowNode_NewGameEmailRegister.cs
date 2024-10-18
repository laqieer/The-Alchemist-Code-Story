// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_NewGameEmailRegister
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.NodeType("System/NewGame/NewEmailGameRegister", 32741)]
  [FlowNode.Pin(0, "Create New Account", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "Failed", FlowNode.PinTypes.Output, 11)]
  public class FlowNode_NewGameEmailRegister : FlowNode
  {
    private const int PIN_INPUT = 0;
    private const int PIN_SUCCESS = 10;
    private const int PIN_FAILED = 11;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      string email = FlowNode_NewGameRegister.gDeviceID.ToString().Replace("-", string.Empty).Substring(0, 16);
      FlowNode_NewGameRegister.gEmail = email;
      Network.RequestAPIImmediate((WebAPI) new ReqAuthEmailRegister(email, FlowNode_NewGameRegister.gPassword, FlowNode_NewGameRegister.gDeviceID, MonoSingleton<GameManager>.Instance.SecretKey, MonoSingleton<GameManager>.Instance.UdId, new Network.ResponseCallback(this.ImmediateResponseCallback)), true);
      this.enabled = true;
    }

    private void ImmediateResponseCallback(WWWResult www)
    {
      Network.RemoveAPI();
      this.enabled = false;
      if (0 <= www.text.IndexOf("\"is_succeeded\":true"))
        this.ActivateOutputLinks(10);
      else
        this.ActivateOutputLinks(11);
    }
  }
}
