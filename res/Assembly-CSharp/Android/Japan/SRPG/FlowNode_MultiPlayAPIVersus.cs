// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiPlayAPIVersus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("Multi/MultiPlayAPI/Versus", 32741)]
  [FlowNode.Pin(10, "VersusStart", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(11, "VersusCreateRoom", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(12, "VersusRoomJoinID", FlowNode.PinTypes.Input, 12)]
  [FlowNode.Pin(13, "VersusRoomUpdate", FlowNode.PinTypes.Input, 13)]
  [FlowNode.Pin(14, "VersusReset", FlowNode.PinTypes.Input, 14)]
  [FlowNode.Pin(15, "VersusLineReq", FlowNode.PinTypes.Input, 15)]
  [FlowNode.Pin(16, "VersusLineMake", FlowNode.PinTypes.Input, 16)]
  [FlowNode.Pin(17, "VersusLineJoin", FlowNode.PinTypes.Input, 17)]
  [FlowNode.Pin(18, "VersusStatus", FlowNode.PinTypes.Input, 18)]
  [FlowNode.Pin(19, "VersusRecvSeasonGift", FlowNode.PinTypes.Input, 19)]
  [FlowNode.Pin(20, "VersusFriendScore", FlowNode.PinTypes.Input, 20)]
  [FlowNode.Pin(80, "Lobby", FlowNode.PinTypes.Input, 80)]
  [FlowNode.Pin(100, "Success", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "Failure", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(4800, "FailedMakeRoom", FlowNode.PinTypes.Output, 4800)]
  [FlowNode.Pin(4900, "NoRoom", FlowNode.PinTypes.Output, 4900)]
  [FlowNode.Pin(5000, "NoMatchVersion", FlowNode.PinTypes.Output, 5000)]
  [FlowNode.Pin(6000, "MultiMaintenance", FlowNode.PinTypes.Output, 6000)]
  [FlowNode.Pin(7000, "VersusNotLineRoom", FlowNode.PinTypes.Output, 7000)]
  [FlowNode.Pin(8000, "VersusFailRoomID", FlowNode.PinTypes.Output, 8000)]
  [FlowNode.Pin(9000, "VersusNotPhotonID", FlowNode.PinTypes.Output, 9000)]
  [FlowNode.Pin(10000, "VersusFaildSeasonGift", FlowNode.PinTypes.Output, 10000)]
  public class FlowNode_MultiPlayAPIVersus : FlowNode_Network
  {
    private const int PIN_IN_VERSUS_START = 10;
    private const int PIN_IN_VERSUS_CREATE_ROOM = 11;
    private const int PIN_IN_VERSUS_ROOM_JOIN_ID = 12;
    private const int PIN_IN_VERSUS_ROOM_UPDATE = 13;
    private const int PIN_IN_VERSUS_RESET = 14;
    private const int PIN_IN_VERSUS_LINE_REQ = 15;
    private const int PIN_IN_VERSUS_LINE_MAKE = 16;
    private const int PIN_IN_VERSUS_LINE_JOIN = 17;
    private const int PIN_IN_VERSUS_STATUS = 18;
    private const int PIN_IN_VERSUS_RECV_SEASON_GIFT = 19;
    private const int PIN_IN_VERSUS_FRIEND_SCORE = 20;
    private const int PIN_IN_VERSUS_LOBBY = 80;
    private const int PIN_OUT_SUCCESS = 100;
    private const int PIN_OUT_FAILURE = 101;
    private const int PIN_OUT_FAILED_MAKE_ROOM = 4800;
    private const int PIN_OUT_NO_ROOM = 4900;
    private const int PIN_OUT_NO_MATCH_VERSION = 5000;
    private const int PIN_OUT_MULTI_MAINTENANCE = 6000;
    private const int PIN_OUT_VERSUS_NOT_LINE_ROOM = 7000;
    private const int PIN_OUT_VERSUS_FAIL_ROOM_ID = 8000;
    private const int PIN_OUT_VERSUS_NOT_PHOTON_ID = 9000;
    private const int PIN_OUT_VERSUS_FAILD_SEASON_GIFT = 10000;
    private FlowNode_MultiPlayAPIVersus.Proccess mProccess;

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 10:
          this.mProccess = (FlowNode_MultiPlayAPIVersus.Proccess) new FlowNode_MultiPlayAPIVersus.Proccess_VersusStart();
          break;
        case 11:
          this.mProccess = (FlowNode_MultiPlayAPIVersus.Proccess) new FlowNode_MultiPlayAPIVersus.Proccess_VersusCreateRoom();
          break;
        case 12:
          this.mProccess = (FlowNode_MultiPlayAPIVersus.Proccess) new FlowNode_MultiPlayAPIVersus.Proccess_VersusRoomJoinID();
          break;
        case 13:
          this.mProccess = (FlowNode_MultiPlayAPIVersus.Proccess) new FlowNode_MultiPlayAPIVersus.Proccess_VersusRoomUpdate();
          break;
        case 14:
          this.mProccess = (FlowNode_MultiPlayAPIVersus.Proccess) new FlowNode_MultiPlayAPIVersus.Proccess_VersusReset();
          break;
        case 15:
          this.mProccess = (FlowNode_MultiPlayAPIVersus.Proccess) new FlowNode_MultiPlayAPIVersus.Proccess_VersusLineReq();
          break;
        case 16:
          this.mProccess = (FlowNode_MultiPlayAPIVersus.Proccess) new FlowNode_MultiPlayAPIVersus.Proccess_VersusLineMake();
          break;
        case 17:
          this.mProccess = (FlowNode_MultiPlayAPIVersus.Proccess) new FlowNode_MultiPlayAPIVersus.Proccess_VersusLineJoin();
          break;
        case 18:
          this.mProccess = (FlowNode_MultiPlayAPIVersus.Proccess) new FlowNode_MultiPlayAPIVersus.Proccess_VersusStatus();
          break;
        case 19:
          this.mProccess = (FlowNode_MultiPlayAPIVersus.Proccess) new FlowNode_MultiPlayAPIVersus.Proccess_VersusReceiveSeasonGift();
          break;
        case 20:
          this.mProccess = (FlowNode_MultiPlayAPIVersus.Proccess) new FlowNode_MultiPlayAPIVersus.Proccess_VersusFriendScore();
          break;
        case 80:
          this.mProccess = (FlowNode_MultiPlayAPIVersus.Proccess) new FlowNode_MultiPlayAPIVersus.Proccess_VersusLobby();
          break;
      }
      if (this.mProccess == null)
        return;
      this.mProccess.SetParent(this);
      this.mProccess.Activate();
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

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.MultiMaintenance:
          case Network.EErrCode.VsMaintenance:
          case Network.EErrCode.MultiVersionMaintenance:
          case Network.EErrCode.MultiTowerMaintenance:
            Network.RemoveAPI();
            Network.ResetError();
            if ((UnityEngine.Object) this == (UnityEngine.Object) null)
              break;
            this.enabled = false;
            this.ActivateOutputLinks(6000);
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
          case Network.EErrCode.RoomNoRoom:
          case Network.EErrCode.VS_NoRoom:
            Network.RemoveAPI();
            Network.ResetError();
            if ((UnityEngine.Object) this == (UnityEngine.Object) null)
              break;
            this.enabled = false;
            this.ActivateOutputLinks(4900);
            break;
          case Network.EErrCode.VS_NotLINERoomInfo:
            Network.RemoveAPI();
            Network.ResetError();
            if ((UnityEngine.Object) this == (UnityEngine.Object) null)
              break;
            this.enabled = false;
            this.ActivateOutputLinks(7000);
            break;
          case Network.EErrCode.VS_FailRoomID:
            Network.RemoveAPI();
            Network.ResetError();
            if ((UnityEngine.Object) this == (UnityEngine.Object) null)
              break;
            this.enabled = false;
            this.ActivateOutputLinks(8000);
            break;
          case Network.EErrCode.VS_NotPhotonAppID:
            Network.RemoveAPI();
            Network.ResetError();
            if ((UnityEngine.Object) this == (UnityEngine.Object) null)
              break;
            this.enabled = false;
            this.ActivateOutputLinks(9000);
            break;
          case Network.EErrCode.VS_FaildSeasonGift:
            Network.RemoveAPI();
            Network.ResetError();
            if ((UnityEngine.Object) this == (UnityEngine.Object) null)
              break;
            this.enabled = false;
            this.ActivateOutputLinks(10000);
            break;
          default:
            this.OnFailed();
            break;
        }
      }
      else
      {
        if (this.mProccess != null && !this.mProccess.Success(www))
          return;
        Network.RemoveAPI();
        this.Success();
      }
    }

    private abstract class Proccess
    {
      protected FlowNode_MultiPlayAPIVersus mParent;

      public void SetParent(FlowNode_MultiPlayAPIVersus apiVersus)
      {
        this.mParent = apiVersus;
      }

      public abstract void Activate();

      public virtual bool Success(WWWResult www)
      {
        return true;
      }
    }

    private class Proccess_VersusLobby : FlowNode_MultiPlayAPIVersus.Proccess
    {
      public override void Activate()
      {
        this.mParent.enabled = true;
        this.mParent.ExecRequest((WebAPI) new ReqVersusLobby(new Network.ResponseCallback(((FlowNode_Network) this.mParent).ResponseCallback)));
      }

      public override bool Success(WWWResult www)
      {
        WebAPI.JSON_BodyResponse<ReqVersusLobby.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqVersusLobby.Response>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.mParent.OnFailed();
          return false;
        }
        GameManager instance = MonoSingleton<GameManager>.Instance;
        instance.RankMatchScheduleId = jsonObject.body.rankmatch_schedule_id;
        instance.RankMatchRankingStatus = jsonObject.body.RankMatchRankingStatus;
        GameManager gameManager = instance;
        long num1 = 0;
        instance.RankMatchNextTime = num1;
        long num2 = num1;
        gameManager.RankMatchExpiredTime = num2;
        if (jsonObject.body.rankmatch_enabletime != null)
        {
          instance.RankMatchExpiredTime = jsonObject.body.rankmatch_enabletime.expired;
          instance.RankMatchNextTime = jsonObject.body.rankmatch_enabletime.next;
          GlobalVars.SelectedQuestID = jsonObject.body.rankmatch_enabletime.iname;
          long matchExpiredTime = instance.RankMatchExpiredTime;
          long num3 = TimeManager.FromDateTime(TimeManager.ServerTime);
          instance.RankMatchBegin = num3 < matchExpiredTime;
        }
        instance.VSDraftType = (VersusDraftType) jsonObject.body.draft_type;
        instance.mVersusEnableId = (long) jsonObject.body.draft_schedule_id;
        return true;
      }
    }

    private class Proccess_VersusStatus : FlowNode_MultiPlayAPIVersus.Proccess
    {
      public override void Activate()
      {
        MonoSingleton<GameManager>.Instance.IsVSCpuBattle = false;
        this.mParent.enabled = true;
        this.mParent.ExecRequest((WebAPI) new ReqVersusStatus(new Network.ResponseCallback(((FlowNode_Network) this.mParent).ResponseCallback)));
      }

      public override bool Success(WWWResult www)
      {
        WebAPI.JSON_BodyResponse<ReqVersusStatus.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqVersusStatus.Response>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.mParent.OnFailed();
          return false;
        }
        GameManager instance = MonoSingleton<GameManager>.Instance;
        instance.Player.SetTowerMatchInfo(jsonObject.body.floor, jsonObject.body.key, jsonObject.body.wincnt, jsonObject.body.is_give_season_gift != 0);
        instance.VersusTowerMatchBegin = !string.IsNullOrEmpty(jsonObject.body.vstower_id);
        instance.VersusTowerMatchReceipt = jsonObject.body.is_season_gift != 0;
        instance.VersusTowerMatchName = jsonObject.body.tower_iname;
        instance.VersusTowerMatchEndAt = jsonObject.body.end_at;
        instance.VersusCoinRemainCnt = jsonObject.body.daycnt;
        instance.VersusLastUid = jsonObject.body.last_enemyuid;
        instance.IsVSFirstWinRewardRecived = jsonObject.body.is_firstwin != 0;
        GlobalVars.SelectedQuestID = jsonObject.body.vstower_id;
        if (jsonObject.body.streakwins != null)
        {
          for (int index = 0; index < jsonObject.body.streakwins.Length; ++index)
          {
            ReqVersusStatus.StreakStatus streakwin = jsonObject.body.streakwins[index];
            switch (instance.SearchVersusJudgeType(streakwin.schedule_id, -1L))
            {
              case STREAK_JUDGE.AllPriod:
                instance.VS_StreakWinCnt_NowAllPriod = streakwin.num;
                instance.VS_StreakWinCnt_BestAllPriod = streakwin.best;
                break;
              case STREAK_JUDGE.Now:
                instance.VS_StreakWinCnt_Now = streakwin.num;
                instance.VS_StreakWinCnt_Best = streakwin.best;
                break;
            }
          }
        }
        if (jsonObject.body.enabletime != null)
        {
          instance.VSFreeExpiredTime = jsonObject.body.enabletime.expired;
          instance.VSFreeNextTime = jsonObject.body.enabletime.next;
          instance.VSDraftType = (VersusDraftType) jsonObject.body.enabletime.draft_type;
          instance.mVersusEnableId = jsonObject.body.enabletime.schedule_id;
          instance.VSDraftId = jsonObject.body.enabletime.draft_id;
          instance.VSDraftQuestId = jsonObject.body.enabletime.iname;
        }
        instance.Player.UpdateVersusTowerTrophyStates(jsonObject.body.tower_iname, jsonObject.body.floor);
        return true;
      }
    }

    private class Proccess_VersusStart : FlowNode_MultiPlayAPIVersus.Proccess
    {
      public override void Activate()
      {
        GlobalVars.SelectedMultiPlayRoomName = string.Empty;
        GlobalVars.VersusRoomReuse = false;
        GlobalVars.ResumeMultiplayPlayerID = 0;
        GlobalVars.ResumeMultiplaySeatID = 0;
        GlobalVars.MultiPlayVersusKey = MonoSingleton<GameManager>.Instance.GetVersusKey(GlobalVars.SelectedMultiPlayVersusType);
        this.mParent.enabled = true;
        this.mParent.ExecRequest((WebAPI) new ReqVersusStart(new Network.ResponseCallback(((FlowNode_Network) this.mParent).ResponseCallback)));
      }

      public override bool Success(WWWResult www)
      {
        WebAPI.JSON_BodyResponse<ReqVersusStart.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqVersusStart.Response>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.mParent.OnFailed();
          return false;
        }
        GameManager instance = MonoSingleton<GameManager>.Instance;
        GlobalVars.SelectedMultiPlayPhotonAppID = jsonObject.body.app_id;
        switch (GlobalVars.SelectedMultiPlayVersusType)
        {
          case VERSUS_TYPE.Free:
            GlobalVars.SelectedQuestID = instance.VSDraftType != VersusDraftType.Draft ? jsonObject.body.maps.free : instance.VSDraftQuestId;
            break;
          case VERSUS_TYPE.Friend:
            GlobalVars.SelectedQuestID = jsonObject.body.maps.friend;
            break;
        }
        DebugUtility.Log("MakeRoom RoomID: AppID:" + GlobalVars.SelectedMultiPlayPhotonAppID + "/ QuestID:" + GlobalVars.SelectedQuestID);
        return true;
      }
    }

    private class Proccess_VersusCreateRoom : FlowNode_MultiPlayAPIVersus.Proccess
    {
      public override void Activate()
      {
        string comment = LocalizedText.Get("sys.DEFAULT_ROOM_COMMENT");
        if (GlobalVars.SelectedMultiPlayVersusType == VERSUS_TYPE.Friend && MonoSingleton<GameManager>.Instance.VSDraftType != VersusDraftType.Normal && GlobalVars.IsVersusDraftMode)
          GlobalVars.SelectedQuestID = MonoSingleton<GameManager>.Instance.VSDraftQuestId;
        FlowNode_MultiPlayAPI.RoomMakeTime = Time.realtimeSinceStartup;
        GlobalVars.SelectedMultiPlayRoomComment = comment;
        this.mParent.enabled = true;
        this.mParent.ExecRequest((WebAPI) new ReqVersusMake(GlobalVars.SelectedMultiPlayVersusType, comment, GlobalVars.SelectedQuestID, false, new Network.ResponseCallback(((FlowNode_Network) this.mParent).ResponseCallback)));
      }

      public override bool Success(WWWResult www)
      {
        WebAPI.JSON_BodyResponse<ReqVersusMake.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqVersusMake.Response>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.mParent.OnFailed();
          return false;
        }
        GlobalVars.SelectedMultiPlayRoomID = jsonObject.body.roomid;
        GlobalVars.SelectedMultiPlayRoomName = jsonObject.body.token;
        if (GlobalVars.SelectedMultiPlayVersusType == VERSUS_TYPE.Friend)
          GlobalVars.EditMultiPlayRoomPassCode = "1";
        return true;
      }
    }

    private class Proccess_VersusRoomJoinID : FlowNode_MultiPlayAPIVersus.Proccess
    {
      public override void Activate()
      {
        if (GlobalVars.SelectedMultiPlayRoomID < FlowNode_MultiPlayAPI.ROOMID_VALIDATE_MIN || GlobalVars.SelectedMultiPlayRoomID > FlowNode_MultiPlayAPI.ROOMID_VALIDATE_MAX)
        {
          this.mParent.Failure();
        }
        else
        {
          this.mParent.enabled = true;
          this.mParent.ExecRequest((WebAPI) new ReqVersusRoomJoin(GlobalVars.SelectedMultiPlayRoomID, new Network.ResponseCallback(((FlowNode_Network) this.mParent).ResponseCallback)));
        }
      }

      public override bool Success(WWWResult www)
      {
        WebAPI.JSON_BodyResponse<ReqVersusRoomJoin.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqVersusRoomJoin.Response>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.mParent.OnFailed();
          return false;
        }
        if (jsonObject.body.quest == null || string.IsNullOrEmpty(jsonObject.body.quest.iname))
        {
          this.mParent.OnFailed();
          return false;
        }
        GlobalVars.SelectedQuestID = jsonObject.body.quest.iname;
        GlobalVars.SelectedMultiPlayPhotonAppID = jsonObject.body.app_id;
        GlobalVars.SelectedMultiPlayRoomName = jsonObject.body.token;
        DebugUtility.Log("JoinRoom  AppID:" + GlobalVars.SelectedMultiPlayPhotonAppID + " Name:" + GlobalVars.SelectedMultiPlayRoomName);
        return true;
      }
    }

    private class Proccess_VersusRoomUpdate : FlowNode_MultiPlayAPIVersus.Proccess
    {
      public override void Activate()
      {
        this.mParent.enabled = true;
        this.mParent.ExecRequest((WebAPI) new ReqVersusRoomUpdate(GlobalVars.SelectedMultiPlayRoomID, GlobalVars.EditMultiPlayRoomComment, GlobalVars.SelectedQuestID, new Network.ResponseCallback(((FlowNode_Network) this.mParent).ResponseCallback)));
      }
    }

    private class Proccess_VersusReset : FlowNode_MultiPlayAPIVersus.Proccess
    {
      public override void Activate()
      {
        GlobalVars.SelectedMultiPlayRoomName = string.Empty;
        GlobalVars.VersusRoomReuse = false;
        GlobalVars.ResumeMultiplayPlayerID = 0;
        GlobalVars.ResumeMultiplaySeatID = 0;
        GlobalVars.MultiPlayVersusKey = MonoSingleton<GameManager>.Instance.GetVersusKey(GlobalVars.SelectedMultiPlayVersusType);
        this.mParent.Success();
      }
    }

    private class Proccess_VersusLineReq : FlowNode_MultiPlayAPIVersus.Proccess
    {
      public override void Activate()
      {
        this.mParent.enabled = true;
        this.mParent.ExecRequest((WebAPI) new ReqVersusLine(GlobalVars.SelectedMultiPlayRoomName, new Network.ResponseCallback(((FlowNode_Network) this.mParent).ResponseCallback)));
      }
    }

    private class Proccess_VersusLineMake : FlowNode_MultiPlayAPIVersus.Proccess
    {
      public override void Activate()
      {
        GlobalVars.VersusRoomReuse = false;
        FlowNode_MultiPlayAPI.RoomMakeTime = Time.realtimeSinceStartup;
        this.mParent.enabled = true;
        this.mParent.ExecRequest((WebAPI) new ReqVersusLineMake(GlobalVars.SelectedMultiPlayRoomName, new Network.ResponseCallback(((FlowNode_Network) this.mParent).ResponseCallback)));
      }
    }

    private class Proccess_VersusLineJoin : FlowNode_MultiPlayAPIVersus.Proccess_VersusRoomJoinID
    {
      public override void Activate()
      {
        GlobalVars.SelectedMultiPlayRoomID = FlowNode_OnUrlSchemeLaunch.LINEParam_decided.roomid;
        if (GlobalVars.SelectedMultiPlayRoomID < FlowNode_MultiPlayAPI.ROOMID_VALIDATE_MIN || GlobalVars.SelectedMultiPlayRoomID > FlowNode_MultiPlayAPI.ROOMID_VALIDATE_MAX)
        {
          this.mParent.Failure();
        }
        else
        {
          this.mParent.enabled = true;
          this.mParent.ExecRequest((WebAPI) new ReqVersusRoomJoin(GlobalVars.SelectedMultiPlayRoomID, new Network.ResponseCallback(((FlowNode_Network) this.mParent).ResponseCallback)));
        }
      }
    }

    private class Proccess_VersusReceiveSeasonGift : FlowNode_MultiPlayAPIVersus.Proccess
    {
      public override void Activate()
      {
        this.mParent.enabled = true;
        this.mParent.ExecRequest((WebAPI) new ReqVersusSeason(new Network.ResponseCallback(((FlowNode_Network) this.mParent).ResponseCallback)));
      }

      public override bool Success(WWWResult www)
      {
        WebAPI.JSON_BodyResponse<ReqVersusSeason.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqVersusSeason.Response>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.mParent.OnFailed();
          return false;
        }
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        player.VersusSeazonGiftReceipt = false;
        player.UnreadMailPeriod |= jsonObject.body.unreadmail == 1;
        return true;
      }
    }

    private class Proccess_VersusFriendScore : FlowNode_MultiPlayAPIVersus.Proccess
    {
      public override void Activate()
      {
        this.mParent.enabled = true;
        this.mParent.ExecRequest((WebAPI) new ReqVersusFriendScore(new Network.ResponseCallback(((FlowNode_Network) this.mParent).ResponseCallback)));
      }

      public override bool Success(WWWResult www)
      {
        WebAPI.JSON_BodyResponse<ReqVersusFriendScore.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqVersusFriendScore.Response>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.mParent.OnFailed();
          return false;
        }
        MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.friends);
        return true;
      }
    }
  }
}
