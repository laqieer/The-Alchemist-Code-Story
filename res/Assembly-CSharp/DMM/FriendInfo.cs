// Decompiled with JetBrains decompiler
// Type: FriendInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
public class FriendInfo
{
  [Obsolete("Use UserId.")]
  public string Name => this.UserId;

  public string UserId { get; protected internal set; }

  public bool IsOnline { get; protected internal set; }

  public string Room { get; protected internal set; }

  public bool IsInRoom => this.IsOnline && !string.IsNullOrEmpty(this.Room);

  public override string ToString()
  {
    return string.Format("{0}\t is: {1}", (object) this.UserId, this.IsOnline ? (!this.IsInRoom ? (object) "on master" : (object) "playing") : (object) "offline");
  }
}
