﻿// Decompiled with JetBrains decompiler
// Type: FlowNode_Random
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;

#nullable disable
[FlowNode.NodeType("Common/Random", 32741)]
[FlowNode.Pin(100, "Do", FlowNode.PinTypes.Input, 100)]
[FlowNode.Pin(101, "CompareLastResult", FlowNode.PinTypes.Input, 101)]
[FlowNode.Pin(1000, "== CompareValue", FlowNode.PinTypes.Output, 1000)]
[FlowNode.Pin(1001, "!= CompareValue", FlowNode.PinTypes.Output, 1001)]
[FlowNode.Pin(1002, "< CompareValue", FlowNode.PinTypes.Output, 1002)]
[FlowNode.Pin(1003, "<= CompareValue", FlowNode.PinTypes.Output, 1003)]
[FlowNode.Pin(1004, "> CompareValue", FlowNode.PinTypes.Output, 1004)]
[FlowNode.Pin(1005, ">= CompareValue", FlowNode.PinTypes.Output, 1005)]
public class FlowNode_Random : FlowNode
{
  public int DivValue = 100;
  public int CompareValue = 50;
  private static uint sLastResult;
  private RandXorshift mRand;

  public override void OnActivate(int pinID)
  {
    switch (pinID)
    {
      case 100:
        if (this.mRand == null)
        {
          this.mRand = new RandXorshift("fnr");
          this.mRand.Seed((uint) DateTime.Now.Ticks);
        }
        FlowNode_Random.sLastResult = this.mRand.Get() % Math.Max(1U, (uint) this.DivValue);
        this.CheckResult(FlowNode_Random.sLastResult);
        break;
      case 101:
        this.CheckResult(FlowNode_Random.sLastResult);
        break;
    }
  }

  private void CheckResult(uint result)
  {
    if ((long) result == (long) this.CompareValue)
      this.ActivateOutputLinks(1000);
    else
      this.ActivateOutputLinks(1001);
    if ((long) result < (long) this.CompareValue)
      this.ActivateOutputLinks(1002);
    if ((long) result <= (long) this.CompareValue)
      this.ActivateOutputLinks(1003);
    if ((long) result > (long) this.CompareValue)
      this.ActivateOutputLinks(1004);
    if ((long) result < (long) this.CompareValue)
      return;
    this.ActivateOutputLinks(1005);
  }
}
