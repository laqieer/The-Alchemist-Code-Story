// Decompiled with JetBrains decompiler
// Type: SRPG.WorldRaidRankingWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(10, "ランキング情報初期化", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(11, "ダメージランキング表示", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(12, "ランキング報酬表示", FlowNode.PinTypes.Input, 12)]
  [FlowNode.Pin(13, "表示情報更新", FlowNode.PinTypes.Input, 13)]
  [FlowNode.Pin(100, "ランキング情報取得", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "入力制限", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "入力制限解除", FlowNode.PinTypes.Output, 102)]
  public class WorldRaidRankingWindow : MonoBehaviour, IFlowInterface
  {
    private const int PIN_INPUT_INIT_RANKING = 10;
    private const int PIN_INPUT_DAMAGE_RANKING = 11;
    private const int PIN_INPUT_RANKING_REWARD = 12;
    private const int PIN_INPUT_UPDATE_VIEW = 13;
    private const int PIN_OUTPUT_REQUEST_RANKING = 100;
    private const int PIN_OUTPUT_INPUT_LIMITATION = 101;
    private const int PIN_OUTPUT_INPUT_RELEASE = 102;
    private const string SVB_NAME_MASK_OBJ = "BossMask";
    private const string SVB_NAME_SELECT_OBJ = "BossSelect";
    private const string SVB_NAME_BOSS_SELECT_IMG = "BossSelectImg";
    private const string SVB_NAME_BOSS_MASK_IMG = "BossMaskImg";
    [SerializeField]
    private GameObject mRankingTop;
    [SerializeField]
    private GameObject mRewardTop;
    [SerializeField]
    private GameObject mWorldRaidRoot;
    [SerializeField]
    private GameObject mRewardRoot;
    [SerializeField]
    private SRPG_Button mBossDataTemplate;
    [SerializeField]
    private WorldRaidRankingContentNode mRankingDataTemplate;
    [SerializeField]
    private WorldRaidRewardContent mRewardDataTemplate;
    [SerializeField]
    private WorldRaidRankingContentNode mPlayerRankingData;
    [SerializeField]
    private SRPG_ScrollRect mRewardScrollRect;
    private static WorldRaidRankingWindow mInstance;
    private List<WorldRaidRankingData> mWorldRaidRankings = new List<WorldRaidRankingData>();
    private WorldRaidRankingData mMyWorldRaidRanking;
    private int mTotalPage;
    private WorldRaidBossParam mCurrentWorldRaidBossParam;
    private int mSelectTab = 11;
    private string mViewWorldRaidBossId = string.Empty;
    private int mNowPage = 1;
    private bool mIsRequestRankingAdd;
    private List<WorldRaidRewardContent> mWorldRaidRewardContents = new List<WorldRaidRewardContent>();
    private List<GameObject> mWorldRaidBossContents = new List<GameObject>();
    [SerializeField]
    private ContentController mContentController;
    private List<WorldRaidRankingParam> mWorldRaidRankingParam = new List<WorldRaidRankingParam>();
    private Vector2 mAnchorPosition = Vector2.zero;

    public static WorldRaidRankingWindow Instance => WorldRaidRankingWindow.mInstance;

    private void Awake()
    {
      WorldRaidRankingWindow.mInstance = this;
      GameUtility.SetGameObjectActive((Component) this.mBossDataTemplate, false);
      GameUtility.SetGameObjectActive((Component) this.mRankingDataTemplate, false);
      GameUtility.SetGameObjectActive((Component) this.mRewardDataTemplate, false);
    }

    private void OnDestroy() => WorldRaidRankingWindow.mInstance = (WorldRaidRankingWindow) null;

    private void Update()
    {
      if (this.mSelectTab != 11 || this.mTotalPage <= this.mNowPage || this.mIsRequestRankingAdd || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mContentController, (UnityEngine.Object) null) || this.mContentController.GetLastPageGrid().y <= 0 || this.mContentController.GetGrid().y < this.mContentController.GetLastPageGrid().y)
        return;
      ++this.mNowPage;
      this.mIsRequestRankingAdd = true;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 10:
          this.Init();
          break;
        case 11:
          this.UpdateViewList(11);
          break;
        case 12:
          this.UpdateViewList(12);
          break;
        case 13:
          if (this.mNowPage > 1)
          {
            this.CreateDamageRanking();
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
            break;
          }
          this.UpdateViewList(this.mSelectTab);
          break;
      }
    }

    private void Init()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
      this.mCurrentWorldRaidBossParam = (WorldRaidBossParam) null;
      WorldRaidParam currentWorldRaidParam = WorldRaidManager.GetCurrentWorldRaidParam();
      List<WorldRaidBossChallengedData> challengedList = WorldRaidManager.GetChallengedList();
      if (currentWorldRaidParam != null)
      {
        foreach (WorldRaidParam.BossInfo bossInfo in currentWorldRaidParam.BossInfoList)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          WorldRaidRankingWindow.\u003CInit\u003Ec__AnonStorey0 initCAnonStorey0 = new WorldRaidRankingWindow.\u003CInit\u003Ec__AnonStorey0();
          // ISSUE: reference to a compiler-generated field
          initCAnonStorey0.boss_info = bossInfo;
          // ISSUE: reference to a compiler-generated field
          initCAnonStorey0.\u0024this = this;
          // ISSUE: reference to a compiler-generated method
          if (challengedList == null || challengedList.Find(new Predicate<WorldRaidBossChallengedData>(initCAnonStorey0.\u003C\u003Em__0)) != null)
          {
            SRPG_Button srpgButton = UnityEngine.Object.Instantiate<SRPG_Button>(this.mBossDataTemplate, this.mWorldRaidRoot.transform, false);
            // ISSUE: method pointer
            ((UnityEvent) srpgButton.onClick).AddListener(new UnityAction((object) initCAnonStorey0, __methodptr(\u003C\u003Em__1)));
            ((Component) srpgButton).gameObject.SetActive(true);
            SerializeValueBehaviour component1 = ((Component) srpgButton).GetComponent<SerializeValueBehaviour>();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null))
            {
              GameObject gameObject1 = component1.list.GetGameObject("BossSelectImg");
              GameObject gameObject2 = component1.list.GetGameObject("BossMaskImg");
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject1, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject2, (UnityEngine.Object) null))
              {
                // ISSUE: reference to a compiler-generated field
                Sprite worldRaidBossIcon = WorldRaidBossManager.GetWorldRaidBossIcon(initCAnonStorey0.boss_info.BossId);
                PolyImage component2 = gameObject1.GetComponent<PolyImage>();
                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component2, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) worldRaidBossIcon, (UnityEngine.Object) null))
                  component2.sprite = worldRaidBossIcon;
                PolyImage component3 = gameObject2.GetComponent<PolyImage>();
                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component3, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) worldRaidBossIcon, (UnityEngine.Object) null))
                  component3.sprite = worldRaidBossIcon;
              }
            }
            // ISSUE: reference to a compiler-generated field
            if (this.mCurrentWorldRaidBossParam == null || initCAnonStorey0.boss_info.IsLastBoss)
            {
              // ISSUE: reference to a compiler-generated field
              this.mCurrentWorldRaidBossParam = initCAnonStorey0.boss_info.BossParam;
              // ISSUE: reference to a compiler-generated field
              if (initCAnonStorey0.boss_info.IsLastBoss)
                ((Component) srpgButton).transform.SetSiblingIndex(0);
            }
            // ISSUE: reference to a compiler-generated field
            ((UnityEngine.Object) srpgButton).name = initCAnonStorey0.boss_info.BossId;
            this.mWorldRaidBossContents.Add(((Component) srpgButton).gameObject);
          }
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) WorldRaidBossManager.Instance, (UnityEngine.Object) null))
        this.mCurrentWorldRaidBossParam = WorldRaidBossManager.Instance.GetCurrentWorldRaidBossParam();
      this.BossSelect(this.mCurrentWorldRaidBossParam, true);
    }

    private void BossSelect(WorldRaidBossParam select_boss, bool is_force = false)
    {
      if (this.mIsRequestRankingAdd || !is_force && this.mCurrentWorldRaidBossParam.Iname == select_boss.Iname)
        return;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
      foreach (GameObject worldRaidBossContent in this.mWorldRaidBossContents)
      {
        if (!(((UnityEngine.Object) worldRaidBossContent).name != this.mCurrentWorldRaidBossParam.Iname) || !(((UnityEngine.Object) worldRaidBossContent).name != select_boss.Iname))
        {
          SerializeValueBehaviour component = worldRaidBossContent.GetComponent<SerializeValueBehaviour>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          {
            GameObject gameObject1 = component.list.GetGameObject("BossMask");
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject1, (UnityEngine.Object) null))
              gameObject1.SetActive(((UnityEngine.Object) worldRaidBossContent).name != select_boss.Iname);
            GameObject gameObject2 = component.list.GetGameObject(nameof (BossSelect));
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject2, (UnityEngine.Object) null))
              gameObject2.SetActive(((UnityEngine.Object) worldRaidBossContent).name == select_boss.Iname);
          }
        }
      }
      this.mCurrentWorldRaidBossParam = select_boss;
      this.mNowPage = 1;
      this.mIsRequestRankingAdd = true;
      this.mWorldRaidRankings = new List<WorldRaidRankingData>();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }

    private void UpdateViewList(int _select_tab)
    {
      if (this.mSelectTab == _select_tab && this.mViewWorldRaidBossId == this.mCurrentWorldRaidBossParam.Iname)
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
      }
      else
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
        this.mSelectTab = _select_tab;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mContentController, (UnityEngine.Object) null))
          this.mContentController.anchoredPosition = new Vector2(0.0f, 0.0f);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mRewardScrollRect, (UnityEngine.Object) null))
          this.mRewardScrollRect.SetNormalizedPosition(new Vector2(0.0f, 1f), true);
        if (this.mViewWorldRaidBossId != this.mCurrentWorldRaidBossParam.Iname)
          this.CreateRankingInfo();
        this.mViewWorldRaidBossId = this.mCurrentWorldRaidBossParam.Iname;
        this.mRankingTop.SetActive(_select_tab == 11);
        this.mRewardTop.SetActive(_select_tab == 12);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
      }
    }

    private void CreateRankingInfo()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPlayerRankingData, (UnityEngine.Object) null) && this.mMyWorldRaidRanking != null)
        this.mPlayerRankingData.Setup(this.mMyWorldRaidRanking);
      this.CreateDamageRanking();
      WorldRaidParam currentWorldRaidParam = WorldRaidManager.GetCurrentWorldRaidParam();
      if (currentWorldRaidParam == null)
        return;
      GameUtility.SetGameObjectActive(this.mRewardTop, false);
      foreach (Component raidRewardContent in this.mWorldRaidRewardContents)
        raidRewardContent.gameObject.SetActive(false);
      foreach (WorldRaidParam.BossInfo bossInfo in currentWorldRaidParam.BossInfoList)
      {
        if (!(this.mCurrentWorldRaidBossParam.Iname != bossInfo.BossId))
        {
          WorldRaidRankingRewardParam rankingRewardParam = WorldRaidRankingRewardParam.GetParam(bossInfo.RankRewardId);
          if (rankingRewardParam != null)
          {
            int count = this.mWorldRaidRewardContents.Count;
            int index = 0;
            using (List<WorldRaidRankingRewardParam.Reward>.Enumerator enumerator = rankingRewardParam.RewardList.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                WorldRaidRankingRewardParam.Reward current = enumerator.Current;
                WorldRaidRewardContent raidRewardContent;
                if (index < count)
                {
                  raidRewardContent = this.mWorldRaidRewardContents[index];
                  ++index;
                }
                else
                {
                  raidRewardContent = UnityEngine.Object.Instantiate<WorldRaidRewardContent>(this.mRewardDataTemplate, this.mRewardRoot.transform, false);
                  this.mWorldRaidRewardContents.Add(raidRewardContent);
                }
                raidRewardContent.Setup(current);
                ((Component) raidRewardContent).gameObject.SetActive(true);
              }
              break;
            }
          }
        }
      }
      GameUtility.SetGameObjectActive(this.mRewardTop, this.mSelectTab == 12);
    }

    private void CreateDamageRanking()
    {
      if (this.mWorldRaidRankings != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mContentController, (UnityEngine.Object) null))
      {
        this.mWorldRaidRankingParam.Clear();
        ContentSource source = new ContentSource();
        foreach (WorldRaidRankingData worldRaidRanking in this.mWorldRaidRankings)
        {
          WorldRaidRankingParam raidRankingParam = new WorldRaidRankingParam();
          raidRankingParam.WorldRaidRanking = worldRaidRanking;
          raidRankingParam.Initialize(source);
          this.mWorldRaidRankingParam.Add(raidRankingParam);
        }
        this.mAnchorPosition = this.mContentController.GetAnchorePos();
        source.SetTable((ContentSource.Param[]) this.mWorldRaidRankingParam.ToArray());
        this.mContentController.Initialize(source, this.mAnchorPosition);
        this.mContentController.ForceUpdate();
      }
      foreach (WorldRaidRankingParam raidRankingParam in this.mWorldRaidRankingParam)
        raidRankingParam.Refresh();
      this.mIsRequestRankingAdd = false;
    }

    public string GetTargetBossInfo()
    {
      return this.mCurrentWorldRaidBossParam == null ? string.Empty : this.mCurrentWorldRaidBossParam.Iname;
    }

    public int GetNowRankingPage() => this.mNowPage;

    public void SetRankingData(ReqWorldRaidRanking.Response worldraid_rankings)
    {
      if (this.mMyWorldRaidRanking == null)
        this.mMyWorldRaidRanking = new WorldRaidRankingData();
      if (worldraid_rankings.my_info != null)
        this.mMyWorldRaidRanking.Deserialize(worldraid_rankings.my_info);
      foreach (JSON_WorldRaidRankingData json in worldraid_rankings.worldraid_ranking)
      {
        WorldRaidRankingData worldRaidRankingData = new WorldRaidRankingData();
        if (worldRaidRankingData.Deserialize(json))
          this.mWorldRaidRankings.Add(worldRaidRankingData);
      }
      if (worldraid_rankings.total_page <= 0)
        return;
      this.mTotalPage = worldraid_rankings.total_page;
    }
  }
}
