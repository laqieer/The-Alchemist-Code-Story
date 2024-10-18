// Decompiled with JetBrains decompiler
// Type: PhotonMessageInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

public struct PhotonMessageInfo
{
  private readonly int timeInt;
  public readonly PhotonPlayer sender;
  public readonly PhotonView photonView;

  public PhotonMessageInfo(PhotonPlayer player, int timestamp, PhotonView view)
  {
    this.sender = player;
    this.timeInt = timestamp;
    this.photonView = view;
  }

  public double timestamp
  {
    get
    {
      return (double) (uint) this.timeInt / 1000.0;
    }
  }

  public override string ToString()
  {
    return string.Format("[PhotonMessageInfo: Sender='{1}' Senttime={0}]", (object) this.timestamp, (object) this.sender);
  }
}
