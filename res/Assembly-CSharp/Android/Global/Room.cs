﻿// Decompiled with JetBrains decompiler
// Type: Room
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : RoomInfo
{
  internal Room(string roomName, RoomOptions options)
    : base(roomName, (ExitGames.Client.Photon.Hashtable) null)
  {
    if (options == null)
      options = new RoomOptions();
    this.visibleField = options.IsVisible;
    this.openField = options.IsOpen;
    this.maxPlayersField = options.MaxPlayers;
    this.autoCleanUpField = false;
    this.InternalCacheProperties(options.CustomRoomProperties);
    this.PropertiesListedInLobby = options.CustomRoomPropertiesForLobby;
  }

  public new string Name
  {
    get
    {
      return this.nameField;
    }
    internal set
    {
      this.nameField = value;
    }
  }

  public new bool IsOpen
  {
    get
    {
      return this.openField;
    }
    set
    {
      if (!this.Equals((object) PhotonNetwork.room))
        Debug.LogWarning((object) "Can't set open when not in that room.");
      if (value != this.openField && !PhotonNetwork.offlineMode)
      {
        NetworkingPeer networkingPeer = PhotonNetwork.networkingPeer;
        ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
        ((Dictionary<object, object>) hashtable).Add((object) (byte) 253, (object) value);
        ExitGames.Client.Photon.Hashtable gameProperties = hashtable;
        __Null local = null;
        int num = 0;
        networkingPeer.OpSetPropertiesOfRoom(gameProperties, (ExitGames.Client.Photon.Hashtable) local, num != 0);
      }
      this.openField = value;
    }
  }

  public new bool IsVisible
  {
    get
    {
      return this.visibleField;
    }
    set
    {
      if (!this.Equals((object) PhotonNetwork.room))
        Debug.LogWarning((object) "Can't set visible when not in that room.");
      if (value != this.visibleField && !PhotonNetwork.offlineMode)
      {
        NetworkingPeer networkingPeer = PhotonNetwork.networkingPeer;
        ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
        ((Dictionary<object, object>) hashtable).Add((object) (byte) 254, (object) value);
        ExitGames.Client.Photon.Hashtable gameProperties = hashtable;
        __Null local = null;
        int num = 0;
        networkingPeer.OpSetPropertiesOfRoom(gameProperties, (ExitGames.Client.Photon.Hashtable) local, num != 0);
      }
      this.visibleField = value;
    }
  }

  public string[] PropertiesListedInLobby { get; private set; }

  public bool AutoCleanUp
  {
    get
    {
      return this.autoCleanUpField;
    }
  }

  public int MaxPlayers
  {
    get
    {
      return (int) this.maxPlayersField;
    }
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
        ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
        ((Dictionary<object, object>) hashtable).Add((object) byte.MaxValue, (object) (byte) value);
        ExitGames.Client.Photon.Hashtable gameProperties = hashtable;
        __Null local = null;
        int num = 0;
        networkingPeer.OpSetPropertiesOfRoom(gameProperties, (ExitGames.Client.Photon.Hashtable) local, num != 0);
      }
      this.maxPlayersField = (byte) value;
    }
  }

  public new int PlayerCount
  {
    get
    {
      if (PhotonNetwork.playerList != null)
        return PhotonNetwork.playerList.Length;
      return 0;
    }
  }

  public string[] ExpectedUsers
  {
    get
    {
      return this.expectedUsersField;
    }
  }

  protected internal int MasterClientId
  {
    get
    {
      return this.masterClientIdField;
    }
    set
    {
      this.masterClientIdField = value;
    }
  }

  public void SetCustomProperties(ExitGames.Client.Photon.Hashtable propertiesToSet, ExitGames.Client.Photon.Hashtable expectedValues = null, bool webForward = false)
  {
    if (propertiesToSet == null)
      return;
    ExitGames.Client.Photon.Hashtable stringKeys1 = ((IDictionary) propertiesToSet).StripToStringKeys();
    ExitGames.Client.Photon.Hashtable stringKeys2 = ((IDictionary) expectedValues).StripToStringKeys();
    bool flag = stringKeys2 == null || ((Dictionary<object, object>) stringKeys2).Count == 0;
    if (flag)
    {
      ((IDictionary) this.CustomProperties).Merge((IDictionary) stringKeys1);
      ((IDictionary) this.CustomProperties).StripKeysWithNullValues();
    }
    if (!PhotonNetwork.offlineMode)
      PhotonNetwork.networkingPeer.OpSetPropertiesOfRoom(stringKeys1, stringKeys2, webForward);
    if (!PhotonNetwork.offlineMode && !flag)
      return;
    this.InternalCacheProperties(stringKeys1);
    NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonCustomRoomPropertiesChanged, (object) stringKeys1);
  }

  public void SetPropertiesListedInLobby(string[] propsListedInLobby)
  {
    PhotonNetwork.networkingPeer.OpSetPropertiesOfRoom(new ExitGames.Client.Photon.Hashtable()
    {
      [(object) (byte) 250] = (object) propsListedInLobby
    }, (ExitGames.Client.Photon.Hashtable) null, false);
    this.PropertiesListedInLobby = propsListedInLobby;
  }

  public void ClearExpectedUsers()
  {
    PhotonNetwork.networkingPeer.OpSetPropertiesOfRoom(new ExitGames.Client.Photon.Hashtable()
    {
      [(object) (byte) 247] = (object) new string[0]
    }, new ExitGames.Client.Photon.Hashtable()
    {
      [(object) (byte) 247] = (object) this.ExpectedUsers
    }, false);
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
    get
    {
      return this.Name;
    }
    internal set
    {
      this.Name = value;
    }
  }

  [Obsolete("Please use IsOpen (updated case for naming).")]
  public new bool open
  {
    get
    {
      return this.IsOpen;
    }
    set
    {
      this.IsOpen = value;
    }
  }

  [Obsolete("Please use IsVisible (updated case for naming).")]
  public new bool visible
  {
    get
    {
      return this.IsVisible;
    }
    set
    {
      this.IsVisible = value;
    }
  }

  [Obsolete("Please use PropertiesListedInLobby (updated case for naming).")]
  public string[] propertiesListedInLobby
  {
    get
    {
      return this.PropertiesListedInLobby;
    }
    private set
    {
      this.PropertiesListedInLobby = value;
    }
  }

  [Obsolete("Please use AutoCleanUp (updated case for naming).")]
  public bool autoCleanUp
  {
    get
    {
      return this.AutoCleanUp;
    }
  }

  [Obsolete("Please use MaxPlayers (updated case for naming).")]
  public int maxPlayers
  {
    get
    {
      return this.MaxPlayers;
    }
    set
    {
      this.MaxPlayers = value;
    }
  }

  [Obsolete("Please use PlayerCount (updated case for naming).")]
  public new int playerCount
  {
    get
    {
      return this.PlayerCount;
    }
  }

  [Obsolete("Please use ExpectedUsers (updated case for naming).")]
  public string[] expectedUsers
  {
    get
    {
      return this.ExpectedUsers;
    }
  }

  [Obsolete("Please use MasterClientId (updated case for naming).")]
  protected internal int masterClientId
  {
    get
    {
      return this.MasterClientId;
    }
    set
    {
      this.MasterClientId = value;
    }
  }
}
