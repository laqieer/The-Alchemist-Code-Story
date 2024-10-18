// Decompiled with JetBrains decompiler
// Type: SRPG.GuildConfirm
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "初期化", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "ギルド加入申請", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(3, "ギルド加入申請を承認", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(4, "ギルド加入申請を拒否", FlowNode.PinTypes.Input, 4)]
  [FlowNode.Pin(5, "ギルドメンバー追放", FlowNode.PinTypes.Input, 5)]
  [FlowNode.Pin(6, "ギルド脱退", FlowNode.PinTypes.Input, 6)]
  [FlowNode.Pin(7, "ギルド加入申請一括拒否", FlowNode.PinTypes.Input, 7)]
  [FlowNode.Pin(101, "ギルド加入申請リクエスト", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "ギルド加入申請を承認完了", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(103, "ギルド加入申請を拒否完了", FlowNode.PinTypes.Output, 103)]
  [FlowNode.Pin(104, "ギルドメンバー追放完了", FlowNode.PinTypes.Output, 104)]
  [FlowNode.Pin(105, "ギルド脱退完了", FlowNode.PinTypes.Output, 105)]
  [FlowNode.Pin(106, "ギルド加入申請一括拒否完了", FlowNode.PinTypes.Output, 106)]
  public class GuildConfirm : MonoBehaviour, IFlowInterface
  {
    private const int PIN_INPUT_INIT = 1;
    private const int PIN_INPUT_START_REQUEST_ENTRY = 2;
    private const int PIN_INPUT_START_GUILD_REQUEST_ENTRY_APPROVAL = 3;
    private const int PIN_INPUT_START_GUILD_REQUEST_ENTRY_REJECT = 4;
    private const int PIN_INPUT_START_GUILD_MEMBER_KICK = 5;
    private const int PIN_INPUT_START_GUILD_LEAVE = 6;
    private const int PIN_INPUT_START_GUILD_REQUEST_NG_ALL = 7;
    private const int PIN_OUTPUT_END_REQUEST_ENTRY = 101;
    private const int PIN_OUTPUT_END_GUILD_REQUEST_ENTRY_APPROVAL = 102;
    private const int PIN_OUTPUT_END_GUILD_REQUEST_ENTRY_REJECT = 103;
    private const int PIN_OUTPUT_END_GUILD_MEMBER_KICK = 104;
    private const int PIN_OUTPUT_END_GUILD_LEAVE = 105;
    private const int PIN_OUTPUT_END_GUILD_REQUEST_NG_ALL = 106;
    [SerializeField]
    private GameObject mEntryRequestRootObject;
    [SerializeField]
    private GameObject mEntryRequestObject;
    [SerializeField]
    private GameObject mEntryRequestFailedObject;
    [SerializeField]
    private GameObject mEntryRequestApprovalRootObject;
    [SerializeField]
    private GameObject mEntryRequestApprovalOKObject;
    [SerializeField]
    private GameObject mEntryRequestApprovalNGObject;
    [SerializeField]
    private GameObject mMemberKickObject;
    [SerializeField]
    private GameObject mLeaveObject;
    [SerializeField]
    private GameObject mEntryRequestNGAll;
    [SerializeField]
    private GameObject mLeaveConfirmObject;
    [SerializeField]
    private GameObject mDontLeaveMessageObject;
    private static GuildConfirm mInstance;
    private GuildData mTargetGuild;
    private GuildMemberData mTargetMember;
    private List<GuildMemberData> mTargetEntryRequests;

    public static GuildConfirm Instance => GuildConfirm.mInstance;

    public GuildData TargetGuild => this.mTargetGuild;

    public GuildMemberData TargetMember => this.mTargetMember;

    public List<GuildMemberData> TargetEntryRequests => this.mTargetEntryRequests;

    private void Awake() => GuildConfirm.mInstance = this;

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          this.Init();
          break;
        case 2:
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
          break;
        case 3:
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
          break;
        case 4:
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 103);
          break;
        case 5:
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 104);
          break;
        case 6:
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 105);
          break;
        case 7:
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 106);
          break;
      }
    }

    private void Init()
    {
      this.mTargetGuild = (GuildData) null;
      this.mTargetMember = (GuildMemberData) null;
      SerializeValueBehaviour component = ((Component) this).GetComponent<SerializeValueBehaviour>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      this.mTargetGuild = component.list.GetObject<GuildData>(GuildSVB_Key.GUILD);
      this.mTargetMember = component.list.GetObject<GuildMemberData>(GuildSVB_Key.MEMBER);
      GuildConfirm.eConfirmType confirmType = this.GetConfirmType();
      DataSource.Bind<GuildData>(((Component) this).gameObject, this.mTargetGuild);
      DataSource.Bind<GuildMemberData>(((Component) this).gameObject, this.mTargetMember);
      this.SetupUI(confirmType);
    }

    public static void SetConfirmType(GameObject svb_obj, GuildConfirm.eConfirmType type)
    {
      if (Object.op_Equality((Object) svb_obj, (Object) null))
        return;
      SerializeValueBehaviour component = svb_obj.GetComponent<SerializeValueBehaviour>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      component.list.SetObject(GuildSVB_Key.CONFIRM, (object) type);
    }

    private GuildConfirm.eConfirmType GetConfirmType()
    {
      SerializeValueBehaviour component = ((Component) this).GetComponent<SerializeValueBehaviour>();
      return Object.op_Inequality((Object) component, (Object) null) ? component.list.GetObject<GuildConfirm.eConfirmType>(GuildSVB_Key.CONFIRM, GuildConfirm.eConfirmType.NONE) : GuildConfirm.eConfirmType.NONE;
    }

    private void SetupUI(GuildConfirm.eConfirmType confirm_type)
    {
      this.HideAll();
      switch (confirm_type)
      {
        case GuildConfirm.eConfirmType.ENTRY_REQUEST:
          this.Confirm_EntryRequest();
          break;
        case GuildConfirm.eConfirmType.ENTRY_REQUEST_APPROVAL_OK:
          this.Confirm_EntryRequestApprovalOK();
          break;
        case GuildConfirm.eConfirmType.ENTRY_REQUEST_APPROVAL_NG:
          this.Confirm_EntryRequestApprovalNG();
          break;
        case GuildConfirm.eConfirmType.MEMBER_KICK:
          this.Confirm_MemberKick();
          break;
        case GuildConfirm.eConfirmType.LEAVE:
          this.Confirm_Leave();
          break;
        case GuildConfirm.eConfirmType.ENTRY_REQUEST_NG_ALL:
          this.Confirm_EntryRequestNGAll();
          break;
      }
      GameParameter.UpdateAll(((Component) this).gameObject);
    }

    private void HideAll()
    {
      this.mEntryRequestRootObject.SetActive(false);
      this.mEntryRequestObject.SetActive(false);
      this.mEntryRequestFailedObject.SetActive(false);
      this.mEntryRequestApprovalRootObject.SetActive(false);
      this.mEntryRequestApprovalOKObject.SetActive(false);
      this.mEntryRequestApprovalNGObject.SetActive(false);
      this.mMemberKickObject.SetActive(false);
      this.mLeaveObject.SetActive(false);
    }

    private void Confirm_EntryRequest()
    {
      if (this.mTargetGuild == null)
        return;
      this.mEntryRequestRootObject.SetActive(true);
      if (this.mTargetGuild.IsMatchConditions(MonoSingleton<GameManager>.Instance.Player))
        this.mEntryRequestObject.SetActive(true);
      else
        this.mEntryRequestFailedObject.SetActive(true);
    }

    private void Confirm_EntryRequestApprovalOK()
    {
      this.mEntryRequestApprovalRootObject.SetActive(true);
      this.mEntryRequestApprovalOKObject.SetActive(true);
      this.mEntryRequestApprovalNGObject.SetActive(true);
      DataSource.Bind<GuildMemberData>(((Component) this).gameObject, this.mTargetMember);
      DataSource.Bind<UnitData>(((Component) this).gameObject, this.mTargetMember.Unit);
      GameParameter.UpdateAll(((Component) this).gameObject);
    }

    private void Confirm_EntryRequestApprovalNG()
    {
      this.mEntryRequestApprovalRootObject.SetActive(true);
      this.mEntryRequestApprovalNGObject.SetActive(true);
      DataSource.Bind<GuildMemberData>(((Component) this).gameObject, this.mTargetMember);
      DataSource.Bind<UnitData>(((Component) this).gameObject, this.mTargetMember.Unit);
      GameParameter.UpdateAll(((Component) this).gameObject);
    }

    private void Confirm_MemberKick()
    {
      this.mMemberKickObject.SetActive(true);
      DataSource.Bind<GuildMemberData>(((Component) this).gameObject, this.mTargetMember);
      DataSource.Bind<UnitData>(((Component) this).gameObject, this.mTargetMember.Unit);
      GameParameter.UpdateAll(((Component) this).gameObject);
    }

    private void Confirm_Leave()
    {
      this.mLeaveObject.SetActive(true);
      this.mLeaveConfirmObject.SetActive(false);
      this.mDontLeaveMessageObject.SetActive(false);
      GameObject gameObject = this.mLeaveConfirmObject;
      if (MonoSingleton<GameManager>.Instance.Player.PlayerGuild.RoleId == GuildMemberData.eRole.MASTAER && MonoSingleton<GameManager>.Instance.Player.Guild.MemberCount > 1)
        gameObject = this.mDontLeaveMessageObject;
      gameObject.SetActive(true);
    }

    private void Confirm_EntryRequestNGAll()
    {
      this.mTargetEntryRequests = new List<GuildMemberData>((IEnumerable<GuildMemberData>) GuildManager.Instance.EntryRequests);
      this.mEntryRequestNGAll.SetActive(true);
    }

    public enum eConfirmType
    {
      NONE,
      ENTRY_REQUEST,
      ENTRY_REQUEST_APPROVAL_OK,
      ENTRY_REQUEST_APPROVAL_NG,
      MEMBER_KICK,
      LEAVE,
      ENTRY_REQUEST_NG_ALL,
    }
  }
}
