// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_StartQuest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UnityEngine;

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
  [FlowNode.Pin(5000, "Download Cancel", FlowNode.PinTypes.Output, 5000)]
  [FlowNode.Pin(5001, "Download Timeout", FlowNode.PinTypes.Output, 5001)]
  public class FlowNode_StartQuest : FlowNode_Network
  {
    public int mReqID = -1;
    protected const int PIN_IN_LOAD = 0;
    protected const int PIN_IN_LOAD_MULTI_PLAY = 100;
    protected const int PIN_IN_LOAD_VERSUS = 200;
    protected const int PIN_IN_LOAD_VERSUS_CPU = 700;
    protected const int PIN_IN_LOAD_RANK_MATCH = 250;
    protected const int PIN_IN_LOAD_MULTI_TOWER = 500;
    protected const int PIN_IN_LOAD_RAID_BOSS = 600;
    protected const int PIN_IN_LOAD_ORDEAL = 1000;
    protected const int PIN_IN_LOAD_GENESIS_BOSS = 1200;
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
    protected const int PIN_OUT_GENESIS_OUT_OF_PERIOD = 1250;
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
      if (pinID == 0 || pinID == 100 || (pinID == 200 || pinID == 250) || (pinID == 500 || pinID == 700 || pinID == 1000))
      {
        instance2.IsMultiPlay = pinID == 100 || pinID == 200 || pinID == 250 || pinID == 500;
        instance2.IsMultiVersus = pinID == 200 || pinID == 250;
        instance1.IsVSCpuBattle = pinID == 700;
        instance2.IsRankMatch = pinID == 250;
        pinID = 0;
      }
      else if (pinID == 600 || pinID == 1200)
        pinID = 0;
      else if (pinID == 10)
      {
        this.mResume = true;
        pinID = 0;
      }
      if (pinID == 0)
      {
        if (this.enabled)
          return;
        this.enabled = true;
        CriticalSection.Enter(CriticalSections.SceneChange);
        if (this.mResume)
        {
          long btlId = (long) GlobalVars.BtlID;
          GlobalVars.BtlID.Set(0L);
          GlobalVars.BtlIDStatus.Set(true);
          if (GlobalVars.QuestType == QuestTypes.Raid)
            this.ExecRequest((WebAPI) new ReqRaidBtlResume(btlId, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          else if (GlobalVars.QuestType == QuestTypes.GenesisBoss)
            this.ExecRequest((WebAPI) new ReqGenesisBossBtlResume(btlId, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          else
            this.ExecRequest((WebAPI) new ReqBtlComResume(btlId, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
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
            if (instance2.IsMultiVersus && AssetDownloader.IsEnableShowSizeBeforeDownloading)
              this.LoadQuestStart();
            else if (flag && AssetDownloader.IsEnableShowSizeBeforeDownloading)
            {
              QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
              AssetDownloader.StartConfirmDownloadQuestContentYesNo(MonoSingleton<GameManager>.Instance.GetBattleEntryUnits(quest), (List<ItemData>) null, quest, (UIUtility.DialogResultEvent) (ok => this.LoadQuestStart()), (UIUtility.DialogResultEvent) (no =>
              {
                AssetDownloader.ClearDownloadRequests(true);
                this.DownloadNotApproved();
              }));
            }
            else
              this.LoadQuestStart();
          }
          else
            this.StartCoroutine(this.StartScene((BattleCore.Json_Battle) null));
        }
      }
      else if (pinID == 20)
      {
        if (instance1.AudienceRoom == null)
          return;
        instance1.AudienceMode = true;
        this.StartCoroutine(this.StartAudience());
      }
      else
      {
        if (pinID != 30)
          return;
        if (Network.IsError)
        {
          this.ActivateOutputLinks(300);
          Network.ResetError();
        }
        else if (!Network.IsStreamConnecting)
        {
          Network.ResetError();
          this.ActivateOutputLinks(300);
        }
        else
        {
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
            }
            else if (audienceManager.IsRetryError)
            {
              DebugUtility.LogError("Not Exist btlInfo");
              Network.Abort();
              this.ActivateOutputLinks(300);
            }
            else
              this.ActivateOutputLinks(9);
          }
          else if (audienceManager.IsRetryError)
          {
            DebugUtility.LogError("Not Exist StartParam");
            Network.Abort();
            this.ActivateOutputLinks(300);
          }
          else
            this.ActivateOutputLinks(9);
        }
      }
    }

    private void LoadQuestStart()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      MyPhoton pt = PunMonoSingleton<MyPhoton>.Instance;
      instance.AudienceMode = false;
      PlayerPartyTypes partyIndex1 = FlowNode_StartQuest.QuestToPartyIndex(this.mStartingQuest.type);
      if (this.mStartingQuest.type == QuestTypes.Arena)
      {
        this.ActivateOutputLinks(5);
        this.StartCoroutine(this.StartScene((BattleCore.Json_Battle) null));
      }
      else if (this.mStartingQuest.type == QuestTypes.Ordeal)
      {
        this.ExecRequest((WebAPI) new ReqBtlOrdealReq(this.mStartingQuest.iname, GlobalVars.OrdealSupports, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      }
      else
      {
        PartyData partyOfType = instance.Player.FindPartyOfType(partyIndex1);
        int partyIndex2 = instance.Player.Partys.IndexOf(partyOfType);
        bool multi = false;
        bool isHost = false;
        int seat = -1;
        int plid = -1;
        string uid = string.Empty;
        List<string> stringList = new List<string>();
        VersusStatusData versusStatusData = (VersusStatusData) null;
        int num1 = 0;
        if ((UnityEngine.Object) pt != (UnityEngine.Object) null)
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
              PlayerPartyTypes playerPartyTypes = PlayerPartyTypes.Versus;
              if (pt.IsRankMatch)
                playerPartyTypes = PlayerPartyTypes.RankMatch;
              PartyData party = instance.Player.Partys[(int) playerPartyTypes];
              if (party != null)
              {
                versusStatusData = new VersusStatusData();
                for (int index = 0; index < party.MAX_UNIT; ++index)
                {
                  long unitUniqueId = party.GetUnitUniqueID(index);
                  if (party.GetUnitUniqueID(index) != 0L)
                  {
                    UnitData unitDataByUniqueId = instance.Player.FindUnitDataByUniqueID(unitUniqueId);
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
            this.ExecRequest((WebAPI) new ReqVersus(this.mStartingQuest.iname, plid, seat, uid, versusStatusData, num1, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), GlobalVars.SelectedMultiPlayVersusType, 0, 0));
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
        else
          this.ExecRequest((WebAPI) new ReqBtlComReq(this.mStartingQuest.iname, GlobalVars.SelectedFriendID, GlobalVars.SelectedSupport.Get(), new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), multi, partyIndex2, isHost, plid, seat, GlobalVars.Location, GlobalVars.SelectedRankingQuestParam));
      }
    }

    public static PlayerPartyTypes QuestToPartyIndex(QuestTypes type)
    {
      switch (type)
      {
        case QuestTypes.Multi:
        case QuestTypes.MultiGps:
          return PlayerPartyTypes.Multiplay;
        case QuestTypes.Arena:
          return PlayerPartyTypes.Arena;
        case QuestTypes.Free:
        case QuestTypes.Extra:
        case QuestTypes.GenesisStory:
        case QuestTypes.GenesisBoss:
          return PlayerPartyTypes.Event;
        case QuestTypes.Character:
          return PlayerPartyTypes.Character;
        case QuestTypes.Tower:
          return PlayerPartyTypes.Tower;
        case QuestTypes.VersusFree:
        case QuestTypes.VersusRank:
          return PlayerPartyTypes.Versus;
        case QuestTypes.Ordeal:
          return PlayerPartyTypes.Ordeal;
        case QuestTypes.RankMatch:
          return PlayerPartyTypes.RankMatch;
        case QuestTypes.Raid:
          return PlayerPartyTypes.Raid;
        default:
          return PlayerPartyTypes.Normal;
      }
    }

    protected override void OnDestroy()
    {
      base.OnDestroy();
    }

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
      this.enabled = false;
      CriticalSection.Leave(CriticalSections.SceneChange);
      Network.RemoveAPI();
      Network.ResetError();
      this.ActivateOutputLinks(3);
    }

    public void OnMultiMaintenance()
    {
      this.enabled = false;
      CriticalSection.Leave(CriticalSections.SceneChange);
      Network.RemoveAPI();
      this.ActivateOutputLinks(4);
    }

    public void OnVersusNoPlayer()
    {
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      if ((UnityEngine.Object) instance != (UnityEngine.Object) null && instance.IsOldestPlayer())
        instance.OpenRoom(true, false);
      this.enabled = false;
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
          case Network.EErrCode.ColoCantSelect:
            this.OnBack();
            break;
          case Network.EErrCode.ColoIsBusy:
            this.OnBack();
            break;
          case Network.EErrCode.ColoCostShort:
            this.OnFailed();
            break;
          case Network.EErrCode.ColoIntervalShort:
            this.OnBack();
            break;
          case Network.EErrCode.ColoBattleNotEnd:
            this.OnFailed();
            break;
          case Network.EErrCode.ColoPlayerLvShort:
            this.OnBack();
            break;
          case Network.EErrCode.VS_BattleNotEnd:
            this.OnFailed();
            break;
          case Network.EErrCode.VS_ComBattleEnd:
            this.OnFailed();
            break;
          case Network.EErrCode.VS_TowerNotPlay:
            this.OnFailed();
            break;
          case Network.EErrCode.VS_NotContinuousEnemy:
            this.OnFailed();
            break;
          case Network.EErrCode.VS_RowerNotMatching:
            this.OnFailed();
            break;
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
                        switch (errCode - 12001)
                        {
                          case Network.EErrCode.Success:
                            this.OnFailed();
                            return;
                          case Network.EErrCode.Version:
                            CriticalSection.Leave(CriticalSections.SceneChange);
                            Network.RemoveAPI();
                            this.enabled = false;
                            this.ActivateOutputLinks(600);
                            return;
                          default:
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
                                      if (errCode != Network.EErrCode.NoBtlInfo)
                                      {
                                        if (errCode != Network.EErrCode.MultiVersionMismatch)
                                        {
                                          if (errCode != Network.EErrCode.QuestArchive_ArchiveNotOpened)
                                          {
                                            if (errCode != Network.EErrCode.Genesis_OutOfPeriod)
                                            {
                                              if (errCode != Network.EErrCode.Raid_OutOfPeriod)
                                              {
                                                if (errCode != Network.EErrCode.Raid_AlreadyBeat)
                                                {
                                                  if (errCode == Network.EErrCode.Raid_CanNotRescueTimeOver)
                                                  {
                                                    CriticalSection.Leave(CriticalSections.SceneChange);
                                                    Network.RemoveAPI();
                                                    Network.ResetError();
                                                    BattleCore.RemoveSuspendData();
                                                    this.ActivateOutputLinks(902);
                                                    this.ActivateOutputLinks(904);
                                                    this.enabled = false;
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
                                                this.enabled = false;
                                                return;
                                              }
                                              CriticalSection.Leave(CriticalSections.SceneChange);
                                              Network.RemoveAPI();
                                              Network.ResetError();
                                              BattleCore.RemoveSuspendData();
                                              this.ActivateOutputLinks(902);
                                              this.ActivateOutputLinks(903);
                                              this.enabled = false;
                                              return;
                                            }
                                            CriticalSection.Leave(CriticalSections.SceneChange);
                                            Network.RemoveAPI();
                                            Network.ResetError();
                                            BattleCore.RemoveSuspendData();
                                            this.ActivateOutputLinks(1250);
                                            this.enabled = false;
                                            return;
                                          }
                                          this.OnBack();
                                          return;
                                        }
                                        this.OnMismatchVersion();
                                        return;
                                      }
                                      this.OnFailed();
                                      return;
                                    }
                                    this.OnFailed();
                                    return;
                                  }
                                  CriticalSection.Leave(CriticalSections.SceneChange);
                                  Network.RemoveAPI();
                                  Network.ResetError();
                                  this.ActivateOutputLinks(400);
                                  this.enabled = false;
                                  return;
                                }
                                this.OnBack();
                                return;
                              }
                              CriticalSection.Leave(CriticalSections.SceneChange);
                              Network.RemoveAPI();
                              Network.ResetError();
                              BattleCore.RemoveSuspendData();
                              this.ActivateOutputLinks(902);
                              this.ActivateOutputLinks(905);
                              this.enabled = false;
                              return;
                            }
                            CriticalSection.Leave(CriticalSections.SceneChange);
                            Network.RemoveAPI();
                            Network.ResetError();
                            BattleCore.RemoveSuspendData();
                            this.ActivateOutputLinks(901);
                            this.enabled = false;
                            return;
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
          Network.RemoveAPI();
          if (res.body.is_rehash != 0)
          {
            GlobalVars.BtlID.Set(res.body.btlid);
            UIUtility.SystemMessage(string.Empty, LocalizedText.Get("sys.FAILED_RESUMEQUEST"), (UIUtility.DialogResultEvent) (go =>
            {
              CriticalSection.Leave(CriticalSections.SceneChange);
              this.ActivateOutputLinks(800);
            }), (GameObject) null, false, -1);
          }
          else if (res.body.is_timeover != 0)
          {
            CriticalSection.Leave(CriticalSections.SceneChange);
            BattleCore.RemoveSuspendData();
            this.ActivateOutputLinks(902);
            this.ActivateOutputLinks(900);
          }
          else if (this.mResume && AssetManager.UseDLC && AssetDownloader.IsEnableShowSizeBeforeDownloading)
          {
            QuestParam questParam = res.body.btlinfo.GetQuestParam();
            AssetDownloader.StartConfirmDownloadQuestContentYesNo(res.body.btlinfo.GetPlayerSideUnits(), (List<ItemData>) null, questParam, (UIUtility.DialogResultEvent) (ok => this.DownloadApproved(text, res)), (UIUtility.DialogResultEvent) (no =>
            {
              AssetDownloader.ClearDownloadRequests(true);
              this.DownloadNotApproved();
            }));
          }
          else if (PunMonoSingleton<MyPhoton>.Instance.IsMultiVersus && !FlowNode_MultiPlayRoomIsDraft.IsDraftMultiPlayRoom() && (AssetManager.UseDLC && AssetDownloader.IsEnableShowSizeBeforeDownloading))
          {
            QuestParam quest_param = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
            AssetDownloader.StartConfirmDownloadQuestContent(res.body.btlinfo.GetPlayerSideUnits(), (List<ItemData>) null, quest_param, (UIUtility.DialogResultEvent) (ok => this.DownloadApproved(text, res)), (AssetDownloader.OnDownloadPopupTimeout) (() =>
            {
              if (quest_param.type == QuestTypes.RankMatch)
                Network.RequestAPI((WebAPI) new ReqRankMatchEnd(res.body.btlid, BtlResultTypes.Cancel, (string) null, (string) null, 0U, (int[]) null, (int[]) null, 0, 0, 0, 0, (int[]) null, new Network.ResponseCallback(this.OnForceBattleCancel), (string) null, (string) null, (string) null), false);
              else
                Network.RequestAPI((WebAPI) new ReqVersusEnd(res.body.btlid, BtlResultTypes.Cancel, (string) null, (string) null, 0U, (int[]) null, (int[]) null, 0, 0, 0, 0, (int[]) null, new Network.ResponseCallback(this.OnForceBattleCancel), GlobalVars.SelectedMultiPlayVersusType, (string) null, (string) null), false);
              AssetDownloader.ClearDownloadRequests(true);
            }), GameSettings.Instance.ForApple_DownloadPopupTimeoutSec);
          }
          else
            this.DownloadApproved(text, res);
        }
      }
    }

    private void DownloadApproved(string text, WebAPI.JSON_BodyResponse<BattleCore.Json_Battle> res)
    {
      this.ActivateOutputLinks(5);
      this.SetVersusAudienceParam(text);
      this.StartCoroutine(this.StartScene(res.body));
    }

    private void DownloadNotApproved()
    {
      this.enabled = false;
      CriticalSection.Leave(CriticalSections.SceneChange);
      this.ActivateOutputLinks(5000);
    }

    public void OnForceBattleCancel(WWWResult www)
    {
      if (Network.IsError)
      {
        this.OnFailed();
        this.enabled = false;
        Network.RemoveAPI();
      }
      else
      {
        this.enabled = false;
        Network.RemoveAPI();
        this.DownloadPopupTimeout();
      }
    }

    private void DownloadPopupTimeout()
    {
      this.enabled = false;
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
            instance.SetRoomParam("started", stringBuilder.ToString());
          }
        }
      }
      instance.BattleStartRoom();
    }

    [DebuggerHidden]
    protected IEnumerator StartScene(BattleCore.Json_Battle json)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_StartQuest.\u003CStartScene\u003Ec__Iterator0() { json = json, \u0024this = this };
    }

    private void OnSceneAwake(GameObject scene)
    {
      SceneBattle component = scene.GetComponent<SceneBattle>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      CriticalSection.Leave(CriticalSections.SceneChange);
      CriticalSection.Leave(CriticalSections.SceneChange);
      SceneAwakeObserver.RemoveListener(new SceneAwakeObserver.SceneEvent(this.OnSceneAwake));
      component.StartQuest(this.mStartingQuest.iname, this.mQuestData);
      this.enabled = false;
      this.ActivateOutputLinks(1);
    }

    [DebuggerHidden]
    private IEnumerator StartAudience()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_StartQuest.\u003CStartAudience\u003Ec__Iterator1() { \u0024this = this };
    }

    private class QuestLauncher
    {
      public QuestParam Quest;
      public BattleCore.Json_Battle InitData;
      public bool Resume;

      public void OnSceneAwake(GameObject scene)
      {
        SceneBattle component = scene.GetComponent<SceneBattle>();
        if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
          return;
        CriticalSection.Leave(CriticalSections.SceneChange);
        CriticalSection.Leave(CriticalSections.SceneChange);
        SceneAwakeObserver.RemoveListener(new SceneAwakeObserver.SceneEvent(this.OnSceneAwake));
        component.StartQuest(this.Quest.iname, this.InitData);
      }
    }
  }
}
