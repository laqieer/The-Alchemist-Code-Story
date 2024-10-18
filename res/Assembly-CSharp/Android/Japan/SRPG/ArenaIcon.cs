// Decompiled with JetBrains decompiler
// Type: SRPG.ArenaIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;

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
      if (!((UnityEngine.Object) this.Badge != (UnityEngine.Object) null))
        return;
      this.Badge.SetActive(MonoSingleton<GameManager>.Instance.Player.HasArenaReward);
    }

    private void Update()
    {
      if (!((UnityEngine.Object) this.Badge != (UnityEngine.Object) null))
        return;
      this.Badge.SetActive(MonoSingleton<GameManager>.Instance.Player.HasArenaReward);
    }
  }
}
