﻿// Decompiled with JetBrains decompiler
// Type: SRPG.QuestTypes
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
    Extra = 11, // 0x0B
    MultiTower = 12, // 0x0C
    Beginner = 13, // 0x0D
    MultiGps = 14, // 0x0E
    Ordeal = 15, // 0x0F
    RankMatch = 16, // 0x10
    None = 127, // 0x7F
  }
}
