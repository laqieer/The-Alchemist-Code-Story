// Decompiled with JetBrains decompiler
// Type: SRPG.AIFlags
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public enum AIFlags
  {
    Positioning = 1,
    Sneaking = 2,
    DisableMove = 4,
    DisableAction = 8,
    DisableSkill = 16, // 0x00000010
    DisableAvoid = 32, // 0x00000020
    CastSkillFriendlyFire = 64, // 0x00000040
    DisableJewelAttack = 128, // 0x00000080
    SelfBuffOnly = 256, // 0x00000100
    DisableTargetPriority = 512, // 0x00000200
    UseOldSort = 1024, // 0x00000400
  }
}
