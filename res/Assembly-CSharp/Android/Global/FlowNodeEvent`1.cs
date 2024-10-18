// Decompiled with JetBrains decompiler
// Type: FlowNodeEvent`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;

public abstract class FlowNodeEvent<T> : FlowNode where T : FlowNode
{
  private static List<FlowNode> mNodes = new List<FlowNode>();

  protected override void Awake()
  {
    this.enabled = false;
    FlowNodeEvent<T>.mNodes.Add((FlowNode) this);
  }

  protected override void OnDestroy()
  {
    FlowNodeEvent<T>.mNodes.Remove((FlowNode) this);
  }

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
