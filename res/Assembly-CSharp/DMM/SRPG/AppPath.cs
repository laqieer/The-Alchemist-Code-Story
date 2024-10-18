// Decompiled with JetBrains decompiler
// Type: SRPG.AppPath
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public static class AppPath
  {
    public static string persistentDataPath => Application.dataPath + "/../data";

    public static string temporaryCachePath => Application.dataPath + "/../temp";

    public static string assetCachePath => Application.dataPath + "/..";

    public static string assetCachePathOld => Application.dataPath + "/..";

    public static string crashLogPath => Application.dataPath + "/../crash";
  }
}
