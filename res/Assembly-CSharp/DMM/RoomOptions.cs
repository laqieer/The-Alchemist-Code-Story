// Decompiled with JetBrains decompiler
// Type: RoomOptions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using ExitGames.Client.Photon;
using System;

#nullable disable
public class RoomOptions
{
  private bool isVisibleField = true;
  private bool isOpenField = true;
  public byte MaxPlayers;
  public int PlayerTtl;
  public int EmptyRoomTtl;
  private bool cleanupCacheOnLeaveField = PhotonNetwork.autoCleanUpPlayerObjects;
  public Hashtable CustomRoomProperties;
  public string[] CustomRoomPropertiesForLobby = new string[0];
  public string[] Plugins;
  private bool suppressRoomEventsField;
  private bool publishUserIdField;
  private bool deleteNullPropertiesField;

  public bool IsVisible
  {
    get => this.isVisibleField;
    set => this.isVisibleField = value;
  }

  public bool IsOpen
  {
    get => this.isOpenField;
    set => this.isOpenField = value;
  }

  public bool CleanupCacheOnLeave
  {
    get => this.cleanupCacheOnLeaveField;
    set => this.cleanupCacheOnLeaveField = value;
  }

  public bool SuppressRoomEvents => this.suppressRoomEventsField;

  public bool PublishUserId
  {
    get => this.publishUserIdField;
    set => this.publishUserIdField = value;
  }

  public bool DeleteNullProperties
  {
    get => this.deleteNullPropertiesField;
    set => this.deleteNullPropertiesField = value;
  }

  [Obsolete("Use property with uppercase naming instead.")]
  public bool isVisible
  {
    get => this.isVisibleField;
    set => this.isVisibleField = value;
  }

  [Obsolete("Use property with uppercase naming instead.")]
  public bool isOpen
  {
    get => this.isOpenField;
    set => this.isOpenField = value;
  }

  [Obsolete("Use property with uppercase naming instead.")]
  public byte maxPlayers
  {
    get => this.MaxPlayers;
    set => this.MaxPlayers = value;
  }

  [Obsolete("Use property with uppercase naming instead.")]
  public bool cleanupCacheOnLeave
  {
    get => this.cleanupCacheOnLeaveField;
    set => this.cleanupCacheOnLeaveField = value;
  }

  [Obsolete("Use property with uppercase naming instead.")]
  public Hashtable customRoomProperties
  {
    get => this.CustomRoomProperties;
    set => this.CustomRoomProperties = value;
  }

  [Obsolete("Use property with uppercase naming instead.")]
  public string[] customRoomPropertiesForLobby
  {
    get => this.CustomRoomPropertiesForLobby;
    set => this.CustomRoomPropertiesForLobby = value;
  }

  [Obsolete("Use property with uppercase naming instead.")]
  public string[] plugins
  {
    get => this.Plugins;
    set => this.Plugins = value;
  }

  [Obsolete("Use property with uppercase naming instead.")]
  public bool suppressRoomEvents => this.suppressRoomEventsField;

  [Obsolete("Use property with uppercase naming instead.")]
  public bool publishUserId
  {
    get => this.publishUserIdField;
    set => this.publishUserIdField = value;
  }
}
