// Decompiled with JetBrains decompiler
// Type: SRPG.GenesisBossInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "Initialize", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "QuestDetail Open", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(101, "Initialized", FlowNode.PinTypes.Output, 101)]
  public class GenesisBossInfo : MonoBehaviour, IFlowInterface, IWebHelp
  {
    public const int PIN_IN_INIT = 1;
    public const int PIN_IN_QUEST_DETAIL_OPEN = 2;
    public const int PIN_OUT_INIT = 101;
    private static GenesisBossInfo mInstance;
    [SerializeField]
    private Transform mBGParent;
    [SerializeField]
    private GameObject[] mTitle;
    [SerializeField]
    private Text mChapterTitle;
    [Space(5f)]
    [SerializeField]
    private GameObject mRewardBox;
    [SerializeField]
    private Text mRwardItemName;
    [SerializeField]
    private Text mRwardItemNum;
    [SerializeField]
    private Transform mRewardIconParent;
    [SerializeField]
    private GenesisRewardIcon mRewardIcon;
    [Space(5f)]
    [SerializeField]
    private GameObject mTicketIcon;
    [SerializeField]
    private GameObject mUseTicketNum;
    [SerializeField]
    private GameObject mHaveTicketNum;
    [SerializeField]
    private GameObject mChallengeButton;
    [Space(5f)]
    [SerializeField]
    private GameObject mGoCommonBoss;
    [SerializeField]
    private GameObject mGoLapBoss;
    [SerializeField]
    private Text mLapBossChapterTitle;
    [Space(5f)]
    [SerializeField]
    private GameObject mGoLapBossReward;
    [SerializeField]
    private GameObject mGoParentLapBossReward;
    [SerializeField]
    private GenesisLapBossReward mTemplateLapBossReward;
    [Space(5f)]
    [SerializeField]
    private GameObject mPrefabQuestDetail;
    private QuestParam mCurrentQuest;
    public static GenesisBossInfo.LapBossBattleInfo LapBossBattleData;

    public static GenesisBossInfo Instance => GenesisBossInfo.mInstance;

    public int BossHP { get; private set; }

    private void Awake() => GenesisBossInfo.mInstance = this;

    private void OnDestroy() => GenesisBossInfo.mInstance = (GenesisBossInfo) null;

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          if (this.Init())
            break;
          DebugUtility.LogError("GenesisBossInfo初期化失敗");
          break;
        case 2:
          this.OpenQuestDetail();
          break;
      }
    }

    public void SetBossInfo(int hp, int round)
    {
      this.BossHP = hp;
      this.SetBossBattleInfo(round);
    }

    private bool Init()
    {
      GenesisChapterManager instance = GenesisChapterManager.Instance;
      if (!Object.op_Implicit((Object) instance))
        return false;
      GenesisChapterParam currentChapterParam = instance.CurrentChapterParam;
      if (currentChapterParam == null)
        return false;
      GenesisChapterModeInfoParam modeInfo = currentChapterParam.GetModeInfo(instance.BossDifficulty);
      if (modeInfo == null)
        return false;
      DataSource.Bind<GenesisChapterModeInfoParam>(((Component) this).gameObject, modeInfo);
      DataSource.Bind<GenesisBossInfo.GenesisBossData>(((Component) this).gameObject, new GenesisBossInfo.GenesisBossData()
      {
        unit = modeInfo.BossUnitParam,
        maxHP = modeInfo.BossHp,
        currentHP = this.BossHP
      });
      if (this.mTitle != null)
      {
        for (int index = 0; index < this.mTitle.Length; ++index)
          this.mTitle[index].SetActive((QuestDifficulties) index == instance.BossDifficulty);
      }
      if (instance.GenesisAssets.BossBG.Length <= modeInfo.ModeUiIndex)
        return false;
      instance.LoadAssets<GameObject>(instance.GenesisAssets.BossBG[modeInfo.ModeUiIndex], new GenesisChapterManager.LoadAssetCallback<GameObject>(this.Downloaded));
      this.mChapterTitle.text = currentChapterParam.Name;
      if (Object.op_Implicit((Object) this.mLapBossChapterTitle))
        this.mLapBossChapterTitle.text = currentChapterParam.Name;
      if (Object.op_Implicit((Object) this.mGoCommonBoss))
        this.mGoCommonBoss.SetActive(!modeInfo.IsLapBoss);
      if (Object.op_Implicit((Object) this.mGoLapBoss))
        this.mGoLapBoss.SetActive(modeInfo.IsLapBoss);
      this.SetupReward(currentChapterParam, modeInfo);
      this.mCurrentQuest = currentChapterParam.GetBossQuest(instance.BossDifficulty);
      if (this.mCurrentQuest == null)
        return false;
      GlobalVars.SelectedQuestID = this.mCurrentQuest.iname;
      ItemData data1 = new ItemData();
      data1.Setup(0L, modeInfo.BossChallengeItemParam, modeInfo.BossChallengeItemNum);
      DataSource.Bind<ItemData>(this.mTicketIcon, data1);
      DataSource.Bind<ItemData>(this.mUseTicketNum, data1);
      ItemData data2 = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemParam(modeInfo.BossChallengeItemParam);
      if (data2 == null)
      {
        data2 = new ItemData();
        data2.Setup(0L, modeInfo.BossChallengeItemParam, 0);
      }
      DataSource.Bind<ItemData>(this.mHaveTicketNum, data2);
      GameParameter.UpdateAll(((Component) this).gameObject);
      this.StartCoroutine(this.UpdateTicketNum());
      return true;
    }

    [DebuggerHidden]
    private IEnumerator UpdateTicketNum()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GenesisBossInfo.\u003CUpdateTicketNum\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    private void Downloaded(GameObject prefab)
    {
      if (Object.op_Equality((Object) prefab, (Object) null))
        DebugUtility.LogError("おかしい");
      Object.Instantiate<GameObject>(prefab, this.mBGParent);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
    }

    public bool GetHelpURL(out string url, out string title)
    {
      title = (string) null;
      url = (string) null;
      if (Object.op_Equality((Object) MonoSingleton<GameManager>.Instance, (Object) null))
        return false;
      GenesisChapterManager instance = GenesisChapterManager.Instance;
      if (Object.op_Equality((Object) instance, (Object) null))
        return false;
      GenesisChapterParam currentChapterParam = instance.CurrentChapterParam;
      if (currentChapterParam == null || string.IsNullOrEmpty(currentChapterParam.BossHintUrl))
        return false;
      title = currentChapterParam.Name;
      url = currentChapterParam.BossHintUrl;
      return true;
    }

    private void OpenQuestDetail()
    {
      if (!Object.op_Implicit((Object) this.mPrefabQuestDetail) || this.mCurrentQuest == null)
        return;
      GameObject gameObject = Object.Instantiate<GameObject>(this.mPrefabQuestDetail);
      DataSource.Bind<QuestParam>(gameObject, this.mCurrentQuest);
      QuestCampaignData[] questCampaigns = MonoSingleton<GameManager>.Instance.FindQuestCampaigns(this.mCurrentQuest);
      if (questCampaigns != null && questCampaigns.Length != 0)
        DataSource.Bind<QuestCampaignData[]>(gameObject, questCampaigns);
      QuestDetail component = gameObject.GetComponent<QuestDetail>();
      if (Object.op_Implicit((Object) component))
      {
        GameUtility.SetGameObjectActive(component.GoCondition, false);
        GameUtility.SetGameObjectActive(component.GoLimit, false);
        GameUtility.SetGameObjectActive(component.GoDropList, false);
      }
      gameObject.SetActive(true);
    }

    private void SetupReward(GenesisChapterParam gcParam, GenesisChapterModeInfoParam gcmiParam)
    {
      if (gcParam == null || gcmiParam == null)
        return;
      if (gcmiParam.IsLapBoss)
      {
        if (Object.op_Implicit((Object) this.mRewardBox))
          this.mRewardBox.SetActive(false);
        if (Object.op_Implicit((Object) this.mGoLapBossReward))
          this.mGoLapBossReward.SetActive(true);
        if (!Object.op_Implicit((Object) this.mGoParentLapBossReward) || !Object.op_Implicit((Object) this.mTemplateLapBossReward))
          return;
        ((Component) this.mTemplateLapBossReward).gameObject.SetActive(false);
        GameUtility.DestroyChildGameObjects(this.mGoParentLapBossReward, new List<GameObject>((IEnumerable<GameObject>) new GameObject[1]
        {
          ((Component) this.mTemplateLapBossReward).gameObject
        }));
        if (gcmiParam.LapBossParam == null || GenesisBossInfo.LapBossBattleData == null)
          return;
        GenesisRewardParam roundReward = gcmiParam.LapBossParam.GetRoundReward(GenesisBossInfo.LapBossBattleData.Round);
        if (roundReward == null || gcmiParam.BossRewardParam == null)
          return;
        for (int index = 0; index < roundReward.RewardList.Count; ++index)
        {
          GenesisRewardDataParam reward = roundReward.RewardList[index];
          GenesisLapBossReward genesisLapBossReward = Object.Instantiate<GenesisLapBossReward>(this.mTemplateLapBossReward, this.mGoParentLapBossReward.transform, false);
          if (Object.op_Implicit((Object) genesisLapBossReward))
          {
            genesisLapBossReward.SetItem(index, reward);
            ((Component) genesisLapBossReward).gameObject.SetActive(true);
          }
        }
      }
      else
      {
        if (Object.op_Implicit((Object) this.mGoLapBossReward))
          this.mGoLapBossReward.SetActive(false);
        if (gcmiParam.BossRewardParam.RewardList.Count <= 0)
        {
          if (!Object.op_Inequality((Object) this.mRewardBox, (Object) null))
            return;
          this.mRewardBox.SetActive(false);
        }
        else
        {
          GenesisRewardDataParam reward = gcmiParam.BossRewardParam.RewardList[0];
          string str = string.Empty;
          switch (reward.ItemType)
          {
            case 0:
              ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(reward.ItemIname);
              if (itemParam != null)
              {
                str = itemParam.name;
                break;
              }
              break;
            case 1:
              str = LocalizedText.Get("sys.GOLD");
              break;
            case 2:
              str = LocalizedText.Get("sys.COIN");
              break;
            case 3:
              AwardParam awardParam = MonoSingleton<GameManager>.Instance.GetAwardParam(reward.ItemIname);
              if (awardParam != null)
              {
                str = awardParam.name;
                break;
              }
              break;
            case 4:
              UnitParam unitParam = MonoSingleton<GameManager>.Instance.GetUnitParam(reward.ItemIname);
              if (unitParam != null)
              {
                str = unitParam.name;
                break;
              }
              break;
            case 5:
              ConceptCardParam conceptCardParam = MonoSingleton<GameManager>.Instance.GetConceptCardParam(reward.ItemIname);
              if (conceptCardParam != null)
              {
                str = conceptCardParam.name;
                break;
              }
              break;
            case 6:
              ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(reward.ItemIname);
              if (artifactParam != null)
              {
                str = artifactParam.name;
                break;
              }
              break;
          }
          if (Object.op_Inequality((Object) this.mRwardItemName, (Object) null))
            this.mRwardItemName.text = str;
          if (Object.op_Inequality((Object) this.mRwardItemNum, (Object) null))
            this.mRwardItemNum.text = reward.ItemNum.ToString();
          if (!Object.op_Inequality((Object) this.mRewardIconParent, (Object) null) || !Object.op_Inequality((Object) this.mRewardIcon, (Object) null))
            return;
          Object.Instantiate<GenesisRewardIcon>(this.mRewardIcon, this.mRewardIconParent).Initialize(reward);
        }
      }
    }

    private bool SetBossBattleInfo(int round)
    {
      GenesisBossInfo.LapBossBattleData = (GenesisBossInfo.LapBossBattleInfo) null;
      GenesisChapterManager instance = GenesisChapterManager.Instance;
      if (Object.op_Equality((Object) instance, (Object) null) || instance.CurrentChapterParam == null)
        return false;
      GenesisChapterModeInfoParam modeInfo = instance.CurrentChapterParam.GetModeInfo(instance.BossDifficulty);
      if (modeInfo == null)
        return false;
      if (modeInfo.LapBossParam == null)
        return true;
      GenesisBossInfo.LapBossBattleData = new GenesisBossInfo.LapBossBattleInfo();
      GenesisBossInfo.LapBossBattleData.LapBossParam = modeInfo.LapBossParam;
      GenesisBossInfo.LapBossBattleData.Round = Mathf.Max(1, round);
      return true;
    }

    public static bool IsLapBossQuest(QuestParam quest)
    {
      if (quest == null)
        return false;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (Object.op_Equality((Object) instance, (Object) null))
        return false;
      GenesisChapterParam chapterParamFromAreaId = instance.GetGenesisChapterParamFromAreaId(quest.ChapterID);
      if (chapterParamFromAreaId != null)
      {
        GenesisChapterModeInfoParam modeInfo = chapterParamFromAreaId.GetModeInfo(quest.difficulty);
        if (modeInfo != null && modeInfo.IsLapBoss)
          return true;
      }
      return false;
    }

    public static bool SetBossBattleInfo(QuestParam quest, int round)
    {
      GenesisBossInfo.LapBossBattleData = (GenesisBossInfo.LapBossBattleInfo) null;
      if (quest == null)
        return false;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (Object.op_Equality((Object) instance, (Object) null))
        return false;
      GenesisChapterParam chapterParamFromAreaId = instance.GetGenesisChapterParamFromAreaId(quest.ChapterID);
      if (chapterParamFromAreaId == null)
        return false;
      GenesisChapterModeInfoParam modeInfo = chapterParamFromAreaId.GetModeInfo(quest.difficulty);
      if (modeInfo == null)
        return false;
      if (modeInfo.LapBossParam == null)
        return true;
      GenesisBossInfo.LapBossBattleData = new GenesisBossInfo.LapBossBattleInfo();
      GenesisBossInfo.LapBossBattleData.LapBossParam = modeInfo.LapBossParam;
      GenesisBossInfo.LapBossBattleData.Round = Mathf.Max(1, round);
      return true;
    }

    public static bool GetBossChallengeItemInfo(
      ref ItemParam ch_item_param,
      ref int ch_item_need_num)
    {
      GenesisChapterManager instance = GenesisChapterManager.Instance;
      if (!Object.op_Implicit((Object) instance))
        return false;
      GenesisChapterParam currentChapterParam = instance.CurrentChapterParam;
      if (currentChapterParam == null)
        return false;
      GenesisChapterModeInfoParam modeInfo = currentChapterParam.GetModeInfo(instance.BossDifficulty);
      if (modeInfo == null)
        return false;
      ch_item_param = modeInfo.BossChallengeItemParam;
      ch_item_need_num = modeInfo.BossChallengeItemNum;
      return true;
    }

    public class GenesisBossData
    {
      public UnitParam unit;
      public int maxHP;
      public int currentHP;
    }

    public class LapBossBattleInfo
    {
      public GenesisLapBossParam LapBossParam;
      public int Round;
    }
  }
}
