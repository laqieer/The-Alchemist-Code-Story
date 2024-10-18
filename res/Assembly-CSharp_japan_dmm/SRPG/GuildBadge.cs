// Decompiled with JetBrains decompiler
// Type: SRPG.GuildBadge
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class GuildBadge : MonoBehaviour
  {
    [SerializeField]
    private GameObject mBadge;
    [SerializeField]
    private GameObject mBadgeLeague;
    private int mGuildLeagueRate;
    private bool mIsIconDisp;

    private void Start()
    {
      GameUtility.SetGameObjectActive(this.mBadge, false);
      GameUtility.SetGameObjectActive(this.mBadgeLeague, false);
      this.mGuildLeagueRate = MonoSingleton<GameManager>.Instance.Player.GuildLeagueRate;
    }

    private void Update()
    {
      bool flag = this.IsBadgeActive();
      if (Object.op_Inequality((Object) this.mBadge, (Object) null) && this.mBadge.GetActive() != flag)
        this.mBadge.SetActive(flag);
      if (Object.op_Inequality((Object) this.mBadgeLeague, (Object) null) && this.mBadgeLeague.GetActive() != flag)
        this.mBadgeLeague.SetActive(flag);
      if (!this.IsUpdate())
        return;
      GameParameter.UpdateAll(((Component) this).gameObject);
    }

    private bool IsBadgeActive()
    {
      return MonoSingleton<GameManager>.Instance.Player.PlayerGuild != null && MonoSingleton<GameManager>.Instance.Player.PlayerGuild.IsJoined && (MonoSingleton<GameManager>.Instance.Player.GuildTrophyData.HasRewards || (MonoSingleton<GameManager>.Instance.Player.PlayerGuild.IsGuildMaster || MonoSingleton<GameManager>.Instance.Player.PlayerGuild.IsSubGuildMaster) && (GuildManager.NotifyEntryRequestCount > 0 || MonoSingleton<GameManager>.Instance.Player.HasGuildReward));
    }

    private bool IsUpdate()
    {
      if (MonoSingleton<GameManager>.Instance.Player == null)
        return false;
      if (MonoSingleton<GameManager>.Instance.Player.GuildLeagueRate == 0 && this.mIsIconDisp != MonoSingleton<GameManager>.Instance.Player.IsGuildGvGJoin)
      {
        this.mIsIconDisp = MonoSingleton<GameManager>.Instance.Player.IsGuildGvGJoin;
        return true;
      }
      if (this.mGuildLeagueRate == MonoSingleton<GameManager>.Instance.Player.GuildLeagueRate)
        return false;
      this.mGuildLeagueRate = MonoSingleton<GameManager>.Instance.Player.GuildLeagueRate;
      return true;
    }
  }
}
