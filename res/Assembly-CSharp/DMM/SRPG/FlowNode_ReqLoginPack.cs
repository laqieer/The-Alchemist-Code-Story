// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqLoginPack
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using Gsc.App;
using Gsc.Network.Encoding;
using MessagePack;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/Login/ReqLoginPack", 32741)]
  [FlowNode.Pin(1, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "Success", FlowNode.PinTypes.Output, 10)]
  public class FlowNode_ReqLoginPack : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      if (SRPG.Network.Mode == SRPG.Network.EConnectMode.Online)
      {
        this.SerializeCompressMethod = EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK;
        this.ExecRequest((WebAPI) new ReqLoginPack(new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), this.SerializeCompressMethod, MonoSingleton<GameManager>.Instance.IsRelogin));
        ((Behaviour) this).enabled = true;
      }
      else
        this.Success();
    }

    private void Success()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(10);
    }

    private void reflectTrophyProgs(JSON_TrophyProgress[] trophy_progs)
    {
      if (trophy_progs == null)
        return;
      Dictionary<int, List<JSON_TrophyProgress>> progs = new Dictionary<int, List<JSON_TrophyProgress>>();
      GameManager instance = MonoSingleton<GameManager>.Instance;
      for (int index = 0; index < trophy_progs.Length; ++index)
      {
        JSON_TrophyProgress trophyProg = trophy_progs[index];
        if (trophyProg != null)
        {
          TrophyParam trophy = instance.MasterParam.GetTrophy(trophyProg.iname);
          if (trophy == null)
          {
            DebugUtility.LogWarning("存在しないミッション:" + trophyProg.iname);
          }
          else
          {
            if (trophy.Objectives[0].type.IsExtraClear())
            {
              int type = (int) trophy.Objectives[0].type;
              if (!progs.ContainsKey(type))
                progs[type] = new List<JSON_TrophyProgress>();
              progs[type].Add(trophy_progs[index]);
            }
            instance.Player.TrophyData.RegistTrophyStateDictByProg(instance.MasterParam.GetTrophy(trophyProg.iname), trophyProg);
            if (trophy.Objectives[0].type == TrophyConditionTypes.logincount_periodoftime)
            {
              TrophyState trophyCounter = instance.Player.TrophyData.GetTrophyCounter(trophy);
              if (trophyCounter != null && trophyCounter.IsCompleted)
                trophyCounter.IsDirty = true;
            }
          }
        }
      }
      instance.Player.TrophyData.CreateInheritingExtraTrophy(progs);
    }

    private void reflectLoginTrophyProgs()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      PlayerData player = instance.Player;
      int loginCount = player.LoginCount;
      TrophyObjective[] trophiesOfType = instance.MasterParam.GetTrophiesOfType(TrophyConditionTypes.logincount);
      if (trophiesOfType == null)
        return;
      for (int index = 0; index < trophiesOfType.Length; ++index)
      {
        if (trophiesOfType[index] != null)
          player.TrophyData.SetTrophyCounter(trophiesOfType[index], loginCount);
      }
    }

    public override void OnSuccess(WWWResult www)
    {
      FlowNode_ReqLoginPack.JSON_ReqLoginPackResponse body;
      if (EncodingTypes.IsJsonSerializeCompressSelected(this.SerializeCompressMethod))
      {
        if (SRPG.Network.IsError)
        {
          this.OnRetry();
          return;
        }
        WebAPI.JSON_BodyResponse<FlowNode_ReqLoginPack.JSON_ReqLoginPackResponse> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqLoginPack.JSON_ReqLoginPackResponse>>(www.text);
        if (jsonObject == null || jsonObject.body == null)
        {
          this.OnRetry();
          return;
        }
        body = jsonObject.body;
      }
      else
      {
        FlowNode_ReqLoginPack.MP_ReqLoginPackResponse loginPackResponse = SerializerCompressorHelper.Decode<FlowNode_ReqLoginPack.MP_ReqLoginPackResponse>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(this.SerializeCompressMethod));
        SRPG.Network.EErrCode stat = (SRPG.Network.EErrCode) loginPackResponse.stat;
        string statMsg = loginPackResponse.stat_msg;
        if (stat != SRPG.Network.EErrCode.Success)
          SRPG.Network.SetServerMetaDataAsError(stat, statMsg);
        if (SRPG.Network.IsError)
        {
          this.OnRetry();
          return;
        }
        body = loginPackResponse.body;
      }
      GameManager instance = MonoSingleton<GameManager>.Instance;
      instance.Player.ResetQuestChallenges();
      instance.ResetJigenQuests();
      if (!instance.Deserialize(body.quests))
      {
        this.OnFailed();
      }
      else
      {
        if (instance.IsRelogin)
        {
          instance.Player.TrophyData.DeleteTrophies(body.trophyprogs);
          instance.Player.TrophyData.DeleteTrophies(body.bingoprogs);
          instance.Player.GuildTrophyData.ClearTrophies();
        }
        LoginNewsInfo.SetPubInfo(body.pubinfo);
        this.reflectTrophyProgs(body.trophyprogs);
        this.reflectTrophyProgs(body.bingoprogs);
        this.reflectLoginTrophyProgs();
        if (body.channel != 0)
          GlobalVars.CurrentChatChannel.Set(body.channel);
        if (body.support != 0L)
          GlobalVars.SelectedSupportUnitUniqueID.Set(body.support);
        if (!string.IsNullOrEmpty(body.device_id))
          BootLoader.GetAccountManager().SetDeviceId((string) null, body.device_id);
        if (body.is_pending == 1)
          FlowNode_Variable.Set("REDRAW_GACHA_PENDING", "1");
        else
          FlowNode_Variable.Set("REDRAW_GACHA_PENDING", "0");
        FlowNode_Variable.Set("SHOW_CHAPTER", "0");
        SRPG.Network.RemoveAPI();
        instance.Player.OnLogin();
        instance.Player.UpdateUnlocks();
        SNSController.RefreshInstalled_Twitter();
        instance.IsRelogin = false;
        this.Success();
      }
    }

    [MessagePackObject(true)]
    public class JSON_ReqLoginPackResponse
    {
      public JSON_QuestProgress[] quests;
      public JSON_TrophyProgress[] trophyprogs;
      public JSON_TrophyProgress[] bingoprogs;
      public Json_ChatChannelMasterParam[] channels;
      public int channel;
      public long support;
      public string device_id;
      public int is_pending;
      public LoginNewsInfo.JSON_PubInfo pubinfo;
    }

    [MessagePackObject(true)]
    public class MP_ReqLoginPackResponse : WebAPI.JSON_BaseResponse
    {
      public FlowNode_ReqLoginPack.JSON_ReqLoginPackResponse body;
    }
  }
}
