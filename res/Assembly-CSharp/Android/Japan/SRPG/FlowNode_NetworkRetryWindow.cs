// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_NetworkRetryWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("Common/NetworkRetryWindow", 32741)]
  [FlowNode.Pin(0, "Create", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_NetworkRetryWindow : FlowNode
  {
    public GameObject Window;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0 || !((UnityEngine.Object) this.Window != (UnityEngine.Object) null))
        return;
      UnityEngine.Object.Instantiate<GameObject>(this.Window);
    }
  }
}
