﻿// Decompiled with JetBrains decompiler
// Type: ExitGames.Client.Photon.Chat.ErrorCode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace ExitGames.Client.Photon.Chat
{
  public class ErrorCode
  {
    public const int Ok = 0;
    public const int OperationNotAllowedInCurrentState = -3;
    public const int InvalidOperationCode = -2;
    public const int InternalServerError = -1;
    public const int InvalidAuthentication = 32767;
    public const int GameIdAlreadyExists = 32766;
    public const int GameFull = 32765;
    public const int GameClosed = 32764;
    public const int ServerFull = 32762;
    public const int UserBlocked = 32761;
    public const int NoRandomMatchFound = 32760;
    public const int GameDoesNotExist = 32758;
    public const int MaxCcuReached = 32757;
    public const int InvalidRegion = 32756;
    public const int CustomAuthenticationFailed = 32755;
  }
}
