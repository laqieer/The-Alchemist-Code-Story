// Decompiled with JetBrains decompiler
// Type: AssetManager_Extensions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
        return "aatc/";
      case AssetManager.AssetFormats.Text:
        return "Text/";
      case AssetManager.AssetFormats.AndroidASTC:
        return "astc/";
      default:
        return "iOS/";
    }
  }
}
