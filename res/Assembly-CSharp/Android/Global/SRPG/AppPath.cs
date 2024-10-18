// Decompiled with JetBrains decompiler
// Type: SRPG.AppPath
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public static class AppPath
  {
    public static string persistentDataPath
    {
      get
      {
        return Application.persistentDataPath;
      }
    }

    public static string temporaryCachePath
    {
      get
      {
        return Application.temporaryCachePath;
      }
    }

    public static string assetCachePath
    {
      get
      {
        return Application.persistentDataPath;
      }
    }

    public static string assetCachePathOld
    {
      get
      {
        return Application.temporaryCachePath;
      }
    }
  }
}
