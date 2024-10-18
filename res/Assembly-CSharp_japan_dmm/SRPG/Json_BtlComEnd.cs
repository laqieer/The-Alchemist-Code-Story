// Decompiled with JetBrains decompiler
// Type: SRPG.Json_BtlComEnd
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class Json_BtlComEnd : Json_PlayerDataAll
  {
    public JSON_QuestProgress[] quests;
    public Json_BtlQuestRanking quest_ranking;
    public Json_FirstClearItem[] fclr_items;
    public Json_BtlRewardConceptCard[] cards;
    public int is_mail_cards;
    public int is_quest_out_of_period;
    public BattleCore.Json_BtlInspSlot[] sins;
    public BattleCore.Json_BtlInsp[] levelup_sins;
    public int guildraid_bp_charge;
    public Json_RuneData[] runes_detail;
  }
}
