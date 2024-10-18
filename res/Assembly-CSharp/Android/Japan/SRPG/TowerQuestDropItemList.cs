// Decompiled with JetBrains decompiler
// Type: SRPG.TowerQuestDropItemList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class TowerQuestDropItemList : QuestDropItemList
  {
    protected override void Refresh()
    {
      if ((UnityEngine.Object) this.ItemTemplate == (UnityEngine.Object) null)
        return;
      for (int index = this.mItems.Count - 1; index >= 0; --index)
        UnityEngine.Object.Destroy((UnityEngine.Object) this.mItems[index]);
      QuestParam dataOfClass = DataSource.FindDataOfClass<QuestParam>(this.gameObject, (QuestParam) null);
      if (dataOfClass == null)
        return;
      List<TowerRewardItem> towerRewardItem = MonoSingleton<GameManager>.Instance.FindTowerReward(MonoSingleton<GameManager>.Instance.FindTowerFloor(dataOfClass.iname).reward_id).GetTowerRewardItem();
      for (int rewardIndex = 0; rewardIndex < towerRewardItem.Count; ++rewardIndex)
      {
        TowerRewardItem reward = towerRewardItem[rewardIndex];
        if (reward != null && !reward.IsDisableReward)
        {
          GameObject rewardObject = this.CreateRewardObject();
          if (!((UnityEngine.Object) rewardObject == (UnityEngine.Object) null))
          {
            this.mItems.Add(rewardObject);
            this.BindData(rewardObject, reward, rewardIndex);
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
      if ((UnityEngine.Object) this.ItemTemplate == (UnityEngine.Object) null)
        return (GameObject) null;
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ItemTemplate);
      gameObject.transform.SetParent(this.transform, false);
      gameObject.SetActive(true);
      return gameObject;
    }

    private void BindData(GameObject obj, TowerRewardItem reward, int rewardIndex)
    {
      DataSource.Bind<TowerRewardItem>(obj, reward, false);
      foreach (GameParameter componentsInChild in obj.GetComponentsInChildren<GameParameter>())
        componentsInChild.Index = rewardIndex;
      TowerRewardUI componentInChildren1 = obj.GetComponentInChildren<TowerRewardUI>();
      if ((UnityEngine.Object) componentInChildren1 != (UnityEngine.Object) null)
        componentInChildren1.Refresh();
      if (reward.type != TowerRewardItem.RewardType.Artifact)
        return;
      ArtifactIcon componentInChildren2 = obj.GetComponentInChildren<ArtifactIcon>();
      if ((UnityEngine.Object) componentInChildren2 == (UnityEngine.Object) null)
        return;
      ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(reward.iname);
      DataSource.Bind<ArtifactParam>(obj, artifactParam, false);
      componentInChildren2.enabled = true;
      componentInChildren2.UpdateValue();
      if (MonoSingleton<GameManager>.Instance.Player.Artifacts.Find((Predicate<ArtifactData>) (x => x.ArtifactParam.iname == reward.iname)) == null)
        reward.is_new = true;
      else
        reward.is_new = false;
    }
  }
}
