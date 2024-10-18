// Decompiled with JetBrains decompiler
// Type: SRPG.GvGEndWindowRoot
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(10, "初期化", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(20, "本日の結果表示", FlowNode.PinTypes.Input, 20)]
  [FlowNode.Pin(30, "シーズンの結果表示", FlowNode.PinTypes.Input, 30)]
  [FlowNode.Pin(1010, "初期化終了", FlowNode.PinTypes.Output, 1010)]
  [FlowNode.Pin(1020, "本日の結果表示する", FlowNode.PinTypes.Output, 1020)]
  [FlowNode.Pin(1030, "本日の結果表示する必要なし", FlowNode.PinTypes.Output, 1030)]
  [FlowNode.Pin(1050, "シーズンの結果表示する", FlowNode.PinTypes.Output, 1050)]
  [FlowNode.Pin(1040, "シーズンの結果表示する必要なし", FlowNode.PinTypes.Output, 1040)]
  public class GvGEndWindowRoot : MonoBehaviour, IFlowInterface
  {
    private const int PIN_INPUT_INIT = 10;
    private const int PIN_INPUT_OPEN_DAILY = 20;
    private const int PIN_INPUT_OPEN_SEASON = 30;
    private const int PIN_INPUT_CLOSE = 100;
    private const int PIN_OUTPUT_INIT_END = 1010;
    private const int PIN_OUTPUT_OPEN_DAILY = 1020;
    private const int PIN_OUTPUT_NOT_OPEN_DAILY = 1030;
    private const int PIN_OUTPUT_NOT_OPEN_SEASON = 1040;
    private const int PIN_OUTPUT_OPEN_SEASON = 1050;

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 10:
          this.Init();
          break;
        case 20:
          this.OpenResult_Daily();
          break;
        case 30:
          this.OpenResult_Season();
          break;
      }
    }

    private void Init()
    {
      if (Object.op_Equality((Object) GvGManager.Instance, (Object) null))
        return;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 1010);
    }

    private void OpenResult_Daily()
    {
      int pinID = 1030;
      if (Object.op_Inequality((Object) GvGManager.Instance, (Object) null) && GvGManager.Instance.ResultDaily != null)
        pinID = 1020;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, pinID);
    }

    private void OpenResult_Season()
    {
      int pinID = 1040;
      if (Object.op_Inequality((Object) GvGManager.Instance, (Object) null) && GvGManager.Instance.ResultSeason != null)
        pinID = 1050;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, pinID);
    }
  }
}
