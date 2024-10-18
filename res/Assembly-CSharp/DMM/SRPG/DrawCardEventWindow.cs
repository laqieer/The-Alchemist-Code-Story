// Decompiled with JetBrains decompiler
// Type: SRPG.DrawCardEventWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "IsEnable", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Enable", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(111, "Disable", FlowNode.PinTypes.Output, 111)]
  public class DrawCardEventWindow : MonoBehaviour, IFlowInterface
  {
    private const int INPUT_PIN_IS_ENABLE = 1;
    private const int OUTPUT_PIN_ENABLE = 101;
    private const int OUTPUT_PIN_DISABLE = 111;
    [SerializeField]
    private GameObject RuleWindow;
    [SerializeField]
    private GameObject DrawCard;
    [SerializeField]
    private GameObject GameStart;

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.Initialize();
    }

    public void Initialize()
    {
      if (Object.op_Inequality((Object) this.DrawCard, (Object) null))
        this.DrawCard.SetActive(false);
      if (Object.op_Inequality((Object) this.GameStart, (Object) null))
        this.GameStart.SetActive(false);
      if (Object.op_Inequality((Object) this.RuleWindow, (Object) null))
        this.RuleWindow.SetActive(false);
      if (DrawCardParam.DrawCardEnabled)
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
      else
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 111);
    }
  }
}
