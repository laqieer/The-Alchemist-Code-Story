// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardArtifactDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  public class ConceptCardArtifactDetail : MonoBehaviour
  {
    private ArtifactData UnlockArtifact;
    [SerializeField]
    private StatusList mArtifactStatus;
    [SerializeField]
    private GameObject mAbilityListItem;
    [SerializeField]
    private Animator mAbilityAnimator;
    [SerializeField]
    private string mAbilityListItemState;
    public GameObject DetailWindow;
    private GameObject mDetailWindow;

    private void Start()
    {
      this.Refresh();
    }

    private void Refresh()
    {
      ArtifactData artifactData = DataSource.FindDataOfClass<ArtifactData>(this.gameObject, (ArtifactData) null);
      if (artifactData == null)
      {
        ArtifactParam dataOfClass = DataSource.FindDataOfClass<ArtifactParam>(this.gameObject, (ArtifactParam) null);
        artifactData = new ArtifactData();
        artifactData.Deserialize(new Json_Artifact()
        {
          iname = dataOfClass.iname,
          rare = dataOfClass.rareini
        });
      }
      this.UnlockArtifact = artifactData;
      DataSource.Bind<ArtifactData>(this.gameObject, this.UnlockArtifact, false);
      DataSource.Bind<ArtifactParam>(this.gameObject, this.UnlockArtifact.ArtifactParam, false);
      if ((UnityEngine.Object) this.mAbilityListItem != (UnityEngine.Object) null)
      {
        MasterParam masterParam = MonoSingleton<GameManager>.Instance.MasterParam;
        GameObject mAbilityListItem = this.mAbilityListItem;
        bool flag = false;
        ArtifactParam artifactParam = artifactData.ArtifactParam;
        List<AbilityData> learningAbilities = artifactData.LearningAbilities;
        if (artifactParam.abil_inames != null)
        {
          AbilityParam data1 = (AbilityParam) null;
          string abil_iname = (string) null;
          for (int index = 0; index < artifactParam.abil_inames.Length; ++index)
          {
            if (!string.IsNullOrEmpty(artifactParam.abil_inames[index]) && artifactParam.abil_shows[index] != 0)
            {
              abil_iname = artifactParam.abil_inames[index];
              data1 = masterParam.GetAbilityParam(artifactParam.abil_inames[index]);
              if (data1 != null)
                break;
            }
          }
          if (data1 == null)
          {
            this.mAbilityAnimator.SetInteger(this.mAbilityListItemState, 0);
            DataSource.Bind<AbilityParam>(this.mAbilityListItem, (AbilityParam) null, false);
            DataSource.Bind<AbilityData>(this.mAbilityListItem, (AbilityData) null, false);
            return;
          }
          DataSource.Bind<AbilityParam>(this.mAbilityListItem, data1, false);
          DataSource.Bind<AbilityData>(mAbilityListItem, (AbilityData) null, false);
          if (learningAbilities != null && learningAbilities != null)
          {
            AbilityData data2 = learningAbilities.Find((Predicate<AbilityData>) (x => x.Param.iname == abil_iname));
            if (data2 != null)
            {
              DataSource.Bind<AbilityData>(mAbilityListItem, data2, false);
              flag = true;
            }
          }
        }
        if (flag)
          this.mAbilityAnimator.SetInteger(this.mAbilityListItemState, 2);
        else
          this.mAbilityAnimator.SetInteger(this.mAbilityListItemState, 0);
      }
      BaseStatus fixed_status = new BaseStatus();
      BaseStatus scale_status = new BaseStatus();
      this.UnlockArtifact.GetHomePassiveBuffStatus(ref fixed_status, ref scale_status, (UnitData) null, 0, true);
      this.mArtifactStatus.SetValues(fixed_status, scale_status, false);
      GameParameter.UpdateAll(this.gameObject);
    }

    public void SetArtifactData()
    {
      ArtifactData dataOfClass = DataSource.FindDataOfClass<ArtifactData>(this.gameObject, (ArtifactData) null);
      if (dataOfClass == null)
        return;
      GlobalVars.ConditionJobs = dataOfClass.ArtifactParam.condition_jobs;
    }

    public void ShowDetail()
    {
      if (this.UnlockArtifact == null || (UnityEngine.Object) this.DetailWindow == (UnityEngine.Object) null)
        return;
      this.StartCoroutine(this.ShowDetailAsync());
    }

    [DebuggerHidden]
    private IEnumerator ShowDetailAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ConceptCardArtifactDetail.\u003CShowDetailAsync\u003Ec__Iterator0() { \u0024this = this };
    }

    private enum AbilityAnimatorType
    {
      Hidden,
      Locked,
      Unlocked,
    }
  }
}
