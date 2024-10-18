// Decompiled with JetBrains decompiler
// Type: FlowNode_SwitchServerTime
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using SRPG;
using System;
using UnityEngine;

[FlowNode.NodeType("Timer/SwitchServerTime", 32741)]
[FlowNode.Pin(0, "Input", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(10, "Before", FlowNode.PinTypes.Output, 10)]
[FlowNode.Pin(11, "After", FlowNode.PinTypes.Output, 11)]
public class FlowNode_SwitchServerTime : FlowNode
{
  [SerializeField]
  public string m_Time = "2018/01/01 15:00:00";
  public const int PIN_INPUT = 0;
  public const int PIN_BEFORE = 10;
  public const int PIN_AFTER = 11;

  public override void OnActivate(int pinID)
  {
    if (pinID != 0)
      return;
    long num1;
    try
    {
      num1 = TimeManager.FromDateTime(DateTime.Parse(this.m_Time));
    }
    catch (Exception ex)
    {
      DebugUtility.LogError("m_Time is parse failed.");
      return;
    }
    long num2 = TimeManager.FromDateTime(TimeManager.ServerTime);
    if (num1 < num2)
      this.ActivateOutputLinks(10);
    else
      this.ActivateOutputLinks(11);
  }
}
