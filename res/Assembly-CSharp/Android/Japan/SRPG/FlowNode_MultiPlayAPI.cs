// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiPlayAPI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using Gsc.App;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("Multi/MultiPlayAPI", 32741)]
  [FlowNode.Pin(0, "RoomMake", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(2, "RoomList", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(3, "RoomJoin", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(4, "RoomMakeLINE", FlowNode.PinTypes.Input, 4)]
  [FlowNode.Pin(5, "RoomLINE", FlowNode.PinTypes.Input, 5)]
  [FlowNode.Pin(6, "RoomJoinLINE", FlowNode.PinTypes.Input, 6)]
  [FlowNode.Pin(7, "RoomUpdate", FlowNode.PinTypes.Input, 7)]
  [FlowNode.Pin(8, "RoomJoinID", FlowNode.PinTypes.Input, 8)]
  [FlowNode.Pin(9, "CheckVersion", FlowNode.PinTypes.Input, 9)]
  [FlowNode.Pin(21, "MultiTowerAutoCreate", FlowNode.PinTypes.Input, 21)]
  [FlowNode.Pin(22, "MultiTowerRoomMake", FlowNode.PinTypes.Input, 22)]
  [FlowNode.Pin(23, "MultiTowerRoomList", FlowNode.PinTypes.Input, 23)]
  [FlowNode.Pin(24, "MultiTowerRoomJoin", FlowNode.PinTypes.Input, 24)]
  [FlowNode.Pin(25, "MultiTowerRoomJoinID", FlowNode.PinTypes.Input, 25)]
  [FlowNode.Pin(26, "MultiTowerRoomUpdate", FlowNode.PinTypes.Input, 26)]
  [FlowNode.Pin(27, "MultiTowerStatus", FlowNode.PinTypes.Input, 27)]
  [FlowNode.Pin(28, "MultiTowerJoinInvitation", FlowNode.PinTypes.Input, 28)]
  [FlowNode.Pin(29, "MultiTowerInRoom", FlowNode.PinTypes.Input, 29)]
  [FlowNode.Pin(30, "MultiTowerRoomJoinList", FlowNode.PinTypes.Input, 30)]
  [FlowNode.Pin(31, "MultiTowerSkip", FlowNode.PinTypes.Input, 31)]
  [FlowNode.Pin(32, "MultiQuestAutoCreate", FlowNode.PinTypes.Input, 32)]
  [FlowNode.Pin(50, "RoomInvitation", FlowNode.PinTypes.Input, 50)]
  [FlowNode.Pin(51, "RoomJoinInvitation", FlowNode.PinTypes.Input, 51)]
  [FlowNode.Pin(55, "PassRelease", FlowNode.PinTypes.Input, 55)]
  [FlowNode.Pin(56, "PassLock", FlowNode.PinTypes.Input, 56)]
  [FlowNode.Pin(60, "MultiRoomWithGPS", FlowNode.PinTypes.Input, 60)]
  [FlowNode.Pin(72, "RankMatchStart", FlowNode.PinTypes.Input, 72)]
  [FlowNode.Pin(70, "RankMatchStatus", FlowNode.PinTypes.Input, 70)]
  [FlowNode.Pin(71, "RankMatchCreateRoom", FlowNode.PinTypes.Input, 71)]
  [FlowNode.Pin(100, "Success", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "Failure", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(4800, "FailedMakeRoom", FlowNode.PinTypes.Output, 4800)]
  [FlowNode.Pin(4801, "IllegalComment", FlowNode.PinTypes.Output, 4801)]
  [FlowNode.Pin(4802, "OutOfDateQuest", FlowNode.PinTypes.Output, 4802)]
  [FlowNode.Pin(4803, "CanNotSkipFloor", FlowNode.PinTypes.Output, 4803)]
  [FlowNode.Pin(4804, "QuestArchiveNotOpened", FlowNode.PinTypes.Output, 4804)]
  [FlowNode.Pin(4900, "NoRoom", FlowNode.PinTypes.Output, 4900)]
  [FlowNode.Pin(4902, "RepelledBlockList", FlowNode.PinTypes.Output, 4902)]
  [FlowNode.Pin(5000, "NoMatchVersion", FlowNode.PinTypes.Output, 5000)]
  [FlowNode.Pin(6000, "MultiMaintenance", FlowNode.PinTypes.Output, 6000)]
  [FlowNode.Pin(7000, "VersusNotLineRoom", FlowNode.PinTypes.Output, 7000)]
  [FlowNode.Pin(8000, "VersusFailRoomID", FlowNode.PinTypes.Output, 8000)]
  [FlowNode.Pin(9000, "VersusNotPhotonID", FlowNode.PinTypes.Output, 9000)]
  [FlowNode.Pin(10000, "VersusFaildSeasonGift", FlowNode.PinTypes.Output, 10000)]
  [FlowNode.Pin(11000, "NotChallengeFloor", FlowNode.PinTypes.Output, 11000)]
  public class FlowNode_MultiPlayAPI : FlowNode_Network
  {
    public static readonly int ROOMID_VALIDATE_MAX = 99999;
    private readonly int MULTI_TOWER_UNIT_MAX = 6;
    private const int PIN_IN_MULTI_ROOM_MAKE = 0;
    private const int PIN_IN_MULTI_ROOM_LIST = 2;
    private const int PIN_IN_MULTI_ROOM_JOIN = 3;
    private const int PIN_IN_MULTI_ROOM_MAKE_LINE = 4;
    private const int PIN_IN_MULTI_ROOM_LINE = 5;
    private const int PIN_IN_MULTI_ROOM_JOIN_LINE = 6;
    private const int PIN_IN_MULTI_ROOM_UPDATE = 7;
    private const int PIN_IN_MULTI_ROOM_JOIN_ID = 8;
    private const int PIN_IN_MULTI_CHECK_VERSION = 9;
    private const int PIN_IN_MULTI_TOWER_AUTO_CREATE = 21;
    private const int PIN_IN_MULTI_TOWER_ROOM_MAKE = 22;
    private const int PIN_IN_MULTI_TOWER_ROOM_LIST = 23;
    private const int PIN_IN_MULTI_TOWER_ROOM_JOIN = 24;
    private const int PIN_IN_MULTI_TOWER_ROOM_JOIN_ID = 25;
    private const int PIN_IN_MULTI_TOWER_ROOM_UPDATE = 26;
    private const int PIN_IN_MULTI_TOWER_STATUS = 27;
    private const int PIN_IN_MULTI_TOWER_JOIN_INVITATION = 28;
    private const int PIN_IN_MULTI_TOWER_IN_ROOM = 29;
    private const int PIN_IN_MULTI_TOWER_ROOM_JOIN_LIST = 30;
    private const int PIN_IN_MULTI_TOWER_SKIP = 31;
    private const int PIN_IN_MULTI_QUEST_AUTO_CREATE = 32;
    private const int PIN_IN_ROOM_INVITATION = 50;
    private const int PIN_IN_ROOM_JOIN_INVITATION = 51;
    private const int PIN_IN_PASS_RELEASE = 55;
    private const int PIN_IN_PASS_LOCK = 56;
    private const int PIN_IN_MULTI_ROOM_WITH_GPS = 60;
    private const int PIN_IN_RANKMATCH_START = 72;
    private const int PIN_IN_RANKMATCH_STATUS = 70;
    private const int PIN_IN_RANKMATCH_CREATE_ROOM = 71;
    private const int PIN_OUT_SUCCESS = 100;
    private const int PIN_OUT_FAILURE = 101;
    private const int PIN_OUT_FAILED_MAKE_ROOM = 4800;
    private const int PIN_OUT_ILLEGAL_COMMENT = 4801;
    private const int PIN_OUT_OUT_OF_DATE_QUEST = 4802;
    private const int PIN_OUT_CAN_NOT_SKIL_FLOOR = 4803;
    private const int PIN_OUT_QUEST_ARCHIVE_NOT_OPENED = 4804;
    private const int PIN_OUT_NO_ROOM = 4900;
    private const int PIN_OUT_REPELLED_BLOCK_LIST = 4902;
    private const int PIN_OUT_NO_MATCH_VERSION = 5000;
    private const int PIN_OUT_MULTI_MAINTENANCE = 6000;
    private const int PIN_OUT_VERSUS_NOT_LINE_ROOM = 7000;
    private const int PIN_OUT_VERSUS_FAIL_ROOM_ID = 8000;
    private const int PIN_OUT_VERSUS_NOT_PHOTON_ID = 9000;
    private const int PIN_OUT_VERSUS_FAILD_SEASON_GIFT = 10000;
    private const int PIN_OUT_NOT_CHALLENGE_FLOOR = 11000;
    public static float RoomMakeTime;
    public static ReqMPRoom.Response RoomList;
    public static readonly int ROOMID_VALIDATE_MIN;

    private FlowNode_MultiPlayAPI.EAPI API { get; set; }

    private void ResetPlacementParam()
    {
      for (int index = 0; index < this.MULTI_TOWER_UNIT_MAX; ++index)
        PlayerPrefsUtility.SetInt(PlayerPrefsUtility.MULTITW_ID_KEY + (object) index, index, false);
      PlayerPrefsUtility.Save();
    }

    private bool IsMultiAreaQuest()
    {
      bool flag = false;
      if (!string.IsNullOrEmpty(GlobalVars.SelectedQuestID))
      {
        QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
        if (quest != null)
          flag = quest.IsMultiAreaQuest;
      }
      return flag;
    }

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 0:
        case 4:
        case 22:
          this.ResetPlacementParam();
          string empty = string.Empty;
          int multiRoomCommentMax = (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.MultiRoomCommentMax;
          string str;
          if (PlayerPrefsUtility.HasKey(PlayerPrefsUtility.ROOM_COMMENT_KEY))
          {
            str = PlayerPrefsUtility.GetString(PlayerPrefsUtility.ROOM_COMMENT_KEY, string.Empty);
            if (!string.IsNullOrEmpty(str) && str.Length > multiRoomCommentMax)
              str = str.Substring(0, multiRoomCommentMax);
          }
          else
            str = LocalizedText.Get("sys.DEFAULT_ROOM_COMMENT");
          if (string.IsNullOrEmpty(str))
            str = LocalizedText.Get("sys.DEFAULT_ROOM_COMMENT");
          if (!MyMsgInput.isLegal(str))
          {
            DebugUtility.Log("comment is not legal");
            this.ActivateOutputLinks(4801);
            break;
          }
          GlobalVars.ResumeMultiplayPlayerID = 0;
          GlobalVars.ResumeMultiplaySeatID = 0;
          GlobalVars.SelectedMultiPlayRoomComment = str;
          DebugUtility.Log("MakeRoom Comment:" + GlobalVars.SelectedMultiPlayRoomComment);
          bool flag = false;
          if (pinID == 4)
          {
            GlobalVars.SelectedQuestID = FlowNode_OnUrlSchemeLaunch.LINEParam_decided.iname;
            GlobalVars.SelectedMultiPlayRoomType = FlowNode_OnUrlSchemeLaunch.LINEParam_decided.type;
            GlobalVars.EditMultiPlayRoomPassCode = "0";
            flag = true;
            DebugUtility.Log("MakeRoom for LINE Quest:" + GlobalVars.SelectedQuestID + " Type:" + (object) GlobalVars.SelectedMultiPlayRoomType + " PassCodeHash:" + GlobalVars.SelectedMultiPlayRoomPassCodeHash);
          }
          GlobalVars.EditMultiPlayRoomPassCode = "0";
          string s = FlowNode_Variable.Get("MultiPlayPasscode");
          if (!string.IsNullOrEmpty(s))
          {
            int result = 0;
            if (int.TryParse(s, out result))
              GlobalVars.EditMultiPlayRoomPassCode = result.ToString();
          }
          bool isHiSpeed = PlayerPrefsUtility.GetInt("MultiPlayHiSpeed", 0) == 1;
          GlobalVars.SelectedMultiPlayHiSpeed = isHiSpeed;
          bool isPrivate = flag;
          FlowNode_MultiPlayAPI.RoomMakeTime = Time.realtimeSinceStartup;
          bool limit = GlobalVars.SelectedMultiPlayLimit & GlobalVars.EditMultiPlayRoomPassCode == "0";
          int unitlv = isPrivate || !limit ? 0 : GlobalVars.MultiPlayJoinUnitLv;
          bool clear = !isPrivate && limit && GlobalVars.MultiPlayClearOnly;
          int selectedMultiTowerFloor1 = GlobalVars.SelectedMultiTowerFloor;
          if (Network.Mode != Network.EConnectMode.Online)
          {
            GlobalVars.SelectedMultiPlayRoomID = (int) (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds;
            GlobalVars.SelectedMultiPlayPhotonAppID = string.Empty;
            GlobalVars.SelectedMultiPlayRoomName = Guid.NewGuid().ToString();
            DebugUtility.Log("MakeRoom RoomID:" + (object) GlobalVars.SelectedMultiPlayRoomID + " AppID:" + GlobalVars.SelectedMultiPlayPhotonAppID + " Name:" + GlobalVars.SelectedMultiPlayRoomName);
            this.Success();
            break;
          }
          MultiInvitationSendWindow.ClearInvited();
          this.enabled = true;
          this.API = FlowNode_MultiPlayAPI.EAPI.MAKE;
          if (this.IsMultiAreaQuest())
          {
            Vector2 location = GlobalVars.Location;
            this.ExecRequest((WebAPI) new ReqMultiAreaRoomMake(GlobalVars.SelectedQuestID, str, GlobalVars.EditMultiPlayRoomPassCode, isPrivate, limit, unitlv, clear, location, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
            break;
          }
          if (pinID != 22)
          {
            this.ExecRequest((WebAPI) new ReqMPRoomMake(GlobalVars.SelectedQuestID, str, GlobalVars.EditMultiPlayRoomPassCode, isPrivate, isHiSpeed, limit, unitlv, clear, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
            break;
          }
          this.ExecRequest((WebAPI) new ReqMultiTwRoomMake(GlobalVars.SelectedMultiTowerID, str, GlobalVars.EditMultiPlayRoomPassCode, selectedMultiTowerFloor1, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          break;
        case 2:
        case 5:
        case 23:
        case 50:
        case 60:
          string fuid = (string) null;
          if (pinID == 5)
            fuid = FlowNode_OnUrlSchemeLaunch.LINEParam_decided.creatorFUID;
          else if (pinID == 50)
            fuid = GlobalVars.MultiInvitationRoomOwner;
          DebugUtility.Log("ListRoom FUID:" + fuid);
          if (Network.Mode != Network.EConnectMode.Online)
          {
            this.StartCoroutine(this.GetPhotonRoomList(fuid));
            break;
          }
          FlowNode_MultiPlayAPI.RoomList = (ReqMPRoom.Response) null;
          string iname = string.Empty;
          if (pinID == 2)
            iname = GlobalVars.SelectedQuestID;
          int selectedMultiTowerFloor2 = GlobalVars.SelectedMultiTowerFloor;
          this.enabled = true;
          this.API = FlowNode_MultiPlayAPI.EAPI.ROOM;
          if (pinID == 60 || this.IsMultiAreaQuest())
          {
            GameManager instance = MonoSingleton<GameManager>.Instance;
            Vector2 location = GlobalVars.Location;
            List<string> stringList = new List<string>();
            if (!string.IsNullOrEmpty(iname))
              stringList.Add(iname);
            else
              stringList = instance.GetMultiAreaQuestList();
            this.ExecRequest((WebAPI) new ReqMultiAreaRoom(fuid, stringList.ToArray(), location, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
            break;
          }
          if (pinID != 23)
          {
            this.ExecRequest((WebAPI) new ReqMPRoom(fuid, iname, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
            break;
          }
          this.ExecRequest((WebAPI) new ReqMultiTwRoom(fuid, GlobalVars.SelectedMultiTowerID, selectedMultiTowerFloor2, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          break;
        case 3:
        case 6:
        case 24:
        case 51:
          if (FlowNode_MultiPlayAPI.RoomList == null || FlowNode_MultiPlayAPI.RoomList.rooms == null || FlowNode_MultiPlayAPI.RoomList.rooms.Length <= 0)
          {
            this.Failure();
            break;
          }
          this.ResetPlacementParam();
          bool LockRoom = false;
          switch (pinID)
          {
            case 6:
              if (FlowNode_MultiPlayAPI.RoomList.rooms.Length != 1 || FlowNode_MultiPlayAPI.RoomList.rooms[0] == null)
              {
                this.Failure();
                return;
              }
              GlobalVars.SelectedMultiPlayRoomID = FlowNode_MultiPlayAPI.RoomList.rooms[0].roomid;
              DebugUtility.Log("JoinRoom for LINE RoomID:" + (object) GlobalVars.SelectedMultiPlayRoomID);
              break;
            case 51:
              if (FlowNode_MultiPlayAPI.RoomList.rooms.Length != 1 || FlowNode_MultiPlayAPI.RoomList.rooms[0] == null)
              {
                this.Failure();
                return;
              }
              LockRoom = GlobalVars.MultiInvitationRoomLocked;
              GlobalVars.SelectedMultiPlayRoomID = FlowNode_MultiPlayAPI.RoomList.rooms[0].roomid;
              DebugUtility.Log("JoinRoom for Invitation RoomID:" + (object) GlobalVars.SelectedMultiPlayRoomID);
              break;
          }
          GlobalVars.ResumeMultiplayPlayerID = 0;
          GlobalVars.ResumeMultiplaySeatID = 0;
          GlobalVars.SelectedQuestID = (string) null;
          for (int index = 0; index < FlowNode_MultiPlayAPI.RoomList.rooms.Length; ++index)
          {
            if (FlowNode_MultiPlayAPI.RoomList.rooms[index].quest != null && !string.IsNullOrEmpty(FlowNode_MultiPlayAPI.RoomList.rooms[index].quest.iname) && FlowNode_MultiPlayAPI.RoomList.rooms[index].roomid == GlobalVars.SelectedMultiPlayRoomID)
            {
              GlobalVars.SelectedQuestID = FlowNode_MultiPlayAPI.RoomList.rooms[index].quest.iname;
              break;
            }
          }
          if (string.IsNullOrEmpty(GlobalVars.SelectedQuestID))
          {
            this.Failure();
            break;
          }
          if (Network.Mode != Network.EConnectMode.Online)
          {
            this.StartCoroutine(this.GetPhotonRoomName());
            break;
          }
          int selectedMultiTowerFloor3 = GlobalVars.SelectedMultiTowerFloor;
          this.enabled = true;
          this.API = FlowNode_MultiPlayAPI.EAPI.JOIN;
          if (pinID != 24)
          {
            this.ExecRequest((WebAPI) new ReqMPRoomJoin(GlobalVars.SelectedMultiPlayRoomID, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), LockRoom));
            break;
          }
          this.ExecRequest((WebAPI) new ReqMultiTwRoomJoin(GlobalVars.SelectedMultiPlayRoomID, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), LockRoom, selectedMultiTowerFloor3, false));
          break;
        case 7:
        case 26:
        case 55:
        case 56:
          if (Network.Mode != Network.EConnectMode.Online)
          {
            this.Success();
            break;
          }
          if (!string.IsNullOrEmpty(GlobalVars.EditMultiPlayRoomComment) && !MyMsgInput.isLegal(GlobalVars.EditMultiPlayRoomComment))
          {
            DebugUtility.Log("comment is not legal");
            this.ActivateOutputLinks(4801);
            break;
          }
          if (pinID == 26 && string.IsNullOrEmpty(GlobalVars.EditMultiPlayRoomComment))
            GlobalVars.EditMultiPlayRoomComment = GlobalVars.SelectedMultiPlayRoomComment;
          switch (pinID)
          {
            case 55:
              GlobalVars.EditMultiPlayRoomPassCode = "0";
              GlobalVars.EditMultiPlayRoomComment = GlobalVars.SelectedMultiPlayRoomComment;
              break;
            case 56:
              GlobalVars.EditMultiPlayRoomPassCode = "1";
              GlobalVars.EditMultiPlayRoomComment = GlobalVars.SelectedMultiPlayRoomComment;
              break;
          }
          this.enabled = true;
          this.API = FlowNode_MultiPlayAPI.EAPI.UPDATE;
          if (GlobalVars.SelectedMultiPlayRoomType != JSON_MyPhotonRoomParam.EType.TOWER)
          {
            this.ExecRequest((WebAPI) new ReqMPRoomUpdate(GlobalVars.SelectedMultiPlayRoomID, GlobalVars.EditMultiPlayRoomComment, GlobalVars.EditMultiPlayRoomPassCode, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
            break;
          }
          string selectedMultiTowerId = GlobalVars.SelectedMultiTowerID;
          int selectedMultiTowerFloor4 = GlobalVars.SelectedMultiTowerFloor;
          this.ExecRequest((WebAPI) new ReqMultiTwRoomUpdate(GlobalVars.SelectedMultiPlayRoomID, GlobalVars.EditMultiPlayRoomComment, GlobalVars.EditMultiPlayRoomPassCode, selectedMultiTowerId, selectedMultiTowerFloor4, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          break;
        case 8:
        case 25:
        case 28:
        case 30:
          if (Network.Mode != Network.EConnectMode.Online)
          {
            this.Failure();
            break;
          }
          if (GlobalVars.SelectedMultiPlayRoomID < FlowNode_MultiPlayAPI.ROOMID_VALIDATE_MIN || GlobalVars.SelectedMultiPlayRoomID > FlowNode_MultiPlayAPI.ROOMID_VALIDATE_MAX)
          {
            this.Failure();
            break;
          }
          this.ResetPlacementParam();
          GlobalVars.ResumeMultiplayPlayerID = 0;
          GlobalVars.ResumeMultiplaySeatID = 0;
          this.enabled = true;
          this.API = FlowNode_MultiPlayAPI.EAPI.JOIN;
          if (pinID != 25 && pinID != 28 && pinID != 30)
          {
            this.ExecRequest((WebAPI) new ReqMPRoomJoin(GlobalVars.SelectedMultiPlayRoomID, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), true));
            break;
          }
          if (pinID == 30)
          {
            this.API = FlowNode_MultiPlayAPI.EAPI.MT_JOIN;
            this.ExecRequest((WebAPI) new ReqMultiTwRoomJoin(GlobalVars.SelectedMultiPlayRoomID, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), false, 0, false));
            break;
          }
          this.API = FlowNode_MultiPlayAPI.EAPI.MT_JOIN;
          this.ExecRequest((WebAPI) new ReqMultiTwRoomJoin(GlobalVars.SelectedMultiPlayRoomID, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), true, 0, pinID == 28));
          break;
        case 9:
          if (Network.Mode != Network.EConnectMode.Online)
          {
            this.Success();
            break;
          }
          this.enabled = true;
          this.API = FlowNode_MultiPlayAPI.EAPI.VERSION;
          this.ExecRequest((WebAPI) new ReqMPVersion(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          break;
        case 21:
          GameManager instance1 = MonoSingleton<GameManager>.Instance;
          MyPhoton instance2 = PunMonoSingleton<MyPhoton>.Instance;
          if ((UnityEngine.Object) instance2 != (UnityEngine.Object) null && instance2.IsConnectedInRoom())
          {
            List<MyPhoton.MyPlayer> roomPlayerList = instance2.GetRoomPlayerList();
            if (roomPlayerList.Count > 1)
            {
              int index = !instance2.IsHost() ? 0 : 1;
              JSON_MyPhotonPlayerParam target_player = JSON_MyPhotonPlayerParam.Parse(roomPlayerList[index].json);
              if (target_player != null && GlobalVars.BlockList != null && (GlobalVars.BlockList.Count > 0 && GlobalVars.BlockList.FindIndex((Predicate<string>) (uid => uid == target_player.UID)) != -1))
              {
                if ((UnityEngine.Object) this == (UnityEngine.Object) null)
                  break;
                this.enabled = false;
                this.ActivateOutputLinks(4902);
                break;
              }
            }
            GlobalVars.CreateAutoMultiTower = true;
            if (instance2.IsCreatedRoom())
            {
              instance2.OpenRoom(true, false);
              MyPhoton.MyRoom currentRoom = instance2.GetCurrentRoom();
              if (currentRoom != null)
              {
                JSON_MyPhotonRoomParam myPhotonRoomParam = JSON_MyPhotonRoomParam.Parse(currentRoom.json);
                if (myPhotonRoomParam != null)
                {
                  List<MultiTowerFloorParam> mtAllFloorParam = instance1.GetMTAllFloorParam(GlobalVars.SelectedMultiTowerID);
                  if (mtAllFloorParam != null)
                    GlobalVars.SelectedMultiTowerFloor = Mathf.Min(mtAllFloorParam.Count, GlobalVars.SelectedMultiTowerFloor + 1);
                  myPhotonRoomParam.challegedMTFloor = GlobalVars.SelectedMultiTowerFloor;
                  myPhotonRoomParam.iname = GlobalVars.SelectedMultiTowerID + "_" + myPhotonRoomParam.challegedMTFloor.ToString();
                  instance2.SetRoomParam(myPhotonRoomParam.Serialize());
                  if (MultiPlayAPIRoom.IsLocked(myPhotonRoomParam.passCode))
                    GlobalVars.EditMultiPlayRoomPassCode = "1";
                }
              }
            }
            instance2.AddMyPlayerParam("BattleStart", (object) false);
            instance2.AddMyPlayerParam("resume", (object) false);
            MyPhoton.MyPlayer myPlayer = instance2.GetMyPlayer();
            if (myPlayer != null)
            {
              JSON_MyPhotonPlayerParam photonPlayerParam = JSON_MyPhotonPlayerParam.Parse(myPlayer.json);
              if (photonPlayerParam != null)
              {
                photonPlayerParam.mtChallengeFloor = instance1.GetMTChallengeFloor();
                photonPlayerParam.mtClearedFloor = instance1.GetMTClearedMaxFloor();
                instance2.SetMyPlayerParam(photonPlayerParam.Serialize());
              }
            }
            this.Success();
            break;
          }
          this.Failure();
          break;
        case 27:
          if (Network.Mode != Network.EConnectMode.Online)
          {
            this.Failure();
            break;
          }
          this.enabled = true;
          this.API = FlowNode_MultiPlayAPI.EAPI.MT_STATUS;
          this.ExecRequest((WebAPI) new ReqMultiTwStatus(GlobalVars.SelectedMultiTowerID, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          break;
        case 29:
          MyPhoton instance3 = PunMonoSingleton<MyPhoton>.Instance;
          if ((UnityEngine.Object) instance3 != (UnityEngine.Object) null && instance3.IsConnectedInRoom())
          {
            this.Success();
            break;
          }
          this.Failure();
          break;
        case 31:
          if (Network.Mode != Network.EConnectMode.Online)
          {
            this.Failure();
            break;
          }
          MultiTowerSkipFloorSel instance4 = MultiTowerSkipFloorSel.Instance;
          int skip_floor = 0;
          if ((bool) ((UnityEngine.Object) instance4))
            skip_floor = instance4.SelectFloor;
          if (skip_floor == 0)
          {
            this.Failure();
            break;
          }
          this.enabled = true;
          this.API = FlowNode_MultiPlayAPI.EAPI.MT_SKIP;
          this.ExecRequest((WebAPI) new ReqMultiTwSkip(GlobalVars.SelectedMultiTowerID, skip_floor, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          break;
        case 32:
          GameManager instance5 = MonoSingleton<GameManager>.Instance;
          MyPhoton instance6 = PunMonoSingleton<MyPhoton>.Instance;
          if ((UnityEngine.Object) instance6 != (UnityEngine.Object) null && instance6.IsConnectedInRoom())
          {
            List<MyPhoton.MyPlayer> roomPlayerList = instance6.GetRoomPlayerList();
            if (roomPlayerList.Count > 1)
            {
              int index = !instance6.IsHost() ? 0 : 1;
              JSON_MyPhotonPlayerParam target_player = JSON_MyPhotonPlayerParam.Parse(roomPlayerList[index].json);
              if (target_player != null && GlobalVars.BlockList != null && (GlobalVars.BlockList.Count > 0 && GlobalVars.BlockList.FindIndex((Predicate<string>) (uid => uid == target_player.UID)) != -1))
              {
                if ((UnityEngine.Object) this == (UnityEngine.Object) null)
                  break;
                this.enabled = false;
                this.ActivateOutputLinks(4902);
                break;
              }
            }
            if (instance6.IsCreatedRoom())
            {
              instance6.OpenRoom(true, false);
              MyPhoton.MyRoom currentRoom = instance6.GetCurrentRoom();
              if (currentRoom != null)
              {
                JSON_MyPhotonRoomParam myPhotonRoomParam = JSON_MyPhotonRoomParam.Parse(currentRoom.json);
                if (myPhotonRoomParam != null)
                {
                  myPhotonRoomParam.started = 0;
                  instance6.SetRoomParam(myPhotonRoomParam.Serialize());
                  if (MultiPlayAPIRoom.IsLocked(myPhotonRoomParam.passCode))
                    GlobalVars.EditMultiPlayRoomPassCode = "1";
                }
              }
            }
            instance6.AddMyPlayerParam("BattleStart", (object) false);
            instance6.AddMyPlayerParam("resume", (object) false);
            MyPhoton.MyPlayer myPlayer = instance6.GetMyPlayer();
            if (myPlayer != null)
            {
              JSON_MyPhotonPlayerParam photonPlayerParam = JSON_MyPhotonPlayerParam.Parse(myPlayer.json);
              if (photonPlayerParam != null)
                instance6.SetMyPlayerParam(photonPlayerParam.Serialize());
            }
            this.Success();
            break;
          }
          this.Failure();
          break;
        case 70:
          if (Network.Mode != Network.EConnectMode.Online)
          {
            this.Failure();
            break;
          }
          MonoSingleton<GameManager>.Instance.IsVSCpuBattle = false;
          this.enabled = true;
          this.API = FlowNode_MultiPlayAPI.EAPI.RANKMATCH_STATUS;
          this.ExecRequest((WebAPI) new ReqRankMatchStatus(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          break;
        case 71:
          if (Network.Mode != Network.EConnectMode.Online)
          {
            this.Success();
            break;
          }
          FlowNode_MultiPlayAPI.RoomMakeTime = Time.realtimeSinceStartup;
          this.enabled = true;
          this.API = FlowNode_MultiPlayAPI.EAPI.VERSUS_MAKE;
          this.ExecRequest((WebAPI) new ReqRankMatchMake(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          break;
        case 72:
          if (Network.Mode != Network.EConnectMode.Online)
          {
            this.Failure();
            break;
          }
          MonoSingleton<GameManager>.Instance.IsVSCpuBattle = false;
          this.enabled = true;
          this.API = FlowNode_MultiPlayAPI.EAPI.RANKMATCH_START;
          this.ExecRequest((WebAPI) new ReqRankMatchStart(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          break;
      }
    }

    private void Success()
    {
      DebugUtility.Log("MultiPlayAPI success");
      if ((UnityEngine.Object) this == (UnityEngine.Object) null)
        return;
      this.enabled = false;
      this.ActivateOutputLinks(100);
    }

    private void Failure()
    {
      DebugUtility.Log("MultiPlayAPI failure");
      if ((UnityEngine.Object) this == (UnityEngine.Object) null)
        return;
      this.enabled = false;
      this.ActivateOutputLinks(101);
    }

    [DebuggerHidden]
    private IEnumerator GetPhotonRoomList(string fuid)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_MultiPlayAPI.\u003CGetPhotonRoomList\u003Ec__Iterator0() { fuid = fuid, \u0024this = this };
    }

    [DebuggerHidden]
    private IEnumerator GetPhotonRoomName()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_MultiPlayAPI.\u003CGetPhotonRoomName\u003Ec__Iterator1() { \u0024this = this };
    }

    public override void OnSuccess(WWWResult www)
    {
      DebugUtility.Log(nameof (OnSuccess));
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.MultiMaintenance:
          case Network.EErrCode.VsMaintenance:
          case Network.EErrCode.MultiVersionMaintenance:
          case Network.EErrCode.MultiTowerMaintenance:
            Network.RemoveAPI();
            if ((UnityEngine.Object) this == (UnityEngine.Object) null)
              break;
            this.enabled = false;
            this.ActivateOutputLinks(6000);
            break;
          case Network.EErrCode.OutOfDateQuest:
            Network.RemoveAPI();
            Network.ResetError();
            if ((UnityEngine.Object) this == (UnityEngine.Object) null)
              break;
            this.enabled = false;
            this.ActivateOutputLinks(4802);
            break;
          case Network.EErrCode.MultiVersionMismatch:
          case Network.EErrCode.VS_Version:
            Network.RemoveAPI();
            Network.ResetError();
            if ((UnityEngine.Object) this == (UnityEngine.Object) null)
              break;
            this.enabled = false;
            this.ActivateOutputLinks(5000);
            break;
          case Network.EErrCode.RoomFailedMakeRoom:
            Network.RemoveAPI();
            Network.ResetError();
            if ((UnityEngine.Object) this == (UnityEngine.Object) null)
              break;
            this.enabled = false;
            this.ActivateOutputLinks(4800);
            break;
          case Network.EErrCode.RoomIllegalComment:
          case Network.EErrCode.VS_IllComment:
            if (this.API == FlowNode_MultiPlayAPI.EAPI.MAKE)
            {
              string str = LocalizedText.Get("sys.DEFAULT_ROOM_COMMENT");
              PlayerPrefsUtility.SetString(PlayerPrefsUtility.ROOM_COMMENT_KEY, str, false);
            }
            Network.RemoveAPI();
            Network.ResetError();
            if ((UnityEngine.Object) this == (UnityEngine.Object) null)
              break;
            this.enabled = false;
            this.ActivateOutputLinks(4801);
            break;
          case Network.EErrCode.RoomNoRoom:
          case Network.EErrCode.VS_NoRoom:
            Network.RemoveAPI();
            Network.ResetError();
            if ((UnityEngine.Object) this == (UnityEngine.Object) null)
              break;
            this.enabled = false;
            this.ActivateOutputLinks(4900);
            break;
          case Network.EErrCode.RepelledBlockList:
            Network.RemoveAPI();
            Network.ResetError();
            if ((UnityEngine.Object) this == (UnityEngine.Object) null)
              break;
            this.enabled = false;
            this.ActivateOutputLinks(4902);
            break;
          case Network.EErrCode.VS_NotLINERoomInfo:
            Network.RemoveAPI();
            if ((UnityEngine.Object) this == (UnityEngine.Object) null)
              break;
            this.enabled = false;
            this.ActivateOutputLinks(7000);
            break;
          case Network.EErrCode.VS_FailRoomID:
            Network.RemoveAPI();
            if ((UnityEngine.Object) this == (UnityEngine.Object) null)
              break;
            this.enabled = false;
            this.ActivateOutputLinks(8000);
            break;
          case Network.EErrCode.VS_NotPhotonAppID:
            Network.RemoveAPI();
            if ((UnityEngine.Object) this == (UnityEngine.Object) null)
              break;
            this.enabled = false;
            this.ActivateOutputLinks(9000);
            break;
          case Network.EErrCode.VS_FaildSeasonGift:
            Network.RemoveAPI();
            if ((UnityEngine.Object) this == (UnityEngine.Object) null)
              break;
            this.enabled = false;
            this.ActivateOutputLinks(10000);
            break;
          case Network.EErrCode.MT_CanNotSkipFloor:
            Network.RemoveAPI();
            Network.ResetError();
            if ((UnityEngine.Object) this == (UnityEngine.Object) null)
              break;
            this.enabled = false;
            this.ActivateOutputLinks(4803);
            break;
          case Network.EErrCode.QuestArchive_ArchiveNotOpened:
            Network.RemoveAPI();
            Network.ResetError();
            if ((UnityEngine.Object) this == (UnityEngine.Object) null)
              break;
            this.enabled = false;
            this.ActivateOutputLinks(4804);
            break;
          default:
            this.OnFailed();
            break;
        }
      }
      else
      {
        if (this.API == FlowNode_MultiPlayAPI.EAPI.MAKE)
        {
          WebAPI.JSON_BodyResponse<ReqMPRoomMake.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqMPRoomMake.Response>>(www.text);
          DebugUtility.Assert(jsonObject != null, "res == null");
          if (jsonObject.body == null)
          {
            this.OnFailed();
            return;
          }
          GlobalVars.SelectedMultiPlayRoomID = jsonObject.body.roomid;
          GlobalVars.SelectedMultiPlayPhotonAppID = jsonObject.body.app_id;
          GlobalVars.SelectedMultiPlayRoomName = jsonObject.body.token;
          DebugUtility.Log("MakeRoom RoomID:" + (object) GlobalVars.SelectedMultiPlayRoomID + " AppID:" + GlobalVars.SelectedMultiPlayPhotonAppID + " Name:" + GlobalVars.SelectedMultiPlayRoomName);
        }
        else if (this.API == FlowNode_MultiPlayAPI.EAPI.ROOM)
        {
          WebAPI.JSON_BodyResponse<ReqMPRoom.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqMPRoom.Response>>(www.text);
          DebugUtility.Assert(jsonObject != null, "res == null");
          if (jsonObject.body == null)
          {
            this.OnFailed();
            return;
          }
          FlowNode_MultiPlayAPI.RoomList = jsonObject.body;
          if (FlowNode_MultiPlayAPI.RoomList == null)
            DebugUtility.Log("ListRoom null");
          else if (FlowNode_MultiPlayAPI.RoomList.rooms == null)
            DebugUtility.Log("ListRoom rooms null");
          else
            DebugUtility.Log("ListRoom num:" + (object) FlowNode_MultiPlayAPI.RoomList.rooms.Length);
        }
        else if (this.API == FlowNode_MultiPlayAPI.EAPI.JOIN)
        {
          WebAPI.JSON_BodyResponse<ReqMPRoomJoin.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqMPRoomJoin.Response>>(www.text);
          DebugUtility.Assert(jsonObject != null, "res == null");
          if (jsonObject.body == null)
          {
            this.OnFailed();
            return;
          }
          if (jsonObject.body.quest == null || string.IsNullOrEmpty(jsonObject.body.quest.iname))
          {
            this.OnFailed();
            return;
          }
          GlobalVars.SelectedQuestID = jsonObject.body.quest.iname;
          GlobalVars.SelectedMultiPlayPhotonAppID = jsonObject.body.app_id;
          GlobalVars.SelectedMultiPlayRoomName = jsonObject.body.token;
          DebugUtility.Log("JoinRoom  AppID:" + GlobalVars.SelectedMultiPlayPhotonAppID + " Name:" + GlobalVars.SelectedMultiPlayRoomName);
        }
        else if (this.API != FlowNode_MultiPlayAPI.EAPI.UPDATE)
        {
          if (this.API == FlowNode_MultiPlayAPI.EAPI.VERSION)
          {
            WebAPI.JSON_BodyResponse<ReqMPVersion.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqMPVersion.Response>>(www.text);
            DebugUtility.Assert(jsonObject != null, "res == null");
            if (jsonObject.body == null)
            {
              this.OnFailed();
              return;
            }
            BootLoader.GetAccountManager().SetDeviceId((string) null, jsonObject.body.device_id);
          }
          else if (this.API == FlowNode_MultiPlayAPI.EAPI.VERSUS_MAKE)
          {
            WebAPI.JSON_BodyResponse<ReqVersusMake.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqVersusMake.Response>>(www.text);
            DebugUtility.Assert(jsonObject != null, "res == null");
            if (jsonObject.body == null)
            {
              this.OnFailed();
              return;
            }
            GlobalVars.SelectedMultiPlayRoomID = jsonObject.body.roomid;
            GlobalVars.SelectedMultiPlayRoomName = jsonObject.body.token;
            if (GlobalVars.SelectedMultiPlayVersusType == VERSUS_TYPE.Friend)
              GlobalVars.EditMultiPlayRoomPassCode = "1";
          }
          else if (this.API == FlowNode_MultiPlayAPI.EAPI.MT_STATUS)
          {
            WebAPI.JSON_BodyResponse<ReqMultiTwStatus.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqMultiTwStatus.Response>>(www.text);
            Debug.Log((object) www.text);
            if (jsonObject == null)
            {
              this.OnFailed();
              return;
            }
            GlobalVars.SelectedMultiPlayPhotonAppID = jsonObject.body.appid;
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body);
          }
          else if (this.API == FlowNode_MultiPlayAPI.EAPI.MT_SKIP)
          {
            WebAPI.JSON_BodyResponse<ReqMultiTwSkip.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqMultiTwSkip.Response>>(www.text);
            Debug.Log((object) www.text);
            if (jsonObject == null)
            {
              this.OnFailed();
              return;
            }
            GameManager instance = MonoSingleton<GameManager>.Instance;
            instance.Deserialize(jsonObject.body.floors);
            if (jsonObject.body.player != null && jsonObject.body.player.player != null)
              instance.Deserialize(jsonObject.body.player.player);
          }
          else if (this.API == FlowNode_MultiPlayAPI.EAPI.MT_JOIN)
          {
            WebAPI.JSON_BodyResponse<ReqMultiTwRoomJoin.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqMultiTwRoomJoin.Response>>(www.text);
            DebugUtility.Assert(jsonObject != null, "res == null");
            if (jsonObject.body == null)
            {
              this.OnFailed();
              return;
            }
            if (jsonObject.body.quest == null || string.IsNullOrEmpty(jsonObject.body.quest.iname))
            {
              this.OnFailed();
              return;
            }
            GameManager instance = MonoSingleton<GameManager>.Instance;
            if (instance.GetMTChallengeFloor() < jsonObject.body.quest.floor)
            {
              Network.RemoveAPI();
              if ((UnityEngine.Object) this == (UnityEngine.Object) null)
                return;
              this.enabled = false;
              this.ActivateOutputLinks(11000);
              return;
            }
            GlobalVars.SelectedMultiTowerID = jsonObject.body.quest.iname;
            GlobalVars.SelectedMultiTowerFloor = jsonObject.body.quest.floor;
            GlobalVars.SelectedMultiPlayPhotonAppID = jsonObject.body.app_id;
            GlobalVars.SelectedMultiPlayRoomName = jsonObject.body.token;
            MultiTowerFloorParam mtFloorParam = instance.GetMTFloorParam(GlobalVars.SelectedMultiTowerID, GlobalVars.SelectedMultiTowerFloor);
            if (mtFloorParam != null)
            {
              QuestParam questParam = mtFloorParam.GetQuestParam();
              if (questParam != null)
                GlobalVars.SelectedQuestID = questParam.iname;
            }
            DebugUtility.Log("JoinRoom  AppID:" + GlobalVars.SelectedMultiPlayPhotonAppID + " Name:" + GlobalVars.SelectedMultiPlayRoomName);
          }
          else if (this.API == FlowNode_MultiPlayAPI.EAPI.RANKMATCH_START)
          {
            WebAPI.JSON_BodyResponse<ReqRankMatchStart.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqRankMatchStart.Response>>(www.text);
            if (jsonObject == null)
            {
              this.OnFailed();
              return;
            }
            GameManager instance = MonoSingleton<GameManager>.Instance;
            PlayerData player = instance.Player;
            int _streak_win = 0;
            if (jsonObject.body.streakwin != null)
              _streak_win = jsonObject.body.streakwin.num;
            player.SetRankMatchInfo(jsonObject.body.rank, jsonObject.body.score, (RankMatchClass) jsonObject.body.type, jsonObject.body.bp, _streak_win, jsonObject.body.wincnt, jsonObject.body.losecnt);
            instance.RankMatchScheduleId = jsonObject.body.schedule_id;
            GlobalVars.SelectedMultiPlayPhotonAppID = jsonObject.body.app_id;
            GameManager gameManager = instance;
            long num1 = 0;
            instance.RankMatchNextTime = num1;
            long num2 = num1;
            gameManager.RankMatchExpiredTime = num2;
            if (jsonObject.body.enabletime != null)
            {
              instance.RankMatchExpiredTime = jsonObject.body.enabletime.expired;
              instance.RankMatchNextTime = jsonObject.body.enabletime.next;
              GlobalVars.SelectedQuestID = jsonObject.body.enabletime.iname;
              long matchExpiredTime = instance.RankMatchExpiredTime;
              long num3 = TimeManager.FromDateTime(TimeManager.ServerTime);
              instance.RankMatchBegin = num3 < matchExpiredTime;
            }
            instance.RankMatchMatchedEnemies = jsonObject.body.enemies;
            GlobalVars.IsVersusDraftMode = false;
          }
          else if (this.API == FlowNode_MultiPlayAPI.EAPI.RANKMATCH_STATUS)
          {
            WebAPI.JSON_BodyResponse<ReqRankMatchStatus.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqRankMatchStatus.Response>>(www.text);
            if (jsonObject == null)
            {
              this.OnFailed();
              return;
            }
            GameManager instance = MonoSingleton<GameManager>.Instance;
            instance.RankMatchScheduleId = jsonObject.body.schedule_id;
            instance.RankMatchRankingStatus = jsonObject.body.RankingStatus;
            GameManager gameManager = instance;
            long num1 = 0;
            instance.RankMatchNextTime = num1;
            long num2 = num1;
            gameManager.RankMatchExpiredTime = num2;
            if (jsonObject.body.enabletime != null)
            {
              instance.RankMatchExpiredTime = jsonObject.body.enabletime.expired;
              instance.RankMatchNextTime = jsonObject.body.enabletime.next;
              GlobalVars.SelectedQuestID = jsonObject.body.enabletime.iname;
              long matchExpiredTime = instance.RankMatchExpiredTime;
              long num3 = TimeManager.FromDateTime(TimeManager.ServerTime);
              instance.RankMatchBegin = num3 < matchExpiredTime;
            }
          }
        }
        Network.RemoveAPI();
        this.Success();
      }
    }

    private enum EAPI
    {
      MAKE,
      ROOM,
      JOIN,
      UPDATE,
      VERSION,
      VERSUS_MAKE,
      MT_STATUS,
      MT_SKIP,
      MT_JOIN,
      RANKMATCH_START,
      RANKMATCH_STATUS,
    }
  }
}
