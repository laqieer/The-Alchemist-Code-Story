// Decompiled with JetBrains decompiler
// Type: SRPG.ArtifactJobList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class ArtifactJobList : MonoBehaviour, IGameParameter
  {
    public GameObject ListItem;
    public GameObject AnyJob;
    private List<JobParam> mCurrentJobs = new List<JobParam>(8);
    private List<GameObject> mJobListItems = new List<GameObject>(8);

    private void Start()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ListItem, (UnityEngine.Object) null))
        this.ListItem.SetActive(false);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AnyJob, (UnityEngine.Object) null))
        return;
      this.AnyJob.SetActive(false);
    }

    private void OnDestroy() => GlobalVars.ConditionJobs = (string[]) null;

    public void UpdateValue()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ListItem, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.AnyJob, (UnityEngine.Object) null))
        return;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ListItem, (UnityEngine.Object) null))
        this.ListItem.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AnyJob, (UnityEngine.Object) null))
        this.AnyJob.SetActive(false);
      string[] conditionJobs = GlobalVars.ConditionJobs;
      MasterParam masterParam = MonoSingleton<GameManager>.Instance.MasterParam;
      int index1 = 0;
      if (conditionJobs != null)
      {
        for (int index2 = 0; index2 < conditionJobs.Length; ++index2)
        {
          if (!string.IsNullOrEmpty(conditionJobs[index2]))
          {
            JobParam jobParam;
            try
            {
              jobParam = masterParam.GetJobParam(conditionJobs[index2]);
            }
            catch (Exception ex)
            {
              continue;
            }
            if (jobParam != null)
            {
              bool flag = false;
              for (int index3 = 0; index3 < this.mCurrentJobs.Count; ++index3)
              {
                if (this.mCurrentJobs[index3].name == jobParam.name && AssetPath.JobIconSmall(this.mCurrentJobs[index3]) == AssetPath.JobIconSmall(jobParam))
                  flag = true;
              }
              if (!flag)
              {
                if (this.mJobListItems.Count <= index1)
                {
                  GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ListItem);
                  gameObject.transform.SetParent(((Component) this).transform, false);
                  this.mJobListItems.Add(gameObject);
                  this.mCurrentJobs.Add((JobParam) null);
                }
                this.mJobListItems[index1].SetActive(true);
                if (this.mCurrentJobs[index1] == jobParam)
                {
                  ++index1;
                }
                else
                {
                  this.mCurrentJobs[index1] = jobParam;
                  ArtifactJobItem component = this.mJobListItems[index1].GetComponent<ArtifactJobItem>();
                  DataSource.Bind<JobParam>(component.jobIcon, jobParam);
                  component.jobName.text = jobParam.name;
                  ++index1;
                }
              }
            }
          }
        }
      }
      for (int index4 = index1; index4 < this.mJobListItems.Count; ++index4)
        this.mJobListItems[index4].SetActive(false);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AnyJob, (UnityEngine.Object) null))
        return;
      this.AnyJob.SetActive(index1 == 0);
    }
  }
}
