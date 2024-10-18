// Decompiled with JetBrains decompiler
// Type: PhotonNetwork
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using ExitGames.Client.Photon;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

#nullable disable
public static class PhotonNetwork
{
  public const string versionPUN = "1.94";
  internal static readonly PhotonHandler photonMono;
  internal static NetworkingPeer networkingPeer;
  public static readonly int MAX_VIEW_IDS = 1000;
  internal const string serverSettingsAssetFile = "PhotonServerSettings";
  public static ServerSettings PhotonServerSettings = (ServerSettings) Resources.Load(nameof (PhotonServerSettings), typeof (ServerSettings));
  public static bool InstantiateInRoomOnly = true;
  public static PhotonLogLevel logLevel = PhotonLogLevel.ErrorsOnly;
  public static float precisionForVectorSynchronization = 9.9E-05f;
  public static float precisionForQuaternionSynchronization = 1f;
  public static float precisionForFloatSynchronization = 0.01f;
  public static bool UseRpcMonoBehaviourCache;
  public static bool UsePrefabCache = true;
  public static Dictionary<string, GameObject> PrefabCache = new Dictionary<string, GameObject>();
  public static HashSet<GameObject> SendMonoMessageTargets;
  public static System.Type SendMonoMessageTargetType = typeof (MonoBehaviour);
  public static bool StartRpcsAsCoroutine = true;
  private static bool isOfflineMode = false;
  private static Room offlineModeRoom = (Room) null;
  [Obsolete("Used for compatibility with Unity networking only.")]
  public static int maxConnections;
  private static bool _mAutomaticallySyncScene = false;
  private static bool m_autoCleanUpPlayerObjects = true;
  private static int sendInterval = 50;
  private static int sendIntervalOnSerialize = 100;
  private static bool m_isMessageQueueRunning = true;
  private static Stopwatch startupStopwatch;
  public static float BackgroundTimeout = 60f;
  internal static int lastUsedViewSubId = 0;
  internal static int lastUsedViewSubIdStatic = 0;
  internal static List<int> manuallyAllocatedViewIds = new List<int>();

  static PhotonNetwork()
  {
    if (Object.op_Inequality((Object) PhotonNetwork.PhotonServerSettings, (Object) null))
      Application.runInBackground = PhotonNetwork.PhotonServerSettings.RunInBackground;
    GameObject gameObject = new GameObject();
    PhotonNetwork.photonMono = gameObject.AddComponent<PhotonHandler>();
    ((Object) gameObject).name = "PhotonMono";
    ((Object) gameObject).hideFlags = (HideFlags) 1;
    ConnectionProtocol protocol = PhotonNetwork.PhotonServerSettings.Protocol;
    PhotonNetwork.networkingPeer = new NetworkingPeer(string.Empty, protocol);
    PhotonNetwork.networkingPeer.QuickResendAttempts = (byte) 2;
    PhotonNetwork.networkingPeer.SentCountAllowance = 7;
    PhotonNetwork.startupStopwatch = new Stopwatch();
    PhotonNetwork.startupStopwatch.Start();
    // ISSUE: method pointer
    PhotonNetwork.networkingPeer.LocalMsTimestampDelegate = new SupportClass.IntegerMillisecondsDelegate((object) null, __methodptr(\u003CPhotonNetwork\u003Em__0));
    CustomTypes.Register();
  }

  public static string gameVersion { get; set; }

  public static string ServerAddress
  {
    get
    {
      return PhotonNetwork.networkingPeer != null ? PhotonNetwork.networkingPeer.ServerAddress : "<not connected>";
    }
  }

  public static CloudRegionCode CloudRegion
  {
    get
    {
      return PhotonNetwork.networkingPeer != null && PhotonNetwork.connected && PhotonNetwork.Server != ServerConnection.NameServer ? PhotonNetwork.networkingPeer.CloudRegion : CloudRegionCode.none;
    }
  }

  public static bool connected
  {
    get
    {
      if (PhotonNetwork.offlineMode)
        return true;
      return PhotonNetwork.networkingPeer != null && !PhotonNetwork.networkingPeer.IsInitialConnect && PhotonNetwork.networkingPeer.State != ClientState.PeerCreated && PhotonNetwork.networkingPeer.State != ClientState.Disconnected && PhotonNetwork.networkingPeer.State != ClientState.Disconnecting && PhotonNetwork.networkingPeer.State != ClientState.ConnectingToNameServer;
    }
  }

  public static bool connecting
  {
    get => PhotonNetwork.networkingPeer.IsInitialConnect && !PhotonNetwork.offlineMode;
  }

  public static bool connectedAndReady
  {
    get
    {
      if (!PhotonNetwork.connected)
        return false;
      if (PhotonNetwork.offlineMode)
        return true;
      ClientState connectionStateDetailed = PhotonNetwork.connectionStateDetailed;
      switch (connectionStateDetailed)
      {
        case ClientState.ConnectingToMasterserver:
        case ClientState.Disconnecting:
        case ClientState.Disconnected:
        case ClientState.ConnectingToNameServer:
        case ClientState.Authenticating:
label_7:
          return false;
        default:
          switch (connectionStateDetailed - 6)
          {
            case ClientState.Uninitialized:
            case ClientState.Queued:
              goto label_7;
            default:
              if (connectionStateDetailed != ClientState.PeerCreated)
                return true;
              goto label_7;
          }
      }
    }
  }

  public static ConnectionState connectionState
  {
    get
    {
      if (PhotonNetwork.offlineMode)
        return ConnectionState.Connected;
      if (PhotonNetwork.networkingPeer == null)
        return ConnectionState.Disconnected;
      PeerStateValue peerState = PhotonNetwork.networkingPeer.PeerState;
      switch ((int) peerState)
      {
        case 0:
          return ConnectionState.Disconnected;
        case 1:
          return ConnectionState.Connecting;
        case 3:
          return ConnectionState.Connected;
        case 4:
          return ConnectionState.Disconnecting;
        default:
          return peerState == 10 ? ConnectionState.InitializingApplication : ConnectionState.Disconnected;
      }
    }
  }

  public static ClientState connectionStateDetailed
  {
    get
    {
      return PhotonNetwork.offlineMode ? (PhotonNetwork.offlineModeRoom != null ? ClientState.Joined : ClientState.ConnectedToMaster) : (PhotonNetwork.networkingPeer == null ? ClientState.Disconnected : PhotonNetwork.networkingPeer.State);
    }
  }

  public static ServerConnection Server
  {
    get
    {
      return PhotonNetwork.networkingPeer != null ? PhotonNetwork.networkingPeer.Server : ServerConnection.NameServer;
    }
  }

  public static AuthenticationValues AuthValues
  {
    get
    {
      return PhotonNetwork.networkingPeer != null ? PhotonNetwork.networkingPeer.AuthValues : (AuthenticationValues) null;
    }
    set
    {
      if (PhotonNetwork.networkingPeer == null)
        return;
      PhotonNetwork.networkingPeer.AuthValues = value;
    }
  }

  public static Room room
  {
    get
    {
      return PhotonNetwork.isOfflineMode ? PhotonNetwork.offlineModeRoom : PhotonNetwork.networkingPeer.CurrentRoom;
    }
  }

  public static PhotonPlayer player
  {
    get
    {
      return PhotonNetwork.networkingPeer == null ? (PhotonPlayer) null : PhotonNetwork.networkingPeer.LocalPlayer;
    }
  }

  public static PhotonPlayer masterClient
  {
    get
    {
      if (PhotonNetwork.offlineMode)
        return PhotonNetwork.player;
      return PhotonNetwork.networkingPeer == null ? (PhotonPlayer) null : PhotonNetwork.networkingPeer.GetPlayerWithId(PhotonNetwork.networkingPeer.mMasterClientId);
    }
  }

  public static string playerName
  {
    get => PhotonNetwork.networkingPeer.PlayerName;
    set => PhotonNetwork.networkingPeer.PlayerName = value;
  }

  public static PhotonPlayer[] playerList
  {
    get
    {
      return PhotonNetwork.networkingPeer == null ? new PhotonPlayer[0] : PhotonNetwork.networkingPeer.mPlayerListCopy;
    }
  }

  public static PhotonPlayer[] otherPlayers
  {
    get
    {
      return PhotonNetwork.networkingPeer == null ? new PhotonPlayer[0] : PhotonNetwork.networkingPeer.mOtherPlayerListCopy;
    }
  }

  public static List<FriendInfo> Friends { get; internal set; }

  public static int FriendsListAge
  {
    get => PhotonNetwork.networkingPeer != null ? PhotonNetwork.networkingPeer.FriendListAge : 0;
  }

  public static IPunPrefabPool PrefabPool
  {
    get => PhotonNetwork.networkingPeer.ObjectPool;
    set => PhotonNetwork.networkingPeer.ObjectPool = value;
  }

  public static bool offlineMode
  {
    get => PhotonNetwork.isOfflineMode;
    set
    {
      if (value == PhotonNetwork.isOfflineMode)
        return;
      if (value && PhotonNetwork.connected)
      {
        Debug.LogError((object) "Can't start OFFLINE mode while connected!");
      }
      else
      {
        if (PhotonNetwork.networkingPeer.PeerState != null)
          ((PhotonPeer) PhotonNetwork.networkingPeer).Disconnect();
        PhotonNetwork.isOfflineMode = value;
        if (PhotonNetwork.isOfflineMode)
        {
          PhotonNetwork.networkingPeer.ChangeLocalID(-1);
          NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnConnectedToMaster);
        }
        else
        {
          PhotonNetwork.offlineModeRoom = (Room) null;
          PhotonNetwork.networkingPeer.ChangeLocalID(-1);
        }
      }
    }
  }

  public static bool automaticallySyncScene
  {
    get => PhotonNetwork._mAutomaticallySyncScene;
    set
    {
      PhotonNetwork._mAutomaticallySyncScene = value;
      if (!PhotonNetwork._mAutomaticallySyncScene || PhotonNetwork.room == null)
        return;
      PhotonNetwork.networkingPeer.LoadLevelIfSynced();
    }
  }

  public static bool autoCleanUpPlayerObjects
  {
    get => PhotonNetwork.m_autoCleanUpPlayerObjects;
    set
    {
      if (PhotonNetwork.room != null)
        Debug.LogError((object) "Setting autoCleanUpPlayerObjects while in a room is not supported.");
      else
        PhotonNetwork.m_autoCleanUpPlayerObjects = value;
    }
  }

  public static bool autoJoinLobby
  {
    get => PhotonNetwork.PhotonServerSettings.JoinLobby;
    set => PhotonNetwork.PhotonServerSettings.JoinLobby = value;
  }

  public static bool EnableLobbyStatistics
  {
    get => PhotonNetwork.PhotonServerSettings.EnableLobbyStatistics;
    set => PhotonNetwork.PhotonServerSettings.EnableLobbyStatistics = value;
  }

  public static List<TypedLobbyInfo> LobbyStatistics
  {
    get => PhotonNetwork.networkingPeer.LobbyStatistics;
    private set => PhotonNetwork.networkingPeer.LobbyStatistics = value;
  }

  public static bool insideLobby => PhotonNetwork.networkingPeer.insideLobby;

  public static TypedLobby lobby
  {
    get => PhotonNetwork.networkingPeer.lobby;
    set => PhotonNetwork.networkingPeer.lobby = value;
  }

  public static int sendRate
  {
    get => 1000 / PhotonNetwork.sendInterval;
    set
    {
      PhotonNetwork.sendInterval = 1000 / value;
      if (Object.op_Inequality((Object) PhotonNetwork.photonMono, (Object) null))
        PhotonNetwork.photonMono.updateInterval = PhotonNetwork.sendInterval;
      if (value >= PhotonNetwork.sendRateOnSerialize)
        return;
      PhotonNetwork.sendRateOnSerialize = value;
    }
  }

  public static int sendRateOnSerialize
  {
    get => 1000 / PhotonNetwork.sendIntervalOnSerialize;
    set
    {
      if (value > PhotonNetwork.sendRate)
      {
        Debug.LogError((object) "Error: Can not set the OnSerialize rate higher than the overall SendRate.");
        value = PhotonNetwork.sendRate;
      }
      PhotonNetwork.sendIntervalOnSerialize = 1000 / value;
      if (!Object.op_Inequality((Object) PhotonNetwork.photonMono, (Object) null))
        return;
      PhotonNetwork.photonMono.updateIntervalOnSerialize = PhotonNetwork.sendIntervalOnSerialize;
    }
  }

  public static bool isMessageQueueRunning
  {
    get => PhotonNetwork.m_isMessageQueueRunning;
    set
    {
      if (value)
        PhotonHandler.StartFallbackSendAckThread();
      PhotonNetwork.networkingPeer.IsSendingOnlyAcks = !value;
      PhotonNetwork.m_isMessageQueueRunning = value;
    }
  }

  public static int unreliableCommandsLimit
  {
    get => PhotonNetwork.networkingPeer.LimitOfUnreliableCommands;
    set => PhotonNetwork.networkingPeer.LimitOfUnreliableCommands = value;
  }

  public static double time => (double) (uint) PhotonNetwork.ServerTimestamp / 1000.0;

  public static int ServerTimestamp
  {
    get
    {
      return PhotonNetwork.offlineMode ? (int) PhotonNetwork.startupStopwatch.ElapsedMilliseconds : PhotonNetwork.networkingPeer.ServerTimeInMilliSeconds;
    }
  }

  public static bool isMasterClient
  {
    get
    {
      return PhotonNetwork.offlineMode || PhotonNetwork.networkingPeer.mMasterClientId == PhotonNetwork.player.ID;
    }
  }

  public static bool inRoom => PhotonNetwork.connectionStateDetailed == ClientState.Joined;

  public static bool isNonMasterClientInRoom
  {
    get => !PhotonNetwork.isMasterClient && PhotonNetwork.room != null;
  }

  public static int countOfPlayersOnMaster => PhotonNetwork.networkingPeer.PlayersOnMasterCount;

  public static int countOfPlayersInRooms => PhotonNetwork.networkingPeer.PlayersInRoomsCount;

  public static int countOfPlayers
  {
    get
    {
      return PhotonNetwork.networkingPeer.PlayersInRoomsCount + PhotonNetwork.networkingPeer.PlayersOnMasterCount;
    }
  }

  public static int countOfRooms => PhotonNetwork.networkingPeer.RoomsCount;

  public static bool NetworkStatisticsEnabled
  {
    get => PhotonNetwork.networkingPeer.TrafficStatsEnabled;
    set => PhotonNetwork.networkingPeer.TrafficStatsEnabled = value;
  }

  public static int ResentReliableCommands => PhotonNetwork.networkingPeer.ResentReliableCommands;

  public static bool CrcCheckEnabled
  {
    get => PhotonNetwork.networkingPeer.CrcEnabled;
    set
    {
      if (!PhotonNetwork.connected && !PhotonNetwork.connecting)
        PhotonNetwork.networkingPeer.CrcEnabled = value;
      else
        Debug.Log((object) ("Can't change CrcCheckEnabled while being connected. CrcCheckEnabled stays " + (object) PhotonNetwork.networkingPeer.CrcEnabled));
    }
  }

  public static int PacketLossByCrcCheck => PhotonNetwork.networkingPeer.PacketLossByCrc;

  public static int MaxResendsBeforeDisconnect
  {
    get => PhotonNetwork.networkingPeer.SentCountAllowance;
    set
    {
      if (value < 3)
        value = 3;
      if (value > 10)
        value = 10;
      PhotonNetwork.networkingPeer.SentCountAllowance = value;
    }
  }

  public static int QuickResends
  {
    get => (int) PhotonNetwork.networkingPeer.QuickResendAttempts;
    set
    {
      if (value < 0)
        value = 0;
      if (value > 3)
        value = 3;
      PhotonNetwork.networkingPeer.QuickResendAttempts = (byte) value;
    }
  }

  public static bool UseAlternativeUdpPorts { get; set; }

  public static event PhotonNetwork.EventCallback OnEventCall;

  public static void SwitchToProtocol(ConnectionProtocol cp)
  {
    PhotonNetwork.networkingPeer.TransportProtocol = cp;
  }

  public static bool ConnectUsingSettings(string gameVersion)
  {
    if (PhotonNetwork.networkingPeer.PeerState != null)
    {
      Debug.LogWarning((object) ("ConnectUsingSettings() failed. Can only connect while in state 'Disconnected'. Current state: " + (object) PhotonNetwork.networkingPeer.PeerState));
      return false;
    }
    if (Object.op_Equality((Object) PhotonNetwork.PhotonServerSettings, (Object) null))
    {
      Debug.LogError((object) "Can't connect: Loading settings failed. ServerSettings asset must be in any 'Resources' folder as: PhotonServerSettings");
      return false;
    }
    if (PhotonNetwork.PhotonServerSettings.HostType == ServerSettings.HostingOption.NotSet)
    {
      Debug.LogError((object) "You did not select a Hosting Type in your PhotonServerSettings. Please set it up or don't use ConnectUsingSettings().");
      return false;
    }
    if (PhotonNetwork.logLevel == PhotonLogLevel.ErrorsOnly)
      PhotonNetwork.logLevel = PhotonNetwork.PhotonServerSettings.PunLogging;
    if (PhotonNetwork.networkingPeer.DebugOut == 1)
      PhotonNetwork.networkingPeer.DebugOut = PhotonNetwork.PhotonServerSettings.NetworkLogging;
    PhotonNetwork.SwitchToProtocol(PhotonNetwork.PhotonServerSettings.Protocol);
    PhotonNetwork.networkingPeer.SetApp(PhotonNetwork.PhotonServerSettings.AppID, gameVersion);
    if (PhotonNetwork.PhotonServerSettings.HostType == ServerSettings.HostingOption.OfflineMode)
    {
      PhotonNetwork.offlineMode = true;
      return true;
    }
    if (PhotonNetwork.offlineMode)
      Debug.LogWarning((object) "ConnectUsingSettings() disabled the offline mode. No longer offline.");
    PhotonNetwork.offlineMode = false;
    PhotonNetwork.isMessageQueueRunning = true;
    PhotonNetwork.networkingPeer.IsInitialConnect = true;
    if (PhotonNetwork.PhotonServerSettings.HostType == ServerSettings.HostingOption.SelfHosted)
    {
      PhotonNetwork.networkingPeer.IsUsingNameServer = false;
      PhotonNetwork.networkingPeer.MasterServerAddress = PhotonNetwork.PhotonServerSettings.ServerPort != 0 ? PhotonNetwork.PhotonServerSettings.ServerAddress + ":" + (object) PhotonNetwork.PhotonServerSettings.ServerPort : PhotonNetwork.PhotonServerSettings.ServerAddress;
      return PhotonNetwork.networkingPeer.Connect(PhotonNetwork.networkingPeer.MasterServerAddress, ServerConnection.MasterServer);
    }
    return PhotonNetwork.PhotonServerSettings.HostType == ServerSettings.HostingOption.BestRegion ? PhotonNetwork.ConnectToBestCloudServer(gameVersion) : PhotonNetwork.networkingPeer.ConnectToRegionMaster(PhotonNetwork.PhotonServerSettings.PreferredRegion);
  }

  public static bool ConnectToMaster(
    string masterServerAddress,
    int port,
    string appID,
    string gameVersion)
  {
    if (PhotonNetwork.networkingPeer.PeerState != null)
    {
      Debug.LogWarning((object) ("ConnectToMaster() failed. Can only connect while in state 'Disconnected'. Current state: " + (object) PhotonNetwork.networkingPeer.PeerState));
      return false;
    }
    if (PhotonNetwork.offlineMode)
    {
      PhotonNetwork.offlineMode = false;
      Debug.LogWarning((object) "ConnectToMaster() disabled the offline mode. No longer offline.");
    }
    if (!PhotonNetwork.isMessageQueueRunning)
    {
      PhotonNetwork.isMessageQueueRunning = true;
      Debug.LogWarning((object) "ConnectToMaster() enabled isMessageQueueRunning. Needs to be able to dispatch incoming messages.");
    }
    PhotonNetwork.networkingPeer.SetApp(appID, gameVersion);
    PhotonNetwork.networkingPeer.IsUsingNameServer = false;
    PhotonNetwork.networkingPeer.IsInitialConnect = true;
    PhotonNetwork.networkingPeer.MasterServerAddress = port != 0 ? masterServerAddress + ":" + (object) port : masterServerAddress;
    return PhotonNetwork.networkingPeer.Connect(PhotonNetwork.networkingPeer.MasterServerAddress, ServerConnection.MasterServer);
  }

  public static bool Reconnect()
  {
    if (string.IsNullOrEmpty(PhotonNetwork.networkingPeer.MasterServerAddress))
    {
      Debug.LogWarning((object) ("Reconnect() failed. It seems the client wasn't connected before?! Current state: " + (object) PhotonNetwork.networkingPeer.PeerState));
      return false;
    }
    if (PhotonNetwork.networkingPeer.PeerState != null)
    {
      Debug.LogWarning((object) ("Reconnect() failed. Can only connect while in state 'Disconnected'. Current state: " + (object) PhotonNetwork.networkingPeer.PeerState));
      return false;
    }
    if (PhotonNetwork.offlineMode)
    {
      PhotonNetwork.offlineMode = false;
      Debug.LogWarning((object) "Reconnect() disabled the offline mode. No longer offline.");
    }
    if (!PhotonNetwork.isMessageQueueRunning)
    {
      PhotonNetwork.isMessageQueueRunning = true;
      Debug.LogWarning((object) "Reconnect() enabled isMessageQueueRunning. Needs to be able to dispatch incoming messages.");
    }
    PhotonNetwork.networkingPeer.IsUsingNameServer = false;
    PhotonNetwork.networkingPeer.IsInitialConnect = false;
    return PhotonNetwork.networkingPeer.ReconnectToMaster();
  }

  public static bool ReconnectAndRejoin()
  {
    if (PhotonNetwork.networkingPeer.PeerState != null)
    {
      Debug.LogWarning((object) ("ReconnectAndRejoin() failed. Can only connect while in state 'Disconnected'. Current state: " + (object) PhotonNetwork.networkingPeer.PeerState));
      return false;
    }
    if (PhotonNetwork.offlineMode)
    {
      PhotonNetwork.offlineMode = false;
      Debug.LogWarning((object) "ReconnectAndRejoin() disabled the offline mode. No longer offline.");
    }
    if (string.IsNullOrEmpty(PhotonNetwork.networkingPeer.GameServerAddress))
    {
      Debug.LogWarning((object) "ReconnectAndRejoin() failed. It seems the client wasn't connected to a game server before (no address).");
      return false;
    }
    if (PhotonNetwork.networkingPeer.enterRoomParamsCache == null)
    {
      Debug.LogWarning((object) "ReconnectAndRejoin() failed. It seems the client doesn't have any previous room to re-join.");
      return false;
    }
    if (!PhotonNetwork.isMessageQueueRunning)
    {
      PhotonNetwork.isMessageQueueRunning = true;
      Debug.LogWarning((object) "ReconnectAndRejoin() enabled isMessageQueueRunning. Needs to be able to dispatch incoming messages.");
    }
    PhotonNetwork.networkingPeer.IsUsingNameServer = false;
    PhotonNetwork.networkingPeer.IsInitialConnect = false;
    return PhotonNetwork.networkingPeer.ReconnectAndRejoin();
  }

  public static bool ConnectToBestCloudServer(string gameVersion)
  {
    if (PhotonNetwork.networkingPeer.PeerState != null)
    {
      Debug.LogWarning((object) ("ConnectToBestCloudServer() failed. Can only connect while in state 'Disconnected'. Current state: " + (object) PhotonNetwork.networkingPeer.PeerState));
      return false;
    }
    if (Object.op_Equality((Object) PhotonNetwork.PhotonServerSettings, (Object) null))
    {
      Debug.LogError((object) "Can't connect: Loading settings failed. ServerSettings asset must be in any 'Resources' folder as: PhotonServerSettings");
      return false;
    }
    if (PhotonNetwork.PhotonServerSettings.HostType == ServerSettings.HostingOption.OfflineMode)
      return PhotonNetwork.ConnectUsingSettings(gameVersion);
    PhotonNetwork.networkingPeer.IsInitialConnect = true;
    PhotonNetwork.networkingPeer.SetApp(PhotonNetwork.PhotonServerSettings.AppID, gameVersion);
    return PhotonNetwork.networkingPeer.ConnectToNameServer();
  }

  public static bool ConnectToRegion(CloudRegionCode region, string gameVersion)
  {
    if (PhotonNetwork.networkingPeer.PeerState != null)
    {
      Debug.LogWarning((object) ("ConnectToRegion() failed. Can only connect while in state 'Disconnected'. Current state: " + (object) PhotonNetwork.networkingPeer.PeerState));
      return false;
    }
    if (Object.op_Equality((Object) PhotonNetwork.PhotonServerSettings, (Object) null))
    {
      Debug.LogError((object) "Can't connect: ServerSettings asset must be in any 'Resources' folder as: PhotonServerSettings");
      return false;
    }
    if (PhotonNetwork.PhotonServerSettings.HostType == ServerSettings.HostingOption.OfflineMode)
      return PhotonNetwork.ConnectUsingSettings(gameVersion);
    PhotonNetwork.networkingPeer.IsInitialConnect = true;
    PhotonNetwork.networkingPeer.SetApp(PhotonNetwork.PhotonServerSettings.AppID, gameVersion);
    if (region == CloudRegionCode.none)
      return false;
    Debug.Log((object) ("ConnectToRegion: " + (object) region));
    return PhotonNetwork.networkingPeer.ConnectToRegionMaster(region);
  }

  public static void OverrideBestCloudServer(CloudRegionCode region)
  {
    PhotonHandler.BestRegionCodeInPreferences = region;
  }

  public static void RefreshCloudServerRating()
  {
    throw new NotImplementedException("not available at the moment");
  }

  public static void NetworkStatisticsReset() => PhotonNetwork.networkingPeer.TrafficStatsReset();

  public static string NetworkStatisticsToString()
  {
    return PhotonNetwork.networkingPeer == null || PhotonNetwork.offlineMode ? "Offline or in OfflineMode. No VitalStats available." : PhotonNetwork.networkingPeer.VitalStatsToString(false);
  }

  [Obsolete("Used for compatibility with Unity networking only. Encryption is automatically initialized while connecting.")]
  public static void InitializeSecurity()
  {
  }

  private static bool VerifyCanUseNetwork()
  {
    if (PhotonNetwork.connected)
      return true;
    Debug.LogError((object) "Cannot send messages when not connected. Either connect to Photon OR use offline mode!");
    return false;
  }

  public static void Disconnect()
  {
    if (PhotonNetwork.offlineMode)
    {
      PhotonNetwork.offlineMode = false;
      PhotonNetwork.offlineModeRoom = (Room) null;
      PhotonNetwork.networkingPeer.State = ClientState.Disconnecting;
      PhotonNetwork.networkingPeer.OnStatusChanged((StatusCode) 1025);
    }
    else
    {
      if (PhotonNetwork.networkingPeer == null)
        return;
      ((PhotonPeer) PhotonNetwork.networkingPeer).Disconnect();
    }
  }

  public static bool FindFriends(string[] friendsToFind)
  {
    return PhotonNetwork.networkingPeer != null && !PhotonNetwork.isOfflineMode && PhotonNetwork.networkingPeer.OpFindFriends(friendsToFind);
  }

  public static bool CreateRoom(string roomName)
  {
    return PhotonNetwork.CreateRoom(roomName, (RoomOptions) null, (TypedLobby) null, (string[]) null);
  }

  public static bool CreateRoom(string roomName, RoomOptions roomOptions, TypedLobby typedLobby)
  {
    return PhotonNetwork.CreateRoom(roomName, roomOptions, typedLobby, (string[]) null);
  }

  public static bool CreateRoom(
    string roomName,
    RoomOptions roomOptions,
    TypedLobby typedLobby,
    string[] expectedUsers)
  {
    if (PhotonNetwork.offlineMode)
    {
      if (PhotonNetwork.offlineModeRoom != null)
      {
        Debug.LogError((object) "CreateRoom failed. In offline mode you still have to leave a room to enter another.");
        return false;
      }
      PhotonNetwork.EnterOfflineRoom(roomName, roomOptions, true);
      return true;
    }
    if (PhotonNetwork.networkingPeer.Server != ServerConnection.MasterServer || !PhotonNetwork.connectedAndReady)
    {
      Debug.LogError((object) "CreateRoom failed. Client is not on Master Server or not yet ready to call operations. Wait for callback: OnJoinedLobby or OnConnectedToMaster.");
      return false;
    }
    typedLobby = typedLobby ?? (!PhotonNetwork.networkingPeer.insideLobby ? (TypedLobby) null : PhotonNetwork.networkingPeer.lobby);
    return PhotonNetwork.networkingPeer.OpCreateGame(new EnterRoomParams()
    {
      RoomName = roomName,
      RoomOptions = roomOptions,
      Lobby = typedLobby,
      ExpectedUsers = expectedUsers
    });
  }

  public static bool JoinRoom(string roomName) => PhotonNetwork.JoinRoom(roomName, (string[]) null);

  public static bool JoinRoom(string roomName, string[] expectedUsers)
  {
    if (PhotonNetwork.offlineMode)
    {
      if (PhotonNetwork.offlineModeRoom != null)
      {
        Debug.LogError((object) "JoinRoom failed. In offline mode you still have to leave a room to enter another.");
        return false;
      }
      PhotonNetwork.EnterOfflineRoom(roomName, (RoomOptions) null, true);
      return true;
    }
    if (PhotonNetwork.networkingPeer.Server != ServerConnection.MasterServer || !PhotonNetwork.connectedAndReady)
    {
      Debug.LogError((object) "JoinRoom failed. Client is not on Master Server or not yet ready to call operations. Wait for callback: OnJoinedLobby or OnConnectedToMaster.");
      return false;
    }
    if (string.IsNullOrEmpty(roomName))
    {
      Debug.LogError((object) "JoinRoom failed. A roomname is required. If you don't know one, how will you join?");
      return false;
    }
    return PhotonNetwork.networkingPeer.OpJoinRoom(new EnterRoomParams()
    {
      RoomName = roomName,
      ExpectedUsers = expectedUsers
    });
  }

  public static bool JoinOrCreateRoom(
    string roomName,
    RoomOptions roomOptions,
    TypedLobby typedLobby)
  {
    return PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, typedLobby, (string[]) null);
  }

  public static bool JoinOrCreateRoom(
    string roomName,
    RoomOptions roomOptions,
    TypedLobby typedLobby,
    string[] expectedUsers)
  {
    if (PhotonNetwork.offlineMode)
    {
      if (PhotonNetwork.offlineModeRoom != null)
      {
        Debug.LogError((object) "JoinOrCreateRoom failed. In offline mode you still have to leave a room to enter another.");
        return false;
      }
      PhotonNetwork.EnterOfflineRoom(roomName, roomOptions, true);
      return true;
    }
    if (PhotonNetwork.networkingPeer.Server != ServerConnection.MasterServer || !PhotonNetwork.connectedAndReady)
    {
      Debug.LogError((object) "JoinOrCreateRoom failed. Client is not on Master Server or not yet ready to call operations. Wait for callback: OnJoinedLobby or OnConnectedToMaster.");
      return false;
    }
    if (string.IsNullOrEmpty(roomName))
    {
      Debug.LogError((object) "JoinOrCreateRoom failed. A roomname is required. If you don't know one, how will you join?");
      return false;
    }
    typedLobby = typedLobby ?? (!PhotonNetwork.networkingPeer.insideLobby ? (TypedLobby) null : PhotonNetwork.networkingPeer.lobby);
    return PhotonNetwork.networkingPeer.OpJoinRoom(new EnterRoomParams()
    {
      RoomName = roomName,
      RoomOptions = roomOptions,
      Lobby = typedLobby,
      CreateIfNotExists = true,
      PlayerProperties = PhotonNetwork.player.CustomProperties,
      ExpectedUsers = expectedUsers
    });
  }

  public static bool JoinRandomRoom()
  {
    return PhotonNetwork.JoinRandomRoom((Hashtable) null, (byte) 0, MatchmakingMode.FillRoom, (TypedLobby) null, (string) null);
  }

  public static bool JoinRandomRoom(Hashtable expectedCustomRoomProperties, byte expectedMaxPlayers)
  {
    return PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties, expectedMaxPlayers, MatchmakingMode.FillRoom, (TypedLobby) null, (string) null);
  }

  public static bool JoinRandomRoom(
    Hashtable expectedCustomRoomProperties,
    byte expectedMaxPlayers,
    MatchmakingMode matchingType,
    TypedLobby typedLobby,
    string sqlLobbyFilter,
    string[] expectedUsers = null)
  {
    if (PhotonNetwork.offlineMode)
    {
      if (PhotonNetwork.offlineModeRoom != null)
      {
        Debug.LogError((object) "JoinRandomRoom failed. In offline mode you still have to leave a room to enter another.");
        return false;
      }
      PhotonNetwork.EnterOfflineRoom("offline room", (RoomOptions) null, true);
      return true;
    }
    if (PhotonNetwork.networkingPeer.Server != ServerConnection.MasterServer || !PhotonNetwork.connectedAndReady)
    {
      Debug.LogError((object) "JoinRandomRoom failed. Client is not on Master Server or not yet ready to call operations. Wait for callback: OnJoinedLobby or OnConnectedToMaster.");
      return false;
    }
    typedLobby = typedLobby ?? (!PhotonNetwork.networkingPeer.insideLobby ? (TypedLobby) null : PhotonNetwork.networkingPeer.lobby);
    return PhotonNetwork.networkingPeer.OpJoinRandomRoom(new OpJoinRandomRoomParams()
    {
      ExpectedCustomRoomProperties = expectedCustomRoomProperties,
      ExpectedMaxPlayers = expectedMaxPlayers,
      MatchingType = matchingType,
      TypedLobby = typedLobby,
      SqlLobbyFilter = sqlLobbyFilter,
      ExpectedUsers = expectedUsers
    });
  }

  public static bool ReJoinRoom(string roomName)
  {
    if (PhotonNetwork.offlineMode)
    {
      Debug.LogError((object) "ReJoinRoom failed due to offline mode.");
      return false;
    }
    if (PhotonNetwork.networkingPeer.Server != ServerConnection.MasterServer || !PhotonNetwork.connectedAndReady)
    {
      Debug.LogError((object) "ReJoinRoom failed. Client is not on Master Server or not yet ready to call operations. Wait for callback: OnJoinedLobby or OnConnectedToMaster.");
      return false;
    }
    if (string.IsNullOrEmpty(roomName))
    {
      Debug.LogError((object) "ReJoinRoom failed. A roomname is required. If you don't know one, how will you join?");
      return false;
    }
    return PhotonNetwork.networkingPeer.OpJoinRoom(new EnterRoomParams()
    {
      RoomName = roomName,
      RejoinOnly = true,
      PlayerProperties = PhotonNetwork.player.CustomProperties
    });
  }

  private static void EnterOfflineRoom(string roomName, RoomOptions roomOptions, bool createdRoom)
  {
    PhotonNetwork.offlineModeRoom = new Room(roomName, roomOptions);
    PhotonNetwork.networkingPeer.ChangeLocalID(1);
    PhotonNetwork.networkingPeer.State = ClientState.ConnectingToGameserver;
    PhotonNetwork.networkingPeer.OnStatusChanged((StatusCode) 1024);
    PhotonNetwork.offlineModeRoom.MasterClientId = 1;
    if (createdRoom)
      NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnCreatedRoom);
    NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnJoinedRoom);
  }

  public static bool JoinLobby() => PhotonNetwork.JoinLobby((TypedLobby) null);

  public static bool JoinLobby(TypedLobby typedLobby)
  {
    if (!PhotonNetwork.connected || PhotonNetwork.Server != ServerConnection.MasterServer)
      return false;
    if (typedLobby == null)
      typedLobby = TypedLobby.Default;
    bool flag = PhotonNetwork.networkingPeer.OpJoinLobby(typedLobby);
    if (flag)
      PhotonNetwork.networkingPeer.lobby = typedLobby;
    return flag;
  }

  public static bool LeaveLobby()
  {
    return PhotonNetwork.connected && PhotonNetwork.Server == ServerConnection.MasterServer && PhotonNetwork.networkingPeer.OpLeaveLobby();
  }

  public static bool LeaveRoom(bool becomeInactive = true)
  {
    if (PhotonNetwork.offlineMode)
    {
      PhotonNetwork.offlineModeRoom = (Room) null;
      PhotonNetwork.networkingPeer.State = ClientState.PeerCreated;
      NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnLeftRoom);
      return true;
    }
    if (PhotonNetwork.room == null)
      Debug.LogWarning((object) ("PhotonNetwork.room is null. You don't have to call LeaveRoom() when you're not in one. State: " + (object) PhotonNetwork.connectionStateDetailed));
    else
      becomeInactive = becomeInactive && PhotonNetwork.room.PlayerTtl != 0;
    return PhotonNetwork.networkingPeer.OpLeaveRoom(becomeInactive);
  }

  public static bool GetCustomRoomList(TypedLobby typedLobby, string sqlLobbyFilter)
  {
    return PhotonNetwork.networkingPeer.OpGetGameList(typedLobby, sqlLobbyFilter);
  }

  public static RoomInfo[] GetRoomList()
  {
    return PhotonNetwork.offlineMode || PhotonNetwork.networkingPeer == null ? new RoomInfo[0] : PhotonNetwork.networkingPeer.mGameListCopy;
  }

  public static void SetPlayerCustomProperties(Hashtable customProperties)
  {
    if (customProperties == null)
    {
      customProperties = new Hashtable();
      foreach (object key in ((Dictionary<object, object>) PhotonNetwork.player.CustomProperties).Keys)
        customProperties[(object) (string) key] = (object) null;
    }
    if (PhotonNetwork.room != null && PhotonNetwork.room.IsLocalClientInside)
      PhotonNetwork.player.SetCustomProperties(customProperties);
    else
      PhotonNetwork.player.InternalCacheProperties(customProperties);
  }

  public static void RemovePlayerCustomProperties(string[] customPropertiesToDelete)
  {
    if (customPropertiesToDelete == null || customPropertiesToDelete.Length == 0 || PhotonNetwork.player.CustomProperties == null)
    {
      PhotonNetwork.player.CustomProperties = new Hashtable();
    }
    else
    {
      for (int index = 0; index < customPropertiesToDelete.Length; ++index)
      {
        string key = customPropertiesToDelete[index];
        if (((Dictionary<object, object>) PhotonNetwork.player.CustomProperties).ContainsKey((object) key))
          ((Dictionary<object, object>) PhotonNetwork.player.CustomProperties).Remove((object) key);
      }
    }
  }

  public static bool RaiseEvent(
    byte eventCode,
    object eventContent,
    bool sendReliable,
    RaiseEventOptions options)
  {
    if (PhotonNetwork.inRoom && eventCode < (byte) 200)
      return PhotonNetwork.networkingPeer.OpRaiseEvent(eventCode, eventContent, sendReliable, options);
    Debug.LogWarning((object) "RaiseEvent() failed. Your event is not being sent! Check if your are in a Room and the eventCode must be less than 200 (0..199).");
    return false;
  }

  public static int AllocateViewID()
  {
    int num = PhotonNetwork.AllocateViewID(PhotonNetwork.player.ID);
    PhotonNetwork.manuallyAllocatedViewIds.Add(num);
    return num;
  }

  public static int AllocateSceneViewID()
  {
    if (!PhotonNetwork.isMasterClient)
    {
      Debug.LogError((object) "Only the Master Client can AllocateSceneViewID(). Check PhotonNetwork.isMasterClient!");
      return -1;
    }
    int num = PhotonNetwork.AllocateViewID(0);
    PhotonNetwork.manuallyAllocatedViewIds.Add(num);
    return num;
  }

  private static int AllocateViewID(int ownerId)
  {
    if (ownerId == 0)
    {
      int num1 = PhotonNetwork.lastUsedViewSubIdStatic;
      int num2 = ownerId * PhotonNetwork.MAX_VIEW_IDS;
      for (int index = 1; index < PhotonNetwork.MAX_VIEW_IDS; ++index)
      {
        num1 = (num1 + 1) % PhotonNetwork.MAX_VIEW_IDS;
        if (num1 != 0)
        {
          int key = num1 + num2;
          if (!PhotonNetwork.networkingPeer.photonViewList.ContainsKey(key))
          {
            PhotonNetwork.lastUsedViewSubIdStatic = num1;
            return key;
          }
        }
      }
      throw new Exception(string.Format("AllocateViewID() failed. Room (user {0}) is out of 'scene' viewIDs. It seems all available are in use.", (object) ownerId));
    }
    int num3 = PhotonNetwork.lastUsedViewSubId;
    int num4 = ownerId * PhotonNetwork.MAX_VIEW_IDS;
    for (int index = 1; index < PhotonNetwork.MAX_VIEW_IDS; ++index)
    {
      num3 = (num3 + 1) % PhotonNetwork.MAX_VIEW_IDS;
      if (num3 != 0)
      {
        int key = num3 + num4;
        if (!PhotonNetwork.networkingPeer.photonViewList.ContainsKey(key) && !PhotonNetwork.manuallyAllocatedViewIds.Contains(key))
        {
          PhotonNetwork.lastUsedViewSubId = num3;
          return key;
        }
      }
    }
    throw new Exception(string.Format("AllocateViewID() failed. User {0} is out of subIds, as all viewIDs are used.", (object) ownerId));
  }

  private static int[] AllocateSceneViewIDs(int countOfNewViews)
  {
    int[] numArray = new int[countOfNewViews];
    for (int index = 0; index < countOfNewViews; ++index)
      numArray[index] = PhotonNetwork.AllocateViewID(0);
    return numArray;
  }

  public static void UnAllocateViewID(int viewID)
  {
    PhotonNetwork.manuallyAllocatedViewIds.Remove(viewID);
    if (!PhotonNetwork.networkingPeer.photonViewList.ContainsKey(viewID))
      return;
    Debug.LogWarning((object) string.Format("UnAllocateViewID() should be called after the PhotonView was destroyed (GameObject.Destroy()). ViewID: {0} still found in: {1}", (object) viewID, (object) PhotonNetwork.networkingPeer.photonViewList[viewID]));
  }

  public static GameObject Instantiate(
    string prefabName,
    Vector3 position,
    Quaternion rotation,
    byte group)
  {
    return PhotonNetwork.Instantiate(prefabName, position, rotation, group, (object[]) null);
  }

  public static GameObject Instantiate(
    string prefabName,
    Vector3 position,
    Quaternion rotation,
    byte group,
    object[] data)
  {
    if (!PhotonNetwork.connected || PhotonNetwork.InstantiateInRoomOnly && !PhotonNetwork.inRoom)
    {
      Debug.LogError((object) ("Failed to Instantiate prefab: " + prefabName + ". Client should be in a room. Current connectionStateDetailed: " + (object) PhotonNetwork.connectionStateDetailed));
      return (GameObject) null;
    }
    GameObject gameObject;
    if (!PhotonNetwork.UsePrefabCache || !PhotonNetwork.PrefabCache.TryGetValue(prefabName, out gameObject))
    {
      gameObject = (GameObject) Resources.Load(prefabName, typeof (GameObject));
      if (PhotonNetwork.UsePrefabCache)
        PhotonNetwork.PrefabCache.Add(prefabName, gameObject);
    }
    if (Object.op_Equality((Object) gameObject, (Object) null))
    {
      Debug.LogError((object) ("Failed to Instantiate prefab: " + prefabName + ". Verify the Prefab is in a Resources folder (and not in a subfolder)"));
      return (GameObject) null;
    }
    if (Object.op_Equality((Object) gameObject.GetComponent<PhotonView>(), (Object) null))
    {
      Debug.LogError((object) ("Failed to Instantiate prefab:" + prefabName + ". Prefab must have a PhotonView component."));
      return (GameObject) null;
    }
    int[] viewIDs = new int[((Component[]) gameObject.GetPhotonViewsInChildren()).Length];
    for (int index = 0; index < viewIDs.Length; ++index)
      viewIDs[index] = PhotonNetwork.AllocateViewID(PhotonNetwork.player.ID);
    Hashtable evData = PhotonNetwork.networkingPeer.SendInstantiate(prefabName, position, rotation, group, viewIDs, data, false);
    return PhotonNetwork.networkingPeer.DoInstantiate(evData, PhotonNetwork.networkingPeer.LocalPlayer, gameObject);
  }

  public static GameObject InstantiateSceneObject(
    string prefabName,
    Vector3 position,
    Quaternion rotation,
    byte group,
    object[] data)
  {
    if (!PhotonNetwork.connected || PhotonNetwork.InstantiateInRoomOnly && !PhotonNetwork.inRoom)
    {
      Debug.LogError((object) ("Failed to InstantiateSceneObject prefab: " + prefabName + ". Client should be in a room. Current connectionStateDetailed: " + (object) PhotonNetwork.connectionStateDetailed));
      return (GameObject) null;
    }
    if (!PhotonNetwork.isMasterClient)
    {
      Debug.LogError((object) ("Failed to InstantiateSceneObject prefab: " + prefabName + ". Client is not the MasterClient in this room."));
      return (GameObject) null;
    }
    GameObject gameObject;
    if (!PhotonNetwork.UsePrefabCache || !PhotonNetwork.PrefabCache.TryGetValue(prefabName, out gameObject))
    {
      gameObject = (GameObject) Resources.Load(prefabName, typeof (GameObject));
      if (PhotonNetwork.UsePrefabCache)
        PhotonNetwork.PrefabCache.Add(prefabName, gameObject);
    }
    if (Object.op_Equality((Object) gameObject, (Object) null))
    {
      Debug.LogError((object) ("Failed to InstantiateSceneObject prefab: " + prefabName + ". Verify the Prefab is in a Resources folder (and not in a subfolder)"));
      return (GameObject) null;
    }
    if (Object.op_Equality((Object) gameObject.GetComponent<PhotonView>(), (Object) null))
    {
      Debug.LogError((object) ("Failed to InstantiateSceneObject prefab:" + prefabName + ". Prefab must have a PhotonView component."));
      return (GameObject) null;
    }
    int[] viewIDs = PhotonNetwork.AllocateSceneViewIDs(((Component[]) gameObject.GetPhotonViewsInChildren()).Length);
    if (viewIDs == null)
    {
      Debug.LogError((object) ("Failed to InstantiateSceneObject prefab: " + prefabName + ". No ViewIDs are free to use. Max is: " + (object) PhotonNetwork.MAX_VIEW_IDS));
      return (GameObject) null;
    }
    Hashtable evData = PhotonNetwork.networkingPeer.SendInstantiate(prefabName, position, rotation, group, viewIDs, data, true);
    return PhotonNetwork.networkingPeer.DoInstantiate(evData, PhotonNetwork.networkingPeer.LocalPlayer, gameObject);
  }

  public static int GetPing() => PhotonNetwork.networkingPeer.RoundTripTime;

  public static void FetchServerTimestamp()
  {
    if (PhotonNetwork.networkingPeer == null)
      return;
    PhotonNetwork.networkingPeer.FetchServerTimestamp();
  }

  public static void SendOutgoingCommands()
  {
    if (!PhotonNetwork.VerifyCanUseNetwork())
      return;
    do
      ;
    while (PhotonNetwork.networkingPeer.SendOutgoingCommands());
  }

  public static bool CloseConnection(PhotonPlayer kickPlayer)
  {
    if (!PhotonNetwork.VerifyCanUseNetwork())
      return false;
    if (!PhotonNetwork.player.IsMasterClient)
    {
      Debug.LogError((object) "CloseConnection: Only the masterclient can kick another player.");
      return false;
    }
    if (kickPlayer == null)
    {
      Debug.LogError((object) "CloseConnection: No such player connected!");
      return false;
    }
    RaiseEventOptions raiseEventOptions = new RaiseEventOptions()
    {
      TargetActors = new int[1]{ kickPlayer.ID }
    };
    return PhotonNetwork.networkingPeer.OpRaiseEvent((byte) 203, (object) null, true, raiseEventOptions);
  }

  public static bool SetMasterClient(PhotonPlayer masterClientPlayer)
  {
    if (!PhotonNetwork.inRoom || !PhotonNetwork.VerifyCanUseNetwork() || PhotonNetwork.offlineMode)
    {
      if (PhotonNetwork.logLevel == PhotonLogLevel.Informational)
        Debug.Log((object) "Can not SetMasterClient(). Not in room or in offlineMode.");
      return false;
    }
    if (PhotonNetwork.room.serverSideMasterClient)
    {
      Hashtable hashtable1 = new Hashtable();
      ((Dictionary<object, object>) hashtable1).Add((object) (byte) 248, (object) masterClientPlayer.ID);
      Hashtable gameProperties = hashtable1;
      Hashtable hashtable2 = new Hashtable();
      ((Dictionary<object, object>) hashtable2).Add((object) (byte) 248, (object) PhotonNetwork.networkingPeer.mMasterClientId);
      Hashtable expectedProperties = hashtable2;
      return PhotonNetwork.networkingPeer.OpSetPropertiesOfRoom(gameProperties, expectedProperties);
    }
    return PhotonNetwork.isMasterClient && PhotonNetwork.networkingPeer.SetMasterClient(masterClientPlayer.ID, true);
  }

  public static void Destroy(PhotonView targetView)
  {
    if (Object.op_Inequality((Object) targetView, (Object) null))
      PhotonNetwork.networkingPeer.RemoveInstantiatedGO(((Component) targetView).gameObject, !PhotonNetwork.inRoom);
    else
      Debug.LogError((object) "Destroy(targetPhotonView) failed, cause targetPhotonView is null.");
  }

  public static void Destroy(GameObject targetGo)
  {
    PhotonNetwork.networkingPeer.RemoveInstantiatedGO(targetGo, !PhotonNetwork.inRoom);
  }

  public static void DestroyPlayerObjects(PhotonPlayer targetPlayer)
  {
    if (PhotonNetwork.player == null)
      Debug.LogError((object) "DestroyPlayerObjects() failed, cause parameter 'targetPlayer' was null.");
    PhotonNetwork.DestroyPlayerObjects(targetPlayer.ID);
  }

  public static void DestroyPlayerObjects(int targetPlayerId)
  {
    if (!PhotonNetwork.VerifyCanUseNetwork())
      return;
    if (PhotonNetwork.player.IsMasterClient || targetPlayerId == PhotonNetwork.player.ID)
      PhotonNetwork.networkingPeer.DestroyPlayerObjects(targetPlayerId, false);
    else
      Debug.LogError((object) ("DestroyPlayerObjects() failed, cause players can only destroy their own GameObjects. A Master Client can destroy anyone's. This is master: " + (object) PhotonNetwork.isMasterClient));
  }

  public static void DestroyAll()
  {
    if (PhotonNetwork.isMasterClient)
      PhotonNetwork.networkingPeer.DestroyAll(false);
    else
      Debug.LogError((object) "Couldn't call DestroyAll() as only the master client is allowed to call this.");
  }

  public static void RemoveRPCs(PhotonPlayer targetPlayer)
  {
    if (!PhotonNetwork.VerifyCanUseNetwork())
      return;
    if (!targetPlayer.IsLocal && !PhotonNetwork.isMasterClient)
      Debug.LogError((object) "Error; Only the MasterClient can call RemoveRPCs for other players.");
    else
      PhotonNetwork.networkingPeer.OpCleanRpcBuffer(targetPlayer.ID);
  }

  public static void RemoveRPCs(PhotonView targetPhotonView)
  {
    if (!PhotonNetwork.VerifyCanUseNetwork())
      return;
    PhotonNetwork.networkingPeer.CleanRpcBufferIfMine(targetPhotonView);
  }

  public static void RemoveRPCsInGroup(int targetGroup)
  {
    if (!PhotonNetwork.VerifyCanUseNetwork())
      return;
    PhotonNetwork.networkingPeer.RemoveRPCsInGroup(targetGroup);
  }

  internal static void RPC(
    PhotonView view,
    string methodName,
    PhotonTargets target,
    bool encrypt,
    params object[] parameters)
  {
    if (!PhotonNetwork.VerifyCanUseNetwork())
      return;
    if (PhotonNetwork.room == null)
      Debug.LogWarning((object) ("RPCs can only be sent in rooms. Call of \"" + methodName + "\" gets executed locally only, if at all."));
    else if (PhotonNetwork.networkingPeer != null)
    {
      if (PhotonNetwork.room.serverSideMasterClient)
        PhotonNetwork.networkingPeer.RPC(view, methodName, target, (PhotonPlayer) null, encrypt, parameters);
      else if (PhotonNetwork.networkingPeer.hasSwitchedMC && target == PhotonTargets.MasterClient)
        PhotonNetwork.networkingPeer.RPC(view, methodName, PhotonTargets.Others, PhotonNetwork.masterClient, encrypt, parameters);
      else
        PhotonNetwork.networkingPeer.RPC(view, methodName, target, (PhotonPlayer) null, encrypt, parameters);
    }
    else
      Debug.LogWarning((object) ("Could not execute RPC " + methodName + ". Possible scene loading in progress?"));
  }

  internal static void RPC(
    PhotonView view,
    string methodName,
    PhotonPlayer targetPlayer,
    bool encrpyt,
    params object[] parameters)
  {
    if (!PhotonNetwork.VerifyCanUseNetwork())
      return;
    if (PhotonNetwork.room == null)
    {
      Debug.LogWarning((object) ("RPCs can only be sent in rooms. Call of \"" + methodName + "\" gets executed locally only, if at all."));
    }
    else
    {
      if (PhotonNetwork.player == null)
        Debug.LogError((object) ("RPC can't be sent to target PhotonPlayer being null! Did not send \"" + methodName + "\" call."));
      if (PhotonNetwork.networkingPeer != null)
        PhotonNetwork.networkingPeer.RPC(view, methodName, PhotonTargets.Others, targetPlayer, encrpyt, parameters);
      else
        Debug.LogWarning((object) ("Could not execute RPC " + methodName + ". Possible scene loading in progress?"));
    }
  }

  public static void CacheSendMonoMessageTargets(System.Type type)
  {
    if ((object) type == null)
      type = PhotonNetwork.SendMonoMessageTargetType;
    PhotonNetwork.SendMonoMessageTargets = PhotonNetwork.FindGameObjectsWithComponent(type);
  }

  public static HashSet<GameObject> FindGameObjectsWithComponent(System.Type type)
  {
    HashSet<GameObject> objectsWithComponent = new HashSet<GameObject>();
    Component[] objectsOfType = (Component[]) Object.FindObjectsOfType(type);
    for (int index = 0; index < objectsOfType.Length; ++index)
    {
      if (Object.op_Inequality((Object) objectsOfType[index], (Object) null))
        objectsWithComponent.Add(objectsOfType[index].gameObject);
    }
    return objectsWithComponent;
  }

  [Obsolete("Use SetInterestGroups(byte group, bool enabled) instead.")]
  public static void SetReceivingEnabled(int group, bool enabled)
  {
    if (!PhotonNetwork.VerifyCanUseNetwork())
      return;
    PhotonNetwork.SetInterestGroups((byte) group, enabled);
  }

  public static void SetInterestGroups(byte group, bool enabled)
  {
    if (!PhotonNetwork.VerifyCanUseNetwork())
      return;
    if (enabled)
    {
      byte[] enableGroups = new byte[1]{ group };
      PhotonNetwork.networkingPeer.SetInterestGroups((byte[]) null, enableGroups);
    }
    else
    {
      byte[] disableGroups = new byte[1]{ group };
      PhotonNetwork.networkingPeer.SetInterestGroups(disableGroups, (byte[]) null);
    }
  }

  [Obsolete("Use SetInterestGroups(byte[] disableGroups, byte[] enableGroups) instead. Mind the parameter order!")]
  public static void SetReceivingEnabled(int[] enableGroups, int[] disableGroups)
  {
    if (!PhotonNetwork.VerifyCanUseNetwork())
      return;
    byte[] numArray1 = (byte[]) null;
    byte[] numArray2 = (byte[]) null;
    if (enableGroups != null)
    {
      numArray2 = new byte[enableGroups.Length];
      Array.Copy((Array) enableGroups, (Array) numArray2, enableGroups.Length);
    }
    if (disableGroups != null)
    {
      numArray1 = new byte[disableGroups.Length];
      Array.Copy((Array) disableGroups, (Array) numArray1, disableGroups.Length);
    }
    PhotonNetwork.networkingPeer.SetInterestGroups(numArray1, numArray2);
  }

  public static void SetInterestGroups(byte[] disableGroups, byte[] enableGroups)
  {
    if (!PhotonNetwork.VerifyCanUseNetwork())
      return;
    PhotonNetwork.networkingPeer.SetInterestGroups(disableGroups, enableGroups);
  }

  [Obsolete("Use SetSendingEnabled(byte group, bool enabled). Interest Groups have a byte-typed ID. Mind the parameter order.")]
  public static void SetSendingEnabled(int group, bool enabled)
  {
    PhotonNetwork.SetSendingEnabled((byte) group, enabled);
  }

  public static void SetSendingEnabled(byte group, bool enabled)
  {
    if (!PhotonNetwork.VerifyCanUseNetwork())
      return;
    PhotonNetwork.networkingPeer.SetSendingEnabled(group, enabled);
  }

  [Obsolete("Use SetSendingEnabled(byte group, bool enabled). Interest Groups have a byte-typed ID. Mind the parameter order.")]
  public static void SetSendingEnabled(int[] enableGroups, int[] disableGroups)
  {
    byte[] numArray1 = (byte[]) null;
    byte[] numArray2 = (byte[]) null;
    if (enableGroups != null)
    {
      numArray2 = new byte[enableGroups.Length];
      Array.Copy((Array) enableGroups, (Array) numArray2, enableGroups.Length);
    }
    if (disableGroups != null)
    {
      numArray1 = new byte[disableGroups.Length];
      Array.Copy((Array) disableGroups, (Array) numArray1, disableGroups.Length);
    }
    PhotonNetwork.SetSendingEnabled(numArray1, numArray2);
  }

  public static void SetSendingEnabled(byte[] disableGroups, byte[] enableGroups)
  {
    if (!PhotonNetwork.VerifyCanUseNetwork())
      return;
    PhotonNetwork.networkingPeer.SetSendingEnabled(disableGroups, enableGroups);
  }

  public static void SetLevelPrefix(short prefix)
  {
    if (!PhotonNetwork.VerifyCanUseNetwork())
      return;
    PhotonNetwork.networkingPeer.SetLevelPrefix(prefix);
  }

  public static void LoadLevel(int levelNumber)
  {
    PhotonNetwork.networkingPeer.AsynchLevelLoadCall = false;
    if (PhotonNetwork.automaticallySyncScene)
      PhotonNetwork.networkingPeer.SetLevelInPropsIfSynced((object) levelNumber, true);
    PhotonNetwork.isMessageQueueRunning = false;
    PhotonNetwork.networkingPeer.loadingLevelAndPausedNetwork = true;
    SceneManager.LoadScene(levelNumber);
  }

  public static AsyncOperation LoadLevelAsync(int levelNumber)
  {
    PhotonNetwork.networkingPeer.AsynchLevelLoadCall = true;
    if (PhotonNetwork.automaticallySyncScene)
      PhotonNetwork.networkingPeer.SetLevelInPropsIfSynced((object) levelNumber, true);
    PhotonNetwork.isMessageQueueRunning = false;
    PhotonNetwork.networkingPeer.loadingLevelAndPausedNetwork = true;
    return SceneManager.LoadSceneAsync(levelNumber, (LoadSceneMode) 0);
  }

  public static void LoadLevel(string levelName)
  {
    PhotonNetwork.networkingPeer.AsynchLevelLoadCall = false;
    if (PhotonNetwork.automaticallySyncScene)
      PhotonNetwork.networkingPeer.SetLevelInPropsIfSynced((object) levelName, true);
    PhotonNetwork.isMessageQueueRunning = false;
    PhotonNetwork.networkingPeer.loadingLevelAndPausedNetwork = true;
    SceneManager.LoadScene(levelName);
  }

  public static AsyncOperation LoadLevelAsync(string levelName)
  {
    PhotonNetwork.networkingPeer.AsynchLevelLoadCall = true;
    if (PhotonNetwork.automaticallySyncScene)
      PhotonNetwork.networkingPeer.SetLevelInPropsIfSynced((object) levelName, true);
    PhotonNetwork.isMessageQueueRunning = false;
    PhotonNetwork.networkingPeer.loadingLevelAndPausedNetwork = true;
    return SceneManager.LoadSceneAsync(levelName, (LoadSceneMode) 0);
  }

  public static bool WebRpc(string name, object parameters)
  {
    return PhotonNetwork.networkingPeer.WebRpc(name, parameters);
  }

  public static bool CallEvent(byte eventCode, object content, int senderId)
  {
    if (PhotonNetwork.OnEventCall == null)
      return false;
    PhotonNetwork.OnEventCall(eventCode, content, senderId);
    return true;
  }

  public delegate void EventCallback(byte eventCode, object content, int senderId);
}
