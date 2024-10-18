// Decompiled with JetBrains decompiler
// Type: SRPG.GiftTypes
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
  }
}
