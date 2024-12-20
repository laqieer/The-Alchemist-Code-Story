﻿// Decompiled with JetBrains decompiler
// Type: FlowNode_OnEndEditMultiPlayRoomPassCode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using SRPG;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[AddComponentMenu("")]
[FlowNode.Pin(1, "Edited", FlowNode.PinTypes.Output, 0)]
[FlowNode.NodeType("Event/OnEndEditMultiPlayRoomPassCode", 58751)]
public class FlowNode_OnEndEditMultiPlayRoomPassCode : FlowNodePersistent
{
  [FlowNode.ShowInInfo]
  [FlowNode.DropTarget(typeof (InputField), true)]
  public InputField Target;

  private void Start()
  {
    if (!((UnityEngine.Object) this.Target != (UnityEngine.Object) null))
      return;
    GlobalVars.EditMultiPlayRoomPassCode = (string) null;
    this.Target.onEndEdit.AddListener((UnityAction<string>) (_param1 => this.OnEndEdit(this.Target)));
    this.enabled = true;
  }

  protected override void OnDestroy()
  {
    base.OnDestroy();
    GUtility.SetImmersiveMove();
    if (!((UnityEngine.Object) this.Target != (UnityEngine.Object) null) || this.Target.onEndEdit == null)
      return;
    this.Target.onEndEdit.RemoveAllListeners();
  }

  private void OnEndEdit(InputField field)
  {
    GUtility.SetImmersiveMove();
    if (field.text.Length <= 0)
      return;
    GlobalVars.EditMultiPlayRoomPassCode = field.text;
    this.Activate(1);
  }

  public override void OnActivate(int pinID)
  {
    if (pinID != 1)
      return;
    this.ActivateOutputLinks(1);
  }
}
