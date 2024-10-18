// Decompiled with JetBrains decompiler
// Type: SRPG.UnitJobClassChange
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [AddComponentMenu("UI/UnitJobClassChange")]
  public class UnitJobClassChange : MonoBehaviour
  {
    public string PrevJobID;
    public string NextJobID;
    public GameObject PrevJob;
    public GameObject NextJob;
    [NonSerialized]
    public int CurrentRank;
    private int mCurrentJobRank;
    private int mNextJobRank;
    public Text NewRank;
    public Text OldRank;

    public int CurrentJobRank
    {
      get
      {
        return this.mCurrentJobRank;
      }
      set
      {
        this.mCurrentJobRank = value;
      }
    }

    public int NextJobRank
    {
      get
      {
        return this.mNextJobRank;
      }
      set
      {
        this.mNextJobRank = value;
      }
    }

    private void Start()
    {
      JobParam data1 = (JobParam) null;
      JobParam data2 = (JobParam) null;
      if (string.IsNullOrEmpty(this.PrevJobID) && string.IsNullOrEmpty(this.NextJobID))
      {
        UnitData unitDataByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedUnitUniqueID);
        if (unitDataByUniqueId == null)
          return;
        JobData jobData = (JobData) null;
        for (int index = 0; index < unitDataByUniqueId.Jobs.Length; ++index)
        {
          if (unitDataByUniqueId.Jobs[index] != null && unitDataByUniqueId.Jobs[index].UniqueID == (long) GlobalVars.SelectedJobUniqueID)
            jobData = unitDataByUniqueId.Jobs[index];
        }
        if (jobData == null)
          return;
        JobData baseJob = unitDataByUniqueId.GetBaseJob(jobData.JobID);
        if (baseJob != null)
          data1 = baseJob.Param;
        data2 = jobData.Param;
      }
      if (!string.IsNullOrEmpty(this.PrevJobID))
        data1 = MonoSingleton<GameManager>.Instance.MasterParam.GetJobParam(this.PrevJobID);
      if (!string.IsNullOrEmpty(this.NextJobID))
        data2 = MonoSingleton<GameManager>.Instance.MasterParam.GetJobParam(this.NextJobID);
      if ((UnityEngine.Object) this.NextJob != (UnityEngine.Object) null)
      {
        DataSource.Bind<JobParam>(this.NextJob, data2, false);
        GameParameter.UpdateAll(this.NextJob);
      }
      if ((UnityEngine.Object) this.PrevJob != (UnityEngine.Object) null)
      {
        DataSource.Bind<JobParam>(this.PrevJob, data1, false);
        GameParameter.UpdateAll(this.PrevJob);
      }
      if ((UnityEngine.Object) this.NewRank != (UnityEngine.Object) null)
        this.NewRank.text = this.NextJobRank.ToString();
      if (!((UnityEngine.Object) this.OldRank != (UnityEngine.Object) null))
        return;
      this.OldRank.text = this.CurrentJobRank.ToString();
    }
  }
}
