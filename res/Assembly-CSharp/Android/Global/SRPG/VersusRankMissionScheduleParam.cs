// Decompiled with JetBrains decompiler
// Type: SRPG.VersusRankMissionScheduleParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
