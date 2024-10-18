// Decompiled with JetBrains decompiler
// Type: SRPG.AppPath
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public static class AppPath
  {
    public static string persistentDataPath
    {
      get
      {
        return Application.dataPath + "/../data";
      }
    }

    public static string temporaryCachePath
    {
      get
      {
        return Application.dataPath + "/../temp";
      }
    }

    public static string assetCachePath
    {
      get
      {
        return Application.dataPath + "/..";
      }
    }

    public static string assetCachePathOld
    {
      get
      {
        return Application.dataPath + "/..";
      }
    }
  }
}
