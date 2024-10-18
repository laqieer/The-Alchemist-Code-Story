// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CheckTipsReaded
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("Tips/CheckTipsReaded", 32741)]
  [FlowNode.Pin(1, "既読か？", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "True", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(3, "False", FlowNode.PinTypes.Output, 3)]
  public class FlowNode_CheckTipsReaded : FlowNode
  {
    private const int PIN_ID_IN = 1;
    private const int PIN_ID_TRUE = 2;
    private const int PIN_ID_FALSE = 3;
    [SerializeField]
    private string Tips;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      if (MonoSingleton<GameManager>.Instance.Tips.Contains(this.Tips))
        this.ActivateOutputLinks(2);
      else
        this.ActivateOutputLinks(3);
    }
  }
}
