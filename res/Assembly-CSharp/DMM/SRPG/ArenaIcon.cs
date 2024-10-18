// Decompiled with JetBrains decompiler
// Type: SRPG.ArenaIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(0, "更新", FlowNode.PinTypes.Input, 0)]
  public class ArenaIcon : MonoBehaviour, IFlowInterface
  {
    private const int PIN_IN_REFRESH = 0;
    [SerializeField]
    private GameObject Badge;

    public void Activated(int pinID)
    {
      if (pinID != 0)
        return;
      this.Refresh();
    }

    private void Refresh()
    {
      if (!Object.op_Inequality((Object) this.Badge, (Object) null))
        return;
      this.Badge.SetActive(MonoSingleton<GameManager>.Instance.Player.HasArenaReward);
    }

    private void Update()
    {
      if (!Object.op_Inequality((Object) this.Badge, (Object) null))
        return;
      this.Badge.SetActive(MonoSingleton<GameManager>.Instance.Player.HasArenaReward);
    }
  }
}
