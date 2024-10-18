// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_TutorialGacha
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

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
      if (!((UnityEngine.Object) this.mGachaController != (UnityEngine.Object) null))
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) this.mGachaController.gameObject);
      this.mGachaController = (GachaController) null;
    }

    public override void OnActivate(int pinID)
    {
      if (pinID != 0 || this.enabled)
        return;
      this.enabled = true;
      this.StartCoroutine(this.PlayGachaAsync());
    }

    [DebuggerHidden]
    private IEnumerator PlayGachaAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_TutorialGacha.\u003CPlayGachaAsync\u003Ec__Iterator0() { \u0024this = this };
    }

    private void Finished()
    {
      this.enabled = false;
      this.ActivateOutputLinks(1);
    }
  }
}
