// Decompiled with JetBrains decompiler
// Type: FlowNode_SGGeoData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

[FlowNode.Pin(1, "Belgium", FlowNode.PinTypes.Output, 2)]
[FlowNode.Pin(2, "Others", FlowNode.PinTypes.Output, 2)]
[AddComponentMenu("")]
[FlowNode.NodeType("SG/GeoData", 32741)]
[FlowNode.Pin(100, "Trigger", FlowNode.PinTypes.Input, 0)]
public class FlowNode_SGGeoData : FlowNode
{
  public override string[] GetInfoLines()
  {
    return base.GetInfoLines();
  }

  public override void OnActivate(int pinID)
  {
    if (pinID != 100)
      return;
    this.StartCoroutine(this.Upload());
  }

  [DebuggerHidden]
  private IEnumerator Upload()
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new FlowNode_SGGeoData.\u003CUpload\u003Ec__Iterator89()
    {
      \u003C\u003Ef__this = this
    };
  }
}
