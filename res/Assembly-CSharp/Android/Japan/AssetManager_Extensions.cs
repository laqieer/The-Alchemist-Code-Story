// Decompiled with JetBrains decompiler
// Type: AssetManager_Extensions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
      case AssetManager.AssetFormats.Windows:
        return "win32/";
      default:
        return "ios/";
    }
  }
}
