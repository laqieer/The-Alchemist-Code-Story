﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_StartQuest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(3, "NoMatchVersion", FlowNode.PinTypes.Output, 12)]
  [FlowNode.NodeType("System/Quest/Start", 32741)]
  [FlowNode.Pin(2, "Failed", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(1, "Started", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(100, "LoadMultiPlay", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "Resume", FlowNode.PinTypes.Input, 4)]
  [FlowNode.Pin(500, "LoadMultiTower", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(600, "NotRoomMT", FlowNode.PinTypes.Output, 22)]
  [FlowNode.Pin(0, "Load", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(9, "NoAudienceData", FlowNode.PinTypes.Output, 18)]
  [FlowNode.Pin(300, "AudienceFailed", FlowNode.PinTypes.Output, 19)]
  [FlowNode.Pin(8, "NoRoom", FlowNode.PinTypes.Output, 17)]
  [FlowNode.Pin(7, "MatchSuccess", FlowNode.PinTypes.Output, 16)]
  [FlowNode.Pin(301, "AudienceFailedMax", FlowNode.PinTypes.Output, 20)]
  [FlowNode.Pin(250, "LoadRankMatch", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(400, "NotGpsQuest", FlowNode.PinTypes.Output, 21)]
  [FlowNode.Pin(200, "LoadVersus", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(6, "ColoRankModify", FlowNode.PinTypes.Output, 15)]
  [FlowNode.Pin(5, "NetworkSuccess", FlowNode.PinTypes.Output, 14)]
  [FlowNode.Pin(4, "MultiMaintenance", FlowNode.PinTypes.Output, 13)]
  [FlowNode.Pin(20, "AudienceConnect", FlowNode.PinTypes.Input, 5)]
  [FlowNode.Pin(30, "AudienceStart", FlowNode.PinTypes.Input, 6)]
  public class FlowNode_StartQuest : FlowNode_Network
  {
    public int mReqID = -1;
    private const int PIN_IN_LOAD_RANK_MATCH = 250;
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
      GameManager instance = MonoSingleton<GameManager>.Instance;
      MyPhoton pt = PunMonoSingleton<MyPhoton>.Instance;
      instance.AudienceMode = false;
      this.mReqID = pinID;
      pt.IsMultiPlay = false;
      pt.IsMultiVersus = false;
      pt.IsRankMatch = false;
      if (pinID == 0 || pinID == 100 || (pinID == 200 || pinID == 250) || pinID == 500)
      {
        pt.IsMultiPlay = pinID == 100 || pinID == 200 || pinID == 250 || pinID == 500;
        pt.IsMultiVersus = pinID == 200 || pinID == 250;
        pt.IsRankMatch = pinID == 250;
        pinID = 0;
      }
      if (pinID == 10)
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
          this.ExecRequest((WebAPI) new ReqBtlComResume(btlId, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        }
        else
        {
          this.mStartingQuest = instance.FindQuest(GlobalVars.SelectedQuestID);
          PlayerPartyTypes partyIndex1 = this.QuestToPartyIndex(this.mStartingQuest.type);
          if (!string.IsNullOrEmpty(this.QuestID))
          {
            GlobalVars.SelectedQuestID = this.QuestID;
            GlobalVars.SelectedFriendID = string.Empty;
          }
          if (!this.PlayOffline && Network.Mode == Network.EConnectMode.Online)
          {
            PartyData partyOfType = instance.Player.FindPartyOfType(partyIndex1);
            int partyIndex2 = instance.Player.Partys.IndexOf(partyOfType);
            if (this.mStartingQuest.type == QuestTypes.Arena)
            {
              this.ActivateOutputLinks(5);
              this.StartCoroutine(this.StartScene((BattleCore.Json_Battle) null));
            }
            else
            {
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
                  List<JSON_MyPhotonPlayerParam> myPlayersStarted = pt.GetMyPlayersStarted();
                  for (int index = 0; index < myPlayersStarted.Count; ++index)
                  {
                    if (myPlayersStarted[index].playerIndex != pt.MyPlayerIndex)
                      stringList.Add(myPlayersStarted[index].UID);
                  }
                }
              }
              if (this.mReqID == 200)
                this.ExecRequest((WebAPI) new ReqVersus(this.mStartingQuest.iname, plid, seat, uid, versusStatusData, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), GlobalVars.SelectedMultiPlayVersusType));
              else if (this.mReqID == 250)
                this.ExecRequest((WebAPI) new ReqRankMatch(this.mStartingQuest.iname, plid, seat, uid, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
              else if (this.mReqID == 500)
                this.ExecRequest((WebAPI) new ReqBtlMultiTwReq(this.mStartingQuest.iname, partyIndex2, plid, seat, stringList.ToArray(), new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
              else
                this.ExecRequest((WebAPI) new ReqBtlComReq(this.mStartingQuest.iname, GlobalVars.SelectedFriendID, GlobalVars.SelectedSupport.Get(), new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), multi, partyIndex2, isHost, plid, seat, GlobalVars.Location, GlobalVars.SelectedRankingQuestParam));
            }
          }
          else
            this.StartCoroutine(this.StartScene((BattleCore.Json_Battle) null));
        }
      }
      else if (pinID == 20)
      {
        if (instance.AudienceRoom == null)
          return;
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
          VersusAudienceManager audienceManager = instance.AudienceManager;
          audienceManager.AddStartQuest();
          if (audienceManager.GetStartedParam() != null)
          {
            if (audienceManager.GetStartedParam().btlinfo != null)
            {
              BattleCore.Json_Battle json = new BattleCore.Json_Battle();
              json.btlinfo = audienceManager.GetStartedParam().btlinfo;
              CriticalSection.Enter(CriticalSections.SceneChange);
              instance.AudienceMode = true;
              this.StartCoroutine(this.StartScene(json));
            }
            else
            {
              DebugUtility.LogError("Not Exist btlInfo");
              if (audienceManager.IsRetryError)
              {
                Network.Abort();
                this.ActivateOutputLinks(300);
              }
              else
                this.ActivateOutputLinks(9);
            }
          }
          else
          {
            DebugUtility.LogError("Not Exist StartParam");
            if (audienceManager.IsRetryError)
            {
              Network.Abort();
              this.ActivateOutputLinks(300);
            }
            else
              this.ActivateOutputLinks(9);
          }
        }
      }
    }

    public PlayerPartyTypes QuestToPartyIndex(QuestTypes type)
    {
      switch (type)
      {
        case QuestTypes.Multi:
          return PlayerPartyTypes.Multiplay;
        case QuestTypes.Arena:
          return PlayerPartyTypes.Arena;
        case QuestTypes.Free:
        case QuestTypes.Extra:
          return PlayerPartyTypes.Event;
        case QuestTypes.Character:
          return PlayerPartyTypes.Character;
        case QuestTypes.Tower:
          return PlayerPartyTypes.Tower;
        case QuestTypes.VersusFree:
        case QuestTypes.VersusRank:
          return PlayerPartyTypes.Versus;
        case QuestTypes.RankMatch:
          return PlayerPartyTypes.RankMatch;
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
          case Network.EErrCode.VS_NotSelfBattle:
            this.OnFailed();
            break;
          case Network.EErrCode.VS_NotPlayer:
            this.OnFailed();
            break;
          case Network.EErrCode.VS_NotQuestInfo:
            this.OnFailed();
            break;
          case Network.EErrCode.VS_NotQuestData:
            this.OnFailed();
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
                switch (errCode - 202)
                {
                  case Network.EErrCode.Success:
                  case Network.EErrCode.Unknown:
                  case Network.EErrCode.AssetVersion:
                  case Network.EErrCode.NoVersionDbg:
                    this.OnMultiMaintenance();
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
                            if (errCode != Network.EErrCode.NotLocation)
                            {
                              if (errCode != Network.EErrCode.NotGpsQuest)
                              {
                                if (errCode != Network.EErrCode.QuestEnd)
                                {
                                  if (errCode != Network.EErrCode.NoBtlInfo)
                                  {
                                    if (errCode == Network.EErrCode.MultiVersionMismatch)
                                    {
                                      this.OnMismatchVersion();
                                      return;
                                    }
                                    this.OnRetry();
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
        WebAPI.JSON_BodyResponse<BattleCore.Json_Battle> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<BattleCore.Json_Battle>>(www.text);
        if (jsonObject.body == null)
        {
          this.OnRetry();
        }
        else
        {
          Network.RemoveAPI();
          this.ActivateOutputLinks(5);
          this.SetVersusAudienceParam(text);
          if (this.mResume && (MonoSingleton<GameManager>.Instance.Player.TutorialFlags & 1L) == 0L && jsonObject.body.btlinfo.qid == "QE_OP_0002")
            MonoSingleton<GameManager>.Instance.CompleteTutorialStep();
          this.StartCoroutine(this.StartScene(jsonObject.body));
        }
      }
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
      return (IEnumerator) new FlowNode_StartQuest.\u003CStartScene\u003Ec__IteratorD2() { json = json, \u003C\u0024\u003Ejson = json, \u003C\u003Ef__this = this };
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
      return (IEnumerator) new FlowNode_StartQuest.\u003CStartAudience\u003Ec__IteratorD3() { \u003C\u003Ef__this = this };
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
