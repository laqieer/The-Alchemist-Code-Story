// Decompiled with JetBrains decompiler
// Type: SRPG.RankMatchMatchingInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
      ProgressWindow.OpenRankMatchLoadScreen();
    }
  }
}
