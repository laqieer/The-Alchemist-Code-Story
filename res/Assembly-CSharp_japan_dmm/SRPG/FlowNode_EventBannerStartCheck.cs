// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_EventBannerStartCheck
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

#nullable disable
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
      return (IEnumerator) new FlowNode_EventBannerStartCheck.\u003CUpdateEventBanner\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }
  }
}
