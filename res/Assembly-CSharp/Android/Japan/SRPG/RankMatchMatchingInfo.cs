// Decompiled with JetBrains decompiler
// Type: SRPG.RankMatchMatchingInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Matching", FlowNode.PinTypes.Input, 1)]
  public class RankMatchMatchingInfo : MonoBehaviour, IFlowInterface
  {
    public void Start()
    {
      MonoSingleton<GameManager>.Instance.AudienceMode = false;
    }

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.StartCoroutine(ProgressWindow.OpenRankMatchLoadScreenAsync());
    }
  }
}
