// Decompiled with JetBrains decompiler
// Type: SRPG.SelectArtifactInfo
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
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  public class SelectArtifactInfo : MonoBehaviour, IFlowInterface
  {
    public GameObject AbilityListItem;
    public float ability_unlock_alpha;
    public float ability_hidden_alpha;
    public GameObject lock_object;

    private void Awake()
    {
    }

    private void Start() => this.Refresh();

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.Refresh();
    }

    private void Refresh()
    {
      ArtifactParam data1 = GlobalVars.ArtifactListItem.param;
      ArtifactData data2 = new ArtifactData();
      data2.Deserialize(new Json_Artifact()
      {
        iname = GlobalVars.ArtifactListItem.param.iname,
        rare = GlobalVars.ArtifactListItem.param.rareini
      });
      DataSource.Bind<ArtifactParam>(((Component) this).gameObject, data1);
      DataSource.Bind<ArtifactData>(((Component) this).gameObject, data2);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AbilityListItem, (UnityEngine.Object) null))
      {
        MasterParam masterParam = MonoSingleton<GameManager>.Instance.MasterParam;
        GameObject abilityListItem = this.AbilityListItem;
        CanvasGroup component = abilityListItem.GetComponent<CanvasGroup>();
        bool flag = false;
        ArtifactParam artifactParam = data2.ArtifactParam;
        List<AbilityData> learningAbilities = data2.LearningAbilities;
        if (artifactParam.abil_inames != null)
        {
          AbilityParam data3 = (AbilityParam) null;
          string abil_iname = (string) null;
          for (int index = 0; index < artifactParam.abil_inames.Length; ++index)
          {
            if (!string.IsNullOrEmpty(artifactParam.abil_inames[index]) && artifactParam.abil_shows[index] != 0)
            {
              abil_iname = artifactParam.abil_inames[index];
              data3 = masterParam.GetAbilityParam(artifactParam.abil_inames[index]);
              if (data3 != null)
                break;
            }
          }
          if (data3 == null)
          {
            component.alpha = this.ability_hidden_alpha;
            DataSource.Bind<AbilityParam>(this.AbilityListItem, (AbilityParam) null);
            DataSource.Bind<AbilityData>(this.AbilityListItem, (AbilityData) null);
            return;
          }
          DataSource.Bind<AbilityParam>(this.AbilityListItem, data3);
          DataSource.Bind<AbilityData>(abilityListItem, (AbilityData) null);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          {
            switch (learningAbilities)
            {
              case null:
              case null:
                break;
              default:
                AbilityData data4 = learningAbilities.Find((Predicate<AbilityData>) (x => x.Param.iname == abil_iname));
                if (data4 != null)
                {
                  DataSource.Bind<AbilityData>(abilityListItem, data4);
                  flag = true;
                  break;
                }
                break;
            }
          }
        }
        component.alpha = !flag ? this.ability_hidden_alpha : this.ability_unlock_alpha;
      }
      GameParameter.UpdateAll(((Component) this).gameObject);
    }
  }
}
