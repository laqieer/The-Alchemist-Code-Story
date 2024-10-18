// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_LocalVariable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Common/LocalVariable", 14209165)]
  [FlowNode.Pin(3, "SetIfNull", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(1, "Set", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "Compare", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(11, "Set", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(21, "Equal", FlowNode.PinTypes.Output, 21)]
  [FlowNode.Pin(22, "Not Equal", FlowNode.PinTypes.Output, 22)]
  public class FlowNode_LocalVariable : FlowNode
  {
    private const int PIN_IN_SET = 1;
    private const int PIN_IN_COMPARE = 2;
    private const int PIN_IN_SET_IF_NULL = 3;
    private const int PIN_OUT_SET = 11;
    private const int PIN_OUT_EQUAL = 21;
    private const int PIN_OUT_NOT_EQUAL = 22;
    [FlowNode.ShowInInfo]
    public string Key;
    [FlowNode.ShowInInfo]
    public string Value;

    public override void OnActivate(int pinID)
    {
      if (string.IsNullOrEmpty(this.Key))
      {
        DebugUtility.LogError("Key is not set.");
      }
      else
      {
        LocalVariable localVariable = ((Component) this).gameObject.RequireComponent<LocalVariable>();
        switch (pinID)
        {
          case 1:
            localVariable.Set(this.Key, this.Value);
            this.ActivateOutputLinks(11);
            break;
          case 2:
            if (!localVariable.Exists(this.Key))
            {
              DebugUtility.LogError("Key is not exists.");
              break;
            }
            if (localVariable.Equal(this.Key, this.Value))
            {
              this.ActivateOutputLinks(21);
              break;
            }
            this.ActivateOutputLinks(22);
            break;
          case 3:
            if (!localVariable.Exists(this.Key))
              localVariable.Set(this.Key, this.Value);
            this.ActivateOutputLinks(11);
            break;
        }
      }
    }
  }
}
