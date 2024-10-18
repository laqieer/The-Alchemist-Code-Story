// Decompiled with JetBrains decompiler
// Type: ExitGames.Client.Photon.Chat.ChatDisconnectCause
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
