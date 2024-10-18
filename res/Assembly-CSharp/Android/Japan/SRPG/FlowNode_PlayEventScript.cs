// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_PlayEventScript
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;

namespace SRPG
{
  [FlowNode.NodeType("SRPG/Play Event Script", 32741)]
  [FlowNode.Pin(1, "Start", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "Finished", FlowNode.PinTypes.Output, 10)]
  public class FlowNode_PlayEventScript : FlowNode
  {
    public string ScriptID;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1 || this.enabled)
        return;
      this.enabled = true;
      this.StartCoroutine(this.LoadAndPlayAsync("Events/" + this.ScriptID));
    }

    [DebuggerHidden]
    private IEnumerator LoadAndPlayAsync(string path)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_PlayEventScript.\u003CLoadAndPlayAsync\u003Ec__Iterator0() { path = path, \u0024this = this };
    }
  }
}
