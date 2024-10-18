// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_TutorialGacha
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("UI/Tutorial Gacha")]
  [FlowNode.Pin(0, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Finished", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_TutorialGacha : FlowNode
  {
    private const int PIN_IN_TUTORIAL_GACHA_START = 0;
    private const int PIN_OU_TUTORIAL_GACHA_FINISHED = 1;
    public int UnitIndex;
    [StringIsResourcePath(typeof (GachaController))]
    public string Prefab_GachaController;
    [SerializeField]
    [StringIsResourcePath(typeof (TutorialGacha))]
    public string Prefab_TutorialGacha;
    private GachaController mGachaController;
    private TutorialGacha m_TutorialGacha;

    protected override void OnDestroy()
    {
      base.OnDestroy();
      if (!Object.op_Inequality((Object) this.mGachaController, (Object) null))
        return;
      Object.Destroy((Object) ((Component) this.mGachaController).gameObject);
      this.mGachaController = (GachaController) null;
    }

    public override void OnActivate(int pinID)
    {
      if (pinID != 0 || ((Behaviour) this).enabled)
        return;
      ((Behaviour) this).enabled = true;
      this.StartCoroutine(this.PlayGachaAsync());
    }

    [DebuggerHidden]
    private IEnumerator PlayGachaAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_TutorialGacha.\u003CPlayGachaAsync\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    private void Finished()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(1);
    }
  }
}
