// Decompiled with JetBrains decompiler
// Type: PhotonPingManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Diagnostics;
using System.Net;
using UnityEngine;

public class PhotonPingManager
{
  public static int Attempts = 5;
  public static bool IgnoreInitialAttempt = true;
  public static int MaxMilliseconsPerPing = 800;
  public bool UseNative;
  private int PingsRunning;

  public Region BestRegion
  {
    get
    {
      Region region = (Region) null;
      int num = int.MaxValue;
      foreach (Region availableRegion in PhotonNetwork.networkingPeer.AvailableRegions)
      {
        Debug.Log((object) ("BestRegion checks region: " + (object) availableRegion));
        if (availableRegion.Ping != 0 && availableRegion.Ping < num)
        {
          num = availableRegion.Ping;
          region = availableRegion;
        }
      }
      return region;
    }
  }

  public bool Done
  {
    get
    {
      return this.PingsRunning == 0;
    }
  }

  [DebuggerHidden]
  public IEnumerator PingSocket(Region region)
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new PhotonPingManager.\u003CPingSocket\u003Ec__Iterator0() { region = region, \u0024this = this };
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
