// Decompiled with JetBrains decompiler
// Type: SRPG.RaidGuildInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class RaidGuildInfo
  {
    private RaidGuildInfoData mBeat;
    private RaidGuildInfoData mRescue;

    public RaidGuildInfoData Beat => this.mBeat;

    public RaidGuildInfoData Rescue => this.mRescue;

    public bool Deserialize(Json_RaidGuildInfo json)
    {
      this.mBeat = new RaidGuildInfoData();
      if (json.beat != null)
        this.mBeat.Deserialize(json.beat);
      this.mRescue = new RaidGuildInfoData();
      if (json.rescue != null)
        this.mRescue.Deserialize(json.rescue);
      return true;
    }
  }
}
