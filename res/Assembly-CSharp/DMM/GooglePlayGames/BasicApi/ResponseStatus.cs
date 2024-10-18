// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.ResponseStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace GooglePlayGames.BasicApi
{
  public enum ResponseStatus
  {
    Timeout = -5, // 0xFFFFFFFB
    VersionUpdateRequired = -4, // 0xFFFFFFFC
    NotAuthorized = -3, // 0xFFFFFFFD
    InternalError = -2, // 0xFFFFFFFE
    LicenseCheckFailed = -1, // 0xFFFFFFFF
    Success = 1,
    SuccessWithStale = 2,
  }
}
