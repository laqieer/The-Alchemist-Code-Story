// Decompiled with JetBrains decompiler
// Type: SRPG.ArtifactGetUnlockWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(101, "Selected Quest", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "Unlock", FlowNode.PinTypes.Output, 100)]
  public class ArtifactGetUnlockWindow : MonoBehaviour, IFlowInterface
  {
    private ArtifactData UnlockArtifact;
    public StatusList ArtifactStatus;
    public GameObject AbilityListItem;
    public float ability_unlock_alpha;
    public float ability_hidden_alpha;
    public GameObject lock_object;

    private void Start()
    {
    }

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.Refresh();
    }

    private void Refresh()
    {
      ArtifactData artifactData = new ArtifactData();
      artifactData.Deserialize(new Json_Artifact()
      {
        iname = GlobalVars.ArtifactListItem.param.iname,
        rare = GlobalVars.ArtifactListItem.param.rareini
      });
      this.UnlockArtifact = artifactData;
      DataSource.Bind<ArtifactData>(this.gameObject, this.UnlockArtifact);
      DataSource.Bind<ArtifactParam>(this.gameObject, this.UnlockArtifact.ArtifactParam);
      if ((UnityEngine.Object) this.AbilityListItem != (UnityEngine.Object) null)
      {
        MasterParam masterParam = MonoSingleton<GameManager>.Instance.MasterParam;
        GameObject abilityListItem = this.AbilityListItem;
        CanvasGroup component = abilityListItem.GetComponent<CanvasGroup>();
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
            component.alpha = this.ability_hidden_alpha;
            DataSource.Bind<AbilityParam>(this.AbilityListItem, (AbilityParam) null);
            DataSource.Bind<AbilityData>(this.AbilityListItem, (AbilityData) null);
            return;
          }
          DataSource.Bind<AbilityParam>(this.AbilityListItem, data1);
          DataSource.Bind<AbilityData>(abilityListItem, (AbilityData) null);
          if ((UnityEngine.Object) component != (UnityEngine.Object) null && learningAbilities != null && learningAbilities != null)
          {
            AbilityData data2 = learningAbilities.Find((Predicate<AbilityData>) (x => x.Param.iname == abil_iname));
            if (data2 != null)
            {
              DataSource.Bind<AbilityData>(abilityListItem, data2);
              flag = true;
            }
          }
        }
        component.alpha = !flag ? this.ability_hidden_alpha : this.ability_unlock_alpha;
      }
      BaseStatus fixed_status = new BaseStatus();
      BaseStatus scale_status = new BaseStatus();
      this.UnlockArtifact.GetHomePassiveBuffStatus(ref fixed_status, ref scale_status, (UnitData) null, 0, true);
      this.ArtifactStatus.SetValues(fixed_status, scale_status);
      GameParameter.UpdateAll(this.gameObject);
    }

    public void SetArtifactData()
    {
      GlobalVars.ConditionJobs = GlobalVars.ArtifactListItem.param.condition_jobs;
    }
  }
}
