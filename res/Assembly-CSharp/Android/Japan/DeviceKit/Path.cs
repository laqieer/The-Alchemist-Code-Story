// Decompiled with JetBrains decompiler
// Type: DeviceKit.Path
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.IO;
using UnityEngine;

namespace DeviceKit
{
  public static class Path
  {
    public static string documentPath
    {
      get
      {
        return Path.devicekit_documentPath();
      }
    }

    public static string applicationDataPath
    {
      get
      {
        return Path.devicekit_applicationDataPath();
      }
    }

    public static string cachePath
    {
      get
      {
        return Path.devicekit_cachePath();
      }
    }

    private static string CreateDirectory(string directory)
    {
      Directory.CreateDirectory(directory);
      return directory;
    }

    private static string devicekit_documentPath()
    {
      return Path.CreateDirectory(Application.persistentDataPath + "/Documents");
    }

    private static string devicekit_applicationDataPath()
    {
      return Path.CreateDirectory(Application.persistentDataPath + "/AppData");
    }

    private static string devicekit_cachePath()
    {
      return Application.temporaryCachePath;
    }
  }
}
