// Decompiled with JetBrains decompiler
// Type: Room
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using ExitGames.Client.Photon;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class Room : RoomInfo
{
  internal Room(string roomName, RoomOptions options)
    : base(roomName, (Hashtable) null)
  {
    if (options == null)
      options = new RoomOptions();
    this.visibleField = options.IsVisible;
    this.openField = options.IsOpen;
    this.maxPlayersField = options.MaxPlayers;
    this.autoCleanUpField = options.CleanupCacheOnLeave;
    this.InternalCacheProperties(options.CustomRoomProperties);
    this.PropertiesListedInLobby = options.CustomRoomPropertiesForLobby;
  }

  public new string Name
  {
    get => this.nameField;
    internal set => this.nameField = value;
  }

  public new bool IsOpen
  {
    get => this.openField;
    set
    {
      if (!this.Equals((object) PhotonNetwork.room))
        Debug.LogWarning((object) "Can't set open when not in that room.");
      if (value != this.openField && !PhotonNetwork.offlineMode)
      {
        NetworkingPeer networkingPeer = PhotonNetwork.networkingPeer;
        Hashtable hashtable = new Hashtable();
        ((Dictionary<object, object>) hashtable).Add((object) (byte) 253, (object) value);
        Hashtable gameProperties = hashtable;
        networkingPeer.OpSetPropertiesOfRoom(gameProperties);
      }
      this.openField = value;
    }
  }

  public new bool IsVisible
  {
    get => this.visibleField;
    set
    {
      if (!this.Equals((object) PhotonNetwork.room))
        Debug.LogWarning((object) "Can't set visible when not in that room.");
      if (value != this.visibleField && !PhotonNetwork.offlineMode)
      {
        NetworkingPeer networkingPeer = PhotonNetwork.networkingPeer;
        Hashtable hashtable = new Hashtable();
        ((Dictionary<object, object>) hashtable).Add((object) (byte) 254, (object) value);
        Hashtable gameProperties = hashtable;
        networkingPeer.OpSetPropertiesOfRoom(gameProperties);
      }
      this.visibleField = value;
    }
  }

  public string[] PropertiesListedInLobby { get; private set; }

  public bool AutoCleanUp => this.autoCleanUpField;

  public int MaxPlayers
  {
    get => (int) this.maxPlayersField;
    set
    {
      if (!this.Equals((object) PhotonNetwork.room))
        Debug.LogWarning((object) "Can't set MaxPlayers when not in that room.");
      if (value > (int) byte.MaxValue)
      {
        Debug.LogWarning((object) ("Can't set Room.MaxPlayers to: " + (object) value + ". Using max value: 255."));
        value = (int) byte.MaxValue;
      }
      if (value != (int) this.maxPlayersField && !PhotonNetwork.offlineMode)
      {
        NetworkingPeer networkingPeer = PhotonNetwork.networkingPeer;
        Hashtable hashtable = new Hashtable();
        ((Dictionary<object, object>) hashtable).Add((object) byte.MaxValue, (object) (byte) value);
        Hashtable gameProperties = hashtable;
        networkingPeer.OpSetPropertiesOfRoom(gameProperties);
      }
      this.maxPlayersField = (byte) value;
    }
  }

  public new int PlayerCount
  {
    get => PhotonNetwork.playerList != null ? PhotonNetwork.playerList.Length : 0;
  }

  public string[] ExpectedUsers => this.expectedUsersField;

  public int PlayerTtl
  {
    get => this.playerTtlField;
    set
    {
      if (!this.Equals((object) PhotonNetwork.room))
        Debug.LogWarning((object) "Can't set PlayerTtl when not in a room.");
      if (value != this.playerTtlField && !PhotonNetwork.offlineMode)
        PhotonNetwork.networkingPeer.OpSetPropertyOfRoom((byte) 246, (object) value);
      this.playerTtlField = value;
    }
  }

  public int EmptyRoomTtl
  {
    get => this.emptyRoomTtlField;
    set
    {
      if (!this.Equals((object) PhotonNetwork.room))
        Debug.LogWarning((object) "Can't set EmptyRoomTtl when not in a room.");
      if (value != this.emptyRoomTtlField && !PhotonNetwork.offlineMode)
        PhotonNetwork.networkingPeer.OpSetPropertyOfRoom((byte) 245, (object) value);
      this.emptyRoomTtlField = value;
    }
  }

  protected internal int MasterClientId
  {
    get => this.masterClientIdField;
    set => this.masterClientIdField = value;
  }

  public void SetCustomProperties(
    Hashtable propertiesToSet,
    Hashtable expectedValues = null,
    bool webForward = false)
  {
    if (propertiesToSet == null)
      return;
    Hashtable stringKeys1 = ((IDictionary) propertiesToSet).StripToStringKeys();
    Hashtable stringKeys2 = ((IDictionary) expectedValues).StripToStringKeys();
    bool flag = stringKeys2 == null || ((Dictionary<object, object>) stringKeys2).Count == 0;
    if (PhotonNetwork.offlineMode || flag)
    {
      ((IDictionary) this.CustomProperties).Merge((IDictionary) stringKeys1);
      ((IDictionary) this.CustomProperties).StripKeysWithNullValues();
    }
    if (!PhotonNetwork.offlineMode)
      PhotonNetwork.networkingPeer.OpSetPropertiesOfRoom(stringKeys1, stringKeys2, webForward);
    if (!PhotonNetwork.offlineMode && !flag)
      return;
    NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonCustomRoomPropertiesChanged, (object) stringKeys1);
  }

  public void SetPropertiesListedInLobby(string[] propsListedInLobby)
  {
    PhotonNetwork.networkingPeer.OpSetPropertiesOfRoom(new Hashtable()
    {
      [(object) (byte) 250] = (object) propsListedInLobby
    });
    this.PropertiesListedInLobby = propsListedInLobby;
  }

  public void ClearExpectedUsers()
  {
    PhotonNetwork.networkingPeer.OpSetPropertiesOfRoom(new Hashtable()
    {
      [(object) (byte) 247] = (object) new string[0]
    }, new Hashtable()
    {
      [(object) (byte) 247] = (object) this.ExpectedUsers
    });
  }

  public void SetExpectedUsers(string[] expectedUsers)
  {
    PhotonNetwork.networkingPeer.OpSetPropertiesOfRoom(new Hashtable()
    {
      [(object) (byte) 247] = (object) expectedUsers
    }, new Hashtable()
    {
      [(object) (byte) 247] = (object) this.ExpectedUsers
    });
  }

  public override string ToString()
  {
    return string.Format("Room: '{0}' {1},{2} {4}/{3} players.", (object) this.nameField, !this.visibleField ? (object) "hidden" : (object) "visible", !this.openField ? (object) "closed" : (object) "open", (object) this.maxPlayersField, (object) this.PlayerCount);
  }

  public new string ToStringFull()
  {
    return string.Format("Room: '{0}' {1},{2} {4}/{3} players.\ncustomProps: {5}", (object) this.nameField, !this.visibleField ? (object) "hidden" : (object) "visible", !this.openField ? (object) "closed" : (object) "open", (object) this.maxPlayersField, (object) this.PlayerCount, (object) ((IDictionary) this.CustomProperties).ToStringFull());
  }

  [Obsolete("Please use Name (updated case for naming).")]
  public new string name
  {
    get => this.Name;
    internal set => this.Name = value;
  }

  [Obsolete("Please use IsOpen (updated case for naming).")]
  public new bool open
  {
    get => this.IsOpen;
    set => this.IsOpen = value;
  }

  [Obsolete("Please use IsVisible (updated case for naming).")]
  public new bool visible
  {
    get => this.IsVisible;
    set => this.IsVisible = value;
  }

  [Obsolete("Please use PropertiesListedInLobby (updated case for naming).")]
  public string[] propertiesListedInLobby
  {
    get => this.PropertiesListedInLobby;
    private set => this.PropertiesListedInLobby = value;
  }

  [Obsolete("Please use AutoCleanUp (updated case for naming).")]
  public bool autoCleanUp => this.AutoCleanUp;

  [Obsolete("Please use MaxPlayers (updated case for naming).")]
  public int maxPlayers
  {
    get => this.MaxPlayers;
    set => this.MaxPlayers = value;
  }

  [Obsolete("Please use PlayerCount (updated case for naming).")]
  public new int playerCount => this.PlayerCount;

  [Obsolete("Please use ExpectedUsers (updated case for naming).")]
  public string[] expectedUsers => this.ExpectedUsers;

  [Obsolete("Please use MasterClientId (updated case for naming).")]
  protected internal int masterClientId
  {
    get => this.MasterClientId;
    set => this.MasterClientId = value;
  }
}
