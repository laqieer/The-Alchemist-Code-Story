// Decompiled with JetBrains decompiler
// Type: SRPG.SelectArtifactInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

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

    private void Start()
    {
      this.Refresh();
    }

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
      DataSource.Bind<ArtifactParam>(this.gameObject, data1);
      DataSource.Bind<ArtifactData>(this.gameObject, data2);
      if ((UnityEngine.Object) this.AbilityListItem != (UnityEngine.Object) null)
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
          if ((UnityEngine.Object) component != (UnityEngine.Object) null && learningAbilities != null && learningAbilities != null)
          {
            AbilityData data4 = learningAbilities.Find((Predicate<AbilityData>) (x => x.Param.iname == abil_iname));
            if (data4 != null)
            {
              DataSource.Bind<AbilityData>(abilityListItem, data4);
              flag = true;
            }
          }
        }
        component.alpha = !flag ? this.ability_hidden_alpha : this.ability_unlock_alpha;
      }
      GameParameter.UpdateAll(this.gameObject);
    }
  }
}
