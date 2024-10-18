// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_IsRemainResultInspirationSkill
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.NodeType("Battle/IsRemainResultInspirationSkill", 4513092)]
  [FlowNode.Pin(1, "ひらめき演出があるか？(初期化)", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(11, "まだひらめき演出があるか？(継続)", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(111, "ある", FlowNode.PinTypes.Output, 111)]
  [FlowNode.Pin(112, "ない", FlowNode.PinTypes.Output, 112)]
  public class FlowNode_IsRemainResultInspirationSkill : FlowNode
  {
    private const int PIN_IN_IS_EXIST = 1;
    private const int PIN_IN_IS_NEXT = 11;
    private const int PIN_OUT_YES = 111;
    private const int PIN_OUT_NO = 112;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1 && pinID != 11)
        return;
      if (pinID == 1)
        ResultInspirationSkill.InitEffect();
      if (ResultInspirationSkill.IsRemainEffect())
      {
        this.ActivateOutputLinks(111);
      }
      else
      {
        ResultInspirationSkill.DestroyEffect();
        this.ActivateOutputLinks(112);
      }
    }
  }
}
