// Decompiled with JetBrains decompiler
// Type: SRPG.QuestTypes
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public enum QuestTypes : byte
  {
    Story = 0,
    Multi = 1,
    Arena = 2,
    Tutorial = 3,
    Free = 4,
    Event = 5,
    Character = 6,
    Tower = 7,
    VersusFree = 8,
    VersusRank = 9,
    Gps = 10, // 0x0A
    StoryExtra = 11, // 0x0B
    MultiTower = 12, // 0x0C
    Beginner = 13, // 0x0D
    MultiGps = 14, // 0x0E
    Ordeal = 15, // 0x0F
    RankMatch = 16, // 0x10
    Raid = 17, // 0x11
    GenesisStory = 18, // 0x12
    GenesisBoss = 19, // 0x13
    AdvanceStory = 20, // 0x14
    AdvanceBoss = 21, // 0x15
    UnitRental = 22, // 0x16
    GuildRaid = 23, // 0x17
    GvG = 24, // 0x18
    WorldRaid = 25, // 0x19
    None = 127, // 0x7F
  }
}
