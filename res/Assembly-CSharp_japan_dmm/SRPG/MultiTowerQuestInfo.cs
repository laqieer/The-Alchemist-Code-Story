// Decompiled with JetBrains decompiler
// Type: SRPG.MultiTowerQuestInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class MultiTowerQuestInfo : MonoBehaviour
  {
    [SerializeField]
    private GameObject EnemyTemplate;
    [SerializeField]
    private GameObject EnemyRoot;
    [SerializeField]
    private Text QuestTitle;
    [SerializeField]
    private Text RecommendLv;
    [SerializeField]
    private GameObject DetailObject;
    [SerializeField]
    private GameObject RewardTemplate;
    [SerializeField]
    private GameObject RewardRoot;
    [SerializeField]
    private MultiTowerQuestInfo.eRewardShowType RewardShowType;
    private GameObject Detail;
    private List<GameObject> mEnemyObject = new List<GameObject>();
    private List<GameObject> mRewardObject = new List<GameObject>();

    public void Refresh()
    {
      GameUtility.SetGameObjectActive(this.RewardTemplate, false);
      MultiTowerFloorParam dataOfClass = DataSource.FindDataOfClass<MultiTowerFloorParam>(((Component) this).gameObject, (MultiTowerFloorParam) null);
      if (dataOfClass == null)
        return;
      this.SetEnemy(dataOfClass);
      if (Object.op_Inequality((Object) this.QuestTitle, (Object) null))
        this.QuestTitle.text = dataOfClass.title + " " + dataOfClass.name;
      if (Object.op_Inequality((Object) this.RecommendLv, (Object) null))
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
      if (jsonObject == null || !Object.op_Inequality((Object) this.EnemyTemplate, (Object) null))
        return;
      for (int index2 = 0; index2 < jsonObject.enemy.Length; ++index2)
      {
        NPCSetting setting = new NPCSetting(jsonObject.enemy[index2]);
        Unit data = new Unit();
        if (data != null && data.Setup(setting: (UnitSetting) setting) && !data.IsGimmick)
        {
          GameObject root;
          if (index1 + 1 > this.mEnemyObject.Count)
          {
            root = Object.Instantiate<GameObject>(this.EnemyTemplate);
            if (!Object.op_Equality((Object) root, (Object) null))
              this.mEnemyObject.Add(root);
            else
              continue;
          }
          else
            root = this.mEnemyObject[index1];
          DataSource.Bind<Unit>(root, data);
          GameParameter.UpdateAll(root);
          if (Object.op_Inequality((Object) this.EnemyRoot, (Object) null))
            root.transform.SetParent(this.EnemyRoot.transform, false);
          root.SetActive(true);
          ++index1;
        }
      }
      for (int index3 = index1; index3 < this.mEnemyObject.Count; ++index3)
        this.mEnemyObject[index3].SetActive(false);
      this.EnemyTemplate.SetActive(false);
    }

    private void SetReward(MultiTowerFloorParam param)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      List<MultiTowerRewardItem> mtFloorReward = instance.GetMTFloorReward(param.reward_id, instance.GetMTRound((int) param.floor));
      if (mtFloorReward == null || Object.op_Equality((Object) this.RewardTemplate, (Object) null))
        return;
      for (int index = this.mRewardObject.Count - 1; index >= 0; --index)
        Object.Destroy((Object) this.mRewardObject[index]);
      if (this.RewardShowType == MultiTowerQuestInfo.eRewardShowType.TopOnly)
      {
        MultiTowerRewardItem reward = mtFloorReward.Count <= 0 ? (MultiTowerRewardItem) null : mtFloorReward[0];
        if (!Object.op_Inequality((Object) this.RewardTemplate, (Object) null) || reward == null)
          return;
        this.RewardTemplate.SetActive(true);
        this.BindData(this.RewardTemplate, reward);
      }
      else
      {
        if (this.RewardShowType != MultiTowerQuestInfo.eRewardShowType.All)
          return;
        for (int index = 0; index < mtFloorReward.Count; ++index)
        {
          GameObject rewardObject = this.CreateRewardObject();
          if (!Object.op_Equality((Object) rewardObject, (Object) null))
          {
            this.mRewardObject.Add(rewardObject);
            this.BindData(rewardObject, mtFloorReward[index]);
          }
        }
      }
    }

    private GameObject CreateRewardObject()
    {
      if (Object.op_Equality((Object) this.RewardTemplate, (Object) null))
        return (GameObject) null;
      if (Object.op_Equality((Object) this.RewardRoot, (Object) null))
      {
        DebugUtility.LogError("CreateRewardObject -> RewardRoot == null");
        return (GameObject) null;
      }
      GameObject rewardObject = Object.Instantiate<GameObject>(this.RewardTemplate);
      rewardObject.transform.SetParent(this.RewardRoot.transform, false);
      rewardObject.SetActive(true);
      return rewardObject;
    }

    private void BindData(GameObject obj, MultiTowerRewardItem reward)
    {
      DataSource.Bind<MultiTowerRewardItem>(obj, reward);
      MultiTowerRewardInfo component = obj.GetComponent<MultiTowerRewardInfo>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      component.Refresh();
    }

    public enum eRewardShowType
    {
      TopOnly,
      All,
    }
  }
}
