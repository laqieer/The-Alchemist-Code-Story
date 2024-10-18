// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_StartQuest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/Quest/Start", 32741)]
  [FlowNode.Pin(0, "Load", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "LoadMultiPlay", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(500, "LoadMultiTower", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(200, "LoadVersus", FlowNode.PinTypes.Input, 4)]
  [FlowNode.Pin(700, "LoadVersusCpu", FlowNode.PinTypes.Input, 5)]
  [FlowNode.Pin(250, "LoadRankMatch", FlowNode.PinTypes.Input, 6)]
  [FlowNode.Pin(600, "LoadRaidBoss", FlowNode.PinTypes.Input, 7)]
  [FlowNode.Pin(1000, "LoadOrdeal", FlowNode.PinTypes.Input, 8)]
  [FlowNode.Pin(1200, "LoadGenesisBoss", FlowNode.PinTypes.Input, 9)]
  [FlowNode.Pin(1300, "LoadAdvanceBoss", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(1400, "LoadGuildRaid", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(1500, "LoadGvG", FlowNode.PinTypes.Input, 12)]
  [FlowNode.Pin(1600, "LoadWorldRaid", FlowNode.PinTypes.Input, 13)]
  [FlowNode.Pin(10, "Resume", FlowNode.PinTypes.Input, 51)]
  [FlowNode.Pin(20, "AudienceConnect", FlowNode.PinTypes.Input, 101)]
  [FlowNode.Pin(30, "AudienceStart", FlowNode.PinTypes.Input, 102)]
  [FlowNode.Pin(1, "Started", FlowNode.PinTypes.Output, 201)]
  [FlowNode.Pin(2, "Failed", FlowNode.PinTypes.Output, 202)]
  [FlowNode.Pin(3, "NoMatchVersion", FlowNode.PinTypes.Output, 203)]
  [FlowNode.Pin(4, "MultiMaintenance", FlowNode.PinTypes.Output, 204)]
  [FlowNode.Pin(5, "NetworkSuccess", FlowNode.PinTypes.Output, 205)]
  [FlowNode.Pin(6, "ColoRankModify", FlowNode.PinTypes.Output, 206)]
  [FlowNode.Pin(7, "MatchSuccess", FlowNode.PinTypes.Output, 207)]
  [FlowNode.Pin(8, "NoRoom", FlowNode.PinTypes.Output, 208)]
  [FlowNode.Pin(9, "NoAudienceData", FlowNode.PinTypes.Output, 209)]
  [FlowNode.Pin(300, "AudienceFailed", FlowNode.PinTypes.Output, 210)]
  [FlowNode.Pin(301, "AudienceFailedMax", FlowNode.PinTypes.Output, 211)]
  [FlowNode.Pin(400, "NotGpsQuest", FlowNode.PinTypes.Output, 212)]
  [FlowNode.Pin(600, "NotRoomMT", FlowNode.PinTypes.Output, 213)]
  [FlowNode.Pin(800, "QuestResumeFailed_IsRehash", FlowNode.PinTypes.Output, 214)]
  [FlowNode.Pin(900, "RaidTimeOut", FlowNode.PinTypes.Output, 215)]
  [FlowNode.Pin(901, "RaidOverRescue", FlowNode.PinTypes.Output, 216)]
  [FlowNode.Pin(902, "RaidBeatRescue", FlowNode.PinTypes.Output, 217)]
  [FlowNode.Pin(903, "RaidOutOfPeriod", FlowNode.PinTypes.Output, 218)]
  [FlowNode.Pin(904, "RaidTimeOverRescue", FlowNode.PinTypes.Output, 219)]
  [FlowNode.Pin(905, "RaidAlreadyRescueCancel", FlowNode.PinTypes.Output, 220)]
  [FlowNode.Pin(1250, "GenesisOutOfPeriod", FlowNode.PinTypes.Output, 221)]
  [FlowNode.Pin(906, "RaidScheduleTimeOut", FlowNode.PinTypes.Output, 222)]
  [FlowNode.Pin(1260, "AdvanceNotOpen", FlowNode.PinTypes.Output, 223)]
  [FlowNode.Pin(1410, "GuildRaidNeedRefresh", FlowNode.PinTypes.Output, 224)]
  [FlowNode.Pin(1610, "WorldRaidNeedRefresh", FlowNode.PinTypes.Output, 225)]
  [FlowNode.Pin(1620, "WorldRaidOutOfPeriod", FlowNode.PinTypes.Output, 226)]
  [FlowNode.Pin(5000, "Download Cancel", FlowNode.PinTypes.Output, 5000)]
  [FlowNode.Pin(5001, "Download Timeout", FlowNode.PinTypes.Output, 5001)]
  public class FlowNode_StartQuest : FlowNode_Network
  {
    protected const int PIN_IN_LOAD = 0;
    protected const int PIN_IN_LOAD_MULTI_PLAY = 100;
    protected const int PIN_IN_LOAD_VERSUS = 200;
    protected const int PIN_IN_LOAD_VERSUS_CPU = 700;
    protected const int PIN_IN_LOAD_RANK_MATCH = 250;
    protected const int PIN_IN_LOAD_MULTI_TOWER = 500;
    protected const int PIN_IN_LOAD_RAID_BOSS = 600;
    protected const int PIN_IN_LOAD_ORDEAL = 1000;
    protected const int PIN_IN_LOAD_GENESIS_BOSS = 1200;
    protected const int PIN_IN_LOAD_ADVANCE_BOSS = 1300;
    protected const int PIN_IN_LOAD_GUILD_RAID = 1400;
    protected const int PIN_IN_LOAD_GVG = 1500;
    protected const int PIN_IN_LOAD_WORLD_RAID = 1600;
    protected const int PIN_IN_RESUME = 10;
    protected const int PIN_IN_AUDIENCE_CONNECT = 20;
    protected const int PIN_IN_AUDIENCE_START = 30;
    protected const int PIN_OUT_STARTED = 1;
    protected const int PIN_OUT_FAILED = 2;
    protected const int PIN_OUT_NO_MATCH_VERSION = 3;
    protected const int PIN_OUT_MULTI_MAINTENANCE = 4;
    protected const int PIN_OUT_NETWORK_SUCCESS = 5;
    protected const int PIN_OUT_COLOSEUM_RANK_MODIFY = 6;
    protected const int PIN_OUT_MATCH_SUCCESS = 7;
    protected const int PIN_OUT_NO_ROOM = 8;
    protected const int PIN_OUT_NO_AUDIENCE_DATA = 9;
    protected const int PIN_OUT_AUDIENCE_FAILED = 300;
    protected const int PIN_OUT_AUDIENCE_FAILED_MAX = 301;
    protected const int PIN_OUT_NOT_GPS_QUEST = 400;
    protected const int PIN_OUT_NOT_ROOM_MULTI_TOWER = 600;
    protected const int PIN_OUT_QUEST_RESUME_FAILED_IS_REHASH = 800;
    protected const int PIN_OUT_RAID_TIMEOUT = 900;
    protected const int PIN_OUT_RAID_OVER_RESCUE = 901;
    protected const int PIN_OUT_RAID_BEAT_RESCUE = 902;
    protected const int PIN_OUT_RAID_OUT_OF_PERIOD = 903;
    protected const int PIN_OUT_RAID_CAN_NOT_RESCUE_TIME_OVER = 904;
    protected const int PIN_OUT_RAID_ALREADY_RESCUE_CANCEL = 905;
    protected const int PIN_OUT_RAID_SCHEDULE_TIMEOUT = 906;
    protected const int PIN_OUT_GENESIS_OUT_OF_PERIOD = 1250;
    protected const int PIN_OUT_ADVANCE_NOT_OPEN = 1260;
    protected const int PIN_OUT_GUILDRAID_NEED_REFRESH = 1410;
    protected const int PIN_OUT_WORLDRAID_NEED_REFRESH = 1610;
    protected const int PIN_OUT_WORLDRAID_OUT_OF_PERIOD = 1620;
    protected const int PIN_OUT_DOWNLOAD_CANCEL = 5000;
    protected const int PIN_OUT_DOWNLOAD_TIMEOUT = 5001;
    [HideInInspector]
    public string QuestID;
    public bool ReplaceScene;
    [HideInInspector]
    public bool PlayOffline;
    protected bool mResume;
    [HideInInspector]
    public RestorePoints RestorePoint;
    public bool SetRestorePoint;
    private BattleCore.Json_Battle mQuestData;
    protected QuestParam mStartingQuest;
    public int mReqID = -1;
    private float mConnectTime;

    public override void OnActivate(int pinID)
    {
      GameManager instance1 = MonoSingleton<GameManager>.Instance;
      MyPhoton instance2 = PunMonoSingleton<MyPhoton>.Instance;
      instance1.AudienceMode = false;
      this.mReqID = pinID;
      instance2.IsMultiPlay = false;
      instance2.IsMultiVersus = false;
      instance1.IsVSCpuBattle = false;
      instance2.IsRankMatch = false;
      this.mResume = false;
      switch (pinID)
      {
        case 10:
          this.mResume = true;
          pinID = 0;
          break;
        case 100:
        case 500:
          instance2.IsMultiPlay = true;
          pinID = 0;
          break;
        case 200:
          instance2.IsMultiPlay = true;
          instance2.IsMultiVersus = true;
          pinID = 0;
          break;
        case 250:
          instance2.IsMultiPlay = true;
          instance2.IsMultiVersus = true;
          instance2.IsRankMatch = true;
          pinID = 0;
          break;
        case 600:
          pinID = 0;
          break;
        case 700:
          instance1.IsVSCpuBattle = true;
          pinID = 0;
          break;
        default:
          if (pinID == 1000 || pinID == 1200 || pinID == 1300 || pinID == 1400 || pinID == 1500 || pinID == 1600)
            goto case 600;
          else
            break;
      }
      switch (pinID)
      {
        case 0:
          if (((Behaviour) this).enabled)
            break;
          ((Behaviour) this).enabled = true;
          CriticalSection.Enter(CriticalSections.SceneChange);
          if (this.mResume)
          {
            long btlId = (long) GlobalVars.BtlID;
            GlobalVars.BtlID.Set(0L);
            GlobalVars.BtlIDStatus.Set(true);
            if (GlobalVars.QuestType == QuestTypes.Raid)
            {
              this.ExecRequest((WebAPI) new ReqRaidBtlResume(btlId, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
              break;
            }
            if (GlobalVars.QuestType == QuestTypes.GuildRaid && MonoSingleton<GameManager>.Instance.Player.PlayerGuild != null)
            {
              this.ExecRequest((WebAPI) new ReqGuildRaidBtlResume(MonoSingleton<GameManager>.Instance.Player.PlayerGuild.Gid, btlId, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
              break;
            }
            switch (GlobalVars.QuestType)
            {
              case QuestTypes.GenesisBoss:
                this.ExecRequest((WebAPI) new ReqGenesisBossBtlResume(btlId, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
                return;
              case QuestTypes.AdvanceBoss:
                this.ExecRequest((WebAPI) new ReqAdvanceBossBtlResume(btlId, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
                return;
              case QuestTypes.GvG:
                return;
              case QuestTypes.WorldRaid:
                this.ExecRequest((WebAPI) new ReqWorldRaidBtlResume(btlId, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
                return;
              default:
                this.ExecRequest((WebAPI) new ReqBtlComResume(btlId, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
                return;
            }
          }
          else
          {
            this.mStartingQuest = instance1.FindQuest(GlobalVars.SelectedQuestID);
            if (!string.IsNullOrEmpty(this.QuestID))
            {
              GlobalVars.SelectedQuestID = this.QuestID;
              GlobalVars.SelectedFriendID = string.Empty;
            }
            if (!this.PlayOffline && Network.Mode == Network.EConnectMode.Online)
            {
              bool flag = false | this.mStartingQuest.IsMulti | MonoSingleton<GameManager>.Instance.AudienceMode | MonoSingleton<GameManager>.Instance.IsVSCpuBattle;
              if (instance2.IsMultiVersus && AssetDownloader.IsEnableShowSizeBeforeDownloading())
              {
                this.LoadQuestStart();
                break;
              }
              if (flag && AssetDownloader.IsEnableShowSizeBeforeDownloading())
              {
                QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
                AssetDownloader.StartConfirmDownloadQuestContentYesNo(MonoSingleton<GameManager>.Instance.GetBattleEntryUnits(quest), (List<ItemData>) null, quest, (UIUtility.DialogResultEvent) (ok => this.LoadQuestStart()), (UIUtility.DialogResultEvent) (no =>
                {
                  AssetDownloader.ClearDownloadRequests(true);
                  this.DownloadNotApproved();
                }));
                break;
              }
              this.LoadQuestStart();
              break;
            }
            this.StartCoroutine(this.StartScene((BattleCore.Json_Battle) null));
            break;
          }
        case 20:
          if (instance1.AudienceRoom == null)
            break;
          instance1.AudienceMode = true;
          this.StartCoroutine(this.StartAudience());
          break;
        case 30:
          if (Network.IsError)
          {
            this.ActivateOutputLinks(300);
            Network.ResetError();
            break;
          }
          if (!Network.IsStreamConnecting)
          {
            Network.ResetError();
            this.ActivateOutputLinks(300);
            break;
          }
          VersusAudienceManager audienceManager = instance1.AudienceManager;
          audienceManager.AddStartQuest();
          if (audienceManager.GetStartedParam() != null)
          {
            if (audienceManager.GetStartedParam().btlinfo != null)
            {
              BattleCore.Json_Battle json = new BattleCore.Json_Battle();
              json.btlinfo = audienceManager.GetStartedParam().btlinfo;
              CriticalSection.Enter(CriticalSections.SceneChange);
              instance1.AudienceMode = true;
              this.StartCoroutine(this.StartScene(json));
              break;
            }
            if (audienceManager.IsRetryError)
            {
              DebugUtility.LogError("Not Exist btlInfo");
              Network.Abort();
              this.ActivateOutputLinks(300);
              break;
            }
            this.ActivateOutputLinks(9);
            break;
          }
          if (audienceManager.IsRetryError)
          {
            DebugUtility.LogError("Not Exist StartParam");
            Network.Abort();
            this.ActivateOutputLinks(300);
            break;
          }
          this.ActivateOutputLinks(9);
          break;
      }
    }

    private void LoadQuestStart()
    {
      GameManager instance1 = MonoSingleton<GameManager>.Instance;
      MyPhoton pt = PunMonoSingleton<MyPhoton>.Instance;
      instance1.AudienceMode = false;
      PlayerPartyTypes partyIndex1 = QuestParam.QuestToPartyIndex(this.mStartingQuest.type);
      GlobalVars.GvGBattleMode.Set(false);
      if (this.mStartingQuest.IsPreCalcResult)
      {
        if (this.mStartingQuest.IsGvG)
        {
          if (MonoSingleton<GameManager>.Instance.Player.PlayerGuild == null)
          {
            this.ActivateOutputLinks(2);
            return;
          }
          GlobalVars.GvGBattleMode.Set(true);
        }
        this.ActivateOutputLinks(5);
        this.StartCoroutine(this.StartScene((BattleCore.Json_Battle) null));
      }
      else if (this.mStartingQuest.type == QuestTypes.Ordeal)
      {
        this.ExecRequest((WebAPI) new ReqBtlOrdealReq(this.mStartingQuest.iname, GlobalVars.OrdealSupports, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      }
      else
      {
        PartyData partyOfType = instance1.Player.FindPartyOfType(partyIndex1);
        int partyIndex2 = instance1.Player.Partys.IndexOf(partyOfType);
        bool multi = false;
        bool isHost = false;
        int seat = -1;
        int plid = -1;
        string uid = string.Empty;
        List<string> stringList = new List<string>();
        VersusStatusData versusStatusData = (VersusStatusData) null;
        int num1 = 0;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) pt, (UnityEngine.Object) null))
        {
          multi = pt.IsMultiPlay;
          isHost = pt.IsOldestPlayer();
          seat = pt.MyPlayerIndex;
          MyPhoton.MyPlayer myPlayer = pt.GetMyPlayer();
          if (myPlayer != null)
            plid = myPlayer.playerID;
          if (pt.IsMultiVersus)
          {
            List<JSON_MyPhotonPlayerParam> myPlayersStarted = pt.GetMyPlayersStarted();
            MyPhoton.MyRoom currentRoom = pt.GetCurrentRoom();
            int num2 = currentRoom == null ? 1 : currentRoom.playerCount;
            JSON_MyPhotonPlayerParam photonPlayerParam = myPlayersStarted.Find((Predicate<JSON_MyPhotonPlayerParam>) (p => p.playerIndex != pt.MyPlayerIndex));
            if (photonPlayerParam != null)
              uid = photonPlayerParam.UID;
            if (!GlobalVars.IsVersusDraftMode)
            {
              if (string.IsNullOrEmpty(uid) || num2 == 1)
              {
                this.OnVersusNoPlayer();
                return;
              }
              PlayerPartyTypes index1 = PlayerPartyTypes.Versus;
              if (pt.IsRankMatch)
                index1 = PlayerPartyTypes.RankMatch;
              PartyData party = instance1.Player.Partys[(int) index1];
              if (party != null)
              {
                versusStatusData = new VersusStatusData();
                for (int index2 = 0; index2 < party.MAX_UNIT; ++index2)
                {
                  long unitUniqueId = party.GetUnitUniqueID(index2);
                  if (party.GetUnitUniqueID(index2) != 0L)
                  {
                    UnitData unitDataByUniqueId = instance1.Player.FindUnitDataByUniqueID(unitUniqueId);
                    if (unitDataByUniqueId != null)
                    {
                      versusStatusData.Add(unitDataByUniqueId.Status.param, unitDataByUniqueId.GetCombination());
                      ++num1;
                    }
                  }
                }
              }
            }
            else
            {
              versusStatusData = new VersusStatusData();
              for (int index = 0; index < VersusDraftList.VersusDraftPartyUnits.Count; ++index)
              {
                UnitData versusDraftPartyUnit = VersusDraftList.VersusDraftPartyUnits[index];
                if (versusDraftPartyUnit != null)
                {
                  versusStatusData.Add(versusDraftPartyUnit.Status.param, versusDraftPartyUnit.GetCombination());
                  ++num1;
                }
              }
            }
          }
          else
          {
            List<JSON_MyPhotonPlayerParam> myPlayersStarted = pt.GetMyPlayersStarted();
            for (int index = 0; index < myPlayersStarted.Count; ++index)
            {
              if (myPlayersStarted[index].playerIndex != pt.MyPlayerIndex)
                stringList.Add(myPlayersStarted[index].UID);
            }
          }
        }
        if (this.mReqID == 200)
        {
          if (GlobalVars.IsVersusDraftMode)
          {
            int enemy_draft_id = 0;
            MyPhoton.MyPlayer player = PunMonoSingleton<MyPhoton>.Instance.GetMyPlayer();
            JSON_MyPhotonPlayerParam photonPlayerParam = pt.GetMyPlayersStarted().Find((Predicate<JSON_MyPhotonPlayerParam>) (p => p.playerID != player.playerID));
            if (photonPlayerParam != null)
              enemy_draft_id = photonPlayerParam.draft_id;
            this.ExecRequest((WebAPI) new ReqVersus(this.mStartingQuest.iname, plid, seat, uid, versusStatusData, num1, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), GlobalVars.SelectedMultiPlayVersusType, VersusDraftList.DraftID, enemy_draft_id));
          }
          else
            this.ExecRequest((WebAPI) new ReqVersus(this.mStartingQuest.iname, plid, seat, uid, versusStatusData, num1, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), GlobalVars.SelectedMultiPlayVersusType));
        }
        else if (this.mReqID == 250)
          this.ExecRequest((WebAPI) new ReqRankMatch(this.mStartingQuest.iname, plid, seat, uid, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        else if (this.mReqID == 500)
          this.ExecRequest((WebAPI) new ReqBtlMultiTwReq(this.mStartingQuest.iname, partyIndex2, plid, seat, stringList.ToArray(), new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        else if (this.mReqID == 700)
          this.ExecRequest((WebAPI) new ReqVersusCpu(this.mStartingQuest.iname, GlobalVars.VersusCpu == null ? 1 : GlobalVars.VersusCpu.Get().Deck, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        else if (this.mReqID == 600)
        {
          RaidBossData raidBossData = RaidManager.Instance.SelectedRaidOwnerType != RaidManager.RaidOwnerType.Self ? RaidManager.Instance.RescueRaidBossData : RaidManager.Instance.CurrentRaidBossData;
          if (raidBossData == null)
            this.ActivateOutputLinks(2);
          else
            this.ExecRequest((WebAPI) new ReqRaidBtlReq(raidBossData.AreaId, raidBossData.RaidBossInfo.BossId, raidBossData.RaidBossInfo.Round, raidBossData.OwnerUID, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        }
        else if (this.mReqID == 1200)
          this.ExecRequest((WebAPI) new ReqGenesisBossBtlReq(this.mStartingQuest.ChapterID, this.mStartingQuest.iname, this.mStartingQuest.difficulty, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        else if (this.mReqID == 1300)
          this.ExecRequest((WebAPI) new ReqAdvanceBossBtlReq(this.mStartingQuest.ChapterID, this.mStartingQuest.iname, this.mStartingQuest.difficulty, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        else if (this.mReqID == 1400)
        {
          if (MonoSingleton<GameManager>.Instance.Player.PlayerGuild == null)
          {
            this.ActivateOutputLinks(2);
          }
          else
          {
            GuildRaidManager instance2 = GuildRaidManager.Instance;
            if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance2, (UnityEngine.Object) null))
            {
              this.ActivateOutputLinks(2);
            }
            else
            {
              GuildRaidBossInfo currentBossInfo = instance2.CurrentBossInfo;
              if (currentBossInfo == null)
              {
                this.ActivateOutputLinks(2);
              }
              else
              {
                int unitsTotalStrength = instance1.Player.GetAllUnitsTotalStrength();
                int round = instance2.CurrentRound;
                if (instance2.BattleType == GuildRaidBattleType.Mock)
                  round = instance2.TrialRound;
                this.ExecRequest((WebAPI) new ReqGuildRaidBtlReq(MonoSingleton<GameManager>.Instance.Player.PlayerGuild.Gid, round, currentBossInfo.BossId, instance2.BattleType, unitsTotalStrength, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
              }
            }
          }
        }
        else if (this.mReqID == 1600)
        {
          this.ExecRequest((WebAPI) new ReqWorldRaidBtlReq(WorldRaidBossManager.GetBossData().BossIname, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        }
        else
        {
          if (PartyWindow2.IsAutoRepeatQuestCheck)
          {
            GlobalVars.SelectedFriendID = (string) null;
            GlobalVars.SelectedSupport.Set((SupportData) null);
          }
          this.ExecRequest((WebAPI) new ReqBtlComReq(this.mStartingQuest.iname, GlobalVars.SelectedFriendID, GlobalVars.SelectedSupport.Get(), new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), multi, partyIndex2, isHost, plid, seat, GlobalVars.Location, GlobalVars.SelectedRankingQuestParam, !PartyWindow2.IsAutoRepeatQuestCheck ? 0 : 1));
        }
      }
    }

    protected override void OnDestroy() => base.OnDestroy();

    private void OnSceneLoad(GameObject sceneRoot)
    {
      SceneAwakeObserver.RemoveListener(new SceneAwakeObserver.SceneEvent(this.OnSceneLoad));
      CriticalSection.Leave(CriticalSections.SceneChange);
    }

    public override void OnBack()
    {
      CriticalSection.Leave(CriticalSections.SceneChange);
      base.OnBack();
    }

    public void OnMismatchVersion()
    {
      ((Behaviour) this).enabled = false;
      CriticalSection.Leave(CriticalSections.SceneChange);
      Network.RemoveAPI();
      Network.ResetError();
      this.ActivateOutputLinks(3);
    }

    public void OnMultiMaintenance()
    {
      ((Behaviour) this).enabled = false;
      CriticalSection.Leave(CriticalSections.SceneChange);
      Network.RemoveAPI();
      this.ActivateOutputLinks(4);
    }

    public void OnVersusNoPlayer()
    {
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance, (UnityEngine.Object) null) && instance.IsOldestPlayer())
        instance.OpenRoom();
      ((Behaviour) this).enabled = false;
      CriticalSection.Leave(CriticalSections.SceneChange);
      Network.RemoveAPI();
      this.ActivateOutputLinks(2);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        Network.EErrCode errCode = Network.ErrCode;
        switch (errCode)
        {
          case Network.EErrCode.GuildRaid_OutOfPeriod:
          case Network.EErrCode.GuildRaid_EnableTimeOutOfPeriod:
          case Network.EErrCode.GuildRaid_ChallengeLimit:
          case Network.EErrCode.GuildRaid_AlreadyBeat:
          case Network.EErrCode.GuildRaid_CanNotChallengeByThereIsNoBoss:
          case Network.EErrCode.GuildRaid_NotChallenge:
          case Network.EErrCode.GuildRaid_NotMainBattle:
          case Network.EErrCode.GuildRaid_NewAreaCanBeReleased:
label_60:
            CriticalSection.Leave(CriticalSections.SceneChange);
            Network.RemoveAPI();
            Network.ResetError();
            BattleCore.RemoveSuspendData();
            UIUtility.SystemMessage(Network.ErrMsg, (UIUtility.DialogResultEvent) (go => this.ActivateOutputLinks(1410)), systemModal: true);
            ((Behaviour) this).enabled = false;
            break;
          default:
            switch (errCode - 3800)
            {
              case Network.EErrCode.Success:
                this.OnBack();
                return;
              case Network.EErrCode.Unknown:
                this.OnBack();
                return;
              case Network.EErrCode.Version:
                this.OnFailed();
                return;
              case Network.EErrCode.AssetVersion:
                this.OnBack();
                return;
              case Network.EErrCode.NoVersionDbg:
                this.OnFailed();
                return;
              case Network.EErrCode.Unknown | Network.EErrCode.NoVersionDbg:
                this.OnBack();
                return;
              default:
                switch (errCode - 10011)
                {
                  case Network.EErrCode.Success:
                    this.OnFailed();
                    return;
                  case Network.EErrCode.Version:
                    this.OnFailed();
                    return;
                  case Network.EErrCode.NoVersionDbg:
                    this.OnFailed();
                    return;
                  case Network.EErrCode.Unknown | Network.EErrCode.NoVersionDbg:
                    this.OnFailed();
                    return;
                  case Network.EErrCode.Version | Network.EErrCode.NoVersionDbg:
                    this.OnFailed();
                    return;
                  default:
                    switch (errCode - 202)
                    {
                      case Network.EErrCode.Success:
                      case Network.EErrCode.Unknown:
                      case Network.EErrCode.AssetVersion:
                      case Network.EErrCode.NoVersionDbg:
                        this.OnMultiMaintenance();
                        return;
                      default:
                        switch (errCode - 10000)
                        {
                          case Network.EErrCode.Success:
                            this.OnFailed();
                            return;
                          case Network.EErrCode.Unknown:
                            this.OnFailed();
                            return;
                          case Network.EErrCode.Version:
                            this.OnFailed();
                            return;
                          case Network.EErrCode.Version | Network.EErrCode.NoVersionDbg:
                            this.OnFailed();
                            return;
                          default:
                            switch (errCode - 3300)
                            {
                              case Network.EErrCode.Success:
                                this.OnBack();
                                return;
                              case Network.EErrCode.Unknown:
                                this.OnBack();
                                return;
                              case Network.EErrCode.AssetVersion:
                                this.OnBack();
                                return;
                              default:
                                switch (errCode - 30000)
                                {
                                  case Network.EErrCode.Success:
                                    CriticalSection.Leave(CriticalSections.SceneChange);
                                    Network.RemoveAPI();
                                    Network.ResetError();
                                    BattleCore.RemoveSuspendData();
                                    UIUtility.SystemMessage(Network.ErrMsg, (UIUtility.DialogResultEvent) (go => this.ActivateOutputLinks(1620)), systemModal: true);
                                    ((Behaviour) this).enabled = false;
                                    return;
                                  case Network.EErrCode.Unknown:
                                  case Network.EErrCode.NoVersionDbg:
                                    CriticalSection.Leave(CriticalSections.SceneChange);
                                    Network.RemoveAPI();
                                    Network.ResetError();
                                    BattleCore.RemoveSuspendData();
                                    UIUtility.SystemMessage(Network.ErrMsg, (UIUtility.DialogResultEvent) (go => this.ActivateOutputLinks(1610)), systemModal: true);
                                    ((Behaviour) this).enabled = false;
                                    return;
                                  default:
                                    switch (errCode - 12001)
                                    {
                                      case Network.EErrCode.Success:
                                        this.OnFailed();
                                        return;
                                      case Network.EErrCode.Version:
                                        CriticalSection.Leave(CriticalSections.SceneChange);
                                        Network.RemoveAPI();
                                        ((Behaviour) this).enabled = false;
                                        this.ActivateOutputLinks(600);
                                        return;
                                      default:
                                        if (errCode != Network.EErrCode.Raid_OutOfPeriod)
                                        {
                                          if (errCode != Network.EErrCode.Raid_OutOfOenTime)
                                          {
                                            if (errCode != Network.EErrCode.Raid_OverRescue)
                                            {
                                              if (errCode != Network.EErrCode.Raid_AlredyRescueCancel)
                                              {
                                                if (errCode != Network.EErrCode.NotLocation)
                                                {
                                                  if (errCode != Network.EErrCode.NotGpsQuest)
                                                  {
                                                    if (errCode != Network.EErrCode.QuestEnd)
                                                    {
                                                      if (errCode != Network.EErrCode.Advance_KeyClose)
                                                      {
                                                        if (errCode != Network.EErrCode.NoBtlInfo)
                                                        {
                                                          if (errCode != Network.EErrCode.MultiVersionMismatch)
                                                          {
                                                            if (errCode != Network.EErrCode.QuestArchive_ArchiveNotOpened)
                                                            {
                                                              if (errCode != Network.EErrCode.Genesis_OutOfPeriod)
                                                              {
                                                                if (errCode != Network.EErrCode.Guild_NotJoined)
                                                                {
                                                                  if (errCode != Network.EErrCode.Advance_NotOpen)
                                                                  {
                                                                    if (errCode != Network.EErrCode.Raid_AlreadyBeat)
                                                                    {
                                                                      if (errCode == Network.EErrCode.Raid_CanNotRescueTimeOver)
                                                                      {
                                                                        CriticalSection.Leave(CriticalSections.SceneChange);
                                                                        Network.RemoveAPI();
                                                                        Network.ResetError();
                                                                        BattleCore.RemoveSuspendData();
                                                                        this.ActivateOutputLinks(904);
                                                                        ((Behaviour) this).enabled = false;
                                                                        return;
                                                                      }
                                                                      this.OnRetry();
                                                                      return;
                                                                    }
                                                                    CriticalSection.Leave(CriticalSections.SceneChange);
                                                                    Network.RemoveAPI();
                                                                    Network.ResetError();
                                                                    BattleCore.RemoveSuspendData();
                                                                    this.ActivateOutputLinks(902);
                                                                    ((Behaviour) this).enabled = false;
                                                                    return;
                                                                  }
                                                                }
                                                                else
                                                                  goto label_60;
                                                              }
                                                              else
                                                              {
                                                                CriticalSection.Leave(CriticalSections.SceneChange);
                                                                Network.RemoveAPI();
                                                                Network.ResetError();
                                                                BattleCore.RemoveSuspendData();
                                                                this.ActivateOutputLinks(1250);
                                                                ((Behaviour) this).enabled = false;
                                                                return;
                                                              }
                                                            }
                                                            else
                                                            {
                                                              this.OnBack();
                                                              return;
                                                            }
                                                          }
                                                          else
                                                          {
                                                            this.OnMismatchVersion();
                                                            return;
                                                          }
                                                        }
                                                        else
                                                        {
                                                          this.OnFailed();
                                                          return;
                                                        }
                                                      }
                                                      CriticalSection.Leave(CriticalSections.SceneChange);
                                                      Network.RemoveAPI();
                                                      Network.ResetError();
                                                      BattleCore.RemoveSuspendData();
                                                      this.ActivateOutputLinks(1260);
                                                      ((Behaviour) this).enabled = false;
                                                      return;
                                                    }
                                                    this.OnFailed();
                                                    return;
                                                  }
                                                  CriticalSection.Leave(CriticalSections.SceneChange);
                                                  Network.RemoveAPI();
                                                  Network.ResetError();
                                                  this.ActivateOutputLinks(400);
                                                  ((Behaviour) this).enabled = false;
                                                  return;
                                                }
                                                this.OnBack();
                                                return;
                                              }
                                              CriticalSection.Leave(CriticalSections.SceneChange);
                                              Network.RemoveAPI();
                                              Network.ResetError();
                                              BattleCore.RemoveSuspendData();
                                              this.ActivateOutputLinks(905);
                                              ((Behaviour) this).enabled = false;
                                              return;
                                            }
                                            CriticalSection.Leave(CriticalSections.SceneChange);
                                            Network.RemoveAPI();
                                            Network.ResetError();
                                            BattleCore.RemoveSuspendData();
                                            this.ActivateOutputLinks(901);
                                            ((Behaviour) this).enabled = false;
                                            return;
                                          }
                                          CriticalSection.Leave(CriticalSections.SceneChange);
                                          Network.RemoveAPI();
                                          Network.ResetError();
                                          BattleCore.RemoveSuspendData();
                                          this.ActivateOutputLinks(906);
                                          ((Behaviour) this).enabled = false;
                                          return;
                                        }
                                        CriticalSection.Leave(CriticalSections.SceneChange);
                                        Network.RemoveAPI();
                                        Network.ResetError();
                                        BattleCore.RemoveSuspendData();
                                        this.ActivateOutputLinks(903);
                                        ((Behaviour) this).enabled = false;
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
      }
      else if (this.mReqID == 30)
      {
        Network.RemoveAPI();
        this.ActivateOutputLinks(5);
      }
      else
      {
        string text = www.text;
        DebugMenu.Log("API", "StartQuest:" + www.text);
        WebAPI.JSON_BodyResponse<BattleCore.Json_Battle> res = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<BattleCore.Json_Battle>>(www.text);
        if (res.body == null)
        {
          this.OnRetry();
        }
        else
        {
          if (res.body.btlinfo != null)
          {
            QuestParam questParam = res.body.btlinfo.GetQuestParam();
            if (questParam != null && questParam.IsGenAdvBoss && res.body.btlinfo.round <= 0)
            {
              bool flag = false;
              if (questParam.IsGenesisBoss)
                flag = GenesisBossInfo.IsLapBossQuest(questParam);
              else if (questParam.IsAdvanceBoss)
                flag = AdvanceBossInfo.IsLapBossQuest(questParam);
              if (flag)
              {
                DebugUtility.LogError(string.Format("LapBoss: round info error! q={0}", (object) questParam.iname));
                this.OnFailed();
                return;
              }
            }
          }
          Network.RemoveAPI();
          if (res.body.is_rehash != 0)
          {
            GlobalVars.BtlID.Set(res.body.btlid);
            UIUtility.SystemMessage(string.Empty, LocalizedText.Get("sys.FAILED_RESUMEQUEST"), (UIUtility.DialogResultEvent) (go =>
            {
              CriticalSection.Leave(CriticalSections.SceneChange);
              this.ActivateOutputLinks(800);
            }));
          }
          else if (res.body.is_timeover != 0)
          {
            CriticalSection.Leave(CriticalSections.SceneChange);
            BattleCore.RemoveSuspendData();
            this.ActivateOutputLinks(900);
          }
          else
          {
            List<UnitData> unitDataList = (List<UnitData>) null;
            if (res.body.forced_deck != null && res.body.forced_deck.units != null && res.body.forced_deck.units.Length > 0)
            {
              unitDataList = new List<UnitData>();
              for (int index = 0; index < res.body.forced_deck.units.Length; ++index)
              {
                if (res.body.forced_deck.units[index] != null && res.body.forced_deck.units[index].iname != null)
                {
                  UnitData unitData = new UnitData();
                  unitData.Deserialize(res.body.forced_deck.units[index]);
                  unitDataList.Add(unitData);
                }
              }
            }
            if (res.body.guild_trophies != null)
              MonoSingleton<GameManager>.Instance.Player.GuildTrophyData.OverwriteGuildTrophyProgress(res.body.guild_trophies);
            if (this.mResume && AssetManager.UseDLC && AssetDownloader.IsEnableShowSizeBeforeDownloading())
            {
              QuestParam questParam = res.body.btlinfo.GetQuestParam();
              List<UnitData> entryUnits = res.body.btlinfo.GetPlayerSideUnits();
              if (unitDataList != null && unitDataList.Count > 0)
                entryUnits = unitDataList;
              AssetDownloader.StartConfirmDownloadQuestContentYesNo(entryUnits, (List<ItemData>) null, questParam, (UIUtility.DialogResultEvent) (ok => this.DownloadApproved(text, res)), (UIUtility.DialogResultEvent) (no =>
              {
                AssetDownloader.ClearDownloadRequests(true);
                this.DownloadNotApproved();
              }));
            }
            else if (PunMonoSingleton<MyPhoton>.Instance.IsMultiVersus && !FlowNode_MultiPlayRoomIsDraft.IsDraftMultiPlayRoom() && AssetManager.UseDLC && AssetDownloader.IsEnableShowSizeBeforeDownloading())
            {
              QuestParam quest_param = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
              AssetDownloader.StartConfirmDownloadQuestContent(res.body.btlinfo.GetPlayerSideUnits(), (List<ItemData>) null, quest_param, (UIUtility.DialogResultEvent) (ok => this.DownloadApproved(text, res)), (AssetDownloader.OnDownloadPopupTimeout) (() =>
              {
                if (quest_param.type == QuestTypes.RankMatch)
                  Network.RequestAPI((WebAPI) new ReqRankMatchEnd(res.body.btlid, BtlResultTypes.Cancel, (string) null, (string) null, 0U, (int[]) null, (int[]) null, 0, 0, 0, 0, (int[]) null, new Network.ResponseCallback(this.OnForceBattleCancel)));
                else
                  Network.RequestAPI((WebAPI) new ReqVersusEnd(res.body.btlid, BtlResultTypes.Cancel, (string) null, (string) null, 0U, (int[]) null, (int[]) null, 0, 0, 0, 0, (int[]) null, new Network.ResponseCallback(this.OnForceBattleCancel), GlobalVars.SelectedMultiPlayVersusType));
                AssetDownloader.ClearDownloadRequests(true);
              }), GameSettings.Instance.ForApple_DownloadPopupTimeoutSec);
            }
            else
              this.DownloadApproved(text, res);
          }
        }
      }
    }

    private void DownloadApproved(
      string text,
      WebAPI.JSON_BodyResponse<BattleCore.Json_Battle> res)
    {
      this.ActivateOutputLinks(5);
      this.SetVersusAudienceParam(text);
      if (res.body.btlinfo.IsRaidQuest && res.body.rescue_member_relation >= 1)
      {
        RaidManager.SelectedLastRaidOwnerType = RaidManager.RaidOwnerType.Rescue;
        RaidManager.SelectedLastRaidRescueMemberType = (RaidRescueMemberType) res.body.rescue_member_relation;
      }
      this.StartCoroutine(this.StartScene(res.body));
    }

    private void DownloadNotApproved()
    {
      ((Behaviour) this).enabled = false;
      CriticalSection.Leave(CriticalSections.SceneChange);
      this.ActivateOutputLinks(5000);
    }

    public void OnForceBattleCancel(WWWResult www)
    {
      if (Network.IsError)
      {
        this.OnFailed();
        ((Behaviour) this).enabled = false;
        Network.RemoveAPI();
      }
      else
      {
        ((Behaviour) this).enabled = false;
        Network.RemoveAPI();
        this.DownloadPopupTimeout();
      }
    }

    private void DownloadPopupTimeout()
    {
      ((Behaviour) this).enabled = false;
      CriticalSection.Leave(CriticalSections.SceneChange);
      this.ActivateOutputLinks(5001);
    }

    private void SetVersusAudienceParam(string text)
    {
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      if (!instance.IsMultiVersus)
        return;
      if (instance.IsOldestPlayer())
      {
        int startIndex = text.IndexOf("\"btlinfo\"");
        if (startIndex != -1)
        {
          StringBuilder stringBuilder = new StringBuilder();
          string str = text.Substring(startIndex);
          string roomParam = instance.GetRoomParam("started");
          if (!string.IsNullOrEmpty(roomParam))
          {
            stringBuilder.Append(roomParam);
            --stringBuilder.Length;
            stringBuilder.Append(",");
            stringBuilder.Append(str);
            --stringBuilder.Length;
            instance.SetRoomParam("started", (object) stringBuilder.ToString());
          }
        }
      }
      instance.BattleStartRoom();
    }

    [DebuggerHidden]
    protected IEnumerator StartScene(BattleCore.Json_Battle json)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_StartQuest.\u003CStartScene\u003Ec__Iterator0()
      {
        json = json,
        \u0024this = this
      };
    }

    private void OnSceneAwake(GameObject scene)
    {
      SceneBattle component = scene.GetComponent<SceneBattle>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      CriticalSection.Leave(CriticalSections.SceneChange);
      CriticalSection.Leave(CriticalSections.SceneChange);
      SceneAwakeObserver.RemoveListener(new SceneAwakeObserver.SceneEvent(this.OnSceneAwake));
      component.StartQuest(this.mStartingQuest.iname, this.mQuestData);
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(1);
    }

    [DebuggerHidden]
    private IEnumerator StartAudience()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_StartQuest.\u003CStartAudience\u003Ec__Iterator1()
      {
        \u0024this = this
      };
    }

    private class QuestLauncher
    {
      public QuestParam Quest;
      public BattleCore.Json_Battle InitData;
      public bool Resume;

      public void OnSceneAwake(GameObject scene)
      {
        SceneBattle component = scene.GetComponent<SceneBattle>();
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          return;
        CriticalSection.Leave(CriticalSections.SceneChange);
        CriticalSection.Leave(CriticalSections.SceneChange);
        SceneAwakeObserver.RemoveListener(new SceneAwakeObserver.SceneEvent(this.OnSceneAwake));
        component.StartQuest(this.Quest.iname, this.InitData);
      }
    }
  }
}
