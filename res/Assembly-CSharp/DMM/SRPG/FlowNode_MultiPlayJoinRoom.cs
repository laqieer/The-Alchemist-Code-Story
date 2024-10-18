// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiPlayJoinRoom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using ExitGames.Client.Photon;
using GR;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Multi/MultiPlayJoinRoom", 32741)]
  [FlowNode.Pin(101, "CreateRoom", FlowNode.PinTypes.Input, 101)]
  [FlowNode.Pin(102, "JoinRoom", FlowNode.PinTypes.Input, 102)]
  [FlowNode.Pin(103, "CreateOrJoinLINE", FlowNode.PinTypes.Input, 103)]
  [FlowNode.Pin(200, "VersusCreateRoom", FlowNode.PinTypes.Input, 200)]
  [FlowNode.Pin(201, "VersusJoinRoom", FlowNode.PinTypes.Input, 201)]
  [FlowNode.Pin(202, "VersusTowerJoinRoom", FlowNode.PinTypes.Input, 202)]
  [FlowNode.Pin(203, "RankMatchJoinRoom", FlowNode.PinTypes.Input, 203)]
  [FlowNode.Pin(204, "RankMatchCreateRoom", FlowNode.PinTypes.Input, 204)]
  [FlowNode.Pin(300, "MTCreateRoom", FlowNode.PinTypes.Input, 300)]
  [FlowNode.Pin(301, "MTJoinRoom", FlowNode.PinTypes.Input, 301)]
  [FlowNode.Pin(302, "MTResumeJoinRoom", FlowNode.PinTypes.Input, 302)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(2, "Failure", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(3, "FailureLobby", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(4, "IllegalQuest", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(5, "FullMember", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(6, "battleversion Failure", FlowNode.PinTypes.Output, 0)]
  public class FlowNode_MultiPlayJoinRoom : FlowNode
  {
    private const int INPUT_PIN_VERSUS_CREATE_ROOM = 200;
    private const int INPUT_PIN_VERSUS_JOIN_ROOM = 201;
    private const int INPUT_PIN_RANK_MATCH_JOIN_ROOM = 203;
    private const int INPUT_PIN_RANK_MATCH_CREATE_ROOM = 204;
    private const int RANKMATCH_SCORE_RANGE_MAX = 900;
    private const int RANKMATCH_SCORE_RANGE_MIN = 100;
    private StateMachine<FlowNode_MultiPlayJoinRoom> mStateMachine;
    private JSON_MyPhotonPlayerParam mJoinPlayerParam;
    private readonly byte VERSUS_PLAYER_MAX = 3;

    private bool IsLINE { get; set; }

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 101:
          DebugUtility.Log("Start Create Room");
          ((Behaviour) this).enabled = true;
          this.IsLINE = false;
          this.mStateMachine = new StateMachine<FlowNode_MultiPlayJoinRoom>(this);
          this.mStateMachine.GotoState<FlowNode_MultiPlayJoinRoom.State_CreateRoom>();
          break;
        case 102:
          DebugUtility.Log("Start Join Room");
          ((Behaviour) this).enabled = true;
          this.IsLINE = false;
          this.mStateMachine = new StateMachine<FlowNode_MultiPlayJoinRoom>(this);
          this.mStateMachine.GotoState<FlowNode_MultiPlayJoinRoom.State_JoinRoom>();
          break;
        case 103:
          DebugUtility.Log("Start Create/Join Room LINE");
          ((Behaviour) this).enabled = true;
          this.IsLINE = true;
          if (JSON_MyPhotonRoomParam.GetCreatorFUID().Equals(FlowNode_OnUrlSchemeLaunch.LINEParam_decided.creatorFUID))
          {
            DebugUtility.Log("Creating LINERoom iname:" + GlobalVars.SelectedQuestID + " type:" + (object) GlobalVars.SelectedMultiPlayRoomType + " creatorFUID:" + FlowNode_OnUrlSchemeLaunch.LINEParam_decided.creatorFUID);
            this.mStateMachine = new StateMachine<FlowNode_MultiPlayJoinRoom>(this);
            this.mStateMachine.GotoState<FlowNode_MultiPlayJoinRoom.State_CreateRoom>();
            break;
          }
          DebugUtility.Log("Joining LINERoom iname:" + GlobalVars.SelectedQuestID + " type:" + (object) GlobalVars.SelectedMultiPlayRoomType + " creatorFUID:" + FlowNode_OnUrlSchemeLaunch.LINEParam_decided.creatorFUID + " name:" + GlobalVars.SelectedMultiPlayRoomName);
          this.mStateMachine = new StateMachine<FlowNode_MultiPlayJoinRoom>(this);
          this.mStateMachine.GotoState<FlowNode_MultiPlayJoinRoom.State_JoinRoom>();
          break;
        case 200:
          DebugUtility.Log("Start Versus Create Room");
          ((Behaviour) this).enabled = true;
          this.IsLINE = false;
          this.mStateMachine = new StateMachine<FlowNode_MultiPlayJoinRoom>(this);
          this.mStateMachine.GotoState<FlowNode_MultiPlayJoinRoom.State_VersusCreate>();
          break;
        case 201:
          DebugUtility.Log("Start Versus Join Room");
          ((Behaviour) this).enabled = true;
          this.IsLINE = false;
          this.mStateMachine = new StateMachine<FlowNode_MultiPlayJoinRoom>(this);
          this.mStateMachine.GotoState<FlowNode_MultiPlayJoinRoom.State_VersusJoin>();
          break;
        case 202:
          DebugUtility.Log("Start Versus Rank Join Room");
          ((Behaviour) this).enabled = true;
          this.IsLINE = false;
          this.mStateMachine = new StateMachine<FlowNode_MultiPlayJoinRoom>(this);
          this.mStateMachine.GotoState<FlowNode_MultiPlayJoinRoom.State_VersusTowerJoin>();
          break;
        case 203:
          DebugUtility.Log("Start Rank Match Join Room");
          ((Behaviour) this).enabled = true;
          this.IsLINE = false;
          this.mStateMachine = new StateMachine<FlowNode_MultiPlayJoinRoom>(this);
          this.mStateMachine.GotoState<FlowNode_MultiPlayJoinRoom.State_VersusRankJoin>();
          break;
        case 204:
          DebugUtility.Log("Start Rank Match Create Room");
          ((Behaviour) this).enabled = true;
          this.IsLINE = false;
          this.mStateMachine = new StateMachine<FlowNode_MultiPlayJoinRoom>(this);
          this.mStateMachine.GotoState<FlowNode_MultiPlayJoinRoom.State_VersusRankCreate>();
          break;
        case 300:
          DebugUtility.Log("Start MultiTower CreateRoom");
          ((Behaviour) this).enabled = true;
          this.IsLINE = false;
          this.mStateMachine = new StateMachine<FlowNode_MultiPlayJoinRoom>(this);
          this.mStateMachine.GotoState<FlowNode_MultiPlayJoinRoom.State_TowerCreate>();
          break;
        case 301:
          DebugUtility.Log("Start MultiTower JoinRoom");
          ((Behaviour) this).enabled = true;
          this.IsLINE = false;
          this.mStateMachine = new StateMachine<FlowNode_MultiPlayJoinRoom>(this);
          this.mStateMachine.GotoState<FlowNode_MultiPlayJoinRoom.State_TowerJoin>();
          break;
        case 302:
          DebugUtility.Log("Start MultiTower Resume JoinRoom");
          ((Behaviour) this).enabled = true;
          this.IsLINE = false;
          this.mStateMachine = new StateMachine<FlowNode_MultiPlayJoinRoom>(this);
          this.mStateMachine.GotoState<FlowNode_MultiPlayJoinRoom.State_ResumeTowerJoinRoom>();
          break;
      }
    }

    private void Update()
    {
      if (this.mStateMachine == null)
        return;
      this.mStateMachine.Update();
    }

    private bool BattleversionFind()
    {
      if (!Network.GetEnvironment.IsEnvironmentFlag(Gsc.App.Environment.EnvironmentFlagBit.ENV_FLG_PHOTONVERSION_OFF))
      {
        MyPhoton.MyRoom currentRoom = PunMonoSingleton<MyPhoton>.Instance.GetCurrentRoom();
        if (currentRoom != null)
        {
          JSON_MyPhotonRoomParam myPhotonRoomParam = JSON_MyPhotonRoomParam.Parse(currentRoom.json);
          if (myPhotonRoomParam != null && myPhotonRoomParam.btlver != MonoSingleton<GameManager>.Instance.BattleVersion)
          {
            this.FailureBattleVersion();
            return false;
          }
        }
        else
        {
          this.FailureBattleVersion();
          return false;
        }
      }
      return true;
    }

    private void Success()
    {
      if (!Network.GetEnvironment.IsEnvironmentFlag(Gsc.App.Environment.EnvironmentFlagBit.ENV_FLG_PHOTONVERSION_OFF) && !this.BattleversionFind())
        return;
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(1);
      DebugUtility.Log("Create/Join Room Success.");
    }

    private void Failure()
    {
      ((Behaviour) this).enabled = false;
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      if (instance.CurrentState != MyPhoton.MyState.NOP)
        instance.Disconnect();
      this.ActivateOutputLinks(2);
      DebugUtility.Log("Create/Join Room Failure.");
      MyPhoton.PhotonSendLog("FlowNode_MultiPlayJoinRoom:Failure", "Create / Join Room Failure");
    }

    private void FailureBattleVersion()
    {
      ((Behaviour) this).enabled = false;
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      if (instance.CurrentState != MyPhoton.MyState.NOP)
        instance.Disconnect();
      this.ActivateOutputLinks(6);
      Debug.Log((object) ("MyState:" + instance.CurrentState.ToString() + "error:" + (object) instance.LastError));
      Debug.LogError((object) "Create/Join Room BattleVersion Failure.");
      MyPhoton.PhotonSendLog("FlowNode_MultiPlayJoinRoom:FailureBattleVersion", "Create/Join Room BattleVersion Failure.");
    }

    private void FailureLobby()
    {
      ((Behaviour) this).enabled = false;
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      if (instance.CurrentState != MyPhoton.MyState.LOBBY)
      {
        instance.Disconnect();
        this.ActivateOutputLinks(2);
      }
      else
        this.ActivateOutputLinks(3);
      DebugUtility.Log("Create/Join Room FailureLobby.");
      MyPhoton.PhotonSendLog("FlowNode_MultiPlayJoinRoom:FailureLobby", "Create/Join Room FailureLobby");
    }

    private void IllegalQuest()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(4);
      DebugUtility.Log("Create/Join Room IllegalQuest.");
      MyPhoton.PhotonSendLog("FlowNode_MultiPlayJoinRoom:IllegalQuest", "Create/Join Room IllegalQuest.");
    }

    private void FailureFullMember()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(5);
      DebugUtility.Log("Join Room FullMember.");
      MyPhoton.PhotonSendLog("FlowNode_MultiPlayJoinRoom:FailureFullMember", "Join Room FullMember.");
    }

    public void GotoState<StateType>() where StateType : State<FlowNode_MultiPlayJoinRoom>, new()
    {
      if (this.mStateMachine == null)
        return;
      this.mStateMachine.GotoState<StateType>();
    }

    private class State_CreateRoom : State<FlowNode_MultiPlayJoinRoom>
    {
      public override void Begin(FlowNode_MultiPlayJoinRoom self)
      {
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
        if (quest == null || !quest.IsMulti || (int) quest.playerNum < 1 || (int) quest.unitNum < 1 || (int) quest.unitNum > 17 || quest.map == null || quest.map.Count <= 0)
        {
          DebugUtility.Log("illegal iname:" + GlobalVars.SelectedQuestID);
          self.IllegalQuest();
        }
        else
        {
          DebugUtility.Log("CreateRoom quest:" + quest.iname + " desc:" + quest.name);
          self.mJoinPlayerParam = JSON_MyPhotonPlayerParam.Create();
          if (self.mJoinPlayerParam == null)
          {
            self.FailureLobby();
          }
          else
          {
            JSON_MyPhotonRoomParam myPhotonRoomParam = new JSON_MyPhotonRoomParam();
            myPhotonRoomParam.creatorName = MonoSingleton<GameManager>.Instance.Player.Name;
            myPhotonRoomParam.creatorLV = MonoSingleton<GameManager>.Instance.Player.CalcLevel();
            myPhotonRoomParam.creatorFUID = JSON_MyPhotonRoomParam.GetCreatorFUID();
            myPhotonRoomParam.roomid = GlobalVars.SelectedMultiPlayRoomID;
            myPhotonRoomParam.comment = GlobalVars.SelectedMultiPlayRoomComment;
            myPhotonRoomParam.passCode = GlobalVars.EditMultiPlayRoomPassCode;
            myPhotonRoomParam.btlSpd = GlobalVars.SelectedMultiPlayBtlSpeed;
            myPhotonRoomParam.autoAllowed = !GlobalVars.SelectedMultiPlayAutoAllowed ? 0 : 1;
            Gsc.App.Environment getEnvironment = Network.GetEnvironment;
            myPhotonRoomParam.btlver = getEnvironment.IsEnvironmentFlag(Gsc.App.Environment.EnvironmentFlagBit.ENV_FLG_PHOTONVERSION_OFF) ? Network.Version : MonoSingleton<GameManager>.Instance.BattleVersion;
            myPhotonRoomParam.iname = GlobalVars.SelectedQuestID;
            myPhotonRoomParam.type = (int) GlobalVars.SelectedMultiPlayRoomType;
            myPhotonRoomParam.isLINE = !self.IsLINE ? 0 : 1;
            myPhotonRoomParam.unitlv = !GlobalVars.SelectedMultiPlayLimit ? 0 : GlobalVars.MultiPlayJoinUnitLv;
            DebugUtility.Log("create isLINE:" + (object) myPhotonRoomParam.isLINE + " iname:" + myPhotonRoomParam.iname + " roomid:" + (object) myPhotonRoomParam.roomid + " appID:" + GlobalVars.SelectedMultiPlayPhotonAppID + " token:" + GlobalVars.SelectedMultiPlayRoomName + " comment:" + myPhotonRoomParam.comment + " pass:" + myPhotonRoomParam.passCode + " type:" + (object) myPhotonRoomParam.type + " json:" + myPhotonRoomParam.Serialize());
            if (instance.CreateRoom((int) quest.playerNum, GlobalVars.SelectedMultiPlayRoomName, myPhotonRoomParam.Serialize(), self.mJoinPlayerParam.Serialize()))
              return;
            self.FailureLobby();
          }
        }
      }

      public override void Update(FlowNode_MultiPlayJoinRoom self)
      {
        if (!((Behaviour) self).enabled)
          return;
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        switch (instance.CurrentState)
        {
          case MyPhoton.MyState.LOBBY:
            DebugUtility.Log("[PUN]create room failed, back to lobby.");
            if (instance.LastError != MyPhoton.MyError.NOP)
            {
              MyPhoton.PhotonSendLog(this.GetType().Name + " Update", "[PUN]create room failed, back to lobby.");
              DebugUtility.Log(instance.LastError.ToString());
              instance.ResetLastError();
            }
            self.Failure();
            break;
          case MyPhoton.MyState.JOINING:
            break;
          case MyPhoton.MyState.ROOM:
            if ((double) (Time.realtimeSinceStartup - FlowNode_MultiPlayAPI.RoomMakeTime) > 25.0)
            {
              self.Failure();
              MyPhoton.PhotonSendLog(this.GetType().Name + " Update", "[PUN]create room too late, give up.");
              DebugUtility.Log("[PUN]create room too late, give up.");
              break;
            }
            self.GotoState<FlowNode_MultiPlayJoinRoom.State_DecidePlayerIndex>();
            break;
          default:
            self.Failure();
            MyPhoton.PhotonSendLog(this.GetType().Name + " Update", "[PUN]create room failed, error.");
            DebugUtility.Log("[PUN]create room failed, error.");
            break;
        }
      }

      public override void End(FlowNode_MultiPlayJoinRoom self)
      {
      }
    }

    private class State_JoinRoom : State<FlowNode_MultiPlayJoinRoom>
    {
      public override void Begin(FlowNode_MultiPlayJoinRoom self)
      {
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        if (string.IsNullOrEmpty(GlobalVars.SelectedMultiPlayRoomName))
        {
          self.FailureLobby();
        }
        else
        {
          QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
          if (quest == null)
          {
            DebugUtility.Log("illegal iname:" + GlobalVars.SelectedQuestID);
            self.IllegalQuest();
          }
          else
          {
            if (GlobalVars.SelectedMultiPlayRoomType == JSON_MyPhotonRoomParam.EType.RAID)
            {
              if (quest.IsVersus)
                GlobalVars.SelectedMultiPlayRoomType = JSON_MyPhotonRoomParam.EType.VERSUS;
              else if (quest.IsMultiTower)
                GlobalVars.SelectedMultiPlayRoomType = JSON_MyPhotonRoomParam.EType.TOWER;
            }
            self.mJoinPlayerParam = JSON_MyPhotonPlayerParam.Create();
            if (self.mJoinPlayerParam == null)
            {
              self.FailureLobby();
            }
            else
            {
              DebugUtility.Log("Joining name:" + GlobalVars.SelectedMultiPlayRoomName + " pnum:" + (object) quest.playerNum + " unum:" + (object) quest.unitNum);
              if (instance.JoinRoom(GlobalVars.SelectedMultiPlayRoomName, self.mJoinPlayerParam.Serialize(), GlobalVars.ResumeMultiplayPlayerID != 0))
                return;
              DebugUtility.Log("error:" + (object) instance.LastError);
              self.FailureLobby();
            }
          }
        }
      }

      public override void Update(FlowNode_MultiPlayJoinRoom self)
      {
        if (!((Behaviour) self).enabled)
          return;
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        switch (instance.CurrentState)
        {
          case MyPhoton.MyState.LOBBY:
            DebugUtility.Log("[PUN]joining failed, back to lobby." + (object) instance.LastError);
            MyPhoton.PhotonSendLog(this.GetType().Name + " Update", "[PUN]joining failed, back to lobby." + (object) instance.LastError);
            if (instance.LastError == MyPhoton.MyError.ROOM_IS_FULL)
            {
              self.FailureFullMember();
              break;
            }
            self.FailureLobby();
            break;
          case MyPhoton.MyState.JOINING:
            break;
          case MyPhoton.MyState.ROOM:
            self.GotoState<FlowNode_MultiPlayJoinRoom.State_DecidePlayerIndex>();
            break;
          default:
            self.Failure();
            DebugUtility.Log("[PUN]joining failed, error.");
            MyPhoton.PhotonSendLog(this.GetType().Name + " Update", "[PUN]joining failed, error.");
            break;
        }
      }

      public override void End(FlowNode_MultiPlayJoinRoom self)
      {
      }
    }

    private class State_DecidePlayerIndex : State<FlowNode_MultiPlayJoinRoom>
    {
      private int CannotJoinCounter = 10;

      public override void Begin(FlowNode_MultiPlayJoinRoom self)
      {
      }

      public override void Update(FlowNode_MultiPlayJoinRoom self)
      {
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        if (instance.CurrentState != MyPhoton.MyState.ROOM)
        {
          Debug.Log((object) "state not Room");
          self.Failure();
        }
        else
        {
          MyPhoton.MyPlayer myPlayer1 = instance.GetMyPlayer();
          List<MyPhoton.MyPlayer> roomPlayerList = instance.GetRoomPlayerList();
          foreach (MyPhoton.MyPlayer myPlayer2 in roomPlayerList)
          {
            JSON_MyPhotonPlayerParam photonPlayerParam = JSON_MyPhotonPlayerParam.Parse(myPlayer2.json);
            if (myPlayer2.playerID < myPlayer1.playerID && photonPlayerParam.playerIndex <= 0)
            {
              DebugUtility.Log("[PUN]waiting for player index turn..." + (object) myPlayer2.playerID + " me:" + (object) myPlayer1.playerID);
              return;
            }
          }
          MyPhoton.MyRoom currentRoom = instance.GetCurrentRoom();
          if (currentRoom.maxPlayers == 0)
          {
            Debug.LogError((object) ("Invalid Room:" + currentRoom.name));
            PhotonNetwork.room.IsOpen = false;
            self.Failure();
          }
          else
          {
            bool[] flagArray = new bool[currentRoom.maxPlayers];
            for (int index = 0; index < flagArray.Length; ++index)
              flagArray[index] = false;
            foreach (MyPhoton.MyPlayer myPlayer3 in roomPlayerList)
            {
              JSON_MyPhotonPlayerParam photonPlayerParam = JSON_MyPhotonPlayerParam.Parse(myPlayer3.json);
              if (myPlayer3.playerID < myPlayer1.playerID && photonPlayerParam.playerIndex > 0)
              {
                flagArray[photonPlayerParam.playerIndex - 1] = true;
                DebugUtility.Log("[PUN]player index " + (object) photonPlayerParam.playerIndex + " is used. (room:" + (object) currentRoom.maxPlayers + ")");
              }
            }
            if (currentRoom.param != null && currentRoom.param.support != null)
            {
              foreach (JSON_MyPhotonPlayerParam photonPlayerParam in currentRoom.param.support)
              {
                if (photonPlayerParam.playerIndex > 0)
                {
                  flagArray[photonPlayerParam.playerIndex - 1] = true;
                  DebugUtility.Log("[PUN]player index " + (object) photonPlayerParam.playerIndex + " is used. (room:" + (object) currentRoom.maxPlayers + ")");
                }
              }
            }
            int playerIndex = 0;
            if (GlobalVars.ResumeMultiplaySeatID > 0)
            {
              playerIndex = GlobalVars.ResumeMultiplaySeatID;
            }
            else
            {
              for (int index = 0; index < currentRoom.maxPlayers; ++index)
              {
                if (!flagArray[index])
                {
                  playerIndex = index + 1;
                  break;
                }
              }
            }
            this.SetParam(instance, currentRoom, myPlayer1, playerIndex);
          }
        }
      }

      public override void End(FlowNode_MultiPlayJoinRoom self)
      {
      }

      private void SetParam(
        MyPhoton pt,
        MyPhoton.MyRoom room,
        MyPhoton.MyPlayer me,
        int playerIndex)
      {
        if (playerIndex <= 0)
        {
          --this.CannotJoinCounter;
          if (this.CannotJoinCounter >= 0)
            return;
          this.self.Failure();
        }
        else
        {
          DebugUtility.Log("[PUN]empty player index found: " + (object) playerIndex);
          if (pt.IsMultiVersus && playerIndex >= 3)
          {
            foreach (PhotonPlayer player in PhotonNetwork.playerList)
            {
              new MyPhoton.MyPlayer().playerID = player.ID;
              Hashtable customProperties = player.CustomProperties;
              if (customProperties != null && ((Dictionary<object, object>) customProperties).Count > 0 && ((Dictionary<object, object>) customProperties).ContainsKey((object) "json"))
              {
                string buffer;
                GameUtility.Binary2Object<string>(out buffer, (byte[]) customProperties[(object) "json"]);
                Debug.Log((object) ("player json : " + buffer));
              }
            }
            Debug.LogError((object) "MultiVersus is playerindex over : 3");
          }
          this.self.mJoinPlayerParam.playerID = me.playerID;
          this.self.mJoinPlayerParam.playerIndex = playerIndex;
          this.self.mJoinPlayerParam.UpdateMultiTowerPlacement(true);
          pt.SetMyPlayerParam(this.self.mJoinPlayerParam.Serialize());
          pt.MyPlayerIndex = playerIndex;
          JSON_MyPhotonRoomParam myPhotonRoomParam = JSON_MyPhotonRoomParam.Parse(room.json);
          if (myPhotonRoomParam != null)
          {
            GlobalVars.SelectedMultiPlayHiSpeed = myPhotonRoomParam.btlSpd > 1;
            GlobalVars.SelectedMultiPlayBtlSpeed = myPhotonRoomParam.btlSpd;
            GlobalVars.SelectedMultiPlayAutoAllowed = myPhotonRoomParam.autoAllowed != 0;
          }
          this.self.Success();
        }
      }
    }

    private class State_VersusCreate : State<FlowNode_MultiPlayJoinRoom>
    {
      public override void Begin(FlowNode_MultiPlayJoinRoom self)
      {
        GameManager instance1 = MonoSingleton<GameManager>.Instance;
        MyPhoton instance2 = PunMonoSingleton<MyPhoton>.Instance;
        self.mJoinPlayerParam = JSON_MyPhotonPlayerParam.Create();
        if (self.mJoinPlayerParam == null)
        {
          self.FailureLobby();
        }
        else
        {
          JSON_MyPhotonRoomParam myPhotonRoomParam = new JSON_MyPhotonRoomParam();
          myPhotonRoomParam.creatorName = MonoSingleton<GameManager>.Instance.Player.Name;
          myPhotonRoomParam.creatorLV = MonoSingleton<GameManager>.Instance.Player.CalcLevel();
          myPhotonRoomParam.creatorFUID = JSON_MyPhotonRoomParam.GetCreatorFUID();
          myPhotonRoomParam.roomid = GlobalVars.SelectedMultiPlayRoomID;
          myPhotonRoomParam.comment = GlobalVars.SelectedMultiPlayRoomComment;
          myPhotonRoomParam.passCode = GlobalVars.EditMultiPlayRoomPassCode;
          myPhotonRoomParam.iname = GlobalVars.SelectedQuestID;
          myPhotonRoomParam.type = (int) GlobalVars.SelectedMultiPlayRoomType;
          myPhotonRoomParam.isLINE = !self.IsLINE ? 0 : 1;
          myPhotonRoomParam.vsmode = instance1.GetVSMode() != VS_MODE.THREE_ON_THREE ? 1 : 0;
          myPhotonRoomParam.draft_type = !GlobalVars.IsVersusDraftMode ? 0 : 1;
          myPhotonRoomParam.draft_deck_id = !GlobalVars.IsVersusDraftMode ? 0 : (int) MonoSingleton<GameManager>.Instance.VSDraftId;
          Gsc.App.Environment getEnvironment = Network.GetEnvironment;
          myPhotonRoomParam.btlver = getEnvironment.IsEnvironmentFlag(Gsc.App.Environment.EnvironmentFlagBit.ENV_FLG_PHOTONVERSION_OFF) ? Network.Version : MonoSingleton<GameManager>.Instance.BattleVersion;
          int plv = -1;
          int floor = -1;
          int audienceMax = (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.AudienceMax;
          string uid = (string) null;
          string luid = (string) null;
          if (GlobalVars.SelectedMultiPlayVersusType == VERSUS_TYPE.Tower)
          {
            plv = myPhotonRoomParam.creatorLV;
            floor = MonoSingleton<GameManager>.Instance.Player.VersusTowerFloor;
            uid = MonoSingleton<GameManager>.Instance.DeviceId;
            luid = MonoSingleton<GameManager>.Instance.VersusLastUid;
          }
          if (instance2.CreateRoom((int) self.VERSUS_PLAYER_MAX, GlobalVars.SelectedMultiPlayRoomName, myPhotonRoomParam.Serialize(), self.mJoinPlayerParam.Serialize(), GlobalVars.MultiPlayVersusKey, floor, plv, luid, uid, audienceMax))
            return;
          self.FailureLobby();
        }
      }

      public override void Update(FlowNode_MultiPlayJoinRoom self)
      {
        if (!((Behaviour) self).enabled)
          return;
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        switch (instance.CurrentState)
        {
          case MyPhoton.MyState.LOBBY:
            DebugUtility.Log("[PUN]create room failed, back to lobby.");
            MyPhoton.PhotonSendLog(this.GetType().Name + " Update", "[PUN]create room failed, back to lobby.");
            if (instance.LastError != MyPhoton.MyError.NOP)
            {
              DebugUtility.Log(instance.LastError.ToString());
              instance.ResetLastError();
            }
            self.Failure();
            break;
          case MyPhoton.MyState.JOINING:
            break;
          case MyPhoton.MyState.ROOM:
            if ((double) (Time.realtimeSinceStartup - FlowNode_MultiPlayAPI.RoomMakeTime) > 25.0)
            {
              self.Failure();
              DebugUtility.Log("[PUN]create room too late, give up.");
              MyPhoton.PhotonSendLog(this.GetType().Name + " Update", "[PUN]create room too late, give up.");
              break;
            }
            self.GotoState<FlowNode_MultiPlayJoinRoom.State_DecidePlayerIndex>();
            break;
          default:
            self.Failure();
            DebugUtility.Log("[PUN]create room failed, error.");
            MyPhoton.PhotonSendLog(this.GetType().Name + " Update", "[PUN]create room failed, error.");
            break;
        }
      }

      public override void End(FlowNode_MultiPlayJoinRoom self)
      {
      }
    }

    private class State_VersusJoin : State<FlowNode_MultiPlayJoinRoom>
    {
      public override void Begin(FlowNode_MultiPlayJoinRoom self)
      {
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        self.mJoinPlayerParam = JSON_MyPhotonPlayerParam.Create();
        if (self.mJoinPlayerParam == null)
        {
          self.FailureLobby();
        }
        else
        {
          if (instance.JoinRandomRoom(self.VERSUS_PLAYER_MAX, self.mJoinPlayerParam.Serialize(), GlobalVars.MultiPlayVersusKey, GlobalVars.SelectedMultiPlayRoomName, uid: self.mJoinPlayerParam.UID))
            return;
          DebugUtility.Log("error:" + (object) instance.LastError);
          self.FailureLobby();
        }
      }

      public override void Update(FlowNode_MultiPlayJoinRoom self)
      {
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        MyPhoton.MyState currentState = instance.CurrentState;
        if (!((Behaviour) self).enabled)
          return;
        switch (currentState)
        {
          case MyPhoton.MyState.LOBBY:
            DebugUtility.Log("[PUN]joining failed, back to lobby." + (object) instance.LastError);
            if (instance.LastError == MyPhoton.MyError.ROOM_IS_FULL)
            {
              self.FailureFullMember();
              break;
            }
            self.FailureLobby();
            break;
          case MyPhoton.MyState.JOINING:
            break;
          case MyPhoton.MyState.ROOM:
            GlobalVars.SelectedMultiPlayRoomName = instance.GetCurrentRoom().name;
            self.GotoState<FlowNode_MultiPlayJoinRoom.State_DecidePlayerIndex>();
            break;
          default:
            self.Failure();
            break;
        }
      }

      public override void End(FlowNode_MultiPlayJoinRoom self)
      {
      }
    }

    private class State_VersusTowerJoin : State<FlowNode_MultiPlayJoinRoom>
    {
      public override void Begin(FlowNode_MultiPlayJoinRoom self)
      {
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        self.mJoinPlayerParam = JSON_MyPhotonPlayerParam.Create();
        if (self.mJoinPlayerParam == null)
        {
          self.FailureLobby();
        }
        else
        {
          int lrange = -1;
          int frange = -1;
          int lv = MonoSingleton<GameManager>.Instance.Player.CalcLevel();
          int versusTowerFloor = MonoSingleton<GameManager>.Instance.Player.VersusTowerFloor;
          string versusLastUid = MonoSingleton<GameManager>.Instance.VersusLastUid;
          string deviceId = MonoSingleton<GameManager>.Instance.DeviceId;
          MonoSingleton<GameManager>.Instance.GetRankMatchCondition(out lrange, out frange);
          if (instance.JoinRoomCheckParam(GlobalVars.MultiPlayVersusKey, self.mJoinPlayerParam.Serialize(), lrange, frange, lv, versusTowerFloor, versusLastUid, deviceId))
            return;
          DebugUtility.Log("error:" + (object) instance.LastError);
          self.FailureLobby();
        }
      }

      public override void Update(FlowNode_MultiPlayJoinRoom self)
      {
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        MyPhoton.MyState currentState = instance.CurrentState;
        if (!((Behaviour) self).enabled)
          return;
        switch (currentState)
        {
          case MyPhoton.MyState.LOBBY:
            DebugUtility.Log("[PUN]joining failed, back to lobby." + (object) instance.LastError);
            if (instance.LastError == MyPhoton.MyError.ROOM_IS_FULL)
            {
              self.FailureFullMember();
              break;
            }
            self.FailureLobby();
            break;
          case MyPhoton.MyState.JOINING:
            break;
          case MyPhoton.MyState.ROOM:
            GlobalVars.SelectedMultiPlayRoomName = instance.GetCurrentRoom().name;
            self.GotoState<FlowNode_MultiPlayJoinRoom.State_DecidePlayerIndex>();
            break;
          default:
            self.Failure();
            break;
        }
      }

      public override void End(FlowNode_MultiPlayJoinRoom self)
      {
      }
    }

    private class State_VersusRankCreate : State<FlowNode_MultiPlayJoinRoom>
    {
      public override void Begin(FlowNode_MultiPlayJoinRoom self)
      {
        GameManager instance1 = MonoSingleton<GameManager>.Instance;
        MyPhoton instance2 = PunMonoSingleton<MyPhoton>.Instance;
        self.mJoinPlayerParam = JSON_MyPhotonPlayerParam.Create();
        if (self.mJoinPlayerParam == null)
        {
          self.FailureLobby();
        }
        else
        {
          JSON_MyPhotonRoomParam myPhotonRoomParam = new JSON_MyPhotonRoomParam();
          myPhotonRoomParam.creatorName = MonoSingleton<GameManager>.Instance.Player.Name;
          myPhotonRoomParam.creatorLV = MonoSingleton<GameManager>.Instance.Player.CalcLevel();
          myPhotonRoomParam.creatorFUID = JSON_MyPhotonRoomParam.GetCreatorFUID();
          myPhotonRoomParam.roomid = GlobalVars.SelectedMultiPlayRoomID;
          myPhotonRoomParam.comment = GlobalVars.SelectedMultiPlayRoomComment;
          myPhotonRoomParam.passCode = GlobalVars.EditMultiPlayRoomPassCode;
          myPhotonRoomParam.iname = GlobalVars.SelectedQuestID;
          myPhotonRoomParam.type = (int) GlobalVars.SelectedMultiPlayRoomType;
          myPhotonRoomParam.isLINE = !self.IsLINE ? 0 : 1;
          myPhotonRoomParam.vsmode = instance1.GetVSMode() != VS_MODE.THREE_ON_THREE ? 1 : 0;
          Gsc.App.Environment getEnvironment = Network.GetEnvironment;
          myPhotonRoomParam.btlver = getEnvironment.IsEnvironmentFlag(Gsc.App.Environment.EnvironmentFlagBit.ENV_FLG_PHOTONVERSION_OFF) ? Network.Version : MonoSingleton<GameManager>.Instance.BattleVersion;
          int creatorLv = myPhotonRoomParam.creatorLV;
          string deviceId = MonoSingleton<GameManager>.Instance.DeviceId;
          int rankMatchScore = MonoSingleton<GameManager>.Instance.Player.RankMatchScore;
          int rankMatchClass = (int) MonoSingleton<GameManager>.Instance.Player.RankMatchClass;
          if (instance2.CreateRoom(GlobalVars.SelectedMultiPlayRoomName, myPhotonRoomParam.Serialize(), self.mJoinPlayerParam.Serialize(), creatorLv, deviceId, rankMatchScore, rankMatchClass))
            return;
          self.FailureLobby();
        }
      }

      public override void Update(FlowNode_MultiPlayJoinRoom self)
      {
        if (!((Behaviour) self).enabled)
          return;
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        switch (instance.CurrentState)
        {
          case MyPhoton.MyState.LOBBY:
            DebugUtility.Log("[PUN]create room failed, back to lobby.");
            MyPhoton.PhotonSendLog(this.GetType().Name + " Update", "[PUN]create room failed, back to lobby.");
            if (instance.LastError != MyPhoton.MyError.NOP)
            {
              DebugUtility.Log(instance.LastError.ToString());
              instance.ResetLastError();
            }
            self.Failure();
            break;
          case MyPhoton.MyState.JOINING:
            break;
          case MyPhoton.MyState.ROOM:
            if ((double) (Time.realtimeSinceStartup - FlowNode_MultiPlayAPI.RoomMakeTime) > 25.0)
            {
              self.Failure();
              DebugUtility.Log("[PUN]create room too late, give up.");
              MyPhoton.PhotonSendLog(this.GetType().Name + " Update", "[PUN]create room too late, give up.");
              break;
            }
            self.GotoState<FlowNode_MultiPlayJoinRoom.State_DecidePlayerIndex>();
            break;
          default:
            self.Failure();
            DebugUtility.Log("[PUN]create room failed, error.");
            MyPhoton.PhotonSendLog(this.GetType().Name + " Update", "[PUN]create room failed, error.");
            break;
        }
      }

      public override void End(FlowNode_MultiPlayJoinRoom self)
      {
      }
    }

    private class State_VersusRankJoin : State<FlowNode_MultiPlayJoinRoom>
    {
      public override void Begin(FlowNode_MultiPlayJoinRoom self)
      {
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        self.mJoinPlayerParam = JSON_MyPhotonPlayerParam.Create();
        if (self.mJoinPlayerParam == null)
        {
          self.FailureLobby();
        }
        else
        {
          int lvRange = -1;
          int lv = MonoSingleton<GameManager>.Instance.Player.CalcLevel();
          int rankMatchScore = MonoSingleton<GameManager>.Instance.Player.RankMatchScore;
          int rankMatchClass = (int) MonoSingleton<GameManager>.Instance.Player.RankMatchClass;
          string deviceId = MonoSingleton<GameManager>.Instance.DeviceId;
          if (instance.JoinRankMatchRoomCheckParam(self.mJoinPlayerParam.Serialize(), lv, lvRange, deviceId, rankMatchScore, 900, 100, rankMatchClass, MonoSingleton<GameManager>.Instance.RankMatchMatchedEnemies))
            return;
          DebugUtility.Log("error:" + (object) instance.LastError);
          self.FailureLobby();
        }
      }

      public override void Update(FlowNode_MultiPlayJoinRoom self)
      {
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        MyPhoton.MyState currentState = instance.CurrentState;
        if (!((Behaviour) self).enabled)
          return;
        switch (currentState)
        {
          case MyPhoton.MyState.LOBBY:
            DebugUtility.Log("[PUN]joining failed, back to lobby." + (object) instance.LastError);
            if (instance.LastError == MyPhoton.MyError.ROOM_IS_FULL)
            {
              self.FailureFullMember();
              break;
            }
            self.FailureLobby();
            break;
          case MyPhoton.MyState.JOINING:
            break;
          case MyPhoton.MyState.ROOM:
            GlobalVars.SelectedMultiPlayRoomName = instance.GetCurrentRoom().name;
            self.GotoState<FlowNode_MultiPlayJoinRoom.State_DecidePlayerIndex>();
            break;
          default:
            self.Failure();
            break;
        }
      }

      public override void End(FlowNode_MultiPlayJoinRoom self)
      {
      }
    }

    private class State_TowerCreate : State<FlowNode_MultiPlayJoinRoom>
    {
      public override void Begin(FlowNode_MultiPlayJoinRoom self)
      {
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        self.mJoinPlayerParam = JSON_MyPhotonPlayerParam.Create();
        if (self.mJoinPlayerParam == null)
        {
          self.FailureLobby();
        }
        else
        {
          JSON_MyPhotonRoomParam myPhotonRoomParam = new JSON_MyPhotonRoomParam();
          myPhotonRoomParam.creatorName = MonoSingleton<GameManager>.Instance.Player.Name;
          myPhotonRoomParam.creatorLV = MonoSingleton<GameManager>.Instance.Player.CalcLevel();
          myPhotonRoomParam.creatorFUID = JSON_MyPhotonRoomParam.GetCreatorFUID();
          myPhotonRoomParam.roomid = GlobalVars.SelectedMultiPlayRoomID;
          myPhotonRoomParam.comment = GlobalVars.SelectedMultiPlayRoomComment;
          myPhotonRoomParam.passCode = GlobalVars.EditMultiPlayRoomPassCode;
          myPhotonRoomParam.btlSpd = GlobalVars.SelectedMultiPlayBtlSpeed;
          myPhotonRoomParam.iname = GlobalVars.SelectedQuestID;
          myPhotonRoomParam.type = (int) GlobalVars.SelectedMultiPlayRoomType;
          myPhotonRoomParam.isLINE = !self.IsLINE ? 0 : 1;
          Gsc.App.Environment getEnvironment = Network.GetEnvironment;
          myPhotonRoomParam.btlver = getEnvironment.IsEnvironmentFlag(Gsc.App.Environment.EnvironmentFlagBit.ENV_FLG_PHOTONVERSION_OFF) ? Network.Version : MonoSingleton<GameManager>.Instance.BattleVersion;
          QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
          MultiTowerFloorParam mtFloorParam = MonoSingleton<GameManager>.Instance.GetMTFloorParam(quest.iname);
          int selectedMultiTowerFloor = GlobalVars.SelectedMultiTowerFloor;
          string deviceId = MonoSingleton<GameManager>.Instance.DeviceId;
          if (instance.CreateRoom((int) quest.playerNum, GlobalVars.SelectedMultiPlayRoomName, myPhotonRoomParam.Serialize(), self.mJoinPlayerParam.Serialize(), mtFloorParam.tower_id, selectedMultiTowerFloor, uid: deviceId, isTower: true))
            return;
          self.FailureLobby();
        }
      }

      public override void Update(FlowNode_MultiPlayJoinRoom self)
      {
        if (!((Behaviour) self).enabled)
          return;
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        switch (instance.CurrentState)
        {
          case MyPhoton.MyState.LOBBY:
            DebugUtility.Log("[PUN]create room failed, back to lobby.");
            MyPhoton.PhotonSendLog(this.GetType().Name + " Update", "[PUN]create room failed, back to lobby.");
            if (instance.LastError != MyPhoton.MyError.NOP)
            {
              DebugUtility.Log(instance.LastError.ToString());
              instance.ResetLastError();
            }
            self.Failure();
            break;
          case MyPhoton.MyState.JOINING:
            break;
          case MyPhoton.MyState.ROOM:
            if ((double) (Time.realtimeSinceStartup - FlowNode_MultiPlayAPI.RoomMakeTime) > 25.0)
            {
              self.Failure();
              DebugUtility.Log("[PUN]create room too late, give up.");
              MyPhoton.PhotonSendLog(this.GetType().Name + " Update", "[PUN]create room too late, give up.");
              break;
            }
            self.GotoState<FlowNode_MultiPlayJoinRoom.State_DecidePlayerIndex>();
            break;
          default:
            self.Failure();
            DebugUtility.Log("[PUN]create room failed, error.");
            MyPhoton.PhotonSendLog(this.GetType().Name + " Update", "[PUN]create room failed, error.");
            break;
        }
      }

      public override void End(FlowNode_MultiPlayJoinRoom self)
      {
      }
    }

    private class State_TowerJoin : State<FlowNode_MultiPlayJoinRoom>
    {
      public override void Begin(FlowNode_MultiPlayJoinRoom self)
      {
        MyPhoton instance1 = PunMonoSingleton<MyPhoton>.Instance;
        self.mJoinPlayerParam = JSON_MyPhotonPlayerParam.Create();
        if (self.mJoinPlayerParam == null)
        {
          self.FailureLobby();
        }
        else
        {
          GameManager instance2 = MonoSingleton<GameManager>.Instance;
          QuestParam quest = instance2.FindQuest(GlobalVars.SelectedQuestID);
          MultiTowerFloorParam mtFloorParam = instance2.GetMTFloorParam(GlobalVars.SelectedQuestID);
          if (instance1.JoinRandomRoom((byte) (short) quest.playerNum, self.mJoinPlayerParam.Serialize(), mtFloorParam.tower_id, floor: GlobalVars.SelectedMultiTowerFloor, pass: 1, uid: self.mJoinPlayerParam.UID))
            return;
          DebugUtility.Log("error:" + (object) instance1.LastError);
          self.FailureLobby();
        }
      }

      public override void Update(FlowNode_MultiPlayJoinRoom self)
      {
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        MyPhoton.MyState currentState = instance.CurrentState;
        if (!((Behaviour) self).enabled)
          return;
        switch (currentState)
        {
          case MyPhoton.MyState.LOBBY:
            DebugUtility.Log("[PUN]joining failed, back to lobby." + (object) instance.LastError);
            if (instance.LastError == MyPhoton.MyError.ROOM_IS_FULL)
            {
              self.FailureFullMember();
              break;
            }
            self.FailureLobby();
            break;
          case MyPhoton.MyState.JOINING:
            break;
          case MyPhoton.MyState.ROOM:
            GlobalVars.SelectedMultiPlayRoomName = instance.GetCurrentRoom().name;
            self.GotoState<FlowNode_MultiPlayJoinRoom.State_DecidePlayerIndex>();
            break;
          default:
            self.Failure();
            break;
        }
      }

      public override void End(FlowNode_MultiPlayJoinRoom self)
      {
      }
    }

    private class State_ResumeTowerJoinRoom : State<FlowNode_MultiPlayJoinRoom>
    {
      public override void Begin(FlowNode_MultiPlayJoinRoom self)
      {
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        if (string.IsNullOrEmpty(GlobalVars.SelectedMultiPlayRoomName))
        {
          Debug.Log((object) ("MyState:" + instance.CurrentState.ToString() + "error:" + (object) instance.LastError));
          Debug.Log((object) ("SelectedMultiPlayPhotonAppID:" + GlobalVars.SelectedMultiPlayPhotonAppID));
          Debug.LogError((object) "SelectedMultiPlayRoomName Empty");
          self.FailureLobby();
        }
        else
        {
          QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
          if (quest == null || !quest.IsMulti || (int) quest.playerNum < 1 || (int) quest.unitNum < 1 || (int) quest.unitNum > 17 || quest.map == null || quest.map.Count <= 0)
          {
            Debug.Log((object) ("MyState:" + instance.CurrentState.ToString() + "error:" + (object) instance.LastError));
            Debug.Log((object) ("SelectedMultiPlayPhotonAppID:" + GlobalVars.SelectedMultiPlayPhotonAppID));
            Debug.LogError((object) ("illegal iname:" + GlobalVars.SelectedQuestID));
            self.IllegalQuest();
          }
          else if (!instance.CheckTowerRoomIsBattle(GlobalVars.SelectedMultiPlayRoomName))
          {
            Debug.Log((object) ("MyState:" + instance.CurrentState.ToString() + "error:" + (object) instance.LastError));
            Debug.Log((object) ("SelectedMultiPlayPhotonAppID:" + GlobalVars.SelectedMultiPlayPhotonAppID));
            Debug.LogError((object) "CheckTowerRoomIsBattle flase");
            self.FailureLobby();
          }
          else
          {
            self.mJoinPlayerParam = JSON_MyPhotonPlayerParam.Create();
            if (self.mJoinPlayerParam == null)
            {
              Debug.Log((object) ("MyState:" + instance.CurrentState.ToString() + "error:" + (object) instance.LastError));
              Debug.Log((object) ("SelectedMultiPlayPhotonAppID:" + GlobalVars.SelectedMultiPlayPhotonAppID));
              Debug.LogError((object) "JoinPlayerParam null");
              self.FailureLobby();
            }
            else
            {
              DebugUtility.Log("Joining name:" + GlobalVars.SelectedMultiPlayRoomName + " pnum:" + (object) quest.playerNum + " unum:" + (object) quest.unitNum);
              if (instance.JoinRoom(GlobalVars.SelectedMultiPlayRoomName, self.mJoinPlayerParam.Serialize(), GlobalVars.ResumeMultiplayPlayerID != 0))
                return;
              Debug.Log((object) ("Joining name:" + GlobalVars.SelectedMultiPlayRoomName + " pnum:" + (object) quest.playerNum + " unum:" + (object) quest.unitNum));
              Debug.Log((object) ("MyState:" + instance.CurrentState.ToString() + "error:" + (object) instance.LastError));
              Debug.Log((object) ("SelectedMultiPlayPhotonAppID:" + GlobalVars.SelectedMultiPlayPhotonAppID));
              Debug.LogError((object) "JoinPlayerParam null");
              self.FailureLobby();
            }
          }
        }
      }

      public override void Update(FlowNode_MultiPlayJoinRoom self)
      {
        if (!((Behaviour) self).enabled)
          return;
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        switch (instance.CurrentState)
        {
          case MyPhoton.MyState.LOBBY:
            Debug.Log((object) ("[PUN]joining failed, back to lobby." + (object) instance.LastError));
            if (instance.LastError == MyPhoton.MyError.ROOM_IS_FULL)
            {
              Debug.Log((object) ("MyState:" + instance.CurrentState.ToString() + "error:" + (object) instance.LastError));
              Debug.LogError((object) "FailureFullMember");
              self.FailureFullMember();
              break;
            }
            Debug.Log((object) ("MyState:" + instance.CurrentState.ToString() + "error:" + (object) instance.LastError));
            Debug.LogError((object) "Network Error");
            self.FailureLobby();
            break;
          case MyPhoton.MyState.JOINING:
            break;
          case MyPhoton.MyState.ROOM:
            self.GotoState<FlowNode_MultiPlayJoinRoom.State_DecidePlayerIndex>();
            break;
          default:
            self.Failure();
            DebugUtility.Log("[PUN]joining failed, error.");
            Debug.Log((object) ("MyState:" + instance.CurrentState.ToString() + "error:" + (object) instance.LastError));
            Debug.LogError((object) "Network Error");
            break;
        }
      }

      public override void End(FlowNode_MultiPlayJoinRoom self)
      {
      }
    }
  }
}
