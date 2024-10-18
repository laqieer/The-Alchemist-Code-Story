// Decompiled with JetBrains decompiler
// Type: FriendInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
