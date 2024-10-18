// Decompiled with JetBrains decompiler
// Type: FriendInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

public class FriendInfo
{
  public string Name { get; protected internal set; }

  public bool IsOnline { get; protected internal set; }

  public string Room { get; protected internal set; }

  public bool IsInRoom
  {
    get
    {
      if (this.IsOnline)
        return !string.IsNullOrEmpty(this.Room);
      return false;
    }
  }

  public override string ToString()
  {
    return string.Format("{0}\t is: {1}", (object) this.Name, this.IsOnline ? (!this.IsInRoom ? (object) "on master" : (object) "playing") : (object) "offline");
  }
}
