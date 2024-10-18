// Decompiled with JetBrains decompiler
// Type: SRPG.GuildLobby
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "初期化", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "表示更新", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(3, "ギルド施設強化を開く", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(4, "GVG参加可能か？", FlowNode.PinTypes.Input, 4)]
  [FlowNode.Pin(100, "初期化完了", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(110, "ギルマスです", FlowNode.PinTypes.Output, 110)]
  [FlowNode.Pin(120, "サブマスです", FlowNode.PinTypes.Output, 120)]
  [FlowNode.Pin(130, "メンバーです", FlowNode.PinTypes.Output, 130)]
  [FlowNode.Pin(140, "ギルド施設強化を開く", FlowNode.PinTypes.Output, 140)]
  [FlowNode.Pin(141, "ギルド施設強化の権限がない", FlowNode.PinTypes.Output, 141)]
  [FlowNode.Pin(150, "ギルドに加入していない", FlowNode.PinTypes.Output, 150)]
  [FlowNode.Pin(200, "ギルドレイド開始", FlowNode.PinTypes.Output, 200)]
  [FlowNode.Pin(300, "GVG参加", FlowNode.PinTypes.Output, 300)]
  [FlowNode.Pin(301, "GVG不参加", FlowNode.PinTypes.Output, 301)]
  [FlowNode.Pin(330, "自動更新", FlowNode.PinTypes.Output, 330)]
  public class GuildLobby : MonoBehaviour, IFlowInterface
  {
    private const int PIN_INPUT_INIT = 1;
    private const int PIN_INPUT_REFRESH = 2;
    private const int PIN_INPUT_GUILD_FACILITY_ENHANCE = 3;
    private const int PIN_INPUT_GVG_STATUS = 4;
    private const int PIN_OUTPUT_INIT_FINISHED = 100;
    private const int PIN_OUTPUT_GUILD_MASTER = 110;
    private const int PIN_OUTPUT_SUB_GUILD_MASTER = 120;
    private const int PIN_OUTPUT_GUILD_MEMBER = 130;
    private const int PIN_OUTPUT_GUILD_FACILITY_ENHANCE = 140;
    private const int PIN_OUTPUT_NOT_OPEN_ENHANCE = 141;
    private const int PIN_OUTPUT_GUILD_ERROR_NOT_ENTRY = 150;
    private const int PIN_OUTPUT_GUILDRAID_START = 200;
    private const int PIN_OUTPUT_GVG_JOIN = 300;
    private const int PIN_OUTPUT_GVG_NOT_JOIN = 301;
    private const int PIN_OUTPUT_GUILD_UPDATE = 330;
    private static GuildLobby mInstance;
    [SerializeField]
    private GameObject mWindow;
    [SerializeField]
    private GameObject mGuildInfo;
    [SerializeField]
    private GameObject mGuildInfoBadge;
    [SerializeField]
    private GameObject mGuildRaidOpen;
    [SerializeField]
    private GameObject mGuildRaidClose;
    [SerializeField]
    private GameObject mGvGOpen;
    [SerializeField]
    private GameObject mGvGClose;
    [SerializeField]
    private int UpdateTime = 20;
    private SerializeValueBehaviour mSerializeValueBehaviour;
    private DateTime UpdateDateTime;

    public static GuildLobby Instance => GuildLobby.mInstance;

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          this.Init();
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
          break;
        case 2:
          this.Refresh();
          break;
        case 3:
          this.OpenGuildFacilityEnhanceUI();
          break;
        case 4:
          if (MonoSingleton<GameManager>.Instance.Player.Guild.GvGJoinStatus != 1)
          {
            UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.GUILD_GVG_NONPARTICIPATION"), (UIUtility.DialogResultEvent) (go => FlowNode_GameObject.ActivateOutputLinks((Component) this, 301)), systemModal: true);
            break;
          }
          if (GvGPeriodParam.IsGvGPrepare())
          {
            UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.GUILD_GVG_PREPARE"), (UIUtility.DialogResultEvent) (go => FlowNode_GameObject.ActivateOutputLinks((Component) this, 301)), systemModal: true);
            break;
          }
          if (GuildManager.Instance.EntryStatus == GuildManager.GvGEntryStatus.NOT_MATCHING)
          {
            UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.GUILD_TEXT_ERROR_GVG_NOT_JOIN"), (UIUtility.DialogResultEvent) (go => FlowNode_GameObject.ActivateOutputLinks((Component) this, 301)), systemModal: true);
            break;
          }
          if (GuildManager.Instance.EntryStatus == GuildManager.GvGEntryStatus.MATCHINGPROCESS)
          {
            UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.GUILD_TEXT_ERROR_GVG_NOW_MATCHING"), (UIUtility.DialogResultEvent) (go => FlowNode_GameObject.ActivateOutputLinks((Component) this, 301)), systemModal: true);
            break;
          }
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 300);
          break;
      }
    }

    private void Awake()
    {
      GuildLobby.mInstance = this;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mWindow, (UnityEngine.Object) null))
        this.mWindow.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mGuildInfoBadge, (UnityEngine.Object) null))
        this.mGuildInfoBadge.SetActive(false);
      this.mSerializeValueBehaviour = ((Component) this).GetComponent<SerializeValueBehaviour>();
    }

    private void Init()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mWindow, (UnityEngine.Object) null))
        this.mWindow.SetActive(true);
      this.UpdateDateTime = TimeManager.ServerTime;
      this.UpdateDateTime = this.UpdateDateTime.AddSeconds((double) this.UpdateTime);
      this.Refresh();
    }

    public void Update()
    {
      if (!(this.UpdateDateTime < TimeManager.ServerTime))
        return;
      this.UpdateDateTime = TimeManager.ServerTime;
      this.UpdateDateTime = this.UpdateDateTime.AddSeconds((double) this.UpdateTime);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 330);
    }

    public void Refresh()
    {
      if (MonoSingleton<GameManager>.Instance.Player.Guild == null || MonoSingleton<GameManager>.Instance.Player.PlayerGuild == null || !MonoSingleton<GameManager>.Instance.Player.PlayerGuild.IsJoined)
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 150);
      }
      else
      {
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) GuildManager.Instance, (UnityEngine.Object) null))
          return;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mGuildInfoBadge, (UnityEngine.Object) null))
          this.mGuildInfoBadge.SetActive(GuildManager.Instance.EntryRequests.Length > 0);
        this.mSerializeValueBehaviour.list.SetObject(GuildSVB_Key.GUILD, (object) MonoSingleton<GameManager>.Instance.Player.Guild);
        DataSource.Bind<GuildData>(((Component) this).gameObject, MonoSingleton<GameManager>.Instance.Player.Guild);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mGuildRaidOpen, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mGuildRaidClose, (UnityEngine.Object) null) && GuildManager.Instance.AppearsGuildRaidPeriodId > 0)
        {
          GuildRaidPeriodParam raidRewardPeriod = MonoSingleton<GameManager>.Instance.GetActiveGuildRaidRewardPeriod();
          GameUtility.SetGameObjectActive(this.mGuildRaidOpen, raidRewardPeriod != null);
          GameUtility.SetGameObjectActive(this.mGuildRaidClose, raidRewardPeriod == null);
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mGvGOpen, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mGvGClose, (UnityEngine.Object) null))
        {
          bool active = GvGPeriodParam.GetGvGExitPeriod() != null;
          GameUtility.SetGameObjectActive(this.mGvGOpen, active);
          GameUtility.SetGameObjectActive(this.mGvGClose, !active);
        }
        if (MonoSingleton<GameManager>.Instance.Player.Guild.GuildMaster != null)
        {
          DataSource.Bind<UnitData>(this.mGuildInfo, MonoSingleton<GameManager>.Instance.Player.Guild.GuildMaster.Unit);
          DataSource.Bind<GuildMemberData>(this.mGuildInfo, MonoSingleton<GameManager>.Instance.Player.Guild.GuildMaster);
        }
        GameParameter.UpdateAll(((Component) this).gameObject);
        if (MonoSingleton<GameManager>.Instance.Player.PlayerGuild.IsGuildMaster)
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 110);
        else if (MonoSingleton<GameManager>.Instance.Player.PlayerGuild.IsSubGuildMaster)
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 120);
        else
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 130);
      }
    }

    private void OpenGuildFacilityEnhanceUI()
    {
      if (MonoSingleton<GameManager>.Instance.Player.Guild.CreatedUid != MonoSingleton<GameManager>.Instance.DeviceId)
      {
        long num = MonoSingleton<GameManager>.Instance.Player.PlayerGuild.JoinedAt + (long) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.GuildInvestCoolTime;
        if (Network.GetServerTime() < num)
        {
          UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.GUILD_TEXT_CANT_INVEST_FACILITY"), (UIUtility.DialogResultEvent) null);
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 141);
          return;
        }
      }
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 140);
    }

    public void OnGuildRaid()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      if (player.CheckUnlock(UnlockTargets.GuildRaid))
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 200);
      else
        LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.GuildRaid);
    }
  }
}
