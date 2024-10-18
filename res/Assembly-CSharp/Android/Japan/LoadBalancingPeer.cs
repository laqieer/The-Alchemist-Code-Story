// Decompiled with JetBrains decompiler
// Type: LoadBalancingPeer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using ExitGames.Client.Photon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class LoadBalancingPeer : PhotonPeer
{
  private readonly Dictionary<byte, object> opParameters = new Dictionary<byte, object>();

  public LoadBalancingPeer(ConnectionProtocol protocolType)
    : base(protocolType)
  {
  }

  public LoadBalancingPeer(IPhotonPeerListener listener, ConnectionProtocol protocolType)
    : base(listener, protocolType)
  {
  }

  internal bool IsProtocolSecure
  {
    get
    {
      return this.UsedProtocol == ConnectionProtocol.WebSocketSecure;
    }
  }

  public virtual bool OpGetRegions(string appId)
  {
    return this.OpCustom((byte) 220, new Dictionary<byte, object>() { [(byte) 224] = (object) appId }, true, (byte) 0, true);
  }

  public virtual bool OpJoinLobby(TypedLobby lobby = null)
  {
    if (this.DebugOut >= DebugLevel.INFO)
      this.Listener.DebugReturn(DebugLevel.INFO, "OpJoinLobby()");
    Dictionary<byte, object> customOpParameters = (Dictionary<byte, object>) null;
    if (lobby != null && !lobby.IsDefault)
    {
      customOpParameters = new Dictionary<byte, object>();
      customOpParameters[(byte) 213] = (object) lobby.Name;
      customOpParameters[(byte) 212] = (object) lobby.Type;
    }
    return this.OpCustom((byte) 229, customOpParameters, true);
  }

  public virtual bool OpLeaveLobby()
  {
    if (this.DebugOut >= DebugLevel.INFO)
      this.Listener.DebugReturn(DebugLevel.INFO, "OpLeaveLobby()");
    return this.OpCustom((byte) 228, (Dictionary<byte, object>) null, true);
  }

  private void RoomOptionsToOpParameters(Dictionary<byte, object> op, RoomOptions roomOptions)
  {
    if (roomOptions == null)
      roomOptions = new RoomOptions();
    Hashtable target = new Hashtable();
    target[(object) (byte) 253] = (object) roomOptions.IsOpen;
    target[(object) (byte) 254] = (object) roomOptions.IsVisible;
    target[(object) (byte) 250] = roomOptions.CustomRoomPropertiesForLobby != null ? (object) roomOptions.CustomRoomPropertiesForLobby : (object) new string[0];
    target.MergeStringKeys((IDictionary) roomOptions.CustomRoomProperties);
    if (roomOptions.MaxPlayers > (byte) 0)
      target[(object) byte.MaxValue] = (object) roomOptions.MaxPlayers;
    op[(byte) 248] = (object) target;
    op[(byte) 241] = (object) roomOptions.CleanupCacheOnLeave;
    if (roomOptions.CleanupCacheOnLeave)
      target[(object) (byte) 249] = (object) true;
    if (roomOptions.PlayerTtl > 0 || roomOptions.PlayerTtl == -1)
    {
      op[(byte) 232] = (object) true;
      op[(byte) 235] = (object) roomOptions.PlayerTtl;
    }
    if (roomOptions.EmptyRoomTtl > 0)
      op[(byte) 236] = (object) roomOptions.EmptyRoomTtl;
    if (roomOptions.SuppressRoomEvents)
      op[(byte) 237] = (object) true;
    if (roomOptions.Plugins != null)
      op[(byte) 204] = (object) roomOptions.Plugins;
    if (!roomOptions.PublishUserId)
      return;
    op[(byte) 239] = (object) true;
  }

  public virtual bool OpCreateRoom(EnterRoomParams opParams)
  {
    if (this.DebugOut >= DebugLevel.INFO)
      this.Listener.DebugReturn(DebugLevel.INFO, "OpCreateRoom()");
    Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
    if (!string.IsNullOrEmpty(opParams.RoomName))
      dictionary[byte.MaxValue] = (object) opParams.RoomName;
    if (opParams.Lobby != null && !string.IsNullOrEmpty(opParams.Lobby.Name))
    {
      dictionary[(byte) 213] = (object) opParams.Lobby.Name;
      dictionary[(byte) 212] = (object) opParams.Lobby.Type;
    }
    if (opParams.ExpectedUsers != null && opParams.ExpectedUsers.Length > 0)
      dictionary[(byte) 238] = (object) opParams.ExpectedUsers;
    if (opParams.OnGameServer)
    {
      if (opParams.PlayerProperties != null && opParams.PlayerProperties.Count > 0)
      {
        dictionary[(byte) 249] = (object) opParams.PlayerProperties;
        dictionary[(byte) 250] = (object) true;
      }
      this.RoomOptionsToOpParameters(dictionary, opParams.RoomOptions);
    }
    return this.OpCustom((byte) 227, dictionary, true);
  }

  public virtual bool OpJoinRoom(EnterRoomParams opParams)
  {
    if (this.DebugOut >= DebugLevel.INFO)
      this.Listener.DebugReturn(DebugLevel.INFO, "OpJoinRoom()");
    Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
    if (!string.IsNullOrEmpty(opParams.RoomName))
      dictionary[byte.MaxValue] = (object) opParams.RoomName;
    if (opParams.CreateIfNotExists)
    {
      dictionary[(byte) 215] = (object) (byte) 1;
      if (opParams.Lobby != null)
      {
        dictionary[(byte) 213] = (object) opParams.Lobby.Name;
        dictionary[(byte) 212] = (object) opParams.Lobby.Type;
      }
    }
    if (opParams.RejoinOnly)
      dictionary[(byte) 215] = (object) (byte) 3;
    if (opParams.ExpectedUsers != null && opParams.ExpectedUsers.Length > 0)
      dictionary[(byte) 238] = (object) opParams.ExpectedUsers;
    if (opParams.OnGameServer)
    {
      if (opParams.PlayerProperties != null && opParams.PlayerProperties.Count > 0)
      {
        dictionary[(byte) 249] = (object) opParams.PlayerProperties;
        dictionary[(byte) 250] = (object) true;
      }
      if (opParams.CreateIfNotExists)
        this.RoomOptionsToOpParameters(dictionary, opParams.RoomOptions);
    }
    return this.OpCustom((byte) 226, dictionary, true);
  }

  public virtual bool OpJoinRandomRoom(OpJoinRandomRoomParams opJoinRandomRoomParams)
  {
    if (this.DebugOut >= DebugLevel.INFO)
      this.Listener.DebugReturn(DebugLevel.INFO, "OpJoinRandomRoom()");
    Hashtable target = new Hashtable();
    target.MergeStringKeys((IDictionary) opJoinRandomRoomParams.ExpectedCustomRoomProperties);
    if (opJoinRandomRoomParams.ExpectedMaxPlayers > (byte) 0)
      target[(object) byte.MaxValue] = (object) opJoinRandomRoomParams.ExpectedMaxPlayers;
    Dictionary<byte, object> customOpParameters = new Dictionary<byte, object>();
    if (target.Count > 0)
      customOpParameters[(byte) 248] = (object) target;
    if (opJoinRandomRoomParams.MatchingType != MatchmakingMode.FillRoom)
      customOpParameters[(byte) 223] = (object) opJoinRandomRoomParams.MatchingType;
    if (opJoinRandomRoomParams.TypedLobby != null && !string.IsNullOrEmpty(opJoinRandomRoomParams.TypedLobby.Name))
    {
      customOpParameters[(byte) 213] = (object) opJoinRandomRoomParams.TypedLobby.Name;
      customOpParameters[(byte) 212] = (object) opJoinRandomRoomParams.TypedLobby.Type;
    }
    if (!string.IsNullOrEmpty(opJoinRandomRoomParams.SqlLobbyFilter))
      customOpParameters[(byte) 245] = (object) opJoinRandomRoomParams.SqlLobbyFilter;
    if (opJoinRandomRoomParams.ExpectedUsers != null && opJoinRandomRoomParams.ExpectedUsers.Length > 0)
      customOpParameters[(byte) 238] = (object) opJoinRandomRoomParams.ExpectedUsers;
    return this.OpCustom((byte) 225, customOpParameters, true);
  }

  public virtual bool OpLeaveRoom(bool becomeInactive)
  {
    Dictionary<byte, object> customOpParameters = new Dictionary<byte, object>();
    if (becomeInactive)
      customOpParameters[(byte) 233] = (object) becomeInactive;
    return this.OpCustom((byte) 254, customOpParameters, true);
  }

  public virtual bool OpGetGameList(TypedLobby lobby, string queryData)
  {
    if (this.DebugOut >= DebugLevel.INFO)
      this.Listener.DebugReturn(DebugLevel.INFO, "OpGetGameList()");
    if (lobby == null)
    {
      if (this.DebugOut >= DebugLevel.INFO)
        this.Listener.DebugReturn(DebugLevel.INFO, "OpGetGameList not sent. Lobby cannot be null.");
      return false;
    }
    if (lobby.Type != LobbyType.SqlLobby)
    {
      if (this.DebugOut >= DebugLevel.INFO)
        this.Listener.DebugReturn(DebugLevel.INFO, "OpGetGameList not sent. LobbyType must be SqlLobby.");
      return false;
    }
    return this.OpCustom((byte) 217, new Dictionary<byte, object>() { [(byte) 213] = (object) lobby.Name, [(byte) 212] = (object) lobby.Type, [(byte) 245] = (object) queryData }, true);
  }

  public virtual bool OpFindFriends(string[] friendsToFind)
  {
    Dictionary<byte, object> customOpParameters = new Dictionary<byte, object>();
    if (friendsToFind != null && friendsToFind.Length > 0)
      customOpParameters[(byte) 1] = (object) friendsToFind;
    return this.OpCustom((byte) 222, customOpParameters, true);
  }

  public bool OpSetCustomPropertiesOfActor(int actorNr, Hashtable actorProperties)
  {
    return this.OpSetPropertiesOfActor(actorNr, actorProperties.StripToStringKeys(), (Hashtable) null, false);
  }

  protected internal bool OpSetPropertiesOfActor(int actorNr, Hashtable actorProperties, Hashtable expectedProperties = null, bool webForward = false)
  {
    if (this.DebugOut >= DebugLevel.INFO)
      this.Listener.DebugReturn(DebugLevel.INFO, "OpSetPropertiesOfActor()");
    if (actorNr <= 0 || actorProperties == null)
    {
      if (this.DebugOut >= DebugLevel.INFO)
        this.Listener.DebugReturn(DebugLevel.INFO, "OpSetPropertiesOfActor not sent. ActorNr must be > 0 and actorProperties != null.");
      return false;
    }
    Dictionary<byte, object> customOpParameters = new Dictionary<byte, object>();
    customOpParameters.Add((byte) 251, (object) actorProperties);
    customOpParameters.Add((byte) 254, (object) actorNr);
    customOpParameters.Add((byte) 250, (object) true);
    if (expectedProperties != null && expectedProperties.Count != 0)
      customOpParameters.Add((byte) 231, (object) expectedProperties);
    if (webForward)
      customOpParameters[(byte) 234] = (object) true;
    return this.OpCustom((byte) 252, customOpParameters, true, (byte) 0, false);
  }

  protected void OpSetPropertyOfRoom(byte propCode, object value)
  {
    this.OpSetPropertiesOfRoom(new Hashtable()
    {
      [(object) propCode] = value
    }, (Hashtable) null, false);
  }

  public bool OpSetCustomPropertiesOfRoom(Hashtable gameProperties, bool broadcast, byte channelId)
  {
    return this.OpSetPropertiesOfRoom(gameProperties.StripToStringKeys(), (Hashtable) null, false);
  }

  protected internal bool OpSetPropertiesOfRoom(Hashtable gameProperties, Hashtable expectedProperties = null, bool webForward = false)
  {
    if (this.DebugOut >= DebugLevel.INFO)
      this.Listener.DebugReturn(DebugLevel.INFO, "OpSetPropertiesOfRoom()");
    Dictionary<byte, object> customOpParameters = new Dictionary<byte, object>();
    customOpParameters.Add((byte) 251, (object) gameProperties);
    customOpParameters.Add((byte) 250, (object) true);
    if (expectedProperties != null && expectedProperties.Count != 0)
      customOpParameters.Add((byte) 231, (object) expectedProperties);
    if (webForward)
      customOpParameters[(byte) 234] = (object) true;
    return this.OpCustom((byte) 252, customOpParameters, true, (byte) 0, false);
  }

  public virtual bool OpAuthenticate(string appId, string appVersion, AuthenticationValues authValues, string regionCode, bool getLobbyStatistics)
  {
    if (this.DebugOut >= DebugLevel.INFO)
      this.Listener.DebugReturn(DebugLevel.INFO, "OpAuthenticate()");
    Dictionary<byte, object> customOpParameters = new Dictionary<byte, object>();
    if (getLobbyStatistics)
      customOpParameters[(byte) 211] = (object) true;
    if (authValues != null && authValues.Token != null)
    {
      customOpParameters[(byte) 221] = (object) authValues.Token;
      return this.OpCustom((byte) 230, customOpParameters, true, (byte) 0, false);
    }
    customOpParameters[(byte) 220] = (object) appVersion;
    customOpParameters[(byte) 224] = (object) appId;
    if (!string.IsNullOrEmpty(regionCode))
      customOpParameters[(byte) 210] = (object) regionCode;
    if (authValues != null)
    {
      if (!string.IsNullOrEmpty(authValues.UserId))
        customOpParameters[(byte) 225] = (object) authValues.UserId;
      if (authValues.AuthType != CustomAuthenticationType.None)
      {
        if (!this.IsProtocolSecure && !this.IsEncryptionAvailable)
        {
          this.Listener.DebugReturn(DebugLevel.ERROR, "OpAuthenticate() failed. When you want Custom Authentication encryption is mandatory.");
          return false;
        }
        customOpParameters[(byte) 217] = (object) authValues.AuthType;
        if (!string.IsNullOrEmpty(authValues.Token))
        {
          customOpParameters[(byte) 221] = (object) authValues.Token;
        }
        else
        {
          if (!string.IsNullOrEmpty(authValues.AuthGetParameters))
            customOpParameters[(byte) 216] = (object) authValues.AuthGetParameters;
          if (authValues.AuthPostData != null)
            customOpParameters[(byte) 214] = authValues.AuthPostData;
        }
      }
    }
    bool flag = this.OpCustom((byte) 230, customOpParameters, true, (byte) 0, this.IsEncryptionAvailable);
    if (!flag)
      this.Listener.DebugReturn(DebugLevel.ERROR, "Error calling OpAuthenticate! Did not work. Check log output, AuthValues and if you're connected.");
    return flag;
  }

  public virtual bool OpAuthenticateOnce(string appId, string appVersion, AuthenticationValues authValues, string regionCode, EncryptionMode encryptionMode, ConnectionProtocol expectedProtocol)
  {
    if (this.DebugOut >= DebugLevel.INFO)
      this.Listener.DebugReturn(DebugLevel.INFO, "OpAuthenticate()");
    Dictionary<byte, object> customOpParameters = new Dictionary<byte, object>();
    if (authValues != null && authValues.Token != null)
    {
      customOpParameters[(byte) 221] = (object) authValues.Token;
      return this.OpCustom((byte) 231, customOpParameters, true, (byte) 0, false);
    }
    if (encryptionMode == EncryptionMode.DatagramEncryption && expectedProtocol != ConnectionProtocol.Udp)
    {
      Debug.LogWarning((object) ("Expected protocol set to UDP, due to encryption mode DatagramEncryption. Changing protocol in PhotonServerSettings from: " + (object) PhotonNetwork.PhotonServerSettings.Protocol));
      PhotonNetwork.PhotonServerSettings.Protocol = ConnectionProtocol.Udp;
      expectedProtocol = ConnectionProtocol.Udp;
    }
    customOpParameters[(byte) 195] = (object) expectedProtocol;
    customOpParameters[(byte) 193] = (object) (byte) encryptionMode;
    customOpParameters[(byte) 220] = (object) appVersion;
    customOpParameters[(byte) 224] = (object) appId;
    if (!string.IsNullOrEmpty(regionCode))
      customOpParameters[(byte) 210] = (object) regionCode;
    if (authValues != null)
    {
      if (!string.IsNullOrEmpty(authValues.UserId))
        customOpParameters[(byte) 225] = (object) authValues.UserId;
      if (authValues.AuthType != CustomAuthenticationType.None)
      {
        customOpParameters[(byte) 217] = (object) authValues.AuthType;
        if (!string.IsNullOrEmpty(authValues.Token))
        {
          customOpParameters[(byte) 221] = (object) authValues.Token;
        }
        else
        {
          if (!string.IsNullOrEmpty(authValues.AuthGetParameters))
            customOpParameters[(byte) 216] = (object) authValues.AuthGetParameters;
          if (authValues.AuthPostData != null)
            customOpParameters[(byte) 214] = authValues.AuthPostData;
        }
      }
    }
    return this.OpCustom((byte) 231, customOpParameters, true, (byte) 0, this.IsEncryptionAvailable);
  }

  public virtual bool OpChangeGroups(byte[] groupsToRemove, byte[] groupsToAdd)
  {
    if (this.DebugOut >= DebugLevel.ALL)
      this.Listener.DebugReturn(DebugLevel.ALL, "OpChangeGroups()");
    Dictionary<byte, object> customOpParameters = new Dictionary<byte, object>();
    if (groupsToRemove != null)
      customOpParameters[(byte) 239] = (object) groupsToRemove;
    if (groupsToAdd != null)
      customOpParameters[(byte) 238] = (object) groupsToAdd;
    return this.OpCustom((byte) 248, customOpParameters, true, (byte) 0);
  }

  public virtual bool OpRaiseEvent(byte eventCode, object customEventContent, bool sendReliable, RaiseEventOptions raiseEventOptions)
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
        this.opParameters[(byte) 247] = (object) raiseEventOptions.CachingOption;
      if (raiseEventOptions.Receivers != ReceiverGroup.Others)
        this.opParameters[(byte) 246] = (object) raiseEventOptions.Receivers;
      if (raiseEventOptions.InterestGroup != (byte) 0)
        this.opParameters[(byte) 240] = (object) raiseEventOptions.InterestGroup;
      if (raiseEventOptions.TargetActors != null)
        this.opParameters[(byte) 252] = (object) raiseEventOptions.TargetActors;
      if (raiseEventOptions.ForwardToWebhook)
        this.opParameters[(byte) 234] = (object) true;
    }
    return this.OpCustom((byte) 253, this.opParameters, sendReliable, raiseEventOptions.SequenceChannel, raiseEventOptions.Encrypt);
  }

  public virtual bool OpSettings(bool receiveLobbyStats)
  {
    if (this.DebugOut >= DebugLevel.ALL)
      this.Listener.DebugReturn(DebugLevel.ALL, "OpSettings()");
    this.opParameters.Clear();
    if (receiveLobbyStats)
      this.opParameters[(byte) 0] = (object) receiveLobbyStats;
    if (this.opParameters.Count == 0)
      return true;
    return this.OpCustom((byte) 218, this.opParameters, true);
  }
}
