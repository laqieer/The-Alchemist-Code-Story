// Decompiled with JetBrains decompiler
// Type: SRPG.MyPhoton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using ExitGames.Client.Photon;
using GR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SRPG
{
  public class MyPhoton : PunMonoSingleton<MyPhoton>
  {
    public static readonly int MAX_PLAYER_NUM = 10;
    public static readonly int TIMEOUT_SECOND = 30;
    public static readonly int SEND_RATE = 30;
    private float mDelaySec = -1f;
    private List<MyPhoton.MyEvent> mEvents = new List<MyPhoton.MyEvent>();
    private List<JSON_MyPhotonPlayerParam> mPlayersStarted = new List<JSON_MyPhotonPlayerParam>();
    private const string STARTED_ROOM = "start";
    private const string BATTLESTART_ROOM = "battle";
    private const string DRAFT_BATTLESTART_ROOM = "draft";
    private MyPhoton.MyState mState;
    private bool mIsRoomListUpdated;
    private bool mIsUpdateRoomProperty;
    private bool mIsUpdatePlayerProperty;
    private MyPhoton.MyError mError;
    private NetworkReachability mNetworkReach;
    private int mSendRoomMessageID;

    public MyPhoton.MyState CurrentState
    {
      get
      {
        return this.mState;
      }
    }

    public bool IsRoomListUpdated
    {
      get
      {
        return this.mIsRoomListUpdated;
      }
      set
      {
        this.mIsRoomListUpdated = value;
      }
    }

    public bool IsUpdateRoomProperty
    {
      get
      {
        return this.mIsUpdateRoomProperty;
      }
      set
      {
        this.mIsUpdateRoomProperty = value;
      }
    }

    public bool IsUpdatePlayerProperty
    {
      get
      {
        return this.mIsUpdatePlayerProperty;
      }
      set
      {
        this.mIsUpdatePlayerProperty = value;
      }
    }

    public MyPhoton.MyError LastError
    {
      get
      {
        return this.mError;
      }
    }

    public void ResetLastError()
    {
      this.mError = MyPhoton.MyError.NOP;
    }

    private void Log(string str)
    {
      if (!GameUtility.IsDebugBuild)
        return;
      DebugUtility.Log(str);
    }

    private void LogWarning(string str)
    {
      if (!GameUtility.IsDebugBuild)
        return;
      DebugUtility.LogWarning(str);
    }

    private void LogError(string str)
    {
      if (!GameUtility.IsDebugBuild)
        return;
      DebugUtility.LogError(str);
    }

    public float TimeOutSec { get; set; }

    public bool SendRoomMessageFlush { get; set; }

    public bool DisconnectIfSendRoomMessageFailed { get; set; }

    public bool SortRoomMessage { get; set; }

    public PeerStateValue ConnectState
    {
      get
      {
        return PhotonNetwork.networkingPeer.PeerState;
      }
    }

    public bool IsDisconnected()
    {
      return this.ConnectState == PeerStateValue.Disconnected;
    }

    protected override void Initialize()
    {
      UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) this);
      if (GameUtility.IsDebugBuild)
        this.gameObject.AddComponent<PhotonLagSimulationGui>().Visible = false;
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
      return "lastrecv:" + (object) (SupportClass.GetTickCount() - PhotonNetwork.networkingPeer.TimestampOfLastSocketReceive) + " og:" + PhotonNetwork.networkingPeer.TrafficStatsOutgoing.ToString() + " ic:" + PhotonNetwork.networkingPeer.TrafficStatsIncoming.ToString();
    }

    private void Update()
    {
      if (this.mState == MyPhoton.MyState.NOP)
        return;
      NetworkReachability internetReachability = Application.internetReachability;
      if (this.mState != MyPhoton.MyState.NOP && internetReachability != this.mNetworkReach && this.mNetworkReach != NetworkReachability.NotReachable)
        this.LogWarning("internet reach change to " + (object) internetReachability + "\n" + this.GetTrafficState());
      this.mNetworkReach = internetReachability;
      if (this.mState != MyPhoton.MyState.ROOM)
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
        this.mError = MyPhoton.MyError.TIMEOUT2;
      }
    }

    public string CurrentAppID { get; private set; }

    public bool StartConnect(string appID, bool autoJoin = false, string ver = "1.0")
    {
      if (this.mState != MyPhoton.MyState.NOP)
      {
        this.mError = MyPhoton.MyError.ILLEGAL_STATE;
        return false;
      }
      this.CurrentAppID = appID;
      PhotonNetwork.autoJoinLobby = autoJoin;
      PhotonNetwork.PhotonServerSettings.AppID = appID;
      PhotonNetwork.PhotonServerSettings.Protocol = ConnectionProtocol.Tcp;
      PhotonNetwork.player.NickName = MonoSingleton<GameManager>.Instance.DeviceId;
      bool flag = PhotonNetwork.ConnectUsingSettings(ver);
      if (flag)
      {
        this.mState = MyPhoton.MyState.CONNECTING;
        PhotonNetwork.NetworkStatisticsEnabled = true;
      }
      else
      {
        this.mState = MyPhoton.MyState.NOP;
        this.mError = MyPhoton.MyError.UNKNOWN;
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
        this.Log("WebRPC '" + webRpcResponse.Name + "' に失敗しました. Error: " + (object) webRpcResponse.ReturnCode + " Message: " + webRpcResponse.DebugMessage);
      foreach (KeyValuePair<string, object> parameter in webRpcResponse.Parameters)
        this.Log("Key:" + parameter.Key + "/ Value:" + parameter.Value);
    }

    public override void OnConnectedToPhoton()
    {
      this.Log("Connected to Photon Server");
      this.Log(PhotonNetwork.ServerAddress);
    }

    public override void OnDisconnectedFromPhoton()
    {
      this.Log("DisconnectedFromPhoton. LostPacket:" + (object) PhotonNetwork.PacketLossByCrcCheck + " MaxResendsBeforeDisconnect:" + (object) PhotonNetwork.MaxResendsBeforeDisconnect + " ResentReliableCommands" + (object) PhotonNetwork.ResentReliableCommands);
      this.mState = MyPhoton.MyState.NOP;
    }

    public override void OnFailedToConnectToPhoton(DisconnectCause cause)
    {
      this.Log("FailedToConnectToPhoton." + cause.ToString());
      if (cause == DisconnectCause.DisconnectByClientTimeout || cause == DisconnectCause.DisconnectByServerTimeout)
        this.mError = MyPhoton.MyError.TIMEOUT;
      if (cause == DisconnectCause.DisconnectByServerUserLimit)
        this.mError = MyPhoton.MyError.FULL_CLIENTS;
      this.mState = MyPhoton.MyState.NOP;
    }

    public override void OnConnectionFail(DisconnectCause cause)
    {
      this.Log("ConnectionFail." + cause.ToString());
      if (cause == DisconnectCause.DisconnectByClientTimeout || cause == DisconnectCause.DisconnectByServerTimeout)
        this.mError = MyPhoton.MyError.TIMEOUT;
      if (cause == DisconnectCause.DisconnectByServerUserLimit)
        this.mError = MyPhoton.MyError.FULL_CLIENTS;
      this.mState = MyPhoton.MyState.NOP;
    }

    public override void OnConnectedToMaster()
    {
      this.Log("Joined Default Lobby.");
      this.mState = MyPhoton.MyState.LOBBY;
      this.mEvents.Clear();
      this.IsRoomListUpdated = false;
      this.Log(PhotonNetwork.ServerAddress);
    }

    public override void OnJoinedLobby()
    {
      this.Log("Joined Lobby.");
      this.mState = MyPhoton.MyState.LOBBY;
      this.mEvents.Clear();
      this.IsRoomListUpdated = false;
      this.Log(PhotonNetwork.ServerAddress);
    }

    public override void OnReceivedRoomListUpdate()
    {
      this.Log("Room List Updated.");
      this.mIsRoomListUpdated = true;
    }

    public override void OnPhotonCreateRoomFailed(object[] codeAndMsg)
    {
      this.Log("Create Room failed.");
      if (codeAndMsg == null || codeAndMsg.Length < 2 || !(codeAndMsg[0] is IConvertible))
      {
        this.mError = MyPhoton.MyError.UNKNOWN;
        this.Log("codeAndMsg is null");
      }
      else
      {
        this.mError = ((IConvertible) codeAndMsg[0]).ToInt32((IFormatProvider) null) != 32766 ? MyPhoton.MyError.UNKNOWN : MyPhoton.MyError.ROOM_NAME_DUPLICATED;
        string str = (string) codeAndMsg[1];
        if (str != null)
          this.Log("err:" + str);
      }
      this.mState = MyPhoton.MyState.LOBBY;
    }

    public override void OnJoinedRoom()
    {
      this.Log("Joined Room.");
      this.mEvents.Clear();
      this.mState = MyPhoton.MyState.ROOM;
      this.mSendRoomMessageID = 0;
    }

    public override void OnPhotonJoinRoomFailed(object[] codeAndMsg)
    {
      this.Log("Join Room failed.");
      if (codeAndMsg == null || codeAndMsg.Length < 2 || !(codeAndMsg[0] is IConvertible))
      {
        this.mError = MyPhoton.MyError.UNKNOWN;
      }
      else
      {
        switch (((IConvertible) codeAndMsg[0]).ToInt32((IFormatProvider) null))
        {
          case 32758:
            this.mError = MyPhoton.MyError.ROOM_IS_NOT_EXIST;
            break;
          case 32764:
            this.mError = MyPhoton.MyError.ROOM_IS_NOT_OPEN;
            break;
          case 32765:
            this.mError = MyPhoton.MyError.ROOM_IS_FULL;
            break;
          default:
            this.mError = MyPhoton.MyError.UNKNOWN;
            break;
        }
        string str = (string) codeAndMsg[1];
        if (str != null)
          this.Log("err:" + str);
      }
      this.mState = MyPhoton.MyState.LOBBY;
    }

    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    {
      this.Log("Join Room failed.");
      if (codeAndMsg == null || codeAndMsg.Length < 2 || !(codeAndMsg[0] is IConvertible))
      {
        this.mError = MyPhoton.MyError.UNKNOWN;
      }
      else
      {
        this.mError = ((IConvertible) codeAndMsg[0]).ToInt32((IFormatProvider) null) != 32760 ? MyPhoton.MyError.UNKNOWN : MyPhoton.MyError.ROOM_IS_NOT_EXIST;
        string str = (string) codeAndMsg[1];
        if (str != null)
          this.Log("err:" + str);
      }
      this.mState = MyPhoton.MyState.LOBBY;
    }

    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
      if (GlobalVars.SelectedMultiPlayRoomType == JSON_MyPhotonRoomParam.EType.VERSUS && PhotonNetwork.isMasterClient)
      {
        Hashtable customProperties = newPlayer.CustomProperties;
        if (customProperties != null && customProperties.ContainsKey((object) "Logger") && PhotonNetwork.room != null)
          PhotonNetwork.room.IsVisible = true;
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
      this.mState = MyPhoton.MyState.LOBBY;
      this.mEvents.Clear();
    }

    public override void OnPhotonCustomRoomPropertiesChanged(Hashtable propertiesThatChanged)
    {
      this.Log("Update Room Property");
      this.mIsUpdateRoomProperty = true;
      if (!propertiesThatChanged.ContainsKey((object) "Audience") || !this.IsOldestPlayer())
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
      int num = 0;
      foreach (char ch in currentRoom.name)
        num += (int) ch;
      return num;
    }

    public List<MyPhoton.MyEvent> GetEvents()
    {
      return this.mEvents;
    }

    private void OnEventHandler(byte eventCode, object content, int senderID)
    {
      Hashtable hashtable = (Hashtable) content;
      string str = (string) null;
      if (hashtable.ContainsKey((object) "s"))
      {
        int num = (int) hashtable[(object) "s"];
        if (num == 0)
        {
          if (hashtable.ContainsKey((object) "m"))
            str = (string) hashtable[(object) "m"];
        }
        else if (hashtable.ContainsKey((object) "m"))
        {
          byte[] data = (byte[]) hashtable[(object) "m"];
          str = MyEncrypt.Decrypt(num + this.GetCryptKey(), data, true);
        }
      }
      byte[] numArray = (byte[]) null;
      if (hashtable.ContainsKey((object) "bm"))
        numArray = MyEncrypt.Decrypt((byte[]) hashtable[(object) "bm"]);
      MyPhoton.MyEvent myEvent = new MyPhoton.MyEvent();
      myEvent.code = (MyPhoton.SEND_TYPE) eventCode;
      myEvent.playerID = senderID;
      myEvent.json = str;
      myEvent.binary = numArray;
      if (hashtable.ContainsKey((object) "sq"))
        myEvent.sendID = (int) hashtable[(object) "sq"];
      this.mEvents.Add(myEvent);
      if (this.SortRoomMessage)
        this.mEvents.Sort((Comparison<MyPhoton.MyEvent>) ((a, b) => a.sendID - b.sendID));
      this.Log("OnEventHandler: " + (object) senderID + (string) hashtable[(object) "msg"]);
    }

    public List<MyPhoton.MyRoom> GetRoomList()
    {
      List<MyPhoton.MyRoom> myRoomList = new List<MyPhoton.MyRoom>();
      foreach (global::RoomInfo room in PhotonNetwork.GetRoomList())
      {
        MyPhoton.MyRoom myRoom = new MyPhoton.MyRoom();
        myRoom.name = room.Name;
        myRoom.playerCount = room.PlayerCount;
        myRoom.maxPlayers = (int) room.MaxPlayers;
        myRoom.open = room.IsOpen;
        Hashtable customProperties = room.CustomProperties;
        if (customProperties != null && customProperties.Count > 0)
        {
          GameUtility.Binary2Object<string>(out myRoom.json, (byte[]) customProperties[(object) "json"]);
          if (customProperties.ContainsKey((object) "lobby"))
            myRoom.lobby = (string) customProperties[(object) "lobby"];
          if (customProperties.ContainsKey((object) "Audience"))
            int.TryParse(customProperties[(object) "Audience"].ToString(), out myRoom.audience);
          if (customProperties.ContainsKey((object) "AudienceMax"))
            myRoom.audienceMax = (int) customProperties[(object) "AudienceMax"];
          if (customProperties.ContainsKey((object) "start"))
            myRoom.start = (bool) customProperties[(object) "start"];
          if (customProperties.ContainsKey((object) "battle"))
            myRoom.battle = (bool) customProperties[(object) "battle"];
          if (customProperties.ContainsKey((object) "draft"))
            myRoom.draft = (bool) customProperties[(object) "draft"];
        }
        myRoomList.Add(myRoom);
      }
      return myRoomList;
    }

    public MyPhoton.MyPlayer GetMyPlayer()
    {
      Hashtable customProperties = PhotonNetwork.player.CustomProperties;
      MyPhoton.MyPlayer myPlayer = new MyPhoton.MyPlayer();
      myPlayer.photonPlayerID = PhotonNetwork.player.ID;
      if (customProperties != null && customProperties.Count > 0)
      {
        GameUtility.Binary2Object<string>(out myPlayer.json, (byte[]) customProperties[(object) "json"]);
        if (customProperties.ContainsKey((object) "resumeID"))
          myPlayer.resumeID = (int) customProperties[(object) "resumeID"];
      }
      return myPlayer;
    }

    public void SetMyPlayerParam(string json)
    {
      Hashtable propertiesToSet = new Hashtable();
      propertiesToSet.Add((object) nameof (json), (object) GameUtility.Object2Binary<string>(json));
      PhotonNetwork.player.SetCustomProperties(propertiesToSet, (Hashtable) null, false);
    }

    public void SetResumeMyPlayer(int playerID = 0)
    {
      if (this.GetMyPlayer() == null)
        return;
      byte[] numArray = (byte[]) null;
      Hashtable customProperties = PhotonNetwork.player.CustomProperties;
      if (customProperties != null && customProperties.ContainsKey((object) "json"))
        numArray = (byte[]) customProperties[(object) "json"];
      Hashtable propertiesToSet = new Hashtable();
      propertiesToSet.Add((object) "json", (object) numArray);
      propertiesToSet.Add((object) "resume", (object) true);
      propertiesToSet.Add((object) "resumeID", (object) playerID);
      PhotonNetwork.player.SetCustomProperties(propertiesToSet, (Hashtable) null, false);
    }

    public void AddMyPlayerParam(string key, object param)
    {
      if (this.GetMyPlayer() == null)
        return;
      Hashtable customProperties = PhotonNetwork.player.CustomProperties;
      if (!customProperties.ContainsKey((object) key))
        customProperties.Add((object) key, param);
      else
        customProperties[(object) key] = param;
      PhotonNetwork.player.SetCustomProperties(customProperties, (Hashtable) null, false);
    }

    public bool IsResume()
    {
      if (this.GetMyPlayer() != null)
      {
        Hashtable customProperties = PhotonNetwork.player.CustomProperties;
        if (customProperties != null && customProperties.ContainsKey((object) "resume"))
          return (bool) customProperties[(object) "resume"];
      }
      return false;
    }

    public bool CreateRoom(int maxPlayerNum, string roomName, string roomJson, string playerJson, string MatchKey = null, int floor = -1, int plv = -1, string luid = null, string uid = null, int audMax = -1, bool isTower = false)
    {
      if (this.mState != MyPhoton.MyState.LOBBY)
      {
        this.mError = MyPhoton.MyError.ILLEGAL_STATE;
        return false;
      }
      PhotonNetwork.player.SetCustomProperties((Hashtable) null, (Hashtable) null, false);
      PhotonNetwork.SetPlayerCustomProperties((Hashtable) null);
      this.SetMyPlayerParam(playerJson);
      RoomOptions roomOptions1 = new RoomOptions();
      roomOptions1.MaxPlayers = (byte) maxPlayerNum;
      roomOptions1.IsVisible = GlobalVars.SelectedMultiPlayRoomType != JSON_MyPhotonRoomParam.EType.VERSUS;
      roomOptions1.IsOpen = true;
      RoomOptions roomOptions2 = roomOptions1;
      Hashtable hashtable1 = new Hashtable();
      hashtable1.Add((object) "json", (object) GameUtility.Object2Binary<string>(roomJson));
      hashtable1.Add((object) "name", (object) roomName);
      hashtable1.Add((object) "start", (object) false);
      hashtable1.Add((object) "battle", (object) false);
      hashtable1.Add((object) "draft", (object) false);
      Hashtable hashtable2 = hashtable1;
      roomOptions2.CustomRoomProperties = hashtable2;
      roomOptions1.CustomRoomPropertiesForLobby = new string[5]
      {
        "json",
        "name",
        "start",
        "battle",
        "draft"
      };
      if (isTower && !string.IsNullOrEmpty(MatchKey))
      {
        int length = roomOptions1.CustomRoomPropertiesForLobby.Length;
        int num1 = roomOptions1.CustomRoomPropertiesForLobby.Length + 3;
        int num2;
        int num3 = floor == -1 ? num1 : (num2 = num1 + 1);
        int num4 = GlobalVars.BlockList == null || GlobalVars.BlockList.Count <= 0 ? num3 : (num2 = num3 + 1);
        int newSize = string.IsNullOrEmpty(uid) ? num4 : (num2 = num4 + 1);
        Array.Resize<string>(ref roomOptions1.CustomRoomPropertiesForLobby, newSize);
        roomOptions1.CustomRoomProperties.Add((object) "lobby", (object) "tower");
        roomOptions1.CustomRoomPropertiesForLobby[roomOptions1.CustomRoomPropertiesForLobby.Length - 1] = "lobby";
        roomOptions1.CustomRoomProperties.Add((object) "MatchType", (object) MatchKey);
        string[] propertiesForLobby1 = roomOptions1.CustomRoomPropertiesForLobby;
        int index1 = length;
        int num5 = index1 + 1;
        string str1 = "MatchType";
        propertiesForLobby1[index1] = str1;
        roomOptions1.CustomRoomProperties.Add((object) "Lock", (object) (GlobalVars.EditMultiPlayRoomPassCode != "0"));
        string[] propertiesForLobby2 = roomOptions1.CustomRoomPropertiesForLobby;
        int index2 = num5;
        int num6 = index2 + 1;
        string str2 = "Lock";
        propertiesForLobby2[index2] = str2;
        if (floor != -1)
        {
          roomOptions1.CustomRoomProperties.Add((object) nameof (floor), (object) floor);
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
          roomOptions1.CustomRoomProperties.Add((object) "BlockList", (object) stringBuilder.ToString());
          roomOptions1.CustomRoomPropertiesForLobby[num6++] = "BlockList";
        }
        if (!string.IsNullOrEmpty(uid))
        {
          roomOptions1.CustomRoomProperties.Add((object) nameof (uid), (object) uid);
          string[] propertiesForLobby3 = roomOptions1.CustomRoomPropertiesForLobby;
          int index3 = num6;
          int num7 = index3 + 1;
          string str3 = nameof (uid);
          propertiesForLobby3[index3] = str3;
        }
      }
      else if (!string.IsNullOrEmpty(MatchKey))
      {
        int length = roomOptions1.CustomRoomPropertiesForLobby.Length;
        int num1 = roomOptions1.CustomRoomPropertiesForLobby.Length + 3;
        int num2;
        int num3 = plv == -1 ? num1 : (num2 = num1 + 1);
        int num4 = floor == -1 ? num3 : (num2 = num3 + 1);
        int num5 = string.IsNullOrEmpty(luid) ? num4 : (num2 = num4 + 1);
        int num6 = string.IsNullOrEmpty(uid) ? num5 : (num2 = num5 + 1);
        int newSize = audMax == -1 ? num6 : (num2 = num6 + 1);
        Array.Resize<string>(ref roomOptions1.CustomRoomPropertiesForLobby, newSize);
        roomOptions1.CustomRoomProperties.Add((object) "MatchType", (object) MatchKey);
        string[] propertiesForLobby1 = roomOptions1.CustomRoomPropertiesForLobby;
        int index1 = length;
        int num7 = index1 + 1;
        string str1 = "MatchType";
        propertiesForLobby1[index1] = str1;
        roomOptions1.CustomRoomProperties.Add((object) "lobby", (object) "vs");
        string[] propertiesForLobby2 = roomOptions1.CustomRoomPropertiesForLobby;
        int index2 = num7;
        int num8 = index2 + 1;
        string str2 = "lobby";
        propertiesForLobby2[index2] = str2;
        roomOptions1.CustomRoomProperties.Add((object) "Audience", (object) "0");
        string[] propertiesForLobby3 = roomOptions1.CustomRoomPropertiesForLobby;
        int index3 = num8;
        int num9 = index3 + 1;
        string str3 = "Audience";
        propertiesForLobby3[index3] = str3;
        if (plv != -1)
        {
          roomOptions1.CustomRoomProperties.Add((object) nameof (plv), (object) plv);
          roomOptions1.CustomRoomPropertiesForLobby[num9++] = nameof (plv);
        }
        if (floor != -1)
        {
          roomOptions1.CustomRoomProperties.Add((object) nameof (floor), (object) floor);
          roomOptions1.CustomRoomPropertiesForLobby[num9++] = nameof (floor);
        }
        if (!string.IsNullOrEmpty(luid))
        {
          roomOptions1.CustomRoomProperties.Add((object) nameof (luid), (object) luid);
          roomOptions1.CustomRoomPropertiesForLobby[num9++] = nameof (luid);
        }
        if (!string.IsNullOrEmpty(uid))
        {
          roomOptions1.CustomRoomProperties.Add((object) nameof (uid), (object) uid);
          roomOptions1.CustomRoomPropertiesForLobby[num9++] = nameof (uid);
        }
        if (audMax > 0)
        {
          roomOptions1.CustomRoomProperties.Add((object) "AudienceMax", (object) audMax);
          string[] propertiesForLobby4 = roomOptions1.CustomRoomPropertiesForLobby;
          int index4 = num9;
          int num10 = index4 + 1;
          string str4 = "AudienceMax";
          propertiesForLobby4[index4] = str4;
        }
      }
      else
      {
        roomOptions1.CustomRoomProperties.Add((object) "lobby", (object) "coop");
        Array.Resize<string>(ref roomOptions1.CustomRoomPropertiesForLobby, roomOptions1.CustomRoomPropertiesForLobby.Length + 1);
        roomOptions1.CustomRoomPropertiesForLobby[roomOptions1.CustomRoomPropertiesForLobby.Length - 1] = "lobby";
      }
      bool room = PhotonNetwork.CreateRoom(roomName, roomOptions1, (TypedLobby) null);
      if (room)
        this.mState = MyPhoton.MyState.JOINING;
      else
        this.mError = MyPhoton.MyError.UNKNOWN;
      return room;
    }

    public bool CreateRoom(string roomName, string roomJson, string playerJson, int plv, string uid, int score, int type)
    {
      if (this.mState != MyPhoton.MyState.LOBBY)
      {
        this.mError = MyPhoton.MyError.ILLEGAL_STATE;
        return false;
      }
      PhotonNetwork.player.SetCustomProperties((Hashtable) null, (Hashtable) null, false);
      PhotonNetwork.SetPlayerCustomProperties((Hashtable) null);
      this.SetMyPlayerParam(playerJson);
      RoomOptions roomOptions1 = new RoomOptions();
      roomOptions1.MaxPlayers = (byte) 3;
      roomOptions1.IsVisible = true;
      roomOptions1.IsOpen = true;
      RoomOptions roomOptions2 = roomOptions1;
      Hashtable hashtable1 = new Hashtable();
      hashtable1.Add((object) "json", (object) GameUtility.Object2Binary<string>(roomJson));
      hashtable1.Add((object) "name", (object) roomName);
      hashtable1.Add((object) "start", (object) false);
      hashtable1.Add((object) "battle", (object) false);
      hashtable1.Add((object) "draft", (object) false);
      Hashtable hashtable2 = hashtable1;
      roomOptions2.CustomRoomProperties = hashtable2;
      roomOptions1.CustomRoomPropertiesForLobby = new string[4]
      {
        "json",
        "name",
        "start",
        "draft"
      };
      int length = roomOptions1.CustomRoomPropertiesForLobby.Length;
      int newSize = roomOptions1.CustomRoomPropertiesForLobby.Length + 7;
      Array.Resize<string>(ref roomOptions1.CustomRoomPropertiesForLobby, newSize);
      roomOptions1.CustomRoomProperties.Add((object) "MatchType", (object) MonoSingleton<GameManager>.Instance.GetVersusKey(VERSUS_TYPE.RankMatch));
      string[] propertiesForLobby1 = roomOptions1.CustomRoomPropertiesForLobby;
      int index1 = length;
      int num1 = index1 + 1;
      string str1 = "MatchType";
      propertiesForLobby1[index1] = str1;
      roomOptions1.CustomRoomProperties.Add((object) "lobby", (object) "vs");
      string[] propertiesForLobby2 = roomOptions1.CustomRoomPropertiesForLobby;
      int index2 = num1;
      int num2 = index2 + 1;
      string str2 = "lobby";
      propertiesForLobby2[index2] = str2;
      roomOptions1.CustomRoomProperties.Add((object) "Audience", (object) "0");
      string[] propertiesForLobby3 = roomOptions1.CustomRoomPropertiesForLobby;
      int index3 = num2;
      int num3 = index3 + 1;
      string str3 = "Audience";
      propertiesForLobby3[index3] = str3;
      roomOptions1.CustomRoomProperties.Add((object) nameof (plv), (object) plv);
      string[] propertiesForLobby4 = roomOptions1.CustomRoomPropertiesForLobby;
      int index4 = num3;
      int num4 = index4 + 1;
      string str4 = nameof (plv);
      propertiesForLobby4[index4] = str4;
      roomOptions1.CustomRoomProperties.Add((object) nameof (score), (object) score);
      string[] propertiesForLobby5 = roomOptions1.CustomRoomPropertiesForLobby;
      int index5 = num4;
      int num5 = index5 + 1;
      string str5 = nameof (score);
      propertiesForLobby5[index5] = str5;
      roomOptions1.CustomRoomProperties.Add((object) nameof (type), (object) type);
      string[] propertiesForLobby6 = roomOptions1.CustomRoomPropertiesForLobby;
      int index6 = num5;
      int num6 = index6 + 1;
      string str6 = nameof (type);
      propertiesForLobby6[index6] = str6;
      roomOptions1.CustomRoomProperties.Add((object) nameof (uid), (object) uid);
      string[] propertiesForLobby7 = roomOptions1.CustomRoomPropertiesForLobby;
      int index7 = num6;
      int num7 = index7 + 1;
      string str7 = nameof (uid);
      propertiesForLobby7[index7] = str7;
      bool room = PhotonNetwork.CreateRoom(roomName, roomOptions1, (TypedLobby) null);
      if (room)
        this.mState = MyPhoton.MyState.JOINING;
      else
        this.mError = MyPhoton.MyError.UNKNOWN;
      return room;
    }

    public bool JoinRoom(string roomName, string playerJson, bool isResume = false)
    {
      if (this.mState != MyPhoton.MyState.LOBBY)
      {
        this.mError = MyPhoton.MyError.ILLEGAL_STATE;
        return false;
      }
      PhotonNetwork.player.SetCustomProperties((Hashtable) null, (Hashtable) null, false);
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
        expectedCustomRoomProperties.Add((object) "name", (object) roomName);
        expectedCustomRoomProperties.Add((object) "start", (object) false);
        flag = PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties, (byte) 0);
      }
      if (flag)
        this.mState = MyPhoton.MyState.JOINING;
      else
        this.mError = MyPhoton.MyError.UNKNOWN;
      return flag;
    }

    public bool JoinRandomRoom(byte maxplayer, string playerJson, string VersusHash, string roomName = null, int floor = -1, int pass = -1, string myuid = null)
    {
      PhotonNetwork.player.SetCustomProperties((Hashtable) null, (Hashtable) null, false);
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
          if (customProperties.ContainsKey((object) "MatchType") && VersusHash == (string) customProperties[(object) "MatchType"])
          {
            if (customProperties.ContainsKey((object) "BlockList"))
            {
              string str = (string) customProperties[(object) "BlockList"];
              if (!string.IsNullOrEmpty(str))
              {
                stringList.AddRange((IEnumerable<string>) str.Split(','));
                if (stringList.FindIndex((Predicate<string>) (id => id == myuid)) != -1)
                  continue;
              }
            }
            if ((!customProperties.ContainsKey((object) "start") || !(bool) customProperties[(object) "start"]) && (floor == -1 || !customProperties.ContainsKey((object) nameof (floor)) || (int) customProperties[(object) nameof (floor)] == floor) && (pass == -1 || !customProperties.ContainsKey((object) "Lock") || !(bool) customProperties[(object) "Lock"]))
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
              if (customProperties.ContainsKey((object) "uid"))
              {
                string target_uid = (string) customProperties[(object) "uid"];
                if (blockList.FindIndex((Predicate<string>) (uid => uid == target_uid)) == -1)
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
        Hashtable hashtable = new Hashtable();
        hashtable.Add((object) "MatchType", (object) VersusHash);
        hashtable.Add((object) "start", (object) false);
        Hashtable expectedCustomRoomProperties = hashtable;
        if (!string.IsNullOrEmpty(roomName))
          expectedCustomRoomProperties.Add((object) "name", (object) roomName);
        if (floor != -1)
          expectedCustomRoomProperties.Add((object) nameof (floor), (object) floor);
        if (pass != -1)
          expectedCustomRoomProperties.Add((object) "Lock", (object) false);
        flag1 = PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties, maxplayer);
      }
      if (flag1)
        this.mState = MyPhoton.MyState.JOINING;
      else
        this.mError = MyPhoton.MyError.UNKNOWN;
      return flag1;
    }

    public bool JoinRoomCheckParam(string VersusHash, string playerJson, int lvRange, int floorRange, int lv, int floor, string lastuid, string myuid)
    {
      if (this.mState != MyPhoton.MyState.LOBBY)
      {
        this.mError = MyPhoton.MyError.ILLEGAL_STATE;
        return false;
      }
      string roomName = string.Empty;
      bool flag1 = false;
      PhotonNetwork.player.SetCustomProperties((Hashtable) null, (Hashtable) null, false);
      PhotonNetwork.SetPlayerCustomProperties((Hashtable) null);
      this.SetMyPlayerParam(playerJson);
      global::RoomInfo[] roomList = PhotonNetwork.GetRoomList();
      List<global::RoomInfo> roomInfoList = new List<global::RoomInfo>();
      foreach (global::RoomInfo roomInfo in roomList)
      {
        Hashtable customProperties = roomInfo.CustomProperties;
        if (customProperties.ContainsKey((object) "MatchType") && VersusHash == (string) customProperties[(object) "MatchType"] && (!customProperties.ContainsKey((object) "uid") || string.IsNullOrEmpty(lastuid) || string.Compare((string) customProperties[(object) "uid"], lastuid) != 0) && ((!customProperties.ContainsKey((object) "luid") || string.IsNullOrEmpty(myuid) || string.Compare((string) customProperties[(object) "luid"], myuid) != 0) && (!customProperties.ContainsKey((object) "start") || !(bool) customProperties[(object) "start"])))
          roomInfoList.Add(roomInfo);
      }
      if (lvRange != -1)
      {
        int num1 = lv - lvRange;
        int num2 = lv + lvRange;
        foreach (global::RoomInfo roomInfo in roomInfoList)
        {
          Hashtable customProperties = roomInfo.CustomProperties;
          if (customProperties.ContainsKey((object) "plv") && customProperties.ContainsKey((object) nameof (floor)))
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
          if (customProperties.ContainsKey((object) nameof (floor)) && floor == (int) customProperties[(object) nameof (floor)])
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
          int num1 = floor - index;
          int num2 = floor + index;
          foreach (global::RoomInfo roomInfo in roomInfoList)
          {
            Hashtable customProperties = roomInfo.CustomProperties;
            if (customProperties.ContainsKey((object) nameof (floor)))
            {
              int num3 = (int) customProperties[(object) nameof (floor)];
              if (num1 <= num3 && num3 <= num2)
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
          this.mState = MyPhoton.MyState.JOINING;
        else
          this.mError = MyPhoton.MyError.UNKNOWN;
      }
      return flag2;
    }

    public bool JoinRankMatchRoomCheckParam(string playerJson, int lv, int lvRange, string myuid, int score, int scoreRangeMax, int scoreRangeMin, int type, string[] exclude_uids)
    {
      if (this.mState != MyPhoton.MyState.LOBBY)
      {
        this.mError = MyPhoton.MyError.ILLEGAL_STATE;
        return false;
      }
      string roomName = string.Empty;
      bool flag1 = false;
      PhotonNetwork.player.SetCustomProperties((Hashtable) null, (Hashtable) null, false);
      PhotonNetwork.SetPlayerCustomProperties((Hashtable) null);
      this.SetMyPlayerParam(playerJson);
      global::RoomInfo[] roomList = PhotonNetwork.GetRoomList();
      List<global::RoomInfo> roomInfoList = new List<global::RoomInfo>();
      string versusKey = MonoSingleton<GameManager>.Instance.GetVersusKey(VERSUS_TYPE.RankMatch);
      foreach (global::RoomInfo roomInfo in roomList)
      {
        Hashtable customProperties = roomInfo.CustomProperties;
        if (customProperties.ContainsKey((object) "MatchType") && versusKey == (string) customProperties[(object) "MatchType"])
        {
          if (customProperties.ContainsKey((object) "uid") && exclude_uids != null && exclude_uids.Length > 0)
          {
            string strA = (string) customProperties[(object) "uid"];
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
          if (customProperties.ContainsKey((object) nameof (score)) && (!customProperties.ContainsKey((object) "start") || !(bool) customProperties[(object) "start"]))
            roomInfoList.Add(roomInfo);
        }
      }
      roomInfoList.Sort((Comparison<global::RoomInfo>) ((a, b) =>
      {
        int num1 = 0;
        int num2 = 0;
        Hashtable customProperties1 = a.CustomProperties;
        if (customProperties1.ContainsKey((object) nameof (score)))
          num1 = Math.Abs(score - (int) customProperties1[(object) nameof (score)]);
        Hashtable customProperties2 = b.CustomProperties;
        if (customProperties2.ContainsKey((object) nameof (score)))
          num2 = Math.Abs(score - (int) customProperties2[(object) nameof (score)]);
        return num1 - num2;
      }));
      if (scoreRangeMin != -1)
      {
        int num1 = score - scoreRangeMin;
        int num2 = score + scoreRangeMin;
        foreach (global::RoomInfo roomInfo in roomInfoList)
        {
          Hashtable customProperties = roomInfo.CustomProperties;
          if (customProperties.ContainsKey((object) nameof (score)))
          {
            int num3 = (int) customProperties[(object) nameof (score)];
            if (num1 <= num3 && num3 <= num2)
            {
              if (lvRange != -1 && customProperties.ContainsKey((object) "plv"))
              {
                int num4 = lv - lvRange;
                int num5 = lv + lvRange;
                int num6 = (int) customProperties[(object) "plv"];
                if (num6 < num4 && num5 < num6)
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
          if (customProperties.ContainsKey((object) nameof (type)) && (int) customProperties[(object) nameof (type)] == type)
          {
            if (lvRange != -1 && customProperties.ContainsKey((object) "plv"))
            {
              int num1 = lv - lvRange;
              int num2 = lv + lvRange;
              int num3 = (int) customProperties[(object) "plv"];
              if (num3 < num1 && num2 < num3)
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
          if (lvRange != -1 && customProperties.ContainsKey((object) "plv"))
          {
            int num1 = lv - lvRange;
            int num2 = lv + lvRange;
            int num3 = (int) customProperties[(object) "plv"];
            if (num3 < num1 && num2 < num3)
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
          this.mState = MyPhoton.MyState.JOINING;
        else
          this.mError = MyPhoton.MyError.UNKNOWN;
      }
      return flag3;
    }

    public bool LeaveRoom()
    {
      if (this.mState != MyPhoton.MyState.ROOM)
      {
        this.mError = MyPhoton.MyError.ILLEGAL_STATE;
        return false;
      }
      bool flag = PhotonNetwork.LeaveRoom();
      if (flag)
        this.mState = MyPhoton.MyState.LEAVING;
      else
        this.mError = MyPhoton.MyError.UNKNOWN;
      return flag;
    }

    public MyPhoton.MyRoom GetCurrentRoom()
    {
      Room room = PhotonNetwork.room;
      MyPhoton.MyRoom myRoom = new MyPhoton.MyRoom();
      if (room != null)
      {
        myRoom.name = room.Name;
        myRoom.playerCount = room.PlayerCount;
        myRoom.maxPlayers = room.MaxPlayers;
        myRoom.open = room.IsOpen;
        myRoom.start = false;
        Hashtable customProperties = room.CustomProperties;
        if (customProperties != null)
        {
          if (customProperties.ContainsKey((object) "json"))
            GameUtility.Binary2Object<string>(out myRoom.json, (byte[]) customProperties[(object) "json"]);
          if (customProperties.ContainsKey((object) "start"))
            myRoom.start = (bool) customProperties[(object) "start"];
          if (customProperties.ContainsKey((object) "battle"))
            myRoom.battle = (bool) customProperties[(object) "battle"];
          if (customProperties.ContainsKey((object) "draft"))
            myRoom.draft = (bool) customProperties[(object) "draft"];
        }
      }
      return myRoom;
    }

    public bool SetRoomParam(string json)
    {
      if (this.mState != MyPhoton.MyState.ROOM)
      {
        this.mError = MyPhoton.MyError.ILLEGAL_STATE;
        return false;
      }
      Hashtable hashtable = new Hashtable();
      hashtable.Add((object) nameof (json), (object) GameUtility.Object2Binary<string>(json));
      Hashtable propertiesToSet = hashtable;
      if (PhotonNetwork.room == null)
      {
        this.mError = MyPhoton.MyError.ILLEGAL_STATE;
        return false;
      }
      PhotonNetwork.room.SetCustomProperties(propertiesToSet, (Hashtable) null, false);
      return true;
    }

    public bool AddRoomParam(string key, string param)
    {
      if (this.mState != MyPhoton.MyState.ROOM)
      {
        this.mError = MyPhoton.MyError.ILLEGAL_STATE;
        return false;
      }
      Room room = PhotonNetwork.room;
      if (room == null)
        return false;
      Hashtable customProperties = room.CustomProperties;
      Hashtable propertiesToSet = new Hashtable();
      if (customProperties != null)
      {
        propertiesToSet.Add((object) "json", customProperties[(object) "json"]);
        if (customProperties.ContainsKey((object) key))
          return false;
      }
      propertiesToSet.Add((object) key, (object) GameUtility.Object2Binary<string>(param));
      room.SetCustomProperties((Hashtable) null, (Hashtable) null, false);
      room.SetCustomProperties(propertiesToSet, (Hashtable) null, false);
      return true;
    }

    public bool UpdateRoomParam(string key, object param)
    {
      if (this.mState != MyPhoton.MyState.ROOM)
      {
        this.mError = MyPhoton.MyError.ILLEGAL_STATE;
        return false;
      }
      Room room = PhotonNetwork.room;
      if (room == null)
        return false;
      Hashtable customProperties = room.CustomProperties;
      if (customProperties != null)
      {
        if (customProperties.ContainsKey((object) key))
          customProperties[(object) key] = param;
        else
          customProperties.Add((object) key, param);
      }
      room.SetCustomProperties((Hashtable) null, (Hashtable) null, false);
      room.SetCustomProperties(customProperties, (Hashtable) null, false);
      return true;
    }

    public bool SetRoomParam(string key, string param)
    {
      if (this.mState != MyPhoton.MyState.ROOM)
      {
        this.mError = MyPhoton.MyError.ILLEGAL_STATE;
        return false;
      }
      Room room = PhotonNetwork.room;
      if (room == null)
        return false;
      Hashtable hashtable = new Hashtable();
      hashtable.Add((object) key, (object) param);
      Hashtable propertiesToSet = hashtable;
      room.SetCustomProperties((Hashtable) null, (Hashtable) null, false);
      room.SetCustomProperties(propertiesToSet, (Hashtable) null, false);
      return true;
    }

    public string GetRoomParam(string key)
    {
      if (this.mState != MyPhoton.MyState.ROOM)
      {
        this.mError = MyPhoton.MyError.ILLEGAL_STATE;
        return (string) null;
      }
      Room room = PhotonNetwork.room;
      if (room == null)
        return (string) null;
      Hashtable customProperties = room.CustomProperties;
      if (customProperties != null)
      {
        object obj = (object) null;
        if (customProperties.TryGetValue((object) key, out obj))
        {
          if (obj.ToString().IndexOf("players") != -1)
            return (string) obj;
          string buffer = string.Empty;
          GameUtility.Binary2Object<string>(out buffer, (byte[]) obj);
          return buffer;
        }
      }
      return (string) null;
    }

    public bool CloseRoom()
    {
      if (this.mState != MyPhoton.MyState.ROOM)
      {
        this.mError = MyPhoton.MyError.ILLEGAL_STATE;
        return false;
      }
      byte[] numArray = (byte[]) null;
      if (PhotonNetwork.room.CustomProperties.Count > 0)
      {
        Hashtable customProperties = PhotonNetwork.room.CustomProperties;
        if (customProperties != null && customProperties.Count > 0)
          numArray = (byte[]) customProperties[(object) "json"];
      }
      Hashtable propertiesToSet = new Hashtable();
      propertiesToSet.Add((object) "json", (object) numArray);
      propertiesToSet.Add((object) "start", (object) true);
      propertiesToSet.Add((object) "battle", (object) true);
      propertiesToSet.Add((object) "draft", (object) false);
      PhotonNetwork.room.SetCustomProperties(propertiesToSet, (Hashtable) null, false);
      if (GlobalVars.SelectedMultiPlayRoomType == JSON_MyPhotonRoomParam.EType.RAID)
        PhotonNetwork.room.IsVisible = false;
      return true;
    }

    public void ForceCloseRoom()
    {
      if (this.mState != MyPhoton.MyState.ROOM)
        return;
      PhotonNetwork.room.IsOpen = false;
      byte[] numArray = (byte[]) null;
      if (PhotonNetwork.room.CustomProperties.Count > 0)
      {
        Hashtable customProperties = PhotonNetwork.room.CustomProperties;
        if (customProperties != null && customProperties.Count > 0)
          numArray = (byte[]) customProperties[(object) "json"];
      }
      Hashtable hashtable = new Hashtable();
      hashtable.Add((object) "json", (object) numArray);
      hashtable.Add((object) "start", (object) false);
      hashtable.Add((object) "battle", (object) false);
      hashtable.Add((object) "draft", (object) false);
      Hashtable propertiesToSet = hashtable;
      PhotonNetwork.room.SetCustomProperties((Hashtable) null, (Hashtable) null, false);
      PhotonNetwork.room.SetCustomProperties(propertiesToSet, (Hashtable) null, false);
    }

    public void BattleStartRoom()
    {
      if (this.mState != MyPhoton.MyState.ROOM)
      {
        this.mError = MyPhoton.MyError.ILLEGAL_STATE;
      }
      else
      {
        if (!this.IsHost())
          return;
        Hashtable propertiesToSet = new Hashtable();
        propertiesToSet.Add((object) "battle", (object) true);
        propertiesToSet.Add((object) "draft", (object) true);
        PhotonNetwork.room.SetCustomProperties(propertiesToSet, (Hashtable) null, false);
      }
    }

    public bool OpenRoom(bool isvisible = true, bool isstarted = false)
    {
      if (this.mState != MyPhoton.MyState.ROOM)
      {
        this.mError = MyPhoton.MyError.ILLEGAL_STATE;
        return false;
      }
      PhotonNetwork.room.IsOpen = true;
      PhotonNetwork.room.IsVisible = isvisible;
      byte[] numArray = (byte[]) null;
      if (PhotonNetwork.room.CustomProperties.Count > 0)
      {
        Hashtable customProperties = PhotonNetwork.room.CustomProperties;
        if (customProperties != null && customProperties.Count > 0)
          numArray = (byte[]) customProperties[(object) "json"];
      }
      Hashtable hashtable = new Hashtable();
      hashtable.Add((object) "json", (object) numArray);
      hashtable.Add((object) "start", (object) isstarted);
      hashtable.Add((object) "battle", (object) isstarted);
      Hashtable propertiesToSet = hashtable;
      PhotonNetwork.room.SetCustomProperties((Hashtable) null, (Hashtable) null, false);
      PhotonNetwork.room.SetCustomProperties(propertiesToSet, (Hashtable) null, false);
      return true;
    }

    public bool IsOldestPlayer(int playerID)
    {
      if (this.mState != MyPhoton.MyState.ROOM)
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
      if (this.mState != MyPhoton.MyState.ROOM)
        return false;
      return this.IsOldestPlayer(this.GetMyPlayer().playerID);
    }

    public int GetOldestPlayer()
    {
      if (this.mState != MyPhoton.MyState.ROOM)
        return 0;
      int num = 0;
      foreach (MyPhoton.MyPlayer roomPlayer in this.GetRoomPlayerList())
      {
        if ((roomPlayer.playerID < num || num == 0) && roomPlayer.start)
          num = roomPlayer.playerID;
      }
      return num;
    }

    public bool IsHost()
    {
      if (this.mState != MyPhoton.MyState.ROOM)
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
      if (this.mState != MyPhoton.MyState.ROOM)
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
      if (this.mState != MyPhoton.MyState.ROOM)
        return false;
      return this.GetMyPlayer().playerID == 1;
    }

    public bool UseEncrypt { get; set; }

    public bool SendRoomMessage(bool reliable, string msg, MyPhoton.SEND_TYPE eventcode = MyPhoton.SEND_TYPE.Normal)
    {
      if (this.mState != MyPhoton.MyState.ROOM)
        return false;
      int num = 0;
      Hashtable hashtable1;
      if (num == 0)
      {
        Hashtable hashtable2 = new Hashtable();
        hashtable2.Add((object) "s", (object) num);
        hashtable2.Add((object) "m", (object) msg);
        hashtable1 = hashtable2;
      }
      else
      {
        byte[] numArray = MyEncrypt.Encrypt(num + this.GetCryptKey(), msg, true);
        Hashtable hashtable2 = new Hashtable();
        hashtable2.Add((object) "s", (object) num);
        hashtable2.Add((object) "m", (object) numArray);
        hashtable1 = hashtable2;
      }
      if (this.SortRoomMessage)
      {
        hashtable1.Add((object) "sq", (object) this.mSendRoomMessageID);
        ++this.mSendRoomMessageID;
      }
      bool flag = PhotonNetwork.RaiseEvent((byte) eventcode, (object) hashtable1, reliable, (RaiseEventOptions) null);
      if (!this.DisconnectIfSendRoomMessageFailed || flag)
        return flag;
      this.Disconnect();
      this.mError = MyPhoton.MyError.RAISE_EVENT_FAILED;
      DebugUtility.LogError("SendRoomMessage failed!");
      return false;
    }

    public bool SendRoomMessageBinary(bool reliable, byte[] msg, MyPhoton.SEND_TYPE eventcode = MyPhoton.SEND_TYPE.Normal, bool isWrite = false)
    {
      if (this.mState != MyPhoton.MyState.ROOM)
        return false;
      byte[] numArray = MyEncrypt.Encrypt(msg);
      Hashtable hashtable1 = new Hashtable();
      hashtable1.Add((object) "bm", (object) numArray);
      Hashtable hashtable2 = hashtable1;
      if (this.SortRoomMessage)
      {
        hashtable2.Add((object) "sq", (object) this.mSendRoomMessageID);
        ++this.mSendRoomMessageID;
      }
      bool flag = PhotonNetwork.RaiseEvent((byte) eventcode, (object) hashtable2, reliable, (RaiseEventOptions) null);
      if (!this.DisconnectIfSendRoomMessageFailed || flag)
        return flag;
      this.Disconnect();
      this.mError = MyPhoton.MyError.RAISE_EVENT_FAILED;
      DebugUtility.LogError("SendRoomMessage failed!");
      return false;
    }

    public void SendFlush()
    {
      PhotonNetwork.SendOutgoingCommands();
    }

    public List<MyPhoton.MyPlayer> GetRoomPlayerList()
    {
      List<MyPhoton.MyPlayer> myPlayerList = new List<MyPhoton.MyPlayer>();
      foreach (PhotonPlayer player in PhotonNetwork.playerList)
      {
        MyPhoton.MyPlayer myPlayer = new MyPhoton.MyPlayer();
        myPlayer.playerID = player.ID;
        Hashtable customProperties = player.CustomProperties;
        if (customProperties != null && customProperties.Count > 0)
        {
          GameUtility.Binary2Object<string>(out myPlayer.json, (byte[]) customProperties[(object) "json"]);
          if (customProperties.ContainsKey((object) "resumeID"))
            myPlayer.resumeID = (int) customProperties[(object) "resumeID"];
          if (customProperties.ContainsKey((object) "BattleStart"))
            myPlayer.start = (bool) customProperties[(object) "BattleStart"];
          if (customProperties.ContainsKey((object) "Logger"))
            continue;
        }
        myPlayerList.Add(myPlayer);
      }
      return myPlayerList;
    }

    public MyPhoton.MyPlayer FindPlayer(List<MyPhoton.MyPlayer> players, int playerID, int playerIndex)
    {
      MyPhoton.MyPlayer myPlayer = (MyPhoton.MyPlayer) null;
      if (players != null)
        myPlayer = ((players.Find((Predicate<MyPhoton.MyPlayer>) (p => p.playerID == playerID)) ?? players.Find((Predicate<MyPhoton.MyPlayer>) (p => p.photonPlayerID == playerID))) ?? players.Find((Predicate<MyPhoton.MyPlayer>) (p => p.playerID == playerIndex))) ?? players.Find((Predicate<MyPhoton.MyPlayer>) (p => p.photonPlayerID == playerIndex));
      return myPlayer;
    }

    public List<JSON_MyPhotonPlayerParam> GetMyPlayersStarted()
    {
      return this.mPlayersStarted;
    }

    public int MyPlayerIndex { get; set; }

    public bool IsMultiPlay { get; set; }

    public bool IsMultiVersus { get; set; }

    public bool IsRankMatch { get; set; }

    public void Reset()
    {
      if (this.mState != MyPhoton.MyState.NOP)
        this.Disconnect();
      this.MyPlayerIndex = 0;
      this.IsMultiPlay = false;
      this.IsMultiVersus = false;
      this.IsRankMatch = false;
      this.mPlayersStarted.Clear();
    }

    public void EnableKeepAlive(bool flag)
    {
      if (PhotonNetwork.isMessageQueueRunning != flag)
        this.Log("[PUN]KeepAlive changed to:" + (object) flag);
      PhotonNetwork.isMessageQueueRunning = flag;
    }

    public bool IsConnected()
    {
      return PhotonNetwork.connected;
    }

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
          if (myPhotonRoomParam != null && myPhotonRoomParam.roomid == selectedMultiPlayRoomId)
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
      MyPhoton.MyRoom myRoom = this.GetRoomList().Find((Predicate<MyPhoton.MyRoom>) (r =>
      {
        if (r.lobby == "tower")
          return r.name == roomname;
        return false;
      }));
      if (myRoom == null || !myRoom.battle)
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

    public void KickMember(MyPhoton.MyPlayer target)
    {
      if (target == null || !PhotonNetwork.connected || !this.IsHost())
        return;
      PhotonPlayer kickPlayer = (PhotonPlayer) null;
      for (int index = 0; index < PhotonNetwork.playerList.Length; ++index)
      {
        if (target.playerID == PhotonNetwork.playerList[index].ID)
          kickPlayer = PhotonNetwork.playerList[index];
      }
      if (kickPlayer == null)
        return;
      PhotonNetwork.CloseConnection(kickPlayer);
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
      public int maxPlayers = 1;
      public bool open = true;
      public string json = string.Empty;
      public string lobby = string.Empty;
      public int playerCount;
      public bool start;
      public bool battle;
      public bool draft;
      public int audience;
      public int audienceMax;
    }

    public class MyPlayer
    {
      public int resumeID = -1;
      public string json = string.Empty;
      public int photonPlayerID;
      public bool start;

      public int playerID
      {
        get
        {
          if (this.resumeID != -1)
            return this.resumeID;
          return this.photonPlayerID;
        }
        set
        {
          this.photonPlayerID = value;
        }
      }
    }
  }
}
