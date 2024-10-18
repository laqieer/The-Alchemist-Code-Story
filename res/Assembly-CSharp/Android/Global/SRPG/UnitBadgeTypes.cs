// Decompiled with JetBrains decompiler
// Type: SRPG.UnitBadgeTypes
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Flags]
  public enum UnitBadgeTypes
  {
    EnableEquipment = 1,
    EnableAwaking = 2,
    EnableRarityUp = 4,
    EnableJobRankUp = 8,
    EnableClassChange = 16, // 0x00000010
  }
}
