// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SetTutorialFlag
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Tutorial/SetTutorialFlag", 32741)]
  [FlowNode.Pin(1, "Set", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(2, "Flag Changed (Yes)", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(3, "Flag Changed (No)", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(5, "Flag Unchanged", FlowNode.PinTypes.Output, 3)]
  [FlowNode.Pin(10, "Test", FlowNode.PinTypes.Input, 20)]
  [FlowNode.Pin(11, "True", FlowNode.PinTypes.Output, 21)]
  [FlowNode.Pin(12, "False", FlowNode.PinTypes.Output, 22)]
  public class FlowNode_SetTutorialFlag : FlowNode
  {
    private const int PIN_ID_SET = 1;
    private const int PIN_ID_UPDATE1 = 2;
    private const int PIN_ID_UPDATE2 = 3;
    private const int PIN_ID_NOUPDATE = 5;
    private const int PIN_ID_TEST = 10;
    private const int PIN_ID_TRUE = 11;
    private const int PIN_ID_FALSE = 12;
    public TutorialFlags mTutorialFlags;
    public string FlagID;
    public string ConfirmText;

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 1:
          if (MonoSingleton<GameManager>.Instance.IsTutorialFlagSet(this.FlagID))
          {
            this.ActivateOutputLinks(5);
            break;
          }
          if (!string.IsNullOrEmpty(this.ConfirmText))
          {
            UIUtility.ConfirmBox(LocalizedText.Get(this.ConfirmText), new UIUtility.DialogResultEvent(this.OnYes), new UIUtility.DialogResultEvent(this.OnNo), systemModal: true);
            break;
          }
          this.OnYes((GameObject) null);
          break;
        case 10:
          if (MonoSingleton<GameManager>.Instance.IsTutorialFlagSet(this.FlagID))
          {
            this.ActivateOutputLinks(11);
            break;
          }
          this.ActivateOutputLinks(12);
          break;
      }
    }

    private void OnYes(GameObject go)
    {
      MonoSingleton<GameManager>.Instance.UpdateTutorialFlags(this.FlagID);
      this.ActivateOutputLinks(2);
    }

    private void OnNo(GameObject go)
    {
      MonoSingleton<GameManager>.Instance.UpdateTutorialFlags(this.FlagID);
      this.ActivateOutputLinks(3);
    }
  }
}
