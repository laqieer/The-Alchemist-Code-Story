// Decompiled with JetBrains decompiler
// Type: SRPG.GvGManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "Initialize", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "Refresh", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(10, "Open Chat", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(100, "Initialized", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "Selected Node", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "Restore Battle", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(103, "Start Anime", FlowNode.PinTypes.Output, 103)]
  [FlowNode.Pin(111, "Occupied Node", FlowNode.PinTypes.Output, 111)]
  [FlowNode.Pin(112, "Select Refresh", FlowNode.PinTypes.Output, 112)]
  [FlowNode.Pin(200, "League RankUp", FlowNode.PinTypes.Output, 200)]
  [FlowNode.Pin(201, "Battle CoolTime Start", FlowNode.PinTypes.Output, 201)]
  [FlowNode.Pin(202, "Battle CoolTime End", FlowNode.PinTypes.Output, 202)]
  [FlowNode.Pin(300, "Get ConceptCard", FlowNode.PinTypes.Output, 300)]
  [FlowNode.Pin(901, "Error", FlowNode.PinTypes.Output, 901)]
  public class GvGManager : MonoBehaviour, IFlowInterface, IWebHelp
  {
    public const int PIN_INPUT_INIT = 1;
    public const int PIN_INPUT_REFRESH = 2;
    public const int PIN_INPUT_OPEN_CHAT = 10;
    public const int PIN_OUTPUT_INITALIZED = 100;
    public const int PIN_OUTPUT_SELECTED_NODE = 101;
    public const int PIN_OUTPUT_RESTORE_BATTLE = 102;
    public const int PIN_OUTPUT_STARTANIME = 103;
    public const int PIN_OUTPUT_OCCUPIED_NODE = 111;
    public const int PIN_OUTPUT_SELAUTOREFRESH = 112;
    public const int PIN_OUTPUT_LEAGUE_RANKUP = 200;
    public const int PIN_OUTPUT_BATTLE_COOLTIME_START = 201;
    public const int PIN_OUTPUT_BATTLE_COOLTIME_END = 202;
    public const int PIN_OUTPUT_GETCONCEPTCARD = 300;
    public const int PIN_OUTPUT_ERROR = 901;
    [SerializeField]
    private GvGMapAssets GvGMapAssetsData;
    [SerializeField]
    private Transform BgParent;
    [SerializeField]
    private List<int> PartyCountImageBorder = new List<int>();
    [SerializeField]
    private bool IsHideChat;
    [SerializeField]
    private int OnePageDefensePartyCountMax = 5;
    [SerializeField]
    private List<Color> GuildColorList = new List<Color>();
    [SerializeField]
    private int SelfColor;
    [SerializeField]
    private int NPCColor = 1;
    [SerializeField]
    private List<int> mStaminaIndexList = new List<int>();
    public const int SELF_DEFAULT_COLOR = 0;
    public const int NPC_DEFAULT_COLOR = 1;
    public const int PARTY_UNIT_NUM_MAX = 3;
    private static GvGManager mInstance;
    private DateTime mCanRefreshTime;
    private DateTime mCanAutoRefreshTime;
    private DateTime mCanDeclareCoolTime;
    private GvGManager.GvGAutoRefreshState AutoRefreshStatusOld;
    private GvGMap mGvGMap;
    private LoadRequest mResourceRequest;
    public static int GvGGroupId;
    public bool IsGvGBattleCoolTimeFirst;
    private GvGManager.GvGStatus OldGvGStatusPhase = GvGManager.GvGStatus.Finished;
    private GvGUsedData mGvGUsedList = new GvGUsedData();
    public string mUsedMessage;
    private GvGRuleParam CacheCurrentRule;
    private int CacheRuleIndex = -1;
    private int CacheRulePeriodId = -1;
    public bool IsExecAutoRefresh = true;

    public int ONE_PAGE_DEFENSE_PARTY_COUNT_MAX => this.OnePageDefensePartyCountMax;

    public int GvGSelfColor => this.SelfColor;

    public int GvGNPCColor => this.NPCColor;

    public static GvGManager Instance => GvGManager.mInstance;

    public GvGPeriodParam GvGPeriod { get; private set; }

    public GvGManager.GvGAutoRefreshState AutoRefreshStatus { get; private set; }

    public RewardData ResultReward { get; private set; }

    public GvGLeagueParam GvGLeagueInfo { get; private set; }

    public bool IsPrivilege { get; private set; }

    public int SelectNodeId { get; set; }

    public bool CanRefresh => TimeManager.ServerTime.Ticks <= this.mCanRefreshTime.Ticks;

    public bool CanAutoRefresh => TimeManager.ServerTime.Ticks >= this.mCanAutoRefreshTime.Ticks;

    public bool CanDeclareCoolTime
    {
      get => TimeManager.ServerTime.Ticks <= this.mCanDeclareCoolTime.Ticks;
    }

    public TimeSpan DeclareCoolTime => this.mCanDeclareCoolTime - TimeManager.ServerTime;

    public List<GvGRankingGuildData> GvGRankingViewList { get; set; }

    public GvGUsedData GvGUsedList => this.mGvGUsedList;

    public GvGManager.GvGStatus GvGStatusPhase()
    {
      GvGManager.GvGStatus gvGstatus = this.SetGvGStatusPhase();
      if (this.OldGvGStatusPhase != gvGstatus)
      {
        if (this.OldGvGStatusPhase == GvGManager.GvGStatus.OffenseFirstHalf && gvGstatus == GvGManager.GvGStatus.OffenseCoolTime && !this.IsGvGBattleCoolTimeFirst)
        {
          this.IsExecAutoRefresh = true;
          this.OldGvGStatusPhase = gvGstatus;
          this.IsGvGBattleCoolTimeFirst = true;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 201);
        }
        else if (gvGstatus != GvGManager.GvGStatus.OffenseCoolTime)
        {
          this.IsGvGBattleCoolTimeFirst = false;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 202);
        }
        this.OldGvGStatusPhase = gvGstatus;
      }
      return gvGstatus;
    }

    public GvGManager.GvGStatus SetGvGStatusPhase()
    {
      if (this.GvGPeriod == null)
        return GvGManager.GvGStatus.Finished;
      DateTime serverTime = TimeManager.ServerTime;
      if (serverTime < this.GvGPeriod.BeginAt || this.GvGPeriod.EndAt <= serverTime)
        return GvGManager.GvGStatus.Finished;
      DateTime result1;
      DateTime.TryParse(serverTime.ToString("yyyy/MM/dd ") + this.GvGPeriod.DeclarationStartTime, out result1);
      DateTime result2;
      DateTime.TryParse(serverTime.ToString("yyyy/MM/dd ") + this.GvGPeriod.DeclarationEndTime, out result2);
      if (result1 <= serverTime && serverTime < result2 || result2 < result1 && (result2 > serverTime || serverTime >= result1))
        return GvGManager.GvGStatus.Declaration;
      DateTime result3;
      DateTime.TryParse(serverTime.ToString("yyyy/MM/dd ") + this.GvGPeriod.BattleStartTime, out result3);
      DateTime result4;
      DateTime.TryParse(serverTime.ToString("yyyy/MM/dd ") + this.GvGPeriod.BattleCoolStartTime, out result4);
      if (result3 <= serverTime && serverTime < result4 || result4 < result3 && (result4 > serverTime || serverTime >= result3))
        return GvGManager.GvGStatus.OffenseFirstHalf;
      DateTime result5;
      DateTime.TryParse(serverTime.ToString("yyyy/MM/dd ") + this.GvGPeriod.BattleCoolStartTime, out result5);
      DateTime result6;
      DateTime.TryParse(serverTime.ToString("yyyy/MM/dd ") + this.GvGPeriod.BattleCoolEndTime, out result6);
      if (result5 <= serverTime && serverTime < result6 || result6 < result5 && (result6 > serverTime || serverTime >= result5))
        return GvGManager.GvGStatus.OffenseCoolTime;
      DateTime result7;
      DateTime.TryParse(serverTime.ToString("yyyy/MM/dd ") + this.GvGPeriod.BattleCoolEndTime, out result7);
      DateTime result8;
      DateTime.TryParse(serverTime.ToString("yyyy/MM/dd ") + this.GvGPeriod.BattleEndTime, out result8);
      if (result7 <= serverTime && serverTime < result8 || result8 < result7 && (result8 > serverTime || serverTime >= result7))
        return GvGManager.GvGStatus.OffenseLatterHalf;
      DateTime result9;
      DateTime.TryParse(serverTime.ToString("yyyy/MM/dd ") + this.GvGPeriod.DeclarationEndTime, out result9);
      DateTime result10;
      DateTime.TryParse(serverTime.ToString("yyyy/MM/dd ") + this.GvGPeriod.BattleStartTime, out result10);
      return result9 <= serverTime && serverTime < result10 || result10 < result9 && (result10 > serverTime || serverTime >= result9) ? GvGManager.GvGStatus.DeclarationCoolTime : GvGManager.GvGStatus.AggregationTime;
    }

    public bool IsGvGStatusOffencePhase
    {
      get
      {
        GvGManager.GvGStatus gvGstatus = this.GvGStatusPhase();
        return gvGstatus == GvGManager.GvGStatus.OffenseFirstHalf || gvGstatus == GvGManager.GvGStatus.OffenseLatterHalf;
      }
    }

    public bool IsGvGOffenceBetween
    {
      get
      {
        GvGManager.GvGStatus gvGstatus = this.GvGStatusPhase();
        switch (gvGstatus)
        {
          case GvGManager.GvGStatus.OffenseFirstHalf:
          case GvGManager.GvGStatus.OffenseLatterHalf:
            return true;
          default:
            return gvGstatus == GvGManager.GvGStatus.OffenseCoolTime;
        }
      }
    }

    public GvGManager.GvGStatus GvGStatusPhaseSetPeriod()
    {
      if (this.GvGPeriod != null)
        return this.GvGStatusPhase();
      this.GvGPeriod = GvGPeriodParam.GetGvGPeriod();
      GvGManager.GvGStatus gvGstatus = this.GvGStatusPhase();
      this.GvGPeriod = (GvGPeriodParam) null;
      return gvGstatus;
    }

    public GvGRuleParam CurrentRule
    {
      get
      {
        if (this.GvGPeriod == null || this.GvGPeriod.RuleCycle == null || this.GvGPeriod.RuleCycle.Count <= 0 || MonoSingleton<GameManager>.Instance.mGvGRuleParam == null)
          return (GvGRuleParam) null;
        int index = 0;
        DateTime date1 = TimeManager.ServerTime.Date;
        DateTime date2 = this.GvGPeriod.BeginAt.Date;
        if (date1 >= date2)
          index = (date1 - date2).Days % this.GvGPeriod.RuleCycle.Count;
        if (this.GvGPeriod.RuleCycle.Count <= index)
          return (GvGRuleParam) null;
        if (this.CacheRuleIndex == index && this.CacheRulePeriodId == this.GvGPeriod.Id && this.CacheCurrentRule != null)
          return this.CacheCurrentRule;
        string rule_iname = this.GvGPeriod.RuleCycle[index];
        this.CacheRuleIndex = index;
        this.CacheRulePeriodId = this.GvGPeriod.Id;
        this.CacheCurrentRule = MonoSingleton<GameManager>.Instance.mGvGRuleParam.Find((Predicate<GvGRuleParam>) (rule => rule.Iname == rule_iname));
        return this.CacheCurrentRule;
      }
    }

    public int CurrentRuleUnitCount
    {
      get
      {
        GvGRuleParam currentRule = this.CurrentRule;
        if (currentRule == null)
          return 0;
        return this.IsGvGStatusOffencePhase || this.GvGStatusPhase() == GvGManager.GvGStatus.OffenseCoolTime ? currentRule.UnitCount : currentRule.DefUnitCount;
      }
    }

    public int RemainDeclareCount { get; set; }

    public List<GvGNodeData> NodeDataList { get; private set; }

    public List<int> MatchingOrder { get; private set; }

    public List<long> UsedUnitList { get; private set; }

    public List<GvGUsedUnitData> UsedUnitData { get; private set; }

    public int UsedUnitTodayCount
    {
      get
      {
        if (this.UsedUnitData == null || this.CacheCurrentRule == null)
          return 0;
        List<GvGUsedUnitData> all = this.UsedUnitData.FindAll((Predicate<GvGUsedUnitData>) (p => p.usedDay == TimeManager.ServerTime.Date));
        return all != null ? all.Count : 0;
      }
    }

    public List<GvGLeagueViewGuild> OtherGuildList { get; private set; }

    public GvGLeagueViewGuild MyGuild { get; private set; }

    public GvGResultData ResultDaily { get; private set; }

    public GvGResultData ResultSeason { get; private set; }

    public GvGLeagueResult ResultLeague { get; private set; }

    public List<GvGHalfTime> HalfTimeList { get; private set; }

    public bool IsPeriod() => GvGPeriodParam.GetGvGPeriod() != null;

    public ViewGuildData FindViewGuild(int guildId)
    {
      return this.MyGuild == null || GvGManager.Instance.MyGuild.id != guildId ? (ViewGuildData) this.OtherGuildList.Find((Predicate<GvGLeagueViewGuild>) (g => g.id == guildId)) : (ViewGuildData) this.MyGuild;
    }

    private void Awake()
    {
      GvGManager.mInstance = this;
      this.IsExecAutoRefresh = true;
    }

    private void OnDestroy() => GvGManager.mInstance = (GvGManager) null;

    private void Update()
    {
      if (!this.CanAutoRefresh || this.AutoRefreshStatus != GvGManager.GvGAutoRefreshState.Top || this.IsExecAutoRefresh || Network.IsConnecting)
        return;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 112);
      this.IsExecAutoRefresh = true;
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          this.Initialize();
          this.Refresh();
          if (MonoSingleton<GameManager>.Instance.Player.ConceptCards == null || MonoSingleton<GameManager>.Instance.Player.ConceptCards.Count == 0 || MonoSingleton<GameManager>.Instance.Player.Runes == null || MonoSingleton<GameManager>.Instance.Player.Runes.Count == 0)
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 300);
          else
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
          this.IsExecAutoRefresh = false;
          break;
        case 2:
          this.Refresh(this.IsExecAutoRefresh);
          this.IsExecAutoRefresh = false;
          break;
        case 10:
          this.OpenChat();
          break;
      }
    }

    private void Initialize()
    {
      if (this.IsHideChat)
        this.HideChatButton();
      this.AutoRefreshStatus = GvGManager.GvGAutoRefreshState.Top;
      if (HomeWindow.GetRestorePoint() != RestorePoints.GVG)
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 103);
      this.GvGLeagueInfo = GvGLeagueParam.GetGvGLeagueParam(MonoSingleton<GameManager>.Instance.Player.GuildLeagueRate);
    }

    private void Refresh(bool isAuto = false)
    {
      this.GvGPeriod = GvGPeriodParam.GetGvGPeriod();
      this.IsExecAutoRefresh = false;
      if (this.GvGPeriod == null)
      {
        this.GvGPeriod = GvGPeriodParam.GetGvGNearPeriod();
        if (this.GvGPeriod == null)
        {
          Debug.LogError((object) "GvgPeriod is nothing.");
          return;
        }
      }
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (instance.Player.PlayerGuild != null && (instance.Player.PlayerGuild.IsGuildMaster || instance.Player.PlayerGuild.IsSubGuildMaster))
        this.IsPrivilege = true;
      if (!isAuto)
        this.SelectNodeId = 0;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.BgParent, (UnityEngine.Object) null))
        Debug.LogError((object) "MapParent is nothing.");
      else if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.GvGMapAssetsData, (UnityEngine.Object) null) || this.GvGMapAssetsData.GvGMaps == null || this.GvGMapAssetsData.GvGMaps.Length < this.GvGPeriod.MapIndex + 1)
      {
        Debug.LogError((object) "GvGMapAssetsData is nothing.");
      }
      else
      {
        this.StartCoroutine(this.DownloadMap());
        GvGInfo component = ((Component) this).gameObject.GetComponent<GvGInfo>();
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          return;
        component.Refresh();
      }
    }

    public void OpenNodeInfo(int nodeId)
    {
      this.SelectNodeId = nodeId;
      GvGMap.Instance.NodeAutoForcus(nodeId);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
    }

    public bool CanDeclareNode(GvGNodeData node)
    {
      return this.IsPrivilege && node.CanDeclare && this.GvGStatusPhase() == GvGManager.GvGStatus.Declaration && this.RemainDeclareCount > 0 && (node.NodeParam.AdjacentNode.FindIndex((Predicate<int>) (n => this.NodeDataList.Find((Predicate<GvGNodeData>) (nd => nd.NodeId == n && nd.GuildId == MonoSingleton<GameManager>.Instance.Player.PlayerGuild.Gid)) != null)) != -1 || node.NodeParam.Rank <= 1 && this.NodeDataList.Find((Predicate<GvGNodeData>) (nd => nd.GuildId == MonoSingleton<GameManager>.Instance.Player.PlayerGuild.Gid)) == null);
    }

    public bool CanAttackNode(GvGNodeData node)
    {
      return node.State == GvGNodeState.DeclareSelf && !node.IsAttackWait && this.IsGvGStatusOffencePhase;
    }

    public GvGManager.WindowBGImageIndex GetNodeImageIndex(GvGNodeData node)
    {
      if (node == null)
        return GvGManager.WindowBGImageIndex.Purple;
      switch (node.State)
      {
        case GvGNodeState.OccupySelf:
        case GvGNodeState.DeclaredEnemy:
          return GvGManager.WindowBGImageIndex.Blue;
        case GvGNodeState.DeclareSelf:
          return GvGManager.WindowBGImageIndex.Red;
        default:
          return GvGManager.WindowBGImageIndex.Purple;
      }
    }

    public GvGManager.WindowBGImageIndex GetNodeIconColor(GvGNodeData node)
    {
      if (node == null)
        return GvGManager.WindowBGImageIndex.Purple;
      switch (node.State)
      {
        case GvGNodeState.DeclareSelf:
          return GvGManager.WindowBGImageIndex.Blue;
        case GvGNodeState.DeclaredEnemy:
          return GvGManager.WindowBGImageIndex.Red;
        default:
          return GvGManager.WindowBGImageIndex.Purple;
      }
    }

    public GvGManager.WindowBGImageIndex GetNodeBattleImageIndex(GvGNodeData node, bool isAttack)
    {
      if (node == null)
        return GvGManager.WindowBGImageIndex.Purple;
      ViewGuildData viewGuild1 = this.FindViewGuild(node.DeclaredGuildId);
      ViewGuildData viewGuild2 = this.FindViewGuild(node.GuildId);
      if (viewGuild1 != null && viewGuild1.id == MonoSingleton<GameManager>.Instance.Player.PlayerGuild.Gid)
        return isAttack ? GvGManager.WindowBGImageIndex.Blue : GvGManager.WindowBGImageIndex.Red;
      if (viewGuild2 == null || viewGuild2.id != MonoSingleton<GameManager>.Instance.Player.PlayerGuild.Gid)
        return GvGManager.WindowBGImageIndex.Purple;
      return isAttack ? GvGManager.WindowBGImageIndex.Red : GvGManager.WindowBGImageIndex.Blue;
    }

    public int GetNodeBattleGuildImageIndex(GvGNodeData node, bool isAttack)
    {
      if (node == null)
      {
        Debug.LogError((object) "GetNodeBattleGuildImageIndex:Node Not Found:");
        return 0;
      }
      ViewGuildData viewGuild1 = this.FindViewGuild(node.DeclaredGuildId);
      ViewGuildData viewGuild2 = this.FindViewGuild(node.GuildId);
      if (viewGuild1 != null && viewGuild1.id == MonoSingleton<GameManager>.Instance.Player.PlayerGuild.Gid && isAttack || viewGuild2 != null && viewGuild2.id == MonoSingleton<GameManager>.Instance.Player.PlayerGuild.Gid && !isAttack)
        return 0;
      int gid = 0;
      if (viewGuild2 != null)
        gid = viewGuild2.id;
      if (isAttack && viewGuild1 != null)
        gid = viewGuild1.id;
      return this.GetMatchingOrderIndex(gid);
    }

    public int GetDefensePartyIconIndex(GvGNodeData node)
    {
      if (this.PartyCountImageBorder.Count == 0)
        return 0;
      for (int index = 0; index < this.PartyCountImageBorder.Count; ++index)
      {
        if (this.PartyCountImageBorder[index] > node.DefensePartyNum)
          return index;
      }
      return this.PartyCountImageBorder.Count - 1;
    }

    public void SetNodeColor(GvGNodeData node, ChangeMaterialList cc)
    {
      if (node == null || UnityEngine.Object.op_Equality((UnityEngine.Object) cc, (UnityEngine.Object) null))
        return;
      int index = this.GetMatchingOrderIndex(node.GuildId);
      if (node.GuildId == 0)
        index = this.GvGNPCColor;
      if (index >= this.GuildColorList.Count)
        return;
      cc.SetColor(this.GuildColorList[index]);
    }

    public void SetNodeColorIndex(int index, ChangeMaterialList cc)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) cc, (UnityEngine.Object) null) || index >= this.GuildColorList.Count)
        return;
      cc.SetColor(this.GuildColorList[index]);
    }

    public void SetNodeColorIndex(int index, Image image)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) image, (UnityEngine.Object) null) || index >= this.GuildColorList.Count)
        return;
      ((Graphic) image).color = this.GuildColorList[index];
    }

    private void OpenChat()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) MonoSingleton<ChatWindow>.Instance, (UnityEngine.Object) null))
        return;
      MonoSingleton<ChatWindow>.Instance.ChangeChatTypeTab(ChatWindow.eChatType.Guild);
      MonoSingleton<ChatWindow>.Instance.Open();
    }

    public void HideChatButton(bool flag = false)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) MonoSingleton<ChatWindow>.Instance, (UnityEngine.Object) null))
        return;
      MonoSingleton<ChatWindow>.Instance.SetActiveOpenCloseButton(flag);
    }

    public void GetStaminaImageIndex(
      GvGNodeData node,
      Slider slider,
      ImageArray image,
      int winNum)
    {
      if (node == null)
        return;
      float num = (float) ((double) (node.NodeParam.ConsecutiveDebuffMax - winNum) / (double) node.NodeParam.ConsecutiveDebuffMax * 100.0);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) slider, (UnityEngine.Object) null))
        slider.value = num;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) image, (UnityEngine.Object) null))
        return;
      for (int index = 0; index < this.mStaminaIndexList.Count; ++index)
      {
        if ((double) num >= (double) this.mStaminaIndexList[index])
        {
          if (image.Images.Length <= index)
          {
            image.ImageIndex = image.Images.Length - 1;
            return;
          }
          image.ImageIndex = index;
          return;
        }
      }
      image.ImageIndex = image.Images.Length - 1;
    }

    [DebuggerHidden]
    private IEnumerator DownloadMap()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GvGManager.\u003CDownloadMap\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    public bool GetHelpURL(out string url, out string title)
    {
      url = string.Empty;
      title = string.Empty;
      GvGPeriodParam gvGperiod = GvGPeriodParam.GetGvGPeriod();
      if (gvGperiod == null)
        return false;
      url = gvGperiod.URL;
      title = gvGperiod.URL_TITLE;
      return true;
    }

    public int GetMatchingOrderIndex(int gid)
    {
      if (this.MatchingOrder == null || this.MatchingOrder.Count == 0)
        return 0;
      if (gid == 0)
        return this.NPCColor;
      if (gid == this.MyGuild.id)
        return this.SelfColor;
      int matchingOrderIndex = 0;
      for (int index = 0; index < this.MatchingOrder.Count; ++index)
      {
        if (matchingOrderIndex == this.NPCColor || matchingOrderIndex == this.SelfColor)
        {
          ++matchingOrderIndex;
          --index;
        }
        else
        {
          if (this.MatchingOrder[index] == gid)
            return matchingOrderIndex;
          ++matchingOrderIndex;
        }
      }
      Debug.LogError((object) ("Matching Not Find:" + gid.ToString()));
      return this.MatchingOrder.Count - 1;
    }

    public void SetAutoRefreshStatus(GvGManager.GvGAutoRefreshState state)
    {
      this.AutoRefreshStatusOld = this.AutoRefreshStatus;
      this.AutoRefreshStatus = state;
    }

    public void RevertAutoRefreshStatus() => this.AutoRefreshStatus = this.AutoRefreshStatusOld;

    public void SetupRefreshWait(int sec)
    {
      this.mCanRefreshTime = TimeManager.ServerTime.AddSeconds((double) sec);
    }

    public void SetupAutoRefreshWait(int sec)
    {
      this.mCanAutoRefreshTime = TimeManager.ServerTime.AddSeconds((double) sec);
    }

    public void SetupDeclareCoolTime(int sec)
    {
      this.mCanDeclareCoolTime = TimeManager.FromUnixTime((long) sec);
    }

    public bool SetupNodeDataList(JSON_GvGNodeData[] json)
    {
      if (json == null || json.Length <= 0)
        return false;
      if (this.NodeDataList == null)
        this.NodeDataList = new List<GvGNodeData>();
      for (int i = 0; i < json.Length; ++i)
      {
        GvGNodeData gvGnodeData = this.NodeDataList.Find((Predicate<GvGNodeData>) (n => n.NodeId == json[i].id)) ?? new GvGNodeData();
        if (!gvGnodeData.Deserialize(json[i]))
          return false;
        if (!this.NodeDataList.Contains(gvGnodeData))
          this.NodeDataList.Add(gvGnodeData);
      }
      return true;
    }

    public bool SetupMatchingOrder(int[] json)
    {
      if (json == null || json.Length <= 0)
        return false;
      if (this.MatchingOrder == null)
        this.MatchingOrder = new List<int>();
      this.MatchingOrder.Clear();
      for (int index = 0; index < json.Length; ++index)
        this.MatchingOrder.Add(json[index]);
      return true;
    }

    public void SetupUsedUnitList(JSON_GvGUsedUnitData[] units)
    {
      if (units == null)
        return;
      this.UsedUnitData = new List<GvGUsedUnitData>();
      this.UsedUnitList = new List<long>();
      for (int index = 0; index < units.Length; ++index)
      {
        GvGUsedUnitData gvGusedUnitData = new GvGUsedUnitData();
        gvGusedUnitData.Deserialize(units[index]);
        this.UsedUnitData.Add(gvGusedUnitData);
        this.UsedUnitList.Add((long) units[index].uiid);
      }
      ((Component) this).gameObject.GetComponent<GvGInfo>().UpdateGameParameter();
    }

    public bool SetupOtherGuildList(JSON_GvGLeagueViewGuild[] json)
    {
      if (json == null)
        return false;
      if (this.OtherGuildList == null)
        this.OtherGuildList = new List<GvGLeagueViewGuild>();
      this.OtherGuildList.Clear();
      for (int index = 0; index < json.Length; ++index)
      {
        GvGLeagueViewGuild gleagueViewGuild = new GvGLeagueViewGuild();
        gleagueViewGuild.Deserialize(json[index]);
        this.OtherGuildList.Add(gleagueViewGuild);
      }
      return true;
    }

    public bool SetupMyGuild(JSON_GvGLeagueViewGuild json)
    {
      if (json == null)
        return false;
      this.MyGuild = new GvGLeagueViewGuild();
      this.MyGuild.Deserialize(json);
      if (json.league != null)
        MonoSingleton<GameManager>.Instance.Player.GuildLeagueRate = json.league.rate;
      return true;
    }

    public bool SetupResultDaily(JSON_GvGResult json)
    {
      this.ResultDaily = (GvGResultData) null;
      if (json == null)
        return false;
      this.ResultDaily = new GvGResultData();
      return this.ResultDaily.Deserialize(json);
    }

    public bool SetupResultSeason(JSON_GvGResult json)
    {
      this.ResultSeason = (GvGResultData) null;
      if (json == null)
        return false;
      this.ResultSeason = new GvGResultData();
      return this.ResultSeason.Deserialize(json);
    }

    public bool SetupLeagueResult(JSON_GvGLeagueResult json)
    {
      this.ResultLeague = (GvGLeagueResult) null;
      if (json == null)
        return false;
      this.ResultLeague = new GvGLeagueResult();
      return this.ResultLeague.Deserialize(json);
    }

    public bool SetupHalfTime(JSON_GvGHalfTime[] json)
    {
      if (json == null || json.Length <= 0)
        return false;
      if (this.HalfTimeList == null)
        this.HalfTimeList = new List<GvGHalfTime>();
      this.HalfTimeList.Clear();
      for (int index = 0; index < json.Length; ++index)
      {
        GvGHalfTime gvGhalfTime = new GvGHalfTime();
        if (!gvGhalfTime.Deserialize(json[index]))
          return false;
        this.HalfTimeList.Add(gvGhalfTime);
      }
      return true;
    }

    public void SetRankingData(JSON_GvGRankingData[] guilds)
    {
      GvGRankingData gvGrankingData = new GvGRankingData();
      gvGrankingData.Deserialize(guilds);
      this.GvGRankingViewList = gvGrankingData.Guilds;
    }

    public void SetupUsedItems(int[] artis, int[] cards, int[] runes)
    {
      this.mGvGUsedList.Deserialize(artis, cards, runes);
    }

    public void SetupUsedMessage(JSON_GvGUsedItems[] items)
    {
      this.mUsedMessage = GvGUsedData.CreateUsedItemsStr(items);
    }

    public bool SetupResultReward(string[] json)
    {
      this.ResultReward = (RewardData) null;
      if (json == null || json.Length < 0)
        return false;
      RewardData rewardData = new RewardData();
      GameManager gm = MonoSingleton<GameManager>.Instance;
      for (int index = 0; index < json.Length; ++index)
        GvGRewardParam.GetReward(json[index])?.Rewards.ForEach((Action<GvGRewardDetailParam>) (reward =>
        {
          switch (reward.Type)
          {
            case RaidRewardType.Item:
              ItemParam ip = gm.GetItemParam(reward.IName);
              if (ip == null)
                break;
              ItemData itemData1 = rewardData.Items.Find((Predicate<ItemData>) (ri => ri.Param.iname == ip.iname));
              if (itemData1 != null)
              {
                itemData1.Gain(reward.Num);
                break;
              }
              ItemData itemData2 = new ItemData();
              if (!itemData2.Setup(0L, ip.iname, reward.Num))
                break;
              rewardData.Items.Add(itemData2);
              break;
            case RaidRewardType.Gold:
              rewardData.Gold += reward.Num;
              break;
            case RaidRewardType.Coin:
              rewardData.Coin += reward.Num;
              break;
            case RaidRewardType.Award:
              if (gm.MasterParam.GetAwardParam(reward.IName) == null)
                break;
              if (!rewardData.GiftRecieveItemDataDic.ContainsKey(reward.IName))
              {
                rewardData.GiftRecieveItemDataDic.Add(reward.IName, new GiftRecieveItemData()
                {
                  iname = reward.IName,
                  num = 1,
                  type = GiftTypes.Award
                });
                break;
              }
              ++rewardData.GiftRecieveItemDataDic[reward.IName].num;
              break;
            case RaidRewardType.Unit:
              if (gm.GetUnitParam(reward.IName) == null)
                break;
              if (!rewardData.GiftRecieveItemDataDic.ContainsKey(reward.IName))
              {
                rewardData.GiftRecieveItemDataDic.Add(reward.IName, new GiftRecieveItemData()
                {
                  iname = reward.IName,
                  num = 1,
                  type = GiftTypes.Unit
                });
                break;
              }
              ++rewardData.GiftRecieveItemDataDic[reward.IName].num;
              break;
            case RaidRewardType.ConceptCard:
              if (ConceptCardData.CreateConceptCardDataForDisplay(reward.IName) == null)
                break;
              if (!rewardData.GiftRecieveItemDataDic.ContainsKey(reward.IName))
              {
                rewardData.GiftRecieveItemDataDic.Add(reward.IName, new GiftRecieveItemData()
                {
                  iname = reward.IName,
                  num = reward.Num,
                  type = GiftTypes.ConceptCard
                });
                break;
              }
              rewardData.GiftRecieveItemDataDic[reward.IName].num += reward.Num;
              break;
            case RaidRewardType.Artifact:
              if (gm.MasterParam.GetArtifactParam(reward.IName) == null)
                break;
              if (!rewardData.GiftRecieveItemDataDic.ContainsKey(reward.IName))
              {
                rewardData.GiftRecieveItemDataDic.Add(reward.IName, new GiftRecieveItemData()
                {
                  iname = reward.IName,
                  num = reward.Num,
                  type = GiftTypes.Artifact
                });
                break;
              }
              rewardData.GiftRecieveItemDataDic[reward.IName].num += reward.Num;
              break;
          }
        }));
      GlobalVars.LastReward.Set(rewardData);
      this.ResultReward = rewardData;
      return true;
    }

    public enum GvGStatus
    {
      Declaration,
      OffenseFirstHalf,
      OffenseCoolTime,
      OffenseLatterHalf,
      DeclarationCoolTime,
      AggregationTime,
      Finished,
    }

    public enum WindowBGImageIndex
    {
      Blue,
      Purple,
      Red,
      Max,
    }

    public enum GvGAutoRefreshState
    {
      Top = 0,
      Battle = 10, // 0x0000000A
      Stop = 20, // 0x00000014
    }
  }
}
