// Decompiled with JetBrains decompiler
// Type: SRPG.TowerQuestDropItemList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
      Transform transform = this.transform;
      List<TowerRewardItem> towerRewardItem = MonoSingleton<GameManager>.Instance.FindTowerReward(MonoSingleton<GameManager>.Instance.FindTowerFloor(dataOfClass.iname).reward_id).GetTowerRewardItem();
      for (int index = 0; index < towerRewardItem.Count; ++index)
      {
        TowerRewardItem item = towerRewardItem[index];
        if (item.visible && item.type != TowerRewardItem.RewardType.Gold)
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ItemTemplate);
          gameObject.transform.SetParent(transform, false);
          gameObject.transform.localScale = this.ItemTemplate.transform.localScale;
          DataSource.Bind<TowerRewardItem>(gameObject, item);
          gameObject.SetActive(true);
          foreach (GameParameter componentsInChild in gameObject.GetComponentsInChildren<GameParameter>())
            componentsInChild.Index = index;
          TowerRewardUI componentInChildren1 = gameObject.GetComponentInChildren<TowerRewardUI>();
          if ((UnityEngine.Object) componentInChildren1 != (UnityEngine.Object) null)
            componentInChildren1.Refresh();
          if (item.type == TowerRewardItem.RewardType.Artifact)
          {
            ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(item.iname);
            DataSource.Bind<ArtifactParam>(gameObject, artifactParam);
            ArtifactIcon componentInChildren2 = gameObject.GetComponentInChildren<ArtifactIcon>();
            if ((UnityEngine.Object) componentInChildren2 == (UnityEngine.Object) null)
              break;
            componentInChildren2.enabled = true;
            componentInChildren2.UpdateValue();
            if (MonoSingleton<GameManager>.Instance.Player.Artifacts.Find((Predicate<ArtifactData>) (x => x.ArtifactParam.iname == item.iname)) != null)
              break;
            item.is_new = true;
            break;
          }
        }
      }
    }
  }
}
