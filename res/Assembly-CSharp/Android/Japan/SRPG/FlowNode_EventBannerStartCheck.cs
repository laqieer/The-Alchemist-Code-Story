// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_EventBannerStartCheck
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("SRPG/EventBannerStartCheck", 32741)]
  [FlowNode.Pin(1, "Check", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "Finished", FlowNode.PinTypes.Output, 10)]
  public class FlowNode_EventBannerStartCheck : FlowNode
  {
    [SerializeField]
    private AppealItemLimitedShop LimitedShopAppealItem;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      this.StartCoroutine(this.UpdateEventBanner());
    }

    [DebuggerHidden]
    private IEnumerator UpdateEventBanner()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_EventBannerStartCheck.\u003CUpdateEventBanner\u003Ec__Iterator0() { \u0024this = this };
    }
  }
}
