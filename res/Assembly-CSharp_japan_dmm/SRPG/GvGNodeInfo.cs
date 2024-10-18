// Decompiled with JetBrains decompiler
// Type: SRPG.GvGNodeInfo
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
  [FlowNode.Pin(1, "Initialize", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(11, "Refresh", FlowNode.PinTypes.Input, 11)]
  public class GvGNodeInfo : MonoBehaviour, IFlowInterface
  {
    public const int PIN_INPUT_INIT = 1;
    public const int PIN_INPUT_REFRESH = 11;
    [SerializeField]
    private Text NodeNameText;
    [SerializeField]
    private RewardListItem RewardItem;
    [SerializeField]
    private GameObject MapParent;
    [SerializeField]
    private GameObject GuildParent;
    [SerializeField]
    private Text OccupyDay;
    [SerializeField]
    private Text OccupyPoint;
    [SerializeField]
    private Text OccupyHalfPoint;
    [SerializeField]
    private List<ImageArray> ImageArrayList = new List<ImageArray>();
    [Space(10f)]
    [SerializeField]
    private List<GameObject> NPCOffObjectList = new List<GameObject>();
    [Space(10f)]
    [SerializeField]
    private GameObject RemainDeclareCoolTime;
    [SerializeField]
    private Text RemainDeclareCoolTimeText;
    [SerializeField]
    private Button DeclareButton;
    [SerializeField]
    private Button AttackButton;
    [SerializeField]
    private Button DefenseSetButton;
    [Space(10f)]
    [SerializeField]
    private ImageArray mDefensePartyIcon;
    private GvGNodeData CurrentNode;
    private List<RewardListItem> RewardItemList = new List<RewardListItem>();

    public static GvGNodeInfo Instance { get; private set; }

    public long[] EditPartyIds { get; private set; }

    private void Awake()
    {
      GvGNodeInfo.Instance = this;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) GvGManager.Instance, (UnityEngine.Object) null))
        return;
      GvGManager.Instance.SetAutoRefreshStatus(GvGManager.GvGAutoRefreshState.Stop);
    }

    private void OnDestroy()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) GvGManager.Instance, (UnityEngine.Object) null))
        GvGManager.Instance.RevertAutoRefreshStatus();
      GvGNodeInfo.Instance = (GvGNodeInfo) null;
    }

    private void Update()
    {
      GvGManager instance = GvGManager.Instance;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance, (UnityEngine.Object) null))
        return;
      bool flag = instance.CanDeclareNode(this.CurrentNode);
      GameUtility.SetGameObjectActive(this.RemainDeclareCoolTime, flag && instance.CanDeclareCoolTime);
      GameUtility.SetGameObjectActive((Component) this.DeclareButton, flag && !instance.CanDeclareCoolTime);
      if (!instance.CanDeclareCoolTime || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RemainDeclareCoolTimeText, (UnityEngine.Object) null))
        return;
      this.RemainDeclareCoolTimeText.text = string.Format(LocalizedText.Get("sys.GVG_DECLARE_COOL_TIME"), (object) instance.DeclareCoolTime.Minutes, (object) instance.DeclareCoolTime.Seconds);
    }

    public void Activated(int pinID)
    {
      if (pinID != 1)
      {
        if (pinID != 11)
          return;
        this.Refresh();
      }
      else
      {
        this.Initialize();
        this.Refresh();
      }
    }

    private void Initialize()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) MonoSingleton<GameManager>.Instance, (UnityEngine.Object) null))
        return;
      GvGManager ggm = GvGManager.Instance;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) ggm, (UnityEngine.Object) null) || ggm.NodeDataList == null || ggm.OtherGuildList == null)
        return;
      this.CurrentNode = ggm.NodeDataList.Find((Predicate<GvGNodeData>) (n => n.NodeId == ggm.SelectNodeId));
      if (this.CurrentNode == null)
        return;
      ChangeMaterialList component = ((Component) this).gameObject.GetComponent<ChangeMaterialList>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
      {
        int num = ggm.GetMatchingOrderIndex(this.CurrentNode.GuildId);
        if (this.CurrentNode.GuildId == 0)
          num = ggm.GvGNPCColor;
        ggm.SetNodeColor(this.CurrentNode, component);
      }
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.MapParent, (UnityEngine.Object) null))
        return;
      DataSource.Bind<QuestParam>(this.MapParent, Array.Find<QuestParam>(MonoSingleton<GameManager>.Instance.Quests, (Predicate<QuestParam>) (q => q.iname == this.CurrentNode.NodeParam.QuestId)));
      GlobalVars.SelectedQuestID = this.CurrentNode.NodeParam.QuestId;
      this.EditPartyIds = new long[3];
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.RewardItem, (UnityEngine.Object) null))
        return;
      ((Component) this.RewardItem).gameObject.SetActive(false);
      this.RewardItemList.ForEach((Action<RewardListItem>) (ri => UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) ri).gameObject)));
      this.RewardItemList.Clear();
      if (ggm.GvGLeagueInfo == null)
        return;
      GvGRewardParam rewardNode = this.CurrentNode.NodeParam.GetRewardNode(ggm.GvGLeagueInfo.Id);
      if (rewardNode == null)
        return;
      for (int index = 0; index < rewardNode.Rewards.Count; ++index)
      {
        if (rewardNode.Rewards[index] != null)
        {
          RewardListItem listItem = UnityEngine.Object.Instantiate<RewardListItem>(this.RewardItem, ((Component) this.RewardItem).transform.parent);
          this.SetRewardIcon(rewardNode.Rewards[index], listItem);
          ((Component) listItem).gameObject.SetActive(true);
          this.RewardItemList.Add(listItem);
        }
      }
    }

    private void Refresh()
    {
      GvGManager gvgm = GvGManager.Instance;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) gvgm, (UnityEngine.Object) null))
        return;
      this.ImageArrayList.ForEach((Action<ImageArray>) (imageArray =>
      {
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) imageArray, (UnityEngine.Object) null))
          return;
        imageArray.ImageIndex = Mathf.Clamp((int) gvgm.GetNodeImageIndex(this.CurrentNode), 0, imageArray.Images.Length - 1);
      }));
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NodeNameText, (UnityEngine.Object) null))
        this.NodeNameText.text = this.CurrentNode.NodeParam.Name;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.GuildParent, (UnityEngine.Object) null))
        return;
      DataSource.Bind<ViewGuildData>(this.GuildParent, GvGManager.Instance.FindViewGuild(this.CurrentNode.GuildId));
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.OccupyDay, (UnityEngine.Object) null))
        return;
      if (this.CurrentNode.GuildId == 0)
      {
        this.OccupyDay.text = "-";
        this.NPCOffObjectList.ForEach((Action<GameObject>) (go => GameUtility.SetGameObjectActive(go, false)));
      }
      else
      {
        this.OccupyDay.text = (TimeManager.ServerTime - this.CurrentNode.CaptureTime).Days.ToString();
        this.NPCOffObjectList.ForEach((Action<GameObject>) (go => GameUtility.SetGameObjectActive(go, true)));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.OccupyPoint, (UnityEngine.Object) null))
        this.OccupyPoint.text = this.CurrentNode.NodeParam.Point.ToString();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.OccupyHalfPoint, (UnityEngine.Object) null))
        this.OccupyHalfPoint.text = this.CurrentNode.NodeParam.FirstHalfPoint.ToString();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mDefensePartyIcon, (UnityEngine.Object) null))
      {
        if (this.CurrentNode.DefensePartyNum == 0)
        {
          GameUtility.SetGameObjectActive((Component) this.mDefensePartyIcon, false);
        }
        else
        {
          GameUtility.SetGameObjectActive((Component) this.mDefensePartyIcon, true);
          this.mDefensePartyIcon.ImageIndex = GvGManager.Instance.GetDefensePartyIconIndex(this.CurrentNode);
        }
      }
      this.RefreshButtons();
    }

    private void SetRewardIcon(GvGRewardDetailParam reward, RewardListItem listItem)
    {
      if (reward == null || UnityEngine.Object.op_Equality((UnityEngine.Object) listItem, (UnityEngine.Object) null))
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      GameObject gameObject = (GameObject) null;
      bool flag = false;
      switch (reward.Type)
      {
        case RaidRewardType.Item:
          ItemParam itemParam = instance.GetItemParam(reward.IName);
          if (itemParam != null)
          {
            gameObject = listItem.RewardItem;
            flag = true;
            DataSource.Bind<ItemParam>(gameObject, itemParam);
            break;
          }
          break;
        case RaidRewardType.Gold:
          gameObject = listItem.RewardGold;
          flag = true;
          break;
        case RaidRewardType.Coin:
          gameObject = listItem.RewardCoin;
          flag = true;
          break;
        case RaidRewardType.Award:
          AwardParam awardParam = instance.GetAwardParam(reward.IName);
          if (awardParam == null)
            return;
          gameObject = listItem.RewardAward;
          DataSource.Bind<AwardParam>(gameObject, awardParam);
          break;
        case RaidRewardType.Unit:
          UnitParam unitParam = instance.GetUnitParam(reward.IName);
          if (unitParam != null)
          {
            gameObject = listItem.RewardUnit;
            DataSource.Bind<UnitParam>(gameObject, unitParam);
            break;
          }
          break;
        case RaidRewardType.ConceptCard:
          ConceptCardData cardDataForDisplay = ConceptCardData.CreateConceptCardDataForDisplay(reward.IName);
          if (cardDataForDisplay != null)
          {
            gameObject = listItem.RewardCard;
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
          ArtifactParam artifactParam = instance.MasterParam.GetArtifactParam(reward.IName);
          if (artifactParam != null)
          {
            gameObject = listItem.RewardArtifact;
            DataSource.Bind<ArtifactParam>(gameObject, artifactParam);
            break;
          }
          break;
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
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
        return;
      gameObject.transform.SetParent(listItem.RewardList, false);
      gameObject.SetActive(true);
    }

    private void RefreshButtons()
    {
      GvGManager instance = GvGManager.Instance;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance, (UnityEngine.Object) null))
        return;
      GameUtility.SetGameObjectActive((Component) this.DeclareButton, instance.CanDeclareNode(this.CurrentNode) && !instance.CanDeclareCoolTime);
      GameUtility.SetGameObjectActive((Component) this.AttackButton, instance.CanAttackNode(this.CurrentNode));
      GameUtility.SetGameObjectActive((Component) this.DefenseSetButton, this.CurrentNode.State == GvGNodeState.OccupySelf || this.CurrentNode.State == GvGNodeState.DeclaredEnemy);
    }

    public void OnDeclareCoolTimeButton()
    {
      UIUtility.SystemMessage(LocalizedText.Get("sys.GVG_DECLARE_COOLTIME_MSG"), (UIUtility.DialogResultEvent) null, systemModal: true);
    }

    public void SetEditParty(long[] units) => units.CopyTo((Array) this.EditPartyIds, 0);
  }
}
