// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_GachaSimpleAnimFlag
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/Gacha/SimpleAnimFlag", 32741)]
  [FlowNode.Pin(0, "簡易演出フラグをON", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "簡易演出フラグをOFF", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "簡易演出フラグ更新終了", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(10, "簡易演出フラグ状態確認", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(11, "簡易演出フラグがON", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(12, "簡易演出フラグがOFF", FlowNode.PinTypes.Output, 12)]
  public class FlowNode_GachaSimpleAnimFlag : FlowNode
  {
    private const int PIN_IN_SET_SIMPLE_ANIM_ON = 0;
    private const int PIN_IN_SET_SIMPLE_ANIM_OFF = 1;
    private const int PIN_OT_SET_SIMPLE_ANIM = 2;
    private const int PIN_IN_IS_SIMPLE_ANIM = 10;
    private const int PIN_OT_IS_SIMPLE_ANIM_ON = 11;
    private const int PIN_OT_IS_SIMPLE_ANIM_OFF = 12;
    [SerializeField]
    private GachaResultThumbnailWindow m_Window;

    public override void OnActivate(int pinID)
    {
      if (Object.op_Equality((Object) this.m_Window, (Object) null))
      {
        DebugUtility.LogError("必要なオブジェクトが指定されていません.");
      }
      else
      {
        int pinID1 = 2;
        if (pinID == 10)
          pinID1 = !this.m_Window.IsSimple ? 12 : 11;
        else
          this.m_Window.SetSimpleAnimFlag(pinID == 0);
        this.ActivateOutputLinks(pinID1);
      }
    }
  }
}
