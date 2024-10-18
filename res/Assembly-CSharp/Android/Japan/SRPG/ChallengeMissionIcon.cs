// Decompiled with JetBrains decompiler
// Type: SRPG.ChallengeMissionIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(0, "更新", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "表示", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "非表示", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(10, "進捗表示", FlowNode.PinTypes.Output, 10)]
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
      }
    }

    public void ShowImages(bool value)
    {
      Image component1 = this.GetComponent<Image>();
      if ((UnityEngine.Object) component1 != (UnityEngine.Object) null)
        component1.enabled = value;
      Button component2 = this.GetComponent<Button>();
      if ((UnityEngine.Object) component2 != (UnityEngine.Object) null)
        component2.enabled = value;
      if (!((UnityEngine.Object) this.Badge != (UnityEngine.Object) null))
        return;
      Image component3 = this.Badge.GetComponent<Image>();
      if (!((UnityEngine.Object) component3 != (UnityEngine.Object) null))
        return;
      component3.enabled = value;
    }

    private void Start()
    {
    }

    [DebuggerHidden]
    private IEnumerator SetAsLastSibling()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ChallengeMissionIcon.\u003CSetAsLastSibling\u003Ec__Iterator0() { \u0024this = this };
    }

    private void Refresh()
    {
      if (!((UnityEngine.Object) MonoSingleton<GameManager>.Instance != (UnityEngine.Object) null))
        return;
      bool flag1 = false;
      bool flag2 = true;
      foreach (TrophyParam trophyParam in ChallengeMission.GetRootTrophiesSortedByPriority())
      {
        if (!ChallengeMission.GetTrophyCounter(trophyParam).IsEnded)
        {
          if (this.IsNotReceiveRewards(trophyParam))
            flag1 = true;
          flag2 = false;
          break;
        }
      }
      this.Badge.SetActive(flag1);
      this.gameObject.SetActive(!flag2);
      if (flag2)
        return;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
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
  }
}
