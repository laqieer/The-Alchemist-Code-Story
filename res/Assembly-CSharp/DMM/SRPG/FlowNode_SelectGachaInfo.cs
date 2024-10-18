// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SelectGachaInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("SRPG/Gacha/SelectGachaInfo", 32741)]
  [FlowNode.Pin(0, "Input", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "SelectDetailOnly", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "SelectDetailAndRate", FlowNode.PinTypes.Output, 11)]
  public class FlowNode_SelectGachaInfo : FlowNode
  {
    private const int PIN_IN_INPUT = 0;
    private const int PIN_OT_SELECT_DETAIL_ONLY = 10;
    private const int PIN_OT_SELECT_DETAIL_RATE = 11;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0 || Object.op_Equality((Object) GachaWindow.Instance, (Object) null))
        return;
      GachaTopParamNew[] currentGacha = GachaWindow.Instance.GetCurrentGacha();
      if (currentGacha == null || currentGacha.Length <= 0)
        return;
      bool flag = currentGacha[0].is_rate_view || currentGacha[0].IsPickupView;
      FlowNode_Variable.Set("SHARED_WEBWINDOW_URL", FlowNode_Variable.Get("SHARED_WEBWINDOW_URL2"));
      if (!flag)
        this.ActivateOutputLinks(10);
      else
        this.ActivateOutputLinks(11);
    }
  }
}
