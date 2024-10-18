// Decompiled with JetBrains decompiler
// Type: FlowNode_OnClick
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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
    this.enabled = false;
  }

  private void Bind()
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
