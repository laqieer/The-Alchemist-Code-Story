// Decompiled with JetBrains decompiler
// Type: SRPG.VersusRankMissionScheduleParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class VersusRankMissionScheduleParam
  {
    private int mScheduleId;
    private string mIName;

    public int ScheduleId
    {
      get
      {
        return this.mScheduleId;
      }
    }

    public string IName
    {
      get
      {
        return this.mIName;
      }
    }

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
