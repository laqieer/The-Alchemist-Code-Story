// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_InitMySound
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

#nullable disable
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
          ((Behaviour) this).enabled = true;
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
      return (IEnumerator) new FlowNode_InitMySound.\u003CRestart\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }
  }
}
