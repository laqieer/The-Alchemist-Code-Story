// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CheckMasterDataLoadError
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/Master/CheckMasterDataLoadError", 32741)]
  [FlowNode.Pin(0, "Check", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(2, "Failed", FlowNode.PinTypes.Output, 2)]
  public class FlowNode_CheckMasterDataLoadError : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (string.IsNullOrEmpty(MonoSingleton<GameManager>.Instance.MasterDataLoadErrorMessage))
        this.Success();
      else
        EmbedSystemMessage.Create(MonoSingleton<GameManager>.Instance.MasterDataLoadErrorMessage, (EmbedSystemMessage.SystemMessageEvent) (go =>
        {
          MonoSingleton<GameManager>.Instance.MasterDataLoadErrorMessage = string.Empty;
          this.Failure();
        }), true);
    }

    private void Success()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(1);
    }

    private void Failure()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(2);
    }
  }
}
