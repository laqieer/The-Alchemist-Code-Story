// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.Quests.IQuestMilestone
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace GooglePlayGames.BasicApi.Quests
{
  public interface IQuestMilestone
  {
    string Id { get; }

    string EventId { get; }

    string QuestId { get; }

    ulong CurrentCount { get; }

    ulong TargetCount { get; }

    byte[] CompletionRewardData { get; }

    MilestoneState State { get; }
  }
}
