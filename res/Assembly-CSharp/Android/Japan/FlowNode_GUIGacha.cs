// Decompiled with JetBrains decompiler
// Type: FlowNode_GUIGacha
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[AddComponentMenu("")]
[FlowNode.NodeType("Gacha/GUI", 32741)]
public class FlowNode_GUIGacha : FlowNode_GUI
{
  protected override void OnInstanceCreate()
  {
    this.Instance.transform.SetParent(this.Instance.transform.root, false);
    this.mListener = GameUtility.RequireComponent<DestroyEventListener>(this.mInstance);
    this.mListener.Listeners += new DestroyEventListener.DestroyEvent(((FlowNode_GUI) this).OnInstanceDestroyTrigger);
  }
}
