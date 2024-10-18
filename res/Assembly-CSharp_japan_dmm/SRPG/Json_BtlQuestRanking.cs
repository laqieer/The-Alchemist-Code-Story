// Decompiled with JetBrains decompiler
// Type: SRPG.Json_BtlQuestRanking
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class Json_BtlQuestRanking
  {
    public int is_new_score;
    public int is_join_reward;
    public int rank;

    public bool IsNewScore => this.is_new_score == 1;

    public bool IsJoinReward => this.is_join_reward == 1;
  }
}
