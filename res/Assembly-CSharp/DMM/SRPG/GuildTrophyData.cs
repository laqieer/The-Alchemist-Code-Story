// Decompiled with JetBrains decompiler
// Type: SRPG.GuildTrophyData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;

#nullable disable
namespace SRPG
{
  public class GuildTrophyData : TrophyBase
  {
    private bool mHasRewards;

    public bool HasRewards => this.mHasRewards;

    public void OverwriteGuildTrophyProgress(
      JSON_TrophyProgress[] trophyProgressList,
      bool is_notify_send = true)
    {
      if (trophyProgressList == null)
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      for (int index = 0; index < trophyProgressList.Length; ++index)
      {
        JSON_TrophyProgress trophyProgress = trophyProgressList[index];
        if (trophyProgress != null)
        {
          TrophyParam guildTrophy = instance.MasterParam.GetGuildTrophy(trophyProgress.iname);
          if (guildTrophy == null)
          {
            DebugUtility.LogWarning("存在しないミッション:" + trophyProgress.iname);
          }
          else
          {
            TrophyState trophyCounter = this.GetTrophyCounter(guildTrophy);
            bool flag = trophyCounter.IsEnded || trophyCounter.IsCompleted;
            trophyCounter.Setup(guildTrophy, trophyProgress);
            if (!trophyCounter.IsEnded && !flag && trophyCounter.IsCompleted && is_notify_send)
              NotifyList.PushTrophy(guildTrophy);
          }
        }
      }
      this.UpdateHasRewards();
    }

    public void UpdateHasRewards() => this.mHasRewards = this.GetCompletedTrophiesCount() > 0;
  }
}
