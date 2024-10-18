// Decompiled with JetBrains decompiler
// Type: SRPG.VersusMatchingInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "Matching", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "DraftMatching", FlowNode.PinTypes.Input, 2)]
  public class VersusMatchingInfo : MonoBehaviour, IFlowInterface
  {
    public void Start()
    {
      MonoSingleton<GameManager>.Instance.AudienceMode = false;
      GlobalVars.IsVersusDraftMode = false;
    }

    public void Activated(int pinID)
    {
      MonoSingleton<GameManager>.Instance.AudienceMode = false;
      if (pinID == 1)
      {
        GlobalVars.IsVersusDraftMode = false;
        this.StartCoroutine(ProgressWindow.OpenVersusLoadScreenAsync());
      }
      else
      {
        if (pinID != 2)
          return;
        GlobalVars.IsVersusDraftMode = true;
        this.StartCoroutine(ProgressWindow.OpenVersusDraftLoadScreenAsync());
      }
    }
  }
}
