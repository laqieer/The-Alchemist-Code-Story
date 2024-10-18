// Decompiled with JetBrains decompiler
// Type: SRPG.SkillMotionDataParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class SkillMotionDataParam
  {
    private List<string> mUnitList;
    private List<string> mJobList;
    private string mMotionId;
    public string mEffectId;
    private SkillMotionDataParam.Flags mFlags;

    public List<string> UnitList => this.mUnitList;

    public List<string> JobList => this.mJobList;

    public string MotionId => this.mMotionId;

    public string EffectId => this.mEffectId;

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
      this.mEffectId = json.effnm;
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
