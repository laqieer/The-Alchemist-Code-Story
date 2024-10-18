// Decompiled with JetBrains decompiler
// Type: SRPG.ArtifactJobs
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class ArtifactJobs : MonoBehaviour, IGameParameter
  {
    private List<JobParam> mCurrentJobs = new List<JobParam>(8);
    private List<GameObject> mJobListItems = new List<GameObject>(8);
    public GameObject ListItem;
    public GameObject AnyJob;
    private bool mUpdated;

    private void Start()
    {
      if ((UnityEngine.Object) this.ListItem != (UnityEngine.Object) null && this.ListItem.activeInHierarchy)
        this.ListItem.SetActive(false);
      if (this.mUpdated)
        return;
      this.UpdateValue();
    }

    public void UpdateValue()
    {
      if ((UnityEngine.Object) this.ListItem == (UnityEngine.Object) null)
        return;
      ArtifactData dataOfClass = DataSource.FindDataOfClass<ArtifactData>(this.gameObject, (ArtifactData) null);
      ArtifactParam artifactParam = dataOfClass == null ? DataSource.FindDataOfClass<ArtifactParam>(this.gameObject, (ArtifactParam) null) : dataOfClass.ArtifactParam;
      if (artifactParam == null)
      {
        for (int index = 0; index < this.mJobListItems.Count; ++index)
          this.mJobListItems[index].SetActive(false);
      }
      else
      {
        Transform transform = this.transform;
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
                  DataSource.Bind<JobParam>(this.mJobListItems[index1], jobParam, false);
                  GameParameter.UpdateAll(this.mJobListItems[index1]);
                  ++index1;
                }
              }
            }
          }
        }
        for (int index2 = index1; index2 < this.mJobListItems.Count; ++index2)
          this.mJobListItems[index2].SetActive(false);
        if ((UnityEngine.Object) this.AnyJob != (UnityEngine.Object) null)
          this.AnyJob.SetActive(index1 == 0);
        this.mUpdated = true;
      }
    }
  }
}
