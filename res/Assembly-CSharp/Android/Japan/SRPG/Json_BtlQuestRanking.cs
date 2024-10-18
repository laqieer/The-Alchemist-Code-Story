// Decompiled with JetBrains decompiler
// Type: SRPG.Json_BtlQuestRanking
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
