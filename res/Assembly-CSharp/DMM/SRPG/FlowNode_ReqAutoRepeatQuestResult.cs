// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqAutoRepeatQuestResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using Gsc.Network.Encoding;
using MessagePack;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("AutoRepeatQuest/ReqResult", 32741)]
  [FlowNode.Pin(10, "自動周回結果の取得開始", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(110, "自動周回結果の取得終了", FlowNode.PinTypes.Output, 110)]
  public class FlowNode_ReqAutoRepeatQuestResult : FlowNode_Network
  {
    private const int PIN_INPUT_GET_RESULT_START = 10;
    private const int PIN_OUTPUT_GET_RESULT_END = 110;

    public override void OnActivate(int pinID)
    {
      if (pinID == 10)
        this.ExecRequest((WebAPI) new ReqAutoRepeatQuestEnd(new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK));
      ((Behaviour) this).enabled = true;
    }

    public override void OnSuccess(WWWResult www)
    {
      if (SRPG.Network.IsError)
      {
        int errCode = (int) SRPG.Network.ErrCode;
        FlowNode_Network.Failed();
      }
      else
      {
        ReqAutoRepeatQuestEnd.Response body;
        if (EncodingTypes.IsJsonSerializeCompressSelected(!GlobalVars.SelectedSerializeCompressMethodWasNodeSet ? EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK : GlobalVars.SelectedSerializeCompressMethod))
        {
          WebAPI.JSON_BodyResponse<ReqAutoRepeatQuestEnd.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqAutoRepeatQuestEnd.Response>>(www.text);
          DebugUtility.Assert(jsonObject != null, "jsonRes == null");
          body = jsonObject.body;
        }
        else
        {
          FlowNode_ReqAutoRepeatQuestResult.MP_AutoRepeatQuestEndResponse questEndResponse = SerializerCompressorHelper.Decode<FlowNode_ReqAutoRepeatQuestResult.MP_AutoRepeatQuestEndResponse>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK));
          DebugUtility.Assert(questEndResponse != null, "mpRes == null");
          body = questEndResponse.body;
        }
        SRPG.Network.RemoveAPI();
        this.SetupSerializeValueBehaviour(body.auto_repeat, body.units, body.runes_detail);
        try
        {
          MonoSingleton<GameManager>.Instance.Deserialize(body.player);
          MonoSingleton<GameManager>.Instance.Deserialize(body.items);
          MonoSingleton<GameManager>.Instance.Deserialize(body.units);
          MonoSingleton<GameManager>.Instance.Deserialize(body.quests);
          MonoSingleton<GameManager>.Instance.Player.Deserialize(body.story_ex_challenge);
          MonoSingleton<GameManager>.Instance.Deserialize(body.area);
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          FlowNode_Network.Failed();
          return;
        }
        try
        {
          MonoSingleton<GameManager>.Instance.Player.TrophyData.OverwriteTrophyProgress(body.trophyprogs);
          MonoSingleton<GameManager>.Instance.Player.TrophyData.OverwriteTrophyProgress(body.bingoprogs);
          MonoSingleton<GameManager>.Instance.Player.TrophyData.OverwriteTrophyProgress(body.guild_trophies);
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          FlowNode_Network.Failed();
          return;
        }
        if (body.cards != null)
        {
          for (int index = 0; index < body.cards.Length; ++index)
          {
            MonoSingleton<GameManager>.Instance.Player.OnDirtyConceptCardData();
            if (body.cards[index].IsGetUnit)
              FlowNode_ConceptCardGetUnit.AddConceptCardData(ConceptCardData.CreateConceptCardDataForDisplay(body.cards[index].iname));
          }
        }
        if (body.auto_repeat != null && body.auto_repeat.drops != null && RuneUtility.CountRuneNum(body.auto_repeat.drops) > 0)
        {
          MonoSingleton<GameManager>.Instance.Player.OnDirtyRuneData();
          MonoSingleton<GameManager>.Instance.Player.SetRuneStorageUsedNum(body.rune_storage_used);
        }
        if (body.guildraid_bp_charge > 0)
          GuildRaidManager.SetNotifyPush();
        MonoSingleton<GameManager>.Instance.Player.AutoRepeatQuestProgress.Reset();
        MyLocalNotification.ResetAutoRepeatQuest();
        ((Behaviour) this).enabled = false;
        this.ActivateOutputLinks(110);
      }
    }

    private void SetupSerializeValueBehaviour(
      Json_AutoRepeatQuestData auto_repeat,
      Json_Unit[] units,
      Json_RuneData[] _runes_detail)
    {
      SerializeValueBehaviour component = ((Component) this).GetComponent<SerializeValueBehaviour>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      AutoRepeatQuest_OldData repeatQuestOldData = new AutoRepeatQuest_OldData();
      repeatQuestOldData.Init(units, player.Lv, player.Exp);
      component.list.SetObject(AutoRepeatQuestSVB_Key.OLD, (object) repeatQuestOldData);
      AutoRepeatQuestData autoRepeatQuestData = new AutoRepeatQuestData();
      autoRepeatQuestData.Deserialize(auto_repeat);
      component.list.SetObject(AutoRepeatQuestSVB_Key.RESULT, (object) autoRepeatQuestData);
      List<RuneData> runeDataList = new List<RuneData>();
      if (_runes_detail != null)
      {
        foreach (Json_RuneData json in _runes_detail)
        {
          RuneData runeData = new RuneData();
          runeData.Deserialize(json);
          runeDataList.Add(runeData);
        }
      }
      component.list.SetObject(AutoRepeatQuestSVB_Key.RUNES, (object) runeDataList);
    }

    [MessagePackObject(true)]
    public class MP_AutoRepeatQuestEndResponse : WebAPI.JSON_BaseResponse
    {
      public ReqAutoRepeatQuestEnd.Response body;
    }
  }
}
