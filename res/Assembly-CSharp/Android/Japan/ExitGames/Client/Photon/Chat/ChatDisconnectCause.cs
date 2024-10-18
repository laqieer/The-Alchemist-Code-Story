// Decompiled with JetBrains decompiler
// Type: ExitGames.Client.Photon.Chat.ChatDisconnectCause
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
