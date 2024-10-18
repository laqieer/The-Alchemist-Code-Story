// Decompiled with JetBrains decompiler
// Type: SRPG.GuildManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(10, "初期化", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(20, "チャットボタン非表示", FlowNode.PinTypes.Input, 20)]
  [FlowNode.Pin(1010, "ロビー生成", FlowNode.PinTypes.Output, 1010)]
  [FlowNode.Pin(1020, "設立or加入", FlowNode.PinTypes.Output, 1020)]
  public class GuildManager : MonoBehaviour, IFlowInterface
  {
    private const long INTERVAL_CHECK_GUILD_INVITE = 1800;
    private static long LastCheckGuildInviteTime = -1;
    public static int NotifyEntryRequestCount;
    public const int PIN_INPUT_INIT = 10;
    public const int PIN_INPUT_HIDE_CHAT_BUTTON = 20;
    public const int PIN_OUTPUT_CREATE_LOBBY = 1010;
    public const int PIN_OUTPUT_CREATE_COMMAND = 1020;
    private static GuildManager mInstance;
    private GuildMemberData[] mEntryRequests;
    public GvGLeagueParam SelectLeagueParam;

    public static bool IsNeedGuildInviteRequest(long now)
    {
      if (MonoSingleton<GameManager>.Instance.Player.PlayerGuild == null || !MonoSingleton<GameManager>.Instance.Player.PlayerGuild.IsJoined || !MonoSingleton<GameManager>.Instance.Player.PlayerGuild.IsGuildMaster && !MonoSingleton<GameManager>.Instance.Player.PlayerGuild.IsSubGuildMaster)
        return false;
      if (GuildManager.LastCheckGuildInviteTime < 0L)
      {
        GuildManager.LastCheckGuildInviteTime = now;
        return true;
      }
      if (GuildManager.LastCheckGuildInviteTime + 1800L > now)
        return false;
      GuildManager.LastCheckGuildInviteTime = now;
      return true;
    }

    public static GuildManager Instance => GuildManager.mInstance;

    public GuildMemberData[] EntryRequests => this.mEntryRequests;

    public int AppearsGuildRaidPeriodId { get; private set; }

    public GuildManager.GvGEntryStatus EntryStatus
    {
      get
      {
        return GvGManager.GvGGroupId == -1 || GvGManager.GvGGroupId == 0 ? (GuildManager.GvGEntryStatus) GvGManager.GvGGroupId : GuildManager.GvGEntryStatus.MATCHING_OK;
      }
    }

    public GuildManager.GuildAttendStatus AttendStatus { get; private set; }

    private void Awake() => GuildManager.mInstance = this;

    public void Activated(int pinID)
    {
      if (pinID != 10)
      {
        if (pinID != 20)
          return;
        MonoSingleton<ChatWindow>.Instance.SetActiveOpenCloseButton(false);
      }
      else
        this.Init();
    }

    private void Init()
    {
      if (MonoSingleton<GameManager>.Instance.Player.PlayerGuild != null && MonoSingleton<GameManager>.Instance.Player.PlayerGuild.IsJoined)
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 1010);
      else
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 1020);
    }

    public void SetEntryRequest(JSON_GuildMember[] json)
    {
      if (json == null)
      {
        this.mEntryRequests = new GuildMemberData[0];
        GuildManager.NotifyEntryRequestCount = 0;
      }
      else
      {
        this.mEntryRequests = new GuildMemberData[json.Length];
        for (int index = 0; index < json.Length; ++index)
        {
          this.mEntryRequests[index] = new GuildMemberData();
          this.mEntryRequests[index].Deserialize(json[index]);
        }
        GuildManager.NotifyEntryRequestCount = this.mEntryRequests.Length;
      }
    }

    public void SetGuildRaidPeriod(int period) => this.AppearsGuildRaidPeriodId = period;

    public void SetGuildAttendStatus(int attend_status)
    {
      this.AttendStatus = (GuildManager.GuildAttendStatus) attend_status;
    }

    public void SetGuildAttendStatus(GuildManager.GuildAttendStatus attend_status)
    {
      this.AttendStatus = attend_status;
    }

    public static void GetEmblem(GameObject obj, string name, GameObject root)
    {
      if (Object.op_Equality((Object) obj, (Object) null) || Object.op_Equality((Object) root, (Object) null))
        return;
      Image component = obj.GetComponent<Image>();
      if (Object.op_Equality((Object) component, (Object) null))
        return;
      string name1 = name;
      ViewGuildData dataOfClass = DataSource.FindDataOfClass<ViewGuildData>(root, (ViewGuildData) null);
      if (dataOfClass != null)
        name1 = dataOfClass.award_id;
      if (string.IsNullOrEmpty(name1))
      {
        ((Behaviour) component).enabled = false;
      }
      else
      {
        SpriteSheet spriteSheet = AssetManager.Load<SpriteSheet>("GuildEmblemImage/GuildEmblemes");
        if (!Object.op_Inequality((Object) spriteSheet, (Object) null))
          return;
        component.sprite = spriteSheet.GetSprite(name1);
        ((Behaviour) component).enabled = true;
      }
    }

    public enum GvGEntry
    {
      NONE,
      JOIN,
      CANCEL,
    }

    public enum GvGEntryStatus
    {
      MATCHINGPROCESS = -1, // 0xFFFFFFFF
      NOT_MATCHING = 0,
      MATCHING_OK = 1,
    }

    public enum GuildAttendStatus
    {
      UNATTENDED,
      ATTENDED,
      CAN_NOT_ATTENDED,
    }
  }
}
