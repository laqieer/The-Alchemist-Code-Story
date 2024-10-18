// Decompiled with JetBrains decompiler
// Type: SRPG.GvGLeagueRanking
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(10, "初期化", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(11, "表示更新", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(100, "リーグの情報を取得", FlowNode.PinTypes.Output, 100)]
  public class GvGLeagueRanking : MonoBehaviour, IFlowInterface
  {
    private const int PIN_INPUT_INIT = 10;
    private const int PIN_INPUT_REFRESH = 11;
    private const int PIN_OUTPUT_REQUEST_LEAGUE = 100;
    [SerializeField]
    private Toggle m_AllLeagueTab;
    [SerializeField]
    private Toggle m_MyLeagueTab;
    [SerializeField]
    private Toggle m_SelectLeagueTab;
    [SerializeField]
    private GvGLeagueRanking.RankingUIRef m_AllLeagueObjects;
    [SerializeField]
    private GvGLeagueRanking.RankingUIRef m_MyLeagueObjects;
    [SerializeField]
    private GvGLeagueRanking.RankingUIRef m_SelectLeagueObjects;
    [SerializeField]
    private FlowNode_ReqGvGLeague m_RequestNode;
    [SerializeField]
    private ScrollablePulldown m_LeagueSelector;
    [SerializeField]
    private SerializeValueBehaviour m_SerializeValue;
    [SerializeField]
    private int REQUEST_GUILD_LIMIT = 20;
    private GvGLeagueRanking.GvGRankingTab m_ActiveTabType;
    private string m_SelectedLeagueID;
    private int m_StartRank = 1;
    private bool m_IsWaitingServerResponse;
    private bool m_IsNeedHigherRank;
    private bool m_IsNeedLowerRank;
    private int m_RecievedTopRank;
    private int m_RecievedWorstRank;
    private int m_RecievedGuildDataCount;
    private int m_CurrentLeagueGuildNum;
    private int m_TopGuildID;
    private bool m_IsRequestTop;
    private bool m_IsFirstViewMyLeague = true;
    private Vector2 m_UpdateLowerRankPos;
    private Vector2 m_UpdateHigherRankPos;
    private List<GvGLeagueRankingContent.ContentParm> mGvGLeagueRankingContentParam = new List<GvGLeagueRankingContent.ContentParm>();

    private GvGLeagueRanking.RankingUIRef ActiveRankingObject
    {
      get
      {
        switch (this.m_ActiveTabType)
        {
          case GvGLeagueRanking.GvGRankingTab.AllLeague:
            return this.m_AllLeagueObjects;
          case GvGLeagueRanking.GvGRankingTab.MyLeague:
            return this.m_MyLeagueObjects;
          case GvGLeagueRanking.GvGRankingTab.SelectLeague:
            return this.m_SelectLeagueObjects;
          default:
            return (GvGLeagueRanking.RankingUIRef) null;
        }
      }
    }

    private ContentController ActiveContentController
    {
      get
      {
        return this.ActiveRankingObject == null ? (ContentController) null : this.ActiveRankingObject.m_ContentController;
      }
    }

    private ScrollRect ActiveScrollRect
    {
      get
      {
        return this.ActiveRankingObject == null ? (ScrollRect) null : this.ActiveRankingObject.m_ScrollRect;
      }
    }

    private void Start()
    {
      this.SetupLeagueSelector();
      // ISSUE: method pointer
      ((UnityEvent<bool>) this.m_AllLeagueTab.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(\u003CStart\u003Em__0)));
      // ISSUE: method pointer
      ((UnityEvent<bool>) this.m_MyLeagueTab.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(\u003CStart\u003Em__1)));
      // ISSUE: method pointer
      ((UnityEvent<bool>) this.m_SelectLeagueTab.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(\u003CStart\u003Em__2)));
      this.SelectTab(GvGLeagueRanking.GvGRankingTab.AllLeague);
    }

    public void Activated(int pinID)
    {
      if (pinID != 10)
      {
        if (pinID != 11)
          return;
        this.Refresh();
        this.m_IsWaitingServerResponse = false;
      }
      else
      {
        this.Init();
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      }
    }

    private void Init() => this.RequestLeagueData(this.m_StartRank);

    private void InitializeDefaultSelectLeagueID()
    {
      GvGLeagueManager.GvGLeagueDataList allLeagueData = GvGLeagueManager.Instance.GetAllLeagueData();
      if (allLeagueData == null)
        return;
      GvGLeagueViewGuild[] leagueGuilds = allLeagueData.GetLeagueGuilds();
      if (leagueGuilds.Length < 1 || leagueGuilds[0].league == null)
        return;
      this.m_SelectedLeagueID = GvGLeagueParam.GetGvGLeagueId(leagueGuilds[0].league.Rate);
      int index = GvGLeagueParam.GetGvGLeagueParamAll().FindIndex((Predicate<GvGLeagueParam>) (param => param.Id == this.m_SelectedLeagueID));
      if (index != -1)
        this.m_LeagueSelector.Selection = index;
      else
        DebugUtility.LogError(string.Format("一致するリーグIDが見つからなかった。=> league_id : \"{0}\"", (object) this.m_SelectedLeagueID));
    }

    private void Refresh()
    {
      if (string.IsNullOrEmpty(this.m_SelectedLeagueID))
        this.InitializeDefaultSelectLeagueID();
      this.CreateRanking();
    }

    private void Update()
    {
      if (this.m_IsWaitingServerResponse || !this.m_IsNeedLowerRank && !this.m_IsNeedHigherRank || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ActiveScrollRect, (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ActiveContentController, (UnityEngine.Object) null))
        return;
      Vector2 anchorePos = this.ActiveContentController.GetAnchorePos();
      if ((double) anchorePos.y >= (double) this.m_UpdateLowerRankPos.y && this.m_IsNeedLowerRank)
      {
        this.RequestLeagueData(Math.Min(this.m_RecievedWorstRank, this.m_CurrentLeagueGuildNum));
      }
      else
      {
        if ((double) anchorePos.y > (double) this.m_UpdateHigherRankPos.y || !this.m_IsNeedHigherRank)
          return;
        int startRank = Math.Max(1, this.m_RecievedTopRank - this.REQUEST_GUILD_LIMIT);
        this.m_IsRequestTop = true;
        this.RequestLeagueData(startRank);
      }
    }

    private void CreateRanking()
    {
      GvGLeagueManager.GvGLeagueDataList gvGleagueDataList = (GvGLeagueManager.GvGLeagueDataList) null;
      GvGLeagueViewGuild[] gleagueViewGuildArray = (GvGLeagueViewGuild[]) null;
      switch (this.m_ActiveTabType)
      {
        case GvGLeagueRanking.GvGRankingTab.AllLeague:
          gvGleagueDataList = GvGLeagueManager.Instance.AllLeagueData;
          gleagueViewGuildArray = gvGleagueDataList == null ? (GvGLeagueViewGuild[]) null : gvGleagueDataList.GetLeagueGuilds();
          break;
        case GvGLeagueRanking.GvGRankingTab.MyLeague:
          if (GvGLeagueManager.Instance.MyGuildLeagueData != null)
          {
            gvGleagueDataList = GvGLeagueManager.Instance.GetLeagueData(GvGLeagueManager.Instance.MyGuildLeagueData.league.Id);
            gleagueViewGuildArray = gvGleagueDataList == null ? (GvGLeagueViewGuild[]) null : gvGleagueDataList.GetLeagueGuilds();
            break;
          }
          break;
        case GvGLeagueRanking.GvGRankingTab.SelectLeague:
          gvGleagueDataList = GvGLeagueManager.Instance.GetLeagueData(this.m_SelectedLeagueID);
          gleagueViewGuildArray = gvGleagueDataList == null ? (GvGLeagueViewGuild[]) null : gvGleagueDataList.GetLeagueGuilds();
          break;
      }
      this.m_CurrentLeagueGuildNum = gvGleagueDataList == null ? 0 : gvGleagueDataList.TotalLeagueCount;
      ContentSource source = new ContentSource();
      this.mGvGLeagueRankingContentParam.Clear();
      if (gleagueViewGuildArray != null)
      {
        for (int index = 0; index < gleagueViewGuildArray.Length; ++index)
        {
          if (gleagueViewGuildArray[index] != null)
          {
            GvGLeagueRankingContent.ContentParm contentParm = new GvGLeagueRankingContent.ContentParm()
            {
              mGvGLeagueData = gleagueViewGuildArray[index],
              onListItemClick = new GvGLeagueRankingContent.OnListItemClick(this.OnListItemClick)
            };
            contentParm.Initialize(source);
            this.mGvGLeagueRankingContentParam.Add(contentParm);
          }
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ActiveContentController, (UnityEngine.Object) null))
      {
        Vector2 anchorePos = this.ActiveContentController.GetAnchorePos();
        source.SetTable((ContentSource.Param[]) this.mGvGLeagueRankingContentParam.ToArray());
        int offset_pos_guild_id = -1;
        if (this.m_ActiveTabType == GvGLeagueRanking.GvGRankingTab.MyLeague && this.m_IsFirstViewMyLeague)
        {
          this.m_IsFirstViewMyLeague = false;
          offset_pos_guild_id = MonoSingleton<GameManager>.Instance.Player.PlayerGuild.Gid;
        }
        if (this.m_IsRequestTop)
        {
          this.m_IsRequestTop = false;
          offset_pos_guild_id = this.m_TopGuildID;
        }
        if (offset_pos_guild_id != -1)
        {
          int index = this.mGvGLeagueRankingContentParam.FindIndex((Predicate<GvGLeagueRankingContent.ContentParm>) (param => param.mGvGLeagueData.id == offset_pos_guild_id));
          if (index != -1)
          {
            float num1 = this.ActiveContentController.m_Spacing.y * (float) (index + 1);
            float num2 = this.ActiveContentController.m_CellSize.y * (float) index;
            anchorePos.y = num1 + num2;
          }
        }
        this.ActiveContentController.Initialize(source, anchorePos);
        this.ActiveContentController.ForceUpdate();
      }
      this.m_IsNeedLowerRank = false;
      this.m_IsNeedHigherRank = false;
      if (gleagueViewGuildArray == null || gleagueViewGuildArray.Length < 1 || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ActiveContentController, (UnityEngine.Object) null) || gvGleagueDataList == null || gleagueViewGuildArray.Length == gvGleagueDataList.TotalLeagueCount)
        return;
      float num3 = this.ActiveContentController.m_Spacing.y + this.ActiveContentController.m_CellSize.y;
      int num4 = 0;
      if ((double) num3 != 0.0)
        num4 = (int) ((double) this.ActiveContentController.GetAnchorePos().y / (double) num3);
      int index1 = Mathf.Min(num4, gleagueViewGuildArray.Length);
      this.m_UpdateLowerRankPos = Vector2.zero;
      this.m_UpdateHigherRankPos = Vector2.zero;
      int num5 = Mathf.Max(gleagueViewGuildArray[index1].league.Rank - 1, 0);
      int num6 = index1;
      for (int index2 = index1; index2 < gleagueViewGuildArray.Length && num5 + 1 == gleagueViewGuildArray[index2].league.Rank; ++index2)
      {
        num6 = index2;
        num5 = gleagueViewGuildArray[index2].league.Rank;
      }
      ref Vector2 local = ref this.m_UpdateLowerRankPos;
      double num7 = (double) num3 * (double) Mathf.Max(num6 - 1, 0);
      Rect rect = this.ActiveScrollRect.viewport.rect;
      double height = (double) ((Rect) ref rect).height;
      double num8 = num7 - height;
      local.y = (float) num8;
      this.m_RecievedWorstRank = num5;
      this.m_IsNeedLowerRank = this.m_RecievedWorstRank != gvGleagueDataList.TotalLeagueCount;
      int num9 = gleagueViewGuildArray[index1].league.Rank + 1;
      int index3 = index1;
      for (int index4 = index1; index4 >= 0 && num9 - 1 == gleagueViewGuildArray[index4].league.Rank; --index4)
      {
        index3 = index4;
        num9 = gleagueViewGuildArray[index4].league.Rank;
      }
      this.m_UpdateHigherRankPos.y = num3 * (float) Mathf.Min(index3 + 1, gleagueViewGuildArray.Length);
      this.m_RecievedTopRank = num9;
      this.m_IsNeedHigherRank = this.m_RecievedTopRank != 1;
      this.m_TopGuildID = gleagueViewGuildArray[index3].id;
    }

    private void SetupLeagueSelector()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_LeagueSelector, (UnityEngine.Object) null))
        return;
      this.m_LeagueSelector.ClearItems();
      List<GvGLeagueParam> league_params = GvGLeagueParam.GetGvGLeagueParamAll();
      if (league_params == null)
      {
        DebugUtility.LogError("league_params が null");
      }
      else
      {
        for (int index = 0; index < league_params.Count; ++index)
          this.m_LeagueSelector.AddItem(league_params[index].Name, index);
        this.m_LeagueSelector.Selection = 0;
        ScrollablePulldown leagueSelector = this.m_LeagueSelector;
        leagueSelector.OnSelectionChangeDelegate = leagueSelector.OnSelectionChangeDelegate + (ScrollablePulldownBase.SelectItemEvent) (index => this.OnSelectLeague(league_params[index]));
      }
    }

    private void SelectLeague(string leagueID)
    {
      this.m_SelectedLeagueID = leagueID;
      if (GvGLeagueManager.Instance.GetLeagueData(this.m_SelectedLeagueID) == null)
        this.RequestLeagueData(this.m_StartRank);
      else
        this.Refresh();
    }

    private void SelectTab(GvGLeagueRanking.GvGRankingTab tab)
    {
      this.m_ActiveTabType = tab;
      GameUtility.SetGameObjectActive(this.m_AllLeagueObjects.m_RankingRoot, tab == GvGLeagueRanking.GvGRankingTab.AllLeague);
      GameUtility.SetGameObjectActive(this.m_MyLeagueObjects.m_RankingRoot, tab == GvGLeagueRanking.GvGRankingTab.MyLeague);
      GameUtility.SetGameObjectActive(this.m_SelectLeagueObjects.m_RankingRoot, tab == GvGLeagueRanking.GvGRankingTab.SelectLeague);
    }

    private void RequestLeagueData(int startRank)
    {
      switch (this.m_ActiveTabType)
      {
        case GvGLeagueRanking.GvGRankingTab.AllLeague:
          this.m_RequestNode.SetRequestAllLeague(startRank, this.REQUEST_GUILD_LIMIT);
          this.m_IsWaitingServerResponse = true;
          break;
        case GvGLeagueRanking.GvGRankingTab.MyLeague:
          this.m_RequestNode.SetRequestMyLeague(startRank, this.REQUEST_GUILD_LIMIT);
          this.m_IsWaitingServerResponse = true;
          break;
        case GvGLeagueRanking.GvGRankingTab.SelectLeague:
          this.m_RequestNode.SetRequesTargetLeague(this.m_SelectedLeagueID, startRank, this.REQUEST_GUILD_LIMIT);
          this.m_IsWaitingServerResponse = true;
          break;
      }
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }

    private void OnListItemClick(GvGLeagueViewGuild leagueData)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_SerializeValue, (UnityEngine.Object) null) || leagueData == null)
        return;
      this.m_SerializeValue.list.SetField(GuildSVB_Key.GUILD_ID, leagueData.id);
    }

    private void OnSelectLeague(GvGLeagueParam param) => this.SelectLeague(param.Id);

    private void OnSelectTab(GvGLeagueRanking.GvGRankingTab tabType)
    {
      if (this.m_ActiveTabType == tabType)
        return;
      this.SelectTab(tabType);
      this.m_TopGuildID = -1;
      if (tabType == GvGLeagueRanking.GvGRankingTab.MyLeague)
        this.RequestLeagueData(0);
      else
        this.RequestLeagueData(this.m_StartRank);
    }

    private enum GvGRankingTab
    {
      AllLeague,
      MyLeague,
      SelectLeague,
    }

    [Serializable]
    private class RankingUIRef
    {
      public GameObject m_RankingRoot;
      public ScrollRect m_ScrollRect;
      public ContentController m_ContentController;
      private RectTransform m_ContentTransformCache;

      public RectTransform ContentTransform
      {
        get
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_ContentTransformCache, (UnityEngine.Object) null))
            return this.m_ContentTransformCache;
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_ContentController, (UnityEngine.Object) null))
            return (RectTransform) null;
          this.m_ContentTransformCache = ((Component) this.m_ContentController).GetComponent<RectTransform>();
          return this.m_ContentTransformCache;
        }
      }
    }
  }
}
