// Decompiled with JetBrains decompiler
// Type: SRPG.Json_TrophyPlayerDataAll
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  public class Json_TrophyPlayerDataAll
  {
    public Json_TrophyPlayerData player;
    public Json_Unit[] units;
    public Json_Item[] items;
    public Json_TrophyConceptCards concept_cards;
    public ReqTrophyStarMission.StarMission star_mission;
  }
}
