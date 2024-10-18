// Decompiled with JetBrains decompiler
// Type: SRPG.VersusRankMissionScheduleParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class VersusRankMissionScheduleParam
  {
    private int mScheduleId;
    private string mIName;

    public int ScheduleId => this.mScheduleId;

    public string IName => this.mIName;

    public bool Deserialize(JSON_VersusRankMissionScheduleParam json)
    {
      if (json == null)
        return false;
      this.mScheduleId = json.schedule_id;
      this.mIName = json.iname;
      return true;
    }
  }
}
