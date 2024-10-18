// Decompiled with JetBrains decompiler
// Type: SRPG.ArtifactJobs
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
  public class ArtifactJobs : MonoBehaviour, IGameParameter
  {
    public GameObject ListItem;
    public GameObject AnyJob;
    private List<JobParam> mCurrentJobs = new List<JobParam>(8);
    private List<GameObject> mJobListItems = new List<GameObject>(8);
    private bool mUpdated;

    private void Start()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ListItem, (UnityEngine.Object) null) && this.ListItem.activeInHierarchy)
        this.ListItem.SetActive(false);
      if (this.mUpdated)
        return;
      this.UpdateValue();
    }

    public void UpdateValue()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ListItem, (UnityEngine.Object) null))
        return;
      ArtifactData dataOfClass = DataSource.FindDataOfClass<ArtifactData>(((Component) this).gameObject, (ArtifactData) null);
      ArtifactParam artifactParam = dataOfClass == null ? DataSource.FindDataOfClass<ArtifactParam>(((Component) this).gameObject, (ArtifactParam) null) : dataOfClass.ArtifactParam;
      if (artifactParam == null)
      {
        for (int index = 0; index < this.mJobListItems.Count; ++index)
          this.mJobListItems[index].SetActive(false);
      }
      else
      {
        Transform transform = ((Component) this).transform;
        MasterParam masterParam = MonoSingleton<GameManager>.Instance.MasterParam;
        string[] conditionJobs = artifactParam.condition_jobs;
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
                if (this.mJobListItems.Count <= index1)
                {
                  GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ListItem);
                  gameObject.transform.SetParent(transform, false);
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
                  DataSource.Bind<JobParam>(this.mJobListItems[index1], jobParam);
                  GameParameter.UpdateAll(this.mJobListItems[index1]);
                  ++index1;
                }
              }
            }
          }
        }
        for (int index3 = index1; index3 < this.mJobListItems.Count; ++index3)
          this.mJobListItems[index3].SetActive(false);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AnyJob, (UnityEngine.Object) null))
          this.AnyJob.SetActive(index1 == 0);
        this.mUpdated = true;
      }
    }
  }
}
