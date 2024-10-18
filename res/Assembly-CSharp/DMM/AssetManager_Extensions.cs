// Decompiled with JetBrains decompiler
// Type: AssetManager_Extensions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
public static class AssetManager_Extensions
{
  public static string ToPath(this AssetManager.AssetFormats platform)
  {
    switch (platform)
    {
      case AssetManager.AssetFormats.AndroidGeneric:
      case AssetManager.AssetFormats.AndroidATC:
        return "aatc/";
      case AssetManager.AssetFormats.AndroidDXT:
        return "adxt/";
      case AssetManager.AssetFormats.AndroidPVR:
        return "apvr/";
      case AssetManager.AssetFormats.AndroidETC2:
        return "aetc2/";
      case AssetManager.AssetFormats.Windows:
        return "win32/";
      default:
        return "ios/";
    }
  }
}
