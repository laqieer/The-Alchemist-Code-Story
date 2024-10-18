﻿// Decompiled with JetBrains decompiler
// Type: SRPG.EUnitFlag
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public enum EUnitFlag
  {
    Entried = 1,
    Moved = 2,
    Action = 4,
    Searched = 8,
    Defended = 16, // 0x00000010
    SideAttack = 32, // 0x00000020
    BackAttack = 64, // 0x00000040
    Escaped = 128, // 0x00000080
    Sneaking = 256, // 0x00000100
    Paralysed = 512, // 0x00000200
    ForceMoved = 1024, // 0x00000400
    ForceEntried = 2048, // 0x00000800
    ForceAuto = 4096, // 0x00001000
    EntryDead = 8192, // 0x00002000
    FirstAction = 16384, // 0x00004000
    DisableFirstVoice = 32768, // 0x00008000
    DamagedActionStart = 65536, // 0x00010000
    TriggeredAutoSkills = 131072, // 0x00020000
    DisableUnitChange = 262144, // 0x00040000
    UnitChanged = 524288, // 0x00080000
    UnitWithdraw = 1048576, // 0x00100000
    CreatedBreakObj = 2097152, // 0x00200000
    Reinforcement = 4194304, // 0x00400000
    ToDying = 8388608, // 0x00800000
    OtherTeam = 16777216, // 0x01000000
    IsHelp = 33554432, // 0x02000000
    IsDynamicTransform = 67108864, // 0x04000000
    InfinitySpawn = 134217728, // 0x08000000
    UnitTransformed = 268435456, // 0x10000000
    IsDtuExec = 536870912, // 0x20000000
    IsDtuUscInitialized = 1073741824, // 0x40000000
  }
}
