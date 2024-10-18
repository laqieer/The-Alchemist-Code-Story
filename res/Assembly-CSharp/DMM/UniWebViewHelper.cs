// Decompiled with JetBrains decompiler
// Type: UniWebViewHelper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.IO;
using UnityEngine;

#nullable disable
public class UniWebViewHelper
{
  public static string StreamingAssetURLForPath(string path)
  {
    UniWebViewLogger.Instance.Critical("The current build target is not supported.");
    return string.Empty;
  }

  public static string PersistentDataURLForPath(string path)
  {
    return Path.Combine("file://" + Application.persistentDataPath, path);
  }
}
