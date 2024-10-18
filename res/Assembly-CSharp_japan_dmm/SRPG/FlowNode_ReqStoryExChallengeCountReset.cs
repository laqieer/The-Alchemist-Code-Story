// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqStoryExChallengeCountReset
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("StoryExChallengeCount/Reset", 32741)]
  [FlowNode.Pin(10, "挑戦回数リセット開始", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(110, "挑戦回数リセット終了", FlowNode.PinTypes.Output, 110)]
  [FlowNode.Pin(120, "幻晶石が不足", FlowNode.PinTypes.Output, 120)]
  public class FlowNode_ReqStoryExChallengeCountReset : FlowNode_Network
  {
    private const int PIN_INPUT_START = 10;
    private const int PIN_OUTPUT_END = 110;
    private const int PIN_OUTPUT_ERR_NOT_ENOUGH_COIN = 120;
    [SerializeField]
    private eResetCostType mCostType;

    public override void OnActivate(int pinID)
    {
      QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
      if (quest == null || pinID != 10)
        return;
      ((Behaviour) this).enabled = true;
      this.ExecRequest((WebAPI) new ReqBtlComReset(quest.iname, this.mCostType, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.QuestResetNotEnoughCoin:
            this.ActivateOutputLinks(120);
            ((Behaviour) this).enabled = false;
            Network.RemoveAPI();
            Network.ResetError();
            break;
          default:
            FlowNode_Network.Failed();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<PartyWindow2.JSON_ReqBtlComResetResponse> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<PartyWindow2.JSON_ReqBtlComResetResponse>>(www.text);
        if (jsonObject.body == null)
        {
          FlowNode_Network.Failed();
        }
        else
        {
          try
          {
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.items);
            if (!MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.quests))
            {
              FlowNode_Network.Failed();
              return;
            }
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
            FlowNode_Network.Failed();
            return;
          }
          Network.RemoveAPI();
          ((Behaviour) this).enabled = false;
          this.ActivateOutputLinks(110);
        }
      }
    }
  }
}
