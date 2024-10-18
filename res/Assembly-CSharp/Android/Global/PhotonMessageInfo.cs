// Decompiled with JetBrains decompiler
// Type: PhotonMessageInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
