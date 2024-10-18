// Decompiled with JetBrains decompiler
// Type: SRPG.UnitBadgeTypes
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
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
