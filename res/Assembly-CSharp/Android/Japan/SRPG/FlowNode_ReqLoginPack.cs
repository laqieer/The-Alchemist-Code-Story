// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqLoginPack
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using Gsc.App;
using System.Collections.Generic;

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
      if (Network.Mode == Network.EConnectMode.Online)
      {
        this.ExecRequest((WebAPI) new ReqLoginPack(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), MonoSingleton<GameManager>.Instance.IsRelogin));
        this.enabled = true;
      }
      else
        this.Success();
    }

    private void Success()
    {
      this.enabled = false;
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
            instance.Player.RegistTrophyStateDictByProg(instance.MasterParam.GetTrophy(trophyProg.iname), trophyProg);
          }
        }
      }
      instance.Player.CreateInheritingExtraTrophy(progs);
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
        {
          TrophyState trophyCounter = player.GetTrophyCounter(trophiesOfType[index].Param, false);
          if (trophyCounter != null && !(trophyCounter.Count == null | trophyCounter.Count.Length < 1))
          {
            trophyCounter.Count[0] = loginCount;
            trophyCounter.IsDirty = true;
          }
        }
      }
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        this.OnRetry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<FlowNode_ReqLoginPack.JSON_ReqLoginPackResponse> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqLoginPack.JSON_ReqLoginPackResponse>>(www.text);
        if (jsonObject.body == null)
        {
          this.OnRetry();
        }
        else
        {
          GameManager instance = MonoSingleton<GameManager>.Instance;
          instance.Player.ResetQuestChallenges();
          instance.ResetJigenQuests(false);
          if (!instance.Deserialize(jsonObject.body.quests))
          {
            this.OnFailed();
          }
          else
          {
            if (instance.IsRelogin)
            {
              instance.Player.DeleteTrophies(jsonObject.body.trophyprogs);
              instance.Player.DeleteTrophies(jsonObject.body.bingoprogs);
            }
            LoginNewsInfo.SetPubInfo(jsonObject.body.pubinfo);
            this.reflectTrophyProgs(jsonObject.body.trophyprogs);
            this.reflectTrophyProgs(jsonObject.body.bingoprogs);
            this.reflectLoginTrophyProgs();
            if (jsonObject.body.channel != 0)
              GlobalVars.CurrentChatChannel.Set(jsonObject.body.channel);
            if (jsonObject.body.support != 0L)
              GlobalVars.SelectedSupportUnitUniqueID.Set(jsonObject.body.support);
            if (!string.IsNullOrEmpty(jsonObject.body.device_id))
              BootLoader.GetAccountManager().SetDeviceId((string) null, jsonObject.body.device_id);
            if (jsonObject.body.is_pending == 1)
              FlowNode_Variable.Set("REDRAW_GACHA_PENDING", "1");
            else
              FlowNode_Variable.Set("REDRAW_GACHA_PENDING", "0");
            FlowNode_Variable.Set("SHOW_CHAPTER", "0");
            Network.RemoveAPI();
            instance.Player.OnLogin();
            instance.IsRelogin = false;
            this.Success();
          }
        }
      }
    }

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
  }
}
