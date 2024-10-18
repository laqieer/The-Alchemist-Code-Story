// Decompiled with JetBrains decompiler
// Type: FlowNodeEvent`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public abstract class FlowNodeEvent<T> : FlowNode where T : FlowNode
{
  private static List<FlowNode> mNodes = new List<FlowNode>();

  protected override void Awake()
  {
    ((Behaviour) this).enabled = false;
    FlowNodeEvent<T>.mNodes.Add((FlowNode) this);
  }

  protected override void OnDestroy() => FlowNodeEvent<T>.mNodes.Remove((FlowNode) this);

  public static void Invoke()
  {
    for (int index = 0; index < FlowNodeEvent<T>.mNodes.Count; ++index)
      FlowNodeEvent<T>.mNodes[index].Activate(-1);
  }

  public override void OnActivate(int pinID)
  {
    if (pinID != -1)
      return;
    this.ActivateOutputLinks(1);
  }
}
