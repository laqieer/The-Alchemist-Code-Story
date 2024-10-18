// Decompiled with JetBrains decompiler
// Type: SRPG.TowerQuestDropItemList
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
  public class TowerQuestDropItemList : QuestDropItemList
  {
    protected override void Refresh()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ItemTemplate, (UnityEngine.Object) null))
        return;
      for (int index = this.mItems.Count - 1; index >= 0; --index)
        UnityEngine.Object.Destroy((UnityEngine.Object) this.mItems[index]);
      QuestParam dataOfClass = DataSource.FindDataOfClass<QuestParam>(((Component) this).gameObject, (QuestParam) null);
      if (dataOfClass == null)
        return;
      List<TowerRewardItem> towerRewardItem = MonoSingleton<GameManager>.Instance.FindTowerReward(MonoSingleton<GameManager>.Instance.FindTowerFloor(dataOfClass.iname).reward_id).GetTowerRewardItem();
      for (int index = 0; index < towerRewardItem.Count; ++index)
      {
        TowerRewardItem reward = towerRewardItem[index];
        if (reward != null && !reward.IsDisableReward)
        {
          GameObject rewardObject = this.CreateRewardObject();
          if (!UnityEngine.Object.op_Equality((UnityEngine.Object) rewardObject, (UnityEngine.Object) null))
          {
            this.mItems.Add(rewardObject);
            this.BindData(rewardObject, reward, index);
          }
        }
      }
    }

    private bool IsDisableReward(TowerRewardItem reward)
    {
      return reward == null || !reward.visible || reward.type == TowerRewardItem.RewardType.Gold;
    }

    private GameObject CreateRewardObject()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ItemTemplate, (UnityEngine.Object) null))
        return (GameObject) null;
      GameObject rewardObject = UnityEngine.Object.Instantiate<GameObject>(this.ItemTemplate);
      rewardObject.transform.SetParent(((Component) this).transform, false);
      rewardObject.SetActive(true);
      return rewardObject;
    }

    private void BindData(GameObject obj, TowerRewardItem reward, int rewardIndex)
    {
      DataSource.Bind<TowerRewardItem>(obj, reward);
      foreach (GameParameter componentsInChild in obj.GetComponentsInChildren<GameParameter>())
        componentsInChild.Index = rewardIndex;
      TowerRewardUI componentInChildren1 = obj.GetComponentInChildren<TowerRewardUI>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren1, (UnityEngine.Object) null))
        componentInChildren1.Refresh();
      if (reward.type != TowerRewardItem.RewardType.Artifact)
        return;
      ArtifactIcon componentInChildren2 = obj.GetComponentInChildren<ArtifactIcon>();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) componentInChildren2, (UnityEngine.Object) null))
        return;
      ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(reward.iname);
      DataSource.Bind<ArtifactParam>(obj, artifactParam);
      ((Behaviour) componentInChildren2).enabled = true;
      componentInChildren2.UpdateValue();
      if (MonoSingleton<GameManager>.Instance.Player.Artifacts.Find((Predicate<ArtifactData>) (x => x.ArtifactParam.iname == reward.iname)) == null)
        reward.is_new = true;
      else
        reward.is_new = false;
    }
  }
}
