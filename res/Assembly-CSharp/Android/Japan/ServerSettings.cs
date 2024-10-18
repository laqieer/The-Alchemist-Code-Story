// Decompiled with JetBrains decompiler
// Type: ServerSettings
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using ExitGames.Client.Photon;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ServerSettings : ScriptableObject
{
  public string AppID = string.Empty;
  public string VoiceAppID = string.Empty;
  public string ChatAppID = string.Empty;
  public CloudRegionFlag EnabledRegions = (CloudRegionFlag) -1;
  public string ServerAddress = string.Empty;
  public int ServerPort = 5055;
  public int VoiceServerPort = 5055;
  public DebugLevel NetworkLogging = DebugLevel.ERROR;
  public bool RunInBackground = true;
  public List<string> RpcList = new List<string>();
  public ServerSettings.HostingOption HostType;
  public CloudRegionCode PreferredRegion;
  public ConnectionProtocol Protocol;
  public bool JoinLobby;
  public bool EnableLobbyStatistics;
  public PhotonLogLevel PunLogging;
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
    get
    {
      return PhotonHandler.BestRegionCodeInPreferences;
    }
  }

  public static CloudRegionCode BestRegionCodeCurrently
  {
    get
    {
      return PhotonHandler.BestRegionCodeCurrently;
    }
  }

  public static void ResetBestRegionCodeInPreferences()
  {
    PhotonHandler.BestRegionCodeInPreferences = CloudRegionCode.none;
  }

  public override string ToString()
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
