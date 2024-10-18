// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_EndQuest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.Pin(0, "End", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("System/クエスト終了", 32741)]
  [FlowNode.Pin(1, "ForceEnd", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "ForceEnded", FlowNode.PinTypes.Output, 101)]
  public class FlowNode_EndQuest : FlowNode
  {
    public bool Restart;

    public override void OnActivate(int pinID)
    {
      if (pinID == 0 && (UnityEngine.Object) SceneBattle.Instance != (UnityEngine.Object) null)
      {
        if (Network.Mode == Network.EConnectMode.Offline)
        {
          QuestParam quest = SceneBattle.Instance.Battle.GetQuest();
          BattleCore.Record questRecord = SceneBattle.Instance.Battle.GetQuestRecord();
          if (quest != null && questRecord != null)
            quest.clear_missions |= questRecord.bonusFlags;
        }
        SceneBattle.Instance.ExitRequest = !this.Restart ? SceneBattle.ExitRequests.End : SceneBattle.ExitRequests.Restart;
      }
      else
      {
        if (pinID != 1)
          return;
        if ((UnityEngine.Object) SceneBattle.Instance == (UnityEngine.Object) null)
        {
          this.enabled = false;
          this.ActivateOutputLinks(101);
        }
        else
        {
          this.enabled = true;
          SceneBattle.Instance.ForceEndQuest();
        }
      }
    }

    private void Update()
    {
      if (!((UnityEngine.Object) SceneBattle.Instance == (UnityEngine.Object) null))
        return;
      this.enabled = false;
      this.ActivateOutputLinks(101);
    }
  }
}
