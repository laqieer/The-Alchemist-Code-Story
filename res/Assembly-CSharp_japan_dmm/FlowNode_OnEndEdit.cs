// Decompiled with JetBrains decompiler
// Type: FlowNode_OnEndEdit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using SRPG;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
[AddComponentMenu("")]
[FlowNode.NodeType("Event/OnEndEdit", 58751)]
[FlowNode.Pin(1, "Edited", FlowNode.PinTypes.Output, 0)]
public class FlowNode_OnEndEdit : FlowNodePersistent
{
  [FlowNode.ShowInInfo]
  [FlowNode.DropTarget(typeof (InputField), true)]
  public InputField Target;

  private void Start()
  {
    if (!Object.op_Inequality((Object) this.Target, (Object) null))
      return;
    // ISSUE: method pointer
    ((UnityEvent<string>) this.Target.onEndEdit).AddListener(new UnityAction<string>((object) this, __methodptr(\u003CStart\u003Em__0)));
    ((Behaviour) this).enabled = true;
  }

  protected override void OnDestroy()
  {
    base.OnDestroy();
    GUtility.SetImmersiveMove();
    if (!Object.op_Inequality((Object) this.Target, (Object) null) || this.Target.onEndEdit == null)
      return;
    ((UnityEventBase) this.Target.onEndEdit).RemoveAllListeners();
  }

  private void OnEndEdit(InputField field)
  {
    GUtility.SetImmersiveMove();
    if (field.text.Length <= 0)
      return;
    GlobalVars.EditPlayerName = field.text;
    this.Activate(1);
  }

  public override void OnActivate(int pinID)
  {
    if (pinID != 1)
      return;
    this.ActivateOutputLinks(1);
  }
}
