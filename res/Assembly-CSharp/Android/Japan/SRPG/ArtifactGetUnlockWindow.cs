// Decompiled with JetBrains decompiler
// Type: SRPG.ArtifactGetUnlockWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "Unlock", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "Selected Quest", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(500, "武具詳細情報セット(in)", FlowNode.PinTypes.Input, 500)]
  [FlowNode.Pin(501, "武具詳細情報セット(out)", FlowNode.PinTypes.Output, 501)]
  public class ArtifactGetUnlockWindow : MonoBehaviour, IFlowInterface
  {
    private const int INPUT_REFRESH = 1;
    private const int INPUT_UNLOCK = 100;
    private const int INPUT_SELECT_QUEST = 101;
    private const int INPUT_ARTIFACT_DETAIL_SET = 500;
    private const int OUTPUT_ARTIFACT_DETAIL_SET = 501;
    private ArtifactData UnlockArtifact;
    public StatusList ArtifactStatus;
    public GameObject AbilityListItem;
    public float ability_unlock_alpha;
    public float ability_hidden_alpha;
    public GameObject lock_object;
    [HeaderBar("▼セット効果確認用のボタン")]
    [SerializeField]
    private Button m_SetEffectsButton;

    private void Start()
    {
    }

    public void Activated(int pinID)
    {
      if (pinID == 1)
        this.Refresh();
      if (pinID != 1)
      {
        if (pinID != 500)
          return;
        this.SetArtifactDetailData();
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 501);
      }
      else
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
      DataSource.Bind<ArtifactData>(this.gameObject, this.UnlockArtifact, false);
      DataSource.Bind<ArtifactParam>(this.gameObject, this.UnlockArtifact.ArtifactParam, false);
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
            DataSource.Bind<AbilityParam>(this.AbilityListItem, (AbilityParam) null, false);
            DataSource.Bind<AbilityData>(this.AbilityListItem, (AbilityData) null, false);
            return;
          }
          DataSource.Bind<AbilityParam>(this.AbilityListItem, data1, false);
          DataSource.Bind<AbilityData>(abilityListItem, (AbilityData) null, false);
          if ((UnityEngine.Object) component != (UnityEngine.Object) null && learningAbilities != null && learningAbilities != null)
          {
            AbilityData data2 = learningAbilities.Find((Predicate<AbilityData>) (x => x.Param.iname == abil_iname));
            if (data2 != null)
            {
              DataSource.Bind<AbilityData>(abilityListItem, data2, false);
              flag = true;
            }
          }
        }
        component.alpha = !flag ? this.ability_hidden_alpha : this.ability_unlock_alpha;
      }
      BaseStatus fixed_status = new BaseStatus();
      BaseStatus scale_status = new BaseStatus();
      this.UnlockArtifact.GetHomePassiveBuffStatus(ref fixed_status, ref scale_status, (UnitData) null, 0, true);
      this.ArtifactStatus.SetValues(fixed_status, scale_status, false);
      if ((UnityEngine.Object) this.m_SetEffectsButton != (UnityEngine.Object) null && this.UnlockArtifact.ArtifactParam != null)
      {
        this.m_SetEffectsButton.interactable = MonoSingleton<GameManager>.Instance.MasterParam.ExistSkillAbilityDeriveDataWithArtifact(this.UnlockArtifact.ArtifactParam.iname);
        if (this.m_SetEffectsButton.interactable)
          ArtifactSetList.SetSelectedArtifactParam(this.UnlockArtifact.ArtifactParam);
      }
      GameParameter.UpdateAll(this.gameObject);
    }

    private void SetArtifactDetailData()
    {
      if (this.UnlockArtifact == null)
        return;
      ArtifactDetailWindow.SetArtifactParam(this.UnlockArtifact.ArtifactParam);
    }

    public void SetArtifactData()
    {
      GlobalVars.ConditionJobs = GlobalVars.ArtifactListItem.param.condition_jobs;
    }
  }
}
