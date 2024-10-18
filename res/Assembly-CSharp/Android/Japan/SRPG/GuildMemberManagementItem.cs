// Decompiled with JetBrains decompiler
// Type: SRPG.GuildMemberManagementItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  public class GuildMemberManagementItem : MonoBehaviour
  {
    [SerializeField]
    private GameObject mKickButton;
    [SerializeField]
    private GameObject mChangeRoleButton;
    private GuildMemberData mMemberData;

    public void Init(GuildMemberData member_data)
    {
      GuildMemberData.eRole roleId = MonoSingleton<GameManager>.Instance.Player.PlayerGuild.RoleId;
      this.mMemberData = member_data;
      switch (roleId)
      {
        case GuildMemberData.eRole.MASTAER:
          this.SetupUI_Master();
          break;
        case GuildMemberData.eRole.MEMBER:
          this.SetupUI_Member();
          break;
        case GuildMemberData.eRole.SUB_MASTAER:
          this.SetupUI_SubMaster();
          break;
        default:
          this.SetupUI_Member();
          break;
      }
    }

    private void SetupUI_Master()
    {
      this.mKickButton.SetActive(true);
      this.mChangeRoleButton.SetActive(true);
      if (!(this.mMemberData.Uid == MonoSingleton<GameManager>.Instance.DeviceId))
        return;
      this.mKickButton.SetActive(false);
      this.mChangeRoleButton.SetActive(false);
    }

    private void SetupUI_SubMaster()
    {
      this.mKickButton.SetActive(true);
      this.mChangeRoleButton.SetActive(false);
      if (this.mMemberData.Uid == MonoSingleton<GameManager>.Instance.DeviceId)
        this.mKickButton.SetActive(false);
      if (this.mMemberData.RoleId == GuildMemberData.eRole.MASTAER)
        this.mKickButton.SetActive(false);
      if (this.mMemberData.RoleId != GuildMemberData.eRole.SUB_MASTAER)
        return;
      this.mKickButton.SetActive(false);
    }

    private void SetupUI_Member()
    {
      this.mKickButton.SetActive(false);
      this.mChangeRoleButton.SetActive(false);
    }
  }
}
