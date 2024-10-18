// Decompiled with JetBrains decompiler
// Type: SRPG.MultiTowerQuestInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class MultiTowerQuestInfo : MonoBehaviour
  {
    private List<GameObject> mEnemyObject = new List<GameObject>();
    public GameObject EnemyTemplate;
    public GameObject EnemyRoot;
    public Text QuestTitle;
    public Text RecommendLv;
    public GameObject DetailObject;
    public GameObject RewardTemplate;
    public GameObject RewardRoot;
    private GameObject Detail;

    private void Start()
    {
    }

    public void Refresh()
    {
      MultiTowerFloorParam dataOfClass = DataSource.FindDataOfClass<MultiTowerFloorParam>(this.gameObject, (MultiTowerFloorParam) null);
      if (dataOfClass == null)
        return;
      this.SetEnemy(dataOfClass);
      if ((UnityEngine.Object) this.QuestTitle != (UnityEngine.Object) null)
        this.QuestTitle.text = dataOfClass.title + " " + dataOfClass.name;
      if ((UnityEngine.Object) this.RecommendLv != (UnityEngine.Object) null)
        this.RecommendLv.text = string.Format(LocalizedText.Get("sys.MULTI_TOWER_RECOMMEND"), (object) dataOfClass.lv, (object) dataOfClass.joblv);
      this.SetReward(dataOfClass);
    }

    private void SetEnemy(MultiTowerFloorParam param)
    {
      int index1 = 0;
      if (param.map == null)
        return;
      string src = AssetManager.LoadTextData(AssetPath.LocalMap(param.map[0].mapSetName));
      if (src == null)
        return;
      JSON_MapUnit jsonObject = JSONParser.parseJSONObject<JSON_MapUnit>(src);
      if (jsonObject == null || !((UnityEngine.Object) this.EnemyTemplate != (UnityEngine.Object) null))
        return;
      for (int index2 = 0; index2 < jsonObject.enemy.Length; ++index2)
      {
        NPCSetting npcSetting = new NPCSetting(jsonObject.enemy[index2]);
        Unit data = new Unit();
        if (data != null && data.Setup((UnitData) null, (UnitSetting) npcSetting, (Unit.DropItem) null, (Unit.DropItem) null) && !data.IsGimmick)
        {
          GameObject root;
          if (index1 + 1 > this.mEnemyObject.Count)
          {
            root = UnityEngine.Object.Instantiate<GameObject>(this.EnemyTemplate);
            if (!((UnityEngine.Object) root == (UnityEngine.Object) null))
              this.mEnemyObject.Add(root);
            else
              continue;
          }
          else
            root = this.mEnemyObject[index1];
          DataSource.Bind<Unit>(root, data);
          GameParameter.UpdateAll(root);
          if ((UnityEngine.Object) this.EnemyRoot != (UnityEngine.Object) null)
            root.transform.SetParent(this.EnemyRoot.transform, false);
          root.SetActive(true);
          ++index1;
        }
      }
      for (int index2 = index1; index2 < this.mEnemyObject.Count; ++index2)
        this.mEnemyObject[index2].SetActive(false);
      this.EnemyTemplate.SetActive(false);
    }

    private void SetReward(MultiTowerFloorParam param)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      List<MultiTowerRewardItem> mtFloorReward = instance.GetMTFloorReward(param.reward_id, instance.GetMTRound((int) param.floor));
      if (mtFloorReward == null || (UnityEngine.Object) this.RewardTemplate == (UnityEngine.Object) null)
        return;
      MultiTowerRewardItem data = mtFloorReward.Count <= 0 ? (MultiTowerRewardItem) null : mtFloorReward[0];
      if (!((UnityEngine.Object) this.RewardTemplate != (UnityEngine.Object) null))
        return;
      DataSource.Bind<MultiTowerRewardItem>(this.RewardTemplate, data);
      MultiTowerRewardInfo component = this.RewardTemplate.GetComponent<MultiTowerRewardInfo>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      component.Refresh();
    }
  }
}
