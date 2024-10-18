// Decompiled with JetBrains decompiler
// Type: SRPG.RaidBeatRankingWindow
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
  [FlowNode.Pin(1, "ランキング初期化", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "初期化完了", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(100, "個人ランキング選択", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(101, "ギルドランキング選択", FlowNode.PinTypes.Input, 101)]
  public class RaidBeatRankingWindow : MonoBehaviour, IFlowInterface
  {
    private const int PIN_INPUT_INIT = 1;
    private const int PIN_OUTPUT_LOADCOMPLTE = 10;
    private const int PIN_INPUT_SELECT_PLAYER = 100;
    private const int PIN_INPUT_SELECT_GUILD = 101;
    [SerializeField]
    private Text mPlayerGuildName;
    [SerializeField]
    private Text mPlayerGuildLevel;
    [SerializeField]
    private GameObject mPlayerGuildEmblem;
    [SerializeField]
    private Text mPlayerGuildMasterName;
    [SerializeField]
    private Text mPlayerGuildMember;
    [SerializeField]
    private Text mPlayerGuildMaxMember;
    [SerializeField]
    private Text mPlayerGuildBeat;
    [SerializeField]
    private Text mPlayerGuildMaxRescue;
    [SerializeField]
    private GameObject mBeatItem;
    [SerializeField]
    private Transform mBeatParent;
    [SerializeField]
    private GameObject mBeatSelf;
    [SerializeField]
    private GameObject mRescueItem;
    [SerializeField]
    private Transform mRescueParent;
    [SerializeField]
    private GameObject mRescueSelf;
    [SerializeField]
    private GameObject mBeatRanking;
    [SerializeField]
    private GameObject mBeatGuildItem;
    [SerializeField]
    private Transform mBeatGuildParent;
    [SerializeField]
    private GameObject mBeatGuildSelf;
    [SerializeField]
    private GameObject mBeatGuildRanking;
    [SerializeField]
    private GameObject mBeatGuildNone;
    [SerializeField]
    private Button mSelectPlayerButton;
    [SerializeField]
    private Button mSelectGuildButton;
    [SerializeField]
    private float mSelScale = 1.1f;
    [SerializeField]
    private float mNonSelScale = 0.9f;
    private static RaidBeatRankingWindow mInstance;
    private RaidRankingList mRankingBeat;
    private RaidRankingList mRankingRescue;
    private List<GameObject> mBeatList = new List<GameObject>();
    private List<GameObject> mRescueList = new List<GameObject>();
    private RaidRankingGuildList mGuildRankingBeat;
    private RaidRankingGuildList mGuildRankingRescue;
    private List<GameObject> mBeatGuildList = new List<GameObject>();
    private GuildEmblemParam[] mEmblems;

    public static RaidBeatRankingWindow Instance => RaidBeatRankingWindow.mInstance;

    private void Awake()
    {
      RaidBeatRankingWindow.mInstance = this;
      this.mBeatItem.SetActive(false);
      this.mRescueItem.SetActive(false);
      this.mBeatGuildItem.SetActive(false);
      this.mEmblems = MonoSingleton<GameManager>.Instance.MasterParam.GetGuildEmblemes();
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          this.Init();
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
          break;
        case 100:
          this.SetScaleButton(true);
          break;
        case 101:
          this.SetScaleButton(false);
          break;
      }
    }

    public void Setup(ReqRaidRankingBeat.Response json)
    {
      if (json == null)
        return;
      if (json.beat != null)
      {
        this.mRankingBeat = new RaidRankingList();
        if (!this.mRankingBeat.Deserialize(json.beat))
          return;
      }
      if (json.rescue == null)
        return;
      this.mRankingRescue = new RaidRankingList();
      if (this.mRankingRescue.Deserialize(json.rescue))
        ;
    }

    public void Setup(ReqRaidRankingGuild.Response json)
    {
      if (json == null)
        return;
      if (json.beat != null)
      {
        this.mGuildRankingBeat = new RaidRankingGuildList();
        if (!this.mGuildRankingBeat.Deserialize(json.beat))
          return;
      }
      if (json.rescue == null)
        return;
      this.mGuildRankingRescue = new RaidRankingGuildList();
      if (this.mGuildRankingRescue.Deserialize(json.rescue))
        ;
    }

    private void Init()
    {
      this.InitBeat();
      this.InitGuild();
      this.InitPlayerInfo();
    }

    private void InitBeat()
    {
      this.Init_BeatList(this.mRankingBeat, this.mBeatList, this.mBeatItem, this.mBeatParent, this.mBeatSelf);
      this.Init_BeatList(this.mRankingRescue, this.mRescueList, this.mRescueItem, this.mRescueParent, this.mRescueSelf);
    }

    private void Init_BeatList(
      RaidRankingList rankingList,
      List<GameObject> currentList,
      GameObject item,
      Transform parent,
      GameObject self)
    {
      for (int index = 0; index < currentList.Count; ++index)
        UnityEngine.Object.Destroy((UnityEngine.Object) currentList[index]);
      currentList.Clear();
      if (rankingList == null || UnityEngine.Object.op_Equality((UnityEngine.Object) item, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) parent, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) self, (UnityEngine.Object) null))
        return;
      self.SetActive(false);
      DataSource.Bind<RaidRankingData>(self, rankingList.MyInfo);
      if (!string.IsNullOrEmpty(rankingList.MyInfo.SelectedAward))
      {
        AwardParam awardParam = MonoSingleton<GameManager>.Instance.GetAwardParam(rankingList.MyInfo.SelectedAward);
        DataSource.Bind<AwardParam>(self, awardParam);
      }
      if (rankingList.MyInfo.Unit != null)
        DataSource.Bind<UnitData>(self, rankingList.MyInfo.Unit);
      self.SetActive(true);
      GameParameter.UpdateAll(self);
      for (int index = 0; index < rankingList.Ranking.Count; ++index)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(item, parent);
        DataSource.Bind<RaidRankingData>(gameObject, rankingList.Ranking[index]);
        if (!string.IsNullOrEmpty(rankingList.Ranking[index].SelectedAward))
        {
          AwardParam awardParam = MonoSingleton<GameManager>.Instance.GetAwardParam(rankingList.Ranking[index].SelectedAward);
          DataSource.Bind<AwardParam>(gameObject, awardParam);
        }
        if (rankingList.Ranking[index].Unit != null)
          DataSource.Bind<UnitData>(gameObject, rankingList.Ranking[index].Unit);
        DataSource.Bind<ViewGuildData>(gameObject, rankingList.Ranking[index].ViewGuild);
        SerializeValueBehaviour component = gameObject.GetComponent<SerializeValueBehaviour>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null) && rankingList.Ranking[index].ViewGuild != null)
          component.list.SetField(GuildSVB_Key.GUILD_ID, rankingList.Ranking[index].ViewGuild.id);
        gameObject.SetActive(true);
      }
    }

    private void InitGuild()
    {
      this.Init_GuildList(this.mGuildRankingBeat, this.mBeatGuildList, this.mBeatGuildItem, this.mBeatGuildParent, this.mBeatGuildSelf);
    }

    private void Init_GuildList(
      RaidRankingGuildList rankingList,
      List<GameObject> currentList,
      GameObject item,
      Transform parent,
      GameObject self)
    {
      for (int index = 0; index < currentList.Count; ++index)
        UnityEngine.Object.Destroy((UnityEngine.Object) currentList[index]);
      currentList.Clear();
      if (rankingList == null || UnityEngine.Object.op_Equality((UnityEngine.Object) item, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) parent, (UnityEngine.Object) null))
        return;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self, (UnityEngine.Object) null))
      {
        self.SetActive(false);
        DataSource.Bind<RaidRankingGuildData>(self, rankingList.MyGuildInfo);
        DataSource.Bind<ViewGuildData>(self, rankingList.MyGuildInfo.ViewGuild);
        if (rankingList.MyGuildInfo.ViewGuild != null)
        {
          GuildEmblemParam data = Array.Find<GuildEmblemParam>(this.mEmblems, (Predicate<GuildEmblemParam>) (emb => emb.Image == rankingList.MyGuildInfo.ViewGuild.award_id));
          DataSource.Bind<GuildEmblemParam>(self, data);
        }
        self.SetActive(true);
        GameParameter.UpdateAll(self);
      }
      for (int i = 0; i < rankingList.Ranking.Count; ++i)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(item, parent);
        DataSource.Bind<RaidRankingGuildData>(gameObject, rankingList.Ranking[i]);
        DataSource.Bind<ViewGuildData>(gameObject, rankingList.Ranking[i].ViewGuild);
        if (rankingList.Ranking[i].ViewGuild != null)
        {
          GuildEmblemParam data = Array.Find<GuildEmblemParam>(this.mEmblems, (Predicate<GuildEmblemParam>) (emb => emb.Image == rankingList.Ranking[i].ViewGuild.award_id));
          DataSource.Bind<GuildEmblemParam>(gameObject, data);
        }
        gameObject.SetActive(true);
      }
    }

    private void InitPlayerInfo()
    {
      if (this.mRankingBeat != null)
      {
        Text componentInChildren = this.mBeatRanking.GetComponentInChildren<Text>();
        if (this.mRankingBeat.MyInfo.Rank != 0 && UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null))
          componentInChildren.text = string.Format(LocalizedText.Get("sys.RAID_RANKING_RANK"), (object) this.mRankingBeat.MyInfo.Rank);
      }
      if (this.mGuildRankingBeat == null)
        return;
      Text componentInChildren1 = this.mBeatGuildRanking.GetComponentInChildren<Text>();
      if (this.mGuildRankingBeat.MyGuildInfo.Rank != 0 && UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren1, (UnityEngine.Object) null))
        componentInChildren1.text = string.Format(LocalizedText.Get("sys.RAID_RANKING_RANK"), (object) this.mGuildRankingBeat.MyGuildInfo.Rank);
      if (this.mGuildRankingBeat.MyGuildInfo.ViewGuild != null)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBeatGuildNone, (UnityEngine.Object) null))
          this.mBeatGuildNone.SetActive(false);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPlayerGuildName, (UnityEngine.Object) null))
          this.mPlayerGuildName.text = this.mGuildRankingBeat.MyGuildInfo.ViewGuild.name;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPlayerGuildLevel, (UnityEngine.Object) null))
          this.mPlayerGuildLevel.text = this.mGuildRankingBeat.MyGuildInfo.ViewGuild.level.ToString();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPlayerGuildEmblem, (UnityEngine.Object) null))
          this.GetEmblem(this.mPlayerGuildEmblem, this.mGuildRankingBeat.MyGuildInfo.ViewGuild.award_id);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPlayerGuildBeat, (UnityEngine.Object) null))
          this.mPlayerGuildBeat.text = this.mGuildRankingBeat.MyGuildInfo.Score.ToString();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPlayerGuildMasterName, (UnityEngine.Object) null))
          this.mPlayerGuildMasterName.text = this.mGuildRankingBeat.MyGuildInfo.ViewGuild.guild_master;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPlayerGuildMember, (UnityEngine.Object) null))
          this.mPlayerGuildMember.text = this.mGuildRankingBeat.MyGuildInfo.ViewGuild.count.ToString();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPlayerGuildMaxMember, (UnityEngine.Object) null))
          this.mPlayerGuildMaxMember.text = this.mGuildRankingBeat.MyGuildInfo.ViewGuild.max_count.ToString();
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPlayerGuildMaxRescue, (UnityEngine.Object) null) || this.mGuildRankingRescue.MyGuildInfo == null)
          return;
        this.mPlayerGuildMaxRescue.text = this.mGuildRankingRescue.MyGuildInfo.Score.ToString();
      }
      else
      {
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBeatGuildNone, (UnityEngine.Object) null))
          return;
        this.mBeatGuildNone.SetActive(true);
      }
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

    private void SetScaleButton(bool player)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSelectPlayerButton, (UnityEngine.Object) null))
        ((Component) this.mSelectPlayerButton).GetComponent<Transform>().localScale = !player ? new Vector3(this.mNonSelScale, this.mNonSelScale, this.mNonSelScale) : new Vector3(this.mSelScale, this.mSelScale, this.mSelScale);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSelectGuildButton, (UnityEngine.Object) null))
        return;
      Transform component = ((Component) this.mSelectGuildButton).GetComponent<Transform>();
      if (player)
        component.localScale = new Vector3(this.mNonSelScale, this.mNonSelScale, this.mNonSelScale);
      else
        component.localScale = new Vector3(this.mSelScale, this.mSelScale, this.mSelScale);
    }
  }
}
