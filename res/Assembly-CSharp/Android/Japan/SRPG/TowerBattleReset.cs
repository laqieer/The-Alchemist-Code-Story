// Decompiled with JetBrains decompiler
// Type: SRPG.TowerBattleReset
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(0, "初期化（実行）", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "初期化（終了）", FlowNode.PinTypes.Output, 100)]
  public class TowerBattleReset : MonoBehaviour, IFlowInterface
  {
    private const int INPUT_INITIALIZE_BEGIN = 0;
    private const int OUTPUT_INITIALIZE_END = 100;
    [SerializeField]
    private GameObject TollRecovery;
    [SerializeField]
    private GameObject FreeRecovery;
    [SerializeField]
    private GameObject HaveItemObject;
    [SerializeField]
    private Text HaveItemNum;
    [HeaderBar("▼ミッション表示エリアのタイトル")]
    [SerializeField]
    private Text m_MissionListTitle;
    [SerializeField]
    private Text m_MissionListTitleNoItem;
    [HeaderBar("▼ミッション表示リストの親")]
    [SerializeField]
    private RectTransform m_MissionListParent;
    [HeaderBar("▼ミッション表示用テンプレート")]
    [SerializeField]
    private QuestMissionItem m_MissionItemTemplate;
    private List<GameObject> m_MissionListItems;

    public void Activated(int pinID)
    {
      if (pinID != 0)
        return;
      this.InitializeMissionItems();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }

    private void Start()
    {
      GameUtility.SetGameObjectActive((Component) this.m_MissionItemTemplate, false);
      GameUtility.SetGameObjectActive((Component) this.m_MissionListTitle, false);
      GameUtility.SetGameObjectActive((Component) this.m_MissionListTitleNoItem, false);
      GameUtility.SetGameObjectActive((Component) this.m_MissionListParent, false);
      TowerParam tower = MonoSingleton<GameManager>.Instance.FindTower(GlobalVars.SelectedTowerID);
      if (tower == null)
        return;
      if (tower.floor_reset_coin > (short) 0)
      {
        this.TollRecovery.SetActive(true);
        this.FreeRecovery.SetActive(false);
        this.TollRecovery.GetComponent<Text>().text = string.Format(LocalizedText.Get("sys.TOWER_BATTLE_RESET_DESCRIPTION_02"), (object) tower.floor_reset_coin.ToString());
        this.HaveItemNum.text = MonoSingleton<GameManager>.Instance.Player.Coin.ToString();
      }
      else
      {
        this.TollRecovery.SetActive(false);
        this.FreeRecovery.SetActive(true);
        this.HaveItemObject.SetActive(false);
      }
    }

    private void InitializeMissionItems()
    {
      this.CreateResetMissionItems(MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID));
      if (this.m_MissionListItems != null && this.m_MissionListItems.Count > 0)
      {
        GameUtility.SetGameObjectActive((Component) this.m_MissionListTitle, true);
      }
      else
      {
        GameUtility.SetGameObjectActive((Component) this.m_MissionListTitleNoItem, true);
        GameUtility.SetGameObjectActive((Component) this.m_MissionListParent, false);
      }
    }

    private void CreateResetMissionItems(QuestParam questParam)
    {
      this.DeleteMissionItems();
      if (questParam == null || questParam.bonusObjective == null || ((UnityEngine.Object) this.m_MissionListParent == (UnityEngine.Object) null || (UnityEngine.Object) this.m_MissionItemTemplate == (UnityEngine.Object) null))
        return;
      if (this.m_MissionListItems == null)
        this.m_MissionListItems = new List<GameObject>();
      DataSource.Bind<QuestParam>(this.gameObject, questParam, false);
      for (int index = 0; index < questParam.bonusObjective.Length; ++index)
      {
        QuestBonusObjective bonusObjective = questParam.bonusObjective[index];
        if (!questParam.IsMissionClear(index) && bonusObjective.IsProgressMission())
        {
          QuestMissionItem missionItem = this.CreateMissionItem(bonusObjective);
          missionItem.SetGameParameterIndex(index);
          this.m_MissionListItems.Add(missionItem.gameObject);
          GameParameter.UpdateAll(missionItem.gameObject);
        }
      }
    }

    private QuestMissionItem CreateMissionItem(QuestBonusObjective bonusObjective)
    {
      if (bonusObjective == null)
        return (QuestMissionItem) null;
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.m_MissionItemTemplate.gameObject);
      gameObject.SetActive(true);
      gameObject.transform.SetParent(this.m_MissionListParent.transform, false);
      return gameObject.GetComponent<QuestMissionItem>();
    }

    private void DeleteMissionItems()
    {
      if (this.m_MissionListItems == null)
        return;
      for (int index = 0; index < this.m_MissionListItems.Count; ++index)
      {
        UnityEngine.Object.Destroy((UnityEngine.Object) this.m_MissionListItems[index]);
        this.m_MissionListItems[index] = (GameObject) null;
      }
      this.m_MissionListItems.Clear();
    }
  }
}
