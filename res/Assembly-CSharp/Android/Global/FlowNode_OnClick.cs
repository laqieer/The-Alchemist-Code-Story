// Decompiled with JetBrains decompiler
// Type: FlowNode_OnClick
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[FlowNode.Pin(1, "Clicked", FlowNode.PinTypes.Output, 0)]
[FlowNode.NodeType("Event/OnClickButton", 58751)]
[AddComponentMenu("")]
public class FlowNode_OnClick : FlowNodePersistent
{
  [FlowNode.DropTarget(typeof (Button), true)]
  [FlowNode.ShowInInfo]
  public Button Target;
  private Button mBound;

  private void Start()
  {
    this.BindTargetButton();
    this.enabled = false;
  }

  private void BindTargetButton()
  {
    if (!((Object) this.Target != (Object) null) || !((Object) this.Target != (Object) this.mBound))
      return;
    this.Target.onClick.AddListener(new UnityAction(this.OnClick));
    this.mBound = this.Target;
  }

  private void OnClick()
  {
    this.Activate(1);
  }

  public override void OnActivate(int pinID)
  {
    if (pinID != 1)
      return;
    this.ActivateOutputLinks(1);
  }
}
