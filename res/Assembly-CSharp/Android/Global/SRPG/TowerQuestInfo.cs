// Decompiled with JetBrains decompiler
// Type: SRPG.TowerQuestInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(0, "更新", FlowNode.PinTypes.Input, 0)]
  public class TowerQuestInfo : MonoBehaviour, IFlowInterface
  {
    private List<TowerEnemyListItem> EnemyList = new List<TowerEnemyListItem>();
    private List<TowerEnemyListItem> UnknownEnemyList = new List<TowerEnemyListItem>();
    private List<Unit> EnemyUnits = new List<Unit>();
    [SerializeField]
    private GameObject EnemiesRoot;
    [SerializeField]
    private GameObject EnemyTemplate;
    [SerializeField]
    private GameObject EnemyTemplateUnKnown;
    [SerializeField]
    private Text RewardText;
    [SerializeField]
    private Text RecommendText;
    [SerializeField]
    private GameObject ItemRoot;
    [SerializeField]
    private GameObject ArtifactRoot;
    [SerializeField]
    private GameObject CoinRoot;
    [SerializeField]
    private GameObject UnkownIcon;
    [SerializeField]
    private GameObject ClearIcon;
    [SerializeField]
    private GameObject DetailtTmplate;
    private GameObject Detail;
    private string FloorID;

    public void Activated(int pinID)
    {
      if (pinID != 0 || !string.IsNullOrEmpty(this.FloorID) && !(this.FloorID != GlobalVars.SelectedQuestID))
        return;
      this.Refresh();
    }

    private void Awake()
    {
      GameUtility.SetGameObjectActive(this.EnemyTemplate, false);
      GameUtility.SetGameObjectActive(this.UnkownIcon, false);
      GameUtility.SetGameObjectActive(this.ClearIcon, false);
    }

    private void SetRecommendText(int lv, int joblv)
    {
      if ((UnityEngine.Object) this.RecommendText == (UnityEngine.Object) null)
        return;
      this.RecommendText.text = LocalizedText.Get("sys.TOWER_RECOMMENDATION_TEXT", (object) lv, (object) joblv);
    }

    private void SetEnemies(JSON_MapEnemyUnit[] json)
    {
      for (int index = 0; index < this.UnknownEnemyList.Count; ++index)
        this.UnknownEnemyList[index].gameObject.SetActive(false);
      List<JSON_MapEnemyUnit> list = ((IEnumerable<JSON_MapEnemyUnit>) json).ToList<JSON_MapEnemyUnit>();
      TowerResuponse towerResuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
      TowerFloorParam towerFloorParam = (TowerFloorParam) null;
      if (towerResuponse != null)
        towerFloorParam = towerResuponse.GetCurrentFloor();
      this.EnemyUnits.Clear();
      int index1 = 0;
      for (int index2 = 0; index2 < list.Count; ++index2)
      {
        NPCSetting npcSetting = new NPCSetting(list[index2]);
        Unit data = new Unit();
        data.Setup((UnitData) null, (UnitSetting) npcSetting, (Unit.DropItem) null, (Unit.DropItem) null);
        if (!data.IsGimmick)
        {
          TowerEnemyListItem towerEnemyListItem;
          if (this.EnemyList.Count <= index1)
          {
            towerEnemyListItem = UnityEngine.Object.Instantiate<GameObject>(this.EnemyTemplate).GetComponent<TowerEnemyListItem>();
            towerEnemyListItem.transform.SetParent(this.EnemiesRoot.transform, false);
            this.EnemyList.Add(towerEnemyListItem);
          }
          else
            towerEnemyListItem = this.EnemyList[index1];
          if (towerFloorParam != null && towerFloorParam.iname == GlobalVars.SelectedQuestID && MonoSingleton<GameManager>.Instance.TowerEnemyUnit != null)
          {
            TowerResuponse.EnemyUnit enemyUnit = MonoSingleton<GameManager>.Instance.TowerEnemyUnit[index2];
            data.Damage((int) data.MaximumStatus.param.hp - enemyUnit.hp, false);
          }
          DataSource.Bind<Unit>(towerEnemyListItem.gameObject, data);
          this.EnemyUnits.Add(data);
          ++index1;
        }
      }
      for (int index2 = 0; index2 < this.EnemyList.Count; ++index2)
      {
        bool flag = index2 < list.Count && index2 < 10;
        this.EnemyList[index2].gameObject.SetActive(flag);
        this.EnemyList[index2].UpdateValue();
      }
    }

    private void SetRewards(TowerRewardParam rewardParam)
    {
      if (rewardParam == null || (UnityEngine.Object) this.RewardText == (UnityEngine.Object) null)
        return;
      GameUtility.SetGameObjectActive(this.ItemRoot, false);
      GameUtility.SetGameObjectActive(this.ArtifactRoot, false);
      GameUtility.SetGameObjectActive(this.CoinRoot, false);
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      using (List<TowerRewardItem>.Enumerator enumerator = rewardParam.GetTowerRewardItem().GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          TowerRewardItem current = enumerator.Current;
          if (current.visible && current.type != TowerRewardItem.RewardType.Gold)
          {
            string str = string.Empty;
            switch (current.type)
            {
              case TowerRewardItem.RewardType.Item:
                ItemParam itemParam = instanceDirect.GetItemParam(current.iname);
                if (itemParam != null)
                  str = itemParam.name;
                DataSource.Bind<ItemParam>(this.ItemRoot, itemParam);
                GameUtility.SetGameObjectActive(this.ItemRoot, true);
                GameParameter.UpdateAll(this.ItemRoot);
                break;
              case TowerRewardItem.RewardType.Coin:
                str = LocalizedText.Get("sys.COIN");
                this.CoinRoot.gameObject.SetActive(true);
                break;
              case TowerRewardItem.RewardType.ArenaCoin:
                str = LocalizedText.Get("sys.ARENA_COIN");
                break;
              case TowerRewardItem.RewardType.MultiCoin:
                str = LocalizedText.Get("sys.MULTI_COIN");
                break;
              case TowerRewardItem.RewardType.KakeraCoin:
                str = LocalizedText.Get("sys.PIECE_POINT");
                break;
              case TowerRewardItem.RewardType.Artifact:
                ArtifactParam artifactParam = instanceDirect.MasterParam.GetArtifactParam(current.iname);
                if (artifactParam != null)
                  str = artifactParam.name;
                DataSource.Bind<ArtifactParam>(this.ArtifactRoot, artifactParam);
                GameUtility.SetGameObjectActive(this.ArtifactRoot, true);
                GameParameter.UpdateAll(this.ArtifactRoot);
                break;
            }
            this.RewardText.text = string.Format("{0} × {1}", (object) str, (object) current.num);
            break;
          }
        }
      }
    }

    public void Refresh()
    {
      TowerFloorParam towerFloor = MonoSingleton<GameManager>.Instance.FindTowerFloor(GlobalVars.SelectedQuestID);
      if (towerFloor == null)
        return;
      QuestParam questParam = towerFloor.GetQuestParam();
      DataSource.Bind<QuestParam>(this.gameObject, questParam);
      this.SetRecommendText((int) towerFloor.lv, (int) towerFloor.joblv);
      int downloadAssetNum = this.GetComponentInParent<FlowNode_DownloadTowerMapSets>().DownloadAssetNum;
      TowerFloorParam currentFloor = MonoSingleton<GameManager>.Instance.TowerResuponse.GetCurrentFloor();
      if (currentFloor == null)
        return;
      if ((int) towerFloor.FloorIndex < (int) currentFloor.FloorIndex + downloadAssetNum)
      {
        if (questParam.state == QuestStates.Cleared)
        {
          GameUtility.SetGameObjectActive(this.UnkownIcon, false);
          GameUtility.SetGameObjectActive((Component) this.RewardText, true);
          GameUtility.SetGameObjectActive(this.ClearIcon, true);
          for (int index = 0; index < this.EnemyList.Count; ++index)
            this.EnemyList[index].gameObject.SetActive(false);
          for (int index = 0; index < this.UnknownEnemyList.Count; ++index)
            this.UnknownEnemyList[index].gameObject.SetActive(false);
          this.SetRewards(MonoSingleton<GameManager>.Instance.FindTowerReward(towerFloor.reward_id));
        }
        else
        {
          string path = AssetPath.LocalMap(towerFloor.map[0].mapSetName);
          string src = AssetManager.LoadTextData(path);
          if (string.IsNullOrEmpty(src))
          {
            DebugUtility.LogError("配置ファイルがありません : QuestIname = " + towerFloor.iname + ",SetFilePath = " + path);
            return;
          }
          JSON_MapUnit jsonObject = JSONParser.parseJSONObject<JSON_MapUnit>(src);
          GameUtility.SetGameObjectActive(this.UnkownIcon, false);
          GameUtility.SetGameObjectActive((Component) this.RewardText, true);
          GameUtility.SetGameObjectActive(this.ClearIcon, false);
          TowerResuponse towerResuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
          if (jsonObject.is_rand > (byte) 0)
          {
            if (towerResuponse.lot_enemies == null || (int) towerFloor.FloorIndex > (int) currentFloor.FloorIndex)
            {
              for (int index = 0; index < this.EnemyList.Count; ++index)
                this.EnemyList[index].gameObject.SetActive(false);
              this.EnemyTemplateUnKnown.SetActive(true);
              int num = 0;
              for (int index = 0; index < towerFloor.rand_tag.Length; ++index)
                num += (int) towerFloor.rand_tag[index];
              for (int index = 0; index < num; ++index)
              {
                if (index >= this.UnknownEnemyList.Count)
                {
                  TowerEnemyListItem component = UnityEngine.Object.Instantiate<GameObject>(this.EnemyTemplateUnKnown).GetComponent<TowerEnemyListItem>();
                  component.transform.SetParent(this.EnemiesRoot.transform, false);
                  this.UnknownEnemyList.Add(component);
                }
                this.UnknownEnemyList[index].gameObject.SetActive(true);
              }
              for (int index = num; index < this.UnknownEnemyList.Count; ++index)
                this.UnknownEnemyList[index].gameObject.SetActive(false);
              this.EnemyTemplateUnKnown.SetActive(false);
            }
            else
            {
              List<JSON_MapEnemyUnit> randFixedUnit = jsonObject.GetRandFixedUnit();
              jsonObject.enemy = new JSON_MapEnemyUnit[towerResuponse.lot_enemies.Length];
              for (int index = 0; index < jsonObject.enemy.Length; ++index)
                jsonObject.enemy[index] = jsonObject.deck[(int) towerResuponse.lot_enemies[index]];
              List<JSON_MapEnemyUnit> jsonMapEnemyUnitList = new List<JSON_MapEnemyUnit>((IEnumerable<JSON_MapEnemyUnit>) jsonObject.enemy);
              jsonMapEnemyUnitList.AddRange((IEnumerable<JSON_MapEnemyUnit>) randFixedUnit);
              jsonObject.enemy = jsonMapEnemyUnitList.ToArray();
              this.SetEnemies(jsonObject.enemy);
            }
          }
          else if (jsonObject.enemy != null)
            this.SetEnemies(jsonObject.enemy);
          this.SetRewards(MonoSingleton<GameManager>.Instance.FindTowerReward(towerFloor.reward_id));
        }
      }
      else
      {
        GameUtility.SetGameObjectActive(this.UnkownIcon, true);
        GameUtility.SetGameObjectActive((Component) this.RewardText, true);
        GameUtility.SetGameObjectActive(this.ClearIcon, false);
        if ((UnityEngine.Object) this.UnkownIcon != (UnityEngine.Object) null)
        {
          Text component = this.UnkownIcon.GetComponent<Text>();
          if ((UnityEngine.Object) component != (UnityEngine.Object) null)
            component.text = LocalizedText.Get("sys.TOWER_UNKNOWN_TEXT", new object[1]
            {
              (object) ((int) towerFloor.FloorIndex - downloadAssetNum + 1)
            });
        }
        for (int index = 0; index < this.EnemyList.Count; ++index)
          this.EnemyList[index].gameObject.SetActive(false);
        for (int index = 0; index < this.UnknownEnemyList.Count; ++index)
          this.UnknownEnemyList[index].gameObject.SetActive(false);
        this.SetRewards(MonoSingleton<GameManager>.Instance.FindTowerReward(towerFloor.reward_id));
      }
      GameParameter.UpdateAll(this.gameObject);
      this.FloorID = GlobalVars.SelectedQuestID;
    }

    public void OnClickDetail()
    {
      QuestParam dataOfClass = DataSource.FindDataOfClass<QuestParam>(this.gameObject, (QuestParam) null);
      if (!((UnityEngine.Object) this.Detail == (UnityEngine.Object) null) || dataOfClass == null)
        return;
      this.Detail = UnityEngine.Object.Instantiate<GameObject>(this.DetailtTmplate);
      DataSource.Bind<QuestParam>(this.Detail, dataOfClass);
      QuestCampaignData[] questCampaigns = MonoSingleton<GameManager>.Instance.FindQuestCampaigns(dataOfClass);
      DataSource.Bind<QuestCampaignData[]>(this.Detail, questCampaigns.Length != 0 ? questCampaigns : (QuestCampaignData[]) null);
      TowerResuponse towerResuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
      TowerFloorParam data = (TowerFloorParam) null;
      if (towerResuponse != null)
        data = towerResuponse.GetCurrentFloor();
      DataSource.Bind<TowerFloorParam>(this.Detail, data);
      this.Detail.SetActive(true);
    }
  }
}
