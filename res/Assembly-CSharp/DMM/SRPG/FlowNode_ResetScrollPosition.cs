// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ResetScrollPosition
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("UI/ResetScrollPosition", 32741)]
  [FlowNode.Pin(0, "Input", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "Output", FlowNode.PinTypes.Output, 10)]
  public class FlowNode_ResetScrollPosition : FlowNode
  {
    [SerializeField]
    private ScrollRect ScrollParent;
    [SerializeField]
    private Transform ResetTransform;
    private float mDecelerationRate;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      this.ResetScrollPosition();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
    }

    private void ResetScrollPosition()
    {
      if (Object.op_Equality((Object) this.ScrollParent, (Object) null))
        return;
      this.mDecelerationRate = this.ScrollParent.decelerationRate;
      this.ScrollParent.decelerationRate = 0.0f;
      RectTransform resetTransform = this.ResetTransform as RectTransform;
      resetTransform.anchoredPosition = new Vector2(resetTransform.anchoredPosition.x, 0.0f);
      this.StartCoroutine(this.RefreshScrollRect());
    }

    [DebuggerHidden]
    private IEnumerator RefreshScrollRect()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_ResetScrollPosition.\u003CRefreshScrollRect\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }
  }
}
