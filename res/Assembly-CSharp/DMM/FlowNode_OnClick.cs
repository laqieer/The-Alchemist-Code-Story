// Decompiled with JetBrains decompiler
// Type: FlowNode_OnClick
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
[AddComponentMenu("")]
[FlowNode.NodeType("Event/OnClickButton", 58751)]
[FlowNode.Pin(1, "Clicked", FlowNode.PinTypes.Output, 0)]
public class FlowNode_OnClick : FlowNodePersistent
{
  [FlowNode.ShowInInfo]
  [FlowNode.DropTarget(typeof (Button), true)]
  public Button Target;
  private Button mBound;

  private void Start()
  {
    this.Bind();
    ((Behaviour) this).enabled = false;
  }

  private void Bind()
  {
    if (!Object.op_Inequality((Object) this.Target, (Object) null) || !Object.op_Inequality((Object) this.Target, (Object) this.mBound))
      return;
    // ISSUE: method pointer
    ((UnityEvent) this.Target.onClick).AddListener(new UnityAction((object) this, __methodptr(OnClick)));
    this.mBound = this.Target;
  }

  private void OnClick() => this.Activate(1);

  public override void OnActivate(int pinID)
  {
    if (pinID != 1)
      return;
    this.ActivateOutputLinks(1);
  }
}
