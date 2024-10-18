// Decompiled with JetBrains decompiler
// Type: LoadBalancingPeer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using ExitGames.Client.Photon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
internal class LoadBalancingPeer : PhotonPeer
{
  private readonly Dictionary<byte, object> opParameters = new Dictionary<byte, object>();

  public LoadBalancingPeer(ConnectionProtocol protocolType)
    : base(protocolType)
  {
  }

  public LoadBalancingPeer(IPhotonPeerListener listener, ConnectionProtocol protocolType)
    : this(protocolType)
  {
    this.Listener = listener;
  }

  internal bool IsProtocolSecure => this.UsedProtocol == 5;

  public virtual bool OpGetRegions(string appId)
  {
    Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
    dictionary[(byte) 224] = (object) appId;
    SendOptions sendOptions1 = new SendOptions();
    ((SendOptions) ref sendOptions1).Reliability = true;
    sendOptions1.Channel = (byte) 0;
    sendOptions1.Encrypt = true;
    SendOptions sendOptions2 = sendOptions1;
    return this.SendOperation((byte) 220, dictionary, sendOptions2);
  }

  public virtual bool OpJoinLobby(TypedLobby lobby = null)
  {
    if (this.DebugOut >= 3)
      this.Listener.DebugReturn((DebugLevel) 3, "OpJoinLobby()");
    Dictionary<byte, object> dictionary = (Dictionary<byte, object>) null;
    if (lobby != null && !lobby.IsDefault)
    {
      dictionary = new Dictionary<byte, object>();
      dictionary[(byte) 213] = (object) lobby.Name;
      dictionary[(byte) 212] = (object) (byte) lobby.Type;
    }
    return this.SendOperation((byte) 229, dictionary, SendOptions.SendReliable);
  }

  public virtual bool OpLeaveLobby()
  {
    if (this.DebugOut >= 3)
      this.Listener.DebugReturn((DebugLevel) 3, "OpLeaveLobby()");
    return this.SendOperation((byte) 228, (Dictionary<byte, object>) null, SendOptions.SendReliable);
  }

  private void RoomOptionsToOpParameters(Dictionary<byte, object> op, RoomOptions roomOptions)
  {
    if (roomOptions == null)
      roomOptions = new RoomOptions();
    Hashtable target = new Hashtable();
    target[(object) (byte) 253] = (object) roomOptions.IsOpen;
    target[(object) (byte) 254] = (object) roomOptions.IsVisible;
    target[(object) (byte) 250] = roomOptions.CustomRoomPropertiesForLobby != null ? (object) roomOptions.CustomRoomPropertiesForLobby : (object) new string[0];
    ((IDictionary) target).MergeStringKeys((IDictionary) roomOptions.CustomRoomProperties);
    if (roomOptions.MaxPlayers > (byte) 0)
      target[(object) byte.MaxValue] = (object) roomOptions.MaxPlayers;
    op[(byte) 248] = (object) target;
    int num1 = 0;
    op[(byte) 241] = (object) roomOptions.CleanupCacheOnLeave;
    if (roomOptions.CleanupCacheOnLeave)
    {
      num1 |= 2;
      target[(object) (byte) 249] = (object) true;
    }
    int num2 = num1 | 1;
    op[(byte) 232] = (object) true;
    if (roomOptions.PlayerTtl > 0 || roomOptions.PlayerTtl == -1)
      op[(byte) 235] = (object) roomOptions.PlayerTtl;
    if (roomOptions.EmptyRoomTtl > 0)
      op[(byte) 236] = (object) roomOptions.EmptyRoomTtl;
    if (roomOptions.SuppressRoomEvents)
    {
      num2 |= 4;
      op[(byte) 237] = (object) true;
    }
    if (roomOptions.Plugins != null)
      op[(byte) 204] = (object) roomOptions.Plugins;
    if (roomOptions.PublishUserId)
    {
      num2 |= 8;
      op[(byte) 239] = (object) true;
    }
    if (roomOptions.DeleteNullProperties)
      num2 |= 16;
    op[(byte) 191] = (object) num2;
  }

  public virtual bool OpCreateRoom(EnterRoomParams opParams)
  {
    if (this.DebugOut >= 3)
      this.Listener.DebugReturn((DebugLevel) 3, "OpCreateRoom()");
    Dictionary<byte, object> op = new Dictionary<byte, object>();
    if (!string.IsNullOrEmpty(opParams.RoomName))
      op[byte.MaxValue] = (object) opParams.RoomName;
    if (opParams.Lobby != null && !string.IsNullOrEmpty(opParams.Lobby.Name))
    {
      op[(byte) 213] = (object) opParams.Lobby.Name;
      op[(byte) 212] = (object) (byte) opParams.Lobby.Type;
    }
    if (opParams.ExpectedUsers != null && opParams.ExpectedUsers.Length > 0)
      op[(byte) 238] = (object) opParams.ExpectedUsers;
    if (opParams.OnGameServer)
    {
      if (opParams.PlayerProperties != null && ((Dictionary<object, object>) opParams.PlayerProperties).Count > 0)
      {
        op[(byte) 249] = (object) opParams.PlayerProperties;
        op[(byte) 250] = (object) true;
      }
      this.RoomOptionsToOpParameters(op, opParams.RoomOptions);
    }
    return this.SendOperation((byte) 227, op, SendOptions.SendReliable);
  }

  public virtual bool OpJoinRoom(EnterRoomParams opParams)
  {
    if (this.DebugOut >= 3)
      this.Listener.DebugReturn((DebugLevel) 3, "OpJoinRoom()");
    Dictionary<byte, object> op = new Dictionary<byte, object>();
    if (!string.IsNullOrEmpty(opParams.RoomName))
      op[byte.MaxValue] = (object) opParams.RoomName;
    if (opParams.CreateIfNotExists)
    {
      op[(byte) 215] = (object) (byte) 1;
      if (opParams.Lobby != null)
      {
        op[(byte) 213] = (object) opParams.Lobby.Name;
        op[(byte) 212] = (object) (byte) opParams.Lobby.Type;
      }
    }
    if (opParams.RejoinOnly)
      op[(byte) 215] = (object) (byte) 3;
    if (opParams.ExpectedUsers != null && opParams.ExpectedUsers.Length > 0)
      op[(byte) 238] = (object) opParams.ExpectedUsers;
    if (opParams.OnGameServer)
    {
      if (opParams.PlayerProperties != null && ((Dictionary<object, object>) opParams.PlayerProperties).Count > 0)
      {
        op[(byte) 249] = (object) opParams.PlayerProperties;
        op[(byte) 250] = (object) true;
      }
      if (opParams.CreateIfNotExists)
        this.RoomOptionsToOpParameters(op, opParams.RoomOptions);
    }
    return this.SendOperation((byte) 226, op, SendOptions.SendReliable);
  }

  public virtual bool OpJoinRandomRoom(OpJoinRandomRoomParams opJoinRandomRoomParams)
  {
    if (this.DebugOut >= 3)
      this.Listener.DebugReturn((DebugLevel) 3, "OpJoinRandomRoom()");
    Hashtable target = new Hashtable();
    ((IDictionary) target).MergeStringKeys((IDictionary) opJoinRandomRoomParams.ExpectedCustomRoomProperties);
    if (opJoinRandomRoomParams.ExpectedMaxPlayers > (byte) 0)
      target[(object) byte.MaxValue] = (object) opJoinRandomRoomParams.ExpectedMaxPlayers;
    Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
    if (((Dictionary<object, object>) target).Count > 0)
      dictionary[(byte) 248] = (object) target;
    if (opJoinRandomRoomParams.MatchingType != MatchmakingMode.FillRoom)
      dictionary[(byte) 223] = (object) (byte) opJoinRandomRoomParams.MatchingType;
    if (opJoinRandomRoomParams.TypedLobby != null && !string.IsNullOrEmpty(opJoinRandomRoomParams.TypedLobby.Name))
    {
      dictionary[(byte) 213] = (object) opJoinRandomRoomParams.TypedLobby.Name;
      dictionary[(byte) 212] = (object) (byte) opJoinRandomRoomParams.TypedLobby.Type;
    }
    if (!string.IsNullOrEmpty(opJoinRandomRoomParams.SqlLobbyFilter))
      dictionary[(byte) 245] = (object) opJoinRandomRoomParams.SqlLobbyFilter;
    if (opJoinRandomRoomParams.ExpectedUsers != null && opJoinRandomRoomParams.ExpectedUsers.Length > 0)
      dictionary[(byte) 238] = (object) opJoinRandomRoomParams.ExpectedUsers;
    return this.SendOperation((byte) 225, dictionary, SendOptions.SendReliable);
  }

  public virtual bool OpLeaveRoom(bool becomeInactive)
  {
    Dictionary<byte, object> dictionary = (Dictionary<byte, object>) null;
    if (becomeInactive)
    {
      dictionary = new Dictionary<byte, object>();
      dictionary[(byte) 233] = (object) becomeInactive;
    }
    return this.SendOperation((byte) 254, dictionary, SendOptions.SendReliable);
  }

  public virtual bool OpGetGameList(TypedLobby lobby, string queryData)
  {
    if (this.DebugOut >= 3)
      this.Listener.DebugReturn((DebugLevel) 3, "OpGetGameList()");
    if (lobby == null)
    {
      if (this.DebugOut >= 3)
        this.Listener.DebugReturn((DebugLevel) 3, "OpGetGameList not sent. Lobby cannot be null.");
      return false;
    }
    if (lobby.Type != LobbyType.SqlLobby)
    {
      if (this.DebugOut >= 3)
        this.Listener.DebugReturn((DebugLevel) 3, "OpGetGameList not sent. LobbyType must be SqlLobby.");
      return false;
    }
    return this.SendOperation((byte) 217, new Dictionary<byte, object>()
    {
      [(byte) 213] = (object) lobby.Name,
      [(byte) 212] = (object) (byte) lobby.Type,
      [(byte) 245] = (object) queryData
    }, SendOptions.SendReliable);
  }

  public virtual bool OpFindFriends(string[] friendsToFind)
  {
    Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
    if (friendsToFind != null && friendsToFind.Length > 0)
      dictionary[(byte) 1] = (object) friendsToFind;
    return this.SendOperation((byte) 222, dictionary, SendOptions.SendReliable);
  }

  public bool OpSetCustomPropertiesOfActor(int actorNr, Hashtable actorProperties)
  {
    return this.OpSetPropertiesOfActor(actorNr, ((IDictionary) actorProperties).StripToStringKeys());
  }

  protected internal bool OpSetPropertiesOfActor(
    int actorNr,
    Hashtable actorProperties,
    Hashtable expectedProperties = null,
    bool webForward = false)
  {
    if (this.DebugOut >= 3)
      this.Listener.DebugReturn((DebugLevel) 3, "OpSetPropertiesOfActor()");
    if (actorNr <= 0 || actorProperties == null)
    {
      if (this.DebugOut >= 3)
        this.Listener.DebugReturn((DebugLevel) 3, "OpSetPropertiesOfActor not sent. ActorNr must be > 0 and actorProperties != null.");
      return false;
    }
    Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
    dictionary.Add((byte) 251, (object) actorProperties);
    dictionary.Add((byte) 254, (object) actorNr);
    dictionary.Add((byte) 250, (object) true);
    if (expectedProperties != null && ((Dictionary<object, object>) expectedProperties).Count != 0)
      dictionary.Add((byte) 231, (object) expectedProperties);
    if (webForward)
      dictionary[(byte) 234] = (object) true;
    SendOptions sendOptions1 = new SendOptions();
    ((SendOptions) ref sendOptions1).Reliability = true;
    sendOptions1.Channel = (byte) 0;
    sendOptions1.Encrypt = false;
    SendOptions sendOptions2 = sendOptions1;
    return this.SendOperation((byte) 252, dictionary, sendOptions2);
  }

  protected internal void OpSetPropertyOfRoom(byte propCode, object value)
  {
    this.OpSetPropertiesOfRoom(new Hashtable()
    {
      [(object) propCode] = value
    });
  }

  public bool OpSetCustomPropertiesOfRoom(Hashtable gameProperties, bool broadcast, byte channelId)
  {
    return this.OpSetPropertiesOfRoom(((IDictionary) gameProperties).StripToStringKeys());
  }

  protected internal bool OpSetPropertiesOfRoom(
    Hashtable gameProperties,
    Hashtable expectedProperties = null,
    bool webForward = false)
  {
    if (this.DebugOut >= 3)
      this.Listener.DebugReturn((DebugLevel) 3, "OpSetPropertiesOfRoom()");
    Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
    dictionary.Add((byte) 251, (object) gameProperties);
    dictionary.Add((byte) 250, (object) true);
    if (expectedProperties != null && ((Dictionary<object, object>) expectedProperties).Count != 0)
      dictionary.Add((byte) 231, (object) expectedProperties);
    if (webForward)
      dictionary[(byte) 234] = (object) true;
    SendOptions sendOptions1 = new SendOptions();
    ((SendOptions) ref sendOptions1).Reliability = true;
    sendOptions1.Channel = (byte) 0;
    sendOptions1.Encrypt = false;
    SendOptions sendOptions2 = sendOptions1;
    return this.SendOperation((byte) 252, dictionary, sendOptions2);
  }

  public virtual bool OpAuthenticate(
    string appId,
    string appVersion,
    AuthenticationValues authValues,
    string regionCode,
    bool getLobbyStatistics)
  {
    if (this.DebugOut >= 3)
      this.Listener.DebugReturn((DebugLevel) 3, "OpAuthenticate()");
    Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
    if (getLobbyStatistics)
      dictionary[(byte) 211] = (object) true;
    if (authValues != null && authValues.Token != null)
    {
      dictionary[(byte) 221] = (object) authValues.Token;
      SendOptions sendOptions1 = new SendOptions();
      ((SendOptions) ref sendOptions1).Reliability = true;
      sendOptions1.Channel = (byte) 0;
      sendOptions1.Encrypt = false;
      SendOptions sendOptions2 = sendOptions1;
      return this.SendOperation((byte) 230, dictionary, sendOptions2);
    }
    dictionary[(byte) 220] = (object) appVersion;
    dictionary[(byte) 224] = (object) appId;
    if (!string.IsNullOrEmpty(regionCode))
      dictionary[(byte) 210] = (object) regionCode;
    if (authValues != null)
    {
      if (!string.IsNullOrEmpty(authValues.UserId))
        dictionary[(byte) 225] = (object) authValues.UserId;
      if (authValues.AuthType != CustomAuthenticationType.None)
      {
        if (!this.IsProtocolSecure && !this.IsEncryptionAvailable)
        {
          this.Listener.DebugReturn((DebugLevel) 1, "OpAuthenticate() failed. When you want Custom Authentication encryption is mandatory.");
          return false;
        }
        dictionary[(byte) 217] = (object) (byte) authValues.AuthType;
        if (!string.IsNullOrEmpty(authValues.Token))
        {
          dictionary[(byte) 221] = (object) authValues.Token;
        }
        else
        {
          if (!string.IsNullOrEmpty(authValues.AuthGetParameters))
            dictionary[(byte) 216] = (object) authValues.AuthGetParameters;
          if (authValues.AuthPostData != null)
            dictionary[(byte) 214] = authValues.AuthPostData;
        }
      }
    }
    SendOptions sendOptions3 = new SendOptions();
    ((SendOptions) ref sendOptions3).Reliability = true;
    sendOptions3.Channel = (byte) 0;
    sendOptions3.Encrypt = this.IsEncryptionAvailable;
    SendOptions sendOptions4 = sendOptions3;
    bool flag = this.SendOperation((byte) 230, dictionary, sendOptions4);
    if (!flag)
      this.Listener.DebugReturn((DebugLevel) 1, "Error calling OpAuthenticate! Did not work. Check log output, AuthValues and if you're connected.");
    return flag;
  }

  public virtual bool OpAuthenticateOnce(
    string appId,
    string appVersion,
    AuthenticationValues authValues,
    string regionCode,
    EncryptionMode encryptionMode,
    ConnectionProtocol expectedProtocol)
  {
    if (this.DebugOut >= 3)
      this.Listener.DebugReturn((DebugLevel) 3, "OpAuthenticate()");
    Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
    if (authValues != null && authValues.Token != null)
    {
      dictionary[(byte) 221] = (object) authValues.Token;
      SendOptions sendOptions1 = new SendOptions();
      ((SendOptions) ref sendOptions1).Reliability = true;
      sendOptions1.Channel = (byte) 0;
      sendOptions1.Encrypt = false;
      SendOptions sendOptions2 = sendOptions1;
      return this.SendOperation((byte) 231, dictionary, sendOptions2);
    }
    if (encryptionMode == EncryptionMode.DatagramEncryption && expectedProtocol != null)
    {
      Debug.LogWarning((object) ("Expected protocol set to UDP, due to encryption mode DatagramEncryption. Changing protocol in PhotonServerSettings from: " + (object) PhotonNetwork.PhotonServerSettings.Protocol));
      PhotonNetwork.PhotonServerSettings.Protocol = (ConnectionProtocol) 0;
      expectedProtocol = (ConnectionProtocol) 0;
    }
    dictionary[(byte) 195] = (object) (byte) expectedProtocol;
    dictionary[(byte) 193] = (object) (byte) encryptionMode;
    dictionary[(byte) 220] = (object) appVersion;
    dictionary[(byte) 224] = (object) appId;
    if (!string.IsNullOrEmpty(regionCode))
      dictionary[(byte) 210] = (object) regionCode;
    if (authValues != null)
    {
      if (!string.IsNullOrEmpty(authValues.UserId))
        dictionary[(byte) 225] = (object) authValues.UserId;
      if (authValues.AuthType != CustomAuthenticationType.None)
      {
        dictionary[(byte) 217] = (object) (byte) authValues.AuthType;
        if (!string.IsNullOrEmpty(authValues.Token))
        {
          dictionary[(byte) 221] = (object) authValues.Token;
        }
        else
        {
          if (!string.IsNullOrEmpty(authValues.AuthGetParameters))
            dictionary[(byte) 216] = (object) authValues.AuthGetParameters;
          if (authValues.AuthPostData != null)
            dictionary[(byte) 214] = authValues.AuthPostData;
        }
      }
    }
    SendOptions sendOptions3 = new SendOptions();
    ((SendOptions) ref sendOptions3).Reliability = true;
    sendOptions3.Channel = (byte) 0;
    sendOptions3.Encrypt = this.IsEncryptionAvailable;
    SendOptions sendOptions4 = sendOptions3;
    return this.SendOperation((byte) 231, dictionary, sendOptions4);
  }

  public virtual bool OpChangeGroups(byte[] groupsToRemove, byte[] groupsToAdd)
  {
    if (this.DebugOut >= 5)
      this.Listener.DebugReturn((DebugLevel) 5, "OpChangeGroups()");
    Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
    if (groupsToRemove != null)
      dictionary[(byte) 239] = (object) groupsToRemove;
    if (groupsToAdd != null)
      dictionary[(byte) 238] = (object) groupsToAdd;
    return this.SendOperation((byte) 248, dictionary, SendOptions.SendReliable);
  }

  public virtual bool OpRaiseEvent(
    byte eventCode,
    object customEventContent,
    bool sendReliable,
    RaiseEventOptions raiseEventOptions)
  {
    this.opParameters.Clear();
    this.opParameters[(byte) 244] = (object) eventCode;
    if (customEventContent != null)
      this.opParameters[(byte) 245] = customEventContent;
    if (raiseEventOptions == null)
    {
      raiseEventOptions = RaiseEventOptions.Default;
    }
    else
    {
      if (raiseEventOptions.CachingOption != EventCaching.DoNotCache)
        this.opParameters[(byte) 247] = (object) (byte) raiseEventOptions.CachingOption;
      if (raiseEventOptions.Receivers != ReceiverGroup.Others)
        this.opParameters[(byte) 246] = (object) (byte) raiseEventOptions.Receivers;
      if (raiseEventOptions.InterestGroup != (byte) 0)
        this.opParameters[(byte) 240] = (object) raiseEventOptions.InterestGroup;
      if (raiseEventOptions.TargetActors != null)
        this.opParameters[(byte) 252] = (object) raiseEventOptions.TargetActors;
      if (raiseEventOptions.ForwardToWebhook)
        this.opParameters[(byte) 234] = (object) true;
    }
    SendOptions sendOptions = new SendOptions();
    ((SendOptions) ref sendOptions).Reliability = sendReliable;
    sendOptions.Channel = raiseEventOptions.SequenceChannel;
    sendOptions.Encrypt = raiseEventOptions.Encrypt;
    return this.SendOperation((byte) 253, this.opParameters, sendOptions);
  }

  public virtual bool OpSettings(bool receiveLobbyStats)
  {
    if (this.DebugOut >= 5)
      this.Listener.DebugReturn((DebugLevel) 5, "OpSettings()");
    this.opParameters.Clear();
    if (receiveLobbyStats)
      this.opParameters[(byte) 0] = (object) receiveLobbyStats;
    return this.opParameters.Count == 0 || this.SendOperation((byte) 218, this.opParameters, SendOptions.SendReliable);
  }

  private enum RoomOptionBit
  {
    CheckUserOnJoin = 1,
    DeleteCacheOnLeave = 2,
    SuppressRoomEvents = 4,
    PublishUserId = 8,
    DeleteNullProps = 16, // 0x00000010
    BroadcastPropsChangeToAll = 32, // 0x00000020
  }
}
