// Decompiled with JetBrains decompiler
// Type: SRPG.RaidBeatRankingWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "初期化", FlowNode.PinTypes.Input, 1)]
  public class RaidBeatRankingWindow : MonoBehaviour, IFlowInterface
  {
    private List<GameObject> mBeatList = new List<GameObject>();
    private List<GameObject> mRescueList = new List<GameObject>();
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
    private static RaidBeatRankingWindow mInstance;
    private RaidRankingList mRankingBeat;
    private RaidRankingList mRankingRescue;

    public static RaidBeatRankingWindow Instance
    {
      get
      {
        return RaidBeatRankingWindow.mInstance;
      }
    }

    private void Awake()
    {
      RaidBeatRankingWindow.mInstance = this;
      this.mBeatItem.SetActive(false);
      this.mRescueItem.SetActive(false);
    }

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.Init();
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
      if (!this.mRankingRescue.Deserialize(json.rescue))
        ;
    }

    private void Init()
    {
      this.Init_List(this.mRankingBeat, this.mBeatList, this.mBeatItem, this.mBeatParent, this.mBeatSelf);
      this.Init_List(this.mRankingRescue, this.mRescueList, this.mRescueItem, this.mRescueParent, this.mRescueSelf);
    }

    private void Init_List(RaidRankingList rankingList, List<GameObject> currentList, GameObject item, Transform parent, GameObject self)
    {
      for (int index = 0; index < currentList.Count; ++index)
        UnityEngine.Object.Destroy((UnityEngine.Object) currentList[index]);
      currentList.Clear();
      if (rankingList == null || (UnityEngine.Object) item == (UnityEngine.Object) null || ((UnityEngine.Object) parent == (UnityEngine.Object) null || (UnityEngine.Object) self == (UnityEngine.Object) null))
        return;
      self.SetActive(false);
      DataSource.Bind<RaidRankingData>(self, rankingList.MyInfo, false);
      if (!string.IsNullOrEmpty(rankingList.MyInfo.SelectedAward))
      {
        AwardParam awardParam = MonoSingleton<GameManager>.Instance.GetAwardParam(rankingList.MyInfo.SelectedAward);
        DataSource.Bind<AwardParam>(self, awardParam, false);
      }
      if (rankingList.MyInfo.Unit != null)
        DataSource.Bind<UnitData>(self, rankingList.MyInfo.Unit, false);
      self.SetActive(true);
      GameParameter.UpdateAll(self);
      for (int index = 0; index < rankingList.Ranking.Count; ++index)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(item, parent);
        DataSource.Bind<RaidRankingData>(gameObject, rankingList.Ranking[index], false);
        if (!string.IsNullOrEmpty(rankingList.Ranking[index].SelectedAward))
        {
          AwardParam awardParam = MonoSingleton<GameManager>.Instance.GetAwardParam(rankingList.Ranking[index].SelectedAward);
          DataSource.Bind<AwardParam>(gameObject, awardParam, false);
        }
        if (rankingList.Ranking[index].Unit != null)
          DataSource.Bind<UnitData>(gameObject, rankingList.Ranking[index].Unit, false);
        DataSource.Bind<ViewGuildData>(gameObject, rankingList.Ranking[index].ViewGuild, false);
        SerializeValueBehaviour component = gameObject.GetComponent<SerializeValueBehaviour>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null && rankingList.Ranking[index].ViewGuild != null)
          component.list.SetField(GuildSVB_Key.GUILD_ID, rankingList.Ranking[index].ViewGuild.id);
        gameObject.SetActive(true);
      }
    }
  }
}
