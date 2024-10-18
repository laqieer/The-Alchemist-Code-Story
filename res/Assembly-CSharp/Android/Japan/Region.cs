// Decompiled with JetBrains decompiler
// Type: Region
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

public class Region
{
  public CloudRegionCode Code;
  public string HostAndPort;
  public int Ping;

  public static CloudRegionCode Parse(string codeAsString)
  {
    codeAsString = codeAsString.ToLower();
    CloudRegionCode cloudRegionCode = CloudRegionCode.none;
    if (Enum.IsDefined(typeof (CloudRegionCode), (object) codeAsString))
      cloudRegionCode = (CloudRegionCode) Enum.Parse(typeof (CloudRegionCode), codeAsString);
    return cloudRegionCode;
  }

  internal static CloudRegionFlag ParseFlag(string codeAsString)
  {
    codeAsString = codeAsString.ToLower();
    CloudRegionFlag cloudRegionFlag = (CloudRegionFlag) 0;
    if (Enum.IsDefined(typeof (CloudRegionFlag), (object) codeAsString))
      cloudRegionFlag = (CloudRegionFlag) Enum.Parse(typeof (CloudRegionFlag), codeAsString);
    return cloudRegionFlag;
  }

  public override string ToString()
  {
    return string.Format("'{0}' \t{1}ms \t{2}", (object) this.Code, (object) this.Ping, (object) this.HostAndPort);
  }
}
