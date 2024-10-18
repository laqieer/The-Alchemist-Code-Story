// Decompiled with JetBrains decompiler
// Type: SRPG.RaidGuildRankingWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "初期化", FlowNode.PinTypes.Input, 1)]
  public class RaidGuildRankingWindow : MonoBehaviour, IFlowInterface
  {
    private const int PIN_INPUT_INIT = 1;
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
    private static RaidGuildRankingWindow mInstance;
    private RaidRankingGuildList mRankingBeat;
    private RaidRankingGuildList mRankingRescue;
    private List<GameObject> mBeatList = new List<GameObject>();
    private List<GameObject> mRescueList = new List<GameObject>();
    private GuildEmblemParam[] mEmblems;

    public static RaidGuildRankingWindow Instance => RaidGuildRankingWindow.mInstance;

    private void Awake()
    {
      RaidGuildRankingWindow.mInstance = this;
      this.mBeatItem.SetActive(false);
      this.mRescueItem.SetActive(false);
      this.mEmblems = MonoSingleton<GameManager>.Instance.MasterParam.GetGuildEmblemes();
    }

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.Init();
    }

    public void Setup(ReqRaidRankingGuild.Response json)
    {
      if (json == null)
        return;
      if (json.beat != null)
      {
        this.mRankingBeat = new RaidRankingGuildList();
        if (!this.mRankingBeat.Deserialize(json.beat))
          return;
      }
      if (json.rescue == null)
        return;
      this.mRankingRescue = new RaidRankingGuildList();
      if (this.mRankingRescue.Deserialize(json.rescue))
        ;
    }

    private void Init()
    {
      this.Init_List(this.mRankingBeat, this.mBeatList, this.mBeatItem, this.mBeatParent, this.mBeatSelf);
      this.Init_List(this.mRankingRescue, this.mRescueList, this.mRescueItem, this.mRescueParent, this.mRescueSelf);
    }

    private void Init_List(
      RaidRankingGuildList rankingList,
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
      DataSource.Bind<RaidRankingGuildData>(self, rankingList.MyGuildInfo);
      DataSource.Bind<ViewGuildData>(self, rankingList.MyGuildInfo.ViewGuild);
      if (rankingList.MyGuildInfo.ViewGuild != null)
      {
        GuildEmblemParam data = Array.Find<GuildEmblemParam>(this.mEmblems, (Predicate<GuildEmblemParam>) (emb => emb.Image == rankingList.MyGuildInfo.ViewGuild.award_id));
        DataSource.Bind<GuildEmblemParam>(self, data);
      }
      self.SetActive(true);
      GameParameter.UpdateAll(self);
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
  }
}
