// Decompiled with JetBrains decompiler
// Type: SRPG.GvGLeagueRewardListContent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class GvGLeagueRewardListContent : MonoBehaviour
  {
    [SerializeField]
    private RewardListItem item;
    [SerializeField]
    private ImageArray image;
    [SerializeField]
    private Text mRankText;
    [SerializeField]
    private Text mMapNameText;
    private const int RANK_MAX = 3;

    public bool SetUpArea(string league)
    {
      GvGNodeParam dataOfClass = DataSource.FindDataOfClass<GvGNodeParam>(((Component) this).gameObject, (GvGNodeParam) null);
      if (dataOfClass == null)
        return false;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mMapNameText, (UnityEngine.Object) null))
        this.mMapNameText.text = dataOfClass.Name;
      GvGRewardParam rewardNode = dataOfClass.GetRewardNode(league);
      if (rewardNode == null)
        return false;
      this.CreateList(this.item, rewardNode.Rewards);
      return true;
    }

    public bool SetUpLeague()
    {
      GvGRewardRankingDetailParam dataOfClass = DataSource.FindDataOfClass<GvGRewardRankingDetailParam>(((Component) this).gameObject, (GvGRewardRankingDetailParam) null);
      if (dataOfClass == null)
        return false;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mRankText, (UnityEngine.Object) null))
        this.mRankText.text = dataOfClass.Ranking.ToString();
      GvGRewardParam reward = GvGRewardParam.GetReward(dataOfClass.RewardId);
      if (reward == null)
        return false;
      this.CreateList(this.item, reward.Rewards);
      return true;
    }

    private void CreateList(RewardListItem listItem, List<GvGRewardDetailParam> rewards)
    {
      GameManager gm = MonoSingleton<GameManager>.Instance;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) listItem, (UnityEngine.Object) null))
        return;
      ((Component) listItem).gameObject.SetActive(false);
      GameUtility.SetGameObjectActive(listItem.RewardUnit, false);
      GameUtility.SetGameObjectActive(listItem.RewardItem, false);
      GameUtility.SetGameObjectActive(listItem.RewardCard, false);
      GameUtility.SetGameObjectActive(listItem.RewardArtifact, false);
      GameUtility.SetGameObjectActive(listItem.RewardAward, false);
      GameUtility.SetGameObjectActive(listItem.RewardGold, false);
      GameUtility.SetGameObjectActive(listItem.RewardCoin, false);
      GameUtility.SetGameObjectActive(listItem.RewardEmblem, false);
      if (rewards == null || rewards.Count == 0)
        return;
      rewards.ForEach((Action<GvGRewardDetailParam>) (reward =>
      {
        string empty = string.Empty;
        GameObject gameObject;
        string name;
        switch (reward.Type)
        {
          case RaidRewardType.Item:
            ItemParam itemParam = gm.GetItemParam(reward.IName);
            if (itemParam == null)
              return;
            gameObject = UnityEngine.Object.Instantiate<GameObject>(listItem.RewardItem);
            DataSource.Bind<ItemParam>(gameObject, itemParam);
            name = itemParam.name;
            break;
          case RaidRewardType.Gold:
            gameObject = UnityEngine.Object.Instantiate<GameObject>(listItem.RewardGold);
            name = LocalizedText.Get("sys.GOLD");
            break;
          case RaidRewardType.Coin:
            gameObject = UnityEngine.Object.Instantiate<GameObject>(listItem.RewardCoin);
            name = LocalizedText.Get("sys.COIN");
            break;
          case RaidRewardType.Award:
            AwardParam awardParam = gm.GetAwardParam(reward.IName);
            if (awardParam == null)
              return;
            gameObject = UnityEngine.Object.Instantiate<GameObject>(listItem.RewardAward);
            DataSource.Bind<AwardParam>(gameObject, awardParam);
            name = awardParam.name;
            break;
          case RaidRewardType.Unit:
            UnitParam unitParam = gm.GetUnitParam(reward.IName);
            if (unitParam == null)
              return;
            gameObject = UnityEngine.Object.Instantiate<GameObject>(listItem.RewardUnit);
            DataSource.Bind<UnitParam>(gameObject, unitParam);
            name = unitParam.name;
            break;
          case RaidRewardType.ConceptCard:
            ConceptCardData cardDataForDisplay = ConceptCardData.CreateConceptCardDataForDisplay(reward.IName);
            if (cardDataForDisplay == null)
              return;
            gameObject = UnityEngine.Object.Instantiate<GameObject>(listItem.RewardCard);
            ConceptCardIcon component1 = gameObject.GetComponent<ConceptCardIcon>();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null))
              component1.Setup(cardDataForDisplay);
            name = cardDataForDisplay.Param.name;
            break;
          case RaidRewardType.Artifact:
            ArtifactParam artifactParam = gm.MasterParam.GetArtifactParam(reward.IName);
            if (artifactParam == null)
              return;
            gameObject = UnityEngine.Object.Instantiate<GameObject>(listItem.RewardArtifact);
            DataSource.Bind<ArtifactParam>(gameObject, artifactParam);
            name = artifactParam.name;
            break;
          case RaidRewardType.GuildEmblem:
            GuildEmblemParam guildEmbleme = gm.MasterParam.GetGuildEmbleme(reward.IName);
            if (guildEmbleme == null || UnityEngine.Object.op_Equality((UnityEngine.Object) listItem.RewardEmblem, (UnityEngine.Object) null))
              return;
            gameObject = UnityEngine.Object.Instantiate<GameObject>(listItem.RewardEmblem);
            GuildManager.GetEmblem(gameObject, guildEmbleme.Image, ((Component) this).gameObject);
            name = guildEmbleme.Name;
            break;
          default:
            return;
        }
        Transform transform1 = gameObject.transform.Find("amount/Text_amount");
        Text component2 = !UnityEngine.Object.op_Inequality((UnityEngine.Object) transform1, (UnityEngine.Object) null) ? (Text) null : ((Component) transform1).GetComponent<Text>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component2, (UnityEngine.Object) null))
          component2.text = reward.Num.ToString();
        Transform transform2 = gameObject.transform.Find("amount/name");
        Text component3 = !UnityEngine.Object.op_Inequality((UnityEngine.Object) transform2, (UnityEngine.Object) null) ? (Text) null : ((Component) transform2).GetComponent<Text>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component3, (UnityEngine.Object) null))
          component3.text = name;
        gameObject.transform.SetParent(listItem.RewardList, false);
        gameObject.SetActive(true);
      }));
    }
  }
}
