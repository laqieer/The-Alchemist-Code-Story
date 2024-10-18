// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.ResponseStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace GooglePlayGames.BasicApi
{
  public enum ResponseStatus
  {
    Timeout = -5,
    VersionUpdateRequired = -4,
    NotAuthorized = -3,
    InternalError = -2,
    LicenseCheckFailed = -1,
    Success = 1,
    SuccessWithStale = 2,
  }
}
