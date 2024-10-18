// Decompiled with JetBrains decompiler
// Type: SRPG.GuildAttendanceRewardReceive
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class GuildAttendanceRewardReceive : MonoBehaviour
  {
    [SerializeField]
    [Header("報酬アイコンのテンプレート")]
    private GameObject RewardTemplate;
    [SerializeField]
    [Header("報酬アイコンのRoot")]
    private Transform RewardListRoot;
    [SerializeField]
    [Header("報酬文言を表示するテキスト")]
    private Text RewardText;

    private void Awake() => GameUtility.SetGameObjectActive(this.RewardTemplate, false);

    private void Start() => this.Setup();

    private void Setup()
    {
      ReqGuildAttend.Response response = FlowNode_ReqGuildAttend.GetResponse();
      if (response == null || response.rewards == null)
      {
        FlowNode_ReqGuildAttend.Clear();
      }
      else
      {
        int yesterdayAttendance = response.yesterday_attendance;
        GuildAttendReward[] guildAttendRewardArray = new GuildAttendReward[response.rewards.Length];
        for (int index = 0; index < response.rewards.Length; ++index)
        {
          GuildAttendReward guildAttendReward = new GuildAttendReward();
          guildAttendReward.Deserialize(response.rewards[index]);
          guildAttendRewardArray[index] = guildAttendReward;
        }
        if (guildAttendRewardArray != null && Object.op_Inequality((Object) this.RewardTemplate, (Object) null) && Object.op_Inequality((Object) this.RewardListRoot, (Object) null))
        {
          for (int index = 0; index < guildAttendRewardArray.Length; ++index)
          {
            GuildAttendReward reward = guildAttendRewardArray[index];
            if (reward != null)
            {
              GameObject gameObject = Object.Instantiate<GameObject>(this.RewardTemplate);
              if (Object.op_Inequality((Object) gameObject, (Object) null))
              {
                RewardListItem component = gameObject.GetComponent<RewardListItem>();
                if (Object.op_Inequality((Object) component, (Object) null))
                  this.SetRewardIcon(reward, component);
                gameObject.transform.SetParent(this.RewardListRoot, false);
                GameUtility.SetGameObjectActive(gameObject, true);
              }
            }
          }
        }
        if (!Object.op_Inequality((Object) this.RewardText, (Object) null))
          return;
        string str = LocalizedText.Get("sys.GUILDATTENDANCEREWARD_TEXT", (object) yesterdayAttendance);
        this.RewardText.text = !string.IsNullOrEmpty(str) ? str : this.RewardText.text;
      }
    }

    private void SetRewardIcon(GuildAttendReward reward, RewardListItem listitem)
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
  }
}
