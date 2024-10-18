// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ClearCache
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;

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
      if (pinID != 0 || this.enabled)
        return;
      CriticalSection.Enter(CriticalSections.Default);
      this.enabled = true;
      this.StartCoroutine(this.ClearCacheAsync());
      this.ActivateOutputLinks(100);
    }

    [DebuggerHidden]
    private IEnumerator ClearCacheAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_ClearCache.\u003CClearCacheAsync\u003Ec__Iterator0() { \u0024this = this };
    }
  }
}
