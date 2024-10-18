// Decompiled with JetBrains decompiler
// Type: PhotonPingManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections;
using System.Diagnostics;
using System.Net;
using UnityEngine;

#nullable disable
public class PhotonPingManager
{
  public bool UseNative;
  public static int Attempts = 5;
  public static bool IgnoreInitialAttempt = true;
  public static int MaxMilliseconsPerPing = 800;
  private const string wssProtocolString = "wss://";
  private int PingsRunning;

  public Region BestRegion
  {
    get
    {
      Region bestRegion = (Region) null;
      int num = int.MaxValue;
      foreach (Region availableRegion in PhotonNetwork.networkingPeer.AvailableRegions)
      {
        Debug.Log((object) ("BestRegion checks region: " + (object) availableRegion));
        if (availableRegion.Ping != 0 && availableRegion.Ping < num)
        {
          num = availableRegion.Ping;
          bestRegion = availableRegion;
        }
      }
      return bestRegion;
    }
  }

  public bool Done => this.PingsRunning == 0;

  [DebuggerHidden]
  public IEnumerator PingSocket(Region region)
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new PhotonPingManager.\u003CPingSocket\u003Ec__Iterator0()
    {
      region = region,
      \u0024this = this
    };
  }

  public static string ResolveHost(string hostName)
  {
    string empty = string.Empty;
    try
    {
      IPAddress[] hostAddresses = Dns.GetHostAddresses(hostName);
      if (hostAddresses.Length == 1)
        return hostAddresses[0].ToString();
      for (int index = 0; index < hostAddresses.Length; ++index)
      {
        IPAddress ipAddress = hostAddresses[index];
        if (ipAddress != null)
        {
          if (ipAddress.ToString().Contains(":"))
            return ipAddress.ToString();
          if (string.IsNullOrEmpty(empty))
            empty = hostAddresses.ToString();
        }
      }
    }
    catch (Exception ex)
    {
      Debug.Log((object) ("Exception caught! " + ex.Source + " Message: " + ex.Message));
    }
    return empty;
  }
}
