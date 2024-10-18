// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_VersusAudienceWaitStarted
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Multi/Versus/AudienceWaitStarted", 32741)]
  [FlowNode.Pin(0, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "ForceFinish", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "Finish", FlowNode.PinTypes.Output, 10)]
  public class FlowNode_VersusAudienceWaitStarted : FlowNode
  {
    private const int PIN_IN_START = 0;
    private const int PIN_IN_FORCE_FINISH = 1;
    private const int PIN_OUT_FINISH = 10;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
      {
        if (pinID != 1)
          return;
        this.Finished();
      }
      else
        ((Behaviour) this).enabled = true;
    }

    private void Update()
    {
      if (MonoSingleton<GameManager>.Instance.AudienceManager.GetStartedParam() == null)
        return;
      this.Finished();
    }

    private void Finished()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(10);
    }
  }
}
