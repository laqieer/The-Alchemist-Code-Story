// Decompiled with JetBrains decompiler
// Type: SRPG.GiftTypes
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public enum GiftTypes : long
  {
    Item = 1,
    Gold = 2,
    Coin = 4,
    ArenaCoin = 8,
    MultiCoin = 16, // 0x0000000000000010
    KakeraCoin = 32, // 0x0000000000000020
    Artifact = 64, // 0x0000000000000040
    Unit = 128, // 0x0000000000000080
    SelectUnitItem = 256, // 0x0000000000000100
    SelectItem = 512, // 0x0000000000000200
    SelectArtifactItem = 1024, // 0x0000000000000400
    Award = 2048, // 0x0000000000000800
    ConceptCard = 4096, // 0x0000000000001000
    SelectConceptCardItem = 8192, // 0x0000000000002000
    SelectSummonTickets = 9984, // 0x0000000000002700
    IgnoreReceiveAll = 10112, // 0x0000000000002780
  }
}
