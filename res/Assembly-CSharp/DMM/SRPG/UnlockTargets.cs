// Decompiled with JetBrains decompiler
// Type: SRPG.UnlockTargets
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  [Flags]
  public enum UnlockTargets : long
  {
    Shop = 1,
    Cave = 2,
    Tour = 4,
    Tower = 8,
    Arena = 16, // 0x0000000000000010
    ShopTabi = 32, // 0x0000000000000020
    ShopKimagure = 64, // 0x0000000000000040
    ShopMonozuki = 128, // 0x0000000000000080
    MultiPlay = 256, // 0x0000000000000100
    UnitAwaking = 512, // 0x0000000000000200
    UnitEvolution = 1024, // 0x0000000000000400
    EnhanceEquip = 2048, // 0x0000000000000800
    EnhanceAbility = 4096, // 0x0000000000001000
    Artifact = 8192, // 0x0000000000002000
    ShopAwakePiece = 16384, // 0x0000000000004000
    LimitedShop = 32768, // 0x0000000000008000
    MultiVS = 65536, // 0x0000000000010000
    Ordeal = 131072, // 0x0000000000020000
    Gallery = 262144, // 0x0000000000040000
    ConceptCard = 524288, // 0x0000000000080000
    KeyQuest = 1048576, // 0x0000000000100000
    EventShop = 2097152, // 0x0000000000200000
    TowerQuest = 4194304, // 0x0000000000400000
    RankMatch = 8388608, // 0x0000000000800000
    Guild = 16777216, // 0x0000000001000000
    Archive = 33554432, // 0x0000000002000000
    Raid = 67108864, // 0x0000000004000000
    EventQuest = 134217728, // 0x0000000008000000
    SeisekiQuest = 268435456, // 0x0000000010000000
    BabelQuest = 536870912, // 0x0000000020000000
    ChallengeQuest = 1073741824, // 0x0000000040000000
    CharacterQuest = 2147483648, // 0x0000000080000000
    GuildRaid = 4294967296, // 0x0000000100000000
    AutoRepeatQuest = 8589934592, // 0x0000000200000000
    Rune = 17179869184, // 0x0000000400000000
    WorldRaid = 34359738368, // 0x0000000800000000
  }
}
