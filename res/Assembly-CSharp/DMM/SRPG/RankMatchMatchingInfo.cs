// Decompiled with JetBrains decompiler
// Type: SRPG.RankMatchMatchingInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "Matching", FlowNode.PinTypes.Input, 1)]
  public class RankMatchMatchingInfo : MonoBehaviour, IFlowInterface
  {
    public void Start() => MonoSingleton<GameManager>.Instance.AudienceMode = false;

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.StartCoroutine(ProgressWindow.OpenRankMatchLoadScreenAsync());
    }
  }
}
