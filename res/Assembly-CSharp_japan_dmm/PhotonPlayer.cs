// Decompiled with JetBrains decompiler
// Type: PhotonPlayer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using ExitGames.Client.Photon;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class PhotonPlayer : 
  IComparable<PhotonPlayer>,
  IComparable<int>,
  IEquatable<PhotonPlayer>,
  IEquatable<int>
{
  private int actorID = -1;
  private string nameField = string.Empty;
  public readonly bool IsLocal;
  public object TagObject;

  public PhotonPlayer(bool isLocal, int actorID, string name)
  {
    this.CustomProperties = new Hashtable();
    this.IsLocal = isLocal;
    this.actorID = actorID;
    this.nameField = name;
  }

  protected internal PhotonPlayer(bool isLocal, int actorID, Hashtable properties)
  {
    this.CustomProperties = new Hashtable();
    this.IsLocal = isLocal;
    this.actorID = actorID;
    this.InternalCacheProperties(properties);
  }

  public int ID => this.actorID;

  public string NickName
  {
    get => this.nameField;
    set
    {
      if (!this.IsLocal)
      {
        Debug.LogError((object) "Error: Cannot change the name of a remote player!");
      }
      else
      {
        if (string.IsNullOrEmpty(value) || value.Equals(this.nameField))
          return;
        this.nameField = value;
        PhotonNetwork.playerName = value;
      }
    }
  }

  public string UserId { get; internal set; }

  public bool IsMasterClient => PhotonNetwork.networkingPeer.mMasterClientId == this.ID;

  public bool IsInactive { get; set; }

  public Hashtable CustomProperties { get; internal set; }

  public Hashtable AllProperties
  {
    get
    {
      Hashtable target = new Hashtable();
      ((IDictionary) target).Merge((IDictionary) this.CustomProperties);
      target[(object) byte.MaxValue] = (object) this.NickName;
      return target;
    }
  }

  public override bool Equals(object p)
  {
    return p is PhotonPlayer photonPlayer && this.GetHashCode() == photonPlayer.GetHashCode();
  }

  public override int GetHashCode() => this.ID;

  internal void InternalChangeLocalID(int newID)
  {
    if (!this.IsLocal)
      Debug.LogError((object) "ERROR You should never change PhotonPlayer IDs!");
    else
      this.actorID = newID;
  }

  internal void InternalCacheProperties(Hashtable properties)
  {
    if (properties == null || ((Dictionary<object, object>) properties).Count == 0 || this.CustomProperties.Equals((object) properties))
      return;
    if (((Dictionary<object, object>) properties).ContainsKey((object) byte.MaxValue))
      this.nameField = (string) properties[(object) byte.MaxValue];
    if (((Dictionary<object, object>) properties).ContainsKey((object) (byte) 253))
      this.UserId = (string) properties[(object) (byte) 253];
    if (((Dictionary<object, object>) properties).ContainsKey((object) (byte) 254))
      this.IsInactive = (bool) properties[(object) (byte) 254];
    ((IDictionary) this.CustomProperties).MergeStringKeys((IDictionary) properties);
    ((IDictionary) this.CustomProperties).StripKeysWithNullValues();
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
    bool flag1 = stringKeys2 == null || ((Dictionary<object, object>) stringKeys2).Count == 0;
    bool flag2 = this.actorID > 0 && !PhotonNetwork.offlineMode;
    if (flag1)
    {
      ((IDictionary) this.CustomProperties).Merge((IDictionary) stringKeys1);
      ((IDictionary) this.CustomProperties).StripKeysWithNullValues();
    }
    if (flag2)
      PhotonNetwork.networkingPeer.OpSetPropertiesOfActor(this.actorID, stringKeys1, stringKeys2, webForward);
    if (flag2 && !flag1)
      return;
    this.InternalCacheProperties(stringKeys1);
    NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonPlayerPropertiesChanged, (object) this, (object) stringKeys1);
  }

  public static PhotonPlayer Find(int ID)
  {
    return PhotonNetwork.networkingPeer != null ? PhotonNetwork.networkingPeer.GetPlayerWithId(ID) : (PhotonPlayer) null;
  }

  public PhotonPlayer Get(int id) => PhotonPlayer.Find(id);

  public PhotonPlayer GetNext() => this.GetNextFor(this.ID);

  public PhotonPlayer GetNextFor(PhotonPlayer currentPlayer)
  {
    return currentPlayer == null ? (PhotonPlayer) null : this.GetNextFor(currentPlayer.ID);
  }

  public PhotonPlayer GetNextFor(int currentPlayerId)
  {
    if (PhotonNetwork.networkingPeer == null || PhotonNetwork.networkingPeer.mActors == null || PhotonNetwork.networkingPeer.mActors.Count < 2)
      return (PhotonPlayer) null;
    Dictionary<int, PhotonPlayer> mActors = PhotonNetwork.networkingPeer.mActors;
    int key1 = int.MaxValue;
    int key2 = currentPlayerId;
    foreach (int key3 in mActors.Keys)
    {
      if (key3 < key2)
        key2 = key3;
      else if (key3 > currentPlayerId && key3 < key1)
        key1 = key3;
    }
    return key1 != int.MaxValue ? mActors[key1] : mActors[key2];
  }

  public int CompareTo(PhotonPlayer other)
  {
    return other == null ? 0 : this.GetHashCode().CompareTo(other.GetHashCode());
  }

  public int CompareTo(int other) => this.GetHashCode().CompareTo(other);

  public bool Equals(PhotonPlayer other)
  {
    return other != null && this.GetHashCode().Equals(other.GetHashCode());
  }

  public bool Equals(int other) => this.GetHashCode().Equals(other);

  public override string ToString()
  {
    return string.IsNullOrEmpty(this.NickName) ? string.Format("#{0:00}{1}{2}", (object) this.ID, !this.IsInactive ? (object) " " : (object) " (inactive)", !this.IsMasterClient ? (object) string.Empty : (object) "(master)") : string.Format("'{0}'{1}{2}", (object) this.NickName, !this.IsInactive ? (object) " " : (object) " (inactive)", !this.IsMasterClient ? (object) string.Empty : (object) "(master)");
  }

  public string ToStringFull()
  {
    return string.Format("#{0:00} '{1}'{2} {3}", (object) this.ID, (object) this.NickName, !this.IsInactive ? (object) string.Empty : (object) " (inactive)", (object) ((IDictionary) this.CustomProperties).ToStringFull());
  }

  [Obsolete("Please use NickName (updated case for naming).")]
  public string name
  {
    get => this.NickName;
    set => this.NickName = value;
  }

  [Obsolete("Please use UserId (updated case for naming).")]
  public string userId
  {
    get => this.UserId;
    internal set => this.UserId = value;
  }

  [Obsolete("Please use IsLocal (updated case for naming).")]
  public bool isLocal => this.IsLocal;

  [Obsolete("Please use IsMasterClient (updated case for naming).")]
  public bool isMasterClient => this.IsMasterClient;

  [Obsolete("Please use IsInactive (updated case for naming).")]
  public bool isInactive
  {
    get => this.IsInactive;
    set => this.IsInactive = value;
  }

  [Obsolete("Please use CustomProperties (updated case for naming).")]
  public Hashtable customProperties
  {
    get => this.CustomProperties;
    internal set => this.CustomProperties = value;
  }

  [Obsolete("Please use AllProperties (updated case for naming).")]
  public Hashtable allProperties => this.AllProperties;
}
