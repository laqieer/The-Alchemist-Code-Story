// Decompiled with JetBrains decompiler
// Type: SRPG.GuildMemberManagementItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
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
