// Decompiled with JetBrains decompiler
// Type: SRPG.GuildRaidRewardList
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
  public class GuildRaidRewardList : SRPG_ListBase
  {
    [SerializeField]
    private RaidRewardListItem mListItem;

    protected override void Start()
    {
      base.Start();
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (instance.Player.mGuildRaidSeasonResult == null)
        return;
      string rewardRankingParam = instance.GetGuildRaidRewardRankingParam(instance.Player.mGuildRaidSeasonResult.Ranking.reward_id, instance.Player.mGuildRaidSeasonResult.Ranking.rank);
      this.CreateList(this.mListItem, instance.GetGuildRaidRewardParam(rewardRankingParam).Rewards);
    }

    private void CreateList(RaidRewardListItem ListItem, List<GuildRaidReward> rewards)
    {
      GameManager gm = MonoSingleton<GameManager>.Instance;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) ListItem, (UnityEngine.Object) null))
        return;
      ((Component) ListItem).gameObject.SetActive(false);
      GameUtility.SetGameObjectActive(ListItem.RewardUnit, false);
      GameUtility.SetGameObjectActive(ListItem.RewardItem, false);
      GameUtility.SetGameObjectActive(ListItem.RewardCard, false);
      GameUtility.SetGameObjectActive(ListItem.RewardArtifact, false);
      GameUtility.SetGameObjectActive(ListItem.RewardAward, false);
      GameUtility.SetGameObjectActive(ListItem.RewardGold, false);
      GameUtility.SetGameObjectActive(ListItem.RewardCoin, false);
      GameUtility.SetGameObjectActive(ListItem.RewardEmblem, false);
      if (rewards == null || rewards.Count == 0)
        return;
      RaidRewardListItem item = ListItem;
      ((Component) item).transform.SetParent(((Component) this).transform, false);
      ((Component) item).gameObject.SetActive(true);
      rewards.ForEach((Action<GuildRaidReward>) (reward =>
      {
        bool flag = false;
        GameObject gameObject;
        switch (reward.Type)
        {
          case RaidRewardType.Item:
            ItemParam itemParam = gm.GetItemParam(reward.IName);
            if (itemParam == null)
              return;
            gameObject = UnityEngine.Object.Instantiate<GameObject>(item.RewardItem);
            DataSource.Bind<ItemParam>(gameObject, itemParam);
            flag = true;
            break;
          case RaidRewardType.Gold:
            gameObject = UnityEngine.Object.Instantiate<GameObject>(item.RewardGold);
            flag = true;
            break;
          case RaidRewardType.Coin:
            gameObject = UnityEngine.Object.Instantiate<GameObject>(item.RewardCoin);
            flag = true;
            break;
          case RaidRewardType.Award:
            AwardParam awardParam = gm.GetAwardParam(reward.IName);
            if (awardParam == null)
              return;
            gameObject = UnityEngine.Object.Instantiate<GameObject>(item.RewardAward);
            DataSource.Bind<AwardParam>(gameObject, awardParam);
            break;
          case RaidRewardType.Unit:
            UnitParam unitParam = gm.GetUnitParam(reward.IName);
            if (unitParam == null)
              return;
            gameObject = UnityEngine.Object.Instantiate<GameObject>(item.RewardUnit);
            DataSource.Bind<UnitParam>(gameObject, unitParam);
            break;
          case RaidRewardType.ConceptCard:
            ConceptCardData cardDataForDisplay = ConceptCardData.CreateConceptCardDataForDisplay(reward.IName);
            if (cardDataForDisplay == null)
              return;
            gameObject = UnityEngine.Object.Instantiate<GameObject>(item.RewardCard);
            ConceptCardIcon component1 = gameObject.GetComponent<ConceptCardIcon>();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null))
            {
              component1.Setup(cardDataForDisplay);
              break;
            }
            break;
          case RaidRewardType.Artifact:
            ArtifactParam artifactParam = gm.MasterParam.GetArtifactParam(reward.IName);
            if (artifactParam == null)
              return;
            gameObject = UnityEngine.Object.Instantiate<GameObject>(item.RewardArtifact);
            DataSource.Bind<ArtifactParam>(gameObject, artifactParam);
            break;
          case RaidRewardType.GuildEmblem:
            GuildEmblemParam guildEmbleme = gm.MasterParam.GetGuildEmbleme(reward.IName);
            if (guildEmbleme == null)
              return;
            gameObject = UnityEngine.Object.Instantiate<GameObject>(item.RewardEmblem);
            this.GetEmblem(gameObject, guildEmbleme.Image);
            break;
          default:
            return;
        }
        if (flag)
        {
          Transform transform = gameObject.transform.Find("amount/Text_amount");
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) transform, (UnityEngine.Object) null))
          {
            Text component2 = ((Component) transform).GetComponent<Text>();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component2, (UnityEngine.Object) null))
              component2.text = reward.Num.ToString();
          }
        }
        gameObject.transform.SetParent(item.RewardList, false);
        gameObject.SetActive(true);
      }));
    }

    private void GetEmblem(GameObject obj, string name)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) obj, (UnityEngine.Object) null))
      {
        obj.SetActive(false);
      }
      else
      {
        Image component = obj.GetComponent<Image>();
        string name1 = name;
        ViewGuildData dataOfClass = DataSource.FindDataOfClass<ViewGuildData>(((Component) this).gameObject, (ViewGuildData) null);
        if (dataOfClass != null)
          name1 = dataOfClass.award_id;
        if (string.IsNullOrEmpty(name1) || UnityEngine.Object.op_Equality((UnityEngine.Object) component, (UnityEngine.Object) null))
        {
          ((Behaviour) component).enabled = false;
        }
        else
        {
          SpriteSheet spriteSheet = AssetManager.Load<SpriteSheet>("GuildEmblemImage/GuildEmblemes");
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) spriteSheet, (UnityEngine.Object) null))
            return;
          component.sprite = spriteSheet.GetSprite(name1);
          ((Behaviour) component).enabled = true;
        }
      }
    }
  }
}
