// Decompiled with JetBrains decompiler
// Type: RoomInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using ExitGames.Client.Photon;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
public class RoomInfo
{
  private Hashtable customPropertiesField = new Hashtable();
  protected byte maxPlayersField;
  protected int emptyRoomTtlField;
  protected int playerTtlField;
  protected string[] expectedUsersField;
  protected bool openField = true;
  protected bool visibleField = true;
  protected bool autoCleanUpField = PhotonNetwork.autoCleanUpPlayerObjects;
  protected string nameField;
  protected internal int masterClientIdField;

  protected internal RoomInfo(string roomName, Hashtable properties)
  {
    this.InternalCacheProperties(properties);
    this.nameField = roomName;
  }

  public bool removedFromList { get; internal set; }

  protected internal bool serverSideMasterClient { get; private set; }

  public Hashtable CustomProperties => this.customPropertiesField;

  public string Name => this.nameField;

  public int PlayerCount { get; private set; }

  public bool IsLocalClientInside { get; set; }

  public byte MaxPlayers => this.maxPlayersField;

  public bool IsOpen => this.openField;

  public bool IsVisible => this.visibleField;

  public override bool Equals(object other)
  {
    return other is RoomInfo roomInfo && this.Name.Equals(roomInfo.nameField);
  }

  public override int GetHashCode() => this.nameField.GetHashCode();

  public override string ToString()
  {
    return string.Format("Room: '{0}' {1},{2} {4}/{3} players.", (object) this.nameField, !this.visibleField ? (object) "hidden" : (object) "visible", !this.openField ? (object) "closed" : (object) "open", (object) this.maxPlayersField, (object) this.PlayerCount);
  }

  public string ToStringFull()
  {
    return string.Format("Room: '{0}' {1},{2} {4}/{3} players.\ncustomProps: {5}", (object) this.nameField, !this.visibleField ? (object) "hidden" : (object) "visible", !this.openField ? (object) "closed" : (object) "open", (object) this.maxPlayersField, (object) this.PlayerCount, (object) ((IDictionary) this.customPropertiesField).ToStringFull());
  }

  protected internal void InternalCacheProperties(Hashtable propertiesToCache)
  {
    if (propertiesToCache == null || ((Dictionary<object, object>) propertiesToCache).Count == 0 || this.customPropertiesField.Equals((object) propertiesToCache))
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
    if (((Dictionary<object, object>) propertiesToCache).ContainsKey((object) (byte) 245))
      this.emptyRoomTtlField = (int) propertiesToCache[(object) (byte) 245];
    if (((Dictionary<object, object>) propertiesToCache).ContainsKey((object) (byte) 246))
      this.playerTtlField = (int) propertiesToCache[(object) (byte) 246];
    ((IDictionary) this.customPropertiesField).MergeStringKeys((IDictionary) propertiesToCache);
    ((IDictionary) this.customPropertiesField).StripKeysWithNullValues();
  }

  [Obsolete("Please use CustomProperties (updated case for naming).")]
  public Hashtable customProperties => this.CustomProperties;

  [Obsolete("Please use Name (updated case for naming).")]
  public string name => this.Name;

  [Obsolete("Please use PlayerCount (updated case for naming).")]
  public int playerCount
  {
    get => this.PlayerCount;
    set => this.PlayerCount = value;
  }

  [Obsolete("Please use IsLocalClientInside (updated case for naming).")]
  public bool isLocalClientInside
  {
    get => this.IsLocalClientInside;
    set => this.IsLocalClientInside = value;
  }

  [Obsolete("Please use MaxPlayers (updated case for naming).")]
  public byte maxPlayers => this.MaxPlayers;

  [Obsolete("Please use IsOpen (updated case for naming).")]
  public bool open => this.IsOpen;

  [Obsolete("Please use IsVisible (updated case for naming).")]
  public bool visible => this.IsVisible;
}
