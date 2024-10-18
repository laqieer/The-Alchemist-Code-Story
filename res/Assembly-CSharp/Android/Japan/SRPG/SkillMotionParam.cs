// Decompiled with JetBrains decompiler
// Type: SRPG.SkillMotionParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace SRPG
{
  public class SkillMotionParam
  {
    private string mIname;
    private List<SkillMotionDataParam> mDataList;

    public string Iname
    {
      get
      {
        return this.mIname;
      }
    }

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
      if (smdp == null)
        return (SkillMotion) null;
      return new SkillMotion(smdp);
    }

    public SkillMotion GetSkillMotion(string unit_id, string jobset_id)
    {
      if (this.mDataList == null)
        return (SkillMotion) null;
      SkillMotionDataParam smdp = !string.IsNullOrEmpty(jobset_id) ? this.mDataList.Find((Predicate<SkillMotionDataParam>) (smd =>
      {
        if (smd.JobList == null || !smd.UnitList.Contains(unit_id))
          return false;
        return smd.JobList.Contains(jobset_id);
      })) : this.mDataList.Find((Predicate<SkillMotionDataParam>) (smd => smd.UnitList.Contains(unit_id)));
      if (smdp == null)
        return (SkillMotion) null;
      return new SkillMotion(smdp);
    }

    public List<string> GetMotionList()
    {
      List<string> stringList = new List<string>();
      if (this.mDataList != null)
      {
        for (int index = 0; index < this.mDataList.Count; ++index)
        {
          SkillMotionDataParam mData = this.mDataList[index];
          if (!string.IsNullOrEmpty(mData.MotionId))
            stringList.Add(mData.MotionId);
        }
      }
      return stringList;
    }

    public bool CheckPassCondition(UnitData self)
    {
      JobSetParam jobSetParam2 = self.GetJobSetParam2(self.CurrentJobId);
      return this.GetSkillMotion(self.UnitID, jobSetParam2.iname) != null;
    }
  }
}
