// Decompiled with JetBrains decompiler
// Type: FlowNode_GUIGacha
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

[FlowNode.NodeType("Gacha/GUI", 32741)]
[AddComponentMenu("")]
public class FlowNode_GUIGacha : FlowNode_GUI
{
  protected override void OnInstanceCreate()
  {
    this.Instance.transform.SetParent(this.Instance.transform.root, false);
    this.mListener = GameUtility.RequireComponent<DestroyEventListener>(this.mInstance);
    this.mListener.Listeners += new DestroyEventListener.DestroyEvent(((FlowNode_GUI) this).OnInstanceDestroyTrigger);
  }
}
