// Decompiled with JetBrains decompiler
// Type: SRPG.StoryExChallengeCountResetRoot
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(10, "開始", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(100, "アイテムで挑戦回数リセット", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(200, "幻晶石で挑戦回数リセット", FlowNode.PinTypes.Output, 200)]
  [FlowNode.Pin(300, "エラー", FlowNode.PinTypes.Output, 300)]
  public class StoryExChallengeCountResetRoot : MonoBehaviour, IFlowInterface
  {
    private const int PIN_INPUT_START = 10;
    private const int PIN_OUTPUT_RESET_ITEM = 100;
    private const int PIN_OUTPUT_RESET_COIN = 200;
    private const int PIN_OUTPUT_ERROR = 300;

    public void Activated(int pinID)
    {
      if (pinID != 10)
        return;
      this.SendOutputPin();
    }

    private void SendOutputPin()
    {
      QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
      if (quest == null)
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 300);
      }
      else
      {
        ResetCostParam resetCost = MonoSingleton<GameManager>.Instance.MasterParam.FindResetCost(quest.ResetCost);
        if (resetCost == null)
        {
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 300);
        }
        else
        {
          if (resetCost.IsEnableItemCost())
          {
            ResetCostInfoParam resetCostInfo = resetCost.GetResetCostInfo(eResetCostType.Item);
            int needNum = resetCostInfo.GetNeedNum((int) quest.dailyReset);
            if (MonoSingleton<GameManager>.Instance.Player.GetItemAmount(resetCostInfo.Item) >= needNum)
            {
              FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
              return;
            }
          }
          if (resetCost.IsEnableCoinCost())
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 200);
          else
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 300);
        }
      }
    }
  }
}
