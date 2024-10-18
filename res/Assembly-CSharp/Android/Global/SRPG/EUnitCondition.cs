﻿// Decompiled with JetBrains decompiler
// Type: SRPG.EUnitCondition
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public enum EUnitCondition
  {
    Poison = 1,
    Paralysed = 2,
    Stun = 4,
    Sleep = 8,
    Charm = 16, // 0x00000010
    Stone = 32, // 0x00000020
    Blindness = 64, // 0x00000040
    DisableSkill = 128, // 0x00000080
    DisableMove = 256, // 0x00000100
    DisableAttack = 512, // 0x00000200
    Zombie = 1024, // 0x00000400
    DeathSentence = 2048, // 0x00000800
    Berserk = 4096, // 0x00001000
    DisableKnockback = 8192, // 0x00002000
    DisableBuff = 16384, // 0x00004000
    DisableDebuff = 32768, // 0x00008000
    Stop = 65536, // 0x00010000
    Fast = 131072, // 0x00020000
    Slow = 262144, // 0x00040000
    AutoHeal = 524288, // 0x00080000
    Donsoku = 1048576, // 0x00100000
    Rage = 2097152, // 0x00200000
    GoodSleep = 4194304, // 0x00400000
    AutoJewel = 8388608, // 0x00800000
    DisableHeal = 16777216, // 0x01000000
    DisableSingleAttack = 33554432, // 0x02000000
    DisableAreaAttack = 67108864, // 0x04000000
    DisableDecCT = 134217728, // 0x08000000
    DisableIncCT = 268435456, // 0x10000000
  }
}
