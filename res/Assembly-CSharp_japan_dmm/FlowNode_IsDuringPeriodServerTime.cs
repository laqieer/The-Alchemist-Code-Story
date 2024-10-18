// Decompiled with JetBrains decompiler
// Type: FlowNode_IsDuringPeriodServerTime
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using SRPG;
using System;
using UnityEngine;

#nullable disable
[FlowNode.NodeType("Timer/IsDuringPeriodServerTime", 32741)]
[FlowNode.Pin(0, "Input", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(10, "true", FlowNode.PinTypes.Output, 10)]
[FlowNode.Pin(11, "false", FlowNode.PinTypes.Output, 11)]
public class FlowNode_IsDuringPeriodServerTime : FlowNode
{
  public const int PIN_INPUT = 0;
  public const int PIN_TRUE = 10;
  public const int PIN_FALSE = 11;
  [SerializeField]
  private string mStartTime = "2018/01/01 15:00:00";
  [SerializeField]
  private string mEndTime = "2018/01/02 15:00:00";

  public override void OnActivate(int pinID)
  {
    if (pinID != 0)
      return;
    long num1;
    try
    {
      num1 = TimeManager.FromDateTime(DateTime.Parse(this.mStartTime));
    }
    catch (Exception ex)
    {
      DebugUtility.LogError("mStartTime is parse failed.");
      return;
    }
    long num2;
    try
    {
      num2 = TimeManager.FromDateTime(DateTime.Parse(this.mEndTime));
    }
    catch (Exception ex)
    {
      DebugUtility.LogError("mEndTime is parse failed.");
      return;
    }
    long num3 = TimeManager.FromDateTime(TimeManager.ServerTime);
    if (num1 <= num3 && num3 <= num2)
      this.ActivateOutputLinks(10);
    else
      this.ActivateOutputLinks(11);
  }
}
