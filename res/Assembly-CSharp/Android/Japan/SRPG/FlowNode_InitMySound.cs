// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_InitMySound
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;

namespace SRPG
{
  [FlowNode.NodeType("Sound/InitMySound", 65535)]
  [FlowNode.Pin(0, "In", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Out", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_InitMySound : FlowNode
  {
    public bool UseEmb;
    public bool ForceReInit;

    private void Init()
    {
      MyCriManager.Setup(this.UseEmb);
      DebugUtility.LogWarning("[MyCriManager] Setup:" + (object) this.UseEmb);
    }

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (MyCriManager.IsInitialized())
      {
        if (!this.ForceReInit && MyCriManager.UsingEmb == this.UseEmb)
        {
          DebugUtility.LogWarning("[MyCriManager] NoNeed to Setup:" + (object) this.UseEmb);
          this.ActivateOutputLinks(1);
        }
        else
        {
          this.enabled = true;
          this.StartCoroutine(this.Restart());
        }
      }
      else
      {
        this.Init();
        this.ActivateOutputLinks(1);
      }
    }

    [DebuggerHidden]
    private IEnumerator Restart()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_InitMySound.\u003CRestart\u003Ec__Iterator0() { \u0024this = this };
    }
  }
}
