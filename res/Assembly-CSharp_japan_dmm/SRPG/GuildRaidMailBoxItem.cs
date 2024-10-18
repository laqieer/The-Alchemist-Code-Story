// Decompiled with JetBrains decompiler
// Type: SRPG.GuildRaidMailBoxItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class GuildRaidMailBoxItem : MonoBehaviour
  {
    [SerializeField]
    private SRPG_Button GetReceiveButton;
    [SerializeField]
    private GameObject RewardUnit;
    [SerializeField]
    private GameObject RewardItem;
    [SerializeField]
    private GameObject RewardCard;
    [SerializeField]
    private GameObject RewardArtifact;
    [SerializeField]
    private GameObject RewardAward;
    [SerializeField]
    private GameObject RewardGold;
    [SerializeField]
    private GameObject RewardCoin;

    public void Setup(GuildRaidMailListItem item, SRPG_Button.ButtonClickEvent callback)
    {
      GameUtility.SetGameObjectActive(this.RewardUnit, false);
      GameUtility.SetGameObjectActive(this.RewardItem, false);
      GameUtility.SetGameObjectActive(this.RewardCard, false);
      GameUtility.SetGameObjectActive(this.RewardArtifact, false);
      GameUtility.SetGameObjectActive(this.RewardAward, false);
      GameUtility.SetGameObjectActive(this.RewardGold, false);
      GameUtility.SetGameObjectActive(this.RewardCoin, false);
      DataSource.Bind<GuildRaidMailListItem>(((Component) this).gameObject, item);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.GetReceiveButton, (UnityEngine.Object) null))
        this.GetReceiveButton.AddListener(callback);
      GameManager gm = MonoSingleton<GameManager>.Instance;
      GuildRaidRewardParam guildRaidRewardParam = gm.GetGuildRaidRewardParam(item.RewardId);
      if (guildRaidRewardParam == null)
        return;
      GuildRaidBossParam guildRaidBossParam = gm.GetGuildRaidBossParam(item.BossId);
      if (guildRaidBossParam == null)
        return;
      DataSource.Bind<GuildRaidBossParam>(((Component) this).gameObject, guildRaidBossParam);
      UnitParam unitParam = MonoSingleton<GameManager>.Instance.GetUnitParam(guildRaidBossParam.UnitIName);
      if (unitParam != null)
        DataSource.Bind<UnitParam>(((Component) this).gameObject, unitParam);
      guildRaidRewardParam.Rewards.ForEach((Action<GuildRaidReward>) (reward =>
      {
        GameObject gameObject = (GameObject) null;
        bool flag = false;
        switch (reward.Type)
        {
          case RaidRewardType.Item:
            ItemParam itemParam = gm.GetItemParam(reward.IName);
            if (itemParam == null)
              return;
            gameObject = UnityEngine.Object.Instantiate<GameObject>(this.RewardItem, this.RewardItem.transform.parent);
            DataSource.Bind<ItemParam>(gameObject, itemParam);
            flag = true;
            break;
          case RaidRewardType.Gold:
            gameObject = UnityEngine.Object.Instantiate<GameObject>(this.RewardGold, this.RewardGold.transform.parent);
            flag = true;
            break;
          case RaidRewardType.Coin:
            gameObject = UnityEngine.Object.Instantiate<GameObject>(this.RewardCoin, this.RewardCoin.transform.parent);
            flag = true;
            break;
          case RaidRewardType.Award:
            AwardParam awardParam = gm.GetAwardParam(reward.IName);
            if (awardParam == null)
              return;
            gameObject = UnityEngine.Object.Instantiate<GameObject>(this.RewardAward, this.RewardAward.transform.parent);
            DataSource.Bind<AwardParam>(gameObject, awardParam);
            break;
          case RaidRewardType.Unit:
            return;
          case RaidRewardType.ConceptCard:
            ConceptCardData cardDataForDisplay = ConceptCardData.CreateConceptCardDataForDisplay(reward.IName);
            if (cardDataForDisplay != null)
            {
              gameObject = UnityEngine.Object.Instantiate<GameObject>(this.RewardCard, this.RewardCard.transform.parent);
              ConceptCardIcon component = gameObject.GetComponent<ConceptCardIcon>();
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
              {
                component.Setup(cardDataForDisplay);
                break;
              }
              break;
            }
            break;
          case RaidRewardType.Artifact:
            ArtifactParam artifactParam = gm.MasterParam.GetArtifactParam(reward.IName);
            if (artifactParam == null)
              return;
            gameObject = UnityEngine.Object.Instantiate<GameObject>(this.RewardArtifact, this.RewardArtifact.transform.parent);
            DataSource.Bind<ArtifactParam>(gameObject, artifactParam);
            break;
          default:
            return;
        }
        if (flag)
        {
          Transform transform = gameObject.transform.Find("amount/Text_amount");
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) transform, (UnityEngine.Object) null))
          {
            Text component = ((Component) transform).GetComponent<Text>();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
              component.text = reward.Num.ToString();
          }
        }
        gameObject.SetActive(true);
      }));
    }
  }
}
