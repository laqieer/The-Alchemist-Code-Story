// Decompiled with JetBrains decompiler
// Type: SRPG.Json_PlayerDataAll
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  public class Json_PlayerDataAll
  {
    public Json_PlayerData player;
    public Json_Unit[] units;
    public Json_Item[] items;
    public Json_Mail[] mails;
    public Json_Party[] parties;
    public Json_Friend[] friends;
    public Json_Artifact[] artifacts;
    public JSON_ConceptCard[] concept_cards;
    public Json_Skin[] skins;
    public Json_Notify notify;
    public Json_MultiFuids[] fuids;
    public int status;
    public string cuid;
    public long tut;
    public int first_contact;
    public Json_Versus vs;
    public string[] tips;
    public JSON_PlayerGuild player_guild;
    public string fu_status;
    public Json_ExpireItem[] expire_items;
    public JSON_TrophyProgress[] trophyprogs;
    public JSON_TrophyProgress[] bingoprogs;
    public JSON_TrophyProgress[] guild_trophies;
    public JSON_PartyOverWrite[] party_decks;
    public string[] bgms;
    public int rune_storage;
    public int rune_storage_used;
    public JSON_StoryExChallengeCount story_ex_challenge;
    public Json_ExpansionPurchase[] expansions;
  }
}
