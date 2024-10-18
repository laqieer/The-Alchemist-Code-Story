// Decompiled with JetBrains decompiler
// Type: SRPG.SkillMotionParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class SkillMotionParam
  {
    private string mIname;
    private List<SkillMotionDataParam> mDataList;

    public string Iname => this.mIname;

    public void Deserialize(JSON_SkillMotionParam json)
    {
      if (json == null)
        return;
      this.mIname = json.iname;
      if (json.datas == null)
        return;
      this.mDataList = new List<SkillMotionDataParam>(json.datas.Length);
      for (int index = 0; index < json.datas.Length; ++index)
      {
        SkillMotionDataParam skillMotionDataParam = new SkillMotionDataParam();
        skillMotionDataParam.Deserialize(json.datas[index]);
        this.mDataList.Add(skillMotionDataParam);
      }
    }

    public SkillMotion GetSkillMotion(string unit_id)
    {
      if (this.mDataList == null)
        return (SkillMotion) null;
      SkillMotionDataParam smdp = this.mDataList.Find((Predicate<SkillMotionDataParam>) (smd => smd.UnitList.Contains(unit_id)));
      return smdp == null ? (SkillMotion) null : new SkillMotion(smdp);
    }

    public SkillMotion GetSkillMotion(string unit_id, string jobset_id)
    {
      if (this.mDataList == null)
        return (SkillMotion) null;
      SkillMotionDataParam smdp = !string.IsNullOrEmpty(jobset_id) ? this.mDataList.Find((Predicate<SkillMotionDataParam>) (smd => smd.JobList != null && smd.UnitList.Contains(unit_id) && smd.JobList.Contains(jobset_id))) : this.mDataList.Find((Predicate<SkillMotionDataParam>) (smd => smd.UnitList.Contains(unit_id)));
      return smdp == null ? (SkillMotion) null : new SkillMotion(smdp);
    }

    public List<string> GetMotionList()
    {
      List<string> motionList = new List<string>();
      if (this.mDataList != null)
      {
        for (int index = 0; index < this.mDataList.Count; ++index)
        {
          SkillMotionDataParam mData = this.mDataList[index];
          if (!string.IsNullOrEmpty(mData.MotionId))
            motionList.Add(mData.MotionId);
        }
      }
      return motionList;
    }

    public List<string> GetEffectList()
    {
      List<string> effectList = new List<string>();
      if (this.mDataList != null)
      {
        for (int index = 0; index < this.mDataList.Count; ++index)
        {
          SkillMotionDataParam mData = this.mDataList[index];
          if (!string.IsNullOrEmpty(mData.EffectId))
            effectList.Add(mData.EffectId);
        }
      }
      return effectList;
    }

    public bool CheckPassCondition(UnitData self)
    {
      JobSetParam jobSetParam2 = self.GetJobSetParam2(self.CurrentJobId);
      return this.GetSkillMotion(self.UnitID, jobSetParam2.iname) != null;
    }
  }
}
