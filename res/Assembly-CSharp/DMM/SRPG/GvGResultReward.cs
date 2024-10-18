// Decompiled with JetBrains decompiler
// Type: SRPG.GvGResultReward
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "報酬はあるか？", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "結果あり", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "結果なし", FlowNode.PinTypes.Output, 101)]
  public class GvGResultReward : MonoBehaviour, IFlowInterface
  {
    public const int PIN_INPUT_ISREWARD = 1;
    public const int PIN_OUTPUT_REWARD = 100;
    public const int PIN_OUTPUT_EMPTY = 101;

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      if (Object.op_Inequality((Object) GvGManager.Instance, (Object) null) && GvGManager.Instance.ResultReward != null)
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      else
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
    }
  }
}
