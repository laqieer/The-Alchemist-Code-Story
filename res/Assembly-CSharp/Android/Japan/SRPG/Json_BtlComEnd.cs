﻿// Decompiled with JetBrains decompiler
// Type: SRPG.Json_BtlComEnd
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class Json_BtlComEnd : Json_PlayerDataAll
  {
    public JSON_QuestProgress[] quests;
    public JSON_TrophyProgress[] trophyprogs;
    public Json_BtlQuestRanking quest_ranking;
    public Json_FirstClearItem[] fclr_items;
    public Json_BtlRewardConceptCard[] cards;
    public int is_mail_cards;
    public int is_quest_out_of_period;
    public BattleCore.Json_BtlInspSlot[] sins;
    public BattleCore.Json_BtlInsp[] levelup_sins;
  }
}