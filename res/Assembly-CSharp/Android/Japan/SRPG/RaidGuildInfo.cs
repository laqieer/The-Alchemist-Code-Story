// Decompiled with JetBrains decompiler
// Type: SRPG.RaidGuildInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class RaidGuildInfo
  {
    private RaidGuildInfoData mBeat;
    private RaidGuildInfoData mRescue;

    public RaidGuildInfoData Beat
    {
      get
      {
        return this.mBeat;
      }
    }

    public RaidGuildInfoData Rescue
    {
      get
      {
        return this.mRescue;
      }
    }

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
