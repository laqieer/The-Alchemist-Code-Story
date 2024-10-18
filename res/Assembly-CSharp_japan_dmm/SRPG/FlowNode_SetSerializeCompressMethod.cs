// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SetSerializeCompressMethod
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Network/SetSerializeCompressMethod", 32741)]
  [FlowNode.Pin(0, "In", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Out", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_SetSerializeCompressMethod : FlowNode
  {
    private const int PINID_IN = 0;
    private const int PINID_OUT = 1;
    [SerializeField]
    private EncodingTypes.ESerializeCompressMethod Method;
    [SerializeField]
    private bool SetMethod;
    [SerializeField]
    private bool UnsetMethod;
    [SerializeField]
    private bool EncryptionOn;
    [SerializeField]
    private bool EncryptionOff;
    [SerializeField]
    private bool MsgPackMDOn;
    [SerializeField]
    private bool MsgPackMDOff;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (this.SetMethod)
      {
        GlobalVars.SelectedSerializeCompressMethod = this.Method;
        GlobalVars.SelectedSerializeCompressMethodWasNodeSet = true;
      }
      if (this.UnsetMethod)
      {
        GlobalVars.SelectedSerializeCompressMethod = this.Method;
        GlobalVars.SelectedSerializeCompressMethodWasNodeSet = false;
      }
      if (this.EncryptionOn)
        GameUtility.Config_UseEncryption.Value = true;
      if (this.EncryptionOff)
        GameUtility.Config_UseEncryption.Value = false;
      if (this.MsgPackMDOn)
        GameUtility.Config_UseSerializedParams.Value = true;
      if (this.MsgPackMDOff)
        GameUtility.Config_UseSerializedParams.Value = false;
      this.ActivateOutputLinks(1);
    }
  }
}
