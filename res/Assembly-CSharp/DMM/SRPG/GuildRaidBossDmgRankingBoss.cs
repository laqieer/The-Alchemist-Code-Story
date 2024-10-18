// Decompiled with JetBrains decompiler
// Type: SRPG.GuildRaidBossDmgRankingBoss
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
  [FlowNode.Pin(10, "BOSSランキング画面選択", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(11, "報酬一覧画面選択", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(100, "BOSSリスト追加読み込み", FlowNode.PinTypes.Output, 100)]
  public class GuildRaidBossDmgRankingBoss : MonoBehaviour, IFlowInterface
  {
    private const int PIN_INPUT_SELECT_BOSSRANKING = 10;
    private const int PIN_INPUT_SELECT_REWARDLIST = 11;
    private const int PIN_OUTPUT_REQBOSSLIST = 100;
    [SerializeField]
    private GameObject mRankingTemplate;
    [SerializeField]
    private GameObject mRewardTemplate;
    [SerializeField]
    private GameObject mRankingView;
    [SerializeField]
    private GameObject mRewardView;
    [SerializeField]
    private Text mTitle;
    [SerializeField]
    private SRPG_ScrollRect Scroll;
    [SerializeField]
    private RectTransform ScrollContent;
    private bool IsLoading;
    private List<GameObject> mCreateList = new List<GameObject>();
    private List<GameObject> mCreateRewardList = new List<GameObject>();
    private GuildRaidBossDmgRankingBoss.SelectType mSelectType;

    private void Awake()
    {
      this.mSelectType = GuildRaidBossDmgRankingBoss.SelectType.Ranking;
      this.Refresh();
      this.Scroll.verticalNormalizedPosition = 1f;
    }

    private void Update()
    {
      if (this.IsLoading || this.mSelectType == GuildRaidBossDmgRankingBoss.SelectType.Reward || GuildRaidManager.Instance.RankingDamageRoundPage >= GuildRaidManager.Instance.RankingDamageRoundPageTotal || !Object.op_Inequality((Object) this.Scroll, (Object) null) || !Object.op_Inequality((Object) this.ScrollContent, (Object) null) || (double) this.Scroll.verticalNormalizedPosition * (double) this.ScrollContent.sizeDelta.y >= 10.0)
        return;
      this.IsLoading = true;
      ++GuildRaidManager.Instance.RankingDamageRoundPage;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }

    public void Activated(int pinID)
    {
      bool flag = false;
      switch (pinID)
      {
        case 10:
          if (this.mSelectType != GuildRaidBossDmgRankingBoss.SelectType.Ranking)
            flag = true;
          this.mSelectType = GuildRaidBossDmgRankingBoss.SelectType.Ranking;
          break;
        case 11:
          this.mSelectType = GuildRaidBossDmgRankingBoss.SelectType.Reward;
          break;
      }
      if (flag)
        this.Scroll.verticalNormalizedPosition = 1f;
      this.Refresh();
    }

    private void Refresh()
    {
      GuildRaidManager instance = GuildRaidManager.Instance;
      GuildRaidBossParam guildRaidBossParam1 = MonoSingleton<GameManager>.Instance.GetGuildRaidBossParam(instance.RankingDamageRoundBossId);
      if (guildRaidBossParam1 != null && Object.op_Inequality((Object) this.mTitle, (Object) null))
        this.mTitle.text = string.Format(LocalizedText.Get("sys.GUILDRAID_DMG_RANKING_TITLE"), (object) instance.RankingDamageRoundRound, (object) guildRaidBossParam1.Name);
      if (Object.op_Equality((Object) instance, (Object) null) || instance.RankingDamageRoundList == null || Object.op_Equality((Object) this.mRankingTemplate, (Object) null) || Object.op_Equality((Object) this.mRewardTemplate, (Object) null))
        return;
      if (Object.op_Inequality((Object) this.mRankingView, (Object) null))
        this.mRankingView.SetActive(true);
      if (Object.op_Inequality((Object) this.mRewardView, (Object) null))
        this.mRewardView.SetActive(true);
      this.mRankingTemplate.SetActive(false);
      for (int index = 0; index < this.mCreateList.Count; ++index)
      {
        if (Object.op_Inequality((Object) this.mCreateList[index], (Object) null))
          Object.Destroy((Object) this.mCreateList[index]);
      }
      this.mCreateList.Clear();
      for (int index = 0; index < instance.RankingDamageRoundList.Count; ++index)
      {
        if (instance.RankingDamageRoundList[index] != null)
        {
          GameObject gameObject = Object.Instantiate<GameObject>(this.mRankingTemplate, this.mRankingTemplate.transform.parent);
          DataSource.Bind<GuildRaidRankingDamage>(gameObject, instance.RankingDamageRoundList[index]);
          gameObject.SetActive(true);
          this.mCreateList.Add(gameObject);
        }
      }
      GuildRaidBossParam guildRaidBossParam2 = MonoSingleton<GameManager>.Instance.GetGuildRaidBossParam(instance.RankingDamageRoundBossId);
      if (guildRaidBossParam2 == null)
        return;
      GuildRaidRewardDmgRankingParam rewardDmgRanking = MonoSingleton<GameManager>.Instance.GetGuildRaidRewardDmgRanking(guildRaidBossParam2.DamageRankingRewardId);
      if (rewardDmgRanking == null || rewardDmgRanking.Ranking == null || rewardDmgRanking.Ranking.Count == 0)
        return;
      this.mRewardTemplate.SetActive(false);
      for (int index = 0; index < this.mCreateRewardList.Count; ++index)
      {
        if (Object.op_Inequality((Object) this.mCreateRewardList[index], (Object) null))
          Object.Destroy((Object) this.mCreateRewardList[index]);
      }
      this.mCreateRewardList.Clear();
      for (int index1 = 0; index1 < rewardDmgRanking.Ranking.Count; ++index1)
      {
        if (rewardDmgRanking.Ranking[index1] != null)
        {
          GuildRaidRewardParam guildRaidRewardRound = MonoSingleton<GameManager>.Instance.GetGuildRaidRewardRound(rewardDmgRanking.Ranking[index1].RewardRoundId, GuildRaidManager.Instance.CurrentRound);
          if (guildRaidRewardRound != null)
          {
            GameObject gameObject = Object.Instantiate<GameObject>(this.mRewardTemplate, this.mRewardTemplate.transform.parent);
            if (!Object.op_Equality((Object) gameObject, (Object) null))
            {
              this.mCreateRewardList.Add(gameObject);
              DataSource.Bind<GuildRaidRewardDmgRankingRankParam>(gameObject, rewardDmgRanking.Ranking[index1]);
              GuildRaidBossDmgRankingBossItem component = gameObject.GetComponent<GuildRaidBossDmgRankingBossItem>();
              if (!Object.op_Equality((Object) component, (Object) null))
              {
                for (int index2 = 0; index2 < guildRaidRewardRound.Rewards.Count; ++index2)
                  this.SetRewardIcon(guildRaidRewardRound.Rewards[index2], component.mReward);
                gameObject.SetActive(true);
              }
            }
          }
        }
      }
      if (this.mSelectType == GuildRaidBossDmgRankingBoss.SelectType.Ranking)
      {
        if (Object.op_Inequality((Object) this.mRankingView, (Object) null))
          this.mRankingView.SetActive(true);
        if (Object.op_Inequality((Object) this.mRewardView, (Object) null))
          this.mRewardView.SetActive(false);
      }
      else if (this.mSelectType == GuildRaidBossDmgRankingBoss.SelectType.Reward)
      {
        if (Object.op_Inequality((Object) this.mRankingView, (Object) null))
          this.mRankingView.SetActive(false);
        if (Object.op_Inequality((Object) this.mRewardView, (Object) null))
          this.mRewardView.SetActive(true);
      }
      this.IsLoading = false;
      GameParameter.UpdateAll(((Component) this).gameObject);
    }

    private void SetRewardIcon(GuildRaidReward reward, RewardListItem listItem)
    {
      if (reward == null || Object.op_Equality((Object) listItem, (Object) null))
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      GameObject gameObject = (GameObject) null;
      bool flag = false;
      listItem.AllNotActive();
      switch (reward.Type)
      {
        case RaidRewardType.Item:
          ItemParam itemParam = instance.GetItemParam(reward.IName);
          if (itemParam == null)
            return;
          gameObject = Object.Instantiate<GameObject>(listItem.RewardItem, (Transform) null);
          if (Object.op_Equality((Object) gameObject, (Object) null))
            return;
          flag = true;
          DataSource.Bind<ItemParam>(gameObject, itemParam);
          break;
        case RaidRewardType.Gold:
          gameObject = Object.Instantiate<GameObject>(listItem.RewardGold, (Transform) null);
          if (Object.op_Equality((Object) gameObject, (Object) null))
            return;
          flag = true;
          break;
        case RaidRewardType.Coin:
          gameObject = Object.Instantiate<GameObject>(listItem.RewardCoin, (Transform) null);
          if (Object.op_Equality((Object) gameObject, (Object) null))
            return;
          flag = true;
          break;
        case RaidRewardType.Award:
          AwardParam awardParam = instance.GetAwardParam(reward.IName);
          if (awardParam == null)
            return;
          gameObject = Object.Instantiate<GameObject>(listItem.RewardAward, (Transform) null);
          if (Object.op_Equality((Object) gameObject, (Object) null))
            return;
          DataSource.Bind<AwardParam>(gameObject, awardParam);
          break;
        case RaidRewardType.Unit:
          UnitParam unitParam = instance.GetUnitParam(reward.IName);
          if (unitParam == null)
            return;
          gameObject = Object.Instantiate<GameObject>(listItem.RewardUnit, (Transform) null);
          if (Object.op_Equality((Object) gameObject, (Object) null))
            return;
          DataSource.Bind<UnitParam>(gameObject, unitParam);
          break;
        case RaidRewardType.ConceptCard:
          ConceptCardData cardDataForDisplay = ConceptCardData.CreateConceptCardDataForDisplay(reward.IName);
          if (cardDataForDisplay == null)
            return;
          gameObject = Object.Instantiate<GameObject>(listItem.RewardCard, (Transform) null);
          if (Object.op_Equality((Object) gameObject, (Object) null))
            return;
          ConceptCardIcon component1 = gameObject.GetComponent<ConceptCardIcon>();
          if (Object.op_Inequality((Object) component1, (Object) null))
          {
            component1.Setup(cardDataForDisplay);
            break;
          }
          break;
        case RaidRewardType.Artifact:
          ArtifactParam artifactParam = instance.MasterParam.GetArtifactParam(reward.IName);
          if (artifactParam == null)
            return;
          gameObject = Object.Instantiate<GameObject>(listItem.RewardArtifact, (Transform) null);
          if (Object.op_Equality((Object) gameObject, (Object) null))
            return;
          DataSource.Bind<ArtifactParam>(gameObject, artifactParam);
          break;
      }
      if (Object.op_Equality((Object) gameObject, (Object) null))
        return;
      if (flag)
      {
        Transform transform = gameObject.transform.Find("amount/Text_amount");
        if (Object.op_Inequality((Object) transform, (Object) null))
        {
          Text component2 = ((Component) transform).GetComponent<Text>();
          if (Object.op_Inequality((Object) component2, (Object) null))
            component2.text = reward.Num.ToString();
        }
      }
      gameObject.transform.SetParent(listItem.RewardList, false);
      gameObject.SetActive(true);
    }

    private enum SelectType
    {
      Ranking,
      Reward,
    }
  }
}
