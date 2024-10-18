// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_TutorialGacha
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections;
using System.Diagnostics;

namespace SRPG
{
  [FlowNode.NodeType("UI/Tutorial Gacha")]
  [FlowNode.Pin(1, "Finished", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(0, "Start", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_TutorialGacha : FlowNode
  {
    public int UnitIndex;
    [StringIsResourcePath(typeof (GachaController))]
    public string Prefab_GachaController;
    private GachaController mGachaController;

    protected override void OnDestroy()
    {
      base.OnDestroy();
      if (!((UnityEngine.Object) this.mGachaController != (UnityEngine.Object) null))
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) this.mGachaController.gameObject);
      this.mGachaController = (GachaController) null;
    }

    public override void OnActivate(int pinID)
    {
      if (pinID != 0 || this.enabled)
        return;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      if (this.UnitIndex < 0 || player.Units.Count <= this.UnitIndex)
        return;
      if (!GlobalVars.IsTutorialEnd)
        AnalyticsManager.TrackTutorialAnalyticsEvent("0_6b_2d.017");
      this.enabled = true;
      this.StartCoroutine(this.PlayGachaAsync(player.Units[this.UnitIndex].UnitParam));
    }

    [DebuggerHidden]
    private IEnumerator PlayGachaAsync(UnitParam unit)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_TutorialGacha.\u003CPlayGachaAsync\u003Ec__IteratorDE() { unit = unit, \u003C\u0024\u003Eunit = unit, \u003C\u003Ef__this = this };
    }
  }
}
