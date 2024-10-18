// Decompiled with JetBrains decompiler
// Type: SRPG.ChallengeMissionIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(0, "更新", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "表示", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "非表示", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(3, "初期化", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(10, "進捗表示", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(20, "進捗非表示", FlowNode.PinTypes.Output, 20)]
  public class ChallengeMissionIcon : MonoBehaviour, IFlowInterface
  {
    public GameObject Badge;

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 0:
          this.Refresh();
          break;
        case 1:
          this.ShowImages(true);
          break;
        case 2:
          this.ShowImages(false);
          break;
        case 3:
          this.Init();
          break;
      }
    }

    public void ShowImages(bool value)
    {
      Image component1 = ((Component) this).GetComponent<Image>();
      if (Object.op_Inequality((Object) component1, (Object) null))
        ((Behaviour) component1).enabled = value;
      Button component2 = ((Component) this).GetComponent<Button>();
      if (Object.op_Inequality((Object) component2, (Object) null))
        ((Behaviour) component2).enabled = value;
      if (!Object.op_Inequality((Object) this.Badge, (Object) null))
        return;
      Image component3 = this.Badge.GetComponent<Image>();
      if (!Object.op_Inequality((Object) component3, (Object) null))
        return;
      ((Behaviour) component3).enabled = value;
    }

    private void Init()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, !this.IsDispChallengeMissionIcon() ? 20 : 10);
      this.Refresh();
    }

    [DebuggerHidden]
    private IEnumerator AutoOpen()
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      ChallengeMissionIcon.\u003CAutoOpen\u003Ec__Iterator0 autoOpenCIterator0 = new ChallengeMissionIcon.\u003CAutoOpen\u003Ec__Iterator0();
      return (IEnumerator) autoOpenCIterator0;
    }

    private void Refresh()
    {
      if (!Object.op_Inequality((Object) MonoSingleton<GameManager>.Instance, (Object) null))
        return;
      bool badge = false;
      bool is_complated = true;
      this.GetIsComplatedAndIsBadgeActive(ref is_complated, ref badge);
      this.Badge.SetActive(badge);
      if (!is_complated)
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
      else
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 20);
    }

    private bool IsNotReceiveRewards(TrophyParam rootTrophy)
    {
      foreach (TrophyParam childeTrophy in ChallengeMission.GetChildeTrophies(rootTrophy))
      {
        TrophyState trophyCounter = ChallengeMission.GetTrophyCounter(childeTrophy);
        if (trophyCounter.IsCompleted && !trophyCounter.IsEnded)
          return true;
      }
      return false;
    }

    private void GetIsComplatedAndIsBadgeActive(ref bool is_complated, ref bool badge)
    {
      badge = false;
      is_complated = true;
      foreach (TrophyParam trophyParam in ChallengeMission.GetRootTrophiesSortedByPriority())
      {
        if (!ChallengeMission.GetTrophyCounter(trophyParam).IsEnded)
        {
          if (this.IsNotReceiveRewards(trophyParam))
            badge = true;
          is_complated = false;
        }
      }
    }

    private bool IsDispChallengeMissionIcon()
    {
      bool badge = false;
      bool is_complated = true;
      this.GetIsComplatedAndIsBadgeActive(ref is_complated, ref badge);
      return !is_complated;
    }
  }
}
