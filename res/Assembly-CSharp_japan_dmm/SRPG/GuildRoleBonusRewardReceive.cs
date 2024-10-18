// Decompiled with JetBrains decompiler
// Type: SRPG.GuildRoleBonusRewardReceive
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class GuildRoleBonusRewardReceive : MonoBehaviour
  {
    [SerializeField]
    [Header("ポップアップタイトルテキスト")]
    private Text TitleText;
    [SerializeField]
    [Header("報酬文言")]
    private Text MainText;
    [SerializeField]
    [Header("報酬リストアイテムの親")]
    private Transform RewardListRoot;
    [SerializeField]
    [Header("報酬リストアイテムのテンプレート")]
    private GameObject RewardListItemTemplate;
    [SerializeField]
    [Header("報酬受け取りオブジェクト")]
    private GameObject ReceivedStamp;
    [SerializeField]
    [Header("報酬受け取り済みオブジェクト")]
    private GameObject AlreadyStamp;

    private void Start() => this.Setup();

    private void Setup()
    {
      ReqGuildRoleBonus.Response response = FlowNode_ReqGuildRoleBonus.GetResponse();
      if (response == null)
        return;
      GuildRoleBonusReward[] guildRoleBonusRewardArray = new GuildRoleBonusReward[response.rewards.Length];
      for (int index = 0; index < response.rewards.Length; ++index)
      {
        GuildRoleBonusReward guildRoleBonusReward = new GuildRoleBonusReward();
        guildRoleBonusReward.Deserialize(response.rewards[index]);
        guildRoleBonusRewardArray[index] = guildRoleBonusReward;
      }
      bool flag = false;
      if (response.status == 1)
        flag = false;
      else if (response.status == 2)
        flag = true;
      if (guildRoleBonusRewardArray != null && Object.op_Inequality((Object) this.RewardListItemTemplate, (Object) null) && Object.op_Inequality((Object) this.RewardListRoot, (Object) null))
      {
        this.RewardListItemTemplate.SetActive(false);
        GameObject gameObject1 = !flag ? this.ReceivedStamp : this.AlreadyStamp;
        for (int index = 0; index < guildRoleBonusRewardArray.Length; ++index)
        {
          GuildRoleBonusReward reward = guildRoleBonusRewardArray[index];
          if (reward != null)
          {
            GameObject gameObject2 = Object.Instantiate<GameObject>(this.RewardListItemTemplate);
            if (Object.op_Inequality((Object) gameObject2, (Object) null))
            {
              RewardListItem component = gameObject2.GetComponent<RewardListItem>();
              if (Object.op_Inequality((Object) component, (Object) null))
                this.SetRewardIcon(reward, component);
              gameObject2.transform.SetParent(this.RewardListRoot, false);
            }
            GameObject gameObject3 = Object.Instantiate<GameObject>(gameObject1);
            if (Object.op_Inequality((Object) gameObject2, (Object) null) && Object.op_Inequality((Object) gameObject3, (Object) null))
            {
              gameObject3.transform.SetParent(gameObject2.transform, false);
              gameObject3.transform.SetAsLastSibling();
            }
            GameUtility.SetGameObjectActive(gameObject2, true);
            GameUtility.SetGameObjectActive(gameObject3, true);
          }
        }
      }
      if (Object.op_Inequality((Object) this.TitleText, (Object) null))
      {
        string empty = string.Empty;
        if (MonoSingleton<GameManager>.Instance.Player.PlayerGuild.IsGuildMaster)
          empty = LocalizedText.Get("sys.GUILDMASTERREWARDTITLE");
        else if (MonoSingleton<GameManager>.Instance.Player.PlayerGuild.IsSubGuildMaster)
          empty = LocalizedText.Get("sys.GUILDMASTERREWARDTITLE_SUB");
        if (!string.IsNullOrEmpty(empty))
          this.TitleText.text = empty;
      }
      if (Object.op_Inequality((Object) this.MainText, (Object) null))
      {
        string empty = string.Empty;
        if (MonoSingleton<GameManager>.Instance.Player.PlayerGuild.IsGuildMaster)
          empty = LocalizedText.Get("sys.GUILDMASTERREWARDTEXT");
        else if (MonoSingleton<GameManager>.Instance.Player.PlayerGuild.IsSubGuildMaster)
          empty = LocalizedText.Get("sys.GUILDMASTERREWARDTEXT_SUB");
        if (!string.IsNullOrEmpty(empty))
          this.MainText.text = empty;
      }
      FlowNode_ReqGuildRoleBonus.Clear();
    }

    private void SetRewardIcon(GuildRoleBonusReward reward, RewardListItem listitem)
    {
      if (reward == null || Object.op_Equality((Object) listitem, (Object) null))
        return;
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
          DataSource.Bind<ItemParam>(gameObject, itemParam);
          flag = true;
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
          flag = true;
          break;
        case RaidRewardType.GuildEmblem:
          gameObject = listitem.RewardEmblem;
          this.SetEmblem(gameObject, reward.item_iname);
          break;
      }
      if (flag)
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

    private void SetEmblem(GameObject obj, string iname)
    {
      if (Object.op_Equality((Object) obj, (Object) null) || string.IsNullOrEmpty(iname))
        return;
      Image component = obj.GetComponent<Image>();
      if (Object.op_Equality((Object) component, (Object) null))
        return;
      SpriteSheet spriteSheet = AssetManager.Load<SpriteSheet>("GuildEmblemImage/GuildEmblemes");
      if (!Object.op_Inequality((Object) spriteSheet, (Object) null))
        return;
      component.sprite = spriteSheet.GetSprite(iname);
      ((Behaviour) component).enabled = true;
    }

    public enum eReceiveStatus
    {
      NOT_RECEIVED = 1,
      ALREADY_RECEIVED = 2,
    }
  }
}
