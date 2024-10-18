// Decompiled with JetBrains decompiler
// Type: FlowNodeEvent`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
