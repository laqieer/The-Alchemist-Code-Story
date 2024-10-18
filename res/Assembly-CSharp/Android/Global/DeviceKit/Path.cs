// Decompiled with JetBrains decompiler
// Type: DeviceKit.Path
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Runtime.InteropServices;

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

    [DllImport("devicekit")]
    private static extern string devicekit_documentPath();

    [DllImport("devicekit")]
    private static extern string devicekit_applicationDataPath();

    [DllImport("devicekit")]
    private static extern string devicekit_cachePath();
  }
}
