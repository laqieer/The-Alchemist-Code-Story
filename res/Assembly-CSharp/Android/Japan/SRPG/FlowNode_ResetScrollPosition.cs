// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ResetScrollPosition
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

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
      if ((UnityEngine.Object) this.ScrollParent == (UnityEngine.Object) null)
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
      return (IEnumerator) new FlowNode_ResetScrollPosition.\u003CRefreshScrollRect\u003Ec__Iterator0() { \u0024this = this };
    }
  }
}
