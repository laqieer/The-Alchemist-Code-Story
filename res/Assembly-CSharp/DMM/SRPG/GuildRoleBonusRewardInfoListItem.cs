// Decompiled with JetBrains decompiler
// Type: SRPG.GuildRoleBonusRewardInfoListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class GuildRoleBonusRewardInfoListItem : MonoBehaviour
  {
    [SerializeField]
    [Header("報酬条件施設レベルテキスト")]
    private Text CondtionsText;
    [SerializeField]
    [Header("報酬アイコンの親")]
    private Transform ListItemRoot;
    [SerializeField]
    [Header("報酬アイコン")]
    private GameObject ListItemTemplate;

    private void Awake() => GameUtility.SetGameObjectActive(this.ListItemTemplate, false);

    public void Setup(int facility_lv, GuildRoleBonusRewardParam param)
    {
      this.Refresh(facility_lv, param);
    }

    private void Refresh(int facility_lv, GuildRoleBonusRewardParam param)
    {
      if (Object.op_Inequality((Object) this.CondtionsText, (Object) null))
        this.CondtionsText.text = facility_lv.ToString();
      if (!Object.op_Inequality((Object) this.ListItemTemplate, (Object) null) || !Object.op_Inequality((Object) this.ListItemRoot, (Object) null) || param.rewards == null)
        return;
      foreach (GuildRoleBonusReward reward in param.rewards)
      {
        GameObject gameObject = Object.Instantiate<GameObject>(this.ListItemTemplate);
        if (Object.op_Inequality((Object) gameObject, (Object) null))
        {
          gameObject.transform.SetParent(this.ListItemRoot, false);
          RewardListItem component = gameObject.GetComponent<RewardListItem>();
          if (Object.op_Inequality((Object) component, (Object) null))
            this.SetRewardIcon(reward, component);
        }
        GameUtility.SetGameObjectActive(gameObject, true);
      }
    }

    private void SetRewardIcon(GuildRoleBonusReward reward, RewardListItem listitem)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      GameObject gameObject = (GameObject) null;
      listitem.AllNotActive();
      bool flag = false;
      switch (reward.type)
      {
        case RaidRewardType.Item:
          ItemParam itemParam = instance.GetItemParam(reward.item_iname);
          if (itemParam == null)
            return;
          gameObject = listitem.RewardItem;
          flag = true;
          DataSource.Bind<ItemParam>(gameObject, itemParam);
          break;
        case RaidRewardType.Gold:
          gameObject = listitem.RewardGold;
          flag = true;
          break;
        case RaidRewardType.Coin:
          gameObject = listitem.RewardCoin;
          flag = true;
          break;
        case RaidRewardType.Award:
          AwardParam awardParam = instance.GetAwardParam(reward.item_iname);
          if (awardParam == null)
            return;
          gameObject = listitem.RewardAward;
          DataSource.Bind<AwardParam>(gameObject, awardParam);
          break;
        case RaidRewardType.Unit:
          UnitParam unitParam = instance.GetUnitParam(reward.item_iname);
          if (unitParam == null)
            return;
          gameObject = listitem.RewardUnit;
          DataSource.Bind<UnitParam>(gameObject, unitParam);
          break;
        case RaidRewardType.ConceptCard:
          ConceptCardData cardDataForDisplay = ConceptCardData.CreateConceptCardDataForDisplay(reward.item_iname);
          if (cardDataForDisplay == null)
            return;
          gameObject = listitem.RewardCard;
          ConceptCardIcon component1 = gameObject.GetComponent<ConceptCardIcon>();
          if (Object.op_Inequality((Object) component1, (Object) null))
            component1.Setup(cardDataForDisplay);
          flag = true;
          break;
        case RaidRewardType.Artifact:
          ArtifactParam artifactParam = instance.MasterParam.GetArtifactParam(reward.item_iname);
          if (artifactParam == null)
            return;
          gameObject = listitem.RewardArtifact;
          DataSource.Bind<ArtifactParam>(gameObject, artifactParam);
          break;
      }
      if (Object.op_Inequality((Object) gameObject, (Object) null) && flag)
      {
        Transform transform = gameObject.transform.Find("amount/Text_amount");
        if (Object.op_Inequality((Object) transform, (Object) null))
        {
          Text component2 = ((Component) transform).GetComponent<Text>();
          if (Object.op_Inequality((Object) component2, (Object) null))
            component2.text = reward.num.ToString();
        }
      }
      GameUtility.SetGameObjectActive(gameObject, true);
    }
  }
}
