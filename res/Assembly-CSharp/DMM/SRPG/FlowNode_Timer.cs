// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_Timer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Timer/Timer", 32741)]
  [FlowNode.Pin(10, "タイマー起動", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(20, "タイマーリセット", FlowNode.PinTypes.Input, 20)]
  [FlowNode.Pin(30, "タイマー停止", FlowNode.PinTypes.Input, 30)]
  [FlowNode.Pin(1001, "完了", FlowNode.PinTypes.Output, 1001)]
  public class FlowNode_Timer : FlowNode
  {
    private const int PIN_INPUT_START = 10;
    private const int PIN_INPUT_RESET = 20;
    private const int PIN_INPUT_STOP = 30;
    private const int PIN_OUTPUT_FINISH = 1001;
    [SerializeField]
    private float mTime;
    private bool mIsCheck;
    private float mRestTime;

    protected override void Awake()
    {
      base.Awake();
      this.ResetTimer();
    }

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 10:
          this.StartCheck();
          break;
        case 20:
          this.ResetTimer();
          break;
        case 30:
          this.StopCheck();
          break;
      }
    }

    private void StartCheck()
    {
      ((Behaviour) this).enabled = true;
      this.mIsCheck = true;
    }

    private void StopCheck()
    {
      ((Behaviour) this).enabled = false;
      this.mIsCheck = false;
    }

    private void ResetTimer() => this.mRestTime = this.mTime;

    private void Update()
    {
      if (!this.mIsCheck)
        return;
      if ((double) this.mRestTime <= 0.0)
      {
        this.ActivateOutputLinks(1001);
        this.ResetTimer();
        this.StopCheck();
      }
      else
        this.mRestTime -= Time.deltaTime;
    }
  }
}
