// Decompiled with JetBrains decompiler
// Type: SRPG.GuildAttendanceRewardInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class GuildAttendanceRewardInfo : MonoBehaviour
  {
    [SerializeField]
    [Header("報酬リストアイテムの親")]
    private Transform RewardListRoot;
    [SerializeField]
    [Header("報酬リストアイテムのテンプレート")]
    private GameObject RewardListItemTemplate;

    private void Awake() => GameUtility.SetGameObjectActive(this.RewardListItemTemplate, false);

    private void Start()
    {
      GuildAttendParam[] guildAttendParams = MonoSingleton<GameManager>.Instance.MasterParam.GuildAttendParams;
      if (guildAttendParams == null)
        return;
      GuildAttendParam guildAttendParam1 = (GuildAttendParam) null;
      long serverTime = Network.GetServerTime();
      for (int index = 0; index < guildAttendParams.Length; ++index)
      {
        GuildAttendParam guildAttendParam2 = guildAttendParams[index];
        if (guildAttendParam2 != null && guildAttendParam2.start_at <= serverTime && serverTime < guildAttendParam2.end_at)
        {
          guildAttendParam1 = guildAttendParam2;
          break;
        }
      }
      this.Refresh(guildAttendParam1);
    }

    private void Refresh(GuildAttendParam param)
    {
      if (param == null || Object.op_Equality((Object) this.RewardListRoot, (Object) null) || Object.op_Equality((Object) this.RewardListItemTemplate, (Object) null) || param.rewards == null)
        return;
      for (int index = 0; index < param.rewards.Length; ++index)
      {
        GuildAttendRewardDetail reward = param.rewards[index];
        if (reward != null)
        {
          GuildAttendRewardParam attendRewardParam = MonoSingleton<GameManager>.Instance.MasterParam.GetGuildAttendRewardParam(reward.reward_id);
          if (attendRewardParam != null)
          {
            GameObject gameObject = Object.Instantiate<GameObject>(this.RewardListItemTemplate);
            if (Object.op_Inequality((Object) gameObject, (Object) null))
            {
              gameObject.transform.SetParent(this.RewardListRoot, false);
              GuildAttendanceRewardInfoListItem component = gameObject.GetComponent<GuildAttendanceRewardInfoListItem>();
              if (Object.op_Inequality((Object) component, (Object) null))
                component.Setup(reward.member_cnt, attendRewardParam);
            }
            GameUtility.SetGameObjectActive(gameObject, true);
          }
        }
      }
    }
  }
}
