// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_NotifyInvitation
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(120, "通知監視リセット", FlowNode.PinTypes.Input, 120)]
  [FlowNode.Pin(100, "通知監視開始", FlowNode.PinTypes.Input, 100)]
  [FlowNode.NodeType("System/Notify/Invitation", 32741)]
  [FlowNode.Pin(110, "通知監視停止", FlowNode.PinTypes.Input, 110)]
  [FlowNode.Pin(191, "通知チェック", FlowNode.PinTypes.Output, 190)]
  public class FlowNode_NotifyInvitation : FlowNodePersistent
  {
    public float m_Interval = 10f;
    public const int INPUT_NOTIFYMONITOR_START = 100;
    public const int INPUT_NOTIFYMONITOR_STOP = 110;
    public const int INPUT_NOTIFYMONITOR_RESET = 120;
    public const int OUTPUT_NOTIFY_GO = 191;
    private bool m_Monitor;
    private float m_Time;

    protected override void Awake()
    {
      base.Awake();
    }

    private void Start()
    {
    }

    protected override void OnDestroy()
    {
      base.OnDestroy();
    }

    private void Update()
    {
      if (!NotifyList.hasInstance)
        return;
      if (this.m_Monitor)
      {
        if ((double) this.m_Interval < 10.0)
          this.m_Interval = 10f;
        this.m_Time += Time.deltaTime;
        if ((double) this.m_Time <= (double) this.m_Interval || !this.CanNotifyGo())
          return;
        this.m_Time = 0.0f;
        this.ActivateOutputLinks(191);
      }
      else
        this.m_Time = 0.0f;
    }

    private bool CanNotifyGo()
    {
      if ((UnityEngine.Object) HomeWindow.Current == (UnityEngine.Object) null && (UnityEngine.Object) SceneBattle.Instance == (UnityEngine.Object) null || (UnityEngine.Object) HomeWindow.Current != (UnityEngine.Object) null && HomeWindow.Current.IsSceneChanging)
        return false;
      if ((UnityEngine.Object) SceneBattle.Instance != (UnityEngine.Object) null)
      {
        BattleCore battle = SceneBattle.Instance.Battle;
        if (battle != null && string.IsNullOrEmpty(battle.QuestID))
          return false;
      }
      return true;
    }

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 100:
          MultiInvitationBadge.isValid = false;
          this.m_Monitor = true;
          this.m_Time = this.m_Interval;
          this.enabled = true;
          break;
        case 110:
          this.m_Monitor = false;
          this.m_Time = 0.0f;
          this.enabled = false;
          break;
        case 120:
          this.m_Time = 0.0f;
          break;
      }
    }
  }
}
