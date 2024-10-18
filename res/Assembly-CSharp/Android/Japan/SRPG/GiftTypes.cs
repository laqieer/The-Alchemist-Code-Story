// Decompiled with JetBrains decompiler
// Type: SRPG.GiftTypes
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
