// Decompiled with JetBrains decompiler
// Type: ExitGames.Client.Photon.Chat.ChannelCreationOptions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace ExitGames.Client.Photon.Chat
{
  public class ChannelCreationOptions
  {
    public static ChannelCreationOptions Default = new ChannelCreationOptions();

    public bool PublishSubscribers { get; set; }

    public int MaxSubscribers { get; set; }
  }
}
