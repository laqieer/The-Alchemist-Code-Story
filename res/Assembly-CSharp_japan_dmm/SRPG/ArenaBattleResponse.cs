// Decompiled with JetBrains decompiler
// Type: SRPG.ArenaBattleResponse
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class ArenaBattleResponse
  {
    public Json_ArenaRewardInfo reward_info;
    public int new_rank;
    public int def_rank;
    public int got_pexp;
    public int got_uexp;
    public int got_gold;

    public void Deserialize(Json_ArenaBattleResponse json)
    {
      this.new_rank = json != null ? json.new_rank : throw new InvalidJSONException();
      this.def_rank = json.def_rank;
      this.got_pexp = json.got_pexp;
      this.got_uexp = json.got_uexp;
      this.got_gold = json.got_gold;
      this.reward_info = new Json_ArenaRewardInfo();
      if (json.reward_info == null)
        return;
      this.reward_info.gold = json.reward_info.gold;
      this.reward_info.coin = json.reward_info.coin;
      this.reward_info.arenacoin = json.reward_info.arenacoin;
    }
  }
}
