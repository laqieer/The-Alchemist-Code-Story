﻿// Decompiled with JetBrains decompiler
// Type: RoomInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;

public class RoomInfo
{
  private ExitGames.Client.Photon.Hashtable customPropertiesField = new ExitGames.Client.Photon.Hashtable();
  protected bool openField = true;
  protected bool visibleField = true;
  protected bool autoCleanUpField = PhotonNetwork.autoCleanUpPlayerObjects;
  protected byte maxPlayersField;
  protected string[] expectedUsersField;
  protected string nameField;
  protected internal int masterClientIdField;

  protected internal RoomInfo(string roomName, ExitGames.Client.Photon.Hashtable properties)
  {
    this.InternalCacheProperties(properties);
    this.nameField = roomName;
  }

  public bool removedFromList { get; internal set; }

  protected internal bool serverSideMasterClient { get; private set; }

  public ExitGames.Client.Photon.Hashtable CustomProperties
  {
    get
    {
      return this.customPropertiesField;
    }
  }

  public string Name
  {
    get
    {
      return this.nameField;
    }
  }

  public int PlayerCount { get; private set; }

  public bool IsLocalClientInside { get; set; }

  public byte MaxPlayers
  {
    get
    {
      return this.maxPlayersField;
    }
  }

  public bool IsOpen
  {
    get
    {
      return this.openField;
    }
  }

  public bool IsVisible
  {
    get
    {
      return this.visibleField;
    }
  }

  public override bool Equals(object other)
  {
    RoomInfo roomInfo = other as RoomInfo;
    if (roomInfo != null)
      return this.Name.Equals(roomInfo.nameField);
    return false;
  }

  public override int GetHashCode()
  {
    return this.nameField.GetHashCode();
  }

  public override string ToString()
  {
    return string.Format("Room: '{0}' {1},{2} {4}/{3} players.", (object) this.nameField, !this.visibleField ? (object) "hidden" : (object) "visible", !this.openField ? (object) "closed" : (object) "open", (object) this.maxPlayersField, (object) this.PlayerCount);
  }

  public string ToStringFull()
  {
    return string.Format("Room: '{0}' {1},{2} {4}/{3} players.\ncustomProps: {5}", (object) this.nameField, !this.visibleField ? (object) "hidden" : (object) "visible", !this.openField ? (object) "closed" : (object) "open", (object) this.maxPlayersField, (object) this.PlayerCount, (object) ((IDictionary) this.customPropertiesField).ToStringFull());
  }

  protected internal void InternalCacheProperties(ExitGames.Client.Photon.Hashtable propertiesToCache)
  {
    if (propertiesToCache == null || ((Dictionary<object, object>) propertiesToCache).Count == 0 || ((object) this.customPropertiesField).Equals((object) propertiesToCache))
      return;
    if (((Dictionary<object, object>) propertiesToCache).ContainsKey((object) (byte) 251))
    {
      this.removedFromList = (bool) propertiesToCache[(object) (byte) 251];
      if (this.removedFromList)
        return;
    }
    if (((Dictionary<object, object>) propertiesToCache).ContainsKey((object) byte.MaxValue))
      this.maxPlayersField = (byte) propertiesToCache[(object) byte.MaxValue];
    if (((Dictionary<object, object>) propertiesToCache).ContainsKey((object) (byte) 253))
      this.openField = (bool) propertiesToCache[(object) (byte) 253];
    if (((Dictionary<object, object>) propertiesToCache).ContainsKey((object) (byte) 254))
      this.visibleField = (bool) propertiesToCache[(object) (byte) 254];
    if (((Dictionary<object, object>) propertiesToCache).ContainsKey((object) (byte) 252))
      this.PlayerCount = (int) (byte) propertiesToCache[(object) (byte) 252];
    if (((Dictionary<object, object>) propertiesToCache).ContainsKey((object) (byte) 249))
      this.autoCleanUpField = (bool) propertiesToCache[(object) (byte) 249];
    if (((Dictionary<object, object>) propertiesToCache).ContainsKey((object) (byte) 248))
    {
      this.serverSideMasterClient = true;
      bool flag = this.masterClientIdField != 0;
      this.masterClientIdField = (int) propertiesToCache[(object) (byte) 248];
      if (flag)
        PhotonNetwork.networkingPeer.UpdateMasterClient();
    }
    if (((Dictionary<object, object>) propertiesToCache).ContainsKey((object) (byte) 247))
      this.expectedUsersField = (string[]) propertiesToCache[(object) (byte) 247];
    ((IDictionary) this.customPropertiesField).MergeStringKeys((IDictionary) propertiesToCache);
  }

  [Obsolete("Please use CustomProperties (updated case for naming).")]
  public ExitGames.Client.Photon.Hashtable customProperties
  {
    get
    {
      return this.CustomProperties;
    }
  }

  [Obsolete("Please use Name (updated case for naming).")]
  public string name
  {
    get
    {
      return this.Name;
    }
  }

  [Obsolete("Please use PlayerCount (updated case for naming).")]
  public int playerCount
  {
    get
    {
      return this.PlayerCount;
    }
    set
    {
      this.PlayerCount = value;
    }
  }

  [Obsolete("Please use IsLocalClientInside (updated case for naming).")]
  public bool isLocalClientInside
  {
    get
    {
      return this.IsLocalClientInside;
    }
    set
    {
      this.IsLocalClientInside = value;
    }
  }

  [Obsolete("Please use MaxPlayers (updated case for naming).")]
  public byte maxPlayers
  {
    get
    {
      return this.MaxPlayers;
    }
  }

  [Obsolete("Please use IsOpen (updated case for naming).")]
  public bool open
  {
    get
    {
      return this.IsOpen;
    }
  }

  [Obsolete("Please use IsVisible (updated case for naming).")]
  public bool visible
  {
    get
    {
      return this.IsVisible;
    }
  }
}
