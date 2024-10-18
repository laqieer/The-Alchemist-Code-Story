// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ClearCache
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/キャッシュクリア", 32741)]
  [FlowNode.Pin(0, "In", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "Out", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(101, "Finished", FlowNode.PinTypes.Output, 0)]
  public class FlowNode_ClearCache : FlowNode
  {
    public const int PINID_CLEAR = 0;
    public const int PINID_OUT = 100;
    public const int PINID_FINISHED = 101;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0 || ((Behaviour) this).enabled)
        return;
      CriticalSection.Enter();
      ((Behaviour) this).enabled = true;
      this.StartCoroutine(this.ClearCacheAsync());
      this.ActivateOutputLinks(100);
    }

    [DebuggerHidden]
    private IEnumerator ClearCacheAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_ClearCache.\u003CClearCacheAsync\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }
  }
}
