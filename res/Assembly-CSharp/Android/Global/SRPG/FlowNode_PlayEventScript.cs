// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_PlayEventScript
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;

namespace SRPG
{
  [FlowNode.Pin(1, "Start", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "Finished", FlowNode.PinTypes.Output, 10)]
  [FlowNode.NodeType("SRPG/Play Event Script", 32741)]
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
      return (IEnumerator) new FlowNode_PlayEventScript.\u003CLoadAndPlayAsync\u003Ec__IteratorD6() { path = path, \u003C\u0024\u003Epath = path, \u003C\u003Ef__this = this };
    }
  }
}
