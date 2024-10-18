// Decompiled with JetBrains decompiler
// Type: SRPG.GuildRoleBonusRewardInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class GuildRoleBonusRewardInfo : MonoBehaviour
  {
    [SerializeField]
    [Header("報酬リストアイテムの親")]
    private Transform RewardListRoot;
    [SerializeField]
    [Header("報酬リストアイテムのテンプレート")]
    private GameObject RewardListItemTemplate;
    [SerializeField]
    [Header("現在の施設本部Lvテキスト")]
    private Text GuildCurrentFacilityLvText;

    private void Awake() => GameUtility.SetGameObjectActive(this.RewardListItemTemplate, false);

    private void Start()
    {
      GuildRoleBonusParam[] guildRoleBonusParams = MonoSingleton<GameManager>.Instance.MasterParam.GuildRoleBonusParams;
      if (guildRoleBonusParams == null)
        return;
      GuildMemberData.eRole roleId = MonoSingleton<GameManager>.Instance.Player.PlayerGuild.RoleId;
      GuildRoleBonusParam guildRoleBonusParam1 = (GuildRoleBonusParam) null;
      long serverTime = Network.GetServerTime();
      for (int index = 0; index < guildRoleBonusParams.Length; ++index)
      {
        GuildRoleBonusParam guildRoleBonusParam2 = guildRoleBonusParams[index];
        if (guildRoleBonusParam2 != null && guildRoleBonusParam2.role == roleId && guildRoleBonusParam2.start_at <= serverTime && serverTime < guildRoleBonusParam2.end_at)
        {
          guildRoleBonusParam1 = guildRoleBonusParam2;
          break;
        }
      }
      this.Refresh(guildRoleBonusParam1);
    }

    private void Refresh(GuildRoleBonusParam param)
    {
      if (param == null || param.rewards == null || Object.op_Equality((Object) this.RewardListRoot, (Object) null) || Object.op_Equality((Object) this.RewardListItemTemplate, (Object) null))
        return;
      GuildFacilityData[] facilities = MonoSingleton<GameManager>.Instance.Player.Guild.Facilities;
      int num = 1;
      if (facilities == null)
        return;
      for (int index = 0; index < facilities.Length; ++index)
      {
        GuildFacilityData guildFacilityData = facilities[index];
        if (guildFacilityData != null && guildFacilityData.Param.Type == GuildFacilityParam.eFacilityType.BASE_CAMP)
        {
          num = guildFacilityData.Level;
          break;
        }
      }
      if (Object.op_Inequality((Object) this.GuildCurrentFacilityLvText, (Object) null))
        this.GuildCurrentFacilityLvText.text = num.ToString();
      if (param.rewards == null)
        return;
      for (int index = 0; index < param.rewards.Length; ++index)
      {
        GuildRoleBonusDetail reward = param.rewards[index];
        if (reward != null)
        {
          GuildRoleBonusRewardParam bonusRewardParam = MonoSingleton<GameManager>.Instance.MasterParam.GetGuildRoleBonusRewardParam(reward.reward_id);
          if (bonusRewardParam != null)
          {
            GameObject gameObject = Object.Instantiate<GameObject>(this.RewardListItemTemplate);
            if (Object.op_Inequality((Object) gameObject, (Object) null))
            {
              gameObject.transform.SetParent(this.RewardListRoot, false);
              GuildRoleBonusRewardInfoListItem component = gameObject.GetComponent<GuildRoleBonusRewardInfoListItem>();
              if (Object.op_Inequality((Object) component, (Object) null))
                component.Setup(reward.lv, bonusRewardParam);
            }
            GameUtility.SetGameObjectActive(gameObject, true);
          }
        }
      }
    }
  }
}
