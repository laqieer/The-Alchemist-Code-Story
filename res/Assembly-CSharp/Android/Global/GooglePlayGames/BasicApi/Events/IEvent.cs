// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.Events.IEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace GooglePlayGames.BasicApi.Events
{
  public interface IEvent
  {
    string Id { get; }

    string Name { get; }

    string Description { get; }

    string ImageUrl { get; }

    ulong CurrentCount { get; }

    EventVisibility Visibility { get; }
  }
}
