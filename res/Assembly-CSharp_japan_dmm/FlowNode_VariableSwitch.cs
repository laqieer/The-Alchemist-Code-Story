// Decompiled with JetBrains decompiler
// Type: FlowNode_VariableSwitch
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using UnityEngine;

#nullable disable
[FlowNode.NodeType("Common/VariableSwitch", 32741)]
[FlowNode.Pin(0, "Input", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(10, "Default", FlowNode.PinTypes.Output, 10)]
public class FlowNode_VariableSwitch : FlowNode
{
  private const int PIN_INPUT = 0;
  private const int PIN_OUTPUT_DEFAULT = 10;
  [SerializeField]
  private string Name;
  [SerializeField]
  private FlowNode_VariableSwitch.PinVariables[] mPinList = new FlowNode_VariableSwitch.PinVariables[0];
  private FlowNode_VariableSwitch.PinVariables[] mBackupPinList = new FlowNode_VariableSwitch.PinVariables[0];
  private FlowNode.Pin[] mPins = new FlowNode.Pin[0];

  protected override void Awake()
  {
    base.Awake();
    this.UpdateCache();
  }

  protected void Start() => this.UpdateCache();

  public override void OnActivate(int pinID)
  {
    string str = FlowNode_Variable.Get(this.Name);
    for (int index = 0; index < this.mPinList.Length; ++index)
    {
      if (this.mPinList[index].value == str)
      {
        this.ActivateOutputLinks(index + 10 + 1);
        return;
      }
    }
    this.ActivateOutputLinks(10);
  }

  public override FlowNode.Pin[] GetDynamicPins() => this.mPins;

  private void UpdateCache()
  {
    this.mPins = new FlowNode.Pin[this.mPinList.Length];
    for (int index = 0; index < this.mPinList.Length; ++index)
      this.mPins[index] = this.mPinList[index].pin;
  }

  [Serializable]
  private class PinVariables
  {
    [SerializeField]
    [HideInInspector]
    public FlowNode.Pin pin;
    [SerializeField]
    public string value;
  }
}
