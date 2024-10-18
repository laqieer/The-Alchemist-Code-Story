// Decompiled with JetBrains decompiler
// Type: ExitGames.Client.Photon.Chat.CustomAuthenticationType
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace ExitGames.Client.Photon.Chat
{
  public enum CustomAuthenticationType : byte
  {
    Custom = 0,
    Steam = 1,
    Facebook = 2,
    Oculus = 3,
    PlayStation = 4,
    Xbox = 5,
    None = 255, // 0xFF
  }
}
