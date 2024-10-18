// Decompiled with JetBrains decompiler
// Type: SRPG.GuildLobby
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "初期化", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "表示更新", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(3, "ギルド施設強化を開く", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(110, "ギルマスです", FlowNode.PinTypes.Output, 110)]
  [FlowNode.Pin(120, "サブマスです", FlowNode.PinTypes.Output, 120)]
  [FlowNode.Pin(130, "メンバーです", FlowNode.PinTypes.Output, 130)]
  [FlowNode.Pin(140, "ギルド施設強化を開く", FlowNode.PinTypes.Output, 140)]
  [FlowNode.Pin(150, "ギルドに加入していない", FlowNode.PinTypes.Output, 150)]
  public class GuildLobby : MonoBehaviour, IFlowInterface
  {
    private const int PIN_INPUT_INIT = 1;
    private const int PIN_INPUT_REFRESH = 2;
    private const int PIN_INPUT_GUILD_FACILITY_ENHANCE = 3;
    private const int PIN_OUTPUT_GUILD_MASTER = 110;
    private const int PIN_OUTPUT_SUB_GUILD_MASTER = 120;
    private const int PIN_OUTPUT_GUILD_MEMBER = 130;
    private const int PIN_OUTPUT_GUILD_FACILITY_ENHANCE = 140;
    private const int PIN_OUTPUT_GUILD_ERROR_NOT_ENTRY = 150;
    private static GuildLobby mInstance;
    [SerializeField]
    private GameObject mWindow;
    [SerializeField]
    private GameObject mGuildInfo;
    private SerializeValueBehaviour mSerializeValueBehaviour;

    public static GuildLobby Instance
    {
      get
      {
        return GuildLobby.mInstance;
      }
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          this.Init();
          break;
        case 2:
          this.Refresh();
          break;
        case 3:
          this.OpenGuildFacilityEnhanceUI();
          break;
      }
    }

    private void Awake()
    {
      GuildLobby.mInstance = this;
      if ((UnityEngine.Object) this.mWindow != (UnityEngine.Object) null)
        this.mWindow.SetActive(false);
      this.mSerializeValueBehaviour = this.GetComponent<SerializeValueBehaviour>();
    }

    private void Init()
    {
      if ((UnityEngine.Object) this.mWindow != (UnityEngine.Object) null)
        this.mWindow.SetActive(true);
      this.Refresh();
    }

    public void Refresh()
    {
      if (MonoSingleton<GameManager>.Instance.Player.Guild == null || MonoSingleton<GameManager>.Instance.Player.PlayerGuild == null || !MonoSingleton<GameManager>.Instance.Player.PlayerGuild.IsJoined)
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 150);
      }
      else
      {
        this.mSerializeValueBehaviour.list.SetObject(GuildSVB_Key.GUILD, (object) MonoSingleton<GameManager>.Instance.Player.Guild);
        DataSource.Bind<GuildData>(this.gameObject, MonoSingleton<GameManager>.Instance.Player.Guild, false);
        DataSource.Bind<UnitData>(this.mGuildInfo, MonoSingleton<GameManager>.Instance.Player.Guild.GuildMaster.Unit, false);
        DataSource.Bind<GuildMemberData>(this.mGuildInfo, MonoSingleton<GameManager>.Instance.Player.Guild.GuildMaster, false);
        GameParameter.UpdateAll(this.gameObject);
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
      if (MonoSingleton<GameManager>.Instance.Player.Guild.CreatedUid != MonoSingleton<GameManager>.Instance.DeviceId && Network.GetServerTime() < MonoSingleton<GameManager>.Instance.Player.PlayerGuild.JoinedAt + (long) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.GuildInvestCoolTime)
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.GUILD_TEXT_CANT_INVEST_FACILITY"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      else
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 140);
    }
  }
}
