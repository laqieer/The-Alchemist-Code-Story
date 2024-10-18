// Decompiled with JetBrains decompiler
// Type: PhotonMessageInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
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

  public double timestamp => (double) (uint) this.timeInt / 1000.0;

  public override string ToString()
  {
    return string.Format("[PhotonMessageInfo: Sender='{1}' Senttime={0}]", (object) this.timestamp, (object) this.sender);
  }
}
