// Decompiled with JetBrains decompiler
// Type: ExitGames.Client.Photon.Chat.ChatOperationCode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace ExitGames.Client.Photon.Chat
{
  public class ChatOperationCode
  {
    public const byte Authenticate = 230;
    public const byte Subscribe = 0;
    public const byte Unsubscribe = 1;
    public const byte Publish = 2;
    public const byte SendPrivate = 3;
    public const byte ChannelHistory = 4;
    public const byte UpdateStatus = 5;
    public const byte AddFriends = 6;
    public const byte RemoveFriends = 7;
  }
}
