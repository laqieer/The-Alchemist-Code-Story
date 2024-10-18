// Decompiled with JetBrains decompiler
// Type: NetworkingPeer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using ExitGames.Client.Photon;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

#nullable disable
internal class NetworkingPeer : LoadBalancingPeer, IPhotonPeerListener
{
  protected internal string AppId;
  private string tokenCache;
  public AuthModeOption AuthMode;
  public EncryptionMode EncryptionMode;
  public const string NameServerHost = "ns.exitgames.com";
  public const string NameServerHttp = "http://ns.exitgamescloud.com:80/photon/n";
  private static readonly Dictionary<ConnectionProtocol, int> ProtocolToNameServerPort = new Dictionary<ConnectionProtocol, int>()
  {
    {
      (ConnectionProtocol) 0,
      5058
    },
    {
      (ConnectionProtocol) 1,
      4533
    },
    {
      (ConnectionProtocol) 4,
      9093
    },
    {
      (ConnectionProtocol) 5,
      19093
    }
  };
  public bool IsInitialConnect;
  public bool insideLobby;
  protected internal List<TypedLobbyInfo> LobbyStatistics = new List<TypedLobbyInfo>();
  public Dictionary<string, RoomInfo> mGameList = new Dictionary<string, RoomInfo>();
  public RoomInfo[] mGameListCopy = new RoomInfo[0];
  private string playername = string.Empty;
  private bool mPlayernameHasToBeUpdated;
  private Room currentRoom;
  private JoinType lastJoinType;
  protected internal EnterRoomParams enterRoomParamsCache;
  private bool didAuthenticate;
  private string[] friendListRequested;
  private int friendListTimestamp;
  private bool isFetchingFriendList;
  public Dictionary<int, PhotonPlayer> mActors = new Dictionary<int, PhotonPlayer>();
  public PhotonPlayer[] mOtherPlayerListCopy = new PhotonPlayer[0];
  public PhotonPlayer[] mPlayerListCopy = new PhotonPlayer[0];
  public bool hasSwitchedMC;
  private HashSet<byte> allowedReceivingGroups = new HashSet<byte>();
  private HashSet<byte> blockSendingGroups = new HashSet<byte>();
  protected internal Dictionary<int, PhotonView> photonViewList = new Dictionary<int, PhotonView>();
  private readonly PhotonStream readStream = new PhotonStream(false, (object[]) null);
  private readonly PhotonStream pStream = new PhotonStream(true, (object[]) null);
  private readonly Dictionary<int, Hashtable> dataPerGroupReliable = new Dictionary<int, Hashtable>();
  private readonly Dictionary<int, Hashtable> dataPerGroupUnreliable = new Dictionary<int, Hashtable>();
  protected internal short currentLevelPrefix;
  protected internal bool loadingLevelAndPausedNetwork;
  protected internal const string CurrentSceneProperty = "curScn";
  protected internal const string CurrentScenePropertyLoadAsync = "curScnLa";
  public static bool UsePrefabCache = true;
  internal IPunPrefabPool ObjectPool;
  public static Dictionary<string, GameObject> PrefabCache = new Dictionary<string, GameObject>();
  private Dictionary<System.Type, List<MethodInfo>> monoRPCMethodsCache = new Dictionary<System.Type, List<MethodInfo>>();
  private readonly Dictionary<string, int> rpcShortcuts;
  private static readonly string OnPhotonInstantiateString = PhotonNetworkingMessage.OnPhotonInstantiate.ToString();
  private string cachedServerAddress;
  private string cachedApplicationName;
  private ServerConnection cachedServerType;
  private AsyncOperation _AsyncLevelLoadingOperation;
  private RaiseEventOptions _levelReloadEventOptions = new RaiseEventOptions()
  {
    Receivers = ReceiverGroup.Others
  };
  private bool _isReconnecting;
  private Dictionary<int, object[]> tempInstantiationData = new Dictionary<int, object[]>();
  public static int ObjectsInOneUpdate = 10;
  private RaiseEventOptions options = new RaiseEventOptions();
  public const int SyncViewId = 0;
  public const int SyncCompressed = 1;
  public const int SyncNullValues = 2;
  public const int SyncFirstValue = 3;
  public bool IsReloadingLevel;
  public bool AsynchLevelLoadCall;

  public NetworkingPeer(string playername, ConnectionProtocol connectionProtocol)
    : base(connectionProtocol)
  {
    this.Listener = (IPhotonPeerListener) this;
    this.LimitOfUnreliableCommands = 40;
    this.lobby = TypedLobby.Default;
    this.PlayerName = playername;
    this.LocalPlayer = new PhotonPlayer(true, -1, this.playername);
    this.AddNewPlayer(this.LocalPlayer.ID, this.LocalPlayer);
    this.rpcShortcuts = new Dictionary<string, int>(PhotonNetwork.PhotonServerSettings.RpcList.Count);
    for (int index = 0; index < PhotonNetwork.PhotonServerSettings.RpcList.Count; ++index)
      this.rpcShortcuts[PhotonNetwork.PhotonServerSettings.RpcList[index]] = index;
    this.State = ClientState.PeerCreated;
  }

  protected internal string AppVersion
  {
    get => string.Format("{0}_{1}", (object) PhotonNetwork.gameVersion, (object) "1.94");
  }

  public AuthenticationValues AuthValues { get; set; }

  private string TokenForInit
  {
    get
    {
      if (this.AuthMode == AuthModeOption.Auth)
        return (string) null;
      return this.AuthValues != null ? this.AuthValues.Token : (string) null;
    }
  }

  public bool IsUsingNameServer { get; protected internal set; }

  public string NameServerAddress => this.GetNameServerAddress();

  public string MasterServerAddress { get; protected internal set; }

  public string GameServerAddress { get; protected internal set; }

  protected internal ServerConnection Server { get; private set; }

  public ClientState State { get; internal set; }

  public TypedLobby lobby { get; set; }

  private bool requestLobbyStatistics
  {
    get => PhotonNetwork.EnableLobbyStatistics && this.Server == ServerConnection.MasterServer;
  }

  public string PlayerName
  {
    get => this.playername;
    set
    {
      if (string.IsNullOrEmpty(value) || value.Equals(this.playername))
        return;
      if (this.LocalPlayer != null)
        this.LocalPlayer.NickName = value;
      this.playername = value;
      if (this.CurrentRoom == null)
        return;
      this.SendPlayerName();
    }
  }

  public Room CurrentRoom
  {
    get
    {
      return this.currentRoom != null && this.currentRoom.IsLocalClientInside ? this.currentRoom : (Room) null;
    }
    private set => this.currentRoom = value;
  }

  public PhotonPlayer LocalPlayer { get; internal set; }

  public int PlayersOnMasterCount { get; internal set; }

  public int PlayersInRoomsCount { get; internal set; }

  public int RoomsCount { get; internal set; }

  protected internal int FriendListAge
  {
    get
    {
      return this.isFetchingFriendList || this.friendListTimestamp == 0 ? 0 : System.Environment.TickCount - this.friendListTimestamp;
    }
  }

  public bool IsAuthorizeSecretAvailable
  {
    get => this.AuthValues != null && !string.IsNullOrEmpty(this.AuthValues.Token);
  }

  public List<Region> AvailableRegions { get; protected internal set; }

  public CloudRegionCode CloudRegion { get; protected internal set; }

  public int mMasterClientId
  {
    get
    {
      if (PhotonNetwork.offlineMode)
        return this.LocalPlayer.ID;
      return this.CurrentRoom == null ? 0 : this.CurrentRoom.MasterClientId;
    }
    private set
    {
      if (this.CurrentRoom == null)
        return;
      this.CurrentRoom.MasterClientId = value;
    }
  }

  private string GetNameServerAddress()
  {
    ConnectionProtocol transportProtocol = this.TransportProtocol;
    int num = 0;
    NetworkingPeer.ProtocolToNameServerPort.TryGetValue(transportProtocol, out num);
    string str = string.Empty;
    if (transportProtocol == 4)
      str = "ws://";
    else if (transportProtocol == 5)
      str = "wss://";
    if (PhotonNetwork.UseAlternativeUdpPorts && this.TransportProtocol == null)
      num = 27000;
    return string.Format("{0}{1}:{2}", (object) str, (object) "ns.exitgames.com", (object) num);
  }

  public virtual bool Connect(string serverAddress, string applicationName)
  {
    Debug.LogError((object) "Avoid using this directly. Thanks.");
    return false;
  }

  public bool ReconnectToMaster()
  {
    if (this.AuthValues == null)
    {
      Debug.LogWarning((object) "ReconnectToMaster() with AuthValues == null is not correct!");
      this.AuthValues = new AuthenticationValues();
    }
    this.AuthValues.Token = this.tokenCache;
    return this.Connect(this.MasterServerAddress, ServerConnection.MasterServer);
  }

  public bool ReconnectAndRejoin()
  {
    if (this.AuthValues == null)
    {
      Debug.LogWarning((object) "ReconnectAndRejoin() with AuthValues == null is not correct!");
      this.AuthValues = new AuthenticationValues();
    }
    this.AuthValues.Token = this.tokenCache;
    if (string.IsNullOrEmpty(this.GameServerAddress) || this.enterRoomParamsCache == null)
      return false;
    this.lastJoinType = JoinType.JoinRoom;
    this.enterRoomParamsCache.RejoinOnly = true;
    return this.Connect(this.GameServerAddress, ServerConnection.GameServer);
  }

  public bool Connect(string serverAddress, ServerConnection type)
  {
    if (PhotonHandler.AppQuits)
    {
      Debug.LogWarning((object) "Ignoring Connect() because app gets closed. If this is an error, check PhotonHandler.AppQuits.");
      return false;
    }
    if (this.State == ClientState.Disconnecting)
    {
      Debug.LogError((object) ("Connect() failed. Can't connect while disconnecting (still). Current state: " + (object) PhotonNetwork.connectionStateDetailed));
      return false;
    }
    this.cachedServerType = type;
    this.cachedServerAddress = serverAddress;
    this.cachedApplicationName = string.Empty;
    this.SetupProtocol(type);
    bool flag = this.Connect(serverAddress, string.Empty, (object) this.TokenForInit);
    if (flag)
    {
      switch (type)
      {
        case ServerConnection.MasterServer:
          this.State = ClientState.ConnectingToMasterserver;
          break;
        case ServerConnection.GameServer:
          this.State = ClientState.ConnectingToGameserver;
          break;
        case ServerConnection.NameServer:
          this.State = ClientState.ConnectingToNameServer;
          break;
      }
    }
    return flag;
  }

  private bool Reconnect()
  {
    this._isReconnecting = true;
    PhotonNetwork.SwitchToProtocol(PhotonNetwork.PhotonServerSettings.Protocol);
    this.SetupProtocol(this.cachedServerType);
    bool flag = this.Connect(this.cachedServerAddress, this.cachedApplicationName, (object) this.TokenForInit);
    if (flag)
    {
      switch (this.cachedServerType)
      {
        case ServerConnection.MasterServer:
          this.State = ClientState.ConnectingToMasterserver;
          break;
        case ServerConnection.GameServer:
          this.State = ClientState.ConnectingToGameserver;
          break;
        case ServerConnection.NameServer:
          this.State = ClientState.ConnectingToNameServer;
          break;
      }
    }
    return flag;
  }

  public bool ConnectToNameServer()
  {
    if (PhotonHandler.AppQuits)
    {
      Debug.LogWarning((object) "Ignoring Connect() because app gets closed. If this is an error, check PhotonHandler.AppQuits.");
      return false;
    }
    this.IsUsingNameServer = true;
    this.CloudRegion = CloudRegionCode.none;
    if (this.State == ClientState.ConnectedToNameServer)
      return true;
    this.SetupProtocol(ServerConnection.NameServer);
    this.cachedServerType = ServerConnection.NameServer;
    this.cachedServerAddress = this.NameServerAddress;
    this.cachedApplicationName = "ns";
    if (!this.Connect(this.NameServerAddress, "ns", (object) this.TokenForInit))
      return false;
    this.State = ClientState.ConnectingToNameServer;
    return true;
  }

  public bool ConnectToRegionMaster(CloudRegionCode region)
  {
    if (PhotonHandler.AppQuits)
    {
      Debug.LogWarning((object) "Ignoring Connect() because app gets closed. If this is an error, check PhotonHandler.AppQuits.");
      return false;
    }
    this.IsUsingNameServer = true;
    this.CloudRegion = region;
    if (this.State == ClientState.ConnectedToNameServer)
      return this.CallAuthenticate();
    this.cachedServerType = ServerConnection.NameServer;
    this.cachedServerAddress = this.NameServerAddress;
    this.cachedApplicationName = "ns";
    this.SetupProtocol(ServerConnection.NameServer);
    if (!this.Connect(this.NameServerAddress, "ns", (object) this.TokenForInit))
      return false;
    this.State = ClientState.ConnectingToNameServer;
    return true;
  }

  protected internal void SetupProtocol(ServerConnection serverType)
  {
    ConnectionProtocol connectionProtocol = this.TransportProtocol;
    if (this.AuthMode == AuthModeOption.AuthOnceWss)
    {
      if (serverType != ServerConnection.NameServer)
      {
        if (PhotonNetwork.logLevel >= PhotonLogLevel.ErrorsOnly)
          Debug.LogWarning((object) ("Using PhotonServerSettings.Protocol when leaving the NameServer (AuthMode is AuthOnceWss): " + (object) PhotonNetwork.PhotonServerSettings.Protocol));
        connectionProtocol = PhotonNetwork.PhotonServerSettings.Protocol;
      }
      else
      {
        if (PhotonNetwork.logLevel >= PhotonLogLevel.ErrorsOnly)
          Debug.LogWarning((object) "Using WebSocket to connect NameServer (AuthMode is AuthOnceWss).");
        connectionProtocol = (ConnectionProtocol) 5;
      }
    }
    System.Type type = System.Type.GetType("ExitGames.Client.Photon.SocketWebTcp, Assembly-CSharp", false);
    if ((object) type == null)
      type = System.Type.GetType("ExitGames.Client.Photon.SocketWebTcp, Assembly-CSharp-firstpass", false);
    if ((object) type != null)
    {
      this.SocketImplementationConfig[(ConnectionProtocol) 4] = type;
      this.SocketImplementationConfig[(ConnectionProtocol) 5] = type;
    }
    if ((object) PhotonHandler.PingImplementation == null)
      PhotonHandler.PingImplementation = typeof (PingMono);
    if (this.TransportProtocol == connectionProtocol)
      return;
    if (PhotonNetwork.logLevel >= PhotonLogLevel.ErrorsOnly)
      Debug.LogWarning((object) ("Protocol switch from: " + (object) this.TransportProtocol + " to: " + (object) connectionProtocol + "."));
    this.TransportProtocol = connectionProtocol;
  }

  public virtual void Disconnect()
  {
    if (this.PeerState == null)
    {
      if (PhotonHandler.AppQuits)
        return;
      Debug.LogWarning((object) string.Format("Can't execute Disconnect() while not connected. Nothing changed. State: {0}", (object) this.State));
    }
    else
    {
      this.State = ClientState.Disconnecting;
      base.Disconnect();
    }
  }

  private bool CallAuthenticate()
  {
    AuthenticationValues authenticationValues = this.AuthValues;
    if (authenticationValues == null)
      authenticationValues = new AuthenticationValues()
      {
        UserId = this.PlayerName
      };
    AuthenticationValues authValues = authenticationValues;
    if (PhotonNetwork.PhotonServerSettings.HostType == ServerSettings.HostingOption.SelfHosted && string.IsNullOrEmpty(authValues.UserId))
      authValues.UserId = Guid.NewGuid().ToString();
    return this.AuthMode == AuthModeOption.Auth ? this.OpAuthenticate(this.AppId, this.AppVersion, authValues, this.CloudRegion.ToString(), this.requestLobbyStatistics) : this.OpAuthenticateOnce(this.AppId, this.AppVersion, authValues, this.CloudRegion.ToString(), this.EncryptionMode, PhotonNetwork.PhotonServerSettings.Protocol);
  }

  private void DisconnectToReconnect()
  {
    switch (this.Server)
    {
      case ServerConnection.MasterServer:
        this.State = ClientState.DisconnectingFromMasterserver;
        base.Disconnect();
        break;
      case ServerConnection.GameServer:
        this.State = ClientState.DisconnectingFromGameserver;
        base.Disconnect();
        break;
      case ServerConnection.NameServer:
        this.State = ClientState.DisconnectingFromNameServer;
        base.Disconnect();
        break;
    }
  }

  public bool GetRegions()
  {
    if (this.Server != ServerConnection.NameServer)
      return false;
    bool regions = this.OpGetRegions(this.AppId);
    if (regions)
      this.AvailableRegions = (List<Region>) null;
    return regions;
  }

  public override bool OpFindFriends(string[] friendsToFind)
  {
    if (this.isFetchingFriendList)
      return false;
    this.friendListRequested = friendsToFind;
    this.isFetchingFriendList = true;
    return base.OpFindFriends(friendsToFind);
  }

  public bool OpCreateGame(EnterRoomParams enterRoomParams)
  {
    bool flag = this.Server == ServerConnection.GameServer;
    enterRoomParams.OnGameServer = flag;
    enterRoomParams.PlayerProperties = this.GetLocalActorProperties();
    if (!flag)
      this.enterRoomParamsCache = enterRoomParams;
    this.lastJoinType = JoinType.CreateRoom;
    return this.OpCreateRoom(enterRoomParams);
  }

  public override bool OpJoinRoom(EnterRoomParams opParams)
  {
    bool flag = this.Server == ServerConnection.GameServer;
    opParams.OnGameServer = flag;
    if (!flag)
      this.enterRoomParamsCache = opParams;
    this.lastJoinType = !opParams.CreateIfNotExists ? JoinType.JoinRoom : JoinType.JoinOrCreateRoom;
    return base.OpJoinRoom(opParams);
  }

  public override bool OpJoinRandomRoom(OpJoinRandomRoomParams opJoinRandomRoomParams)
  {
    this.enterRoomParamsCache = new EnterRoomParams();
    this.enterRoomParamsCache.Lobby = opJoinRandomRoomParams.TypedLobby;
    this.enterRoomParamsCache.ExpectedUsers = opJoinRandomRoomParams.ExpectedUsers;
    this.lastJoinType = JoinType.JoinRandomRoom;
    return base.OpJoinRandomRoom(opJoinRandomRoomParams);
  }

  public override bool OpRaiseEvent(
    byte eventCode,
    object customEventContent,
    bool sendReliable,
    RaiseEventOptions raiseEventOptions)
  {
    return !PhotonNetwork.offlineMode && base.OpRaiseEvent(eventCode, customEventContent, sendReliable, raiseEventOptions);
  }

  private void ReadoutProperties(
    Hashtable gameProperties,
    Hashtable pActorProperties,
    int targetActorNr)
  {
    if (pActorProperties != null && ((Dictionary<object, object>) pActorProperties).Count > 0)
    {
      if (targetActorNr > 0)
      {
        PhotonPlayer playerWithId = this.GetPlayerWithId(targetActorNr);
        if (playerWithId != null)
        {
          Hashtable properties = this.ReadoutPropertiesForActorNr(pActorProperties, targetActorNr);
          playerWithId.InternalCacheProperties(properties);
          NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonPlayerPropertiesChanged, (object) playerWithId, (object) properties);
        }
      }
      else
      {
        foreach (object key in ((Dictionary<object, object>) pActorProperties).Keys)
        {
          int num = (int) key;
          Hashtable pActorProperty = (Hashtable) pActorProperties[key];
          string name = (string) pActorProperty[(object) byte.MaxValue];
          PhotonPlayer player = this.GetPlayerWithId(num);
          if (player == null)
          {
            player = new PhotonPlayer(false, num, name);
            this.AddNewPlayer(num, player);
          }
          player.InternalCacheProperties(pActorProperty);
          NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonPlayerPropertiesChanged, (object) player, (object) pActorProperty);
        }
      }
    }
    if (this.CurrentRoom == null || gameProperties == null)
      return;
    this.CurrentRoom.InternalCacheProperties(gameProperties);
    NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonCustomRoomPropertiesChanged, (object) gameProperties);
    if (!PhotonNetwork.automaticallySyncScene)
      return;
    this.LoadLevelIfSynced();
  }

  private Hashtable ReadoutPropertiesForActorNr(Hashtable actorProperties, int actorNr)
  {
    return ((Dictionary<object, object>) actorProperties).ContainsKey((object) actorNr) ? (Hashtable) actorProperties[(object) actorNr] : actorProperties;
  }

  public void ChangeLocalID(int newID)
  {
    if (this.LocalPlayer == null)
      Debug.LogWarning((object) string.Format("LocalPlayer is null or not in mActors! LocalPlayer: {0} mActors==null: {1} newID: {2}", (object) this.LocalPlayer, (object) (this.mActors == null), (object) newID));
    if (this.mActors.ContainsKey(this.LocalPlayer.ID))
      this.mActors.Remove(this.LocalPlayer.ID);
    this.LocalPlayer.InternalChangeLocalID(newID);
    this.mActors[this.LocalPlayer.ID] = this.LocalPlayer;
    this.RebuildPlayerListCopies();
  }

  private void LeftLobbyCleanup()
  {
    this.mGameList = new Dictionary<string, RoomInfo>();
    this.mGameListCopy = new RoomInfo[0];
    if (!this.insideLobby)
      return;
    this.insideLobby = false;
    NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnLeftLobby);
  }

  private void LeftRoomCleanup()
  {
    bool flag1 = this.CurrentRoom != null;
    bool flag2 = this.CurrentRoom == null ? PhotonNetwork.autoCleanUpPlayerObjects : this.CurrentRoom.AutoCleanUp;
    this.hasSwitchedMC = false;
    this.CurrentRoom = (Room) null;
    this.mActors = new Dictionary<int, PhotonPlayer>();
    this.mPlayerListCopy = new PhotonPlayer[0];
    this.mOtherPlayerListCopy = new PhotonPlayer[0];
    this.allowedReceivingGroups = new HashSet<byte>();
    this.blockSendingGroups = new HashSet<byte>();
    this.mGameList = new Dictionary<string, RoomInfo>();
    this.mGameListCopy = new RoomInfo[0];
    this.isFetchingFriendList = false;
    this.ChangeLocalID(-1);
    if (flag2)
    {
      this.LocalCleanupAnythingInstantiated(true);
      PhotonNetwork.manuallyAllocatedViewIds = new List<int>();
    }
    if (!flag1)
      return;
    NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnLeftRoom);
  }

  protected internal void LocalCleanupAnythingInstantiated(bool destroyInstantiatedGameObjects)
  {
    if (this.tempInstantiationData.Count > 0)
      Debug.LogWarning((object) "It seems some instantiation is not completed, as instantiation data is used. You should make sure instantiations are paused when calling this method. Cleaning now, despite this.");
    if (destroyInstantiatedGameObjects)
    {
      HashSet<GameObject> gameObjectSet = new HashSet<GameObject>();
      foreach (PhotonView photonView in this.photonViewList.Values)
      {
        if (photonView.isRuntimeInstantiated)
          gameObjectSet.Add(((Component) photonView).gameObject);
      }
      foreach (GameObject go in gameObjectSet)
        this.RemoveInstantiatedGO(go, true);
    }
    this.tempInstantiationData.Clear();
    PhotonNetwork.lastUsedViewSubId = 0;
    PhotonNetwork.lastUsedViewSubIdStatic = 0;
  }

  private void GameEnteredOnGameServer(OperationResponse operationResponse)
  {
    if (operationResponse.ReturnCode != (short) 0)
    {
      switch (operationResponse.OperationCode)
      {
        case 225:
          if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
          {
            Debug.Log((object) ("Join failed on GameServer. Changing back to MasterServer. Msg: " + operationResponse.DebugMessage));
            if (operationResponse.ReturnCode == (short) 32758)
              Debug.Log((object) "Most likely the game became empty during the switch to GameServer.");
          }
          NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonRandomJoinFailed, (object) operationResponse.ReturnCode, (object) operationResponse.DebugMessage);
          break;
        case 226:
          if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
          {
            Debug.Log((object) ("Join failed on GameServer. Changing back to MasterServer. Msg: " + operationResponse.DebugMessage));
            if (operationResponse.ReturnCode == (short) 32758)
              Debug.Log((object) "Most likely the game became empty during the switch to GameServer.");
          }
          NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonJoinRoomFailed, (object) operationResponse.ReturnCode, (object) operationResponse.DebugMessage);
          break;
        case 227:
          if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
            Debug.Log((object) ("Create failed on GameServer. Changing back to MasterServer. Msg: " + operationResponse.DebugMessage));
          NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonCreateRoomFailed, (object) operationResponse.ReturnCode, (object) operationResponse.DebugMessage);
          break;
      }
      this.DisconnectToReconnect();
    }
    else
    {
      Room room = new Room(this.enterRoomParamsCache.RoomName, this.enterRoomParamsCache.RoomOptions);
      room.IsLocalClientInside = true;
      this.CurrentRoom = room;
      this.State = ClientState.Joined;
      if (operationResponse.Parameters.ContainsKey((byte) 252))
        this.UpdatedActorList((int[]) operationResponse.Parameters[(byte) 252]);
      this.ChangeLocalID((int) operationResponse[(byte) 254]);
      Hashtable pActorProperties = (Hashtable) operationResponse[(byte) 249];
      this.ReadoutProperties((Hashtable) operationResponse[(byte) 248], pActorProperties, 0);
      if (!this.CurrentRoom.serverSideMasterClient)
        this.CheckMasterClient(-1);
      if (this.mPlayernameHasToBeUpdated)
        this.SendPlayerName();
      switch (operationResponse.OperationCode)
      {
        case 227:
          NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnCreatedRoom);
          break;
      }
    }
  }

  private void AddNewPlayer(int ID, PhotonPlayer player)
  {
    if (!this.mActors.ContainsKey(ID))
    {
      this.mActors[ID] = player;
      this.RebuildPlayerListCopies();
    }
    else
      Debug.LogError((object) ("Adding player twice: " + (object) ID));
  }

  private void RemovePlayer(int ID, PhotonPlayer player)
  {
    this.mActors.Remove(ID);
    if (player.IsLocal)
      return;
    this.RebuildPlayerListCopies();
  }

  private void RebuildPlayerListCopies()
  {
    this.mPlayerListCopy = new PhotonPlayer[this.mActors.Count];
    this.mActors.Values.CopyTo(this.mPlayerListCopy, 0);
    List<PhotonPlayer> photonPlayerList = new List<PhotonPlayer>();
    for (int index = 0; index < this.mPlayerListCopy.Length; ++index)
    {
      PhotonPlayer photonPlayer = this.mPlayerListCopy[index];
      if (!photonPlayer.IsLocal)
        photonPlayerList.Add(photonPlayer);
    }
    this.mOtherPlayerListCopy = photonPlayerList.ToArray();
  }

  private void ResetPhotonViewsOnSerialize()
  {
    foreach (PhotonView photonView in this.photonViewList.Values)
      photonView.lastOnSerializeDataSent = (object[]) null;
  }

  private void HandleEventLeave(int actorID, EventData evLeave)
  {
    if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
      Debug.Log((object) ("HandleEventLeave for player ID: " + (object) actorID + " evLeave: " + evLeave.ToStringFull()));
    PhotonPlayer playerWithId = this.GetPlayerWithId(actorID);
    if (playerWithId == null)
    {
      Debug.LogError((object) string.Format("Received event Leave for unknown player ID: {0}", (object) actorID));
    }
    else
    {
      bool isInactive = playerWithId.IsInactive;
      if (evLeave.Parameters.ContainsKey((byte) 233))
      {
        playerWithId.IsInactive = (bool) evLeave.Parameters[(byte) 233];
        if (playerWithId.IsInactive != isInactive)
          NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonPlayerActivityChanged, (object) playerWithId);
        if (playerWithId.IsInactive && isInactive)
        {
          Debug.LogWarning((object) ("HandleEventLeave for player ID: " + (object) actorID + " isInactive: " + (object) playerWithId.IsInactive + ". Stopping handling if inactive."));
          return;
        }
      }
      if (evLeave.Parameters.ContainsKey((byte) 203))
      {
        if ((int) evLeave[(byte) 203] != 0)
        {
          this.mMasterClientId = (int) evLeave[(byte) 203];
          this.UpdateMasterClient();
        }
      }
      else if (!this.CurrentRoom.serverSideMasterClient)
        this.CheckMasterClient(actorID);
      if (playerWithId.IsInactive && !isInactive)
        return;
      if (this.CurrentRoom != null && this.CurrentRoom.AutoCleanUp)
        this.DestroyPlayerObjects(actorID, true);
      this.RemovePlayer(actorID, playerWithId);
      NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonPlayerDisconnected, (object) playerWithId);
    }
  }

  private void CheckMasterClient(int leavingPlayerId)
  {
    bool flag1 = this.mMasterClientId == leavingPlayerId;
    bool flag2 = leavingPlayerId > 0;
    if (flag2 && !flag1)
      return;
    int number;
    if (this.mActors.Count <= 1)
    {
      number = this.LocalPlayer.ID;
    }
    else
    {
      number = int.MaxValue;
      foreach (int key in this.mActors.Keys)
      {
        if (key < number && key != leavingPlayerId)
          number = key;
      }
    }
    this.mMasterClientId = number;
    if (!flag2)
      return;
    NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnMasterClientSwitched, (object) this.GetPlayerWithId(number));
  }

  protected internal void UpdateMasterClient()
  {
    NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnMasterClientSwitched, (object) PhotonNetwork.masterClient);
  }

  private static int ReturnLowestPlayerId(PhotonPlayer[] players, int playerIdToIgnore)
  {
    if (players == null || players.Length == 0)
      return -1;
    int num = int.MaxValue;
    for (int index = 0; index < players.Length; ++index)
    {
      PhotonPlayer player = players[index];
      if (player.ID != playerIdToIgnore && player.ID < num)
        num = player.ID;
    }
    return num;
  }

  protected internal bool SetMasterClient(int playerId, bool sync)
  {
    if (this.mMasterClientId == playerId || !this.mActors.ContainsKey(playerId))
      return false;
    if (sync)
    {
      Hashtable customEventContent = new Hashtable();
      ((Dictionary<object, object>) customEventContent).Add((object) (byte) 1, (object) playerId);
      if (!this.OpRaiseEvent((byte) 208, (object) customEventContent, true, (RaiseEventOptions) null))
        return false;
    }
    this.hasSwitchedMC = true;
    this.CurrentRoom.MasterClientId = playerId;
    NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnMasterClientSwitched, (object) this.GetPlayerWithId(playerId));
    return true;
  }

  public bool SetMasterClient(int nextMasterId)
  {
    Hashtable hashtable1 = new Hashtable();
    ((Dictionary<object, object>) hashtable1).Add((object) (byte) 248, (object) nextMasterId);
    Hashtable gameProperties = hashtable1;
    Hashtable hashtable2 = new Hashtable();
    ((Dictionary<object, object>) hashtable2).Add((object) (byte) 248, (object) this.mMasterClientId);
    Hashtable expectedProperties = hashtable2;
    return this.OpSetPropertiesOfRoom(gameProperties, expectedProperties);
  }

  protected internal PhotonPlayer GetPlayerWithId(int number)
  {
    if (this.mActors == null)
      return (PhotonPlayer) null;
    PhotonPlayer playerWithId = (PhotonPlayer) null;
    this.mActors.TryGetValue(number, out playerWithId);
    return playerWithId;
  }

  private void SendPlayerName()
  {
    if (this.State == ClientState.Joining)
    {
      this.mPlayernameHasToBeUpdated = true;
    }
    else
    {
      if (this.LocalPlayer == null)
        return;
      this.LocalPlayer.NickName = this.PlayerName;
      Hashtable actorProperties = new Hashtable();
      actorProperties[(object) byte.MaxValue] = (object) this.PlayerName;
      if (this.LocalPlayer.ID <= 0)
        return;
      this.OpSetPropertiesOfActor(this.LocalPlayer.ID, actorProperties);
      this.mPlayernameHasToBeUpdated = false;
    }
  }

  private Hashtable GetLocalActorProperties()
  {
    if (PhotonNetwork.player != null)
      return PhotonNetwork.player.AllProperties;
    return new Hashtable()
    {
      [(object) byte.MaxValue] = (object) this.PlayerName
    };
  }

  public void DebugReturn(DebugLevel level, string message)
  {
    if (level == 1)
      Debug.LogError((object) message);
    else if (level == 2)
      Debug.LogWarning((object) message);
    else if (level == 3 && PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
    {
      Debug.Log((object) message);
    }
    else
    {
      if (level != 5 || PhotonNetwork.logLevel != PhotonLogLevel.Full)
        return;
      Debug.Log((object) message);
    }
  }

  public void OnOperationResponse(OperationResponse operationResponse)
  {
    if (PhotonNetwork.networkingPeer.State == ClientState.Disconnecting)
    {
      if (PhotonNetwork.logLevel < PhotonLogLevel.Informational)
        return;
      Debug.Log((object) ("OperationResponse ignored while disconnecting. Code: " + (object) operationResponse.OperationCode));
    }
    else
    {
      if (operationResponse.ReturnCode == (short) 0)
      {
        if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
          Debug.Log((object) operationResponse.ToString());
      }
      else if (operationResponse.ReturnCode == (short) -3)
        Debug.LogError((object) ("Operation " + (object) operationResponse.OperationCode + " could not be executed (yet). Wait for state JoinedLobby or ConnectedToMaster and their callbacks before calling operations. WebRPCs need a server-side configuration. Enum OperationCode helps identify the operation."));
      else if (operationResponse.ReturnCode == (short) 32752)
        Debug.LogError((object) ("Operation " + (object) operationResponse.OperationCode + " failed in a server-side plugin. Check the configuration in the Dashboard. Message from server-plugin: " + operationResponse.DebugMessage));
      else if (operationResponse.ReturnCode == (short) 32760)
        Debug.LogWarning((object) ("Operation failed: " + operationResponse.ToStringFull()));
      else
        Debug.LogError((object) ("Operation failed: " + operationResponse.ToStringFull() + " Server: " + (object) this.Server));
      if (operationResponse.Parameters.ContainsKey((byte) 221))
      {
        if (this.AuthValues == null)
          this.AuthValues = new AuthenticationValues();
        this.AuthValues.Token = operationResponse[(byte) 221] as string;
        this.tokenCache = this.AuthValues.Token;
      }
      byte operationCode = operationResponse.OperationCode;
      switch (operationCode)
      {
        case 217:
          if (operationResponse.ReturnCode != (short) 0)
          {
            this.DebugReturn((DebugLevel) 1, "GetGameList failed: " + operationResponse.ToStringFull());
            break;
          }
          this.mGameList = new Dictionary<string, RoomInfo>();
          Hashtable hashtable = (Hashtable) operationResponse[(byte) 222];
          foreach (object key in ((Dictionary<object, object>) hashtable).Keys)
          {
            string str = (string) key;
            this.mGameList[str] = new RoomInfo(str, (Hashtable) hashtable[key]);
          }
          this.mGameListCopy = new RoomInfo[this.mGameList.Count];
          this.mGameList.Values.CopyTo(this.mGameListCopy, 0);
          NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnReceivedRoomListUpdate);
          break;
        case 219:
          NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnWebRpcResponse, (object) operationResponse);
          break;
        case 220:
          if (operationResponse.ReturnCode == short.MaxValue)
          {
            Debug.LogError((object) string.Format("The appId this client sent is unknown on the server (Cloud). Check settings. If using the Cloud, check account."));
            NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnFailedToConnectToPhoton, (object) DisconnectCause.InvalidAuthentication);
            this.State = ClientState.Disconnecting;
            base.Disconnect();
            break;
          }
          if (operationResponse.ReturnCode != (short) 0)
          {
            Debug.LogError((object) ("GetRegions failed. Can't provide regions list. Error: " + (object) operationResponse.ReturnCode + ": " + operationResponse.DebugMessage));
            break;
          }
          string[] strArray1 = operationResponse[(byte) 210] as string[];
          string[] strArray2 = operationResponse[(byte) 230] as string[];
          if (strArray1 == null || strArray2 == null || strArray1.Length != strArray2.Length)
          {
            Debug.LogError((object) ("The region arrays from Name Server are not ok. Must be non-null and same length. " + (object) (strArray1 == null) + " " + (object) (strArray2 == null) + "\n" + operationResponse.ToStringFull()));
            break;
          }
          this.AvailableRegions = new List<Region>(strArray1.Length);
          for (int index = 0; index < strArray1.Length; ++index)
          {
            string str = strArray1[index];
            if (!string.IsNullOrEmpty(str))
            {
              string lower = str.ToLower();
              CloudRegionCode cloudRegionCode = Region.Parse(lower);
              bool flag1 = true;
              if (PhotonNetwork.PhotonServerSettings.HostType == ServerSettings.HostingOption.BestRegion && PhotonNetwork.PhotonServerSettings.EnabledRegions != (CloudRegionFlag) 0)
              {
                CloudRegionFlag flag2 = Region.ParseFlag(cloudRegionCode);
                flag1 = (PhotonNetwork.PhotonServerSettings.EnabledRegions & flag2) != (CloudRegionFlag) 0;
                if (!flag1 && PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
                  Debug.Log((object) ("Skipping region because it's not in PhotonServerSettings.EnabledRegions: " + (object) cloudRegionCode));
              }
              if (flag1)
                this.AvailableRegions.Add(new Region(cloudRegionCode, lower, strArray2[index]));
            }
          }
          if (PhotonNetwork.PhotonServerSettings.HostType != ServerSettings.HostingOption.BestRegion)
            break;
          CloudRegionCode bestFromPrefs = PhotonHandler.BestRegionCodeInPreferences;
          if (bestFromPrefs != CloudRegionCode.none && this.AvailableRegions.Exists((Predicate<Region>) (x => x.Code == bestFromPrefs)))
          {
            Debug.Log((object) ("Best region found in PlayerPrefs. Connecting to: " + (object) bestFromPrefs));
            if (this.ConnectToRegionMaster(bestFromPrefs))
              break;
            PhotonHandler.PingAvailableRegionsAndConnectToBest();
            break;
          }
          PhotonHandler.PingAvailableRegionsAndConnectToBest();
          break;
        case 222:
          bool[] flagArray = operationResponse[(byte) 1] as bool[];
          string[] strArray3 = operationResponse[(byte) 2] as string[];
          if (flagArray != null && strArray3 != null && this.friendListRequested != null && flagArray.Length == this.friendListRequested.Length)
          {
            List<FriendInfo> friendInfoList = new List<FriendInfo>(this.friendListRequested.Length);
            for (int index = 0; index < this.friendListRequested.Length; ++index)
              friendInfoList.Insert(index, new FriendInfo()
              {
                UserId = this.friendListRequested[index],
                Room = strArray3[index],
                IsOnline = flagArray[index]
              });
            PhotonNetwork.Friends = friendInfoList;
          }
          else
            Debug.LogError((object) "FindFriends failed to apply the result, as a required value wasn't provided or the friend list length differed from result.");
          this.friendListRequested = (string[]) null;
          this.isFetchingFriendList = false;
          this.friendListTimestamp = System.Environment.TickCount;
          if (this.friendListTimestamp == 0)
            this.friendListTimestamp = 1;
          NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnUpdatedFriendList);
          break;
        case 225:
          if (operationResponse.ReturnCode != (short) 0)
          {
            if (operationResponse.ReturnCode == (short) 32760)
            {
              if (PhotonNetwork.logLevel >= PhotonLogLevel.Full)
                Debug.Log((object) "JoinRandom failed: No open game. Calling: OnPhotonRandomJoinFailed() and staying on master server.");
            }
            else if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
              Debug.LogWarning((object) string.Format("JoinRandom failed: {0}.", (object) operationResponse.ToStringFull()));
            NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonRandomJoinFailed, (object) operationResponse.ReturnCode, (object) operationResponse.DebugMessage);
            break;
          }
          this.enterRoomParamsCache.RoomName = (string) operationResponse[byte.MaxValue];
          this.GameServerAddress = (string) operationResponse[(byte) 230];
          if (PhotonNetwork.UseAlternativeUdpPorts && this.TransportProtocol == null)
            this.GameServerAddress = this.GameServerAddress.Replace("5058", "27000").Replace("5055", "27001").Replace("5056", "27002");
          this.DisconnectToReconnect();
          break;
        case 226:
          if (this.Server != ServerConnection.GameServer)
          {
            if (operationResponse.ReturnCode != (short) 0)
            {
              if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
                Debug.Log((object) string.Format("JoinRoom failed (room maybe closed by now). Client stays on masterserver: {0}. State: {1}", (object) operationResponse.ToStringFull(), (object) this.State));
              NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonJoinRoomFailed, (object) operationResponse.ReturnCode, (object) operationResponse.DebugMessage);
              break;
            }
            this.GameServerAddress = (string) operationResponse[(byte) 230];
            if (PhotonNetwork.UseAlternativeUdpPorts && this.TransportProtocol == null)
              this.GameServerAddress = this.GameServerAddress.Replace("5058", "27000").Replace("5055", "27001").Replace("5056", "27002");
            this.DisconnectToReconnect();
            break;
          }
          this.GameEnteredOnGameServer(operationResponse);
          break;
        case 227:
          if (this.Server == ServerConnection.GameServer)
          {
            this.GameEnteredOnGameServer(operationResponse);
            break;
          }
          if (operationResponse.ReturnCode != (short) 0)
          {
            if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
              Debug.LogWarning((object) string.Format("CreateRoom failed, client stays on masterserver: {0}.", (object) operationResponse.ToStringFull()));
            this.State = !this.insideLobby ? ClientState.ConnectedToMaster : ClientState.JoinedLobby;
            NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonCreateRoomFailed, (object) operationResponse.ReturnCode, (object) operationResponse.DebugMessage);
            break;
          }
          string str1 = (string) operationResponse[byte.MaxValue];
          if (!string.IsNullOrEmpty(str1))
            this.enterRoomParamsCache.RoomName = str1;
          this.GameServerAddress = (string) operationResponse[(byte) 230];
          if (PhotonNetwork.UseAlternativeUdpPorts && this.TransportProtocol == null)
            this.GameServerAddress = this.GameServerAddress.Replace("5058", "27000").Replace("5055", "27001").Replace("5056", "27002");
          this.DisconnectToReconnect();
          break;
        case 228:
          this.State = ClientState.Authenticated;
          this.LeftLobbyCleanup();
          break;
        case 229:
          this.State = ClientState.JoinedLobby;
          this.insideLobby = true;
          NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnJoinedLobby);
          break;
        case 230:
        case 231:
          if (operationResponse.ReturnCode != (short) 0)
          {
            if (operationResponse.ReturnCode == (short) -2)
              Debug.LogError((object) string.Format("If you host Photon yourself, make sure to start the 'Instance LoadBalancing' " + this.ServerAddress));
            else if (operationResponse.ReturnCode == short.MaxValue)
            {
              Debug.LogError((object) string.Format("The appId this client sent is unknown on the server (Cloud). Check settings. If using the Cloud, check account."));
              NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnFailedToConnectToPhoton, (object) DisconnectCause.InvalidAuthentication);
            }
            else if (operationResponse.ReturnCode == (short) 32755)
            {
              Debug.LogError((object) string.Format("Custom Authentication failed (either due to user-input or configuration or AuthParameter string format). Calling: OnCustomAuthenticationFailed()"));
              NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnCustomAuthenticationFailed, (object) operationResponse.DebugMessage);
            }
            else
              Debug.LogError((object) string.Format("Authentication failed: '{0}' Code: {1}", (object) operationResponse.DebugMessage, (object) operationResponse.ReturnCode));
            this.State = ClientState.Disconnecting;
            base.Disconnect();
            if (operationResponse.ReturnCode == (short) 32757)
            {
              if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
                Debug.LogWarning((object) string.Format("Currently, the limit of users is reached for this title. Try again later. Disconnecting"));
              NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonMaxCccuReached);
              NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnConnectionFail, (object) DisconnectCause.MaxCcuReached);
              break;
            }
            if (operationResponse.ReturnCode == (short) 32756)
            {
              if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
                Debug.LogError((object) string.Format("The used master server address is not available with the subscription currently used. Got to Photon Cloud Dashboard or change URL. Disconnecting."));
              NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnConnectionFail, (object) DisconnectCause.InvalidRegion);
              break;
            }
            if (operationResponse.ReturnCode != (short) 32753)
              break;
            if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
              Debug.LogError((object) string.Format("The authentication ticket expired. You need to connect (and authenticate) again. Disconnecting."));
            NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnConnectionFail, (object) DisconnectCause.AuthenticationTicketExpired);
            break;
          }
          if (this.Server == ServerConnection.NameServer || this.Server == ServerConnection.MasterServer)
          {
            if (operationResponse.Parameters.ContainsKey((byte) 225))
            {
              string parameter = (string) operationResponse.Parameters[(byte) 225];
              if (!string.IsNullOrEmpty(parameter))
              {
                if (this.AuthValues == null)
                  this.AuthValues = new AuthenticationValues();
                this.AuthValues.UserId = parameter;
                PhotonNetwork.player.UserId = parameter;
                if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
                  this.DebugReturn((DebugLevel) 3, string.Format("Received your UserID from server. Updating local value to: {0}", (object) parameter));
              }
            }
            if (operationResponse.Parameters.ContainsKey((byte) 202))
            {
              this.PlayerName = (string) operationResponse.Parameters[(byte) 202];
              if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
                this.DebugReturn((DebugLevel) 3, string.Format("Received your NickName from server. Updating local value to: {0}", (object) this.playername));
            }
            if (operationResponse.Parameters.ContainsKey((byte) 192))
              this.SetupEncryption((Dictionary<byte, object>) operationResponse.Parameters[(byte) 192]);
          }
          if (this.Server == ServerConnection.NameServer)
          {
            this.MasterServerAddress = operationResponse[(byte) 230] as string;
            if (PhotonNetwork.UseAlternativeUdpPorts && this.TransportProtocol == null)
              this.MasterServerAddress = this.MasterServerAddress.Replace("5058", "27000").Replace("5055", "27001").Replace("5056", "27002");
            this.DisconnectToReconnect();
          }
          else if (this.Server == ServerConnection.MasterServer)
          {
            if (this.AuthMode != AuthModeOption.Auth)
              this.OpSettings(this.requestLobbyStatistics);
            if (PhotonNetwork.autoJoinLobby)
            {
              this.State = ClientState.Authenticated;
              this.OpJoinLobby(this.lobby);
            }
            else
            {
              this.State = ClientState.ConnectedToMaster;
              NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnConnectedToMaster);
            }
          }
          else if (this.Server == ServerConnection.GameServer)
          {
            this.State = ClientState.Joining;
            this.enterRoomParamsCache.PlayerProperties = this.GetLocalActorProperties();
            this.enterRoomParamsCache.OnGameServer = true;
            if (this.lastJoinType == JoinType.JoinRoom || this.lastJoinType == JoinType.JoinRandomRoom || this.lastJoinType == JoinType.JoinOrCreateRoom)
              this.OpJoinRoom(this.enterRoomParamsCache);
            else if (this.lastJoinType == JoinType.CreateRoom)
              this.OpCreateGame(this.enterRoomParamsCache);
          }
          if (!operationResponse.Parameters.ContainsKey((byte) 245))
            break;
          Dictionary<string, object> parameter1 = (Dictionary<string, object>) operationResponse.Parameters[(byte) 245];
          if (parameter1 == null)
            break;
          NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnCustomAuthenticationResponse, (object) parameter1);
          break;
        default:
          switch (operationCode)
          {
            case 251:
              Hashtable pActorProperties = (Hashtable) operationResponse[(byte) 249];
              this.ReadoutProperties((Hashtable) operationResponse[(byte) 248], pActorProperties, 0);
              return;
            case 252:
              return;
            case 253:
              return;
            case 254:
              this.DisconnectToReconnect();
              return;
            default:
              Debug.LogWarning((object) string.Format("OperationResponse unhandled: {0}", (object) operationResponse.ToString()));
              return;
          }
      }
    }
  }

  public void OnStatusChanged(StatusCode statusCode)
  {
    if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
      Debug.Log((object) string.Format("OnStatusChanged: {0} current State: {1}", (object) statusCode.ToString(), (object) this.State));
    switch (statusCode - 1039)
    {
      case 0:
      case 2:
      case 3:
      case 4:
        if (this.IsInitialConnect)
        {
          Debug.LogWarning((object) (statusCode.ToString() + " while connecting to: " + this.ServerAddress + ". Check if the server is available."));
          this.IsInitialConnect = false;
          NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnFailedToConnectToPhoton, (object) (DisconnectCause) statusCode);
        }
        else
          NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnConnectionFail, (object) (DisconnectCause) statusCode);
        if (this.AuthValues != null)
          this.AuthValues.Token = (string) null;
        base.Disconnect();
        break;
      case 1:
        if (this.IsInitialConnect)
        {
          if (!this._isReconnecting)
          {
            Debug.LogWarning((object) (statusCode.ToString() + " while connecting to: " + this.ServerAddress + ". Check if the server is available."));
            this.IsInitialConnect = false;
            NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnFailedToConnectToPhoton, (object) (DisconnectCause) statusCode);
          }
        }
        else
          NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnConnectionFail, (object) (DisconnectCause) statusCode);
        if (this.AuthValues != null)
          this.AuthValues.Token = (string) null;
        base.Disconnect();
        break;
      case 9:
label_24:
        this._isReconnecting = false;
        if (this.Server == ServerConnection.NameServer)
        {
          this.State = ClientState.ConnectedToNameServer;
          if (!this.didAuthenticate && this.CloudRegion == CloudRegionCode.none)
            this.OpGetRegions(this.AppId);
        }
        if (this.Server != ServerConnection.NameServer && (this.AuthMode == AuthModeOption.AuthOnce || this.AuthMode == AuthModeOption.AuthOnceWss))
        {
          Debug.Log((object) ("didAuthenticate " + (object) this.didAuthenticate + " AuthMode " + (object) this.AuthMode));
          break;
        }
        if (this.didAuthenticate || this.IsUsingNameServer && this.CloudRegion == CloudRegionCode.none)
          break;
        this.didAuthenticate = this.CallAuthenticate();
        if (!this.didAuthenticate)
          break;
        this.State = ClientState.Authenticating;
        break;
      case 10:
        Debug.LogError((object) ("Encryption wasn't established: " + (object) statusCode + ". Going to authenticate anyways."));
        AuthenticationValues authValues = this.AuthValues;
        if (authValues == null)
          authValues = new AuthenticationValues()
          {
            UserId = this.PlayerName
          };
        this.OpAuthenticate(this.AppId, this.AppVersion, authValues, this.CloudRegion.ToString(), this.requestLobbyStatistics);
        break;
      default:
        switch (statusCode - 1022)
        {
          case 0:
          case 1:
            this.IsInitialConnect = false;
            this.State = ClientState.PeerCreated;
            if (this.AuthValues != null)
              this.AuthValues.Token = (string) null;
            NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnFailedToConnectToPhoton, (object) (DisconnectCause) statusCode);
            return;
          case 2:
            if (this.State == ClientState.ConnectingToNameServer)
            {
              if (PhotonNetwork.logLevel >= PhotonLogLevel.Full)
                Debug.Log((object) "Connected to NameServer.");
              this.Server = ServerConnection.NameServer;
              if (this.AuthValues != null)
                this.AuthValues.Token = (string) null;
            }
            if (this.State == ClientState.ConnectingToGameserver)
            {
              if (PhotonNetwork.logLevel >= PhotonLogLevel.Full)
                Debug.Log((object) "Connected to gameserver.");
              this.Server = ServerConnection.GameServer;
              this.State = ClientState.ConnectedToGameserver;
            }
            if (this.State == ClientState.ConnectingToMasterserver)
            {
              if (PhotonNetwork.logLevel >= PhotonLogLevel.Full)
                Debug.Log((object) "Connected to masterserver.");
              this.Server = ServerConnection.MasterServer;
              this.State = ClientState.Authenticating;
              if (this.IsInitialConnect)
              {
                this.IsInitialConnect = false;
                NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnConnectedToPhoton);
              }
            }
            if (this.TransportProtocol != 5)
            {
              if (this.Server != ServerConnection.NameServer && this.AuthMode != AuthModeOption.Auth || PhotonNetwork.offlineMode)
                return;
              this.EstablishEncryption();
              return;
            }
            if (this.DebugOut == 3)
            {
              Debug.Log((object) "Skipping EstablishEncryption. Protocol is secure.");
              goto label_24;
            }
            else
              goto label_24;
          case 3:
            this.didAuthenticate = false;
            this.isFetchingFriendList = false;
            if (this.Server == ServerConnection.GameServer)
              this.LeftRoomCleanup();
            if (this.Server == ServerConnection.MasterServer)
              this.LeftLobbyCleanup();
            if (this.State == ClientState.DisconnectingFromMasterserver)
            {
              if (!this.Connect(this.GameServerAddress, ServerConnection.GameServer))
                return;
              this.State = ClientState.ConnectingToGameserver;
              return;
            }
            if (this.State == ClientState.DisconnectingFromGameserver || this.State == ClientState.DisconnectingFromNameServer)
            {
              this.SetupProtocol(ServerConnection.MasterServer);
              if (!this.Connect(this.MasterServerAddress, ServerConnection.MasterServer))
                return;
              this.State = ClientState.ConnectingToMasterserver;
              return;
            }
            if (this._isReconnecting)
              return;
            if (this.AuthValues != null)
              this.AuthValues.Token = (string) null;
            this.IsInitialConnect = false;
            this.State = ClientState.PeerCreated;
            NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnDisconnectedFromPhoton);
            return;
          case 4:
            if (this.IsInitialConnect)
            {
              Debug.LogError((object) ("Exception while connecting to: " + this.ServerAddress + ". Check if the server is available."));
              if (this.ServerAddress == null || this.ServerAddress.StartsWith("127.0.0.1"))
              {
                Debug.LogWarning((object) "The server address is 127.0.0.1 (localhost): Make sure the server is running on this machine. Android and iOS emulators have their own localhost.");
                if (this.ServerAddress == this.GameServerAddress)
                  Debug.LogWarning((object) "This might be a misconfiguration in the game server config. You need to edit it to a (public) address.");
              }
              this.State = ClientState.PeerCreated;
              DisconnectCause disconnectCause = (DisconnectCause) statusCode;
              this.IsInitialConnect = false;
              NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnFailedToConnectToPhoton, (object) disconnectCause);
            }
            else
            {
              this.State = ClientState.PeerCreated;
              NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnConnectionFail, (object) (DisconnectCause) statusCode);
            }
            base.Disconnect();
            return;
          case 8:
            return;
          default:
            Debug.LogError((object) ("Received unknown status code: " + (object) statusCode));
            return;
        }
    }
  }

  public void OnEvent(EventData photonEvent)
  {
    if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
      Debug.Log((object) string.Format("OnEvent: {0}", (object) photonEvent.ToString()));
    int num1 = -1;
    PhotonPlayer photonPlayer = (PhotonPlayer) null;
    if (photonEvent.Parameters.ContainsKey((byte) 254))
    {
      num1 = (int) photonEvent[(byte) 254];
      photonPlayer = this.GetPlayerWithId(num1);
    }
    byte code = photonEvent.Code;
    switch (code)
    {
      case 200:
        this.ExecuteRpc(photonEvent[(byte) 245] as Hashtable, num1);
        break;
      case 201:
      case 206:
        Hashtable hashtable1 = (Hashtable) photonEvent[(byte) 245];
        int networkTime = (int) hashtable1[(object) (byte) 0];
        short correctPrefix = -1;
        byte num2 = 10;
        int num3 = 1;
        if (((Dictionary<object, object>) hashtable1).ContainsKey((object) (byte) 1))
        {
          correctPrefix = (short) hashtable1[(object) (byte) 1];
          num3 = 2;
        }
        for (byte index = num2; (int) index - (int) num2 < ((Dictionary<object, object>) hashtable1).Count - num3; ++index)
          this.OnSerializeRead(hashtable1[(object) index] as object[], photonPlayer, networkTime, correctPrefix);
        break;
      case 202:
        this.DoInstantiate((Hashtable) photonEvent[(byte) 245], photonPlayer, (GameObject) null);
        break;
      case 203:
        if (photonPlayer == null || !photonPlayer.IsMasterClient)
        {
          Debug.LogError((object) ("Error: Someone else(" + (object) photonPlayer + ") then the masterserver requests a disconnect!"));
          break;
        }
        if (this._AsyncLevelLoadingOperation != null)
          this._AsyncLevelLoadingOperation = (AsyncOperation) null;
        PhotonNetwork.LeaveRoom(false);
        break;
      case 204:
        int key1 = (int) ((Hashtable) photonEvent[(byte) 245])[(object) (byte) 0];
        PhotonView photonView1 = (PhotonView) null;
        if (this.photonViewList.TryGetValue(key1, out photonView1))
        {
          this.RemoveInstantiatedGO(((Component) photonView1).gameObject, true);
          break;
        }
        if (this.DebugOut < 1)
          break;
        Debug.LogError((object) ("Ev Destroy Failed. Could not find PhotonView with instantiationId " + (object) key1 + ". Sent by actorNr: " + (object) num1));
        break;
      case 207:
        int playerId = (int) ((Hashtable) photonEvent[(byte) 245])[(object) (byte) 0];
        if (playerId >= 0)
        {
          this.DestroyPlayerObjects(playerId, true);
          break;
        }
        if (this.DebugOut >= 3)
          Debug.Log((object) ("Ev DestroyAll! By PlayerId: " + (object) num1));
        this.DestroyAll(true);
        break;
      case 208:
        this.SetMasterClient((int) ((Hashtable) photonEvent[(byte) 245])[(object) (byte) 1], false);
        break;
      case 209:
        int[] parameter1 = (int[]) photonEvent.Parameters[(byte) 245];
        int viewID1 = parameter1[0];
        int num4 = parameter1[1];
        PhotonView photonView2 = PhotonView.Find(viewID1);
        if (Object.op_Equality((Object) photonView2, (Object) null))
        {
          Debug.LogWarning((object) ("Can't find PhotonView of incoming OwnershipRequest. ViewId not found: " + (object) viewID1));
          break;
        }
        if (PhotonNetwork.logLevel == PhotonLogLevel.Informational)
          Debug.Log((object) ("Ev OwnershipRequest " + (object) photonView2.ownershipTransfer + ". ActorNr: " + (object) num1 + " takes from: " + (object) num4 + ". local RequestedView.ownerId: " + (object) photonView2.ownerId + " isOwnerActive: " + (object) photonView2.isOwnerActive + ". MasterClient: " + (object) this.mMasterClientId + ". This client's player: " + PhotonNetwork.player.ToStringFull()));
        switch (photonView2.ownershipTransfer)
        {
          case OwnershipOption.Fixed:
            Debug.LogWarning((object) "Ownership mode == fixed. Ignoring request.");
            return;
          case OwnershipOption.Takeover:
            if (num4 != photonView2.ownerId && (num4 != 0 || photonView2.ownerId != this.mMasterClientId) && photonView2.ownerId != 0)
              return;
            photonView2.OwnerShipWasTransfered = true;
            PhotonPlayer playerWithId = this.GetPlayerWithId(photonView2.ownerId);
            photonView2.ownerId = num1;
            if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
              Debug.LogWarning((object) (photonView2.ToString() + " ownership transfered to: " + (object) num1));
            NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnOwnershipTransfered, (object) photonView2, (object) photonPlayer, (object) playerWithId);
            return;
          case OwnershipOption.Request:
            if (num4 != PhotonNetwork.player.ID && !PhotonNetwork.player.IsMasterClient || photonView2.ownerId != PhotonNetwork.player.ID && (!PhotonNetwork.player.IsMasterClient || photonView2.isOwnerActive))
              return;
            NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnOwnershipRequest, (object) photonView2, (object) photonPlayer);
            return;
          default:
            return;
        }
      case 210:
        int[] parameter2 = (int[]) photonEvent.Parameters[(byte) 245];
        if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
          Debug.Log((object) ("Ev OwnershipTransfer. ViewID " + (object) parameter2[0] + " to: " + (object) parameter2[1] + " Time: " + (object) (System.Environment.TickCount % 1000)));
        int viewID2 = parameter2[0];
        int ID = parameter2[1];
        PhotonView photonView3 = PhotonView.Find(viewID2);
        if (!Object.op_Inequality((Object) photonView3, (Object) null))
          break;
        int ownerId = photonView3.ownerId;
        photonView3.OwnerShipWasTransfered = true;
        photonView3.ownerId = ID;
        NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnOwnershipTransfered, (object) photonView3, (object) PhotonPlayer.Find(ID), (object) PhotonPlayer.Find(ownerId));
        break;
      case 212:
        if ((bool) photonEvent.Parameters[(byte) 245])
        {
          PhotonNetwork.LoadLevelAsync(SceneManagerHelper.ActiveSceneName);
          break;
        }
        PhotonNetwork.LoadLevel(SceneManagerHelper.ActiveSceneName);
        break;
      case 223:
        if (this.AuthValues == null)
          this.AuthValues = new AuthenticationValues();
        this.AuthValues.Token = photonEvent[(byte) 221] as string;
        this.tokenCache = this.AuthValues.Token;
        break;
      case 224:
        string[] strArray = photonEvent[(byte) 213] as string[];
        byte[] numArray1 = photonEvent[(byte) 212] as byte[];
        int[] numArray2 = photonEvent[(byte) 229] as int[];
        int[] numArray3 = photonEvent[(byte) 228] as int[];
        this.LobbyStatistics.Clear();
        for (int index = 0; index < strArray.Length; ++index)
        {
          TypedLobbyInfo typedLobbyInfo = new TypedLobbyInfo();
          typedLobbyInfo.Name = strArray[index];
          typedLobbyInfo.Type = (LobbyType) numArray1[index];
          typedLobbyInfo.PlayerCount = numArray2[index];
          typedLobbyInfo.RoomCount = numArray3[index];
          this.LobbyStatistics.Add(typedLobbyInfo);
        }
        NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnLobbyStatisticsUpdate);
        break;
      case 226:
        this.PlayersInRoomsCount = (int) photonEvent[(byte) 229];
        this.PlayersOnMasterCount = (int) photonEvent[(byte) 227];
        this.RoomsCount = (int) photonEvent[(byte) 228];
        break;
      case 229:
        Hashtable hashtable2 = (Hashtable) photonEvent[(byte) 222];
        foreach (object key2 in ((Dictionary<object, object>) hashtable2).Keys)
        {
          string str = (string) key2;
          RoomInfo roomInfo = new RoomInfo(str, (Hashtable) hashtable2[key2]);
          if (roomInfo.removedFromList)
            this.mGameList.Remove(str);
          else
            this.mGameList[str] = roomInfo;
        }
        this.mGameListCopy = new RoomInfo[this.mGameList.Count];
        this.mGameList.Values.CopyTo(this.mGameListCopy, 0);
        NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnReceivedRoomListUpdate);
        break;
      case 230:
        this.mGameList = new Dictionary<string, RoomInfo>();
        Hashtable hashtable3 = (Hashtable) photonEvent[(byte) 222];
        foreach (object key3 in ((Dictionary<object, object>) hashtable3).Keys)
        {
          string str = (string) key3;
          this.mGameList[str] = new RoomInfo(str, (Hashtable) hashtable3[key3]);
        }
        this.mGameListCopy = new RoomInfo[this.mGameList.Count];
        this.mGameList.Values.CopyTo(this.mGameListCopy, 0);
        NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnReceivedRoomListUpdate);
        break;
      default:
        switch (code)
        {
          case 251:
            if (PhotonNetwork.CallEvent(photonEvent.Code, photonEvent[(byte) 218], num1))
              return;
            Debug.LogWarning((object) "Warning: Unhandled Event ErrorInfo (251). Set PhotonNetwork.OnEventCall to the method PUN should call for this event.");
            return;
          case 253:
            int targetActorNr = (int) photonEvent[(byte) 253];
            Hashtable gameProperties = (Hashtable) null;
            Hashtable pActorProperties = (Hashtable) null;
            if (targetActorNr == 0)
              gameProperties = (Hashtable) photonEvent[(byte) 251];
            else
              pActorProperties = (Hashtable) photonEvent[(byte) 251];
            this.ReadoutProperties(gameProperties, pActorProperties, targetActorNr);
            return;
          case 254:
            if (this._AsyncLevelLoadingOperation != null)
              this._AsyncLevelLoadingOperation = (AsyncOperation) null;
            this.HandleEventLeave(num1, photonEvent);
            return;
          case byte.MaxValue:
            bool flag = false;
            Hashtable properties = (Hashtable) photonEvent[(byte) 249];
            if (photonPlayer == null)
            {
              bool isLocal = this.LocalPlayer.ID == num1;
              this.AddNewPlayer(num1, new PhotonPlayer(isLocal, num1, properties));
              this.ResetPhotonViewsOnSerialize();
            }
            else
            {
              flag = photonPlayer.IsInactive;
              photonPlayer.InternalCacheProperties(properties);
              photonPlayer.IsInactive = false;
            }
            if (num1 == this.LocalPlayer.ID)
            {
              this.UpdatedActorList((int[]) photonEvent[(byte) 252]);
              if (this.lastJoinType == JoinType.JoinOrCreateRoom && this.LocalPlayer.ID == 1)
                NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnCreatedRoom);
              NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnJoinedRoom);
              return;
            }
            NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonPlayerConnected, (object) this.mActors[num1]);
            if (!flag)
              return;
            NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonPlayerActivityChanged, (object) this.mActors[num1]);
            return;
          default:
            if (photonEvent.Code >= (byte) 200 || PhotonNetwork.CallEvent(photonEvent.Code, photonEvent[(byte) 245], num1))
              return;
            Debug.LogWarning((object) ("Warning: Unhandled event " + (object) photonEvent + ". Set PhotonNetwork.OnEventCall."));
            return;
        }
    }
  }

  public void OnMessage(object messages)
  {
  }

  private void SetupEncryption(Dictionary<byte, object> encryptionData)
  {
    if (this.AuthMode == AuthModeOption.Auth && this.DebugOut == 1)
    {
      Debug.LogWarning((object) ("SetupEncryption() called but ignored. Not XB1 compiled. EncryptionData: " + encryptionData.ToStringFull()));
    }
    else
    {
      if (this.DebugOut == 3)
        Debug.Log((object) ("SetupEncryption() got called. " + encryptionData.ToStringFull()));
      switch ((EncryptionMode) (byte) encryptionData[(byte) 0])
      {
        case EncryptionMode.PayloadEncryption:
          this.InitPayloadEncryption((byte[]) encryptionData[(byte) 1]);
          break;
        case EncryptionMode.DatagramEncryption:
          this.InitDatagramEncryption((byte[]) encryptionData[(byte) 1], (byte[]) encryptionData[(byte) 2], false);
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }
  }

  protected internal void UpdatedActorList(int[] actorsInRoom)
  {
    for (int index = 0; index < actorsInRoom.Length; ++index)
    {
      int num = actorsInRoom[index];
      if (this.LocalPlayer.ID != num && !this.mActors.ContainsKey(num))
        this.AddNewPlayer(num, new PhotonPlayer(false, num, string.Empty));
    }
  }

  private void SendVacantViewIds()
  {
    Debug.Log((object) "SendVacantViewIds()");
    List<int> intList = new List<int>();
    foreach (PhotonView photonView in this.photonViewList.Values)
    {
      if (!photonView.isOwnerActive)
        intList.Add(photonView.viewID);
    }
    Debug.Log((object) ("Sending vacant view IDs. Length: " + (object) intList.Count));
    this.OpRaiseEvent((byte) 211, (object) intList.ToArray(), true, (RaiseEventOptions) null);
  }

  public static void SendMonoMessage(
    PhotonNetworkingMessage methodString,
    params object[] parameters)
  {
    HashSet<GameObject> gameObjectSet = PhotonNetwork.SendMonoMessageTargets == null ? PhotonNetwork.FindGameObjectsWithComponent(PhotonNetwork.SendMonoMessageTargetType) : PhotonNetwork.SendMonoMessageTargets;
    string str = methodString.ToString();
    object obj = parameters == null || parameters.Length != 1 ? (object) parameters : parameters[0];
    foreach (GameObject gameObject in gameObjectSet)
    {
      if (Object.op_Inequality((Object) gameObject, (Object) null))
        gameObject.SendMessage(str, obj, (SendMessageOptions) 1);
    }
  }

  protected internal void ExecuteRpc(Hashtable rpcData, int senderID = 0)
  {
    if (rpcData == null || !((Dictionary<object, object>) rpcData).ContainsKey((object) (byte) 0))
    {
      Debug.LogError((object) ("Malformed RPC; this should never occur. Content: " + SupportClass.DictionaryToString((IDictionary) rpcData)));
    }
    else
    {
      int viewID = (int) rpcData[(object) (byte) 0];
      int num1 = 0;
      if (((Dictionary<object, object>) rpcData).ContainsKey((object) (byte) 1))
        num1 = (int) (short) rpcData[(object) (byte) 1];
      string rpc;
      if (((Dictionary<object, object>) rpcData).ContainsKey((object) (byte) 5))
      {
        int index = (int) (byte) rpcData[(object) (byte) 5];
        if (index > PhotonNetwork.PhotonServerSettings.RpcList.Count - 1)
        {
          Debug.LogError((object) ("Could not find RPC with index: " + (object) index + ". Going to ignore! Check PhotonServerSettings.RpcList"));
          return;
        }
        rpc = PhotonNetwork.PhotonServerSettings.RpcList[index];
      }
      else
        rpc = (string) rpcData[(object) (byte) 3];
      object[] parameters1 = (object[]) null;
      if (((Dictionary<object, object>) rpcData).ContainsKey((object) (byte) 4))
        parameters1 = (object[]) rpcData[(object) (byte) 4];
      if (parameters1 == null)
        parameters1 = new object[0];
      PhotonView photonView = this.GetPhotonView(viewID);
      if (Object.op_Equality((Object) photonView, (Object) null))
      {
        int num2 = viewID / PhotonNetwork.MAX_VIEW_IDS;
        bool flag1 = num2 == this.LocalPlayer.ID;
        bool flag2 = num2 == senderID;
        if (flag1)
          Debug.LogWarning((object) ("Received RPC \"" + rpc + "\" for viewID " + (object) viewID + " but this PhotonView does not exist! View was/is ours." + (!flag2 ? (object) " Remote called." : (object) " Owner called.") + " By: " + (object) senderID));
        else
          Debug.LogWarning((object) ("Received RPC \"" + rpc + "\" for viewID " + (object) viewID + " but this PhotonView does not exist! Was remote PV." + (!flag2 ? (object) " Remote called." : (object) " Owner called.") + " By: " + (object) senderID + " Maybe GO was destroyed but RPC not cleaned up."));
      }
      else if (photonView.prefix != num1)
        Debug.LogError((object) ("Received RPC \"" + rpc + "\" on viewID " + (object) viewID + " with a prefix of " + (object) num1 + ", our prefix is " + (object) photonView.prefix + ". The RPC has been ignored."));
      else if (string.IsNullOrEmpty(rpc))
      {
        Debug.LogError((object) ("Malformed RPC; this should never occur. Content: " + SupportClass.DictionaryToString((IDictionary) rpcData)));
      }
      else
      {
        if (PhotonNetwork.logLevel >= PhotonLogLevel.Full)
          Debug.Log((object) ("Received RPC: " + rpc));
        if (photonView.group != (byte) 0 && !this.allowedReceivingGroups.Contains(photonView.group))
          return;
        System.Type[] callParameterTypes = new System.Type[0];
        if (parameters1.Length > 0)
        {
          callParameterTypes = new System.Type[parameters1.Length];
          int index1 = 0;
          for (int index2 = 0; index2 < parameters1.Length; ++index2)
          {
            object obj = parameters1[index2];
            callParameterTypes[index1] = obj != null ? obj.GetType() : (System.Type) null;
            ++index1;
          }
        }
        int num3 = 0;
        int num4 = 0;
        if (!PhotonNetwork.UseRpcMonoBehaviourCache || photonView.RpcMonoBehaviours == null || photonView.RpcMonoBehaviours.Length == 0)
          photonView.RefreshRpcMonoBehaviourCache();
        for (int index3 = 0; index3 < photonView.RpcMonoBehaviours.Length; ++index3)
        {
          MonoBehaviour rpcMonoBehaviour = photonView.RpcMonoBehaviours[index3];
          if (Object.op_Equality((Object) rpcMonoBehaviour, (Object) null))
          {
            Debug.LogError((object) "ERROR You have missing MonoBehaviours on your gameobjects!");
          }
          else
          {
            System.Type type = rpcMonoBehaviour.GetType();
            List<MethodInfo> methodInfoList = (List<MethodInfo>) null;
            if (!this.monoRPCMethodsCache.TryGetValue(type, out methodInfoList))
            {
              List<MethodInfo> methods = SupportClass.GetMethods(type, typeof (PunRPC));
              this.monoRPCMethodsCache[type] = methods;
              methodInfoList = methods;
            }
            if (methodInfoList != null)
            {
              for (int index4 = 0; index4 < methodInfoList.Count; ++index4)
              {
                MethodInfo mo = methodInfoList[index4];
                if (mo.Name.Equals(rpc))
                {
                  ++num4;
                  ParameterInfo[] cachedParemeters = mo.GetCachedParemeters();
                  if (cachedParemeters.Length == callParameterTypes.Length)
                  {
                    if (this.CheckTypeMatch(cachedParemeters, callParameterTypes))
                    {
                      ++num3;
                      object obj = mo.Invoke((object) rpcMonoBehaviour, parameters1);
                      if (PhotonNetwork.StartRpcsAsCoroutine && (object) mo.ReturnType == (object) typeof (IEnumerator))
                        rpcMonoBehaviour.StartCoroutine((IEnumerator) obj);
                    }
                  }
                  else if (cachedParemeters.Length - 1 == callParameterTypes.Length)
                  {
                    if (this.CheckTypeMatch(cachedParemeters, callParameterTypes) && (object) cachedParemeters[cachedParemeters.Length - 1].ParameterType == (object) typeof (PhotonMessageInfo))
                    {
                      ++num3;
                      int timestamp = (int) rpcData[(object) (byte) 2];
                      object[] parameters2 = new object[parameters1.Length + 1];
                      parameters1.CopyTo((Array) parameters2, 0);
                      parameters2[parameters2.Length - 1] = (object) new PhotonMessageInfo(this.GetPlayerWithId(senderID), timestamp, photonView);
                      object obj = mo.Invoke((object) rpcMonoBehaviour, parameters2);
                      if (PhotonNetwork.StartRpcsAsCoroutine && (object) mo.ReturnType == (object) typeof (IEnumerator))
                        rpcMonoBehaviour.StartCoroutine((IEnumerator) obj);
                    }
                  }
                  else if (cachedParemeters.Length == 1 && cachedParemeters[0].ParameterType.IsArray)
                  {
                    ++num3;
                    object obj = mo.Invoke((object) rpcMonoBehaviour, new object[1]
                    {
                      (object) parameters1
                    });
                    if (PhotonNetwork.StartRpcsAsCoroutine && (object) mo.ReturnType == (object) typeof (IEnumerator))
                      rpcMonoBehaviour.StartCoroutine((IEnumerator) obj);
                  }
                }
              }
            }
          }
        }
        if (num3 == 1)
          return;
        string str = string.Empty;
        for (int index = 0; index < callParameterTypes.Length; ++index)
        {
          System.Type type = callParameterTypes[index];
          if (str != string.Empty)
            str += ", ";
          str = (object) type != null ? str + type.Name : str + "null";
        }
        if (num3 == 0)
        {
          if (num4 == 0)
            Debug.LogError((object) ("PhotonView with ID " + (object) viewID + " has no method \"" + rpc + "\" marked with the [PunRPC](C#) or @PunRPC(JS) property! Args: " + str));
          else
            Debug.LogError((object) ("PhotonView with ID " + (object) viewID + " has no method \"" + rpc + "\" that takes " + (object) callParameterTypes.Length + " argument(s): " + str));
        }
        else
          Debug.LogError((object) ("PhotonView with ID " + (object) viewID + " has " + (object) num3 + " methods \"" + rpc + "\" that takes " + (object) callParameterTypes.Length + " argument(s): " + str + ". Should be just one?"));
      }
    }
  }

  private bool CheckTypeMatch(ParameterInfo[] methodParameters, System.Type[] callParameterTypes)
  {
    if (methodParameters.Length < callParameterTypes.Length)
      return false;
    for (int index = 0; index < callParameterTypes.Length; ++index)
    {
      System.Type parameterType = methodParameters[index].ParameterType;
      if ((object) callParameterTypes[index] != null && !parameterType.IsAssignableFrom(callParameterTypes[index]) && (!parameterType.IsEnum || !Enum.GetUnderlyingType(parameterType).IsAssignableFrom(callParameterTypes[index])))
        return false;
    }
    return true;
  }

  internal Hashtable SendInstantiate(
    string prefabName,
    Vector3 position,
    Quaternion rotation,
    byte group,
    int[] viewIDs,
    object[] data,
    bool isGlobalObject)
  {
    int viewId = viewIDs[0];
    Hashtable customEventContent = new Hashtable();
    customEventContent[(object) (byte) 0] = (object) prefabName;
    if (Vector3.op_Inequality(position, Vector3.zero))
      customEventContent[(object) (byte) 1] = (object) position;
    if (Quaternion.op_Inequality(rotation, Quaternion.identity))
      customEventContent[(object) (byte) 2] = (object) rotation;
    if (group != (byte) 0)
      customEventContent[(object) (byte) 3] = (object) group;
    if (viewIDs.Length > 1)
      customEventContent[(object) (byte) 4] = (object) viewIDs;
    if (data != null)
      customEventContent[(object) (byte) 5] = (object) data;
    if (this.currentLevelPrefix > (short) 0)
      customEventContent[(object) (byte) 8] = (object) this.currentLevelPrefix;
    customEventContent[(object) (byte) 6] = (object) PhotonNetwork.ServerTimestamp;
    customEventContent[(object) (byte) 7] = (object) viewId;
    this.OpRaiseEvent((byte) 202, (object) customEventContent, true, new RaiseEventOptions()
    {
      CachingOption = !isGlobalObject ? EventCaching.AddToRoomCache : EventCaching.AddToRoomCacheGlobal
    });
    return customEventContent;
  }

  internal GameObject DoInstantiate(
    Hashtable evData,
    PhotonPlayer photonPlayer,
    GameObject resourceGameObject)
  {
    string str = (string) evData[(object) (byte) 0];
    int timestamp = (int) evData[(object) (byte) 6];
    int instantiationId = (int) evData[(object) (byte) 7];
    Vector3 position = !((Dictionary<object, object>) evData).ContainsKey((object) (byte) 1) ? Vector3.zero : (Vector3) evData[(object) (byte) 1];
    Quaternion identity = Quaternion.identity;
    if (((Dictionary<object, object>) evData).ContainsKey((object) (byte) 2))
      identity = (Quaternion) evData[(object) (byte) 2];
    byte num1 = 0;
    if (((Dictionary<object, object>) evData).ContainsKey((object) (byte) 3))
      num1 = (byte) evData[(object) (byte) 3];
    short num2 = 0;
    if (((Dictionary<object, object>) evData).ContainsKey((object) (byte) 8))
      num2 = (short) evData[(object) (byte) 8];
    int[] numArray;
    if (((Dictionary<object, object>) evData).ContainsKey((object) (byte) 4))
      numArray = (int[]) evData[(object) (byte) 4];
    else
      numArray = new int[1]{ instantiationId };
    object[] instantiationData = !((Dictionary<object, object>) evData).ContainsKey((object) (byte) 5) ? (object[]) null : (object[]) evData[(object) (byte) 5];
    if (num1 != (byte) 0 && !this.allowedReceivingGroups.Contains(num1))
      return (GameObject) null;
    if (this.ObjectPool != null)
    {
      GameObject go = this.ObjectPool.Instantiate(str, position, identity);
      PhotonView[] photonViewsInChildren = go.GetPhotonViewsInChildren();
      if (photonViewsInChildren.Length != numArray.Length)
        throw new Exception("Error in Instantiation! The resource's PhotonView count is not the same as in incoming data.");
      for (int index = 0; index < photonViewsInChildren.Length; ++index)
      {
        photonViewsInChildren[index].didAwake = false;
        photonViewsInChildren[index].viewID = 0;
        photonViewsInChildren[index].prefix = (int) num2;
        photonViewsInChildren[index].instantiationId = instantiationId;
        photonViewsInChildren[index].isRuntimeInstantiated = true;
        photonViewsInChildren[index].instantiationDataField = instantiationData;
        photonViewsInChildren[index].didAwake = true;
        photonViewsInChildren[index].viewID = numArray[index];
      }
      go.SendMessage(NetworkingPeer.OnPhotonInstantiateString, (object) new PhotonMessageInfo(photonPlayer, timestamp, (PhotonView) null), (SendMessageOptions) 1);
      return go;
    }
    if (Object.op_Equality((Object) resourceGameObject, (Object) null))
    {
      if (!NetworkingPeer.UsePrefabCache || !NetworkingPeer.PrefabCache.TryGetValue(str, out resourceGameObject))
      {
        resourceGameObject = (GameObject) Resources.Load(str, typeof (GameObject));
        if (NetworkingPeer.UsePrefabCache)
          NetworkingPeer.PrefabCache.Add(str, resourceGameObject);
      }
      if (Object.op_Equality((Object) resourceGameObject, (Object) null))
      {
        Debug.LogError((object) ("PhotonNetwork error: Could not Instantiate the prefab [" + str + "]. Please verify you have this gameobject in a Resources folder."));
        return (GameObject) null;
      }
    }
    PhotonView[] photonViewsInChildren1 = resourceGameObject.GetPhotonViewsInChildren();
    if (photonViewsInChildren1.Length != numArray.Length)
      throw new Exception("Error in Instantiation! The resource's PhotonView count is not the same as in incoming data.");
    for (int index = 0; index < numArray.Length; ++index)
    {
      photonViewsInChildren1[index].viewID = numArray[index];
      photonViewsInChildren1[index].prefix = (int) num2;
      photonViewsInChildren1[index].instantiationId = instantiationId;
      photonViewsInChildren1[index].isRuntimeInstantiated = true;
    }
    this.StoreInstantiationData(instantiationId, instantiationData);
    GameObject gameObject = Object.Instantiate<GameObject>(resourceGameObject, position, identity);
    for (int index = 0; index < numArray.Length; ++index)
    {
      photonViewsInChildren1[index].viewID = 0;
      photonViewsInChildren1[index].prefix = -1;
      photonViewsInChildren1[index].prefixBackup = -1;
      photonViewsInChildren1[index].instantiationId = -1;
      photonViewsInChildren1[index].isRuntimeInstantiated = false;
    }
    this.RemoveInstantiationData(instantiationId);
    gameObject.SendMessage(NetworkingPeer.OnPhotonInstantiateString, (object) new PhotonMessageInfo(photonPlayer, timestamp, (PhotonView) null), (SendMessageOptions) 1);
    return gameObject;
  }

  private void StoreInstantiationData(int instantiationId, object[] instantiationData)
  {
    this.tempInstantiationData[instantiationId] = instantiationData;
  }

  public object[] FetchInstantiationData(int instantiationId)
  {
    object[] objArray = (object[]) null;
    if (instantiationId == 0)
      return (object[]) null;
    this.tempInstantiationData.TryGetValue(instantiationId, out objArray);
    return objArray;
  }

  private void RemoveInstantiationData(int instantiationId)
  {
    this.tempInstantiationData.Remove(instantiationId);
  }

  public void DestroyPlayerObjects(int playerId, bool localOnly)
  {
    if (playerId <= 0)
    {
      Debug.LogError((object) ("Failed to Destroy objects of playerId: " + (object) playerId));
    }
    else
    {
      if (!localOnly)
      {
        this.OpRemoveFromServerInstantiationsOfPlayer(playerId);
        this.OpCleanRpcBuffer(playerId);
        this.SendDestroyOfPlayer(playerId);
      }
      HashSet<GameObject> gameObjectSet = new HashSet<GameObject>();
      foreach (PhotonView photonView in this.photonViewList.Values)
      {
        if (Object.op_Inequality((Object) photonView, (Object) null) && photonView.CreatorActorNr == playerId)
          gameObjectSet.Add(((Component) photonView).gameObject);
      }
      foreach (GameObject go in gameObjectSet)
        this.RemoveInstantiatedGO(go, true);
      foreach (PhotonView photonView in this.photonViewList.Values)
      {
        if (photonView.ownerId == playerId)
          photonView.ownerId = photonView.CreatorActorNr;
      }
    }
  }

  public void DestroyAll(bool localOnly)
  {
    if (!localOnly)
    {
      this.OpRemoveCompleteCache();
      this.SendDestroyOfAll();
    }
    this.LocalCleanupAnythingInstantiated(true);
  }

  protected internal void RemoveInstantiatedGO(GameObject go, bool localOnly)
  {
    if (Object.op_Equality((Object) go, (Object) null))
    {
      Debug.LogError((object) "Failed to 'network-remove' GameObject because it's null.");
    }
    else
    {
      PhotonView[] componentsInChildren = go.GetComponentsInChildren<PhotonView>(true);
      if (componentsInChildren == null || componentsInChildren.Length <= 0)
      {
        Debug.LogError((object) ("Failed to 'network-remove' GameObject because has no PhotonView components: " + (object) go));
      }
      else
      {
        PhotonView photonView = componentsInChildren[0];
        int creatorActorNr = photonView.CreatorActorNr;
        int instantiationId = photonView.instantiationId;
        if (!localOnly)
        {
          if (!photonView.isMine)
          {
            Debug.LogError((object) ("Failed to 'network-remove' GameObject. Client is neither owner nor masterClient taking over for owner who left: " + (object) photonView));
            return;
          }
          if (instantiationId < 1)
          {
            Debug.LogError((object) ("Failed to 'network-remove' GameObject because it is missing a valid InstantiationId on view: " + (object) photonView + ". Not Destroying GameObject or PhotonViews!"));
            return;
          }
        }
        if (!localOnly)
          this.ServerCleanInstantiateAndDestroy(instantiationId, creatorActorNr, photonView.isRuntimeInstantiated);
        for (int index = componentsInChildren.Length - 1; index >= 0; --index)
        {
          PhotonView view = componentsInChildren[index];
          if (!Object.op_Equality((Object) view, (Object) null))
          {
            if (view.instantiationId >= 1)
              this.LocalCleanPhotonView(view);
            if (!localOnly)
              this.OpCleanRpcBuffer(view);
          }
        }
        if (PhotonNetwork.logLevel >= PhotonLogLevel.Full)
          Debug.Log((object) ("Network destroy Instantiated GO: " + ((Object) go).name));
        if (this.ObjectPool != null)
        {
          foreach (PhotonView photonViewsInChild in go.GetPhotonViewsInChildren())
            photonViewsInChild.viewID = 0;
          this.ObjectPool.Destroy(go);
        }
        else
          Object.Destroy((Object) go);
      }
    }
  }

  private void ServerCleanInstantiateAndDestroy(
    int instantiateId,
    int creatorId,
    bool isRuntimeInstantiated)
  {
    Hashtable customEventContent1 = new Hashtable();
    customEventContent1[(object) (byte) 7] = (object) instantiateId;
    RaiseEventOptions raiseEventOptions1 = new RaiseEventOptions()
    {
      CachingOption = EventCaching.RemoveFromRoomCache,
      TargetActors = new int[1]{ creatorId }
    };
    this.OpRaiseEvent((byte) 202, (object) customEventContent1, true, raiseEventOptions1);
    Hashtable customEventContent2 = new Hashtable();
    customEventContent2[(object) (byte) 0] = (object) instantiateId;
    RaiseEventOptions raiseEventOptions2 = (RaiseEventOptions) null;
    if (!isRuntimeInstantiated)
    {
      raiseEventOptions2 = new RaiseEventOptions();
      raiseEventOptions2.CachingOption = EventCaching.AddToRoomCacheGlobal;
      Debug.Log((object) ("Destroying GO as global. ID: " + (object) instantiateId));
    }
    this.OpRaiseEvent((byte) 204, (object) customEventContent2, true, raiseEventOptions2);
  }

  private void SendDestroyOfPlayer(int actorNr)
  {
    this.OpRaiseEvent((byte) 207, (object) new Hashtable()
    {
      [(object) (byte) 0] = (object) actorNr
    }, true, (RaiseEventOptions) null);
  }

  private void SendDestroyOfAll()
  {
    this.OpRaiseEvent((byte) 207, (object) new Hashtable()
    {
      [(object) (byte) 0] = (object) -1
    }, true, (RaiseEventOptions) null);
  }

  private void OpRemoveFromServerInstantiationsOfPlayer(int actorNr)
  {
    this.OpRaiseEvent((byte) 202, (object) null, true, new RaiseEventOptions()
    {
      CachingOption = EventCaching.RemoveFromRoomCache,
      TargetActors = new int[1]{ actorNr }
    });
  }

  protected internal void RequestOwnership(int viewID, int fromOwner)
  {
    Debug.Log((object) ("RequestOwnership(): " + (object) viewID + " from: " + (object) fromOwner + " Time: " + (object) (System.Environment.TickCount % 1000)));
    this.OpRaiseEvent((byte) 209, (object) new int[2]
    {
      viewID,
      fromOwner
    }, true, new RaiseEventOptions()
    {
      Receivers = ReceiverGroup.All
    });
  }

  protected internal void TransferOwnership(int viewID, int playerID)
  {
    Debug.Log((object) ("TransferOwnership() view " + (object) viewID + " to: " + (object) playerID + " Time: " + (object) (System.Environment.TickCount % 1000)));
    this.OpRaiseEvent((byte) 210, (object) new int[2]
    {
      viewID,
      playerID
    }, true, new RaiseEventOptions()
    {
      Receivers = ReceiverGroup.All
    });
  }

  public bool LocalCleanPhotonView(PhotonView view)
  {
    view.removedFromLocalViewList = true;
    return this.photonViewList.Remove(view.viewID);
  }

  public PhotonView GetPhotonView(int viewID)
  {
    PhotonView photonView1 = (PhotonView) null;
    this.photonViewList.TryGetValue(viewID, out photonView1);
    if (Object.op_Equality((Object) photonView1, (Object) null))
    {
      foreach (PhotonView photonView2 in Object.FindObjectsOfType(typeof (PhotonView)) as PhotonView[])
      {
        if (photonView2.viewID == viewID)
        {
          if (photonView2.didAwake)
            Debug.LogWarning((object) ("Had to lookup view that wasn't in photonViewList: " + (object) photonView2));
          return photonView2;
        }
      }
    }
    return photonView1;
  }

  public void RegisterPhotonView(PhotonView netView)
  {
    if (!Application.isPlaying)
      this.photonViewList = new Dictionary<int, PhotonView>();
    else if (netView.viewID == 0)
    {
      Debug.Log((object) ("PhotonView register is ignored, because viewID is 0. No id assigned yet to: " + (object) netView));
    }
    else
    {
      PhotonView photonView = (PhotonView) null;
      if (this.photonViewList.TryGetValue(netView.viewID, out photonView))
      {
        if (!Object.op_Inequality((Object) netView, (Object) photonView))
          return;
        Debug.LogError((object) string.Format("PhotonView ID duplicate found: {0}. New: {1} old: {2}. Maybe one wasn't destroyed on scene load?! Check for 'DontDestroyOnLoad'. Destroying old entry, adding new.", (object) netView.viewID, (object) netView, (object) photonView));
        this.RemoveInstantiatedGO(((Component) photonView).gameObject, true);
      }
      this.photonViewList.Add(netView.viewID, netView);
      if (PhotonNetwork.logLevel < PhotonLogLevel.Full)
        return;
      Debug.Log((object) ("Registered PhotonView: " + (object) netView.viewID));
    }
  }

  public void OpCleanRpcBuffer(int actorNumber)
  {
    this.OpRaiseEvent((byte) 200, (object) null, true, new RaiseEventOptions()
    {
      CachingOption = EventCaching.RemoveFromRoomCache,
      TargetActors = new int[1]{ actorNumber }
    });
  }

  public void OpRemoveCompleteCacheOfPlayer(int actorNumber)
  {
    this.OpRaiseEvent((byte) 0, (object) null, true, new RaiseEventOptions()
    {
      CachingOption = EventCaching.RemoveFromRoomCache,
      TargetActors = new int[1]{ actorNumber }
    });
  }

  public void OpRemoveCompleteCache()
  {
    this.OpRaiseEvent((byte) 0, (object) null, true, new RaiseEventOptions()
    {
      CachingOption = EventCaching.RemoveFromRoomCache,
      Receivers = ReceiverGroup.MasterClient
    });
  }

  private void RemoveCacheOfLeftPlayers()
  {
    this.SendOperation((byte) 253, new Dictionary<byte, object>()
    {
      [(byte) 244] = (object) (byte) 0,
      [(byte) 247] = (object) (byte) 7
    }, SendOptions.SendReliable);
  }

  public void CleanRpcBufferIfMine(PhotonView view)
  {
    if (view.ownerId != this.LocalPlayer.ID && !this.LocalPlayer.IsMasterClient)
      Debug.LogError((object) ("Cannot remove cached RPCs on a PhotonView thats not ours! " + (object) view.owner + " scene: " + (object) view.isSceneView));
    else
      this.OpCleanRpcBuffer(view);
  }

  public void OpCleanRpcBuffer(PhotonView view)
  {
    Hashtable customEventContent = new Hashtable();
    customEventContent[(object) (byte) 0] = (object) view.viewID;
    RaiseEventOptions raiseEventOptions = new RaiseEventOptions()
    {
      CachingOption = EventCaching.RemoveFromRoomCache
    };
    this.OpRaiseEvent((byte) 200, (object) customEventContent, true, raiseEventOptions);
  }

  public void RemoveRPCsInGroup(int group)
  {
    foreach (PhotonView view in this.photonViewList.Values)
    {
      if ((int) view.group == group)
        this.CleanRpcBufferIfMine(view);
    }
  }

  public void SetLevelPrefix(short prefix) => this.currentLevelPrefix = prefix;

  internal void RPC(
    PhotonView view,
    string methodName,
    PhotonTargets target,
    PhotonPlayer player,
    bool encrypt,
    params object[] parameters)
  {
    if (this.blockSendingGroups.Contains(view.group))
      return;
    if (view.viewID < 1)
      Debug.LogError((object) ("Illegal view ID:" + (object) view.viewID + " method: " + methodName + " GO:" + ((Object) ((Component) view).gameObject).name));
    if (PhotonNetwork.logLevel >= PhotonLogLevel.Full)
      Debug.Log((object) ("Sending RPC \"" + methodName + "\" to target: " + (object) target + " or player:" + (object) player + "."));
    Hashtable hashtable = new Hashtable();
    hashtable[(object) (byte) 0] = (object) view.viewID;
    if (view.prefix > 0)
      hashtable[(object) (byte) 1] = (object) (short) view.prefix;
    hashtable[(object) (byte) 2] = (object) PhotonNetwork.ServerTimestamp;
    int num = 0;
    if (this.rpcShortcuts.TryGetValue(methodName, out num))
      hashtable[(object) (byte) 5] = (object) (byte) num;
    else
      hashtable[(object) (byte) 3] = (object) methodName;
    if (parameters != null && parameters.Length > 0)
      hashtable[(object) (byte) 4] = (object) parameters;
    if (player != null)
    {
      if (this.LocalPlayer.ID == player.ID)
      {
        this.ExecuteRpc(hashtable, player.ID);
      }
      else
      {
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions()
        {
          TargetActors = new int[1]{ player.ID },
          Encrypt = encrypt
        };
        this.OpRaiseEvent((byte) 200, (object) hashtable, true, raiseEventOptions);
      }
    }
    else
    {
      switch (target)
      {
        case PhotonTargets.All:
          RaiseEventOptions raiseEventOptions1 = new RaiseEventOptions()
          {
            InterestGroup = view.group,
            Encrypt = encrypt
          };
          this.OpRaiseEvent((byte) 200, (object) hashtable, true, raiseEventOptions1);
          this.ExecuteRpc(hashtable, this.LocalPlayer.ID);
          break;
        case PhotonTargets.Others:
          RaiseEventOptions raiseEventOptions2 = new RaiseEventOptions()
          {
            InterestGroup = view.group,
            Encrypt = encrypt
          };
          this.OpRaiseEvent((byte) 200, (object) hashtable, true, raiseEventOptions2);
          break;
        case PhotonTargets.MasterClient:
          if (this.mMasterClientId == this.LocalPlayer.ID)
          {
            this.ExecuteRpc(hashtable, this.LocalPlayer.ID);
            break;
          }
          RaiseEventOptions raiseEventOptions3 = new RaiseEventOptions()
          {
            Receivers = ReceiverGroup.MasterClient,
            Encrypt = encrypt
          };
          this.OpRaiseEvent((byte) 200, (object) hashtable, true, raiseEventOptions3);
          break;
        case PhotonTargets.AllBuffered:
          RaiseEventOptions raiseEventOptions4 = new RaiseEventOptions()
          {
            CachingOption = EventCaching.AddToRoomCache,
            Encrypt = encrypt
          };
          this.OpRaiseEvent((byte) 200, (object) hashtable, true, raiseEventOptions4);
          this.ExecuteRpc(hashtable, this.LocalPlayer.ID);
          break;
        case PhotonTargets.OthersBuffered:
          RaiseEventOptions raiseEventOptions5 = new RaiseEventOptions()
          {
            CachingOption = EventCaching.AddToRoomCache,
            Encrypt = encrypt
          };
          this.OpRaiseEvent((byte) 200, (object) hashtable, true, raiseEventOptions5);
          break;
        case PhotonTargets.AllViaServer:
          RaiseEventOptions raiseEventOptions6 = new RaiseEventOptions()
          {
            InterestGroup = view.group,
            Receivers = ReceiverGroup.All,
            Encrypt = encrypt
          };
          this.OpRaiseEvent((byte) 200, (object) hashtable, true, raiseEventOptions6);
          if (!PhotonNetwork.offlineMode)
            break;
          this.ExecuteRpc(hashtable, this.LocalPlayer.ID);
          break;
        case PhotonTargets.AllBufferedViaServer:
          RaiseEventOptions raiseEventOptions7 = new RaiseEventOptions()
          {
            InterestGroup = view.group,
            Receivers = ReceiverGroup.All,
            CachingOption = EventCaching.AddToRoomCache,
            Encrypt = encrypt
          };
          this.OpRaiseEvent((byte) 200, (object) hashtable, true, raiseEventOptions7);
          if (!PhotonNetwork.offlineMode)
            break;
          this.ExecuteRpc(hashtable, this.LocalPlayer.ID);
          break;
        default:
          Debug.LogError((object) ("Unsupported target enum: " + (object) target));
          break;
      }
    }
  }

  public void SetInterestGroups(byte[] disableGroups, byte[] enableGroups)
  {
    if (disableGroups != null)
    {
      if (disableGroups.Length == 0)
      {
        this.allowedReceivingGroups.Clear();
      }
      else
      {
        for (int index = 0; index < disableGroups.Length; ++index)
        {
          byte disableGroup = disableGroups[index];
          if (disableGroup <= (byte) 0)
            Debug.LogError((object) ("Error: PhotonNetwork.SetInterestGroups was called with an illegal group number: " + (object) disableGroup + ". The group number should be at least 1."));
          else if (this.allowedReceivingGroups.Contains(disableGroup))
            this.allowedReceivingGroups.Remove(disableGroup);
        }
      }
    }
    if (enableGroups != null)
    {
      if (enableGroups.Length == 0)
      {
        for (byte index = 0; index < byte.MaxValue; ++index)
          this.allowedReceivingGroups.Add(index);
        this.allowedReceivingGroups.Add(byte.MaxValue);
      }
      else
      {
        for (int index = 0; index < enableGroups.Length; ++index)
        {
          byte enableGroup = enableGroups[index];
          if (enableGroup <= (byte) 0)
            Debug.LogError((object) ("Error: PhotonNetwork.SetInterestGroups was called with an illegal group number: " + (object) enableGroup + ". The group number should be at least 1."));
          else
            this.allowedReceivingGroups.Add(enableGroup);
        }
      }
    }
    if (PhotonNetwork.offlineMode)
      return;
    this.OpChangeGroups(disableGroups, enableGroups);
  }

  public void SetSendingEnabled(byte group, bool enabled)
  {
    if (!enabled)
      this.blockSendingGroups.Add(group);
    else
      this.blockSendingGroups.Remove(group);
  }

  public void SetSendingEnabled(byte[] disableGroups, byte[] enableGroups)
  {
    if (disableGroups != null)
    {
      for (int index = 0; index < disableGroups.Length; ++index)
        this.blockSendingGroups.Add(disableGroups[index]);
    }
    if (enableGroups == null)
      return;
    for (int index = 0; index < enableGroups.Length; ++index)
      this.blockSendingGroups.Remove(enableGroups[index]);
  }

  public void NewSceneLoaded()
  {
    if (this.loadingLevelAndPausedNetwork)
    {
      this.loadingLevelAndPausedNetwork = false;
      PhotonNetwork.isMessageQueueRunning = true;
    }
    List<int> intList = new List<int>();
    foreach (KeyValuePair<int, PhotonView> photonView in this.photonViewList)
    {
      if (Object.op_Equality((Object) photonView.Value, (Object) null))
        intList.Add(photonView.Key);
    }
    for (int index = 0; index < intList.Count; ++index)
      this.photonViewList.Remove(intList[index]);
    if (intList.Count <= 0 || PhotonNetwork.logLevel < PhotonLogLevel.Informational)
      return;
    Debug.Log((object) ("New level loaded. Removed " + (object) intList.Count + " scene view IDs from last level."));
  }

  public void RunViewUpdate()
  {
    if (!PhotonNetwork.connected || PhotonNetwork.offlineMode || this.mActors == null)
      return;
    if (PhotonNetwork.inRoom && this._AsyncLevelLoadingOperation != null && this._AsyncLevelLoadingOperation.isDone)
    {
      this._AsyncLevelLoadingOperation = (AsyncOperation) null;
      this.LoadLevelIfSynced();
    }
    if (this.mActors.Count <= 1)
      return;
    int num = 0;
    this.options.Reset();
    List<int> intList = (List<int>) null;
    Dictionary<int, PhotonView>.Enumerator enumerator = this.photonViewList.GetEnumerator();
    while (enumerator.MoveNext())
    {
      PhotonView view = enumerator.Current.Value;
      if (Object.op_Equality((Object) view, (Object) null))
      {
        Debug.LogError((object) string.Format("PhotonView with ID {0} wasn't properly unregistered! Please report this case to developer@photonengine.com", (object) enumerator.Current.Key));
        if (intList == null)
          intList = new List<int>(4);
        intList.Add(enumerator.Current.Key);
      }
      else if (view.synchronization != ViewSynchronization.Off && view.isMine && ((Component) view).gameObject.activeInHierarchy && !this.blockSendingGroups.Contains(view.group))
      {
        object[] objArray = this.OnSerializeWrite(view);
        if (objArray != null)
        {
          if (view.synchronization == ViewSynchronization.ReliableDeltaCompressed || view.mixedModeIsReliable)
          {
            Hashtable customEventContent = (Hashtable) null;
            if (!this.dataPerGroupReliable.TryGetValue((int) view.group, out customEventContent))
            {
              customEventContent = new Hashtable(NetworkingPeer.ObjectsInOneUpdate);
              this.dataPerGroupReliable[(int) view.group] = customEventContent;
            }
            ((Dictionary<object, object>) customEventContent).Add((object) (byte) (((Dictionary<object, object>) customEventContent).Count + 10), (object) objArray);
            ++num;
            if (((Dictionary<object, object>) customEventContent).Count >= NetworkingPeer.ObjectsInOneUpdate)
            {
              num -= ((Dictionary<object, object>) customEventContent).Count;
              this.options.InterestGroup = view.group;
              customEventContent[(object) (byte) 0] = (object) PhotonNetwork.ServerTimestamp;
              if (this.currentLevelPrefix >= (short) 0)
                customEventContent[(object) (byte) 1] = (object) this.currentLevelPrefix;
              this.OpRaiseEvent((byte) 206, (object) customEventContent, true, this.options);
              ((Dictionary<object, object>) customEventContent).Clear();
            }
          }
          else
          {
            Hashtable customEventContent = (Hashtable) null;
            if (!this.dataPerGroupUnreliable.TryGetValue((int) view.group, out customEventContent))
            {
              customEventContent = new Hashtable(NetworkingPeer.ObjectsInOneUpdate);
              this.dataPerGroupUnreliable[(int) view.group] = customEventContent;
            }
            ((Dictionary<object, object>) customEventContent).Add((object) (byte) (((Dictionary<object, object>) customEventContent).Count + 10), (object) objArray);
            ++num;
            if (((Dictionary<object, object>) customEventContent).Count >= NetworkingPeer.ObjectsInOneUpdate)
            {
              num -= ((Dictionary<object, object>) customEventContent).Count;
              this.options.InterestGroup = view.group;
              customEventContent[(object) (byte) 0] = (object) PhotonNetwork.ServerTimestamp;
              if (this.currentLevelPrefix >= (short) 0)
                customEventContent[(object) (byte) 1] = (object) this.currentLevelPrefix;
              this.OpRaiseEvent((byte) 201, (object) customEventContent, false, this.options);
              ((Dictionary<object, object>) customEventContent).Clear();
            }
          }
        }
      }
    }
    if (intList != null)
    {
      int index = 0;
      for (int count = intList.Count; index < count; ++index)
        this.photonViewList.Remove(intList[index]);
    }
    if (num == 0)
      return;
    foreach (int key in this.dataPerGroupReliable.Keys)
    {
      this.options.InterestGroup = (byte) key;
      Hashtable customEventContent = this.dataPerGroupReliable[key];
      if (((Dictionary<object, object>) customEventContent).Count != 0)
      {
        customEventContent[(object) (byte) 0] = (object) PhotonNetwork.ServerTimestamp;
        if (this.currentLevelPrefix >= (short) 0)
          customEventContent[(object) (byte) 1] = (object) this.currentLevelPrefix;
        this.OpRaiseEvent((byte) 206, (object) customEventContent, true, this.options);
        ((Dictionary<object, object>) customEventContent).Clear();
      }
    }
    foreach (int key in this.dataPerGroupUnreliable.Keys)
    {
      this.options.InterestGroup = (byte) key;
      Hashtable customEventContent = this.dataPerGroupUnreliable[key];
      if (((Dictionary<object, object>) customEventContent).Count != 0)
      {
        customEventContent[(object) (byte) 0] = (object) PhotonNetwork.ServerTimestamp;
        if (this.currentLevelPrefix >= (short) 0)
          customEventContent[(object) (byte) 1] = (object) this.currentLevelPrefix;
        this.OpRaiseEvent((byte) 201, (object) customEventContent, false, this.options);
        ((Dictionary<object, object>) customEventContent).Clear();
      }
    }
  }

  private object[] OnSerializeWrite(PhotonView view)
  {
    if (view.synchronization == ViewSynchronization.Off)
      return (object[]) null;
    PhotonMessageInfo info = new PhotonMessageInfo(this.LocalPlayer, PhotonNetwork.ServerTimestamp, view);
    this.pStream.ResetWriteStream();
    this.pStream.SendNext((object) null);
    this.pStream.SendNext((object) null);
    this.pStream.SendNext((object) null);
    view.SerializeView(this.pStream, info);
    if (this.pStream.Count <= 3)
      return (object[]) null;
    object[] array = this.pStream.ToArray();
    array[0] = (object) view.viewID;
    array[1] = (object) false;
    array[2] = (object) null;
    if (view.synchronization == ViewSynchronization.Unreliable)
      return array;
    if (view.synchronization == ViewSynchronization.UnreliableOnChange)
    {
      if (this.AlmostEquals(array, view.lastOnSerializeDataSent))
      {
        if (view.mixedModeIsReliable)
          return (object[]) null;
        view.mixedModeIsReliable = true;
        view.lastOnSerializeDataSent = array;
      }
      else
      {
        view.mixedModeIsReliable = false;
        view.lastOnSerializeDataSent = array;
      }
      return array;
    }
    if (view.synchronization != ViewSynchronization.ReliableDeltaCompressed)
      return (object[]) null;
    object[] objArray = this.DeltaCompressionWrite(view.lastOnSerializeDataSent, array);
    view.lastOnSerializeDataSent = array;
    return objArray;
  }

  private void OnSerializeRead(
    object[] data,
    PhotonPlayer sender,
    int networkTime,
    short correctPrefix)
  {
    int viewID = (int) data[0];
    PhotonView photonView = this.GetPhotonView(viewID);
    if (Object.op_Equality((Object) photonView, (Object) null))
      Debug.LogWarning((object) ("Received OnSerialization for view ID " + (object) viewID + ". We have no such PhotonView! Ignored this if you're leaving a room. State: " + (object) this.State));
    else if (photonView.prefix > 0 && (int) correctPrefix != photonView.prefix)
    {
      Debug.LogError((object) ("Received OnSerialization for view ID " + (object) viewID + " with prefix " + (object) correctPrefix + ". Our prefix is " + (object) photonView.prefix));
    }
    else
    {
      if (photonView.group != (byte) 0 && !this.allowedReceivingGroups.Contains(photonView.group))
        return;
      if (photonView.synchronization == ViewSynchronization.ReliableDeltaCompressed)
      {
        object[] objArray = this.DeltaCompressionRead(photonView.lastOnSerializeDataReceived, data);
        if (objArray == null)
        {
          if (PhotonNetwork.logLevel < PhotonLogLevel.Informational)
            return;
          Debug.Log((object) ("Skipping packet for " + ((Object) photonView).name + " [" + (object) photonView.viewID + "] as we haven't received a full packet for delta compression yet. This is OK if it happens for the first few frames after joining a game."));
          return;
        }
        photonView.lastOnSerializeDataReceived = objArray;
        data = objArray;
      }
      if (sender.ID != photonView.ownerId && (!photonView.OwnerShipWasTransfered || photonView.ownerId == 0) && photonView.currentMasterID == -1)
        photonView.ownerId = sender.ID;
      this.readStream.SetReadStream(data, (byte) 3);
      PhotonMessageInfo info = new PhotonMessageInfo(sender, networkTime, photonView);
      photonView.DeserializeView(this.readStream, info);
    }
  }

  private object[] DeltaCompressionWrite(object[] previousContent, object[] currentContent)
  {
    if (currentContent == null || previousContent == null || previousContent.Length != currentContent.Length)
      return currentContent;
    if (currentContent.Length <= 3)
      return (object[]) null;
    object[] objArray = previousContent;
    objArray[1] = (object) false;
    int num = 0;
    Queue<int> intQueue = (Queue<int>) null;
    for (int index = 3; index < currentContent.Length; ++index)
    {
      object one = currentContent[index];
      object two = previousContent[index];
      if (this.AlmostEquals(one, two))
      {
        ++num;
        objArray[index] = (object) null;
      }
      else
      {
        objArray[index] = one;
        if (one == null)
        {
          if (intQueue == null)
            intQueue = new Queue<int>(currentContent.Length);
          intQueue.Enqueue(index);
        }
      }
    }
    if (num > 0)
    {
      if (num == currentContent.Length - 3)
        return (object[]) null;
      objArray[1] = (object) true;
      if (intQueue != null)
        objArray[2] = (object) intQueue.ToArray();
    }
    objArray[0] = currentContent[0];
    return objArray;
  }

  private object[] DeltaCompressionRead(object[] lastOnSerializeDataReceived, object[] incomingData)
  {
    if (!(bool) incomingData[1])
      return incomingData;
    if (lastOnSerializeDataReceived == null)
      return (object[]) null;
    int[] target = incomingData[2] as int[];
    for (int nr = 3; nr < incomingData.Length; ++nr)
    {
      if ((target == null || !Extensions.Contains(target, nr)) && incomingData[nr] == null)
      {
        object obj = lastOnSerializeDataReceived[nr];
        incomingData[nr] = obj;
      }
    }
    return incomingData;
  }

  private bool AlmostEquals(object[] lastData, object[] currentContent)
  {
    if (lastData == null && currentContent == null)
      return true;
    if (lastData == null || currentContent == null || lastData.Length != currentContent.Length)
      return false;
    for (int index = 0; index < currentContent.Length; ++index)
    {
      if (!this.AlmostEquals(currentContent[index], lastData[index]))
        return false;
    }
    return true;
  }

  private bool AlmostEquals(object one, object two)
  {
    if (one == null || two == null)
      return one == null && two == null;
    if (one.Equals(two))
      return true;
    switch (one)
    {
      case Vector3 _:
        if (((Vector3) one).AlmostEquals((Vector3) two, PhotonNetwork.precisionForVectorSynchronization))
          return true;
        break;
      case Vector2 _:
        if (((Vector2) one).AlmostEquals((Vector2) two, PhotonNetwork.precisionForVectorSynchronization))
          return true;
        break;
      case Quaternion _:
        if (((Quaternion) one).AlmostEquals((Quaternion) two, PhotonNetwork.precisionForQuaternionSynchronization))
          return true;
        break;
      case float target:
        if (target.AlmostEquals((float) two, PhotonNetwork.precisionForFloatSynchronization))
          return true;
        break;
    }
    return false;
  }

  protected internal static bool GetMethod(
    MonoBehaviour monob,
    string methodType,
    out MethodInfo mi)
  {
    mi = (MethodInfo) null;
    if (Object.op_Equality((Object) monob, (Object) null) || string.IsNullOrEmpty(methodType))
      return false;
    List<MethodInfo> methods = SupportClass.GetMethods(monob.GetType(), (System.Type) null);
    for (int index = 0; index < methods.Count; ++index)
    {
      MethodInfo methodInfo = methods[index];
      if (methodInfo.Name.Equals(methodType))
      {
        mi = methodInfo;
        return true;
      }
    }
    return false;
  }

  protected internal void LoadLevelIfSynced()
  {
    if (!PhotonNetwork.automaticallySyncScene || PhotonNetwork.isMasterClient || PhotonNetwork.room == null)
      return;
    if (this._AsyncLevelLoadingOperation != null)
    {
      if (!this._AsyncLevelLoadingOperation.isDone)
        return;
      this._AsyncLevelLoadingOperation = (AsyncOperation) null;
    }
    if (!((Dictionary<object, object>) PhotonNetwork.room.CustomProperties).ContainsKey((object) "curScn"))
      return;
    bool flag = ((Dictionary<object, object>) PhotonNetwork.room.CustomProperties).ContainsKey((object) "curScnLa");
    object customProperty = PhotonNetwork.room.CustomProperties[(object) "curScn"];
    switch (customProperty)
    {
      case int num:
        if (SceneManagerHelper.ActiveSceneBuildIndex == num)
          break;
        if (flag)
        {
          this._AsyncLevelLoadingOperation = PhotonNetwork.LoadLevelAsync((int) customProperty);
          break;
        }
        PhotonNetwork.LoadLevel((int) customProperty);
        break;
      case string _:
        if (!(SceneManagerHelper.ActiveSceneName != (string) customProperty))
          break;
        if (flag)
        {
          this._AsyncLevelLoadingOperation = PhotonNetwork.LoadLevelAsync((string) customProperty);
          break;
        }
        PhotonNetwork.LoadLevel((string) customProperty);
        break;
    }
  }

  protected internal void SetLevelInPropsIfSynced(
    object levelId,
    bool initiatingCall,
    bool asyncLoading = false)
  {
    if (!PhotonNetwork.automaticallySyncScene || !PhotonNetwork.isMasterClient || PhotonNetwork.room == null)
      return;
    if (levelId == null)
    {
      Debug.LogError((object) "Parameter levelId can't be null!");
    }
    else
    {
      if (!asyncLoading && ((Dictionary<object, object>) PhotonNetwork.room.CustomProperties).ContainsKey((object) "curScn"))
      {
        object obj;
        switch (PhotonNetwork.room.CustomProperties[(object) "curScn"])
        {
          case int num2 when SceneManagerHelper.ActiveSceneBuildIndex == num2:
            this.SendLevelReloadEvent();
            return;
          case string _ when SceneManagerHelper.ActiveSceneName != null && SceneManagerHelper.ActiveSceneName.Equals((string) obj):
            bool flag = false;
            if (!this.IsReloadingLevel)
            {
              if (levelId is int num1)
                flag = num1 == SceneManagerHelper.ActiveSceneBuildIndex;
              else if (levelId is string)
                flag = SceneManagerHelper.ActiveSceneName.Equals((string) levelId);
            }
            if (initiatingCall && this.IsReloadingLevel)
              flag = false;
            if (!flag)
              return;
            this.SendLevelReloadEvent();
            return;
        }
      }
      Hashtable propertiesToSet = new Hashtable();
      switch (levelId)
      {
        case int num:
          propertiesToSet[(object) "curScn"] = (object) num;
          break;
        case string _:
          propertiesToSet[(object) "curScn"] = (object) (string) levelId;
          break;
        default:
          Debug.LogError((object) "Parameter levelId must be int or string!");
          break;
      }
      if (asyncLoading)
        propertiesToSet[(object) "curScnLa"] = (object) true;
      PhotonNetwork.room.SetCustomProperties(propertiesToSet);
      this.SendOutgoingCommands();
    }
  }

  private void SendLevelReloadEvent()
  {
    this.IsReloadingLevel = true;
    if (!PhotonNetwork.inRoom)
      return;
    this.OpRaiseEvent((byte) 212, (object) this.AsynchLevelLoadCall, true, this._levelReloadEventOptions);
  }

  public void SetApp(string appId, string gameVersion)
  {
    this.AppId = appId.Trim();
    if (string.IsNullOrEmpty(gameVersion))
      return;
    PhotonNetwork.gameVersion = gameVersion.Trim();
  }

  public bool WebRpc(string uriPath, object parameters)
  {
    return this.SendOperation((byte) 219, new Dictionary<byte, object>()
    {
      {
        (byte) 209,
        (object) uriPath
      },
      {
        (byte) 208,
        parameters
      }
    }, SendOptions.SendReliable);
  }
}
