﻿// Decompiled with JetBrains decompiler
// Type: ExitGames.Client.Photon.Chat.ChatDisconnectCause
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace ExitGames.Client.Photon.Chat
{
  public enum ChatDisconnectCause
  {
    None,
    DisconnectByServerUserLimit,
    ExceptionOnConnect,
    DisconnectByServer,
    TimeoutDisconnect,
    Exception,
    InvalidAuthentication,
    MaxCcuReached,
    InvalidRegion,
    OperationNotAllowedInCurrentState,
    CustomAuthenticationFailed,
  }
}
