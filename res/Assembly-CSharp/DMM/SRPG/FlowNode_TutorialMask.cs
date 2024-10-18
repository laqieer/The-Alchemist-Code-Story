// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_TutorialMask
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Tutorial/Mask", 32741)]
  [FlowNode.Pin(1, "CREATE", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "DESTORY", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "OPEND", FlowNode.PinTypes.Output, 102)]
  public class FlowNode_TutorialMask : FlowNode
  {
    private const int PIN_IN_CREATE = 1;
    private const int PIN_OUT_DESTORY = 101;
    private const int PIN_OUT_OPEND = 102;
    public FlowNode_TutorialMask.eComponentId ComponentId;
    [SerializeField]
    private GameObject Mask;
    [SerializeField]
    private float NoResponseTime;
    [SerializeField]
    private string MaskPath = "UI/TutMask";
    private Vector3 mWorldPosition;
    private TutorialMask.eActionType mActionType;
    private bool mIsWorld2Screen;
    private string mText;
    private Vector2 mSize;

    public override void OnActivate(int pinID)
    {
      if (string.IsNullOrEmpty(this.MaskPath) || pinID != 1)
        return;
      this.StartCoroutine(this.CreateMask());
    }

    public void Setup(
      TutorialMask.eActionType act_type,
      Vector3 world_pos,
      bool is_world2screen,
      string text = null)
    {
      this.mActionType = act_type;
      this.mWorldPosition = world_pos;
      this.mIsWorld2Screen = is_world2screen;
      this.mText = text;
    }

    public void SetupMaskSize(float width, float height) => this.mSize = new Vector2(width, height);

    [DebuggerHidden]
    private IEnumerator CreateMask()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_TutorialMask.\u003CCreateMask\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    private void OnOpendMask() => this.ActivateOutputLinks(102);

    private void OnDestroyMask() => this.ActivateOutputLinks(101);

    public enum eComponentId
    {
      MOVE_UNIT,
      ATTACK_TARGET_DESC,
      NORMAL_ATTACK_DESC,
      ABILITY_DESC,
      TAP_NORMAL_ATTACK,
      EXEC_NORMAL_ATTACK,
      SELECT_DIR,
      CLOSE_BATTLE_INFO,
    }
  }
}
