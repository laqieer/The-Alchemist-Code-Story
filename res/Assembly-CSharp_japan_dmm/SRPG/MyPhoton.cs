// Decompiled with JetBrains decompiler
// Type: SRPG.MyPhoton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using ExitGames.Client.Photon;
using GR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class MyPhoton : PunMonoSingleton<MyPhoton>
  {
    public static readonly int MAX_PLAYER_NUM = 10;
    public static readonly int TIMEOUT_SECOND = 30;
    public static readonly int SEND_RATE = 30;
    private const string LOBBY = "lobby";
    private const string AUDIENCE = "Audience";
    private const string AUDIENCE_MAX = "AudienceMax";
    private const string STARTED_ROOM = "start";
    private const string BATTLESTART_ROOM = "battle";
    private const string DRAFT_BATTLESTART_ROOM = "draft";
    public const string BATTLE_VERSION_ROOM = "btlver";
    public const string STARTED_PARAM_KEY = "started";
    private float mDelaySec = -1f;
    private NetworkReachability mNetworkReach;
    private List<MyPhoton.MyEvent> mEvents = new List<MyPhoton.MyEvent>();
    private MyPhoton.MyRoom mCurrentRoomCache;
    private int mSendRoomMessageID;
    private List<JSON_MyPhotonPlayerParam> mPlayersStarted = new List<JSON_MyPhotonPlayerParam>();
    private const string BaseLoggerTitle = "application";
    private const LogKit.LogLevel BaseLogLevel = LogKit.LogLevel.Info;

    public MyPhoton.MyState CurrentState { get; private set; }

    public bool IsRoomListUpdated { get; set; }

    public bool IsUpdateRoomProperty { get; set; }

    public bool IsUpdatePlayerProperty { get; set; }

    public MyPhoton.MyError LastError { get; private set; }

    public void ResetLastError() => this.LastError = MyPhoton.MyError.NOP;

    public float TimeOutSec { get; set; }

    public bool SendRoomMessageFlush { get; set; }

    public bool DisconnectIfSendRoomMessageFailed { get; set; }

    public bool SortRoomMessage { get; set; }

    public bool IsDisconnected() => PhotonNetwork.networkingPeer.PeerState == null;

    protected override void Initialize()
    {
      UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) this);
      PhotonNetwork.logLevel = PhotonLogLevel.ErrorsOnly;
      PhotonNetwork.OnEventCall += new PhotonNetwork.EventCallback(this.OnEventHandler);
      PhotonNetwork.CrcCheckEnabled = true;
      PhotonNetwork.QuickResends = 3;
      PhotonNetwork.MaxResendsBeforeDisconnect = 7;
      PhotonNetwork.logLevel = PhotonLogLevel.ErrorsOnly;
      PhotonNetwork.sendRate = MyPhoton.SEND_RATE;
      this.UseEncrypt = true;
      this.TimeOutSec = (float) MyPhoton.TIMEOUT_SECOND;
    }

    protected override void Release()
    {
    }

    public string GetTrafficState()
    {
      int num = SupportClass.GetTickCount() - PhotonNetwork.networkingPeer.TimestampOfLastSocketReceive;
      TrafficStats trafficStatsOutgoing = PhotonNetwork.networkingPeer.TrafficStatsOutgoing;
      TrafficStats trafficStatsIncoming = PhotonNetwork.networkingPeer.TrafficStatsIncoming;
      return "lastrecv:" + (object) num + " og:" + trafficStatsOutgoing.ToString() + " ic:" + trafficStatsIncoming.ToString();
    }

    private void Update()
    {
      if (this.CurrentState == MyPhoton.MyState.NOP)
        return;
      NetworkReachability internetReachability = Application.internetReachability;
      if (this.CurrentState != MyPhoton.MyState.NOP && internetReachability != this.mNetworkReach && this.mNetworkReach != null)
        this.LogWarning("internet reach change to " + (object) internetReachability + "\n" + this.GetTrafficState());
      this.mNetworkReach = internetReachability;
      if (this.CurrentState != MyPhoton.MyState.ROOM)
      {
        this.mDelaySec = -1f;
        this.mSendRoomMessageID = 0;
      }
      else if ((double) (SupportClass.GetTickCount() - PhotonNetwork.networkingPeer.TimestampOfLastSocketReceive) < (double) this.TimeOutSec * 1000.0)
      {
        this.mDelaySec = -1f;
      }
      else
      {
        if ((double) this.mDelaySec < 0.0)
        {
          this.mDelaySec = 0.0f;
          this.LogWarning(PhotonNetwork.NetworkStatisticsToString() + "\n" + this.GetTrafficState());
        }
        this.mDelaySec += Time.deltaTime;
        if ((double) this.mDelaySec < (double) this.TimeOutSec)
          return;
        this.LogWarning("maybe connection lost.");
        this.LogWarning(PhotonNetwork.NetworkStatisticsToString() + "\n" + this.GetTrafficState());
        this.Disconnect();
        this.LastError = MyPhoton.MyError.TIMEOUT2;
      }
    }

    public string CurrentAppID { get; private set; }

    public bool StartConnect(string appID, bool autoJoin = false, string ver = "1.0")
    {
      if (this.CurrentState != MyPhoton.MyState.NOP)
      {
        this.LastError = MyPhoton.MyError.ILLEGAL_STATE;
        return false;
      }
      this.CurrentAppID = appID;
      PhotonNetwork.autoJoinLobby = autoJoin;
      PhotonNetwork.PhotonServerSettings.AppID = appID;
      PhotonNetwork.PhotonServerSettings.Protocol = (ConnectionProtocol) 1;
      PhotonNetwork.player.NickName = MonoSingleton<GameManager>.Instance.DeviceId;
      bool flag = PhotonNetwork.ConnectUsingSettings(ver);
      if (flag)
      {
        this.CurrentState = MyPhoton.MyState.CONNECTING;
        PhotonNetwork.NetworkStatisticsEnabled = true;
      }
      else
      {
        this.CurrentState = MyPhoton.MyState.NOP;
        this.LastError = MyPhoton.MyError.UNKNOWN;
      }
      this.Log("StartConnect:" + (object) flag);
      this.IsUpdatePlayerProperty = false;
      return flag;
    }

    public void Disconnect()
    {
      this.Log("call Disconnect().");
      this.mEvents.Clear();
      if (this.CurrentState == MyPhoton.MyState.NOP)
        return;
      PhotonNetwork.Disconnect();
    }

    public override void OnWebRpcResponse(OperationResponse response)
    {
      this.Log("WebRpc:" + response.ToStringFull());
      if (response.ReturnCode == (short) 0)
        return;
      WebRpcResponse webRpcResponse = new WebRpcResponse(response);
      if (webRpcResponse.ReturnCode != 0)
      {
        this.Log("WebRPC '" + webRpcResponse.Name + "' に失敗しました. Error: " + (object) webRpcResponse.ReturnCode + " Message: " + webRpcResponse.DebugMessage);
        MyPhoton.PhotonSendLog("MyPhoton:OnWebRpcResponse", "WebRPC '" + webRpcResponse.Name + "' に失敗しました. Error: " + (object) webRpcResponse.ReturnCode + " Message: " + webRpcResponse.DebugMessage);
      }
      Dictionary<string, object> parameters = webRpcResponse.Parameters;
      StringBuilder stringBuilder = new StringBuilder();
      foreach (KeyValuePair<string, object> keyValuePair in parameters)
      {
        this.Log("Key:" + keyValuePair.Key + "/ Value:" + keyValuePair.Value);
        stringBuilder.Append("Key:" + keyValuePair.Key + "/ Value:" + keyValuePair.Value + "\n");
      }
      MyPhoton.PhotonSendLog("MyPhoton:OnWebRpcResponse", stringBuilder.ToString());
    }

    public override void OnConnectedToPhoton()
    {
      this.Log("Connected to Photon Server");
      this.Log(PhotonNetwork.ServerAddress);
    }

    public override void OnDisconnectedFromPhoton()
    {
      this.Log("DisconnectedFromPhoton. LostPacket:" + (object) PhotonNetwork.PacketLossByCrcCheck + " MaxResendsBeforeDisconnect:" + (object) PhotonNetwork.MaxResendsBeforeDisconnect + " ResentReliableCommands" + (object) PhotonNetwork.ResentReliableCommands);
      this.CurrentState = MyPhoton.MyState.NOP;
    }

    public override void OnFailedToConnectToPhoton(DisconnectCause cause)
    {
      this.Log("FailedToConnectToPhoton." + cause.ToString());
      if (cause == DisconnectCause.DisconnectByClientTimeout || cause == DisconnectCause.DisconnectByServerTimeout)
        this.LastError = MyPhoton.MyError.TIMEOUT;
      if (cause == DisconnectCause.DisconnectByServerUserLimit)
        this.LastError = MyPhoton.MyError.FULL_CLIENTS;
      this.CurrentState = MyPhoton.MyState.NOP;
    }

    public override void OnConnectionFail(DisconnectCause cause)
    {
      this.Log("ConnectionFail." + cause.ToString());
      if (cause == DisconnectCause.DisconnectByClientTimeout || cause == DisconnectCause.DisconnectByServerTimeout)
        this.LastError = MyPhoton.MyError.TIMEOUT;
      if (cause == DisconnectCause.DisconnectByServerUserLimit)
        this.LastError = MyPhoton.MyError.FULL_CLIENTS;
      this.CurrentState = MyPhoton.MyState.NOP;
    }

    public override void OnConnectedToMaster()
    {
      this.Log("Joined Default Lobby.");
      this.CurrentState = MyPhoton.MyState.LOBBY;
      this.mEvents.Clear();
      this.IsRoomListUpdated = false;
      this.Log(PhotonNetwork.ServerAddress);
    }

    public override void OnJoinedLobby()
    {
      this.Log("Joined Lobby.");
      this.CurrentState = MyPhoton.MyState.LOBBY;
      this.mEvents.Clear();
      this.IsRoomListUpdated = false;
      this.Log(PhotonNetwork.ServerAddress);
    }

    public override void OnReceivedRoomListUpdate()
    {
      this.Log("Room List Updated.");
      this.IsRoomListUpdated = true;
    }

    public override void OnPhotonCreateRoomFailed(object[] codeAndMsg)
    {
      this.Log("Create Room failed.");
      if (codeAndMsg == null || codeAndMsg.Length < 2 || !(codeAndMsg[0] is IConvertible))
      {
        this.LastError = MyPhoton.MyError.UNKNOWN;
        this.Log("codeAndMsg is null");
        MyPhoton.PhotonSendLog("MyPhoton:OnPhotonCreateRoomFailed", "codeAndMsg is null");
      }
      else
      {
        this.LastError = ((IConvertible) codeAndMsg[0]).ToInt32((IFormatProvider) null) != 32766 ? MyPhoton.MyError.UNKNOWN : MyPhoton.MyError.ROOM_NAME_DUPLICATED;
        string str = (string) codeAndMsg[1];
        if (str != null)
        {
          this.Log("err:" + str);
          MyPhoton.PhotonSendLog("MyPhoton:OnPhotonCreateRoomFailed", "err:" + str);
        }
      }
      this.CurrentState = MyPhoton.MyState.LOBBY;
    }

    public override void OnJoinedRoom()
    {
      this.Log("Joined Room.");
      this.mEvents.Clear();
      this.CurrentState = MyPhoton.MyState.ROOM;
      this.mSendRoomMessageID = 0;
    }

    public override void OnPhotonJoinRoomFailed(object[] codeAndMsg)
    {
      this.Log("Join Room failed.");
      if (codeAndMsg == null || codeAndMsg.Length < 2 || !(codeAndMsg[0] is IConvertible))
      {
        this.LastError = MyPhoton.MyError.UNKNOWN;
        MyPhoton.PhotonSendLog("MyPhoton:OnPhotonJoinRoomFailed", "codeAndMsg is null");
      }
      else
      {
        switch (((IConvertible) codeAndMsg[0]).ToInt32((IFormatProvider) null))
        {
          case 32758:
            this.LastError = MyPhoton.MyError.ROOM_IS_NOT_EXIST;
            break;
          case 32764:
            this.LastError = MyPhoton.MyError.ROOM_IS_NOT_OPEN;
            break;
          case 32765:
            this.LastError = MyPhoton.MyError.ROOM_IS_FULL;
            break;
          default:
            this.LastError = MyPhoton.MyError.UNKNOWN;
            break;
        }
        string str = (string) codeAndMsg[1];
        if (str != null)
          this.Log("err:" + str);
        MyPhoton.PhotonSendLog("MyPhoton:OnPhotonJoinRoomFailed", "codeAndMsg is null");
      }
      this.CurrentState = MyPhoton.MyState.LOBBY;
    }

    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    {
      this.Log("Join Room failed.");
      if (codeAndMsg == null || codeAndMsg.Length < 2 || !(codeAndMsg[0] is IConvertible))
      {
        this.LastError = MyPhoton.MyError.UNKNOWN;
        MyPhoton.PhotonSendLog("MyPhoton:OnPhotonRandomJoinFailed", "codeAndMsg is null");
      }
      else
      {
        this.LastError = ((IConvertible) codeAndMsg[0]).ToInt32((IFormatProvider) null) != 32760 ? MyPhoton.MyError.UNKNOWN : MyPhoton.MyError.ROOM_IS_NOT_EXIST;
        string str = (string) codeAndMsg[1];
        if (str != null)
        {
          this.Log("err:" + str);
          MyPhoton.PhotonSendLog("MyPhoton:OnPhotonRandomJoinFailed", "err: " + str);
        }
      }
      this.CurrentState = MyPhoton.MyState.LOBBY;
    }

    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
      switch (GlobalVars.SelectedMultiPlayRoomType)
      {
        case JSON_MyPhotonRoomParam.EType.VERSUS:
          Hashtable customProperties = newPlayer.CustomProperties;
          if (customProperties != null && ((Dictionary<object, object>) customProperties).ContainsKey((object) "Logger") && PhotonNetwork.room != null)
          {
            PhotonNetwork.room.IsVisible = true;
            break;
          }
          break;
        case JSON_MyPhotonRoomParam.EType.RANKMATCH:
          if (!PhotonNetwork.isMasterClient)
            break;
          goto case JSON_MyPhotonRoomParam.EType.VERSUS;
      }
      this.Log("Join other player to your room. playerID:" + (object) newPlayer.ID);
    }

    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
      this.Log("Leave other player from your room. playerID:" + (object) otherPlayer.ID);
      this.IsUpdatePlayerProperty = true;
    }

    public override void OnLeftRoom()
    {
      this.Log("Left Room.");
      this.CurrentState = MyPhoton.MyState.LOBBY;
      this.mEvents.Clear();
    }

    public override void OnPhotonCustomRoomPropertiesChanged(Hashtable propertiesThatChanged)
    {
      this.Log("Update Room Property");
      this.IsUpdateRoomProperty = true;
      if (!((Dictionary<object, object>) propertiesThatChanged).ContainsKey((object) "Audience") || !this.IsOldestPlayer())
        return;
      MyPhoton.MyRoom currentRoom = this.GetCurrentRoom();
      if (currentRoom == null)
        return;
      string s = propertiesThatChanged[(object) "Audience"].ToString();
      int result = 0;
      int.TryParse(s, out result);
      JSON_MyPhotonRoomParam myPhotonRoomParam = JSON_MyPhotonRoomParam.Parse(currentRoom.json);
      if (myPhotonRoomParam == null || result == myPhotonRoomParam.audienceNum)
        return;
      myPhotonRoomParam.audienceNum = result;
      this.SetRoomParam(myPhotonRoomParam.Serialize());
    }

    public override void OnPhotonPlayerPropertiesChanged(object[] playerAndupdateProps)
    {
      this.IsUpdatePlayerProperty = true;
    }

    private int GetCryptKey()
    {
      MyPhoton.MyRoom currentRoom = this.GetCurrentRoom();
      if (currentRoom == null || string.IsNullOrEmpty(currentRoom.name))
        return 123;
      int cryptKey = 0;
      foreach (char ch in currentRoom.name)
        cryptKey += (int) ch;
      return cryptKey;
    }

    public List<MyPhoton.MyEvent> GetEvents() => this.mEvents;

    private void OnEventHandler(byte eventCode, object content, int senderID)
    {
      Hashtable hashtable = (Hashtable) content;
      string str = (string) null;
      if (((Dictionary<object, object>) hashtable).ContainsKey((object) "s"))
      {
        int num = (int) hashtable[(object) "s"];
        if (num == 0)
        {
          if (((Dictionary<object, object>) hashtable).ContainsKey((object) "m"))
            str = (string) hashtable[(object) "m"];
        }
        else if (((Dictionary<object, object>) hashtable).ContainsKey((object) "m"))
        {
          byte[] data = (byte[]) hashtable[(object) "m"];
          str = MyEncrypt.Decrypt(num + this.GetCryptKey(), data, true);
        }
      }
      byte[] numArray = (byte[]) null;
      if (((Dictionary<object, object>) hashtable).ContainsKey((object) "bm"))
        numArray = MyEncrypt.Decrypt((byte[]) hashtable[(object) "bm"]);
      MyPhoton.MyEvent myEvent = new MyPhoton.MyEvent();
      myEvent.code = (MyPhoton.SEND_TYPE) eventCode;
      myEvent.playerID = senderID;
      myEvent.json = str;
      myEvent.binary = numArray;
      if (((Dictionary<object, object>) hashtable).ContainsKey((object) "sq"))
        myEvent.sendID = (int) hashtable[(object) "sq"];
      this.mEvents.Add(myEvent);
      if (this.SortRoomMessage)
        this.mEvents.Sort((Comparison<MyPhoton.MyEvent>) ((a, b) => a.sendID - b.sendID));
      this.Log("OnEventHandler: " + (object) senderID + (string) hashtable[(object) "msg"]);
    }

    public List<MyPhoton.MyRoom> GetRoomList()
    {
      List<MyPhoton.MyRoom> roomList = new List<MyPhoton.MyRoom>();
      foreach (global::RoomInfo room in PhotonNetwork.GetRoomList())
      {
        MyPhoton.MyRoom myRoom = new MyPhoton.MyRoom();
        myRoom.name = room.Name;
        myRoom.playerCount = room.PlayerCount;
        myRoom.maxPlayers = (int) room.MaxPlayers;
        myRoom.open = room.IsOpen;
        Hashtable customProperties = room.CustomProperties;
        if (customProperties != null && ((Dictionary<object, object>) customProperties).Count > 0)
        {
          GameUtility.Binary2Object<string>(out myRoom.json, (byte[]) customProperties[(object) "json"]);
          if (!string.IsNullOrEmpty(myRoom.json))
          {
            myRoom.param = JSON_MyPhotonRoomParam.Parse(myRoom.json);
            myRoom.playerCount += myRoom.param.support != null ? myRoom.param.support.Length : 0;
          }
          if (((Dictionary<object, object>) customProperties).ContainsKey((object) "lobby"))
            myRoom.lobby = (string) customProperties[(object) "lobby"];
          if (((Dictionary<object, object>) customProperties).ContainsKey((object) "Audience"))
            int.TryParse(customProperties[(object) "Audience"].ToString(), out myRoom.audience);
          if (((Dictionary<object, object>) customProperties).ContainsKey((object) "AudienceMax"))
            myRoom.audienceMax = (int) customProperties[(object) "AudienceMax"];
          if (((Dictionary<object, object>) customProperties).ContainsKey((object) "start"))
            myRoom.start = (bool) customProperties[(object) "start"];
          if (((Dictionary<object, object>) customProperties).ContainsKey((object) "battle"))
            myRoom.battle = (bool) customProperties[(object) "battle"];
          if (((Dictionary<object, object>) customProperties).ContainsKey((object) "draft"))
            myRoom.draft = (bool) customProperties[(object) "draft"];
        }
        roomList.Add(myRoom);
      }
      return roomList;
    }

    public MyPhoton.MyPlayer GetMyPlayer()
    {
      Hashtable customProperties = PhotonNetwork.player.CustomProperties;
      MyPhoton.MyPlayer myPlayer = new MyPhoton.MyPlayer();
      myPlayer.photonPlayerID = PhotonNetwork.player.ID;
      if (customProperties != null && ((Dictionary<object, object>) customProperties).Count > 0)
      {
        GameUtility.Binary2Object<string>(out myPlayer.json, (byte[]) customProperties[(object) "json"]);
        if (((Dictionary<object, object>) customProperties).ContainsKey((object) "resumeID"))
          myPlayer.resumeID = (int) customProperties[(object) "resumeID"];
      }
      return myPlayer;
    }

    public void SetMyPlayerParam(string json)
    {
      Hashtable propertiesToSet = new Hashtable();
      ((Dictionary<object, object>) propertiesToSet).Add((object) nameof (json), (object) GameUtility.Object2Binary<string>(json));
      PhotonNetwork.player.SetCustomProperties(propertiesToSet);
    }

    public void SetResumeMyPlayer(int playerID = 0)
    {
      if (this.GetMyPlayer() == null)
        return;
      byte[] numArray = (byte[]) null;
      Hashtable customProperties = PhotonNetwork.player.CustomProperties;
      if (customProperties != null && ((Dictionary<object, object>) customProperties).ContainsKey((object) "json"))
        numArray = (byte[]) customProperties[(object) "json"];
      Hashtable propertiesToSet = new Hashtable();
      ((Dictionary<object, object>) propertiesToSet).Add((object) "json", (object) numArray);
      ((Dictionary<object, object>) propertiesToSet).Add((object) "resume", (object) true);
      ((Dictionary<object, object>) propertiesToSet).Add((object) "resumeID", (object) playerID);
      PhotonNetwork.player.SetCustomProperties(propertiesToSet);
    }

    public void AddMyPlayerParam(string key, object param)
    {
      if (this.GetMyPlayer() == null)
        return;
      Hashtable customProperties = PhotonNetwork.player.CustomProperties;
      if (!((Dictionary<object, object>) customProperties).ContainsKey((object) key))
        ((Dictionary<object, object>) customProperties).Add((object) key, param);
      else
        customProperties[(object) key] = param;
      PhotonNetwork.player.SetCustomProperties(customProperties);
    }

    public bool IsResume()
    {
      if (this.GetMyPlayer() != null)
      {
        Hashtable customProperties = PhotonNetwork.player.CustomProperties;
        if (customProperties != null && ((Dictionary<object, object>) customProperties).ContainsKey((object) "resume"))
          return (bool) customProperties[(object) "resume"];
      }
      return false;
    }

    public bool CreateRoom(
      int maxPlayerNum,
      string roomName,
      string roomJson,
      string playerJson,
      string MatchKey = null,
      int floor = -1,
      int plv = -1,
      string luid = null,
      string uid = null,
      int audMax = -1,
      bool isTower = false)
    {
      if (this.CurrentState != MyPhoton.MyState.LOBBY)
      {
        this.LastError = MyPhoton.MyError.ILLEGAL_STATE;
        return false;
      }
      PhotonNetwork.player.SetCustomProperties((Hashtable) null);
      PhotonNetwork.SetPlayerCustomProperties((Hashtable) null);
      this.SetMyPlayerParam(playerJson);
      RoomOptions roomOptions1 = new RoomOptions();
      roomOptions1.MaxPlayers = (byte) maxPlayerNum;
      roomOptions1.IsVisible = GlobalVars.SelectedMultiPlayRoomType != JSON_MyPhotonRoomParam.EType.VERSUS;
      roomOptions1.IsOpen = true;
      if (!Network.GetEnvironment.IsEnvironmentFlag(Gsc.App.Environment.EnvironmentFlagBit.ENV_FLG_PHOTONVERSION_OFF))
      {
        RoomOptions roomOptions2 = roomOptions1;
        Hashtable hashtable1 = new Hashtable();
        ((Dictionary<object, object>) hashtable1).Add((object) "json", (object) GameUtility.Object2Binary<string>(roomJson));
        ((Dictionary<object, object>) hashtable1).Add((object) "name", (object) roomName);
        ((Dictionary<object, object>) hashtable1).Add((object) "start", (object) false);
        ((Dictionary<object, object>) hashtable1).Add((object) "battle", (object) false);
        ((Dictionary<object, object>) hashtable1).Add((object) "draft", (object) false);
        ((Dictionary<object, object>) hashtable1).Add((object) "btlver", (object) MonoSingleton<GameManager>.Instance.BattleVersion);
        Hashtable hashtable2 = hashtable1;
        roomOptions2.CustomRoomProperties = hashtable2;
        roomOptions1.CustomRoomPropertiesForLobby = new string[6]
        {
          "json",
          "name",
          "start",
          "battle",
          "draft",
          "btlver"
        };
      }
      else
      {
        RoomOptions roomOptions3 = roomOptions1;
        Hashtable hashtable3 = new Hashtable();
        ((Dictionary<object, object>) hashtable3).Add((object) "json", (object) GameUtility.Object2Binary<string>(roomJson));
        ((Dictionary<object, object>) hashtable3).Add((object) "name", (object) roomName);
        ((Dictionary<object, object>) hashtable3).Add((object) "start", (object) false);
        ((Dictionary<object, object>) hashtable3).Add((object) "battle", (object) false);
        ((Dictionary<object, object>) hashtable3).Add((object) "draft", (object) false);
        Hashtable hashtable4 = hashtable3;
        roomOptions3.CustomRoomProperties = hashtable4;
        roomOptions1.CustomRoomPropertiesForLobby = new string[5]
        {
          "json",
          "name",
          "start",
          "battle",
          "draft"
        };
      }
      if (isTower && !string.IsNullOrEmpty(MatchKey))
      {
        int length = roomOptions1.CustomRoomPropertiesForLobby.Length;
        int num1 = roomOptions1.CustomRoomPropertiesForLobby.Length + 3;
        int num2;
        int num3 = floor == -1 ? num1 : (num2 = num1 + 1);
        int num4 = GlobalVars.BlockList == null || GlobalVars.BlockList.Count <= 0 ? num3 : (num2 = num3 + 1);
        int newSize = string.IsNullOrEmpty(uid) ? num4 : (num2 = num4 + 1);
        Array.Resize<string>(ref roomOptions1.CustomRoomPropertiesForLobby, newSize);
        ((Dictionary<object, object>) roomOptions1.CustomRoomProperties).Add((object) "lobby", (object) "tower");
        roomOptions1.CustomRoomPropertiesForLobby[roomOptions1.CustomRoomPropertiesForLobby.Length - 1] = "lobby";
        ((Dictionary<object, object>) roomOptions1.CustomRoomProperties).Add((object) "MatchType", (object) MatchKey);
        string[] propertiesForLobby1 = roomOptions1.CustomRoomPropertiesForLobby;
        int index1 = length;
        int num5 = index1 + 1;
        propertiesForLobby1[index1] = "MatchType";
        ((Dictionary<object, object>) roomOptions1.CustomRoomProperties).Add((object) "Lock", (object) (GlobalVars.EditMultiPlayRoomPassCode != "0"));
        string[] propertiesForLobby2 = roomOptions1.CustomRoomPropertiesForLobby;
        int index2 = num5;
        int num6 = index2 + 1;
        propertiesForLobby2[index2] = "Lock";
        if (floor != -1)
        {
          ((Dictionary<object, object>) roomOptions1.CustomRoomProperties).Add((object) nameof (floor), (object) floor);
          roomOptions1.CustomRoomPropertiesForLobby[num6++] = nameof (floor);
        }
        string[] array = GlobalVars.BlockList.ToArray();
        if (array != null && array.Length > 0)
        {
          StringBuilder stringBuilder = new StringBuilder();
          for (int index3 = 0; index3 < array.Length; ++index3)
          {
            if (index3 > 0)
              stringBuilder.Append(",");
            stringBuilder.Append(array[index3]);
          }
          ((Dictionary<object, object>) roomOptions1.CustomRoomProperties).Add((object) "BlockList", (object) stringBuilder.ToString());
          roomOptions1.CustomRoomPropertiesForLobby[num6++] = "BlockList";
        }
        if (!string.IsNullOrEmpty(uid))
        {
          ((Dictionary<object, object>) roomOptions1.CustomRoomProperties).Add((object) nameof (uid), (object) uid);
          string[] propertiesForLobby3 = roomOptions1.CustomRoomPropertiesForLobby;
          int index4 = num6;
          int num7 = index4 + 1;
          propertiesForLobby3[index4] = nameof (uid);
        }
      }
      else if (!string.IsNullOrEmpty(MatchKey))
      {
        int length = roomOptions1.CustomRoomPropertiesForLobby.Length;
        int num8 = roomOptions1.CustomRoomPropertiesForLobby.Length + 3;
        int num9;
        int num10 = plv == -1 ? num8 : (num9 = num8 + 1);
        int num11 = floor == -1 ? num10 : (num9 = num10 + 1);
        int num12 = string.IsNullOrEmpty(luid) ? num11 : (num9 = num11 + 1);
        int num13 = string.IsNullOrEmpty(uid) ? num12 : (num9 = num12 + 1);
        int newSize = audMax == -1 ? num13 : (num9 = num13 + 1);
        Array.Resize<string>(ref roomOptions1.CustomRoomPropertiesForLobby, newSize);
        ((Dictionary<object, object>) roomOptions1.CustomRoomProperties).Add((object) "MatchType", (object) MatchKey);
        string[] propertiesForLobby4 = roomOptions1.CustomRoomPropertiesForLobby;
        int index5 = length;
        int num14 = index5 + 1;
        propertiesForLobby4[index5] = "MatchType";
        ((Dictionary<object, object>) roomOptions1.CustomRoomProperties).Add((object) "lobby", (object) "vs");
        string[] propertiesForLobby5 = roomOptions1.CustomRoomPropertiesForLobby;
        int index6 = num14;
        int num15 = index6 + 1;
        propertiesForLobby5[index6] = "lobby";
        ((Dictionary<object, object>) roomOptions1.CustomRoomProperties).Add((object) "Audience", (object) "0");
        string[] propertiesForLobby6 = roomOptions1.CustomRoomPropertiesForLobby;
        int index7 = num15;
        int num16 = index7 + 1;
        propertiesForLobby6[index7] = "Audience";
        if (plv != -1)
        {
          ((Dictionary<object, object>) roomOptions1.CustomRoomProperties).Add((object) nameof (plv), (object) plv);
          roomOptions1.CustomRoomPropertiesForLobby[num16++] = nameof (plv);
        }
        if (floor != -1)
        {
          ((Dictionary<object, object>) roomOptions1.CustomRoomProperties).Add((object) nameof (floor), (object) floor);
          roomOptions1.CustomRoomPropertiesForLobby[num16++] = nameof (floor);
        }
        if (!string.IsNullOrEmpty(luid))
        {
          ((Dictionary<object, object>) roomOptions1.CustomRoomProperties).Add((object) nameof (luid), (object) luid);
          roomOptions1.CustomRoomPropertiesForLobby[num16++] = nameof (luid);
        }
        if (!string.IsNullOrEmpty(uid))
        {
          ((Dictionary<object, object>) roomOptions1.CustomRoomProperties).Add((object) nameof (uid), (object) uid);
          roomOptions1.CustomRoomPropertiesForLobby[num16++] = nameof (uid);
        }
        if (audMax > 0)
        {
          ((Dictionary<object, object>) roomOptions1.CustomRoomProperties).Add((object) "AudienceMax", (object) audMax);
          string[] propertiesForLobby7 = roomOptions1.CustomRoomPropertiesForLobby;
          int index8 = num16;
          int num17 = index8 + 1;
          propertiesForLobby7[index8] = "AudienceMax";
        }
      }
      else
      {
        ((Dictionary<object, object>) roomOptions1.CustomRoomProperties).Add((object) "lobby", (object) "coop");
        Array.Resize<string>(ref roomOptions1.CustomRoomPropertiesForLobby, roomOptions1.CustomRoomPropertiesForLobby.Length + 1);
        roomOptions1.CustomRoomPropertiesForLobby[roomOptions1.CustomRoomPropertiesForLobby.Length - 1] = "lobby";
      }
      bool room = PhotonNetwork.CreateRoom(roomName, roomOptions1, (TypedLobby) null);
      if (room)
        this.CurrentState = MyPhoton.MyState.JOINING;
      else
        this.LastError = MyPhoton.MyError.UNKNOWN;
      return room;
    }

    public bool CreateRoom(
      string roomName,
      string roomJson,
      string playerJson,
      int plv,
      string uid,
      int score,
      int type)
    {
      if (this.CurrentState != MyPhoton.MyState.LOBBY)
      {
        this.LastError = MyPhoton.MyError.ILLEGAL_STATE;
        return false;
      }
      PhotonNetwork.player.SetCustomProperties((Hashtable) null);
      PhotonNetwork.SetPlayerCustomProperties((Hashtable) null);
      this.SetMyPlayerParam(playerJson);
      RoomOptions roomOptions1 = new RoomOptions();
      roomOptions1.MaxPlayers = (byte) 3;
      roomOptions1.IsVisible = false;
      roomOptions1.IsOpen = true;
      if (!Network.GetEnvironment.IsEnvironmentFlag(Gsc.App.Environment.EnvironmentFlagBit.ENV_FLG_PHOTONVERSION_OFF))
      {
        RoomOptions roomOptions2 = roomOptions1;
        Hashtable hashtable1 = new Hashtable();
        ((Dictionary<object, object>) hashtable1).Add((object) "json", (object) GameUtility.Object2Binary<string>(roomJson));
        ((Dictionary<object, object>) hashtable1).Add((object) "name", (object) roomName);
        ((Dictionary<object, object>) hashtable1).Add((object) "start", (object) false);
        ((Dictionary<object, object>) hashtable1).Add((object) "battle", (object) false);
        ((Dictionary<object, object>) hashtable1).Add((object) "draft", (object) false);
        ((Dictionary<object, object>) hashtable1).Add((object) "btlver", (object) MonoSingleton<GameManager>.Instance.BattleVersion);
        Hashtable hashtable2 = hashtable1;
        roomOptions2.CustomRoomProperties = hashtable2;
        roomOptions1.CustomRoomPropertiesForLobby = new string[5]
        {
          "json",
          "name",
          "start",
          "draft",
          "btlver"
        };
      }
      else
      {
        RoomOptions roomOptions3 = roomOptions1;
        Hashtable hashtable3 = new Hashtable();
        ((Dictionary<object, object>) hashtable3).Add((object) "json", (object) GameUtility.Object2Binary<string>(roomJson));
        ((Dictionary<object, object>) hashtable3).Add((object) "name", (object) roomName);
        ((Dictionary<object, object>) hashtable3).Add((object) "start", (object) false);
        ((Dictionary<object, object>) hashtable3).Add((object) "battle", (object) false);
        ((Dictionary<object, object>) hashtable3).Add((object) "draft", (object) false);
        Hashtable hashtable4 = hashtable3;
        roomOptions3.CustomRoomProperties = hashtable4;
        roomOptions1.CustomRoomPropertiesForLobby = new string[4]
        {
          "json",
          "name",
          "start",
          "draft"
        };
      }
      int length = roomOptions1.CustomRoomPropertiesForLobby.Length;
      int newSize = roomOptions1.CustomRoomPropertiesForLobby.Length + 7;
      Array.Resize<string>(ref roomOptions1.CustomRoomPropertiesForLobby, newSize);
      ((Dictionary<object, object>) roomOptions1.CustomRoomProperties).Add((object) "MatchType", (object) MonoSingleton<GameManager>.Instance.GetVersusKey(VERSUS_TYPE.RankMatch));
      string[] propertiesForLobby1 = roomOptions1.CustomRoomPropertiesForLobby;
      int index1 = length;
      int num1 = index1 + 1;
      propertiesForLobby1[index1] = "MatchType";
      ((Dictionary<object, object>) roomOptions1.CustomRoomProperties).Add((object) "lobby", (object) "vs");
      string[] propertiesForLobby2 = roomOptions1.CustomRoomPropertiesForLobby;
      int index2 = num1;
      int num2 = index2 + 1;
      propertiesForLobby2[index2] = "lobby";
      ((Dictionary<object, object>) roomOptions1.CustomRoomProperties).Add((object) "Audience", (object) "0");
      string[] propertiesForLobby3 = roomOptions1.CustomRoomPropertiesForLobby;
      int index3 = num2;
      int num3 = index3 + 1;
      propertiesForLobby3[index3] = "Audience";
      ((Dictionary<object, object>) roomOptions1.CustomRoomProperties).Add((object) nameof (plv), (object) plv);
      string[] propertiesForLobby4 = roomOptions1.CustomRoomPropertiesForLobby;
      int index4 = num3;
      int num4 = index4 + 1;
      propertiesForLobby4[index4] = nameof (plv);
      ((Dictionary<object, object>) roomOptions1.CustomRoomProperties).Add((object) nameof (score), (object) score);
      string[] propertiesForLobby5 = roomOptions1.CustomRoomPropertiesForLobby;
      int index5 = num4;
      int num5 = index5 + 1;
      propertiesForLobby5[index5] = nameof (score);
      ((Dictionary<object, object>) roomOptions1.CustomRoomProperties).Add((object) nameof (type), (object) type);
      string[] propertiesForLobby6 = roomOptions1.CustomRoomPropertiesForLobby;
      int index6 = num5;
      int num6 = index6 + 1;
      propertiesForLobby6[index6] = nameof (type);
      ((Dictionary<object, object>) roomOptions1.CustomRoomProperties).Add((object) nameof (uid), (object) uid);
      string[] propertiesForLobby7 = roomOptions1.CustomRoomPropertiesForLobby;
      int index7 = num6;
      int num7 = index7 + 1;
      propertiesForLobby7[index7] = nameof (uid);
      bool room = PhotonNetwork.CreateRoom(roomName, roomOptions1, (TypedLobby) null);
      if (room)
        this.CurrentState = MyPhoton.MyState.JOINING;
      else
        this.LastError = MyPhoton.MyError.UNKNOWN;
      return room;
    }

    public bool JoinRoom(string roomName, string playerJson, bool isResume = false)
    {
      if (this.CurrentState != MyPhoton.MyState.LOBBY)
      {
        this.LastError = MyPhoton.MyError.ILLEGAL_STATE;
        return false;
      }
      PhotonNetwork.player.SetCustomProperties((Hashtable) null);
      PhotonNetwork.SetPlayerCustomProperties((Hashtable) null);
      this.SetMyPlayerParam(playerJson);
      bool flag;
      if (isResume)
      {
        flag = PhotonNetwork.JoinRoom(roomName);
      }
      else
      {
        Hashtable expectedCustomRoomProperties = new Hashtable();
        ((Dictionary<object, object>) expectedCustomRoomProperties).Add((object) "name", (object) roomName);
        ((Dictionary<object, object>) expectedCustomRoomProperties).Add((object) "start", (object) false);
        flag = PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties, (byte) 0);
      }
      if (flag)
        this.CurrentState = MyPhoton.MyState.JOINING;
      else
        this.LastError = MyPhoton.MyError.UNKNOWN;
      return flag;
    }

    public bool JoinRandomRoom(
      byte maxPlayerNum,
      string playerJson,
      string VersusHash,
      string roomName = null,
      int floor = -1,
      int pass = -1,
      string uid = null)
    {
      PhotonNetwork.player.SetCustomProperties((Hashtable) null);
      PhotonNetwork.SetPlayerCustomProperties((Hashtable) null);
      this.SetMyPlayerParam(playerJson);
      bool flag1 = false;
      if (floor != -1)
      {
        string roomName1 = string.Empty;
        bool flag2 = false;
        global::RoomInfo[] roomList = PhotonNetwork.GetRoomList();
        List<global::RoomInfo> roomInfoList1 = new List<global::RoomInfo>();
        List<string> stringList = new List<string>();
        foreach (global::RoomInfo roomInfo in roomList)
        {
          stringList.Clear();
          Hashtable customProperties = roomInfo.CustomProperties;
          if (((Dictionary<object, object>) customProperties).ContainsKey((object) "MatchType") && VersusHash == (string) customProperties[(object) "MatchType"])
          {
            if (((Dictionary<object, object>) customProperties).ContainsKey((object) "BlockList"))
            {
              string str = (string) customProperties[(object) "BlockList"];
              if (!string.IsNullOrEmpty(str))
              {
                stringList.AddRange((IEnumerable<string>) str.Split(','));
                if (stringList.FindIndex((Predicate<string>) (id => id == uid)) != -1)
                  continue;
              }
            }
            if ((!((Dictionary<object, object>) customProperties).ContainsKey((object) "start") || !(bool) customProperties[(object) "start"]) && (floor == -1 || !((Dictionary<object, object>) customProperties).ContainsKey((object) nameof (floor)) || (int) customProperties[(object) nameof (floor)] == floor) && (pass == -1 || !((Dictionary<object, object>) customProperties).ContainsKey((object) "Lock") || !(bool) customProperties[(object) "Lock"]) && (Network.GetEnvironment.IsEnvironmentFlag(Gsc.App.Environment.EnvironmentFlagBit.ENV_FLG_PHOTONVERSION_OFF) || !((Dictionary<object, object>) customProperties).ContainsKey((object) "btlver") || !((string) customProperties[(object) "btlver"] != MonoSingleton<GameManager>.Instance.BattleVersion)))
              roomInfoList1.Add(roomInfo);
          }
        }
        if (roomInfoList1 != null && roomInfoList1.Count > 0)
        {
          List<global::RoomInfo> roomInfoList2 = new List<global::RoomInfo>((IEnumerable<global::RoomInfo>) ((IEnumerable<global::RoomInfo>) roomInfoList1.ToArray()).OrderBy<global::RoomInfo, Guid>((Func<global::RoomInfo, Guid>) (room => Guid.NewGuid())));
          if (GlobalVars.BlockList != null && GlobalVars.BlockList.Count > 0)
          {
            List<string> blockList = GlobalVars.BlockList;
            foreach (global::RoomInfo roomInfo in roomInfoList2)
            {
              Hashtable customProperties = roomInfo.CustomProperties;
              if (((Dictionary<object, object>) customProperties).ContainsKey((object) nameof (uid)))
              {
                string target_uid = (string) customProperties[(object) nameof (uid)];
                if (blockList.FindIndex((Predicate<string>) (buid => buid == target_uid)) == -1)
                {
                  flag2 = true;
                  roomName1 = roomInfo.Name;
                  break;
                }
              }
            }
          }
          else
          {
            flag2 = true;
            roomName1 = roomInfoList2[0].Name;
          }
        }
        if (flag2)
          flag1 = PhotonNetwork.JoinRoom(roomName1);
      }
      else
      {
        Hashtable hashtable1 = new Hashtable();
        Hashtable hashtable2;
        if (!Network.GetEnvironment.IsEnvironmentFlag(Gsc.App.Environment.EnvironmentFlagBit.ENV_FLG_PHOTONVERSION_OFF))
        {
          Hashtable hashtable3 = new Hashtable();
          ((Dictionary<object, object>) hashtable3).Add((object) "MatchType", (object) VersusHash);
          ((Dictionary<object, object>) hashtable3).Add((object) "start", (object) false);
          ((Dictionary<object, object>) hashtable3).Add((object) "btlver", (object) MonoSingleton<GameManager>.Instance.BattleVersion);
          hashtable2 = hashtable3;
        }
        else
        {
          Hashtable hashtable4 = new Hashtable();
          ((Dictionary<object, object>) hashtable4).Add((object) "MatchType", (object) VersusHash);
          ((Dictionary<object, object>) hashtable4).Add((object) "start", (object) false);
          hashtable2 = hashtable4;
        }
        Hashtable hashtable5 = new Hashtable();
        ((Dictionary<object, object>) hashtable5).Add((object) "MatchType", (object) VersusHash);
        ((Dictionary<object, object>) hashtable5).Add((object) "start", (object) false);
        Hashtable hashtable6 = hashtable5;
        Hashtable expectedCustomRoomProperties;
        if (!string.IsNullOrEmpty(roomName))
        {
          expectedCustomRoomProperties = hashtable6;
          ((Dictionary<object, object>) expectedCustomRoomProperties).Add((object) "name", (object) roomName);
        }
        else
          expectedCustomRoomProperties = hashtable2;
        if (floor != -1)
          ((Dictionary<object, object>) expectedCustomRoomProperties).Add((object) nameof (floor), (object) floor);
        if (pass != -1)
          ((Dictionary<object, object>) expectedCustomRoomProperties).Add((object) "Lock", (object) false);
        flag1 = PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties, maxPlayerNum);
      }
      if (flag1)
        this.CurrentState = MyPhoton.MyState.JOINING;
      else
        this.LastError = MyPhoton.MyError.UNKNOWN;
      return flag1;
    }

    public bool JoinRoomCheckParam(
      string VersusHash,
      string playerJson,
      int lvRange,
      int floorRange,
      int lv,
      int floor,
      string lastuid,
      string uid)
    {
      if (this.CurrentState != MyPhoton.MyState.LOBBY)
      {
        this.LastError = MyPhoton.MyError.ILLEGAL_STATE;
        return false;
      }
      string roomName = string.Empty;
      bool flag1 = false;
      PhotonNetwork.player.SetCustomProperties((Hashtable) null);
      PhotonNetwork.SetPlayerCustomProperties((Hashtable) null);
      this.SetMyPlayerParam(playerJson);
      global::RoomInfo[] roomList = PhotonNetwork.GetRoomList();
      List<global::RoomInfo> roomInfoList = new List<global::RoomInfo>();
      foreach (global::RoomInfo roomInfo in roomList)
      {
        Hashtable customProperties = roomInfo.CustomProperties;
        if (((Dictionary<object, object>) customProperties).ContainsKey((object) "MatchType") && VersusHash == (string) customProperties[(object) "MatchType"] && (!((Dictionary<object, object>) customProperties).ContainsKey((object) nameof (uid)) || string.IsNullOrEmpty(lastuid) || string.Compare((string) customProperties[(object) nameof (uid)], lastuid) != 0) && (!((Dictionary<object, object>) customProperties).ContainsKey((object) "luid") || string.IsNullOrEmpty(uid) || string.Compare((string) customProperties[(object) "luid"], uid) != 0) && (!((Dictionary<object, object>) customProperties).ContainsKey((object) "start") || !(bool) customProperties[(object) "start"]) && (Network.GetEnvironment.IsEnvironmentFlag(Gsc.App.Environment.EnvironmentFlagBit.ENV_FLG_PHOTONVERSION_OFF) || ((Dictionary<object, object>) customProperties).ContainsKey((object) "btlver") && !((string) customProperties[(object) "btlver"] != MonoSingleton<GameManager>.Instance.BattleVersion)))
          roomInfoList.Add(roomInfo);
      }
      if (lvRange != -1)
      {
        int num1 = lv - lvRange;
        int num2 = lv + lvRange;
        foreach (global::RoomInfo roomInfo in roomInfoList)
        {
          Hashtable customProperties = roomInfo.CustomProperties;
          if (((Dictionary<object, object>) customProperties).ContainsKey((object) "plv") && ((Dictionary<object, object>) customProperties).ContainsKey((object) nameof (floor)))
          {
            int num3 = (int) customProperties[(object) nameof (floor)];
            int num4 = (int) customProperties[(object) "plv"];
            if (num1 <= num4 && num4 <= num2 && num3 == floor)
            {
              roomName = roomInfo.Name;
              flag1 = true;
              break;
            }
          }
        }
      }
      if (!flag1)
      {
        foreach (global::RoomInfo roomInfo in roomInfoList)
        {
          Hashtable customProperties = roomInfo.CustomProperties;
          if (((Dictionary<object, object>) customProperties).ContainsKey((object) nameof (floor)) && floor == (int) customProperties[(object) nameof (floor)])
          {
            roomName = roomInfo.Name;
            flag1 = true;
            break;
          }
        }
      }
      if (floorRange != -1 && !flag1)
      {
        for (int index = 1; index <= floorRange; ++index)
        {
          int num5 = floor - index;
          int num6 = floor + index;
          foreach (global::RoomInfo roomInfo in roomInfoList)
          {
            Hashtable customProperties = roomInfo.CustomProperties;
            if (((Dictionary<object, object>) customProperties).ContainsKey((object) nameof (floor)))
            {
              int num7 = (int) customProperties[(object) nameof (floor)];
              if (num5 <= num7 && num7 <= num6)
              {
                roomName = roomInfo.Name;
                flag1 = true;
                break;
              }
            }
          }
          if (flag1)
            break;
        }
      }
      bool flag2 = false;
      if (flag1)
      {
        flag2 = PhotonNetwork.JoinRoom(roomName);
        if (flag2)
          this.CurrentState = MyPhoton.MyState.JOINING;
        else
          this.LastError = MyPhoton.MyError.UNKNOWN;
      }
      return flag2;
    }

    public bool JoinRankMatchRoomCheckParam(
      string playerJson,
      int lv,
      int lvRange,
      string uid,
      int score,
      int scoreRangeMax,
      int scoreRangeMin,
      int type,
      string[] exclude_uids)
    {
      if (this.CurrentState != MyPhoton.MyState.LOBBY)
      {
        this.LastError = MyPhoton.MyError.ILLEGAL_STATE;
        return false;
      }
      string roomName = string.Empty;
      bool flag1 = false;
      PhotonNetwork.player.SetCustomProperties((Hashtable) null);
      PhotonNetwork.SetPlayerCustomProperties((Hashtable) null);
      this.SetMyPlayerParam(playerJson);
      global::RoomInfo[] roomList = PhotonNetwork.GetRoomList();
      List<global::RoomInfo> roomInfoList = new List<global::RoomInfo>();
      string versusKey = MonoSingleton<GameManager>.Instance.GetVersusKey(VERSUS_TYPE.RankMatch);
      foreach (global::RoomInfo roomInfo in roomList)
      {
        Hashtable customProperties = roomInfo.CustomProperties;
        if (((Dictionary<object, object>) customProperties).ContainsKey((object) "MatchType") && versusKey == (string) customProperties[(object) "MatchType"])
        {
          if (((Dictionary<object, object>) customProperties).ContainsKey((object) nameof (uid)) && exclude_uids != null && exclude_uids.Length > 0)
          {
            string strA = (string) customProperties[(object) nameof (uid)];
            bool flag2 = false;
            foreach (string excludeUid in exclude_uids)
            {
              if (string.Compare(strA, excludeUid) == 0)
              {
                flag2 = true;
                break;
              }
            }
            if (flag2)
              continue;
          }
          if (((Dictionary<object, object>) customProperties).ContainsKey((object) nameof (score)) && (!((Dictionary<object, object>) customProperties).ContainsKey((object) "start") || !(bool) customProperties[(object) "start"]) && (Network.GetEnvironment.IsEnvironmentFlag(Gsc.App.Environment.EnvironmentFlagBit.ENV_FLG_PHOTONVERSION_OFF) || !((Dictionary<object, object>) customProperties).ContainsKey((object) "btlver") || !((string) customProperties[(object) "btlver"] != MonoSingleton<GameManager>.Instance.BattleVersion)))
            roomInfoList.Add(roomInfo);
        }
      }
      roomInfoList.Sort((Comparison<global::RoomInfo>) ((a, b) =>
      {
        int num1 = 0;
        int num2 = 0;
        Hashtable customProperties1 = a.CustomProperties;
        if (((Dictionary<object, object>) customProperties1).ContainsKey((object) nameof (score)))
          num1 = Math.Abs(score - (int) customProperties1[(object) nameof (score)]);
        Hashtable customProperties2 = b.CustomProperties;
        if (((Dictionary<object, object>) customProperties2).ContainsKey((object) nameof (score)))
          num2 = Math.Abs(score - (int) customProperties2[(object) nameof (score)]);
        return num1 - num2;
      }));
      if (scoreRangeMin != -1)
      {
        int num3 = score - scoreRangeMin;
        int num4 = score + scoreRangeMin;
        foreach (global::RoomInfo roomInfo in roomInfoList)
        {
          Hashtable customProperties = roomInfo.CustomProperties;
          if (((Dictionary<object, object>) customProperties).ContainsKey((object) nameof (score)))
          {
            int num5 = (int) customProperties[(object) nameof (score)];
            if (num3 <= num5 && num5 <= num4)
            {
              if (lvRange != -1 && ((Dictionary<object, object>) customProperties).ContainsKey((object) "plv"))
              {
                int num6 = lv - lvRange;
                int num7 = lv + lvRange;
                int num8 = (int) customProperties[(object) "plv"];
                if (num8 < num6 && num7 < num8)
                  continue;
              }
              flag1 = true;
              roomName = roomInfo.Name;
              break;
            }
          }
        }
      }
      if (!flag1)
      {
        foreach (global::RoomInfo roomInfo in roomInfoList)
        {
          Hashtable customProperties = roomInfo.CustomProperties;
          if (((Dictionary<object, object>) customProperties).ContainsKey((object) nameof (type)) && (int) customProperties[(object) nameof (type)] == type)
          {
            if (lvRange != -1 && ((Dictionary<object, object>) customProperties).ContainsKey((object) "plv"))
            {
              int num9 = lv - lvRange;
              int num10 = lv + lvRange;
              int num11 = (int) customProperties[(object) "plv"];
              if (num11 < num9 && num10 < num11)
                continue;
            }
            flag1 = true;
            roomName = roomInfo.Name;
            break;
          }
        }
      }
      if (!flag1 && scoreRangeMax != -1)
      {
        foreach (global::RoomInfo roomInfo in roomInfoList)
        {
          Hashtable customProperties = roomInfo.CustomProperties;
          if (lvRange != -1 && ((Dictionary<object, object>) customProperties).ContainsKey((object) "plv"))
          {
            int num12 = lv - lvRange;
            int num13 = lv + lvRange;
            int num14 = (int) customProperties[(object) "plv"];
            if (num14 < num12 && num13 < num14)
              continue;
          }
          flag1 = true;
          roomName = roomInfo.Name;
          break;
        }
      }
      bool flag3 = false;
      if (flag1)
      {
        flag3 = PhotonNetwork.JoinRoom(roomName);
        if (flag3)
          this.CurrentState = MyPhoton.MyState.JOINING;
        else
          this.LastError = MyPhoton.MyError.UNKNOWN;
      }
      return flag3;
    }

    public bool LeaveRoom()
    {
      if (this.CurrentState != MyPhoton.MyState.ROOM)
      {
        this.LastError = MyPhoton.MyError.ILLEGAL_STATE;
        return false;
      }
      bool flag = PhotonNetwork.LeaveRoom();
      if (flag)
        this.CurrentState = MyPhoton.MyState.LEAVING;
      else
        this.LastError = MyPhoton.MyError.UNKNOWN;
      return flag;
    }

    public MyPhoton.MyRoom GetCurrentRoom()
    {
      Room room = PhotonNetwork.room;
      if (room == null)
      {
        this.mCurrentRoomCache = (MyPhoton.MyRoom) null;
        return (MyPhoton.MyRoom) null;
      }
      if (this.mCurrentRoomCache == null)
        this.mCurrentRoomCache = new MyPhoton.MyRoom();
      this.mCurrentRoomCache.name = room.Name;
      this.mCurrentRoomCache.playerCount = room.PlayerCount;
      this.mCurrentRoomCache.maxPlayers = room.MaxPlayers;
      this.mCurrentRoomCache.open = room.IsOpen;
      this.mCurrentRoomCache.start = false;
      Hashtable customProperties = room.CustomProperties;
      if (customProperties != null)
      {
        if (((Dictionary<object, object>) customProperties).ContainsKey((object) "json"))
        {
          string buffer;
          GameUtility.Binary2Object<string>(out buffer, (byte[]) customProperties[(object) "json"]);
          if (!string.IsNullOrEmpty(buffer))
          {
            if (this.mCurrentRoomCache.json != buffer)
              this.mCurrentRoomCache.param = JsonUtility.FromJson<JSON_MyPhotonRoomParam>(buffer);
            this.mCurrentRoomCache.playerCount += this.mCurrentRoomCache.param.support != null ? this.mCurrentRoomCache.param.support.Length : 0;
          }
          this.mCurrentRoomCache.json = buffer;
        }
        if (((Dictionary<object, object>) customProperties).ContainsKey((object) "start"))
          this.mCurrentRoomCache.start = (bool) customProperties[(object) "start"];
        if (((Dictionary<object, object>) customProperties).ContainsKey((object) "battle"))
          this.mCurrentRoomCache.battle = (bool) customProperties[(object) "battle"];
        if (((Dictionary<object, object>) customProperties).ContainsKey((object) "draft"))
          this.mCurrentRoomCache.draft = (bool) customProperties[(object) "draft"];
      }
      return this.mCurrentRoomCache;
    }

    public bool SetRoomParam(string json)
    {
      if (this.CurrentState != MyPhoton.MyState.ROOM)
      {
        this.LastError = MyPhoton.MyError.ILLEGAL_STATE;
        return false;
      }
      Hashtable hashtable = new Hashtable();
      ((Dictionary<object, object>) hashtable).Add((object) nameof (json), (object) GameUtility.Object2Binary<string>(json));
      Hashtable propertiesToSet = hashtable;
      if (PhotonNetwork.room == null)
      {
        this.LastError = MyPhoton.MyError.ILLEGAL_STATE;
        return false;
      }
      PhotonNetwork.room.SetCustomProperties(propertiesToSet);
      return true;
    }

    public bool SetRoomParam(string key, object param)
    {
      if (this.CurrentState != MyPhoton.MyState.ROOM)
      {
        this.LastError = MyPhoton.MyError.ILLEGAL_STATE;
        return false;
      }
      Room room = PhotonNetwork.room;
      if (room == null)
        return false;
      if (key == "started")
        param = (object) GameUtility.Object2Binary<object>(param);
      Hashtable hashtable = new Hashtable();
      ((Dictionary<object, object>) hashtable).Add((object) key, param);
      Hashtable propertiesToSet = hashtable;
      room.SetCustomProperties((Hashtable) null);
      room.SetCustomProperties(propertiesToSet);
      return true;
    }

    public bool UpdateRoomParam(string key, object param)
    {
      if (this.CurrentState != MyPhoton.MyState.ROOM)
      {
        this.LastError = MyPhoton.MyError.ILLEGAL_STATE;
        return false;
      }
      Room room = PhotonNetwork.room;
      if (room == null)
        return false;
      if (key == "started")
        param = (object) GameUtility.Object2Binary<object>(param);
      Hashtable customProperties = room.CustomProperties;
      if (customProperties != null)
      {
        if (((Dictionary<object, object>) customProperties).ContainsKey((object) key))
          customProperties[(object) key] = param;
        else
          ((Dictionary<object, object>) customProperties).Add((object) key, param);
      }
      room.SetCustomProperties((Hashtable) null);
      room.SetCustomProperties(customProperties);
      return true;
    }

    public string GetRoomParam(string key)
    {
      if (this.CurrentState != MyPhoton.MyState.ROOM)
      {
        this.LastError = MyPhoton.MyError.ILLEGAL_STATE;
        return (string) null;
      }
      Room room = PhotonNetwork.room;
      if (room == null)
        return (string) null;
      Hashtable customProperties = room.CustomProperties;
      if (customProperties != null)
      {
        object data = (object) null;
        if (((Dictionary<object, object>) customProperties).TryGetValue((object) key, out data))
        {
          if (data.ToString().IndexOf("players") == -1)
          {
            string buffer;
            GameUtility.Binary2Object<string>(out buffer, (byte[]) data);
            return buffer;
          }
          if (!(key == "started"))
            return (string) data;
          string buffer1;
          GameUtility.Binary2Object<string>(out buffer1, (byte[]) data);
          return buffer1;
        }
      }
      return (string) null;
    }

    public bool CloseRoom()
    {
      if (this.CurrentState != MyPhoton.MyState.ROOM)
      {
        this.LastError = MyPhoton.MyError.ILLEGAL_STATE;
        return false;
      }
      byte[] numArray = (byte[]) null;
      if (((Dictionary<object, object>) PhotonNetwork.room.CustomProperties).Count > 0)
      {
        Hashtable customProperties = PhotonNetwork.room.CustomProperties;
        if (customProperties != null && ((Dictionary<object, object>) customProperties).Count > 0)
          numArray = (byte[]) customProperties[(object) "json"];
      }
      Hashtable propertiesToSet = new Hashtable();
      ((Dictionary<object, object>) propertiesToSet).Add((object) "json", (object) numArray);
      ((Dictionary<object, object>) propertiesToSet).Add((object) "start", (object) true);
      ((Dictionary<object, object>) propertiesToSet).Add((object) "battle", (object) true);
      ((Dictionary<object, object>) propertiesToSet).Add((object) "draft", (object) false);
      PhotonNetwork.room.SetCustomProperties(propertiesToSet);
      if (GlobalVars.SelectedMultiPlayRoomType == JSON_MyPhotonRoomParam.EType.RAID)
        PhotonNetwork.room.IsVisible = false;
      return true;
    }

    public void ForceCloseRoom()
    {
      if (this.CurrentState != MyPhoton.MyState.ROOM)
        return;
      PhotonNetwork.room.IsOpen = false;
      byte[] numArray = (byte[]) null;
      if (((Dictionary<object, object>) PhotonNetwork.room.CustomProperties).Count > 0)
      {
        Hashtable customProperties = PhotonNetwork.room.CustomProperties;
        if (customProperties != null && ((Dictionary<object, object>) customProperties).Count > 0)
          numArray = (byte[]) customProperties[(object) "json"];
      }
      Hashtable hashtable = new Hashtable();
      ((Dictionary<object, object>) hashtable).Add((object) "json", (object) numArray);
      ((Dictionary<object, object>) hashtable).Add((object) "start", (object) false);
      ((Dictionary<object, object>) hashtable).Add((object) "battle", (object) false);
      ((Dictionary<object, object>) hashtable).Add((object) "draft", (object) false);
      Hashtable propertiesToSet = hashtable;
      PhotonNetwork.room.SetCustomProperties((Hashtable) null);
      PhotonNetwork.room.SetCustomProperties(propertiesToSet);
    }

    public void BattleStartRoom()
    {
      if (this.CurrentState != MyPhoton.MyState.ROOM)
      {
        this.LastError = MyPhoton.MyError.ILLEGAL_STATE;
      }
      else
      {
        if (!this.IsHost())
          return;
        Hashtable propertiesToSet = new Hashtable();
        ((Dictionary<object, object>) propertiesToSet).Add((object) "battle", (object) true);
        ((Dictionary<object, object>) propertiesToSet).Add((object) "draft", (object) true);
        PhotonNetwork.room.SetCustomProperties(propertiesToSet);
      }
    }

    public bool OpenRoom(bool isvisible = true, bool isstarted = false)
    {
      if (this.CurrentState != MyPhoton.MyState.ROOM)
      {
        this.LastError = MyPhoton.MyError.ILLEGAL_STATE;
        return false;
      }
      PhotonNetwork.room.IsOpen = true;
      PhotonNetwork.room.IsVisible = isvisible;
      byte[] numArray = (byte[]) null;
      if (((Dictionary<object, object>) PhotonNetwork.room.CustomProperties).Count > 0)
      {
        Hashtable customProperties = PhotonNetwork.room.CustomProperties;
        if (customProperties != null && ((Dictionary<object, object>) customProperties).Count > 0)
          numArray = (byte[]) customProperties[(object) "json"];
      }
      Hashtable hashtable = new Hashtable();
      ((Dictionary<object, object>) hashtable).Add((object) "json", (object) numArray);
      ((Dictionary<object, object>) hashtable).Add((object) "start", (object) isstarted);
      ((Dictionary<object, object>) hashtable).Add((object) "battle", (object) isstarted);
      Hashtable propertiesToSet = hashtable;
      PhotonNetwork.room.SetCustomProperties((Hashtable) null);
      PhotonNetwork.room.SetCustomProperties(propertiesToSet);
      return true;
    }

    public bool IsOldestPlayer(int playerID)
    {
      if (this.CurrentState != MyPhoton.MyState.ROOM)
        return false;
      bool flag = false;
      foreach (MyPhoton.MyPlayer roomPlayer in this.GetRoomPlayerList())
      {
        if (roomPlayer.playerID < playerID)
          return false;
        if (roomPlayer.playerID == playerID)
          flag = true;
      }
      return flag;
    }

    public bool IsOldestPlayer()
    {
      return this.CurrentState == MyPhoton.MyState.ROOM && this.IsOldestPlayer(this.GetMyPlayer().playerID);
    }

    public int GetOldestPlayer()
    {
      if (this.CurrentState != MyPhoton.MyState.ROOM)
        return 0;
      int oldestPlayer = 0;
      foreach (MyPhoton.MyPlayer roomPlayer in this.GetRoomPlayerList())
      {
        if ((roomPlayer.playerID < oldestPlayer || oldestPlayer == 0) && roomPlayer.start)
          oldestPlayer = roomPlayer.playerID;
      }
      return oldestPlayer;
    }

    public bool IsHost()
    {
      if (this.CurrentState != MyPhoton.MyState.ROOM)
        return false;
      MyPhoton.MyPlayer myPlayer1 = this.GetMyPlayer();
      List<MyPhoton.MyPlayer> roomPlayerList = this.GetRoomPlayerList();
      int photonPlayerId = myPlayer1.photonPlayerID;
      foreach (MyPhoton.MyPlayer myPlayer2 in roomPlayerList)
      {
        if (myPlayer2.photonPlayerID < photonPlayerId)
          return false;
      }
      return true;
    }

    public bool IsHost(int playerID)
    {
      if (this.CurrentState != MyPhoton.MyState.ROOM)
        return false;
      foreach (MyPhoton.MyPlayer roomPlayer in this.GetRoomPlayerList())
      {
        if (roomPlayer.playerID < playerID)
          return false;
      }
      return true;
    }

    public bool IsCreatedRoom()
    {
      return this.CurrentState == MyPhoton.MyState.ROOM && this.GetMyPlayer().playerID == 1;
    }

    public bool UseEncrypt { get; set; }

    [Obsolete("SendRoomMessageBinaryを使用してください", true)]
    public bool SendRoomMessage(bool reliable, string msg, MyPhoton.SEND_TYPE eventcode = MyPhoton.SEND_TYPE.Normal)
    {
      if (this.CurrentState != MyPhoton.MyState.ROOM)
        return false;
      int num = 0;
      Hashtable eventContent;
      if (num == 0)
      {
        Hashtable hashtable = new Hashtable();
        ((Dictionary<object, object>) hashtable).Add((object) "s", (object) num);
        ((Dictionary<object, object>) hashtable).Add((object) "m", (object) msg);
        eventContent = hashtable;
      }
      else
      {
        byte[] numArray = MyEncrypt.Encrypt(num + this.GetCryptKey(), msg, true);
        Hashtable hashtable = new Hashtable();
        ((Dictionary<object, object>) hashtable).Add((object) "s", (object) num);
        ((Dictionary<object, object>) hashtable).Add((object) "m", (object) numArray);
        eventContent = hashtable;
      }
      if (this.SortRoomMessage)
      {
        ((Dictionary<object, object>) eventContent).Add((object) "sq", (object) this.mSendRoomMessageID);
        ++this.mSendRoomMessageID;
      }
      bool flag = PhotonNetwork.RaiseEvent((byte) eventcode, (object) eventContent, reliable, (RaiseEventOptions) null);
      if (!this.DisconnectIfSendRoomMessageFailed || flag)
        return flag;
      this.Disconnect();
      this.LastError = MyPhoton.MyError.RAISE_EVENT_FAILED;
      this.LogError("SendRoomMessage failed!");
      return false;
    }

    public bool SendRoomMessageBinary(bool reliable, byte[] msg, MyPhoton.SEND_TYPE eventcode = MyPhoton.SEND_TYPE.Normal)
    {
      if (this.CurrentState != MyPhoton.MyState.ROOM)
        return false;
      byte[] numArray = MyEncrypt.Encrypt(msg);
      Hashtable hashtable = new Hashtable();
      ((Dictionary<object, object>) hashtable).Add((object) "bm", (object) numArray);
      Hashtable eventContent = hashtable;
      if (this.SortRoomMessage)
      {
        ((Dictionary<object, object>) eventContent).Add((object) "sq", (object) this.mSendRoomMessageID);
        ++this.mSendRoomMessageID;
      }
      bool flag = PhotonNetwork.RaiseEvent((byte) eventcode, (object) eventContent, reliable, (RaiseEventOptions) null);
      if (!this.DisconnectIfSendRoomMessageFailed || flag)
        return flag;
      this.Disconnect();
      this.LastError = MyPhoton.MyError.RAISE_EVENT_FAILED;
      this.LogError("SendRoomMessage failed!");
      return false;
    }

    public void SendFlush() => PhotonNetwork.SendOutgoingCommands();

    public List<MyPhoton.MyPlayer> GetRoomPlayerList(bool noSerialize = false)
    {
      List<MyPhoton.MyPlayer> roomPlayerList = new List<MyPhoton.MyPlayer>();
      foreach (PhotonPlayer player in PhotonNetwork.playerList)
      {
        MyPhoton.MyPlayer myPlayer = new MyPhoton.MyPlayer();
        myPlayer.playerID = player.ID;
        Hashtable customProperties = player.CustomProperties;
        if (customProperties != null && ((Dictionary<object, object>) customProperties).Count > 0)
        {
          GameUtility.Binary2Object<string>(out myPlayer.json, (byte[]) customProperties[(object) "json"]);
          if (((Dictionary<object, object>) customProperties).ContainsKey((object) "resumeID"))
            myPlayer.resumeID = (int) customProperties[(object) "resumeID"];
          if (((Dictionary<object, object>) customProperties).ContainsKey((object) "BattleStart"))
            myPlayer.start = (bool) customProperties[(object) "BattleStart"];
          if (((Dictionary<object, object>) customProperties).ContainsKey((object) "Logger"))
            continue;
        }
        roomPlayerList.Add(myPlayer);
      }
      bool flag = true;
      int photonPlayerId = this.GetMyPlayer().photonPlayerID;
      foreach (MyPhoton.MyPlayer myPlayer in roomPlayerList)
      {
        if (myPlayer.photonPlayerID < photonPlayerId)
          flag = false;
      }
      MyPhoton.MyRoom room = this.GetCurrentRoom();
      if (room != null && room.param != null && room.param.support != null)
      {
        for (int i = 0; i < room.param.support.Length; ++i)
        {
          if (!flag || !UnityEngine.Object.op_Inequality((UnityEngine.Object) RoomPlayerList.Instance, (UnityEngine.Object) null) || RoomPlayerList.Instance.MultiSupportList.Find((Predicate<MultiSupportData>) (s => s.UID == room.param.support[i].UID)) != null)
          {
            MyPhoton.MyPlayer myPlayer = new MyPhoton.MyPlayer()
            {
              playerID = room.param.support[i].playerID
            };
            if (noSerialize)
              myPlayer.param = room.param.support[i];
            else
              myPlayer.json = room.param.support[i].Serialize();
            roomPlayerList.Add(myPlayer);
          }
        }
      }
      return roomPlayerList;
    }

    public MyPhoton.MyPlayer FindPlayer(
      List<MyPhoton.MyPlayer> players,
      int playerID,
      int playerIndex)
    {
      MyPhoton.MyPlayer player = (MyPhoton.MyPlayer) null;
      if (players != null)
        player = players.Find((Predicate<MyPhoton.MyPlayer>) (p => p.resumeID == playerID || p.photonPlayerID == playerID));
      return player;
    }

    public List<JSON_MyPhotonPlayerParam> GetMyPlayersStarted() => this.mPlayersStarted;

    public int MyPlayerIndex { get; set; }

    public bool IsMultiPlay { get; set; }

    public bool IsMultiVersus { get; set; }

    public bool IsRankMatch { get; set; }

    public void Reset()
    {
      if (this.CurrentState != MyPhoton.MyState.NOP)
        this.Disconnect();
      this.MyPlayerIndex = 0;
      this.IsMultiPlay = false;
      this.IsMultiVersus = false;
      this.IsRankMatch = false;
      this.mPlayersStarted.Clear();
    }

    public void SetPlayersStarted(JSON_MyPhotonPlayerParam[] players)
    {
      this.mPlayersStarted.Clear();
      foreach (JSON_MyPhotonPlayerParam player in players)
      {
        player.SetupUnits();
        this.mPlayersStarted.Add(player);
      }
    }

    public void EnableKeepAlive(bool isMessageQueueRunning)
    {
      if (PhotonNetwork.isMessageQueueRunning != isMessageQueueRunning)
        this.Log("[PUN]KeepAlive changed to:" + (object) isMessageQueueRunning);
      PhotonNetwork.isMessageQueueRunning = isMessageQueueRunning;
    }

    public bool IsConnected() => PhotonNetwork.connected;

    public MyPhoton.MyRoom SearchRoom(int roomID)
    {
      MyPhoton.MyRoom myRoom = (MyPhoton.MyRoom) null;
      int selectedMultiPlayRoomId = GlobalVars.SelectedMultiPlayRoomID;
      if (this.CurrentState != MyPhoton.MyState.LOBBY)
        return (MyPhoton.MyRoom) null;
      List<MyPhoton.MyRoom> roomList = this.GetRoomList();
      for (int index = 0; index < roomList.Count; ++index)
      {
        if (!(roomList[index].lobby != "vs") && roomList[index].name.IndexOf("_friend") != -1 && !string.IsNullOrEmpty(roomList[index].json))
        {
          JSON_MyPhotonRoomParam myPhotonRoomParam = JSON_MyPhotonRoomParam.Parse(roomList[index].json);
          if (myPhotonRoomParam != null && myPhotonRoomParam.roomid == selectedMultiPlayRoomId && myPhotonRoomParam.btlver == MonoSingleton<GameManager>.Instance.BattleVersion)
          {
            myRoom = roomList[index];
            break;
          }
        }
      }
      return myRoom;
    }

    public bool IsConnectedInRoom()
    {
      return PhotonNetwork.connected && this.CurrentState == MyPhoton.MyState.ROOM;
    }

    public bool IsBattle(string roomname)
    {
      bool flag1 = false;
      if (this.CurrentState != MyPhoton.MyState.LOBBY)
        return false;
      List<MyPhoton.MyRoom> roomList = this.GetRoomList();
      for (int index1 = 0; index1 < roomList.Count; ++index1)
      {
        if (!(roomList[index1].lobby != "tower") && roomList[index1].name.Equals(roomname))
        {
          flag1 = roomList[index1].battle;
          Debug.Log((object) ("Room Name : " + roomname + " Room is found"));
          if (!string.IsNullOrEmpty(roomList[index1].json))
          {
            JSON_MyPhotonRoomParam myPhotonRoomParam = JSON_MyPhotonRoomParam.Parse(roomList[index1].json);
            if (myPhotonRoomParam != null && myPhotonRoomParam.players != null)
            {
              bool flag2 = false;
              string fuid = MonoSingleton<GameManager>.Instance.Player.FUID;
              for (int index2 = 0; index2 < myPhotonRoomParam.players.Length; ++index2)
              {
                if (myPhotonRoomParam.players[index2].FUID == fuid)
                {
                  flag2 = true;
                  break;
                }
              }
              flag1 &= flag2;
              break;
            }
            break;
          }
          break;
        }
      }
      return flag1;
    }

    public bool CheckTowerRoomIsBattle(string roomname)
    {
      if (this.CurrentState != MyPhoton.MyState.LOBBY)
        return false;
      MyPhoton.MyRoom myRoom = this.GetRoomList().Find((Predicate<MyPhoton.MyRoom>) (r => r.lobby == "tower" && r.name == roomname));
      Debug.Log((object) ("Room Name Serch: " + roomname));
      if (myRoom == null)
        return false;
      Debug.Log((object) ("room.name : " + myRoom.name));
      if (!myRoom.battle)
        return false;
      Debug.Log((object) ("Room Name : " + roomname + " Room is found"));
      if (string.IsNullOrEmpty(myRoom.json))
        return true;
      JSON_MyPhotonRoomParam myPhotonRoomParam = JSON_MyPhotonRoomParam.Parse(myRoom.json);
      if (myPhotonRoomParam == null || myPhotonRoomParam.players == null)
        return true;
      string fuid = MonoSingleton<GameManager>.Instance.Player.FUID;
      for (int index = 0; index < myPhotonRoomParam.players.Length; ++index)
      {
        if (myPhotonRoomParam.players[index].FUID == fuid)
          return true;
      }
      Debug.LogError((object) "Room is not battle.");
      return false;
    }

    public void KickMember(int playerId)
    {
      if (!PhotonNetwork.connected || !this.IsHost())
        return;
      PhotonPlayer kickPlayer = (PhotonPlayer) null;
      for (int index = 0; index < PhotonNetwork.playerList.Length; ++index)
      {
        if (playerId == PhotonNetwork.playerList[index].ID)
          kickPlayer = PhotonNetwork.playerList[index];
      }
      if (kickPlayer == null)
        return;
      PhotonNetwork.CloseConnection(kickPlayer);
    }

    public void KickMember(MyPhoton.MyPlayer target)
    {
      if (target == null)
        return;
      this.KickMember(target.playerID);
    }

    public static void PhotonSendLog(string status, string msg)
    {
      if (string.IsNullOrEmpty(MonoSingleton<GameManager>.Instance.DeviceId) || string.IsNullOrEmpty(status) && string.IsNullOrEmpty(msg))
        return;
      FlowNode_SendLogMessage.SendLogGenerator sendLogGenerator = new FlowNode_SendLogMessage.SendLogGenerator();
      if (!string.IsNullOrEmpty(status))
        sendLogGenerator.Add("Status", status);
      if (!string.IsNullOrEmpty(msg))
        sendLogGenerator.Add(nameof (msg), msg);
      sendLogGenerator.AddCommon(true, false, false, true);
      LogKit.Logger.CreateLogger("application").Post(nameof (PhotonSendLog), LogKit.LogLevel.Info, sendLogGenerator.GetSendMessage());
    }

    private void Log(string str)
    {
      if (!GameUtility.IsDebugBuild)
        return;
      DebugUtility.Log(str);
    }

    private void LogWarning(string str)
    {
      MyPhoton.PhotonSendLog(nameof (LogWarning), str);
      if (!GameUtility.IsDebugBuild)
        return;
      DebugUtility.LogWarning(str);
    }

    private void LogError(string str)
    {
      MyPhoton.PhotonSendLog(nameof (LogError), str);
      if (!GameUtility.IsDebugBuild)
        return;
      DebugUtility.LogError(str);
    }

    public enum MyState
    {
      NOP,
      CONNECTING,
      LOBBY,
      JOINING,
      ROOM,
      LEAVING,
      NUM,
    }

    public enum MyError
    {
      NOP,
      UNKNOWN,
      ILLEGAL_STATE,
      TIMEOUT,
      TIMEOUT2,
      FULL_CLIENTS,
      ROOM_NAME_DUPLICATED,
      ROOM_IS_FULL,
      ROOM_IS_NOT_EXIST,
      ROOM_IS_NOT_OPEN,
      RAISE_EVENT_FAILED,
      NUM,
    }

    public enum SEND_TYPE : byte
    {
      Normal,
      Resume,
      Sync,
    }

    public class MyEvent
    {
      public MyPhoton.SEND_TYPE code;
      public int playerID;
      public string json;
      public byte[] binary;
      public int sendID;
    }

    public class MyRoom
    {
      public string name = string.Empty;
      public int playerCount;
      public int maxPlayers = 1;
      public bool open = true;
      public bool start;
      public bool battle;
      public bool draft;
      public string json = string.Empty;
      public string lobby = string.Empty;
      public int audience;
      public int audienceMax;
      public JSON_MyPhotonRoomParam param;
    }

    public class MyPlayer
    {
      public int photonPlayerID;
      public int resumeID = -1;
      public string json = string.Empty;
      public bool start;
      public JSON_MyPhotonPlayerParam param;

      public int playerID
      {
        get => this.resumeID != -1 ? this.resumeID : this.photonPlayerID;
        set => this.photonPlayerID = value;
      }
    }
  }
}
