// Decompiled with JetBrains decompiler
// Type: SRPG.RaidRewardIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class RaidRewardIcon : MonoBehaviour
  {
    [SerializeField]
    private GameObject mRewardUnit;
    [SerializeField]
    private GameObject mRewardItem;
    [SerializeField]
    private GameObject mRewardCard;
    [SerializeField]
    private GameObject mRewardArtifact;
    [SerializeField]
    private GameObject mRewardAward;
    [SerializeField]
    private GameObject mRewardGold;
    [SerializeField]
    private GameObject mRewardCoin;

    public void Initialize(GiftData reward)
    {
      if (Object.op_Inequality((Object) this.mRewardUnit, (Object) null))
        this.mRewardUnit.SetActive(false);
      if (Object.op_Inequality((Object) this.mRewardItem, (Object) null))
        this.mRewardItem.SetActive(false);
      if (Object.op_Inequality((Object) this.mRewardCard, (Object) null))
        this.mRewardCard.SetActive(false);
      if (Object.op_Inequality((Object) this.mRewardArtifact, (Object) null))
        this.mRewardArtifact.SetActive(false);
      if (Object.op_Inequality((Object) this.mRewardAward, (Object) null))
        this.mRewardAward.SetActive(false);
      if (Object.op_Inequality((Object) this.mRewardGold, (Object) null))
        this.mRewardGold.SetActive(false);
      if (Object.op_Inequality((Object) this.mRewardCoin, (Object) null))
        this.mRewardCoin.SetActive(false);
      GameManager instance = MonoSingleton<GameManager>.Instance;
      bool flag = false;
      int num = 1;
      GameObject gameObject = (GameObject) null;
      if (!string.IsNullOrEmpty(reward.iname))
      {
        if (reward.CheckGiftTypeIncluded(GiftTypes.Item))
        {
          ItemParam itemParam = instance.GetItemParam(reward.iname);
          if (itemParam == null)
            return;
          DataSource.Bind<ItemParam>(this.mRewardItem, itemParam);
          gameObject = this.mRewardItem;
          num = reward.num;
          flag = true;
        }
        else if (reward.CheckGiftTypeIncluded(GiftTypes.Award))
        {
          AwardParam awardParam = instance.GetAwardParam(reward.iname);
          if (awardParam == null)
            return;
          DataSource.Bind<AwardParam>(this.mRewardAward, awardParam);
          gameObject = this.mRewardAward;
        }
        else if (reward.CheckGiftTypeIncluded(GiftTypes.Unit))
        {
          UnitParam unitParam = instance.GetUnitParam(reward.iname);
          if (unitParam == null)
            return;
          DataSource.Bind<UnitParam>(this.mRewardUnit, unitParam);
          gameObject = this.mRewardUnit;
        }
        else if (reward.CheckGiftTypeIncluded(GiftTypes.ConceptCard))
        {
          ConceptCardData cardDataForDisplay = ConceptCardData.CreateConceptCardDataForDisplay(reward.iname);
          if (cardDataForDisplay == null)
            return;
          ConceptCardIcon component = this.mRewardCard.GetComponent<ConceptCardIcon>();
          if (Object.op_Inequality((Object) component, (Object) null))
            component.Setup(cardDataForDisplay);
          gameObject = this.mRewardCard;
          num = reward.num;
          flag = true;
        }
        else if (reward.CheckGiftTypeIncluded(GiftTypes.Artifact))
        {
          ArtifactParam artifactParam = instance.MasterParam.GetArtifactParam(reward.iname);
          if (artifactParam == null)
            return;
          DataSource.Bind<ArtifactParam>(this.mRewardArtifact, artifactParam);
          gameObject = this.mRewardArtifact;
        }
      }
      else if (reward.gold > 0)
      {
        gameObject = this.mRewardGold;
        num = reward.gold;
        flag = true;
      }
      else if (reward.coin > 0)
      {
        gameObject = this.mRewardCoin;
        num = reward.coin;
        flag = true;
      }
      else if (!string.IsNullOrEmpty(reward.ConceptCardIname))
        ;
      if (!Object.op_Inequality((Object) gameObject, (Object) null))
        return;
      gameObject.SetActive(true);
      if (!flag)
        return;
      Transform transform = gameObject.transform.Find("amount/Text_amount");
      if (!Object.op_Inequality((Object) transform, (Object) null))
        return;
      Text component1 = ((Component) transform).GetComponent<Text>();
      if (!Object.op_Inequality((Object) component1, (Object) null))
        return;
      component1.text = num.ToString();
    }

    public void Initialize(RaidReward reward)
    {
      if (Object.op_Inequality((Object) this.mRewardUnit, (Object) null))
        this.mRewardUnit.SetActive(false);
      if (Object.op_Inequality((Object) this.mRewardItem, (Object) null))
        this.mRewardItem.SetActive(false);
      if (Object.op_Inequality((Object) this.mRewardCard, (Object) null))
        this.mRewardCard.SetActive(false);
      if (Object.op_Inequality((Object) this.mRewardArtifact, (Object) null))
        this.mRewardArtifact.SetActive(false);
      if (Object.op_Inequality((Object) this.mRewardAward, (Object) null))
        this.mRewardAward.SetActive(false);
      if (Object.op_Inequality((Object) this.mRewardGold, (Object) null))
        this.mRewardGold.SetActive(false);
      if (Object.op_Inequality((Object) this.mRewardCoin, (Object) null))
        this.mRewardCoin.SetActive(false);
      GameManager instance = MonoSingleton<GameManager>.Instance;
      bool flag = false;
      GameObject gameObject = (GameObject) null;
      switch (reward.Type)
      {
        case RaidRewardType.Item:
          ItemParam itemParam = instance.GetItemParam(reward.IName);
          if (itemParam == null)
            return;
          DataSource.Bind<ItemParam>(this.mRewardItem, itemParam);
          gameObject = this.mRewardItem;
          flag = true;
          break;
        case RaidRewardType.Gold:
          gameObject = this.mRewardGold;
          flag = true;
          break;
        case RaidRewardType.Coin:
          gameObject = this.mRewardCoin;
          flag = true;
          break;
        case RaidRewardType.Award:
          AwardParam awardParam = instance.GetAwardParam(reward.IName);
          if (awardParam == null)
            return;
          DataSource.Bind<AwardParam>(this.mRewardAward, awardParam);
          gameObject = this.mRewardAward;
          break;
        case RaidRewardType.Unit:
          UnitParam unitParam = instance.GetUnitParam(reward.IName);
          if (unitParam == null)
            return;
          DataSource.Bind<UnitParam>(this.mRewardUnit, unitParam);
          gameObject = this.mRewardUnit;
          break;
        case RaidRewardType.ConceptCard:
          ConceptCardData cardDataForDisplay = ConceptCardData.CreateConceptCardDataForDisplay(reward.IName);
          if (cardDataForDisplay == null)
            return;
          ConceptCardIcon component1 = this.mRewardCard.GetComponent<ConceptCardIcon>();
          if (Object.op_Inequality((Object) component1, (Object) null))
            component1.Setup(cardDataForDisplay);
          gameObject = this.mRewardCard;
          flag = true;
          break;
        case RaidRewardType.Artifact:
          ArtifactParam artifactParam = instance.MasterParam.GetArtifactParam(reward.IName);
          if (artifactParam == null)
            return;
          DataSource.Bind<ArtifactParam>(this.mRewardArtifact, artifactParam);
          gameObject = this.mRewardArtifact;
          break;
      }
      if (!Object.op_Inequality((Object) gameObject, (Object) null))
        return;
      gameObject.SetActive(true);
      if (!flag)
        return;
      Transform transform = gameObject.transform.Find("amount/Text_amount");
      if (!Object.op_Inequality((Object) transform, (Object) null))
        return;
      Text component2 = ((Component) transform).GetComponent<Text>();
      if (!Object.op_Inequality((Object) component2, (Object) null))
        return;
      component2.text = reward.Num.ToString();
    }

    public void Initialize(int raid_reward_type, string iname, int num)
    {
      JSON_RaidReward json = new JSON_RaidReward()
      {
        item_type = raid_reward_type,
        item_iname = iname,
        item_num = num
      };
      RaidReward reward = new RaidReward();
      reward.Deserialize(json);
      this.Initialize(reward);
    }
  }
}
