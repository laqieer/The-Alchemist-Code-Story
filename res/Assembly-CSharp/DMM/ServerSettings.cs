// Decompiled with JetBrains decompiler
// Type: ServerSettings
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using ExitGames.Client.Photon;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
[Serializable]
public class ServerSettings : ScriptableObject
{
  public string AppID = string.Empty;
  public string VoiceAppID = string.Empty;
  public string ChatAppID = string.Empty;
  public ServerSettings.HostingOption HostType;
  public CloudRegionCode PreferredRegion;
  public CloudRegionFlag EnabledRegions = (CloudRegionFlag) -1;
  public ConnectionProtocol Protocol;
  public string ServerAddress = string.Empty;
  public int ServerPort = 5055;
  public int VoiceServerPort = 5055;
  public bool JoinLobby;
  public bool EnableLobbyStatistics;
  public PhotonLogLevel PunLogging;
  public DebugLevel NetworkLogging = (DebugLevel) 1;
  public bool RunInBackground = true;
  public List<string> RpcList = new List<string>();
  [HideInInspector]
  public bool DisableAutoOpenWizard;

  public void UseCloudBestRegion(string cloudAppid)
  {
    this.HostType = ServerSettings.HostingOption.BestRegion;
    this.AppID = cloudAppid;
  }

  public void UseCloud(string cloudAppid)
  {
    this.HostType = ServerSettings.HostingOption.PhotonCloud;
    this.AppID = cloudAppid;
  }

  public void UseCloud(string cloudAppid, CloudRegionCode code)
  {
    this.HostType = ServerSettings.HostingOption.PhotonCloud;
    this.AppID = cloudAppid;
    this.PreferredRegion = code;
  }

  public void UseMyServer(string serverAddress, int serverPort, string application)
  {
    this.HostType = ServerSettings.HostingOption.SelfHosted;
    this.AppID = application == null ? "master" : application;
    this.ServerAddress = serverAddress;
    this.ServerPort = serverPort;
  }

  public static bool IsAppId(string val)
  {
    try
    {
      Guid guid = new Guid(val);
    }
    catch
    {
      return false;
    }
    return true;
  }

  public static CloudRegionCode BestRegionCodeInPreferences
  {
    get => PhotonHandler.BestRegionCodeInPreferences;
  }

  public static void ResetBestRegionCodeInPreferences()
  {
    PhotonHandler.BestRegionCodeInPreferences = CloudRegionCode.none;
  }

  public virtual string ToString()
  {
    return "ServerSettings: " + (object) this.HostType + " " + this.ServerAddress;
  }

  public enum HostingOption
  {
    NotSet,
    PhotonCloud,
    SelfHosted,
    OfflineMode,
    BestRegion,
  }
}
