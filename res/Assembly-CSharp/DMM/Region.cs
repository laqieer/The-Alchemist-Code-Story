// Decompiled with JetBrains decompiler
// Type: Region
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
public class Region
{
  public CloudRegionCode Code;
  public string Cluster;
  public string HostAndPort;
  public int Ping;

  public Region(CloudRegionCode code)
  {
    this.Code = code;
    this.Cluster = code.ToString();
  }

  public Region(CloudRegionCode code, string regionCodeString, string address)
  {
    this.Code = code;
    this.Cluster = regionCodeString;
    this.HostAndPort = address;
  }

  public static CloudRegionCode Parse(string codeAsString)
  {
    if (codeAsString == null)
      return CloudRegionCode.none;
    int length = codeAsString.IndexOf('/');
    if (length > 0)
      codeAsString = codeAsString.Substring(0, length);
    codeAsString = codeAsString.ToLower();
    return Enum.IsDefined(typeof (CloudRegionCode), (object) codeAsString) ? (CloudRegionCode) Enum.Parse(typeof (CloudRegionCode), codeAsString) : CloudRegionCode.none;
  }

  internal static CloudRegionFlag ParseFlag(CloudRegionCode region)
  {
    return Enum.IsDefined(typeof (CloudRegionFlag), (object) region.ToString()) ? (CloudRegionFlag) Enum.Parse(typeof (CloudRegionFlag), region.ToString()) : (CloudRegionFlag) 0;
  }

  [Obsolete]
  internal static CloudRegionFlag ParseFlag(string codeAsString)
  {
    codeAsString = codeAsString.ToLower();
    CloudRegionFlag flag = (CloudRegionFlag) 0;
    if (Enum.IsDefined(typeof (CloudRegionFlag), (object) codeAsString))
      flag = (CloudRegionFlag) Enum.Parse(typeof (CloudRegionFlag), codeAsString);
    return flag;
  }

  public override string ToString()
  {
    return string.Format("'{0}' \t{1}ms \t{2}", (object) this.Cluster, (object) this.Ping, (object) this.HostAndPort);
  }
}
