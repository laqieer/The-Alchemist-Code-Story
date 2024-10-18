// Decompiled with JetBrains decompiler
// Type: FlowNode_GUIGacha
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
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
