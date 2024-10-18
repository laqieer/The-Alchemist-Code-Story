// Decompiled with JetBrains decompiler
// Type: SRPG.MultiTowerQuestInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class MultiTowerQuestInfo : MonoBehaviour
  {
    private List<GameObject> mEnemyObject = new List<GameObject>();
    private List<GameObject> mRewardObject = new List<GameObject>();
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

    public void Refresh()
    {
      GameUtility.SetGameObjectActive(this.RewardTemplate, false);
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
          DataSource.Bind<Unit>(root, data, false);
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
      for (int index = this.mRewardObject.Count - 1; index >= 0; --index)
        UnityEngine.Object.Destroy((UnityEngine.Object) this.mRewardObject[index]);
      if (this.RewardShowType == MultiTowerQuestInfo.eRewardShowType.TopOnly)
      {
        MultiTowerRewardItem reward = mtFloorReward.Count <= 0 ? (MultiTowerRewardItem) null : mtFloorReward[0];
        if (!((UnityEngine.Object) this.RewardTemplate != (UnityEngine.Object) null) || reward == null)
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
          if (!((UnityEngine.Object) rewardObject == (UnityEngine.Object) null))
          {
            this.mRewardObject.Add(rewardObject);
            this.BindData(rewardObject, mtFloorReward[index]);
          }
        }
      }
    }

    private GameObject CreateRewardObject()
    {
      if ((UnityEngine.Object) this.RewardTemplate == (UnityEngine.Object) null)
        return (GameObject) null;
      if ((UnityEngine.Object) this.RewardRoot == (UnityEngine.Object) null)
      {
        DebugUtility.LogError("CreateRewardObject -> RewardRoot == null");
        return (GameObject) null;
      }
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.RewardTemplate);
      gameObject.transform.SetParent(this.RewardRoot.transform, false);
      gameObject.SetActive(true);
      return gameObject;
    }

    private void BindData(GameObject obj, MultiTowerRewardItem reward)
    {
      DataSource.Bind<MultiTowerRewardItem>(obj, reward, false);
      MultiTowerRewardInfo component = obj.GetComponent<MultiTowerRewardInfo>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
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
