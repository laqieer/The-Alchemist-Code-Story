// Decompiled with JetBrains decompiler
// Type: SRPG.Json_BtlQuestRanking
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class Json_BtlQuestRanking
  {
    public int is_new_score;
    public int is_join_reward;
    public int rank;

    public bool IsNewScore
    {
      get
      {
        return this.is_new_score == 1;
      }
    }

    public bool IsJoinReward
    {
      get
      {
        return this.is_join_reward == 1;
      }
    }
  }
}
