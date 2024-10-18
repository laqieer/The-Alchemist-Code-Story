// Decompiled with JetBrains decompiler
// Type: SRPG.CombatPowerRanking
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
  [FlowNode.Pin(100, "ギルドランキングを取得", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "メンバー別ランキングを取得", FlowNode.PinTypes.Output, 101)]
  public class CombatPowerRanking : MonoBehaviour, IFlowInterface
  {
    private const int PIN_INPUT_INIT = 10;
    private const int PIN_INPUT_REFRESH = 11;
    private const int PIN_OUTPUT_REQUEST_GUILD_RANKING = 100;
    private const int PIN_OUTPUT_REQUEST_MEMBER_RANKING = 101;
    [SerializeField]
    private Toggle m_AllPortTab;
    [SerializeField]
    private Toggle m_PortMembersTab;
    [SerializeField]
    private Button m_AllPortSelfItem;
    [SerializeField]
    private CombatPowerRanking.RankingUIRef m_AllPortObjects;
    [SerializeField]
    private CombatPowerRanking.RankingUIRef m_PortMembersObjects;
    [SerializeField]
    private FlowNode_ReqGuildRanking m_RequestNodeGuildRanking;
    [SerializeField]
    private FlowNode_ReqGuildRankingMembers m_RequestNodeGuildRankingMembers;
    [SerializeField]
    private SerializeValueBehaviour m_SerializeValue;
    [SerializeField]
    private CombatPowerRanking.GvGRankingTab m_ActiveTabType;
    private List<CombatPowerRankingViewGuild> m_GuildRankingData = new List<CombatPowerRankingViewGuild>();
    private List<CombatPowerRankingGuildMember> m_GuildMemberRankingData = new List<CombatPowerRankingGuildMember>();
    private CombatPowerRankingViewGuild m_GuildRankingDataSelf;
    private static CombatPowerRanking s_instance;

    public static CombatPowerRanking Instance => CombatPowerRanking.s_instance;

    private void Awake() => CombatPowerRanking.s_instance = this;

    private void OnDestroy() => CombatPowerRanking.s_instance = (CombatPowerRanking) null;

    private CombatPowerRanking.RankingUIRef ActiveRankingObject
    {
      get
      {
        switch (this.m_ActiveTabType)
        {
          case CombatPowerRanking.GvGRankingTab.AllPort:
            return this.m_AllPortObjects;
          case CombatPowerRanking.GvGRankingTab.PortMembers:
            return this.m_PortMembersObjects;
          default:
            return (CombatPowerRanking.RankingUIRef) null;
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
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_AllPortTab, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.m_AllPortTab.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(\u003CStart\u003Em__0)));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_PortMembersTab, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.m_PortMembersTab.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(\u003CStart\u003Em__1)));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_AllPortSelfItem, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.m_AllPortSelfItem.onClick).AddListener(new UnityAction((object) this, __methodptr(\u003CStart\u003Em__2)));
        ((Component) this.m_AllPortSelfItem).gameObject.SetActive(false);
      }
      this.SelectTab(this.m_ActiveTabType);
    }

    public void Activated(int pinID)
    {
      if (pinID != 10)
      {
        if (pinID != 11)
          return;
        this.Refresh();
      }
      else
      {
        this.Init();
        this.ActivateRequestPin();
      }
    }

    private void Init() => this.RequestRakingData();

    private void Refresh()
    {
      this.CreateRanking();
      if (this.m_ActiveTabType != CombatPowerRanking.GvGRankingTab.AllPort || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_AllPortSelfItem, (UnityEngine.Object) null))
        return;
      if (this.m_GuildRankingDataSelf != null)
      {
        GameUtility.SetGameObjectActive((Component) this.m_AllPortSelfItem, true);
        GameObject gameObject = ((Component) this.m_AllPortSelfItem).gameObject;
        DataSource.Bind<ViewGuildData>(gameObject, (ViewGuildData) this.m_GuildRankingDataSelf);
        DataSource.Bind<CombatPowerRankingViewGuild>(gameObject, this.m_GuildRankingDataSelf);
        GameParameter.UpdateAll(gameObject);
      }
      else
        GameUtility.SetGameObjectActive((Component) this.m_AllPortSelfItem, false);
    }

    private void ActivateRequestPin()
    {
      switch (this.m_ActiveTabType)
      {
        case CombatPowerRanking.GvGRankingTab.AllPort:
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
          break;
        case CombatPowerRanking.GvGRankingTab.PortMembers:
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
          break;
      }
    }

    private void CreateRanking()
    {
      ContentSource source = new ContentSource();
      ContentSource.Param[] values = (ContentSource.Param[]) null;
      switch (this.m_ActiveTabType)
      {
        case CombatPowerRanking.GvGRankingTab.AllPort:
          List<CombatPowerRanking.GuildContentParam> guildContentParamList = new List<CombatPowerRanking.GuildContentParam>();
          for (int index = 0; index < this.m_GuildRankingData.Count; ++index)
          {
            if (this.m_GuildRankingData[index] != null)
            {
              CombatPowerRanking.GuildContentParam guildContentParam = new CombatPowerRanking.GuildContentParam()
              {
                mGuildData = this.m_GuildRankingData[index],
                onListItemClick = new CombatPowerRanking.GuildContentParam.OnListItemClick(this.OnGuildListItemClick)
              };
              guildContentParam.Initialize(source);
              guildContentParamList.Add(guildContentParam);
            }
          }
          values = (ContentSource.Param[]) guildContentParamList.ToArray();
          break;
        case CombatPowerRanking.GvGRankingTab.PortMembers:
          List<CombatPowerRanking.GuildMemberContentParam> memberContentParamList = new List<CombatPowerRanking.GuildMemberContentParam>();
          for (int index = 0; index < this.m_GuildMemberRankingData.Count; ++index)
          {
            if (this.m_GuildMemberRankingData[index] != null)
            {
              CombatPowerRanking.GuildMemberContentParam memberContentParam = new CombatPowerRanking.GuildMemberContentParam()
              {
                mMemberData = this.m_GuildMemberRankingData[index],
                onListItemClick = new CombatPowerRanking.GuildMemberContentParam.OnListItemClick(this.OnGuildMemberListItemClick)
              };
              memberContentParam.Initialize(source);
              memberContentParamList.Add(memberContentParam);
            }
          }
          values = (ContentSource.Param[]) memberContentParamList.ToArray();
          break;
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ActiveContentController, (UnityEngine.Object) null))
        return;
      Vector2 anchorePos = this.ActiveContentController.GetAnchorePos();
      source.SetTable(values);
      this.ActiveContentController.Initialize(source, anchorePos);
      this.ActiveContentController.ForceUpdate();
    }

    public void SetupGuildRankingData(ReqGuildRanking.Response json)
    {
      if (json == null || json.ranking == null || json.self_rank == null || json.self_rank.IsEmptyData)
        return;
      this.m_GuildRankingDataSelf = new CombatPowerRankingViewGuild();
      this.m_GuildRankingDataSelf.Deserialize(json.self_rank);
      this.m_GuildRankingData.Clear();
      for (int index = 0; index < json.ranking.Length; ++index)
      {
        JSON_CombatPowerRankingViewGuild json1 = json.ranking[index];
        if (json1 != null)
        {
          CombatPowerRankingViewGuild rankingViewGuild = new CombatPowerRankingViewGuild();
          rankingViewGuild.Deserialize(json1);
          this.m_GuildRankingData.Add(rankingViewGuild);
        }
      }
    }

    public void SetupGuildMembersRankingData(ReqGuildRankingMembers.Response json)
    {
      if (json == null || json.ranking == null)
        return;
      this.m_GuildMemberRankingData.Clear();
      for (int index = 0; index < json.ranking.Length; ++index)
      {
        JSON_CombatPowerRankingGuildMember json1 = json.ranking[index];
        if (json1 != null)
        {
          CombatPowerRankingGuildMember rankingGuildMember = new CombatPowerRankingGuildMember();
          rankingGuildMember.Deserialize(json1);
          this.m_GuildMemberRankingData.Add(rankingGuildMember);
        }
      }
    }

    private void SelectTab(CombatPowerRanking.GvGRankingTab tab)
    {
      this.m_ActiveTabType = tab;
      GameUtility.SetGameObjectActive(this.m_AllPortObjects.m_RankingRoot, tab == CombatPowerRanking.GvGRankingTab.AllPort);
      GameUtility.SetGameObjectActive(this.m_PortMembersObjects.m_RankingRoot, tab == CombatPowerRanking.GvGRankingTab.PortMembers);
    }

    private void RequestRakingData()
    {
      switch (this.m_ActiveTabType)
      {
        case CombatPowerRanking.GvGRankingTab.AllPort:
          this.m_RequestNodeGuildRanking.SetRequestParam(MonoSingleton<GameManager>.Instance.Player.Guild.UniqueID);
          break;
        case CombatPowerRanking.GvGRankingTab.PortMembers:
          this.m_RequestNodeGuildRankingMembers.SetRequestParam(MonoSingleton<GameManager>.Instance.Player.Guild.UniqueID);
          break;
      }
      this.ActivateRequestPin();
    }

    private void OnGuildListItemClick(ViewGuildData guildData)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_SerializeValue, (UnityEngine.Object) null) || guildData == null)
        return;
      this.m_SerializeValue.list.SetField(GuildSVB_Key.GUILD_ID, guildData.id);
    }

    private void OnGuildMemberListItemClick(CombatPowerRankingGuildMember memberData)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_SerializeValue, (UnityEngine.Object) null) || memberData != null)
        ;
    }

    private void OnSelectTab(CombatPowerRanking.GvGRankingTab tabType)
    {
      if (this.m_ActiveTabType == tabType)
        return;
      this.SelectTab(tabType);
      this.RequestRakingData();
    }

    private enum GvGRankingTab
    {
      AllPort,
      PortMembers,
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

    public class GuildContentParam : ContentSource.Param
    {
      public CombatPowerRankingViewGuild mGuildData;
      public CombatPowerRanking.GuildContentParam.OnListItemClick onListItemClick;

      public override void OnEnable(ContentNode node)
      {
        base.OnEnable(node);
        this.BindData(node);
      }

      public override void OnClick(ContentNode node)
      {
        base.OnClick(node);
        if (this.onListItemClick == null)
          return;
        this.onListItemClick((ViewGuildData) this.mGuildData);
      }

      private void BindData(ContentNode node)
      {
        DataSource.Bind<ViewGuildData>(((Component) node).gameObject, (ViewGuildData) this.mGuildData);
        DataSource.Bind<CombatPowerRankingViewGuild>(((Component) node).gameObject, this.mGuildData);
        GameParameter.UpdateAll(((Component) node).gameObject);
      }

      public delegate void OnListItemClick(ViewGuildData guildData);
    }

    public class GuildMemberContentParam : ContentSource.Param
    {
      public CombatPowerRankingGuildMember mMemberData;
      public CombatPowerRanking.GuildMemberContentParam.OnListItemClick onListItemClick;

      public override void OnEnable(ContentNode node)
      {
        base.OnEnable(node);
        this.BindData(node);
      }

      public override void OnClick(ContentNode node)
      {
        base.OnClick(node);
        if (this.onListItemClick == null)
          return;
        this.onListItemClick(this.mMemberData);
      }

      private void BindData(ContentNode node)
      {
        DataSource.Bind<GuildMemberData>(((Component) node).gameObject, (GuildMemberData) this.mMemberData);
        DataSource.Bind<CombatPowerRankingGuildMember>(((Component) node).gameObject, this.mMemberData);
        DataSource.Bind<UnitData>(((Component) node).gameObject, this.mMemberData.Unit);
        GameParameter.UpdateAll(((Component) node).gameObject);
      }

      public delegate void OnListItemClick(CombatPowerRankingGuildMember memberData);
    }
  }
}
