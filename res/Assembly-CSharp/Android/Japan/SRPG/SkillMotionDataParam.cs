// Decompiled with JetBrains decompiler
// Type: SRPG.SkillMotionDataParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

namespace SRPG
{
  public class SkillMotionDataParam
  {
    private List<string> mUnitList;
    private List<string> mJobList;
    private string mMotionId;
    private SkillMotionDataParam.Flags mFlags;

    public List<string> UnitList
    {
      get
      {
        return this.mUnitList;
      }
    }

    public List<string> JobList
    {
      get
      {
        return this.mJobList;
      }
    }

    public string MotionId
    {
      get
      {
        return this.mMotionId;
      }
    }

    public bool IsBattleScene
    {
      get
      {
        return (this.mFlags & SkillMotionDataParam.Flags.IsBattleScene) != (SkillMotionDataParam.Flags) 0;
      }
    }

    public void Deserialize(JSON_SkillMotionDataParam json)
    {
      if (json == null)
        return;
      if (json.unit_ids != null)
      {
        this.mUnitList = new List<string>(json.unit_ids.Length);
        for (int index = 0; index < json.unit_ids.Length; ++index)
          this.mUnitList.Add(json.unit_ids[index]);
      }
      if (json.job_ids != null)
      {
        this.mJobList = new List<string>(json.job_ids.Length);
        for (int index = 0; index < json.job_ids.Length; ++index)
          this.mJobList.Add(json.job_ids[index]);
      }
      this.mMotionId = json.motnm;
      this.mFlags = (SkillMotionDataParam.Flags) 0;
      if (json.isbtl == 0)
        return;
      this.mFlags |= SkillMotionDataParam.Flags.IsBattleScene;
    }

    public enum Flags
    {
      IsBattleScene = 1,
    }
  }
}
